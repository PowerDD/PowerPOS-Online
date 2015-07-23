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

namespace PowerPOS_Online
{
    public partial class FmInitialData : Form
    {
        const int _PROGRESS_ALL = 9;
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
                'Sync' BOOL DEFAULT 0)");

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            StringBuilder sb = new StringBuilder(@"INSERT OR REPLACE INTO Barcode (Barcode, OrderNo, Product, Cost, OperationCost, 
                    SellPrice, ReceivedDate, ReceivedBy, SellNo, SellDate, SellBy, SellFinished, Customer) ");
            var i = 0;
            foreach (BarcodeEntity d in azureTable.ExecuteQuery(query))
            {
                if (i != 0) sb.Append(" UNION ALL ");
                sb.Append(string.Format(@" SELECT '{0}', '{1}', '{2}', {3}, {4}, {5}, {6}, '{7}', '{8}', {9}, '{10}', {11}, '{12}'",
                    d.RowKey, d.OrderNo, d.Product, d.Cost, d.OperationCost == null ? 0 : d.OperationCost, d.SellPrice == null ? 0 : d.SellPrice,
                    d.ReceivedDate.ToString() == "1/1/0001 12:00:00 AM" ? "NULL" : "'" + d.ReceivedDate.ToString("yyyy-MM-dd HH:mm:ss") + "'", d.ReceivedBy, d.SellNo,
                    d.SellDate.ToString() == "1/1/0001 12:00:00 AM" ? "NULL" : "'" + d.SellDate.ToString("yyyy-MM-dd HH:mm:ss") + "'", d.SellBy, d.SellFinished ? 1 : 0, d.Customer));
                i++;
                if (i % 500 == 0)
                {
                    i = 0;
                    Util.DBExecute(sb.ToString());
                    sb = new StringBuilder(@"INSERT OR REPLACE INTO Barcode (Barcode, OrderNo, Product, Cost, OperationCost, 
                    SellPrice, ReceivedDate, ReceivedBy, SellNo, SellDate, SellBy, SellFinished, Customer) ");
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
            Util.DBExecute(string.Format(@"INSERT OR REPLACE INTO Product (Shop, ID, Name, CoverImage, Category, Brand, Price, Price1, Price2, Price3, Price4, Price5, Warranty,
                WebPrice, WebPrice1, WebPrice2, WebPrice3, WebPrice4, WebPrice5, WebWarranty, Cost)
                SELECT '{0}', p.ID, p.Name, p.CoverImage, p.Category, p.Brand, ps.Price, ps.Price1, ps.Price2, ps.Price3, ps.Price4, ps.Price5, ps.Warranty,
                p.Price, p.Price1, p.Price2, p.Price3, p.Price4, p.Price5, p.Warranty, ps.Cost
                FROM (SELECT DISTINCT Product FROM Barcode) b
                LEFT JOIN Product p
                ON b.Product = p.ID
                AND p.Shop = '{1}'
                LEFT JOIN Product ps
                ON b.Product = ps.ID
                AND ps.Shop = '{0}'
                ", Param.ShopId, Param.ShopParent));

            Util.DBExecute(string.Format(@"UPDATE Product SET Price = NULL, Price1 = NULL, Price2 = NULL, Price3 = NULL, Price4 = NULL, Price5 = NULL, Cost = NULL,
                WebPrice = NULL, WebPrice1 = NULL, WebPrice2 = NULL, WebPrice3 = NULL, WebPrice4 = NULL, WebPrice5 = NULL
                WHERE Shop = '{0}'", Param.ShopParent));

            if (Param.SystemConfig.SellPrice != null)
            {
                var json = Param.SystemConfig.SellPrice;
                Util.DBExecute(string.Format(@"UPDATE Product SET Price = {1}, Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price,0) <> {1}", Param.ShopId, json.Price));
                Util.DBExecute(string.Format(@"UPDATE Product SET Price1 = {1}, Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price1,0) <> {1}", Param.ShopId, json.Price1));
                Util.DBExecute(string.Format(@"UPDATE Product SET Price2 = {1}, Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price2,0) <> {1}", Param.ShopId, json.Price2));
                Util.DBExecute(string.Format(@"UPDATE Product SET Price3 = {1}, Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price3,0) <> {1}", Param.ShopId, json.Price3));
                Util.DBExecute(string.Format(@"UPDATE Product SET Price4 = {1}, Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price4,0) <> {1}", Param.ShopId, json.Price4));
                Util.DBExecute(string.Format(@"UPDATE Product SET Price5 = {1}, Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price5,0) <> {1}", Param.ShopId, json.Price5));
                Util.DBExecute(string.Format(@"UPDATE Product SET Warranty = IFNULL(WebWarranty,0), Sync = 1 WHERE Shop = '{0}' AND IFNULL(WebWarranty,0) <> IFNULL(Warranty,'')", Param.ShopId));
            }

            DataTable dt = Util.DBQuery(string.Format(@"SELECT ID, Name, CoverImage, Price, Price1, Price2, Price3, Price4, Price5, Warranty, IFNULL(Cost,0) Cost, Category, Brand 
                FROM Product
                WHERE Shop = '{0}'
                AND Sync = 1", Param.ShopId));
            Console.WriteLine("Update product total = {0} records", dt.Rows.Count);

            var azureTable = Param.AzureTableClient.GetTableReference("Product");
            TableBatchOperation batchOperation = new TableBatchOperation();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    ProductUpdateEntity data = new ProductUpdateEntity(Param.ShopId, dt.Rows[i]["ID"].ToString());
                    data.Name = dt.Rows[i]["Name"].ToString();
                    data.CoverImage = dt.Rows[i]["CoverImage"].ToString();
                    data.Price = double.Parse(dt.Rows[i]["Price"].ToString());
                    data.Price1 = double.Parse(dt.Rows[i]["Price1"].ToString());
                    data.Price2 = double.Parse(dt.Rows[i]["Price2"].ToString());
                    data.Price3 = double.Parse(dt.Rows[i]["Price3"].ToString());
                    data.Price4 = double.Parse(dt.Rows[i]["Price4"].ToString());
                    data.Price5 = double.Parse(dt.Rows[i]["Price5"].ToString());
                    data.Cost = double.Parse(dt.Rows[i]["Cost"].ToString());
                    data.Category = dt.Rows[i]["Category"].ToString();
                    data.Brand = dt.Rows[i]["Brand"].ToString();
                    data.Warranty = int.Parse(dt.Rows[i]["Warranty"].ToString());
                    batchOperation.InsertOrMerge(data);
                    if (batchOperation.Count == 100)
                    {
                        azureTable.ExecuteBatch(batchOperation);
                        batchOperation = new TableBatchOperation();
                    }
                }
                catch { }
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
            for (int i = 0; i < dt.Rows.Count; i++)
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
            for (int i = 0; i < dt.Rows.Count; i++)
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
                'Sync' BOOL DEFAULT 0,
                PRIMARY KEY ('Shop', 'ID'))
            ");

            var azureTable = Param.AzureTableClient.GetTableReference("Product");
            var query = new TableQuery<ProductEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, shop));
            const string command = @"INSERT OR REPLACE INTO Product (Shop, ID, Name, Price, Price1, Price2, Price3, Price4, Price5, Warranty, Cost, CoverImage, Category, Brand) ";
            var sb = new StringBuilder(command);
            int i = 0;
            foreach (ProductEntity d in azureTable.ExecuteQuery(query))
            {
                if (i != 0) sb.Append(" UNION ALL ");
                sb.Append(string.Format(@" SELECT '{0}', '{1}', '{2}', {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, '{11}', '{12}', '{13}'",
                    d.PartitionKey, d.RowKey, d.Name, d.Price, d.Price1, d.Price2, d.Price3, d.Price4, d.Price5, d.Warranty, d.Cost, d.CoverImage, d.Category, d.Brand));
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

            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS 'SellDetail' (
                'SellNo' VARCHAR NOT NULL,
                'Product' VARCHAR NOT NULL,
                'SellPrice' DOUBLE NOT NULL,
                'Cost' DOUBLE DEFAULT 0,
                'Quantity' INT NOT NULL,
                'Sync' BOOL DEFAULT 0,
                PRIMARY KEY ('SellNo', 'Product'))");

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var azureTable = Param.AzureTableClient.GetTableReference("SellHeader");
            azureTable.CreateIfNotExists();
            var query = new TableQuery<SellHeaderEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, Param.ShopId));
            const string command = @"INSERT OR REPLACE INTO SellHeader (SellNo, Customer, CustomerSex, CustomerAge, Credit, PayType, Cash, DiscountPercent, DiscountCash, 
                Paid, Profit, TotalPrice, PointReceived, PointUse, Comment, SellDate, SellBy) ";
            var sb = new StringBuilder(command);
            int i = 0;
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
        }

        private void bwLoadSell_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

    }
}
