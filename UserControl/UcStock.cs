﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XPTable.Models;
using System.Threading;
using System.IO;

namespace PowerPOS_Online
{
    public partial class UcStock : UserControl
    {
        private bool _FIRST_LOAD;
        private int _QTY = 0;
        private int _RECEIVED = 0;
        public static int printType = 0;
        int row = -1;
        public static string productNo;
        public static string OrderNo;
        public static string ProductName;

        public UcStock()
        {
            InitializeComponent();
        }

        private void UcStock_Load(object sender, EventArgs e)
        {
            Util.InitialTable(table1);
            _FIRST_LOAD = true;
            LoadData();
            cbbPrintType.SelectedIndex = 0;
        }

        private void LoadData()
        {
            _FIRST_LOAD = false;
            SearchData();
        }

        private void SearchData()
        {
            DataTable dt;
            if (!_FIRST_LOAD)
            {
                _QTY = 0;
                _RECEIVED = 0;
                dt = Util.DBQuery(string.Format(@"
                    SELECT DISTINCT b.Product, p.Name, COUNT(*) ProductCount, IFNULL(r.Stock, 0) Stock
                    FROM Barcode b
                        LEFT JOIN Product p
                            ON b.Product = p.ID
                            AND p.Shop = '{0}'
                        LEFT JOIN ( 
                                SELECT DISTINCT Product, COUNT(*) Stock
                                FROM Barcode 
                                WHERE ReceivedDate IS NOT NULL 
                                AND (SellNo = '' OR SellNo IS NULL)
                                AND ReceivedBy = '{1}' 
                                AND Stock = 1 
                                GROUP BY Product
                        ) r
                            ON b.Product = r.Product
                    WHERE (b.ReceivedDate NOT NULL OR b.ReceivedBy = '{1}') AND (SellNo = '' OR SellNo IS NULL)        
                    GROUP BY b.Product
                    ORDER BY p.Name
                ", Param.ShopId,  Param.UserId));
                table1.BeginUpdate();
                tableModel1.Rows.Clear();
                tableModel1.RowHeight = 22;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var progress = float.Parse(dt.Rows[i]["Stock"].ToString()) / float.Parse(dt.Rows[i]["ProductCount"].ToString()) * 100.0f;
                    tableModel1.Rows.Add(new Row(
                        new Cell[] {
                        new Cell("" + (i+1)),
                        new Cell(dt.Rows[i]["Product"].ToString()),
                        new Cell(dt.Rows[i]["Name"].ToString()),
                        new Cell(int.Parse(dt.Rows[i]["ProductCount"].ToString()).ToString("#,##0")),
                        new Cell(int.Parse(dt.Rows[i]["Stock"].ToString()).ToString("#,##0")),
                        new Cell((int)progress)
                        }));
                    _QTY += int.Parse(dt.Rows[i]["ProductCount"].ToString());
                    _RECEIVED += int.Parse(dt.Rows[i]["Stock"].ToString());
                }
                table1.EndUpdate();
                lblRecords.Text = dt.Rows.Count.ToString("#,##0");

                ptbProduct.Visible = false;
                if (_RECEIVED != 0)
                {
                    progressBar1.Visible = true;
                    progressBar1.Maximum = _QTY;
                    progressBar1.Value = _RECEIVED;
                }
            }
            txtBarcode.Focus();


        }

        private void DownloadImage(string url, string savePath, string fileName)
        {
            ptbProduct.ImageLocation = url;
            DownloadImage d = new DownloadImage();
            Thread thread = new Thread(() => d.Download(url, savePath, fileName));
            thread.Start();
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                DataTable dt = Util.DBQuery(string.Format(@"SELECT p.ID, p.CoverImage, IFNULL(SellDate, '') SellDate,b.Stock 
                    FROM Barcode b LEFT JOIN Product p ON b.product = p.id
                    WHERE Barcode = '{0}'  AND p.Shop = '{1}' AND SellDate IS NULL", txtBarcode.Text, Param.ShopId));

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

                    if (dt.Rows[0]["Stock"].ToString() != "0")
                    {
                        lblStatus.ForeColor = Color.Red;
                        lblStatus.Text = "เคยตรวจสอบสินค้าชิ้นนี้แล้ว";
                        SearchData();
                    }
                    else
                    {
                        Util.DBExecute(string.Format(@"UPDATE Barcode SET Stock = 1, Sync = 1 WHERE Barcode = '{0}'", txtBarcode.Text));
                        SearchData();

                        lblStatus.ForeColor = Color.Green;
                        lblStatus.Text = "ตรวจสอบสินค้าเรียบร้อยแล้ว";

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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printType = cbbPrintType.SelectedIndex;
            if (printType != 0)
            {
                if (Param.SystemConfig.Bill.PrintType == "Y")
                {
                    var cnt = int.Parse(Param.SystemConfig.Bill.PrintCount.ToString());
                    for (int i = 1; i <= cnt; i++)
                        Util.PrintCheckStock();
                }
                else if (Param.SystemConfig.Bill.PrintType == "A")
                {
                    if (MessageBox.Show("คุณต้องการพิมพ์สต็อกสินค้าหรือไม่ ?", "การพิมพ์", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        Util.PrintCheckStock();
                }
            }
            else
            {
                MessageBox.Show("กรุณาเลือกประเภทการพิมพ์", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
                ProductName = tableModel1.Rows[row].Cells[2].Text;
                FmStockDetail frm = new FmStockDetail();
                frm.Show();
            }
            else
            {
                MessageBox.Show("กรุณาเลือกรายการที่ต้องการดูรายละเอียดการขาย", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
