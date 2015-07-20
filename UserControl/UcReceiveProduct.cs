using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAzure.Storage.Table;
using XPTable.Models;
using System.Threading;
using System.IO;

namespace PowerPOS_Online
{
    public partial class UcReceiveProduct : UserControl
    {
        private int _QTY = 0;
        private int _RECEIVED = 0;
        private bool _FIRST_LOAD;

        public UcReceiveProduct()
        {
            InitializeComponent();
        }

        private void UcReceiveProduct_Load(object sender, EventArgs e)
        {
            Util.InitialTable(table1);
            _FIRST_LOAD = true;
            LoadData();
        }

        private void LoadData()
        {
            DataTable dt = Util.DBQuery(@"SELECT DISTINCT OrderNo FROM Barcode WHERE ReceivedDate IS NULL ORDER BY OrderNo");
            cbbOrderNo.Items.Clear();
            cbbOrderNo.Items.Add("เลขที่ใบสั่งซื้อ");
            if (dt.Rows.Count == 0)
            {
                cbbOrderNo.Enabled = false;
            }
            else
            {
                cbbOrderNo.Enabled = true;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cbbOrderNo.Items.Add(dt.Rows[i]["OrderNo"].ToString());
                }
            }
            cbbOrderNo.SelectedIndex = 0;

            _FIRST_LOAD = false;
        }

