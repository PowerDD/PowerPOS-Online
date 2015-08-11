using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XPTable.Models;
using System.IO;
using System.Threading;

namespace PowerPOS_Online
{
    public partial class UcProduct : UserControl
    {
        private int _ROW_INDEX = -1;
        private bool _FIRST_LOAD;
        public static string barcode;
        string id;
        int row = -1;
        public UcProduct()
        {
            InitializeComponent();
        }

        private void UcProduct_Load(object sender, EventArgs e)
        {
            Util.InitialTable(table1);
            _FIRST_LOAD = true;
            LoadData();
            if (Param.SystemConfig.SellPrice != null)
            {
                btnUseWebPrice.Enabled = false;
                btnSave.Enabled = false;
                btnUsePercentPrice.Enabled = false;
                btnConfig.Enabled = false;
            }
        }

        private void LoadData()
        {
            DataTable dt = Util.DBQuery(string.Format(@"SELECT Name FROM Category WHERE Shop = '{0}' ORDER BY Priority, Name", Param.ShopId));
            cbbCategory.Items.Clear();
            cbbCategory.Items.Add("หมวดหมู่สินค้าทั้งหมด");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbbCategory.Items.Add(dt.Rows[i]["Name"].ToString());
            }
            cbbCategory.SelectedIndex = 0;

            dt = Util.DBQuery(string.Format(@"SELECT Name FROM Brand WHERE Shop = '{0}' ORDER BY Priority, Name", Param.ShopId));
            cbbBrand.Items.Clear();
            cbbBrand.Items.Add("ยี่ห้อสินค้าทั้งหมด");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbbBrand.Items.Add(dt.Rows[i]["Name"].ToString());
            }
            cbbBrand.SelectedIndex = 0;

