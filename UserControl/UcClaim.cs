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
using System.Net;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Threading;
using Newtonsoft.Json;

namespace PowerPOS_Online
{
    public partial class UcClaim : UserControl
    {
        private List<BarcodeEntity> barcodeEntityList;
        private List<BarcodeEntity2> barcodeEntityList2;
        private ProductEntity productEntity;        
        private bool downloading;
        private List<string> shopList;
        private List<string> customerList;
        private List<string> shopList2;
        private List<string> customerList2;
        public static string barcode;
        public static string sellprice;
        public static string Product;
        public static DateTime SellDate;
        public UcClaim()
        {
            InitializeComponent();
        }

        private void UcClaim_Load(object sender, EventArgs e)
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
                btnClaim.Visible = false;

                bwSearch.RunWorkerAsync();
            }
        }

        private void bwSearch_DoWork(object sender, DoWorkEventArgs e)
        {
            //dynamic json = JsonConvert.DeserializeObject(Util.GetApiData("/claim/info", "shop=" + Param.ShopId + "&barcode='140916151107023'"));

            var azureTable = Param.AzureTableClient.GetTableReference("Barcode");
            TableQuery<BarcodeEntity> query = new TableQuery<BarcodeEntity>().Where(
                TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, barcode)
            );

            barcodeEntityList = new List<BarcodeEntity>();
            foreach (BarcodeEntity entity in azureTable.ExecuteQuery(query))
            {
                barcodeEntityList.Add(entity);
            }
            barcodeEntityList = barcodeEntityList.OrderByDescending(o => o.SellDate).ToList();
        }

        private void bwSearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var index = barcodeEntityList.Count - 1;
            if (barcodeEntityList.Count == 0 || (barcodeEntityList[index].SellDate.ToString() == "1/1/0001 12:00:00 AM"))
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
                txtBarcode.Text = "";

                table1.BeginUpdate();
                tableModel1.Rows.Clear();
                tableModel1.RowHeight = 22;
                shopList = new List<string>();
                customerList = new List<string>();

                for (int i = 0; i < barcodeEntityList.Count; i++)
                {
                    tableModel1.Rows.Add(new Row(
                        new Cell[] {
                        new Cell("" + (i+1)),
                        new Cell(barcodeEntityList[i].SellDate.ToLocalTime().ToString("dd MMMM yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("th-TH"))),
                        new Cell(Param.ShopNameHashtable.Contains(barcodeEntityList[i].PartitionKey) ? Param.ShopNameHashtable[barcodeEntityList[i].PartitionKey].ToString() : barcodeEntityList[i].PartitionKey),
                        new Cell(barcodeEntityList[i].ReceivedDate.ToLocalTime().ToString("dd MMMM yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("th-TH"))),
                        new Cell(Param.CustomerNameHashtable.Contains(barcodeEntityList[i].Customer) ? Param.CustomerNameHashtable[barcodeEntityList[i].Customer].ToString() : barcodeEntityList[i].Customer)
                        })
                    );
                    if (!Param.ShopNameHashtable.Contains(barcodeEntityList[i].PartitionKey))
                        shopList.Add(barcodeEntityList[i].PartitionKey);
                    if (!Param.CustomerNameHashtable.Contains(barcodeEntityList[i].Customer))
                        customerList.Add(barcodeEntityList[i].Customer);
                }
                table1.EndUpdate();
                //bwGetProduct.RunWorkerAsync();
                bwSearchBarcode.RunWorkerAsync();
            }
        }

        private void DownloadImage(string url, string savePath, string fileName)
        {
            ptbProduct.ImageLocation = url;
            DownloadImage d = new DownloadImage();
            Thread thread = new Thread(() => d.Download(url, savePath, fileName));
            thread.Start();
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
                var remain = 0.0;
                if (barcodeEntityList2[0].SellDate.ToString() == "1/1/0001 12:00:00 AM")
                {
                    remain = (productEntity.Warranty == 0) ? 0 : productEntity.Warranty - (DateTime.Now - barcodeEntityList[0].SellDate).TotalDays;
                }
                else
                {
                    remain = (productEntity.Warranty == 0) ? 0 : productEntity.Warranty - (DateTime.Now - barcodeEntityList2[0].SellDate).TotalDays;
                }

                if (lblStatus.Text != "สินค้านี้ทำการเคลมแล้ว")
                {
                    btnClaim.Visible = remain > 0;
                }

                lblWarranty.Text = (productEntity.Warranty == 0) ? "สินค้าไม่มีประกัน" : "ประกัน " + productEntity.Warranty + " วัน" +
                    ((remain > 0) ? " (เหลือประกันอีก " + remain.ToString("#,###") + " วัน)" : " (ประกันหมดแล้ว " + (remain * -1).ToString("#,###") + " วัน)");
                lblWarranty.Visible = true;

                lblWarrantyStatus.Text = (productEntity.Warranty == 0) ? "ไม่สามารถเคลมสินค้าได้ค่ะ" : ((remain <= 0) ? "สินค้าหมดประกันแล้วค่ะ" : "");
                lblWarrantyStatus.Visible = productEntity.Warranty == 0 || remain <= 0;

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
            //bwSearchBarcode.RunWorkerAsync();
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

        private void btnClaim_Click(object sender, EventArgs e)
        {
            var index = barcodeEntityList.Count - 1;

            FmClaim fm = new FmClaim();
            //DataTable dt = Util.DBQuery(string.Format(@"SELECT SellPrice FROM Barcode WHERE Barcode = '{0}'", UcClaim.barcode));
            sellprice = barcodeEntityList2[index].SellPrice.ToString();
            Product = barcodeEntityList2[index].Product;
            SellDate = barcodeEntityList2[index].SellDate;
            var result = fm.ShowDialog(this);
            if (result == DialogResult.Yes)
            {

            }
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
                    tableModel1.Rows[i].Cells[4].Text = Param.CustomerNameHashtable[tableModel1.Rows[i].Cells[4].Text].ToString();
                }
                catch { }
            }


        }

        private void btnCheckClaim_Click(object sender, EventArgs e)
        {

        }

        private void bwSearchBarcode_DoWork(object sender, DoWorkEventArgs e)
        {
            var index = barcodeEntityList.Count - 1;
            var azureTable = Param.AzureTableClient.GetTableReference("BarcodeStock");
            TableQuery<BarcodeEntity2> queryT = new TableQuery<BarcodeEntity2>().Where(
                TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, barcodeEntityList[index].RowKey)
            );

            barcodeEntityList2 = new List<BarcodeEntity2>();
            foreach (BarcodeEntity2 entity in azureTable.ExecuteQuery(queryT))
            {
                barcodeEntityList2.Add(entity);
            }
            barcodeEntityList2 = barcodeEntityList2.OrderByDescending(o => o.SellDate).ToList();
        }

        private void bwSearchBarcode_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                var index = barcodeEntityList2.Count - 1;
                if (barcodeEntityList2[index].Comment == null)
                {
                    barcodeEntityList2[index].Comment = "";

                }

                string[] val = barcodeEntityList2[0].Comment.Split('(');
                string[] change = barcodeEntityList2[0].Comment.Split('[');
                //if (barcodeEntityList2.Count == 0 || (barcodeEntityList2[0].SellDate.ToString() == "1/1/0001 12:00:00 AM" && Change[0].ToString() == ""))
                if (barcodeEntityList2.Count == 0)
                {
                    lblStatus.Text = "ไม่มีข้อมูลสินค้านี้ในระบบค่ะ";
                    lblStatus.ForeColor = Color.Red;
                    lblStatus.Visible = true;
                    txtBarcode.Enabled = true;
                }
                else if (barcodeEntityList2[0].SellDate.ToString() == "1/1/0001 12:00:00 AM")
                {
                }
                else if (val[0].ToString() == "เคลมสินค้า")
                {
                    lblStatus.Text = "สินค้านี้ทำการเคลมแล้ว";
                    lblStatus.ForeColor = Color.Red;
                    lblStatus.Visible = true;
                    txtBarcode.Enabled = true;
                    btnClaim.Visible = false;

                    table1.BeginUpdate();
                    shopList2 = new List<string>();
                    customerList2 = new List<string>();

                    for (int i = 0; i < barcodeEntityList2.Count; i++)
                    {
                        tableModel1.Rows.Add(new Row(
                            new Cell[] {
                        new Cell("" + (tableModel1.Rows.Count+1)),
                        new Cell(barcodeEntityList2[i].SellDate.ToLocalTime().ToString("dd MMMM yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("th-TH"))),
                        new Cell(Param.ShopNameHashtable.Contains(barcodeEntityList2[i].PartitionKey) ? Param.ShopNameHashtable[barcodeEntityList2[i].PartitionKey].ToString() : barcodeEntityList2[i].PartitionKey),
                        new Cell(barcodeEntityList2[i].ReceivedDate.ToLocalTime().ToString("dd MMMM yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("th-TH"))),
                        new Cell(Param.CustomerNameHashtable.Contains(barcodeEntityList2[i].Customer) ? Param.CustomerNameHashtable[barcodeEntityList2[i].Customer].ToString() : barcodeEntityList2[i].Customer)
                            })
                        );
                        if (!Param.ShopNameHashtable.Contains(barcodeEntityList2[i].PartitionKey))
                            shopList.Add(barcodeEntityList2[i].PartitionKey);
                        if (!Param.CustomerNameHashtable.Contains(barcodeEntityList2[i].Customer))
                            customerList.Add(barcodeEntityList2[i].Customer);
                    }
                    table1.EndUpdate();
                    
                    FmClaimHistory fm = new FmClaimHistory();
                    fm.Show();
                }
                else if (change[0].ToString() == "เปลี่ยนสินค้า")
                {
                    string[] bar = change[1].ToString().Split(']');
                    lblStatus.Text = "สินค้านี้เปลี่ยนให้บาร์โค้ด " + bar[0].ToString() + "";
                    lblStatus.ForeColor = Color.Red;
                    lblStatus.Visible = true;
                    txtBarcode.Enabled = true;

                    table1.BeginUpdate();
                    shopList2 = new List<string>();
                    customerList2 = new List<string>();

                    for (int i = 0; i < barcodeEntityList2.Count; i++)
                    {
                        tableModel1.Rows.Add(new Row(
                            new Cell[] {
                        new Cell("" + (tableModel1.Rows.Count+1)),
                        new Cell(barcodeEntityList2[i].SellDate.ToLocalTime().ToString("dd MMMM yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("th-TH"))),
                        new Cell(Param.ShopNameHashtable.Contains(barcodeEntityList2[i].PartitionKey) ? Param.ShopNameHashtable[barcodeEntityList2[i].PartitionKey].ToString() : barcodeEntityList2[i].PartitionKey),
                        new Cell(barcodeEntityList2[i].ReceivedDate.ToLocalTime().ToString("dd MMMM yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("th-TH"))),
                        new Cell(Param.CustomerNameHashtable.Contains(barcodeEntityList2[i].Customer) ? Param.CustomerNameHashtable[barcodeEntityList2[i].Customer].ToString() : barcodeEntityList2[i].Customer)
                            })
                        );
                        if (!Param.ShopNameHashtable.Contains(barcodeEntityList2[i].PartitionKey))
                            shopList.Add(barcodeEntityList2[i].PartitionKey);
                        if (!Param.CustomerNameHashtable.Contains(barcodeEntityList2[i].Customer))
                            customerList.Add(barcodeEntityList2[i].Customer);
                    }
                    table1.EndUpdate();

                    FmClaimHistory fm = new FmClaimHistory();
                    fm.Show();


                }
                else
                {
                    lblStatus.Visible = false;
                    txtBarcode.Enabled = true;
                    txtBarcode.Text = "";

                    table1.BeginUpdate();
                    shopList2 = new List<string>();
                    customerList2 = new List<string>();

                    for (int i = 0; i < barcodeEntityList2.Count; i++)
                    {
                        tableModel1.Rows.Add(new Row(
                            new Cell[] {
                        new Cell("" + (tableModel1.Rows.Count+1)),
                        new Cell(barcodeEntityList2[i].SellDate.ToLocalTime().ToString("dd MMMM yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("th-TH"))),
                        new Cell(Param.ShopNameHashtable.Contains(barcodeEntityList2[i].PartitionKey) ? Param.ShopNameHashtable[barcodeEntityList2[i].PartitionKey].ToString() : barcodeEntityList2[i].PartitionKey),
                        new Cell(barcodeEntityList2[i].ReceivedDate.ToLocalTime().ToString("dd MMMM yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("th-TH"))),
                        new Cell(Param.CustomerNameHashtable.Contains(barcodeEntityList2[i].Customer) ? Param.CustomerNameHashtable[barcodeEntityList2[i].Customer].ToString() : barcodeEntityList2[i].Customer)
                            })
                        );
                        if (!Param.ShopNameHashtable.Contains(barcodeEntityList2[i].PartitionKey))
                            shopList.Add(barcodeEntityList2[i].PartitionKey);
                        if (!Param.CustomerNameHashtable.Contains(barcodeEntityList2[i].Customer))
                            customerList.Add(barcodeEntityList2[i].Customer);
                    }
                    table1.EndUpdate();
                }
            }
            catch (Exception ex){ Console.WriteLine(ex); }
            bwGetProduct.RunWorkerAsync();
        }
    }
}