        private void cbbOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ptbProduct.Visible = false;
            SearchData();
        }

        private void SearchData()
        {
            if (cbbOrderNo.SelectedIndex == 0)
            {
                table1.BeginUpdate();
                tableModel1.Rows.Clear();
                table1.EndUpdate();

                gbOrderNo.Height = 69;
                progressBar1.Visible = false;
                gbCost.Visible = false;

            }
            else if (!_FIRST_LOAD)
            {
                _QTY = 0;
                _RECEIVED = 0;
                DataTable dt = Util.DBQuery(string.Format(@"
                    SELECT DISTINCT b.Product, p.Name, COUNT(*) ProductCount, IFNULL(r.ReceivedCount, 0) ReceivedCount
                    FROM Barcode b
                        LEFT JOIN Product p
                            ON b.Product = p.ID
                            AND p.Shop = '{0}'
                        LEFT JOIN ( 
                                SELECT DISTINCT Product, COUNT(*) ReceivedCount 
                                FROM Barcode 
                                WHERE ReceivedDate IS NOT NULL 
                                AND ReceivedBy = '' 
                                AND OrderNo =  '{1}' 
                                GROUP BY Product
                        ) r
                            ON b.Product = r.Product
                    WHERE (b.ReceivedDate IS NULL OR b.ReceivedBy = '')
                        AND b.OrderNo = '{1}'
                    GROUP BY b.Product
                    ORDER BY p.Name
                ", Param.ShopId, cbbOrderNo.SelectedItem.ToString()));
                table1.BeginUpdate();
                tableModel1.Rows.Clear();
                tableModel1.RowHeight = 22;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var progress = float.Parse(dt.Rows[i]["ReceivedCount"].ToString()) / float.Parse(dt.Rows[i]["ProductCount"].ToString()) * 100.0f;
                    tableModel1.Rows.Add(new Row(
                        new Cell[] {
                        new Cell("" + (i+1)),
                        new Cell(dt.Rows[i]["Product"].ToString()),
                        new Cell(dt.Rows[i]["Name"].ToString()),
                        new Cell(int.Parse(dt.Rows[i]["ProductCount"].ToString()).ToString("#,##0")),
                        new Cell(int.Parse(dt.Rows[i]["ReceivedCount"].ToString()).ToString("#,##0")),
                        new Cell((int)progress)
                        }));
                    _QTY += int.Parse(dt.Rows[i]["ProductCount"].ToString());
                    _RECEIVED += int.Parse(dt.Rows[i]["ReceivedCount"].ToString());
                }
                table1.EndUpdate();

                ptbProduct.Visible = false;
                if (_QTY == 0 || _RECEIVED == 0)
                {
                    gbOrderNo.Height = 69;
                    progressBar1.Visible = false;
                    gbCost.Visible = false;
                }
                else if (_RECEIVED != 0)
                {
                    gbCost.Visible = true;
                    gbOrderNo.Height = 96;
                    progressBar1.Visible = true;
                    progressBar1.Maximum = _QTY;
                    progressBar1.Value = _RECEIVED;
                }
            }
            txtBarcode.Focus();
        }

        private void table1_EndSort(object sender, XPTable.Events.ColumnEventArgs e)
        {
            for (int i = 0; i < table1.TableModel.Rows.Count; i++)
            {
                table1.TableModel.Rows[i].Cells[0].Text = (i + 1).ToString();
            }
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                DataTable dt = Util.DBQuery(string.Format(@"SELECT OrderNo, p.ID, p.CoverImage, IFNULL(ReceivedDate, '') ReceivedDate 
                    FROM Barcode b LEFT JOIN Product p ON b.product = p.id
                    WHERE Barcode = '{0}'", txtBarcode.Text));

                lblStatus.Visible = true;

                if (dt.Rows.Count == 0)
                {
                    lblStatus.ForeColor = Color.Red;
                    lblStatus.Text = "ไม่พบข้อมูลสินค้าชิ้นนี้ในระบบ";
                    //_SKU = "0";
                }
                else
                {
                    Param.ProductId = dt.Rows[0]["ID"].ToString();

                    if (cbbOrderNo.SelectedItem.ToString() != dt.Rows[0]["OrderNo"].ToString())
                    {
                        cbbOrderNo.SelectedIndex = cbbOrderNo.FindString(dt.Rows[0]["OrderNo"].ToString());
                    }

                    if (dt.Rows[0]["ReceivedDate"].ToString() != "")
                    {
                        lblStatus.ForeColor = Color.Red;
                        lblStatus.Text = "เคยรับสินค้าชิ้นนี้เข้าระบบแล้ว";
                        SearchData();
                    }
                    else
                    {
                        Util.DBExecute(string.Format(@"UPDATE Barcode SET ReceivedDate = STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), receivedBy = '{1}', Sync = 1
                            WHERE Barcode = '{0}'", txtBarcode.Text, ""));
                        SearchData();

                        lblStatus.ForeColor = Color.Green;
                        lblStatus.Text = "รับสินค้าเข้าระบบเรียบร้อยแล้ว";

                        ptbProduct.Visible = true;
                        ptbProduct.Image = null;

                        var filename = @"Resource/Images/Product/" + Param.ProductId + ".jpg";
                        if (!File.Exists(filename))
                        {
                            if (dt.Rows[0]["CoverImage"].ToString() != "")
                            {
                                DownloadImage(dt.Rows[0]["CoverImage"].ToString(), @"Resource/Images/Product/", Param.ProductId + ".jpg");
                            }
                        }
                        else
                        {
                            try { ptbProduct.Image = Image.FromFile(filename); }
                            catch
                            {
                                if (dt.Rows[0]["CoverImage"].ToString() != "")
                                {
                                    DownloadImage(dt.Rows[0]["CoverImage"].ToString(), @"Resource/Images/Product/", Param.ProductId + ".jpg");
                                }
                            }
                        }

                    }

                }
                txtBarcode.Text = "";
                txtBarcode.Focus();
            }
        }
        private void DownloadImage(string url, string savePath, string fileName)
        {
            ptbProduct.ImageLocation = url;
            DownloadImage d = new DownloadImage();
            Thread thread = new Thread(() => d.Download(url, savePath, fileName));
            thread.Start();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dt = Util.DBQuery(@"SELECT CAST(IFNULL(SUM(OperationCost),0) AS DOUBLE) OperationCost, COUNT(*) ProductCount
                FROM Barcode 
                WHERE ReceivedDate IS NOT NULL
                AND SellDate IS NULL
                AND OrderNo = '" + cbbOrderNo.SelectedItem.ToString() + "'");
            var count = int.Parse(dt.Rows[0]["ProductCount"].ToString()) * 1.0;
            var currentCost = double.Parse(dt.Rows[0]["OperationCost"].ToString()) * 1.0;
            var cost = (((int)nudCost.Value * 1.0 + currentCost) / count).ToString("#,##0.00");

            if (MessageBox.Show("เฉลี่ยค่าดำเนินการ = " + cost + " บาท/ชิ้น", "ยืนยันข้อมูล", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                == DialogResult.OK)
            {
                Util.DBExecute(@"UPDATE Barcode SET 
                    OperationCost = " + cost + @", ReceivedBy = '"+Param.UserId+@"', Sync = 1  
                    WHERE ReceivedDate IS NOT NULL AND SellBy = '' AND OrderNo = '" + cbbOrderNo.SelectedItem.ToString() + "'");
                MessageBox.Show("รับสินค้าหมายเลขคำสั่งซื้อเลขที่ " + cbbOrderNo.SelectedItem.ToString() + "\n เสร็จเรียบร้อบแล้ว", "สถานะการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SearchData();
            }

        }

        private void tableModel1_SelectionChanged(object sender, XPTable.Events.SelectionEventArgs e)
        {
            if (table1.TableModel.Rows.Count > 0)
            {
                var row = e.TableModel.Selections.SelectedItems[0].Index;
                Param.ProductId = table1.TableModel.Rows[row].Cells[1].Text;
                DataTable dt = Util.DBQuery(string.Format(@"SELECT CoverImage FROM Product WHERE ID = '{0}' AND Shop = {1}", Param.ProductId, Param.ShopParent));
                ptbProduct.Image = null;
                ptbProduct.Visible = true;

                var filename = @"Resource/Images/Product/" + Param.ProductId + ".jpg";
                if (!File.Exists(filename))
                {
                    if (dt.Rows[0]["CoverImage"].ToString() != "")
                    {
                        DownloadImage(dt.Rows[0]["CoverImage"].ToString(), @"Resource/Images/Product/", Param.ProductId + ".jpg");
                    }
                }
                else
                {
                    try { ptbProduct.Image = Image.FromFile(filename); }
                    catch
                    {
                        if (dt.Rows[0]["CoverImage"].ToString() != "")
                        {
                            DownloadImage(dt.Rows[0]["CoverImage"].ToString(), @"Resource/Images/Product/", Param.ProductId + ".jpg");
                        }
                    }
                }
            }
        }

        private void txtBarcode_Enter(object sender, EventArgs e)
        {
            lblStatus.Text = "";
        }
    }
}
