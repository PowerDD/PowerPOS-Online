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
            lblStatus.Visible = false;

            if (e.KeyCode == Keys.Return && barcode != txtBarcode.Text.Trim())
            {
                lblStatus.Visible = true;
                lblStatus.Text = "กำลังค้นหาข้อมูลค่ะ";
                lblStatus.ForeColor = Color.Green;
                txtBarcode.Enabled = false;
                barcode = txtBarcode.Text.Trim();


                table1.BeginUpdate();
                tableModel1.Rows.Clear();
                table1.EndUpdate();

                lblName.Visible = false;
                lblWarrantyStatus.Visible = false;
                lblWarranty.Visible = false;
                ptbProduct.Visible = false;
                btnReturn.Visible = false;

                bwSearch.RunWorkerAsync();
            }
        }

        private void bwSearch_DoWork(object sender, DoWorkEventArgs e)
        {
            var azureTable = Param.AzureTableClient.GetTableReference("BarcodeStock");
            TableQuery<BarcodeEntity> query = new TableQuery<BarcodeEntity>().Where(TableQuery.CombineFilters(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, Param.ShopId),TableOperators.And,TableQuery.GenerateFilterCondition( "RowKey", QueryComparisons.Equal, barcode)));

            barcodeEntityList = new List<BarcodeEntity>();
            foreach (BarcodeEntity entity in azureTable.ExecuteQuery(query))
            {
                barcodeEntityList.Add(entity);
            }
            barcodeEntityList = barcodeEntityList.OrderByDescending(o => o.SellDate).ToList();
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
                    new Cell(Param.CustomerNameHashtable.Contains(barcodeEntityList[i].Customer) ? Param.CustomerNameHashtable[barcodeEntityList[i].Customer].ToString() : barcodeEntityList[i].Customer)
                    //,
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
                bwGetProduct.RunWorkerAsync();
            }
        }

        private void bwGetProduct_DoWork(object sender, DoWorkEventArgs e)
        {
            var index = barcodeEntityList.Count - 1;

            var azureTable = Param.AzureTableClient.GetTableReference("Product");
            TableOperation retrieveOperation = TableOperation.Retrieve<ProductEntity>(barcodeEntityList[index].PartitionKey, barcodeEntityList[index].Product);
            TableResult retrievedResult = azureTable.Execute(retrieveOperation);
            productEntity = (ProductEntity)retrievedResult.Result;

            var filename = @"Resource/Images/Product/" + barcodeEntityList[index].Product + ".jpg";
            if (!File.Exists(filename))
            {
                downloading = true;
                bwDownloadImage.RunWorkerAsync();
            }
            else
            {
                downloading = false;
            }

            if (shopList.Count > 0)
            {
                bwGetShopName.RunWorkerAsync();
            }
            if (customerList.Count > 0)
            {
                bwGetCustomerName.RunWorkerAsync();
            }

        }

        private void bwGetProduct_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

                    lblWarranty.Text = "สินค้าชิ้นนี้" + ((remain > 0) ? " ขายไปแล้ว " + remain.ToString("#,###") + " วัน" : " ขายไปแล้ว " + (remain * -1).ToString("#,###") + " วัน");
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
            var azureTable = Param.AzureTableClient.GetTableReference("Shop");
            for (int i = 0; i < shopList.Count; i++)
            {
                TableQuery<ShopEntity> query = new TableQuery<ShopEntity>().Where(
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, shopList[i])
                );
                foreach (ShopEntity entity in azureTable.ExecuteQuery(query))
                {
                    Param.ShopNameHashtable[shopList[i]] = entity.Name;
                }
            }
        }

        private void bwGetShopName_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            for (int i = 0; i < tableModel1.Rows.Count; i++)
            {
                try
                {
                    tableModel1.Rows[i].Cells[2].Text = Param.ShopNameHashtable[tableModel1.Rows[i].Cells[2].Text].ToString();
                }
                catch { }
            }
        }

        private void bwGetCustomerName_DoWork(object sender, DoWorkEventArgs e)
        {
            var azureTable = Param.AzureTableClient.GetTableReference("Customer");
            for (int i = 0; i < customerList.Count; i++)
            {
                TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>().Where(
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, customerList[i])
                );
                foreach (CustomerEntity entity in azureTable.ExecuteQuery(query))
                {
                    Param.CustomerNameHashtable[customerList[i]] = entity.Name;
                }
            }
        }

        private void bwGetCustomerName_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            for (int i = 0; i < tableModel1.Rows.Count; i++)
            {
                try
                {
                    tableModel1.Rows[i].Cells[2].Text = Param.CustomerNameHashtable[tableModel1.Rows[i].Cells[2].Text].ToString();
                }
                catch { }
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            DataTable dt;
            dt = Util.DBQuery(string.Format(@"SELECT b.Barcode, b.SellNo, p.ID, b.SellPrice
                                            FROM Barcode b
                                                LEFT JOIN Product p
                                                ON b.product = p.id
                                            WHERE p.Shop = '{0}' AND Barcode = '{1}'", Param.ShopId, txtBarcode.Text));

            Util.DBExecute(string.Format(@"INSERT INTO ReturnProduct (SellNo, ReturnDate, Product, Barcode, SellPrice, ReturnBy, Sync)
                    SELECT '{0}', STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), Product, Barcode, SellPrice, '{2}', 1 FROM Barcode WHERE SellNo = '{0}'AND Barcode = '{1}' GROUP BY Product",
                    dt.Rows[0]["SellNo"].ToString(), dt.Rows[0]["Barcode"].ToString(), Param.UserId));

            Util.DBExecute(string.Format(@"UPDATE Barcode SET SellBy = '',SellNo = '',SellFinished = 'Flase',Customer = '',SellPrice = '',SellDate = null ,Sync = 1 WHERE Barcode = '{0}'", txtBarcode.Text));

            Util.DBExecute(string.Format(@"UPDATE SellHeader SET Profit = (SELECT SUM(SellPrice-Cost-OperationCost) FROM Barcode WHERE SellNo = '{0}')
                    , TotalPrice = (SELECT SUM(SellPrice) FROM Barcode WHERE SellNo = '{0}') ,Sync = 1 WHERE SellNo = '{0}'", dt.Rows[0]["SellNo"].ToString()));
            
            Util.DBExecute(string.Format(@"UPDATE SellDetail SET SellPrice =  IFNULL((SELECT SUM(SellPrice) FROM Barcode WHERE SellNo = '{0}' AND Product = '{1}'),0)  , Cost = IFNULL((SELECT SUM(Cost) FROM Barcode WHERE SellNo = '{0}' AND Product = '{1}'),0), Quantity = IFNULL((SELECT COUNT(*) FROM Barcode WHERE SellNo = '{0}' AND Product = '{1}'),0),
                     Sync = 1 WHERE SellNo = '{0}'", dt.Rows[0]["SellNo"].ToString(), dt.Rows[0]["ID"].ToString()));

            DataTable dtap = Util.DBQuery(string.Format(@"SELECT * FROM SellHeader WHERE SellNo = '{0}'", dt.Rows[0]["SellNo"].ToString()));

            if (dtap.Rows[0]["TotalPrice"].ToString() == "0" || dtap.Rows[0]["TotalPrice"].ToString() == "")
            {
                Util.DBExecute(string.Format(@"UPDATE SellHeader SET Comment = 'คืนสินค้า' ,Sync = 1 WHERE SellNo = '{0}'", dt.Rows[0]["SellNo"].ToString()));
                Util.DBExecute(string.Format(@"UPDATE SellDetail SET Comment = 'คืนสินค้า' ,Sync = 1 WHERE SellNo = '{0}'", dt.Rows[0]["SellNo"].ToString()));

            }

            //txtBarcode.Focus();
            lblStatus.Visible = true;
            lblStatus.Text = "รับคืนสินค้าในชิ้นนี้แล้ว";
            lblWarranty.Visible = false;
            ptbProduct.Image = null;
            lblName.Visible = false;
            btnReturn.Visible = false;
            lblStatus.ForeColor = Color.Green;       
            
        }
    }
}
