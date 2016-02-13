using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XPTable.Models;
using XPTable.Renderers;

namespace PowerPOS_Online
{
    public partial class FmInitialData : Form
    {
        const int _PROGRESS_ALL = 11;
        public static int _PROGRESS_STEP = 0;


        public FmInitialData()
        {
            InitializeComponent();
            Param.lblStatus = lblStatus;
        }

        private void FmInitialData_Load(object sender, EventArgs e)
        {
            pgbStatus.Value = 0;
            _PROGRESS_STEP = 0;

            if (!File.Exists(Param.LoadingImageLocal))
            {
                pictureBox1.ImageLocation = Param.LoadingImageUrl;
                DownloadLoadingImage();
            }
            else
            {
                try
                {
                    pictureBox1.Image = Image.FromFile(Param.LoadingImageLocal);
                }
                catch
                {
                    pictureBox1.ImageLocation = Param.LoadingImageUrl;
                    DownloadLoadingImage();
                }
            }

            Util.SetStatusMessage("กำลังตรวจสอบสิทธิ์การใช้งานระบบ", Param.StatusIcon.None);
            bwCheckLicense.RunWorkerAsync();
            
        }

        private async void DownloadLoadingImage()
        {
            if (!Directory.Exists("Resource/Images")) Directory.CreateDirectory("Resource/Images");
            if (File.Exists(Param.LoadingImageLocal)) File.Delete(Param.LoadingImageLocal);
            using (var client = new WebClient())
            {
                await client.DownloadFileTaskAsync(new Uri(Param.LoadingImageUrl), Param.LoadingImageLocal);
            }
        }