            _FIRST_LOAD = false;
            SearchData();
        }

        private void table1_EndSort(object sender, XPTable.Events.ColumnEventArgs e)
        {
            for (int i = 0; i < table1.TableModel.Rows.Count; i++)
            {
                table1.TableModel.Rows[i].Cells[0].Text = (i + 1).ToString();
            }
        }

        private void table1_SelectionChanged(object sender, XPTable.Events.SelectionEventArgs e)
        {
            if (table1.TableModel.Rows.Count > 0)
            {
                try
                {
                    _ROW_INDEX = e.TableModel.Selections.SelectedItems[0].Index;
                    pnlPrice.Visible = true;
                    ptbProduct.Image = null;
                    Param.ProductId = table1.TableModel.Rows[_ROW_INDEX].Cells[1].Text;
                    Param.CategoryName = table1.TableModel.Rows[_ROW_INDEX].Cells[3].Text;
                    lblCategory.Text = Param.CategoryName;

                    var filename = @"Resource/Images/Product/" + Param.ProductId + ".jpg";
                    DataTable dt = Util.DBQuery(string.Format("SELECT CoverImage FROM Product WHERE ID = '{0}' AND Shop = {1}", Param.ProductId, Param.ShopParent));
                    

                    if (!File.Exists(filename))
                    {
                        if (dt.Rows.Count > 0 && dt.Rows[0]["CoverImage"].ToString() != "")
                        {
                            DownloadImage(dt.Rows[0]["CoverImage"].ToString(), @"Resource/Images/Product/", Param.ProductId + ".jpg");
                        }
                    }
                    else
                    {
                        try { ptbProduct.Image = Image.FromFile(filename); }
                        catch
                        {
                            if (dt.Rows.Count > 0 && dt.Rows[0]["CoverImage"].ToString() != "")
                            {
                                DownloadImage(dt.Rows[0]["CoverImage"].ToString(), @"Resource/Images/Product/", Param.ProductId + ".jpg");
                            }
                        }
                    }


                    lblCost.Text = double.Parse(table1.TableModel.Rows[_ROW_INDEX].Cells[7].Data.ToString()).ToString("#,##0.00");
                    txtPrice.Text = int.Parse(table1.TableModel.Rows[_ROW_INDEX].Cells[11].Data.ToString()).ToString("#,##0");
                    txtPrice1.Text = int.Parse(table1.TableModel.Rows[_ROW_INDEX].Cells[12].Data.ToString()).ToString("#,##0");
                    txtPrice2.Text = int.Parse(table1.TableModel.Rows[_ROW_INDEX].Cells[13].Data.ToString()).ToString("#,##0");
                    nudPrice.Value = int.Parse(table1.TableModel.Rows[_ROW_INDEX].Cells[8].Data.ToString());
                    nudPrice1.Value = int.Parse(table1.TableModel.Rows[_ROW_INDEX].Cells[9].Data.ToString());
                    nudPrice2.Value = int.Parse(table1.TableModel.Rows[_ROW_INDEX].Cells[10].Data.ToString());
                    nudPrice_ValueChanged(sender, e);
                    nudPrice1_ValueChanged(sender, e);
                    nudPrice2_ValueChanged(sender, e);
                    btnSave.Enabled = false;
                    lblCategory.Text = table1.TableModel.Rows[_ROW_INDEX].Cells[4].Text;
                    id = table1.TableModel.Rows[_ROW_INDEX].Cells[1].Text;

                }
                catch
                {
                    pnlPrice.Visible = false;
                }
            }
        }

        private void nudPrice_ValueChanged(object sender, EventArgs e)
        {
            var percent = (((int)nudPrice.Value * 1.00 / double.Parse(lblCost.Text.Replace(",", "")) * 100) - 100);
            txtPercent.Text = ((int)nudPrice.Value == 0 || lblCost.Text == "0.00") ? "∞" : percent.ToString("#,##0.00");
            nudPrice.ForeColor = percent < 0 ? Color.Red : Color.Black;
            btnSave.Enabled = true;
        }

        private void nudPrice1_ValueChanged(object sender, EventArgs e)
        {
            var percent = (((int)nudPrice1.Value * 1.00 / double.Parse(lblCost.Text.Replace(",", "")) * 100) - 100);
            txtPercent1.Text = ((int)nudPrice1.Value == 0 || lblCost.Text == "0.00") ? "∞" : percent.ToString("#,##0.00");
            nudPrice1.ForeColor = percent < 0 ? Color.Red : Color.Black;
            btnSave.Enabled = true;
        }

        private void nudPrice2_ValueChanged(object sender, EventArgs e)
        {
            var percent = (((int)nudPrice2.Value * 1.00 / double.Parse(lblCost.Text.Replace(",", "")) * 100) - 100);
            txtPercent2.Text = ((int)nudPrice2.Value == 0 || lblCost.Text == "0.00") ? "∞" : percent.ToString("#,##0.00");
            nudPrice2.ForeColor = percent < 0 ? Color.Red : Color.Black;
            btnSave.Enabled = true;
        }

        private void btnUseWebPrice_Click(object sender, EventArgs e)
        {
            nudPrice.Value = int.Parse(txtPrice.Text.Replace(",", ""));
            nudPrice1.Value = int.Parse(txtPrice1.Text.Replace(",", ""));
            nudPrice2.Value = int.Parse(txtPrice2.Text.Replace(",", ""));
        }

        private void btnUsePercentPrice_Click(object sender, EventArgs e)
        {
            DataTable dt = Util.DBQuery(@"SELECT IFNULL(Price,0) Price, IFNULL(Price1,0) Price1, IFNULL(Price2,0) Price2 FROM Category c LEFT JOIN CategoryProfit p ON c.id = p.id WHERE LOWER(name) = '" + lblCategory.Text.ToLower() + "'");
            if (double.Parse(dt.Rows[0]["Price"].ToString()) == 0)
            {
                btnConfig_Click(sender, e);
            }
            else
            {
                nudPrice.Value = (int)Math.Ceiling((100 + double.Parse(dt.Rows[0]["Price"].ToString())) * double.Parse(lblCost.Text.Replace(",", "")) / 100);
                nudPrice1.Value = (int)Math.Ceiling((100 + double.Parse(dt.Rows[0]["Price1"].ToString())) * double.Parse(lblCost.Text.Replace(",", "")) / 100);
                nudPrice2.Value = (int)Math.Ceiling((100 + double.Parse(dt.Rows[0]["Price2"].ToString())) * double.Parse(lblCost.Text.Replace(",", "")) / 100);
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            Param.CategoryName = lblCategory.Text;
            FmProfitConfig ul = new FmProfitConfig();
            var result = ul.ShowDialog(this);
        }

        private void DownloadImage(string url, string savePath, string fileName)
        {
            ptbProduct.ImageLocation = url;
            DownloadImage d = new DownloadImage();
            Thread thread = new Thread(() => d.Download(url, savePath, fileName));
            thread.Start();
        }

        private void SearchData()
        {
            DataTable dt;
            if (!_FIRST_LOAD)
            {
                if (Param.SystemConfig.SellPrice != null)
                {
                    dt = Util.DBQuery(string.Format(@"SELECT DISTINCT p.ID, p.Name, c.Name Category, b.Name Brand, IFNULL(cnt.Cost,0) Cost,  IFNULL(p.Warranty, 0)  Warranty,   
                    IFNULL(p.Price, 0) Price, IFNULL(p.Price1, 0)  Price1,IFNULL(p.Price2, 0)  Price2, IFNULL(p.WebPrice,0) WebPrice, IFNULL(p.WebPrice1,0) WebPrice1, IFNULL(p.WebPrice2,0) WebPrice2, IFNULL(cnt.ProductCount, 0) ProductCount
                    FROM Barcode b
                        LEFT JOIN Product p
                            ON b.Product = p.ID
                            AND p.Shop = '{0}'
                        LEFT JOIN Category c
                            ON p.Category = c.ID
                            AND p.Shop = c.Shop
                        LEFT JOIN Brand b
                            ON p.Brand = b.ID
                            AND p.Shop = b.Shop
                        LEFT JOIN (
                            SELECT Product, AVG(Cost+OperationCost) Cost, COUNT(*) ProductCount FROM Barcode WHERE ReceivedDate IS NOT NULL AND (SellNo = '' OR SellNo IS NULL) GROUP BY Product
                        ) cnt
		            ON p.ID = cnt.Product
		            WHERE (p.ID LIKE '%{1}%' OR p.Name LIKE '%{1}%') {2} {3} {4} {5}
                    ORDER BY p.Name", Param.ShopId, txtSearch.Text.Trim(),
                            (cbbCategory.SelectedIndex != 0) ? "AND c.Name = '" + cbbCategory.SelectedItem.ToString() + "'" : "",
                            (cbbBrand.SelectedIndex != 0) ? "AND b.Name = '" + cbbBrand.SelectedItem.ToString() + "'" : "",
                            (cbNoPrice.Checked) ? "AND p.Price = 0" : "",
                            (cbNoStock.Checked) ? "AND IFNULL(cnt.ProductCount, 0) = 0" : ""
                        ));
                }
                else
                {
                    dt = Util.DBQuery(string.Format(@"SELECT DISTINCT p.ID, p.Name, c.Name Category, b.Name Brand,  IFNULL(cnt.Cost,0) Cost,  IFNULL(p.Warranty, 0)  Warranty,   
                    IFNULL(p.Price, 0) Price, IFNULL(p.Price1, 0)  Price1,IFNULL(p.Price2, 0)  Price2,IFNULL(p.WebPrice,0) WebPrice, IFNULL(p.WebPrice1,0) WebPrice1, IFNULL(p.WebPrice2,0) WebPrice2, IFNULL(cnt.ProductCount, 0) ProductCount
                    FROM Barcode b
                        LEFT JOIN Product p
                            ON b.Product = p.ID
                            AND p.Shop = '{0}'
                        LEFT JOIN Category c
                            ON p.Category = c.ID
                            AND p.Shop = c.Shop
                        LEFT JOIN Brand b
                            ON p.Brand = b.ID
                            AND p.Shop = b.Shop
                        LEFT JOIN (
                            SELECT Product, AVG(Cost+OperationCost) Cost, COUNT(*) ProductCount FROM Barcode WHERE ReceivedDate IS NOT NULL AND (SellNo = '' OR SellNo IS NULL) GROUP BY Product
                        ) cnt
		            ON p.ID = cnt.Product
		            WHERE (p.ID LIKE '%{1}%' OR p.Name LIKE '%{1}%') {2} {3} {4} {5}
                    ORDER BY p.Name", Param.ShopId, txtSearch.Text.Trim(),
                           (cbbCategory.SelectedIndex != 0) ? "AND c.Name = '" + cbbCategory.SelectedItem.ToString() + "'" : "",
                           (cbbBrand.SelectedIndex != 0) ? "AND b.Name = '" + cbbBrand.SelectedItem.ToString() + "'" : "",
                           (cbNoPrice.Checked) ? "AND p.Price = 0" : "",
                           (cbNoStock.Checked) ? "AND IFNULL(cnt.ProductCount, 0) = 0" : ""
                       ));
                }

                table1.BeginUpdate();
                tableModel1.Rows.Clear();
                tableModel1.RowHeight = 22;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int warranty = int.Parse(dt.Rows[i]["Warranty"].ToString());
                    tableModel1.Rows.Add(new Row(
                        new Cell[] {
                    new Cell("" + (i+1)),
                    new Cell(dt.Rows[i]["ID"].ToString()),
                    new Cell(dt.Rows[i]["Name"].ToString()),
                    new Cell(int.Parse(dt.Rows[i]["ProductCount"].ToString())),
                    new Cell(dt.Rows[i]["Category"].ToString()),
                    new Cell(dt.Rows[i]["Brand"].ToString()),
                    new Cell((warranty == 365) ? "1 ปี" : ((warranty == 0) ? "-" : ((warranty%30 == 0) ? warranty/30+" เดือน" : warranty+" วัน"))),
                    new Cell(double.Parse(dt.Rows[i]["Cost"].ToString())),
                    new Cell(double.Parse(dt.Rows[i]["Price"].ToString())),
                    new Cell(double.Parse(dt.Rows[i]["Price1"].ToString())),
                    new Cell(double.Parse(dt.Rows[i]["Price2"].ToString())),
                    new Cell(double.Parse(dt.Rows[i]["WebPrice"].ToString())),
                    new Cell(double.Parse(dt.Rows[i]["WebPrice1"].ToString())),
                    new Cell(double.Parse(dt.Rows[i]["WebPrice2"].ToString()))
                        }));
                }
                table1.EndUpdate();

                lblRecords.Text = dt.Rows.Count.ToString("#,##0");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                DataTable dt;
                dt = Util.DBQuery(string.Format(@"SELECT Barcode FROM Barcode WHERE Barcode = '" + txtSearch.Text.Trim() + "'"));
                if (dt.Rows.Count > 0)
                {
                    barcode = dt.Rows[0]["Barcode"].ToString();
                    FmBarcode frm = new FmBarcode();
                    frm.Show();
                    txtSearch.Text = "";
                }
                else
                {
                    SearchData();
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (Param.SystemConfig.Bill.PrintType == "Y")
            {
                var cnt = int.Parse(Param.SystemConfig.Bill.PrintCount.ToString());
                for (int i = 1; i <= cnt; i++)
                    Util.PrintStock();
            }
            else if (Param.SystemConfig.Bill.PrintType == "A")
            {
                if (MessageBox.Show("คุณต้องการพิมพ์ใบเสร็จรับเงินหรือไม่ ?", "การพิมพ์", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Util.PrintStock();
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณแน่ใจหรือไม่ ที่จะกำหนดสินค้านี้ ?", "ยืนยันข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Util.DBExecute(string.Format(@"UPDATE Product SET Price = '" + nudPrice.Value.ToString() + "', Price1 = '" + nudPrice1.Value.ToString() + "', Price2 = '" + nudPrice2.Value.ToString() + "',Sync = 1 WHERE ID = '{0}' AND shop = '{1}'",id,Param.ShopId));
                SearchData();
            }
        }
    }
}
