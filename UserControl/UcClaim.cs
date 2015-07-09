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

namespace PowerPOS_Online
{
    public partial class UcClaim : UserControl
    {
        private List<BarcodeEntity> barcodeEntityList;
        private ProductEntity productEntity;
        private string barcode;
        private bool downloading;
        private Hashtable shop = new Hashtable();
        private List<string> shopList;

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
            Param.azureTable = Param.azureTableClient.GetTableReference("Barcode2");
            TableQuery<BarcodeEntity> query = new TableQuery<BarcodeEntity>().Where(
                TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, barcode)
            );

            barcodeEntityList = new List<BarcodeEntity>();
            foreach (BarcodeEntity entity in Param.azureTable.ExecuteQuery(query))
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
                txtBarcode.Text = "";

                table1.BeginUpdate();
                tableModel1.Rows.Clear();
                tableModel1.RowHeight = 22;
                shopList = new List<string>();
                for (int i = 0; i < barcodeEntityList.Count; i++)
                {
                    tableModel1.Rows.Add(new Row(
                        new Cell[] {
                    new Cell("" + (i+1)),
                    new Cell(barcodeEntityList[i].SellDate),
                    new Cell(shop.Contains(barcodeEntityList[i].PartitionKey) ? shop[barcodeEntityList[i].PartitionKey].ToString() : barcodeEntityList[i].PartitionKey),
                    new Cell(barcodeEntityList[i].ReceivedDate),
                    new Cell(barcodeEntityList[i].Customer)
                    //,
                    //new Cell(Param.userEntityList[i].LastLogin.ToString("dd/MM/yyyy") == "01/01/0544" ? "-" : Param.userEntityList[i].LastLogin.ToString("dd/MM/yyyy hh:mm:ss")),
                    })
                    );
                    if (!shop.Contains(barcodeEntityList[i].PartitionKey))
                        shopList.Add(barcodeEntityList[i].PartitionKey);
                }
                table1.EndUpdate();
                bwGetProduct.RunWorkerAsync();
            }
        }

        private void bwGetProduct_DoWork(object sender, DoWorkEventArgs e)
        {
            var index = barcodeEntityList.Count - 1;

            Param.azureTable = Param.azureTableClient.GetTableReference("Product");
            TableOperation retrieveOperation = TableOperation.Retrieve<ProductEntity>(barcodeEntityList[index].PartitionKey, barcodeEntityList[index].Product.PadLeft(8, '0'));
            TableResult retrievedResult = Param.azureTable.Execute(retrieveOperation);
            productEntity = (ProductEntity)retrievedResult.Result;
            
            var filename = @"Resource/Images/Product/" + barcodeEntityList[index].PartitionKey + "-" + barcodeEntityList[index].Product.PadLeft(8, '0') + ".jpg";
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

        }

        private void bwGetProduct_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (productEntity != null)
            {
                lblName.Text = productEntity.Name;
                lblName.Visible = true;

                var remain = (productEntity.Warranty == 0) ? 0 : productEntity.Warranty - (DateTime.Now - barcodeEntityList[0].SellDate).TotalDays;

                btnClaim.Visible = remain > 0;

                lblWarranty.Text = (productEntity.Warranty == 0) ? "สินค้าไม่มีประกัน" : "ประกัน " + productEntity.Warranty + " วัน" +
                    ((remain > 0) ? " (เหลือประกันอีก " + remain.ToString("#,###") + " วัน)" : " (ประกันหมดแล้ว " + (remain * -1).ToString("#,###") + " วัน)");
                lblWarranty.Visible = true;

                lblWarrantyStatus.Text = (productEntity.Warranty == 0) ? "ไม่สามารถเคลมสินค้าได้ค่ะ" : ((remain <= 0) ? "สินค้าหมดประกันแล้วค่ะ" : "");
                lblWarrantyStatus.Visible = productEntity.Warranty == 0 || remain <= 0;

                ptbProduct.Image = null;

                var index = barcodeEntityList.Count - 1;
                var filename = @"Resource/Images/Product/" + barcodeEntityList[index].PartitionKey + "-" + barcodeEntityList[index].Product.PadLeft(8, '0') + ".jpg";
                if (File.Exists(filename) && !downloading)
                {
                    ptbProduct.Image = Image.FromFile(filename);
                    ptbProduct.Visible = true;
                }
                else
                {
                    var sp = productEntity.CoverImage.Split('|');
                    if (sp.Length > 2)
                    {
                        ptbProduct.ImageLocation = sp[1];
                        ptbProduct.Visible = true;
                    }
                    else
                    {
                        ptbProduct.Visible = false;
                    }
                }
            }
        }

        private void bwDownloadImage_DoWork(object sender, DoWorkEventArgs e)
        {
            var index = barcodeEntityList.Count - 1;
            var filename = @"Resource/Images/Product/" + barcodeEntityList[index].PartitionKey + "-" + barcodeEntityList[index].Product.PadLeft(8, '0') + ".jpg";
            var sp = productEntity.CoverImage.Split('|');
            if (sp.Length > 2)
            {
                if (!Directory.Exists("Resource/Images/Product")) Directory.CreateDirectory("Resource/Images/Product");
                using (var client = new WebClient())
                {
                    client.DownloadFile(sp[2], filename);
                }
            }

        }

        private void btnClaim_Click(object sender, EventArgs e)
        {

        }

        private void bwGetShopName_DoWork(object sender, DoWorkEventArgs e)
        {
            Param.azureTable = Param.azureTableClient.GetTableReference("Shop");
            for (int i = 0; i < shopList.Count; i++)
            {
                TableQuery<ShopEntity> query = new TableQuery<ShopEntity>().Where(
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, shopList[i])
                );
                foreach (ShopEntity entity in Param.azureTable.ExecuteQuery(query))
                {
                    shop[shopList[i]] = entity.Name;
                }
            }
        }

        private void bwGetShopName_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            for (int i = 0; i < tableModel1.Rows.Count; i++)
            {
                tableModel1.Rows[i].Cells[2].Text = shop[tableModel1.Rows[i].Cells[2].Text].ToString();
            }
        }
    }
}
