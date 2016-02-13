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
        private bool _SELECT_ORDER = false;
        int row = -1;       
        public static string productNo;
        public static string OrderNo;
        public static string ProductName;


        public UcReceiveProduct()
        {
            InitializeComponent();
        }

        private void UcReceiveProduct_Load(object sender, EventArgs e)
        {
            Util.InitialTable(table1);
            if (Param.SystemConfig.SellPrice != null) rdbNonShip.Checked = true; else rdbShip.Checked = true;
            _FIRST_LOAD = true;
            LoadData();
        }

        private void checkRadio(object sender, EventArgs e)
        {
            nudCost.Enabled = rdbShip.Checked;
        }

        private void LoadData()
        {
            if (Param.SystemConfig.SellPrice != null)
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

                DataTable dtab = Util.DBQuery(@"SELECT DISTINCT OrderNo FROM Barcode WHERE OrderNo NOT IN (SELECT OrderNo  FROM Barcode WHERE ReceivedDate IS NULL ) AND OrderNo <> ''  ORDER BY OrderNo");
                cbbOrder.Items.Clear();
                cbbOrder.Items.Add("เลขที่ใบสั่งซื้อ");
                if (dtab.Rows.Count == 0)
                {
                    cbbOrder.Enabled = false;
                }
                else
                {
                    cbbOrder.Enabled = true;
                    for (int i = 0; i < dtab.Rows.Count; i++)
                    {
                        cbbOrder.Items.Add(dtab.Rows[i]["OrderNo"].ToString());
                    }
                }
                cbbOrder.SelectedIndex = 0;


                DataTable dtNo = Util.DBQuery(@"SELECT DISTINCT OrderNo FROM PurchaseOrder po WHERE po.Quantity <> po.ReceivedQty  ORDER BY OrderNo");
                cbbOrderNoSerial.Items.Clear();
                cbbOrderNoSerial.Items.Add("เลขที่ใบสั่งซื้อ");
                if (dtNo.Rows.Count == 0)
                {
                    cbbOrderNoSerial.Enabled = false;
                }
                else
                {
                    cbbOrderNoSerial.Enabled = true;
                    for (int i = 0; i < dtNo.Rows.Count; i++)
                    {
                        cbbOrderNoSerial.Items.Add(dtNo.Rows[i]["OrderNo"].ToString());
                    }
                }
                cbbOrderNoSerial.SelectedIndex = 0;
            }
            else
            {
                DataTable dt = Util.DBQuery(@"SELECT DISTINCT OrderNo FROM Barcode WHERE (ReceivedDate IS NULL OR (OperationCost = 0 OR OperationCost = '')) AND Ship <> 2 ORDER BY OrderNo");
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

                DataTable dtab = Util.DBQuery(@"SELECT DISTINCT OrderNo FROM Barcode WHERE OrderNo NOT IN (SELECT OrderNo  FROM Barcode WHERE ReceivedDate IS NULL OR (OperationCost = 0 OR OperationCost = '') AND Ship <> 2) AND OrderNo <> ''  ORDER BY OrderNo");
                cbbOrder.Items.Clear();
                cbbOrder.Items.Add("เลขที่ใบสั่งซื้อ");
                if (dtab.Rows.Count == 0)
                {
                    cbbOrder.Enabled = false;
                }
                else
                {
                    cbbOrder.Enabled = true;
                    for (int i = 0; i < dtab.Rows.Count; i++)
                    {
                        cbbOrder.Items.Add(dtab.Rows[i]["OrderNo"].ToString());
                    }
                }
                cbbOrder.SelectedIndex = 0;

                DataTable dtNo = Util.DBQuery(@"SELECT DISTINCT OrderNo FROM PurchaseOrder po WHERE po.Quantity <> po.ReceivedQty  ORDER BY OrderNo");
                cbbOrderNoSerial.Items.Clear();
                cbbOrderNoSerial.Items.Add("เลขที่ใบสั่งซื้อ");
                if (dtNo.Rows.Count == 0)
                {
                    cbbOrderNoSerial.Enabled = false;
                }
                else
                {
                    cbbOrderNoSerial.Enabled = true;
                    for (int i = 0; i < dtNo.Rows.Count; i++)
                    {
                        cbbOrderNoSerial.Items.Add(dtNo.Rows[i]["OrderNo"].ToString());
                    }
                }

                cbbOrderNoSerial.SelectedIndex = 0;

            }


            _FIRST_LOAD = false;
        }

        private void cbbOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ptbProduct.Visible = false;
            if (cbbOrder.SelectedIndex != 0 && _SELECT_ORDER == false)
            {
                _SELECT_ORDER = true;
                cbbOrder.SelectedItem = "เลขที่ใบสั่งซื้อ";
                _FIRST_LOAD = false;
                _SELECT_ORDER = false;
            }
            SearchData();
        }

        private void SearchData()
        {
            if (cbbOrderNo.SelectedIndex == 0)
            {
                table1.BeginUpdate();
                tableModel1.Rows.Clear();
                table1.EndUpdate();

                gbOrderNo.Height = 53;
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
                                AND ReceivedBy = '{2}' 
                                AND OrderNo =  '{1}' 
                                GROUP BY Product
                        ) r
                            ON b.Product = r.Product
                    WHERE (b.ReceivedDate IS NULL OR b.ReceivedBy = '{2}')
                        AND b.OrderNo = '{1}'
                    GROUP BY b.Product
                    ORDER BY p.Name
                ", Param.ShopId, cbbOrderNo.SelectedItem.ToString(), Param.UserId));
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
                    gbOrderNo.Height = 53;
                    progressBar1.Visible = false;
                    gbCost.Visible = false;
                }
                else if (_RECEIVED != 0)
                {
                    gbCost.Visible = true;
                    gbOrderNo.Height = 79;
                    progressBar1.Visible = true;
                    progressBar1.Maximum = _QTY;
                    progressBar1.Value = _RECEIVED;
                }
            }
            txtBarcode.Focus();
        }

        private void SearchDataOrder()
        {
            if (cbbOrder.SelectedIndex == 0 )
            {
                table1.BeginUpdate();
                tableModel1.Rows.Clear();
                table1.EndUpdate();

                gbOrder.Height = 69;
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
                                AND ReceivedBy = '{2}' 
                                AND OrderNo =  '{1}' 
                                GROUP BY Product
                        ) r
                            ON b.Product = r.Product
                    WHERE (b.ReceivedDate IS NULL OR b.ReceivedBy = '{2}')
                        AND b.OrderNo = '{1}'
                    GROUP BY b.Product
                    ORDER BY p.Name
                ", Param.ShopId, cbbOrder.SelectedItem.ToString(), Param.UserId));
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

            }
            txtBarcode.Focus();
        }

        private void SearchDataNoSerial()
        {
            if (cbbOrderNoSerial.SelectedIndex == 0)
            {
                //table1.BeginUpdate();
                //tableModel1.Rows.Clear();
                //table1.EndUpdate();

                //gbOrderNo.Height = 53;
                //progressBar1.Visible = false;
                //gbCost.Visible = false;
            }
            else if (!_FIRST_LOAD)
            {
                _QTY = 0;
                _RECEIVED = 0;
                DataTable dt = Util.DBQuery(string.Format(@"
                    SELECT po.*, p.Name FROM PurchaseOrder po
                    LEFT JOIN Product p
                    ON po.Product = p.ID
                    WHERE OrderNo = '{1}'
                    AND p.shop = '{0}'
                    ORDER BY p.Name
                ", Param.ShopId, cbbOrderNoSerial.SelectedItem.ToString(), Param.UserId));
                table1.BeginUpdate();
                tableModel1.Rows.Clear();
                tableModel1.RowHeight = 22;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var progress = (float.Parse(dt.Rows[i]["ReceivedQty"].ToString()) * 100.0f) / float.Parse(dt.Rows[i]["Quantity"].ToString()) ;
                    tableModel1.Rows.Add(new Row(
                        new Cell[] {
                        new Cell("" + (i+1)),
                        new Cell(dt.Rows[i]["Product"].ToString()),
                        new Cell(dt.Rows[i]["Name"].ToString()),
                        new Cell(int.Parse(dt.Rows[i]["Quantity"].ToString()).ToString("#,##0")),
                        new Cell(int.Parse(dt.Rows[i]["ReceivedQty"].ToString()).ToString("#,##0")),
                        new Cell((int)progress)
                        }));
                    _QTY += int.Parse(dt.Rows[i]["Quantity"].ToString());
                    _RECEIVED += int.Parse(dt.Rows[i]["ReceivedQty"].ToString());
                }
                table1.EndUpdate();

                ptbProduct.Visible = false;
                if (_QTY == 0 || _RECEIVED == 0)
                {
                    gbOrderNoSerial.Height = 53;
                    progressBar2.Visible = false;
                    gbCost.Visible = false;
                }
                else if (_RECEIVED != 0)
                {
                    gbCost.Visible = true;
                    gbOrderNoSerial.Height = 79;
                    progressBar2.Visible = true;
                    progressBar2.Maximum = _QTY;
                    progressBar2.Value = _RECEIVED;
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
            try
            {
                //if (txtBarcode.Text != "")
                //{
                    if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                    {
                        Param.BarcodeNo = txtBarcode.Text;

                        DataTable dt = Util.DBQuery(string.Format(@"SELECT b.OrderNo, p.ID, p.CoverImage, IFNULL(b.ReceivedDate, '') ReceivedDate 
                    FROM Barcode b LEFT JOIN Product p ON b.product = p.id WHERE b.Barcode = '{0}'", txtBarcode.Text));

                        lblStatus.Visible = true;

                        if (dt.Rows.Count == 0)
                        {
                            dt = Util.DBQuery(string.Format(@"SELECT Barcode FROM Product WHERE Barcode LIKE '%{0}%'", txtBarcode.Text));
                            Console.WriteLine(txtBarcode.Text + "" + Param.BarcodeNo + "" + dt.Rows.Count.ToString());
                            if (dt.Rows.Count == 0)
                            {
                                lblStatus.ForeColor = Color.Red;
                                lblStatus.Text = "ไม่พบข้อมูลสินค้าชิ้นนี้ในระบบ";
                            }
                            else
                            {
                                Param.status = "Received";
                                OrderNo = cbbOrderNoSerial.SelectedItem.ToString();
                                FmSelectProduct frm = new FmSelectProduct();
                                var result = frm.ShowDialog(this);
                                if (result == System.Windows.Forms.DialogResult.OK)
                                {
                                    SearchDataNoSerial();
                                }


                                //lblStatus.ForeColor = Color.Pink;
                                //lblStatus.Text = "พบข้อมูลสินค้าชิ้นนี้ในระบบ";
                            }
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
                                Util.DBExecute(string.Format(@"UPDATE Barcode SET ReceivedDate = STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), ReceivedBy = '{1}', Sync = 1
                        WHERE Barcode = '{0}'", txtBarcode.Text, Param.UserId));
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
                //}
                //else
                //{
                //    MessageBox.Show("กรุณากรอกข้อมูลบาร์โค้ด", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
            }
            catch { }
           
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
                double ship = 0;
                if (rdbShip.Checked == true)
                {
                    ship = 1;
                }
                else
                {
                    ship = 2;
                }
                Util.DBExecute(@"UPDATE Barcode SET 
                    OperationCost = " + cost + @", ReceivedBy = '"+Param.UserId+@"', Sync = 1, Ship = '" + ship + "' WHERE ReceivedDate IS NOT NULL AND SellBy = '' AND OrderNo = '" + cbbOrderNo.SelectedItem.ToString() + "'");
                MessageBox.Show("รับสินค้าหมายเลขคำสั่งซื้อเลขที่ " + cbbOrderNo.SelectedItem.ToString() + "\n เสร็จเรียบร้อบแล้ว", "สถานะการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tableModel1.Rows.Clear();
                LoadData();
                //SearchData();
            }

        }

        private void tableModel1_SelectionChanged(object sender, XPTable.Events.SelectionEventArgs e)
        {
            if (table1.TableModel.Rows.Count > 0)
            {
                try
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
                catch { }
            }
        }

        private void txtBarcode_Enter(object sender, EventArgs e)
        {
            lblStatus.Text = "";
        }

        private void cbbOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            ptbProduct.Visible = false;
            if (cbbOrderNo.SelectedIndex != 0 && _SELECT_ORDER == false)
            {
                _SELECT_ORDER = true;
                cbbOrderNo.SelectedItem = "เลขที่ใบสั่งซื้อ";
                _FIRST_LOAD = false;
                _SELECT_ORDER = false;

            }
            SearchDataOrder();
        }

        private void table1_CellDoubleClick(object sender, XPTable.Events.CellMouseEventArgs e)
        {
            if (e.Row < table1.RowCount)
            {
                row = e.Row;
            }
            if (row != -1)
            {
                productNo = tableModel1.Rows[row].Cells[1].Text;
                if (cbbOrder.SelectedIndex == 0) { OrderNo = cbbOrderNo.SelectedItem.ToString(); } else { OrderNo = cbbOrder.SelectedItem.ToString(); }
                ProductName = tableModel1.Rows[row].Cells[2].Text;
                FmOrderDetail frm = new FmOrderDetail();
                frm.Show();
            }
            else
            {
                MessageBox.Show("กรุณาเลือกรายการที่ต้องการดูรายละเอียดการขาย", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cbbOrderNoSerial_SelectedIndexChanged(object sender, EventArgs e)
        {
            ptbProduct.Visible = false;
            SearchDataNoSerial();
        }
    }
}