        private async void bwDownloadGif_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!Directory.Exists("Resource/Images")) Directory.CreateDirectory("Resource/Images");
            if (File.Exists(Param.LoadingImageLocal)) File.Delete(Param.LoadingImageLocal);
            using (var client = new WebClient())
            {
                await client.DownloadFileTaskAsync(new Uri(Param.LoadingImageUrl), Param.LoadingImageLocal);
            }
        }

        private void bwCheckLicense_DoWork(object sender, DoWorkEventArgs e)
        {
            Util.GetApiConfig();
            if (Param.ApiChecked)
            {
                if (Util.CanConnectInternet())
                {
                    dynamic jsonObject = Util.LoadAppConfig();
                    if (!jsonObject.success)
                        Param.ApiChecked = false;
                }
            }

            while (!Param.ApiChecked)
            {
                FmLicense fm = new FmLicense();
                var result = fm.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    break;
                }
            }
        }

        private void bwCheckLicense_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!Param.ApiChecked)
            {
                this.Dispose();
            }
            else
            {
                Param.AzureStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=http;AccountName=" + Param.DatabaseName + ";AccountKey=" + Param.DatabasePassword);
                Param.AzureTableClient = Param.AzureStorageAccount.CreateCloudTableClient();
                Util.SetStatusMessage("กำลัง Sync ข้อมูลเข้าระบบ Cloud", Param.StatusIcon.None);
                bwSyncData.RunWorkerAsync();
            }
        }

        private void bwLoadShopInfo_DoWork(object sender, DoWorkEventArgs e)
        {
            Util.LoadShopInfo();
        }

        private void bwLoadShopInfo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //this.Text = string.Format("Power POS - ร้าน {0} ({1})", Param.shopName, Param.computerName);
            //menuStrip1.Enabled = true;
            //toolStrip1.Enabled = true;

            _PROGRESS_STEP++;
            pgbStatus.Value = (int)(_PROGRESS_STEP * 1.0 / _PROGRESS_ALL * 1.0 * 100);

            Util.SetStatusMessage("กำลังโหลดข้อมูลการตั้งค่าระบบ", Param.StatusIcon.None);
            bwLoadShopConfig.RunWorkerAsync();
        }

        private void bwLoadShopConfig_DoWork(object sender, DoWorkEventArgs e)
        {
            Util.LoadConfig();
        }

        private void bwLoadShopConfig_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _PROGRESS_STEP++;
            pgbStatus.Value = (int)(_PROGRESS_STEP * 1.0 / _PROGRESS_ALL * 1.0 * 100);
            //mniSell_Click(sender, e);

            Util.SetStatusMessage("กำลังโหลดข้อมูลบาร์โค้ดสินค้า", Param.StatusIcon.None);
            bwLoadBarcode.RunWorkerAsync();
        }

        private void bwLoadBarcode_DoWork(object sender, DoWorkEventArgs e)
        {
            var startDate = DateTime.Now;
            var azureTable = Param.AzureTableClient.GetTableReference("BarcodeStock");
            TableQuery<BarcodeEntity> query = new TableQuery<BarcodeEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, Param.ShopId));
            //TableContinuationToken continuationToken = null;

            //var list = azureTable.ExecuteQuery(query).ToList();

            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS 'Barcode' (
                'Barcode' VARCHAR PRIMARY KEY NOT NULL,
                'OrderNo' VARCHAR NOT NULL,
                'Product' VARCHAR,
                'Cost' FLOAT DEFAULT 0,
                'OperationCost' FLOAT DEFAULT 0,
                'SellPrice' FLOAT DEFAULT 0,
                'ReceivedDate' VARCHAR,
                'ReceivedBy' VARCHAR DEFAULT 0,
                'SellNo' VARCHAR,
                'SellDate' VARCHAR,
                'SellBy' VARCHAR,
                'SellFinished' BOOL DEFAULT 0,
                'Customer' VARCHAR,
                'Comment' VARCHAR,
                'Stock' FLOAT DEFAULT 0,
                'Ship' FLOAT ,
                'Sync' BOOL DEFAULT 0)");

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            StringBuilder sb = new StringBuilder(@"INSERT OR REPLACE INTO Barcode (Barcode, OrderNo, Product, Cost, OperationCost, 
                    SellPrice, ReceivedDate, ReceivedBy, SellNo, SellDate, SellBy, SellFinished, Customer, Comment, Stock, Ship) ");
            var i = 0;

            foreach (BarcodeEntity d in azureTable.ExecuteQuery(query))
            {
                if (i != 0) sb.Append(" UNION ALL ");
                sb.Append(string.Format(@" SELECT '{0}', '{1}', '{2}', {3}, {4}, {5}, {6}, '{7}', '{8}', {9}, '{10}', {11}, '{12}', '{13}', '{14}','{15}'",
                    d.RowKey, d.OrderNo, d.Product, d.Cost, d.OperationCost == null ? 0 : d.OperationCost, d.SellPrice == null ? 0 : d.SellPrice,
                    d.ReceivedDate.ToString() == "1/1/0001 12:00:00 AM" ? "NULL" : "'" + d.ReceivedDate.ToString("yyyy-MM-dd HH:mm:ss") + "'", d.ReceivedBy, d.SellNo,
                    d.SellDate.ToString() == "1/1/0001 12:00:00 AM" ? "NULL" : "'" + d.SellDate.ToString("yyyy-MM-dd HH:mm:ss") + "'", d.SellBy, d.SellFinished ? 1 : 0, d.Customer, d.Comment, d.Stock, d.Ship));
                i++;
                if (i % 500 == 0)
                {
                    i = 0;
                    Util.DBExecute(sb.ToString());
                    sb = new StringBuilder(@"INSERT OR REPLACE INTO Barcode (Barcode, OrderNo, Product, Cost, OperationCost, 
                    SellPrice, ReceivedDate, ReceivedBy, SellNo, SellDate, SellBy, SellFinished, Customer, Comment, Stock, Ship) ");
                }
            }
            Util.DBExecute(sb.ToString());

            Console.WriteLine("Load shop barcode = {0} seconds", (DateTime.Now - startDate).TotalSeconds);

            LoadCategory(Param.ShopParent);
            LoadCategory(Param.ShopId);
            LoadBrand(Param.ShopParent);
            LoadBrand(Param.ShopId);
        }

        private void bwLoadBarcode_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _PROGRESS_STEP++;
            pgbStatus.Value = (int)(_PROGRESS_STEP * 1.0 / _PROGRESS_ALL * 1.0 * 100);
            Util.SetStatusMessage("กำลังดาวน์โหลดข้อมูลสินค้า", Param.StatusIcon.None);
            bwLoadProduct.RunWorkerAsync();
        }

        private void bwLoadProduct_DoWork(object sender, DoWorkEventArgs e)
        {
            var startDate = DateTime.Now;
            InsertProduct(Param.ShopParent);
            InsertProduct(Param.ShopId);
            Console.WriteLine("Load parent product = {0} seconds", (DateTime.Now - startDate).TotalSeconds);
        }

        private void bwLoadProduct_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _PROGRESS_STEP++;
            pgbStatus.Value = (int)(_PROGRESS_STEP * 1.0 / _PROGRESS_ALL * 1.0 * 100);
            Util.SetStatusMessage("กำลังเตรียมข้อมูลสินค้าเพื่อขาย", Param.StatusIcon.None);
            bwInitialShopProduct.RunWorkerAsync();
        }

        private void bwInitialShopProduct_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            DataTable dt;
            Console.WriteLine(Param.MemberType);

            //PurchaseOrder
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var azureTableOrder = Param.AzureTableClient.GetTableReference("PurchaseOrder");
            azureTableOrder.CreateIfNotExists();
            TableQuery<PurchaseOrderEntity> queryOrder = new TableQuery<PurchaseOrderEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, Param.ShopId));

            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS 'PurchaseOrder' (
                'Shop'  VARCHAR NOT NULL,
                'OrderNo' VARCHAR NOT NULL,
                'Product' VARCHAR NOT NULL,
                'Quantity' FLOAT NOT NULL,
                'ReceivedQty' FLOAT ,
                'PriceCost' FLOAT NOT NULL DEFAULT 0 ,
                'PriceTotal' FLOAT NOT NULL DEFAULT 0 ,
                'OrderDate' VARCHAR NOT NULL,
                'ReceivedDate' VARCHAR,
                'ReceivedBy' VARCHAR,
                'Sync' BOOL DEFAULT 0,
                PRIMARY KEY ('Shop', 'OrderNo', 'Product'))");

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            StringBuilder sb = new StringBuilder(@"INSERT OR REPLACE INTO PurchaseOrder (Shop, OrderNo, Product, Quantity, ReceivedQty, PriceCost, PriceTotal, ReceivedDate, ReceivedBy) ");
            i = 0;

            foreach (PurchaseOrderEntity d in azureTableOrder.ExecuteQuery(queryOrder))
            {
                string[] val = d.RowKey.Split('-');

                if (i != 0) sb.Append(" UNION ALL ");
                sb.Append(string.Format(@" SELECT '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}'",
                    Param.ShopId, val[0].ToString(), val[1].ToString(), d.Quantity == null ? 0 : d.Quantity, d.ReceivedQty == null ? 0 : d.ReceivedQty,
                    d.PriceCost == null ? 0 : d.PriceCost, d.PriceTotal == null ? 0 : d.PriceTotal,
                    d.ReceivedDate.ToString() == "1/1/0001 12:00:00 AM" ? "''" : "'" + d.ReceivedDate.ToString("yyyy-MM-dd HH:mm:ss") + "'", d.ReceivedBy));
                i++;
                if (i % 500 == 0)
                {
                    i = 0;
                    Util.DBExecute(sb.ToString());
                    sb = new StringBuilder(@"INSERT OR REPLACE INTO PurchaseOrder (Shop, OrderNo, Product, Quantity, ReceivedQty, PriceCost, PriceTotal, ReceivedDate, ReceivedBy) ");
                }
            }
            Util.DBExecute(sb.ToString());

            //if (Param.MemberType != "Sales")
            //{
            //    //dt = Util.DBQuery(string.Format(@"SELECT '{0}', p.ID, p.Name, p.CoverImage, p.Category, p.Brand, p.Price, p.Price1, p.Price2, p.Price3, p.Price4, p.Price5, p.Warranty,
            //    //p.Price WebPrice, p.Price1 WebPrice1, p.Price2 WebPrice2, p.Price3 WebPrice3, p.Price4 WebPrice4, p.Price5 WebPrice5, ps.Warranty WebWarranty, ps.Cost, p.Barcode
            //    //FROM (SELECT DISTINCT Product FROM Barcode) b
            //    //LEFT JOIN Product p
            //    //ON b.Product = p.ID
            //    //AND p.Shop = '{1}'
            //    //LEFT JOIN Product ps
            //    //ON b.Product = ps.ID
            //    //AND ps.Shop = '{0}'
            //    //", Param.ShopId, Param.ShopParent));

            //    //while (i < dt.Rows.Count)
            //    //{
            //    //    Util.DBExecute(string.Format(@"INSERT OR REPLACE INTO Product (Shop, ID, Name, CoverImage, Category, Brand, Price, Price1, Price2, Price3, Price4, Price5, Warranty,
            //    //    WebPrice, WebPrice1, WebPrice2, WebPrice3, WebPrice4, WebPrice5, WebWarranty, Cost, Barcode) 
            //    //    VALUES ('{0}','{1}','{2}','{3}','{4}','{5}',{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},'{21}')"
            //    //    , Param.ShopId, dt.Rows[i]["ID"].ToString(), dt.Rows[i]["Name"].ToString(), dt.Rows[i]["CoverImage"].ToString(), dt.Rows[i]["Category"].ToString(), dt.Rows[i]["Brand"].ToString(), dt.Rows[i]["Price"].ToString(), dt.Rows[i]["Price1"].ToString()
            //    //    , dt.Rows[i]["Price2"].ToString(), dt.Rows[i]["Price3"].ToString(), dt.Rows[i]["Price4"].ToString(), dt.Rows[i]["Price5"].ToString(), dt.Rows[i]["Warranty"].ToString(), dt.Rows[i]["WebPrice"].ToString(), dt.Rows[i]["WebPrice1"].ToString(), dt.Rows[i]["WebPrice2"].ToString()
            //    //    , dt.Rows[i]["WebPrice3"].ToString(), dt.Rows[i]["WebPrice4"].ToString(), dt.Rows[i]["WebPrice5"].ToString(), dt.Rows[i]["WebWarranty"].ToString(), dt.Rows[i]["Cost"].ToString(), dt.Rows[i]["Barcode"].ToString()));
            //    //    i++;
            //    //}
            //}
            //else
            //{
            //    //Util.DBExecute(string.Format(@"INSERT OR REPLACE INTO Product (Shop, ID, Name, CoverImage, Category, Brand, Price, Price1, Price2, Price3, Price4, Price5, Warranty,
            //    //    WebPrice, WebPrice1, WebPrice2, WebPrice3, WebPrice4, WebPrice5, WebWarranty, Cost) 
            //    //    SELECT '{0}', p.ID, p.Name, p.CoverImage, p.Category, p.Brand, p.Price, p.Price1, p.Price2, p.Price3, p.Price4, p.Price5, p.Warranty,
            //    //    p.Price WebPrice, p.Price1 WebPrice1, p.Price2 WebPrice2, p.Price3 WebPrice3, p.Price4 WebPrice4, p.Price5 WebPrice5, ps.Warranty WebWarranty, ps.Cost
            //    //    FROM (SELECT DISTINCT Product FROM Barcode) b
            //    //    LEFT JOIN Product p
            //    //    ON b.Product = p.ID
            //    //    AND p.Shop = '{1}'
            //    //    LEFT JOIN Product ps
            //    //    ON b.Product = ps.ID
            //    //    AND ps.Shop = '{0}'", Param.ShopId, Param.ShopParent));
            //    //i++;

            //}

            dt = Util.DBQuery(string.Format(@"SELECT DISTINCT shop, ID,Name,CoverImage, Category, Brand, Price,Price1, Price2, Price3, Price4, Price5, Warranty, webPrice, webPrice1, webPrice2, webPrice3, webPrice4, webPrice5, webWarranty, Cost, Barcode FROM 
            (SELECT '{0}' shop, p.ID, p.Name, p.CoverImage, p.Category, p.Brand, p.Price, p.Price1, p.Price2, p.Price3, p.Price4, p.Price5, p.Warranty,
            p.Price WebPrice, p.Price1 WebPrice1, p.Price2 WebPrice2, p.Price3 WebPrice3, p.Price4 WebPrice4, p.Price5 WebPrice5, ps.Warranty WebWarranty, ps.Cost, p.Barcode
            FROM (SELECT DISTINCT Product FROM Barcode) b
            LEFT JOIN Product p ON b.Product = p.ID AND p.Shop = '{1}'
            LEFT JOIN Product ps ON b.Product = ps.ID AND ps.Shop = '{0}'
            WHERE NOT EXISTS(SELECT *
                                FROM Product pp
                                WHERE ps.id = pp.id
                                AND pp.shop = '{0}')
            UNION ALL 
            SELECT '{0}' shop, p.ID, p.Name, p.CoverImage, p.Category, p.Brand, p.Price, p.Price1, p.Price2, p.Price3, p.Price4, p.Price5, p.Warranty,
            p.Price WebPrice, p.Price1 WebPrice1, p.Price2 WebPrice2, p.Price3 WebPrice3, p.Price4 WebPrice4, p.Price5 WebPrice5, ps.Warranty WebWarranty, ps.Cost, p.Barcode
            FROM (SELECT DISTINCT Product FROM PurchaseOrder) b
            LEFT JOIN Product p ON b.Product = p.ID AND p.Shop = '{1}'
            LEFT JOIN Product ps ON b.Product = ps.ID AND ps.Shop = '{0}'
            WHERE NOT EXISTS (SELECT *
                                FROM Product pp
                                WHERE ps.id = pp.id
                                AND pp.shop = '{0}')) pp
            ", Param.ShopId, Param.ShopParent));

            i = 0;
            while (i < dt.Rows.Count)
            {
                Util.DBExecute(string.Format(@"INSERT OR REPLACE INTO Product (Shop, ID, Name, CoverImage, Category, Brand, Price, Price1, Price2, Price3, Price4, Price5, Warranty,
                WebPrice, WebPrice1, WebPrice2, WebPrice3, WebPrice4, WebPrice5, WebWarranty, Cost, Barcode, Sync) 
                VALUES ('{0}','{1}','{2}','{3}','{4}','{5}',{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},'{21}', 1)"
                , Param.ShopId, dt.Rows[i]["ID"].ToString(), dt.Rows[i]["Name"].ToString(), dt.Rows[i]["CoverImage"].ToString(), dt.Rows[i]["Category"].ToString(), dt.Rows[i]["Brand"].ToString(), 0, 0, 0, 0, 0, 0
                , dt.Rows[i]["Warranty"].ToString(), dt.Rows[i]["WebPrice"].ToString() == "" ? "''" : dt.Rows[i]["WebPrice"].ToString(), dt.Rows[i]["WebPrice1"].ToString() == "" ? "''" : dt.Rows[i]["WebPrice1"].ToString(), dt.Rows[i]["WebPrice2"].ToString() == "" ? "''" : dt.Rows[i]["WebPrice2"].ToString()
                , dt.Rows[i]["WebPrice3"].ToString() == "" ? "''" : dt.Rows[i]["WebPrice3"].ToString(), dt.Rows[i]["WebPrice4"].ToString() == "" ? "''" : dt.Rows[i]["WebPrice4"].ToString(), dt.Rows[i]["WebPrice5"].ToString() == "" ? "''" : dt.Rows[i]["WebPrice5"].ToString(), dt.Rows[i]["WebWarranty"].ToString() == "" ? "''" : dt.Rows[i]["WebWarranty"].ToString(), dt.Rows[i]["Cost"].ToString() == "" ? "''" : dt.Rows[i]["Cost"].ToString(), dt.Rows[i]["Barcode"].ToString()));
                i++;
            }
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                dt = Util.DBQuery("SELECT * FROM Product WHERE Sync = 1");

                var azureT = Param.AzureTableClient.GetTableReference("Product");
                TableBatchOperation batch = new TableBatchOperation();
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    dynamic d = new DynamicEntity(Param.ShopId, row["ID"].ToString());
                    d.Brand = row["Brand"].ToString();
                    d.Category = row["Category"].ToString();
                    d.Name = row["Name"].ToString();
                    d.CoverImage = row["CoverImage"].ToString();
                    if (row["Warranty"].ToString() == "" || row["Warranty"].ToString() == null) { d.Warranty = 0; } else { d.Warranty = int.Parse(row["Warranty"].ToString()); }
                    if (row["Price"].ToString() == "" || row["Price"].ToString() == null) { d.Price = 0; } else { d.Price = double.Parse(row["Price"].ToString()); }
                    if (row["Price1"].ToString() == "" || row["Price1"].ToString() == null) { d.Price1 = 0; } else { d.Price1 = double.Parse(row["Price1"].ToString()); }
                    if (row["Price2"].ToString() == "" || row["Price2"].ToString() == null) { d.Price2 = 0; } else { d.Price2 = double.Parse(row["Price2"].ToString()); }
                    if (row["Price3"].ToString() == "" || row["Price3"].ToString() == null) { d.Price3 = 0; } else { d.Price3 = double.Parse(row["Price3"].ToString()); }
                    if (row["Price4"].ToString() == "" || row["Price4"].ToString() == null) { d.Price4 = 0; } else { d.Price4 = double.Parse(row["Price4"].ToString()); }
                    if (row["Price5"].ToString() == "" || row["Price5"].ToString() == null) { d.Price5 = 0; } else { d.Price5 = double.Parse(row["Price5"].ToString()); }
                    if (row["Cost"].ToString() == "" || row["Cost"].ToString() == null) { d.Cost = 0; } else { d.Cost = double.Parse(row["Cost"].ToString()); }
                    if (row["Quantity"].ToString() == "" || row["Quantity"].ToString() == null) { d.Quantity = 0; } else { d.Quantity = double.Parse(row["Quantity"].ToString()); }

                    batch.InsertOrMerge(d);

                    Util.DBExecute(string.Format("UPDATE Product SET Sync = '0' WHERE ID = '{0}' AND Shop = '{1}'", row["ID"].ToString(), Param.ShopId));

                    if (batch.Count == 100)
                    {
                        azureT.ExecuteBatch(batch);
                        batch = new TableBatchOperation();
                    }
                }
                if (batch.Count > 0)
                    azureT.ExecuteBatch(batch);
            }
            catch (Exception ex)
            {
                Util.WriteErrorLog(ex.Message);
                Util.WriteErrorLog(ex.StackTrace);
            }

            if (Param.MemberType != "Sales")
            {
                if (Param.ShopId == "00000018")
                {
                    var json = Param.SystemConfig.SellPrice;
                    Console.WriteLine(json.Price);
                    Util.DBExecute(string.Format(@"UPDATE Product SET Price = {1}, Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price,0) <> {1}", Param.ShopId, json.Price));
                    //Console.WriteLine(string.Format(@"UPDATE Product SET Price = (SELECT {1} FROM Product WHERE Shop = '{2}'), Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price,0) <> {1}", Param.ShopId, json.Price, Param.ShopParent));
                    Util.DBExecute(string.Format(@"UPDATE Product SET Price1 = {1}, Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price1,0) <> {1}", Param.ShopId, json.Price1));
                    Util.DBExecute(string.Format(@"UPDATE Product SET Price2 = {1}, Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price2,0) <> {1}", Param.ShopId, json.Price2));
                    Util.DBExecute(string.Format(@"UPDATE Product SET Price3 = {1}, Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price3,0) <> {1}", Param.ShopId, json.Price3));
                    Util.DBExecute(string.Format(@"UPDATE Product SET Price4 = {1}, Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price4,0) <> {1}", Param.ShopId, json.Price4));
                    Util.DBExecute(string.Format(@"UPDATE Product SET Price5 = {1}, Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price5,0) <> {1}", Param.ShopId, json.Price5));
                    Util.DBExecute(string.Format(@"UPDATE Product SET Warranty = IFNULL(WebWarranty,0), Sync = 1 WHERE Shop = '{0}' AND IFNULL(WebWarranty,0) <> IFNULL(Warranty,'')", Param.ShopId));
                    Util.DBExecute(string.Format(@"UPDATE Product SET Price = {1},Price1 = {1},Price2 = {1},Price3 = {1},Price4 = {1},Sync = 1 WHERE Shop = '{0}' AND Category = '00001'", Param.ShopId, "99"));
                }
                //else if (Param.ShopId == "00000009")
                //{
                //    var json = Param.SystemConfig.SellPrice;
                //    Console.WriteLine(json.Price);
                //    Util.DBExecute(string.Format(@"UPDATE Product SET Price = {1}, Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price,0) <> {1}", Param.ShopId, json.Price));
                //    Util.DBExecute(string.Format(@"UPDATE Product SET Price1 = {1}, Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price1,0) <> {1}", Param.ShopId, json.Price1));
                //    Util.DBExecute(string.Format(@"UPDATE Product SET Price2 = {1}, Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price2,0) <> {1}", Param.ShopId, json.Price2));
                //    Util.DBExecute(string.Format(@"UPDATE Product SET Price3 = {1}, Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price3,0) <> {1}", Param.ShopId, json.Price3));
                //    Util.DBExecute(string.Format(@"UPDATE Product SET Price4 = {1}, Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price4,0) <> {1}", Param.ShopId, json.Price4));
                //    Util.DBExecute(string.Format(@"UPDATE Product SET Price5 = {1}, Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price5,0) <> {1}", Param.ShopId, json.Price5));
                //    Util.DBExecute(string.Format(@"UPDATE Product SET Warranty = IFNULL(WebWarranty,0), Sync = 1 WHERE Shop = '{0}' AND IFNULL(WebWarranty,0) <> IFNULL(Warranty,'')", Param.ShopId));
                //    Util.DBExecute(string.Format(@"UPDATE Product SET Price = {1},Price1 = {1},Price2 = {1},Price3 = {1},Price4 = {1},Price5 = {1} ,Sync = 1 WHERE Shop = '{0}' AND Category = '00001'", Param.ShopId, "99"));
                //}
                else
                {
                    var json = Param.SystemConfig.SellPrice;
                    Console.WriteLine(json.Price);
                    Util.DBExecute(string.Format(@"UPDATE Product SET Price = (SELECT p.{1} FROM Product p   WHERE Product.ID = p.ID   AND p.shop = '{2}'), Sync = 1 WHERE Shop = '{0}'", Param.ShopId, json.Price, Param.ShopParent));
                    Console.WriteLine(string.Format(@"UPDATE Product SET Price = (SELECT {1} FROM Product WHERE Shop = '{2}'), Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price,0) <> {1}", Param.ShopId, json.Price, Param.ShopParent));
                    Util.DBExecute(string.Format(@"UPDATE Product SET Price1 = (SELECT p.{1} FROM Product p   WHERE Product.ID = p.ID   AND p.shop = '{2}'), Sync = 1 WHERE Shop = '{0}' ", Param.ShopId, json.Price1, Param.ShopParent));
                    Util.DBExecute(string.Format(@"UPDATE Product SET Price2 = (SELECT p.{1} FROM Product p   WHERE Product.ID = p.ID   AND p.shop = '{2}'), Sync = 1 WHERE Shop = '{0}' ", Param.ShopId, json.Price2, Param.ShopParent));
                    Util.DBExecute(string.Format(@"UPDATE Product SET Price3 = (SELECT p.{1} FROM Product p   WHERE Product.ID = p.ID   AND p.shop = '{2}'), Sync = 1 WHERE Shop = '{0}' ", Param.ShopId, json.Price3, Param.ShopParent));
                    Util.DBExecute(string.Format(@"UPDATE Product SET Price4 = (SELECT p.{1} FROM Product p   WHERE Product.ID = p.ID   AND p.shop = '{2}'), Sync = 1 WHERE Shop = '{0}' ", Param.ShopId, json.Price4, Param.ShopParent));
                    Util.DBExecute(string.Format(@"UPDATE Product SET Price5 = (SELECT p.{1} FROM Product p   WHERE Product.ID = p.ID   AND p.shop = '{2}'), Sync = 1 WHERE Shop = '{0}' ", Param.ShopId, json.Price5, Param.ShopParent));
                    Util.DBExecute(string.Format(@"UPDATE Product SET Warranty = IFNULL(WebWarranty,0), Sync = 1 WHERE Shop = '{0}' AND IFNULL(WebWarranty,0) <> IFNULL(Warranty,'')", Param.ShopId));
                }
            }
            //else
            //{
            //    Util.DBExecute(string.Format(@"UPDATE Product SET Sync = 1 WHERE Shop = '{0}'", Param.ShopId));
            //}
            // เพิ่มลงใน Product
     

            Util.DBExecute(string.Format(@"UPDATE Product SET  Price5 = NULL, Cost = NULL, WebPrice5 = NULL
                WHERE Shop = '{0}'", Param.ShopParent));

            dt = Util.DBQuery(string.Format(@"SELECT ID, Name, CoverImage, Price, Price1, Price2, Price3, Price4, Price5, Warranty, IFNULL(Cost,0) Cost, Category, Brand 
                FROM Product
                WHERE Shop = '{0}'
                AND Sync = 1", Param.ShopId));
            Console.WriteLine("Update product total = {0} records", dt.Rows.Count);

            var azureTable = Param.AzureTableClient.GetTableReference("Product");
            TableBatchOperation batchOperation = new TableBatchOperation();
            for (i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    ProductUpdateEntity data = new ProductUpdateEntity(Param.ShopId, dt.Rows[i]["ID"].ToString());
                    data.Name = dt.Rows[i]["Name"].ToString();
                    data.CoverImage = dt.Rows[i]["CoverImage"].ToString();
                    if (dt.Rows[i]["Price"].ToString() == "" || dt.Rows[i]["Price"].ToString() == null) { data.Price = 0; } else { data.Price = double.Parse(dt.Rows[i]["Price"].ToString()); }
                    if (dt.Rows[i]["Price1"].ToString() == "" || dt.Rows[i]["Price1"].ToString() == null) { data.Price1 = 0; } else { data.Price1 = double.Parse(dt.Rows[i]["Price1"].ToString()); }
                    if (dt.Rows[i]["Price2"].ToString() == "" || dt.Rows[i]["Price2"].ToString() == null) { data.Price2 = 0; } else { data.Price2 = double.Parse(dt.Rows[i]["Price2"].ToString()); }
                    if (dt.Rows[i]["Price3"].ToString() == "" || dt.Rows[i]["Price3"].ToString() == null) { data.Price3 = 0; } else { data.Price3 = double.Parse(dt.Rows[i]["Price3"].ToString()); }
                    if (dt.Rows[i]["Price4"].ToString() == "" || dt.Rows[i]["Price4"].ToString() == null) { data.Price4 = 0; } else { data.Price4 = double.Parse(dt.Rows[i]["Price4"].ToString()); }
                    if (dt.Rows[i]["Price5"].ToString() == "" || dt.Rows[i]["Price5"].ToString() == null) { data.Price5 = 0; } else { data.Price5 = double.Parse(dt.Rows[i]["Price5"].ToString()); }
                    if (dt.Rows[i]["Cost"].ToString() == "" || dt.Rows[i]["Cost"].ToString() == null) { data.Cost = 0; } else { data.Cost = double.Parse(dt.Rows[i]["Cost"].ToString()); }
                    if (dt.Rows[i]["Warranty"].ToString() == "" || dt.Rows[i]["Warranty"].ToString() == null) { data.Warranty = 0; } else { data.Warranty = int.Parse(dt.Rows[i]["Warranty"].ToString()); }

                    data.Category = dt.Rows[i]["Category"].ToString();
                    data.Brand = dt.Rows[i]["Brand"].ToString();
                    //data.Warranty = int.Parse(dt.Rows[i]["Warranty"].ToString());
                    //data.Cost = double.Parse(dt.Rows[i]["Cost"].ToString());
                    //data.Price = double.Parse(dt.Rows[i]["Price"].ToString());
                    //data.Price1 = double.Parse(dt.Rows[i]["Price1"].ToString());
                    //data.Price2 = double.Parse(dt.Rows[i]["Price2"].ToString());
                    //data.Price3 = double.Parse(dt.Rows[i]["Price3"].ToString());
                    //data.Price4 = double.Parse(dt.Rows[i]["Price4"].ToString());
                    //data.Price5 = double.Parse(dt.Rows[i]["Price5"].ToString());                    
                    batchOperation.InsertOrMerge(data);
                    if (batchOperation.Count == 100)
                    {
                        azureTable.ExecuteBatch(batchOperation);
                        batchOperation = new TableBatchOperation();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            if (batchOperation.Count > 0)
            {
                azureTable.ExecuteBatch(batchOperation);
            }
            Util.DBExecute(string.Format(@"UPDATE Product SET Sync = 0 WHERE Shop = '{0}'", Param.ShopId));



            Util.DBExecute(string.Format(@"INSERT OR REPLACE INTO Brand (Shop, ID, Name, Sync)
                SELECT DISTINCT '{1}', bd.ID, bd.Name, 1
                FROM (SELECT DISTINCT Product FROM Barcode) b
                    LEFT JOIN Product p
                    ON b.Product = p.ID
                    LEFT JOIN Brand bd
                    ON p.Brand = bd.ID
                    AND bd.Shop = '{0}'
                WHERE bd.ID NOT IN ( SELECT ID FROM Brand WHERE Shop = '{1}' )
                ", Param.ShopParent, Param.ShopId));

            dt = Util.DBQuery(string.Format(@"SELECT ID, Name FROM Brand WHERE Shop = '{0}' AND Sync = 1", Param.ShopId));
            azureTable = Param.AzureTableClient.GetTableReference("Brand");
            batchOperation = new TableBatchOperation();
            for (i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    CategoryEntity data = new CategoryEntity(Param.ShopId, dt.Rows[i]["ID"].ToString());
                    data.Name = dt.Rows[i]["Name"].ToString();
                    batchOperation.InsertOrMerge(data);
                }
                catch { }
            }
            if (batchOperation.Count > 0)
                azureTable.ExecuteBatch(batchOperation);
            Util.DBExecute(string.Format(@"DELETE Brand WHERE Shop = '{0}'", Param.ShopParent));
            Util.DBExecute(string.Format(@"UPDATE Brand SET Sync = 0 WHERE Shop = '{0}'", Param.ShopId));
            Console.WriteLine("Update brand total = {0} records", dt.Rows.Count);




            Util.DBExecute(string.Format(@"INSERT OR REPLACE INTO Category (Shop, ID, Name, Sync)
                SELECT DISTINCT '{1}', bd.ID, bd.Name, 1
                FROM (SELECT DISTINCT Product FROM Barcode) b
                    LEFT JOIN Product p
                    ON b.Product = p.ID
                    LEFT JOIN Category bd
                    ON p.Category = bd.ID
                    AND bd.Shop = '{0}'
                WHERE bd.ID NOT IN ( SELECT ID FROM Category WHERE Shop = '{1}' )
                ", Param.ShopParent, Param.ShopId));

            dt = Util.DBQuery(string.Format(@"SELECT ID, Name FROM Category WHERE Shop = '{0}' AND Sync = 1", Param.ShopId));
            azureTable = Param.AzureTableClient.GetTableReference("Category");
            batchOperation = new TableBatchOperation();
            for (i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    CategoryEntity data = new CategoryEntity(Param.ShopId, dt.Rows[i]["ID"].ToString());
                    data.Name = dt.Rows[i]["Name"].ToString();
                    batchOperation.InsertOrMerge(data);
                }
                catch { }
            }
            if (batchOperation.Count > 0)
                azureTable.ExecuteBatch(batchOperation);
            Util.DBExecute(string.Format(@"DELETE Category WHERE Shop = '{0}'", Param.ShopParent));
            Util.DBExecute(string.Format(@"UPDATE Category SET Sync = 0 WHERE Shop = '{0}'", Param.ShopId));
            Console.WriteLine("Update category total = {0} records", dt.Rows.Count);



            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS 'SellTemp' (
                'Product' VARCHAR NOT NULL,
                'ProductName' VARCHAR NOT NULL,
                'Price' FLOAT NOT NULL DEFAULT 0,
                'Amount' FLOAT NOT NULL DEFAULT 0 ,
                'TotalPrice' FLOAT NOT NULL DEFAULT 0,
                'PriceCost' FLOAT NOT NULL DEFAULT 0)");
        }

        private void bwInitialShopProduct_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _PROGRESS_STEP++;
            pgbStatus.Value = (int)(_PROGRESS_STEP * 1.0 / _PROGRESS_ALL * 1.0 * 100);
            Util.SetStatusMessage("กำลังโหลดข้อมูลลูกค้า", Param.StatusIcon.None);
            bwLoadCustomer.RunWorkerAsync();
        }

        private void LoadCategory(string shop)
        {
            var azureTable = Param.AzureTableClient.GetTableReference("Category");

            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS 'Category' (
                'Shop' VARCHAR NOT NULL ,
                'ID' VARCHAR NOT NULL,
                'Name' VARCHAR NOT NULL,
                'CanDelete' BOOL DEFAULT 0,
                'Active' BOOL DEFAULT 1,
                'Priority' INT DEFAULT 0,
                'Sync' BOOL DEFAULT 0,
                PRIMARY KEY ('Shop', 'ID'))");
            Util.DBExecute(string.Format(@"DELETE FROM Category WHERE Shop = '{0}'", Param.ShopId));

            const string command = @"INSERT OR REPLACE INTO Category (Shop, ID, Name, Active, Priority) ";
            var sb = new StringBuilder(command);
            int i = 0;
            TableQuery<CategoryEntity> queryC = new TableQuery<CategoryEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, shop));
            foreach (CategoryEntity d in azureTable.ExecuteQuery(queryC))
            {
                if (i != 0) sb.Append(" UNION ALL ");
                sb.Append(string.Format(@" SELECT '{0}', '{1}', '{2}', {3}, {4}", shop, d.RowKey, d.Name, d.Active ? 1 : 0, d.Priority));
                i++;
                if (i % 500 == 0)
                {
                    i = 0;
                    Util.DBExecute(sb.ToString());
                    sb = new StringBuilder(command);
                }
            }
            if (i != 0)
                Util.DBExecute(sb.ToString());


            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS 'CategoryProfit' (
                    'ID' VARCHAR NOT NULL ,
                    'Price' FLOAT DEFAULT 0,
                    'Price1' FLOAT DEFAULT 0,
                    'Price2' FLOAT DEFAULT 0,
                    'Price3' FLOAT DEFAULT 0, 
                    'Price4' FLOAT DEFAULT 0, 
                    'Price5' FLOAT DEFAULT 0,
                    'Sync' BOOL DEFAULT 0,
                    PRIMARY KEY ('ID'))");

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var azureTableA = Param.AzureTableClient.GetTableReference("CategoryProfit");
            azureTableA.CreateIfNotExists();
            var queryA = new TableQuery<CategoryProfitEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, Param.ShopId));
            const string comm = @"INSERT OR REPLACE INTO CategoryProfit (ID, Price, Price1, Price2, Price3, Price4, Price5) ";
            var sbb = new StringBuilder(comm);

            i = 0;
            foreach (CategoryProfitEntity d in azureTableA.ExecuteQuery(queryA))
            {
                if (i != 0) sbb.Append(" UNION ALL ");
                sbb.Append(string.Format(@" SELECT '{0}', {1}, {2}, {3}, {4}, {5}, {6}", d.RowKey, d.Price, d.Price1, d.Price2, d.Price3, d.Price4, d.Price5));
                i++;
                if (i % 500 == 0)
                {
                    i = 0;
                    Util.DBExecute(sbb.ToString());
                    sbb = new StringBuilder(comm);
                }
            }
            Util.DBExecute(sbb.ToString());


            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS 'ChangePrice' (
                    'SellNo' VARCHAR NOT NULL ,
                    'Product' VARCHAR NOT NULL,
                    'Price' FLOAT DEFAULT 0,
                    'ChangePrice' FLOAT DEFAULT 0,
                    'ChangeBy' VARCHAR , 
                    'Sync' BOOL DEFAULT 0,
                    PRIMARY KEY ('SellNo', 'Product'))");

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var azureTableCh = Param.AzureTableClient.GetTableReference("ChangePrice");
            azureTableCh.CreateIfNotExists();
            var queryCh = new TableQuery<ChangePriceEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, Param.ShopId));
            const string comman = @"INSERT OR REPLACE INTO ChangePrice (SellNo, Product, Price, ChangePrice, ChangeBy) ";
            var sbCh = new StringBuilder(comman);

            i = 0;
            foreach (ChangePriceEntity d in azureTableCh.ExecuteQuery(queryCh))
            {
                if (i != 0) sbCh.Append(" UNION ALL ");
                sbCh.Append(string.Format(@" SELECT '{0}', {1}, {2}, {3}, {4}, {5}, {6}", d.RowKey, d.Product, d.Price,  d.ChangePrice, d.ChangeBy));
                i++;
                if (i % 500 == 0)
                {
                    i = 0;
                    Util.DBExecute(sbCh.ToString());
                    sbCh = new StringBuilder(comman);
                }
            }
            Util.DBExecute(sbCh.ToString());
        }

        private void LoadBrand(string shop)
        {
            var azureTable = Param.AzureTableClient.GetTableReference("Brand");

            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS 'Brand' (
                'Shop' VARCHAR NOT NULL ,
                'ID' VARCHAR NOT NULL,
                'Name' VARCHAR NOT NULL,
                'CanDelete' BOOL DEFAULT 0,
                'Active' BOOL DEFAULT 1,
                'Priority' INT DEFAULT 0,
                'Sync' BOOL DEFAULT 0,
                PRIMARY KEY ('Shop', 'ID'))");
            Util.DBExecute(string.Format(@"DELETE FROM Brand WHERE Shop = '{0}'", Param.ShopId));

            const string command = @"INSERT OR REPLACE INTO Brand (Shop, ID, Name, Active, Priority) ";
            var sb = new StringBuilder(command);
            int i = 0;
            TableQuery<CategoryEntity> queryC = new TableQuery<CategoryEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, shop));
            foreach (CategoryEntity d in azureTable.ExecuteQuery(queryC))
            {
                if (i != 0) sb.Append(" UNION ALL ");
                sb.Append(string.Format(@" SELECT '{0}', '{1}', '{2}', {3}, {4}", shop, d.RowKey, d.Name, d.Active ? 1 : 0, d.Priority));
                i++;
                if (i % 500 == 0)
                {
                    i = 0;
                    Util.DBExecute(sb.ToString());
                    sb = new StringBuilder(command);
                }
            }
            if (i != 0)
                Util.DBExecute(sb.ToString());
        }

        private void InsertProduct(string shop)
        {
            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS 'Product' (
                'Shop' VARCHAR NOT NULL ,
                'ID' VARCHAR NOT NULL ,
                'Name' VARCHAR, 
                'CoverImage' VARCHAR,
                'Price' FLOAT DEFAULT 0,
                'Price1' FLOAT DEFAULT 0,
                'Price2' FLOAT DEFAULT 0,
                'Price3' FLOAT DEFAULT 0,
                'Price4' FLOAT DEFAULT 0,
                'Price5' FLOAT DEFAULT 0,
                'Warranty' INT DEFAULT 0,
                'WebPrice' FLOAT DEFAULT 0,
                'WebPrice1' FLOAT DEFAULT 0,
                'WebPrice2' FLOAT DEFAULT 0,
                'WebPrice3' FLOAT DEFAULT 0,
                'WebPrice4' FLOAT DEFAULT 0,
                'WebPrice5' FLOAT DEFAULT 0,
                'WebWarranty' INT DEFAULT 0,
                'Cost' FLOAT DEFAULT 0,
                'Category' VARCHAR,
                'Brand' VARCHAR,
                'Barcode' VARCHAR,
                'Quantity' FLOAT DEFAULT 0,
                'Sync' BOOL DEFAULT 0,
                PRIMARY KEY ('Shop', 'ID'))
            ");

            var azureTable = Param.AzureTableClient.GetTableReference("Product");
            var query = new TableQuery<ProductEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, shop));
            const string command = @"INSERT OR REPLACE INTO Product (Shop, ID, Name, Price, Price1, Price2, Price3, Price4, Price5, Warranty, Cost, CoverImage, Category, Brand, Barcode, Quantity) ";
            var sb = new StringBuilder(command);
            int i = 0;
            foreach (ProductEntity d in azureTable.ExecuteQuery(query))
            {
                if (i != 0) sb.Append(" UNION ALL ");
                sb.Append(string.Format(@" SELECT '{0}', '{1}', '{2}', {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, '{11}', '{12}', '{13}', '{14}', '{15}'",
                    d.PartitionKey, d.RowKey, d.Name, d.Price, d.Price1, d.Price2, d.Price3, d.Price4, d.Price5, d.Warranty, d.Cost, d.CoverImage, d.Category, d.Brand, d.BuyerCode, d.Quantity == null ? 0 : d.Quantity));
              
                i++;
                if (i % 500 == 0)
                {
                    i = 0;
                    Util.DBExecute(sb.ToString());
                    sb = new StringBuilder(command);
                }
            }
            Util.DBExecute(sb.ToString());
        }

        private void bwSyncData_DoWork(object sender, DoWorkEventArgs e)
        {
            Util.SyncData();
        }

        private void bwSyncData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _PROGRESS_STEP++;
            pgbStatus.Value = (int)(_PROGRESS_STEP * 1.0 / _PROGRESS_ALL * 1.0 * 100);
            Util.SetStatusMessage("กำลังโหลดข้อมูลรายละเอียดของร้านค้า", Param.StatusIcon.None);
            bwLoadShopInfo.RunWorkerAsync();
        }

        private void bwLoadCustomer_DoWork(object sender, DoWorkEventArgs e)
        {
            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS 'Customer' (
                'ID' VARCHAR PRIMARY KEY NOT NULL,
                'Member' VARCHAR,
                'Firstname' VARCHAR,
                'Lastname' VARCHAR,
                'Nickname' VARCHAR,
                'CitizenID' VARCHAR,
                'Birthday' VARCHAR,
                'Sex' VARCHAR,
                'CardNo' VARCHAR,
                'Mobile' VARCHAR,
                'Email' VARCHAR,
                'ShopName' VARCHAR,
                'Address' VARCHAR,
                'Address2' VARCHAR,
                'SubDistrict' VARCHAR,
                'District' VARCHAR,
                'Province' VARCHAR,
                'ZipCode' VARCHAR,
                'ShopSameAddress' BOOL DEFAULT 1,
                'ShopAddress' VARCHAR,
                'ShopAddress2' VARCHAR,
                'ShopSubDistrict' VARCHAR,
                'ShopDistrict' VARCHAR,
                'ShopProvince' VARCHAR,
                'ShopZipCode' VARCHAR,
                'SellPrice' INT DEFAULT 0,
                'DiscountPercent' INT DEFAULT 0,
                'Credit' INT DEFAULT 0,
                'Comment' VARCHAR,
                'AddDate' VARCHAR,
                'AddBy' VARCHAR,
                'UpdateDate' VARCHAR,
                'UpdateBy' VARCHAR,
                'Sync' BOOL DEFAULT 0)");

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var azureTable = Param.AzureTableClient.GetTableReference("Customer");
            var query = new TableQuery<CustomerEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, Param.ShopId));
            const string command = @"INSERT OR REPLACE INTO Customer (ID, Member, Firstname, Lastname, Nickname, CitizenID, Birthday, 
                Sex, CardNo, Mobile, Email, ShopName,
                Address, Address2, SubDistrict, District, Province, ZipCode, 
                ShopSameAddress, ShopAddress, ShopAddress2, ShopSubDistrict, ShopDistrict, ShopProvince, ShopZipCode, 
                SellPrice, DiscountPercent, Credit, Comment, AddDate, AddBy, UpdateDate, UpdateBy) ";
            var sb = new StringBuilder(command);
            int i = 0;
            foreach (CustomerEntity d in azureTable.ExecuteQuery(query))
            {
                if (i != 0) sb.Append(" UNION ALL ");
                sb.Append(string.Format(@" SELECT '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}',
                    '{7}', '{8}', '{9}', '{10}', '{11}',
                    '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', 
                    {18}, '{19}', '{20}', '{21}', '{22}', '{23}', '{24}',
                    {25}, {26}, {27}, '{28}', '{29}', '{30}', '{31}', '{32}'",
                    d.RowKey, d.Member, d.Firstname, d.Lastname, d.Nickname, d.CitizenID,
                    d.Birthday.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00" ? "" : d.Birthday.ToString("yyyy-MM-dd HH:mm:ss"), 
                    d.Sex, d.CardNo, d.Mobile, d.Email, d.ShopName,
                    d.Address, d.Address2, d.SubDistrict, d.District, d.Province, d.ZipCode,
                    d.ShopSameAddress ? 1 : 0, d.ShopAddress, d.ShopAddress2, d.ShopSubDistrict, d.ShopDistrict, d.ShopProvince, d.ShopZipCode,
                    d.SellPrice, d.DiscountPercent, d.Credit, d.Comment, d.AddDate.ToString("yyyy-MM-dd HH:mm:ss"), d.AddBy,
                    d.UpdateDate.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00" ? "" : d.UpdateDate.ToString("yyyy-MM-dd HH:mm:ss"), d.UpdateBy));
                i++;
                if (i % 500 == 0)
                {
                    i = 0;
                    Util.DBExecute(sb.ToString());
                    sb = new StringBuilder(command);
                }
            }
            Util.DBExecute(sb.ToString());

            if (i == 0){
                Util.DBExecute(@"INSERT OR REPLACE INTO Customer (ID, Firstname, Lastname, AddDate, AddBy, Sync)
                SELECT '000000', 'ลูกค้า', 'ทั่วไป', STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), '0000', 1");
            }

            
        }

        private void bwLoadCustomer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _PROGRESS_STEP++;
            pgbStatus.Value = (int)(_PROGRESS_STEP * 1.0 / _PROGRESS_ALL * 1.0 * 100);
            Util.SetStatusMessage("กำลังโหลดข้อมูลการขายสินค้า", Param.StatusIcon.None);
            bwLoadSell.RunWorkerAsync();
        }

        private void bwLoadSell_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS 'SellHeader' (
                'SellNo' VARCHAR PRIMARY KEY NOT NULL,
                'Customer' VARCHAR DEFAULT '000000',
                'CustomerSex' VARCHAR,
                'CustomerAge' INT,
                'Credit' INT DEFAULT 0,
                'PayType' VARCHAR DEFAULT '0',
                'Cash' DOUBLE DEFAULT 0,
                'DiscountPercent' DOUBLE DEFAULT 0,
                'DiscountCash' DOUBLE DEFAULT 0,
                'Paid' BOOL DEFAULT 0,
                'Profit' DOUBLE DEFAULT 0,
                'TotalPrice' DOUBLE DEFAULT 0,
                'PointReceived' INT DEFAULT 0,
                'PointUse' INT DEFAULT 0,
                'Comment' VARCHAR,
                'SellDate' VARCHAR,
                'SellBy' VARCHAR,
                'Sync' BOOL DEFAULT 0)");

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var azureTable = Param.AzureTableClient.GetTableReference("SellHeader");
            azureTable.CreateIfNotExists();
            var query = new TableQuery<SellHeaderEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, Param.ShopId));
            const string command = @"INSERT OR REPLACE INTO SellHeader (SellNo, Customer, CustomerSex, CustomerAge, Credit, PayType, Cash, DiscountPercent, DiscountCash, 
                Paid, Profit, TotalPrice, PointReceived, PointUse, Comment, SellDate, SellBy) ";
            var sb = new StringBuilder(command);
            
            foreach (SellHeaderEntity d in azureTable.ExecuteQuery(query))
            {
                if (i != 0) sb.Append(" UNION ALL ");
                sb.Append(string.Format(@" SELECT '{0}', '{1}', '{2}', {3}, {4}, '{5}', {6}, {7}, {8},
                    {9}, {10}, {11}, {12}, {13}, '{14}', '{15}', '{16}'",
                    d.RowKey, d.Customer, d.CustomerSex, d.CustomerAge, d.Credit, d.PayType, d.Cash, d.DiscountPercent, d.DiscountCash,
                    d.Paid ? 1 : 0, d.Profit, d.TotalPrice, d.PointReceived, d.PointUse, d.Comment, d.SellDate.ToString("yyyy-MM-dd HH:mm:ss"), d.SellBy));
                i++;
                if (i % 500 == 0)
                {
                    i = 0;
                    Util.DBExecute(sb.ToString());
                    sb = new StringBuilder(command);
                }
            }
            Util.DBExecute(sb.ToString());


            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS 'SellDetail' (
                'SellNo' VARCHAR NOT NULL,
                'Product' VARCHAR NOT NULL,
                'SellPrice' DOUBLE NOT NULL,
                'Cost' DOUBLE DEFAULT 0,
                'Quantity' INT NOT NULL,
                'Comment' VARCHAR,
                'Sync' BOOL DEFAULT 0,
                PRIMARY KEY ('SellNo', 'Product'))");


            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var azureTableD = Param.AzureTableClient.GetTableReference("SellDetail");
            azureTableD.CreateIfNotExists();
            var queryD = new TableQuery<SellDetailEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, Param.ShopId));
            const string cmd = @"INSERT OR REPLACE INTO SellDetail (SellNo, Product, SellPrice, Cost, Quantity) ";
            var sbd = new StringBuilder(cmd);
            i = 0;
            foreach (SellDetailEntity s in azureTableD.ExecuteQuery(queryD))
            {
                string[] val = s.RowKey.Split('-');

                if (i != 0) sbd.Append(" UNION ALL ");
                sbd.Append(string.Format(@" SELECT '{0}', '{1}', '{2}', {3}, {4}",
                    val[0].ToString(), val[1].ToString(), s.SellPrice, s.Cost, s.Quantity));
                i++;
                if (i % 500 == 0)
                {
                    i = 0;
                    Util.DBExecute(sbd.ToString());
                    sbd = new StringBuilder(cmd);
                }
            }
            Util.DBExecute(sbd.ToString());

        }

        private void bwLoadSell_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _PROGRESS_STEP++;
            pgbStatus.Value = (int)(_PROGRESS_STEP * 1.0 / _PROGRESS_ALL * 1.0 * 100);
            Util.SetStatusMessage("กำลังโหลดข้อมูลการคืน", Param.StatusIcon.None);
            bwLoadReturnProduct.RunWorkerAsync();
        }

        private void bwLoadReturnProduct_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS 'ReturnProduct' (
                'SellNo' VARCHAR NOT NULL,
                'ReturnDate' VARCHAR,
                'Product' VARCHAR NOT NULL,
                'Barcode' VARCHAR NOT NULL,
                'SellPrice' DOUBLE NOT NULL,
                'Quantity' INT NOT NULL,
                'ReturnBy' VARCHAR,
                'Sync' BOOL DEFAULT 0,
                PRIMARY KEY ('SellNo', 'Barcode'))");


            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var azureTable = Param.AzureTableClient.GetTableReference("ReturnProduct");
            azureTable.CreateIfNotExists();
            var query = new TableQuery<ReturnEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, Param.ShopId));
            const string command = @"INSERT OR REPLACE INTO ReturnProduct ( SellNo,Barcode, ReturnDate, SellPrice, Product, ReturnBy, Quantity) ";
            var sb = new StringBuilder(command);

            foreach (ReturnEntity d in azureTable.ExecuteQuery(query))
            {
                string[] val = d.RowKey.Split('-');

                if (i != 0) sb.Append(" UNION ALL ");
                sb.Append(string.Format(@" SELECT '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'",
                val[0].ToString(), val[1].ToString(), d.ReturnDate.ToString("yyyy-MM-dd HH:mm:ss"), d.SellPrice, d.Product, d.ReturnBy, d.Quantity));
                i++;
                if (i % 500 == 0)
                {
                    i = 0;
                    Util.DBExecute(sb.ToString());
                    sb = new StringBuilder(command);
                }
            }
            Util.DBExecute(sb.ToString());
        }

        private void bwLoadReturnProduct_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _PROGRESS_STEP++;
            pgbStatus.Value = (int)(_PROGRESS_STEP * 1.0 / _PROGRESS_ALL * 1.0 * 100);
            Util.SetStatusMessage("กำลังโหลดข้อมูลเคลมสินค้า", Param.StatusIcon.None);
            bwLoadClaim.RunWorkerAsync();
        }

        private void bwLoadClaim_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS 'Claim' (
                'ClaimNo' VARCHAR NOT NULL,
                'ClaimType' VARCHAR NOT NULL,
                'ClaimDate' VARCHAR NOT NULL,
                'Product' VARCHAR NOT NULL,
                'Barcode' VARCHAR NOT NULL,
                'BarcodeClaim' VARCHAR ,
                'Description' VARCHAR NOT NULL,
                'Price' DOUBLE NOT NULL,
                'PriceClaim' DOUBLE ,
                'Firstname' VARCHAR,
                'Lastname' VARCHAR,
                'Nickname' VARCHAR,
                'Tel' VARCHAR,
                'Email' VARCHAR,
                'ClaimBy' VARCHAR,
                'Sync' BOOL DEFAULT 0,
                PRIMARY KEY ('ClaimNo', 'Barcode'))");


            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var azureTable = Param.AzureTableClient.GetTableReference("Claim");
            azureTable.CreateIfNotExists();
            var query = new TableQuery<ClaimEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, Param.ShopId));
            const string command = @"INSERT OR REPLACE INTO Claim ( ClaimNo, Barcode, ClaimType, BarcodeClaim, ClaimDate, Description, Price, PriceClaim, Product, Firstname, Lastname, Nickname, Tel, Email, ClaimBy) ";
            var sb = new StringBuilder(command);

            foreach (ClaimEntity d in azureTable.ExecuteQuery(query))
            {
                if (i != 0) sb.Append(" UNION ALL ");
                sb.Append(string.Format(@" SELECT '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}'",
                d.RowKey, d.Barcode, d.ClaimType, d.BarcodeClaim, d.ClaimDate.ToString("yyyy-MM-dd HH:mm:ss"), d.Description, d.Price, d.PriceClaim, d.Product, d.Firstname, d.Lastname, d.Nickname, d.Tel, d.Email, d.ClaimBy));
                i++;
                if (i % 500 == 0)
                {
                    i = 0;
                    Util.DBExecute(sb.ToString());
                    sb = new StringBuilder(command);
                }
            }
            Util.DBExecute(sb.ToString());
        }

        private void bwLoadClaim_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _PROGRESS_STEP++;
            pgbStatus.Value = (int)(_PROGRESS_STEP * 1.0 / _PROGRESS_ALL * 1.0 * 100);
            Util.SetStatusMessage("กำลังโหลดข้อมูลจังหวัด", Param.StatusIcon.None);
            bwLoadProvince.RunWorkerAsync();
        }

        private void bwLoadProvince_DoWork(object sender, DoWorkEventArgs e)
        {
            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS 'Province' (
                'ID' VARCHAR PRIMARY KEY NOT NULL,
                'Name' VARCHAR)");

            Util.DBExecute(@"CREATE  TABLE District (
                'Province' VARCHAR NOT NULL ,
                'ID' VARCHAR NOT NULL ,
                'Name' VARCHAR,
                'ZipCode' VARCHAR, 
                PRIMARY KEY ('Province', 'ID'))");

            DataTable dt = Util.DBQuery("SELECT COUNT(*) cnt FROM Province");
            if (dt.Rows[0]["cnt"].ToString() == "0")
            {
                var azureTable = Param.AzureTableClient.GetTableReference("Province");
                var query = new TableQuery<ProvinceEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "TH"));

                string command = @"INSERT OR REPLACE INTO Province (ID, Name) ";
                var sb = new StringBuilder(command);
                int i = 0;
                foreach (ProvinceEntity d in azureTable.ExecuteQuery(query))
                {
                    if (i != 0) sb.Append(" UNION ALL ");
                    sb.Append(string.Format(@" SELECT '{0}', '{1}'",
                        d.RowKey, d.Th));
                    i++;
                }
                Util.DBExecute(sb.ToString());

                query = new TableQuery<ProvinceEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.NotEqual, "TH"));
                command = @"INSERT OR REPLACE INTO District (Province, ID, Name, Zipcode) ";
                sb = new StringBuilder(command);
                i = 0;
                foreach (ProvinceEntity d in azureTable.ExecuteQuery(query))
                {
                    if (i != 0) sb.Append(" UNION ALL ");
                    sb.Append(string.Format(@" SELECT '{0}', '{1}', '{2}', '{3}'",
                        d.PartitionKey.Replace("TH", ""), d.RowKey, d.Th, d.Zipcode));
                    i++;
                    if (i % 500 == 0)
                    {
                        i = 0;
                        Util.DBExecute(sb.ToString());
                        sb = new StringBuilder(command);
                    }
                }
                Util.DBExecute(sb.ToString());
            }

        }

        private void bwLoadProvince_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _PROGRESS_STEP++;
            pgbStatus.Value = (int)(_PROGRESS_STEP * 1.0 / _PROGRESS_ALL * 1.0 * 100);
            Util.SetStatusMessage("โหลดข้อมูลเสร็จเรียบร้อยแล้ว", Param.StatusIcon.Success);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void bwLoadCategoryProfit_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void bwLoadCategoryProfit_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

       



    }
}
