using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.WindowsAzure.Storage.Table;
using XPTable.Models;
using System.Net;
using System.Globalization;
using System.Threading;

namespace PowerPOS_Online
{
    public partial class UcReturn : UserControl
    {
        private List<BarcodeEntity> barcodeEntityList;
        private ProductEntity productEntity;
        private BarcodeEntity barcodeEntity;
        private string barcode;
        private bool downloading;
        private List<string> shopList;
        private List<string> customerList;
        DataTable _TABLE_RETURN;

        public UcReturn()
        {
            InitializeComponent();
        }

        private void UcReturn_Load(object sender, EventArgs e)
        {
            Util.InitialTable(table1); 
            barcode = string.Empty;
            ptbProduct.SizeMode = PictureBoxSizeMode.StretchImage;
            txtBarcode.Focus();
        }

        private void SetEnglishKeyboard(object sender, EventArgs e)
        {
            Util.SetKeyboardLayout(Util.GetInputLanguageByName("EN"));
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                lblStatus.Visible = false;

                if (e.KeyCode == Keys.Return)
                {
                    Param.BarcodeNo = txtBarcode.Text;

                    DataTable dt = Util.DBQuery(string.Format(@"SELECT b.OrderNo, p.ID, p.CoverImage, IFNULL(b.ReceivedDate, '') ReceivedDate 
                    FROM Barcode b LEFT JOIN Product p ON b.product = p.id WHERE b.Barcode = '{0}'", txtBarcode.Text));

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
                            Param.status = "Return";
                            FmSelectProduct frm = new FmSelectProduct();
                            var result = frm.ShowDialog(this);
                            if (result == System.Windows.Forms.DialogResult.OK)
                            {
                                lblStatus.Visible = false;
                                txtBarcode.Enabled = true;

                                table1.BeginUpdate();
                                tableModel1.Rows.Clear();
                                tableModel1.RowHeight = 22;
                                if (FmSelectProduct.amount != "")
                                {
                                    for (int i = 0; i < 1; i++)
                                    {
                                        string customer = FmReturnSellDetail.customer;
                                        string SellDate = FmReturnSellDetail.sellDate;
                                        int Amount = int.Parse(FmSelectProduct.amount);
                                        string SellP = (int.Parse(FmReturnSellDetail.sellP) * int.Parse(FmSelectProduct.amount)).ToString();
                                        tableModel1.Rows.Add(new Row(
                                            new Cell[] {
                                    new Cell("" + (i+1)),
                                    new Cell(SellDate),
                                    new Cell(customer),
                                    new Cell(Amount),
                                    new Cell(SellP)
                                          }));
                                    }
                                }
                                table1.EndUpdate();

                                var remain = (DateTime.Now - Convert.ToDateTime(FmReturnSellDetail.sellDate)).TotalDays;

                                btnReturn.Visible = remain > 0;

                                int day = Convert.ToInt32(remain);
                                if (day == 0)
                                {
                                    lblWarranty.Text = "สินค้าชิ้นนี้ขายไปแล้วในวันนี้";
                                }
                                else
                                {
                                    lblWarranty.Text = "สินค้าชิ้นนี้" + ((day > 0) ? " ขายไปแล้ว " + day.ToString("#,###") + " วัน" : " ขายไปแล้ว " + (day * -1).ToString("#,###") + " วัน");
                                }
                                lblWarranty.Visible = true;

                            }
                        }


                        //lblStatus.ForeColor = Color.Pink;
                        //lblStatus.Text = "พบข้อมูลสินค้าชิ้นนี้ในระบบ";
                        //_SKU = "0";
                    }
                    else
                    {
                        _TABLE_RETURN = Util.DBQuery(string.Format(@"SELECT p.Name, p.ID product, IFNULL(p.Price, 0) Price, IFNULL(p.Price1, 0) Price1, IFNULL(p.Price2, 0) Price2, 
                        b.ReceivedDate, b.ReceivedBy, b.SellDate, b.SellBy,b.Comment , sh.SellNo, c.ID customer, c.firstname , c.lastname, b.SellPrice, p.CoverImage
                        FROM Barcode b 
                            LEFT JOIN Product p 
                            ON b.Product = p.ID 
                             LEFT JOIN SellHeader sh
                            ON b.SellNo = sh.SellNo
                            LEFT JOIN Customer c
                            ON sh.Customer = c.ID
                        WHERE b.Barcode = '{0}' AND (b.SellDate IS NOT NULL OR b.SellDate = '') AND p.shop = '{1}'", txtBarcode.Text, Param.ShopId));
                        lblStatus.Visible = true;

                        if (_TABLE_RETURN.Rows.Count == 0)
                        {
                            lblStatus.Text = "สินค้าชิ้นนี้ยังไม่ได้ทำการขาย";
                            lblStatus.ForeColor = Color.Red;
                            lblStatus.Visible = true;
                            txtBarcode.Enabled = true;
                        }
                        else
                        {
                            lblStatus.Visible = false;
                            txtBarcode.Enabled = true;
                            //txtBarcode.Text = "";

                            table1.BeginUpdate();
                            tableModel1.Rows.Clear();
                            tableModel1.RowHeight = 22;
                            //shopList = new List<string>();
                            //customerList = new List<string>();
                            if (_TABLE_RETURN.Rows[0]["SellNo"].ToString() != "")
                            {
                                for (int i = 0; i < _TABLE_RETURN.Rows.Count; i++)
                                {
                                    string customer = _TABLE_RETURN.Rows[i]["firstname"].ToString() + " " + _TABLE_RETURN.Rows[i]["lastname"].ToString();
                                    string SellDate = Convert.ToDateTime(_TABLE_RETURN.Rows[i]["SellDate"]).ToLocalTime().ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH"));
                                    string SellPrice = Convert.ToDouble(_TABLE_RETURN.Rows[i]["SellPrice"]).ToString();

                                    tableModel1.Rows.Add(new Row(
                                        new Cell[] {
                                    new Cell("" + (i+1)),
                                    new Cell(SellDate),
                                    new Cell(customer),
                                    new Cell(1),
                                    new Cell(SellPrice)
                                      }));
                                }
                            }
                            table1.EndUpdate();

                            var remain = (DateTime.Now - Convert.ToDateTime(_TABLE_RETURN.Rows[0]["SellDate"])).TotalDays;

                            btnReturn.Visible = remain > 0;

                            int day = Convert.ToInt32(remain);
                            if (day == 0)
                            {
                                lblWarranty.Text = "สินค้าชิ้นนี้ขายไปแล้วในวันนี้";
                            }
                            else
                            {
                                lblWarranty.Text = "สินค้าชิ้นนี้" + ((day > 0) ? " ขายไปแล้ว " + day.ToString("#,###") + " วัน" : " ขายไปแล้ว " + (day * -1).ToString("#,###") + " วัน");
                            }
                            lblWarranty.Visible = true;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            //if (e.KeyCode == Keys.Return && barcode != txtBarcode.Text.Trim())
            //{
            //    lblStatus.Visible = true;
            //    lblStatus.Text = "กำลังค้นหาข้อมูลค่ะ";
            //    lblStatus.ForeColor = Color.Green;
            //    txtBarcode.Enabled = false;
            //    barcode = txtBarcode.Text.Trim();


            //    table1.BeginUpdate();
            //    tableModel1.Rows.Clear();
            //    table1.EndUpdate();

            //    lblName.Visible = false;
            //    lblWarrantyStatus.Visible = false;
            //    lblWarranty.Visible = false;
            //    ptbProduct.Visible = false;
            //    btnReturn.Visible = false;

            //    bwSearch.RunWorkerAsync();
            //}
        }

        private void DownloadImage(string url, string savePath, string fileName)
        {
            ptbProduct.ImageLocation = url;
            DownloadImage d = new DownloadImage();
            Thread thread = new Thread(() => d.Download(url, savePath, fileName));
            thread.Start();
        }

        private void bwSearch_DoWork(object sender, DoWorkEventArgs e)
        {
            //var azureTable = Param.AzureTableClient.GetTableReference("BarcodeStock");
            //TableQuery<BarcodeEntity> query = new TableQuery<BarcodeEntity>().Where(TableQuery.CombineFilters(
            //    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, Param.ShopId),TableOperators.And,TableQuery.GenerateFilterCondition( "RowKey", QueryComparisons.Equal, barcode)
            //));

            //barcodeEntityList = new List<BarcodeEntity>();
            //foreach (BarcodeEntity entity in azureTable.ExecuteQuery(query))
            //{
            //    barcodeEntityList.Add(entity);
            //}
            //barcodeEntityList = barcodeEntityList.OrderByDescending(o => o.SellDate).ToList();
        }

        private void bwSearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
            if (barcodeEntityList.Count == 0)
            {
                lblStatus.Text = "ไม่มีข้อมูลสินค้านี้ในระบบค่ะ";
                lblStatus.ForeColor = Color.Red;
                lblStatus.Visible = true;
                txtBarcode.Enabled = true;
            }
            else
            {
                lblStatus.Visible = false;
                txtBarcode.Enabled = true;
                //txtBarcode.Text = "";

                table1.BeginUpdate();
                tableModel1.Rows.Clear();
                tableModel1.RowHeight = 22;
                shopList = new List<string>();
                customerList = new List<string>();
                if (barcodeEntityList[0].SellNo != "")
                {
                    for (int i = 0; i < barcodeEntityList.Count; i++)
                    {
                        tableModel1.Rows.Add(new Row(
                            new Cell[] {
                    new Cell("" + (i+1)),
                    new Cell(barcodeEntityList[i].SellDate.ToLocalTime().ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH"))),
                    new Cell(Param.CustomerNameHashtable.Contains(barcodeEntityList[i].Customer) ? Param.CustomerNameHashtable[barcodeEntityList[i].Customer].ToString() : barcodeEntityList[i].Customer),
                    new Cell(barcodeEntityList[i].SellPrice.ToString())//,
                    //new Cell(Param.userEntityList[i].LastLogin.ToString("dd/MM/yyyy") == "01/01/0544" ? "-" : Param.userEntityList[i].LastLogin.ToString("dd/MM/yyyy hh:mm:ss")),
                    })
                        );
                        if (!Param.ShopNameHashtable.Contains(barcodeEntityList[i].PartitionKey))
                            shopList.Add(barcodeEntityList[i].PartitionKey);
                        if (!Param.CustomerNameHashtable.Contains(barcodeEntityList[i].Customer))
                            customerList.Add(barcodeEntityList[i].Customer);
                    }
                }
                table1.EndUpdate();
                //bwGetProduct.RunWorkerAsync();
            }
        }

        private void bwGetProduct_DoWork(object sender, DoWorkEventArgs e)
        {
            //var index = barcodeEntityList.Count - 1;

            //var azureTable = Param.AzureTableClient.GetTableReference("Product");
            //TableOperation retrieveOperation = TableOperation.Retrieve<ProductEntity>(barcodeEntityList[index].PartitionKey, barcodeEntityList[index].Product);
            //TableResult retrievedResult = azureTable.Execute(retrieveOperation);
            //productEntity = (ProductEntity)retrievedResult.Result;

            //var filename = @"Resource/Images/Product/" + barcodeEntityList[index].Product + ".jpg";
            //if (!File.Exists(filename))
            //{
            //    downloading = true;
            //    bwDownloadImage.RunWorkerAsync();
            //}
            //else
            //{
            //    downloading = false;
            //}

            //if (shopList.Count > 0)
            //{
            //    bwGetShopName.RunWorkerAsync();
            //}
            //if (customerList.Count > 0)
            //{
            //    bwGetCustomerName.RunWorkerAsync();
            //}

        }

        private void bwGetProduct_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if (productEntity != null)
            //{
            //    lblName.Text = productEntity.Name;
            //    lblName.Visible = true;

            //    if (barcodeEntityList[0].SellNo == "")
            //    {
            //        lblStatus.Text = "สินค้าชิ้นนี้ยังไม่ได้ทำการขาย";
            //        lblStatus.ForeColor = Color.Red;
            //        lblStatus.Visible = true;
            //    }
            //    else
            //    {
            //        var remain = (DateTime.Now - barcodeEntityList[0].SellDate).TotalDays;

            //        btnReturn.Visible = remain > 0;

            //        int day = Convert.ToInt32(remain);
            //        if (day == 0)
            //        {
            //            lblWarranty.Text = "สินค้าชิ้นนี้ขายไปแล้วในวันนี้";
            //        }
            //        else
            //        {
            //            lblWarranty.Text = "สินค้าชิ้นนี้" + ((day > 0) ? " ขายไปแล้ว " + day.ToString("#,###") + " วัน" : " ขายไปแล้ว " + (day * -1).ToString("#,###") + " วัน");
            //        }
            //        lblWarranty.Visible = true;

            //        //lblWarrantyStatus.Text = (productEntity.Warranty == 0) ? "ไม่สามารถเคลมสินค้าได้ค่ะ" : ((remain <= 0) ? "สินค้าหมดประกันแล้วค่ะ" : "");
            //        //lblWarrantyStatus.Visible = productEntity.Warranty == 0 || remain <= 0;
            //    }
            //    ptbProduct.Image = null;

            //    var index = barcodeEntityList.Count - 1;
            //    var filename = @"Resource/Images/Product/" + barcodeEntityList[index].Product + ".jpg";
            //    if (File.Exists(filename) && !downloading)
            //    {
            //        try
            //        {
            //            ptbProduct.Image = Image.FromFile(filename);
            //            ptbProduct.Visible = true;
            //        }
            //        catch
            //        {
            //            downloading = true;
            //            bwDownloadImage.RunWorkerAsync();
            //        }
            //    }
            //    if (downloading)
            //    {
            //        ptbProduct.ImageLocation = productEntity.CoverImage;
            //        ptbProduct.Visible = true;
            //        /*var sp = productEntity.CoverImage.Split('|');
            //        if (sp.Length > 2)
            //        {
            //            ptbProduct.ImageLocation = sp[1];
            //            ptbProduct.Visible = true;
            //        }
            //        else
            //        {
            //            ptbProduct.Visible = false;
            //        }*/
            //    }
            //}
        }

        private void bwDownloadImage_DoWork(object sender, DoWorkEventArgs e)
        {
            var index = barcodeEntityList.Count - 1;
            var filename = @"Resource/Images/Product/" + barcodeEntityList[index].Product + ".jpg";
            //var sp = productEntity.CoverImage.Split('|');
            //if (sp.Length > 2)
            //{
            if (!Directory.Exists("Resource/Images/Product")) Directory.CreateDirectory("Resource/Images/Product");
            if (File.Exists(filename)) File.Delete(filename);
            using (var client = new WebClient())
            {
                //client.DownloadFile(sp[2], filename);
                client.DownloadFile(productEntity.CoverImage, filename);

            }
            //}

        }

        private void bwGetShopName_DoWork(object sender, DoWorkEventArgs e)
        {
            //var azureTable = Param.AzureTableClient.GetTableReference("Shop");
            //for (int i = 0; i < shopList.Count; i++)
            //{
            //    TableQuery<ShopEntity> query = new TableQuery<ShopEntity>().Where(
            //        TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, shopList[i])
            //    );
            //    foreach (ShopEntity entity in azureTable.ExecuteQuery(query))
            //    {
            //        Param.ShopNameHashtable[shopList[i]] = entity.Name;
            //    }
            //}
        }

        private void bwGetShopName_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //for (int i = 0; i < tableModel1.Rows.Count; i++)
            //{
            //    try
            //    {
            //        tableModel1.Rows[i].Cells[2].Text = Param.ShopNameHashtable[tableModel1.Rows[i].Cells[2].Text].ToString();
            //    }
            //    catch { }
            //}
        }

        private void bwGetCustomerName_DoWork(object sender, DoWorkEventArgs e)
        {
            //var azureTable = Param.AzureTableClient.GetTableReference("Customer");
            //for (int i = 0; i < customerList.Count; i++)
            //{
            //    TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>().Where(
            //        TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, customerList[i])
            //    );
            //    foreach (CustomerEntity entity in azureTable.ExecuteQuery(query))
            //    {
            //        Param.CustomerNameHashtable[customerList[i]] = entity.Name;
            //    }
            //}
        }

        private void bwGetCustomerName_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //for (int i = 0; i < tableModel1.Rows.Count; i++)
            //{
            //    try
            //    {
            //        tableModel1.Rows[i].Cells[2].Text = Param.CustomerNameHashtable[tableModel1.Rows[i].Cells[2].Text].ToString();
            //    }
            //    catch { }
            //}
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt;

                dt = Util.DBQuery(string.Format(@"SELECT b.OrderNo, p.ID, p.CoverImage, IFNULL(b.ReceivedDate, '') ReceivedDate 
                    FROM Barcode b LEFT JOIN Product p ON b.product = p.id WHERE b.Barcode = '{0}'", txtBarcode.Text));
                if (dt.Rows.Count == 0)
                {
                    if (MessageBox.Show("คุณแน่ใจหรือไม่ ที่จะยืนยันการรับคืนสินค้านี้ ?", "ยืนยันข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DataTable RId = Util.DBQuery(string.Format(@"SELECT IFNULL(SUBSTR(MAX(Barcode), 1,6)||SUBSTR('0000'||(SUBSTR(MAX(Barcode), 7, 4)+1), -4, 4), SUBSTR(STRFTIME('%Y%m{0}R'), 3)||'0001') Return
                                        FROM ReturnProduct
                                        WHERE SUBSTR(Barcode, 1, 4) = SUBSTR(STRFTIME('%Y%m'), 3, 4)
                                        AND SUBSTR(Barcode, 5, 1) = '{0}'", Param.DevicePrefix));
                        var Return = RId.Rows[0]["Return"].ToString();

                        Util.DBExecute(string.Format(@"INSERT INTO ReturnProduct (SellNo, ReturnDate, Product, Barcode, SellPrice, Quantity, ReturnBy, Sync)
                                         SELECT '{0}', STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), '{1}', '{2}', '{3}', '{4}', '{5}', 1 ",
                           FmReturnSellDetail.sellN, FmReturnSellDetail.Pid, Return, tableModel1.Rows[0].Cells[4].Text, FmSelectProduct.amount, Param.UserId));

                        DataTable price = Util.DBQuery(string.Format(@"SELECT SellPrice, Cost, Quantity FROM SellDetail WHERE SellNo = '{0}' AND Product = '{1}'", FmReturnSellDetail.sellN, FmReturnSellDetail.Pid));

                        var sellPrice = Convert.ToInt32(price.Rows[0]["SellPrice"].ToString()) - (Convert.ToInt32(FmSelectProduct.amount) * Convert.ToInt32(FmReturnSellDetail.sellP));
                        var amount = Convert.ToInt32(price.Rows[0]["Quantity"].ToString()) - Convert.ToInt32(FmSelectProduct.amount);
                        var costT = Convert.ToInt32(price.Rows[0]["Cost"].ToString()) - (Convert.ToInt32(FmSelectProduct.amount) * Convert.ToInt32(FmReturnSellDetail.costPrice));

                        Util.DBExecute(string.Format(@"UPDATE SellDetail SET SellPrice = '{2}', Cost = '{4}', Quantity = '{3}',Sync = 1 WHERE SellNo = '{0}' AND Product = '{1}'",
                        FmReturnSellDetail.sellN, FmReturnSellDetail.Pid, sellPrice, amount, costT));

                        Util.DBExecute(string.Format(@"UPDATE SellHeader SET Profit = (SELECT SUM(SellPrice-Cost) FROM SellDetail WHERE SellNo = '{0}')
                                        , TotalPrice = (SELECT SUM(SellPrice) FROM SellDetail WHERE SellNo = '{0}') ,Sync = 1 WHERE SellNo = '{0}'", FmReturnSellDetail.sellN));

                        DataTable dtap = Util.DBQuery(string.Format(@"SELECT * FROM SellHeader WHERE SellNo = '{0}'", FmReturnSellDetail.sellN));

                        if (dtap.Rows[0]["TotalPrice"].ToString() == "0" || dtap.Rows[0]["TotalPrice"].ToString() == "")
                        {
                            Util.DBExecute(string.Format(@"UPDATE SellHeader SET Comment = 'คืนสินค้า' ,Sync = 1 WHERE SellNo = '{0}'", FmReturnSellDetail.sellN));
                            Util.DBExecute(string.Format(@"UPDATE SellDetail SET Comment = 'คืนสินค้า' ,Sync = 1 WHERE SellNo = '{0}'", FmReturnSellDetail.sellN));

                        }

                        dtap = Util.DBQuery(string.Format(@"SELECT SellNo, Product, Quantity FROM SellDetail WHERE SellNo = '{0}'", FmReturnSellDetail.sellN));
                        for (int i = 0; i < dtap.Rows.Count; i++)
                        {
                            if (dtap.Rows[i]["Quantity"].ToString() == "0" || dtap.Rows[i]["Quantity"].ToString() == "")
                            {
                                Util.DBExecute(string.Format(@"UPDATE SellDetail SET Comment = 'คืนสินค้า' ,Sync = 1 WHERE SellNo = '{0}' AND product = '{1}' ", FmReturnSellDetail.sellN, dtap.Rows[i]["Product"].ToString()));

                            }
                        }


                        DataTable QTY = Util.DBQuery(string.Format(@"SELECT Quantity FROM Product WHERE id = '{0}' AND shop = '{1}'", FmReturnSellDetail.Pid, Param.ShopId));

                        int qty = Convert.ToInt32(QTY.Rows[0]["Quantity"].ToString()) + Convert.ToInt32(FmSelectProduct.amount);

                        Util.DBExecute(string.Format(@"UPDATE Product SET Quantity = '{0}',Sync = 1 WHERE id = '{1}' AND shop = '{2}'", qty, FmReturnSellDetail.Pid, Param.ShopId));
                    }
                }

                else
                {
                    dt = Util.DBQuery(string.Format(@"SELECT b.Barcode, b.SellNo, p.ID, b.SellPrice
                                            FROM Barcode b
                                                LEFT JOIN Product p
                                                ON b.product = p.id
                                            WHERE p.Shop = '{0}' AND b.Barcode = '{1}'", Param.ShopId, txtBarcode.Text));

                    Util.DBExecute(string.Format(@"INSERT INTO ReturnProduct (SellNo, ReturnDate, Product, Barcode, SellPrice, ReturnBy, Quantity, Sync)
                    SELECT '{0}', STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), Product, Barcode, SellPrice, '{2}', 1, 1 FROM Barcode WHERE SellNo = '{0}'AND Barcode = '{1}' GROUP BY Product",
                            dt.Rows[0]["SellNo"].ToString(), dt.Rows[0]["Barcode"].ToString(), Param.UserId));

                    Util.DBExecute(string.Format(@"UPDATE Barcode SET SellBy = '',SellNo = '',SellFinished = 'Flase',Customer = '',SellPrice = '',SellDate = null ,Sync = 1 WHERE Barcode = '{0}'", txtBarcode.Text));

                    Util.DBExecute(string.Format(@"UPDATE SellDetail SET SellPrice =  IFNULL((SELECT SUM(SellPrice) FROM Barcode WHERE SellNo = '{0}' AND Product = '{1}'),0)  , Cost = IFNULL((SELECT SUM(Cost) FROM Barcode WHERE SellNo = '{0}' AND Product = '{1}'),0), Quantity = IFNULL((SELECT COUNT(*) FROM Barcode WHERE SellNo = '{0}' AND Product = '{1}'),0),
                     Sync = 1 WHERE SellNo = '{0}' AND Product = '{1}'", dt.Rows[0]["SellNo"].ToString(), dt.Rows[0]["ID"].ToString()));

                    Util.DBExecute(string.Format(@"UPDATE SellHeader SET Profit = (SELECT SUM(SellPrice-Cost) FROM SellDetail WHERE SellNo = '{0}')
                    , TotalPrice = (SELECT SUM(SellPrice) FROM SellDetail WHERE SellNo = '{0}') ,Sync = 1 WHERE SellNo = '{0}'", dt.Rows[0]["SellNo"].ToString()));

                    DataTable dtap = Util.DBQuery(string.Format(@"SELECT * FROM SellHeader WHERE SellNo = '{0}'", dt.Rows[0]["SellNo"].ToString()));

                    if (dtap.Rows[0]["TotalPrice"].ToString() == "0" || dtap.Rows[0]["TotalPrice"].ToString() == "")
                    {
                        Util.DBExecute(string.Format(@"UPDATE SellHeader SET Comment = 'คืนสินค้า' ,Sync = 1 WHERE SellNo = '{0}'", dt.Rows[0]["SellNo"].ToString()));
                        Util.DBExecute(string.Format(@"UPDATE SellDetail SET Comment = 'คืนสินค้า' ,Sync = 1 WHERE SellNo = '{0}'", dt.Rows[0]["SellNo"].ToString()));

                    }
                }

                //txtBarcode.Focus();
                lblStatus.Visible = true;
                lblStatus.Text = "รับคืนสินค้าในชิ้นนี้แล้ว";
                tableModel1.Rows.Clear();
                txtBarcode.Text = "";
                lblWarranty.Visible = false;
                ptbProduct.Image = null;
                lblName.Visible = false;
                btnReturn.Visible = false;
                lblStatus.ForeColor = Color.Green;
            }
            catch
            { } 
        }

        private void Return()
        {
            if (productEntity != null)
            {
                lblName.Text = productEntity.Name;
                lblName.Visible = true;

                if (barcodeEntityList[0].SellNo == "")
                {
                    lblStatus.Text = "สินค้าชิ้นนี้ยังไม่ได้ทำการขาย";
                    lblStatus.ForeColor = Color.Red;
                    lblStatus.Visible = true;
                }
                else
                {
                    var remain = (DateTime.Now - barcodeEntityList[0].SellDate).TotalDays;

                    btnReturn.Visible = remain > 0;

                    int day = Convert.ToInt32(remain);
                    if (day == 0)
                    {
                        lblWarranty.Text = "สินค้าชิ้นนี้ขายไปแล้วในวันนี้";
                    }
                    else
                    {
                        lblWarranty.Text = "สินค้าชิ้นนี้" + ((day > 0) ? " ขายไปแล้ว " + day.ToString("#,###") + " วัน" : " ขายไปแล้ว " + (day * -1).ToString("#,###") + " วัน");
                    }
                    lblWarranty.Visible = true;

                    //lblWarrantyStatus.Text = (productEntity.Warranty == 0) ? "ไม่สามารถเคลมสินค้าได้ค่ะ" : ((remain <= 0) ? "สินค้าหมดประกันแล้วค่ะ" : "");
                    //lblWarrantyStatus.Visible = productEntity.Warranty == 0 || remain <= 0;
                }
                ptbProduct.Image = null;

                var index = barcodeEntityList.Count - 1;
                var filename = @"Resource/Images/Product/" + barcodeEntityList[index].Product + ".jpg";
                if (File.Exists(filename) && !downloading)
                {
                    try
                    {
                        ptbProduct.Image = Image.FromFile(filename);
                        ptbProduct.Visible = true;
                    }
                    catch
                    {
                        downloading = true;
                        bwDownloadImage.RunWorkerAsync();
                    }
                }
                if (downloading)
                {
                    ptbProduct.ImageLocation = productEntity.CoverImage;
                    ptbProduct.Visible = true;
                    /*var sp = productEntity.CoverImage.Split('|');
                    if (sp.Length > 2)
                    {
                        ptbProduct.ImageLocation = sp[1];
                        ptbProduct.Visible = true;
                    }
                    else
                    {
                        ptbProduct.Visible = false;
                    }*/
                }
            }
        }
    }
}
