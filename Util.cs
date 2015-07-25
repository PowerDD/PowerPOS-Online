using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XPTable.Models;
using XPTable.Renderers;

namespace PowerPOS_Online
{
    public class Util
    {

        public static void GetApiConfig()
        {
            Param.ApiUrl = Properties.Settings.Default.ApiUrl;
            Param.ApiKey = Properties.Settings.Default.ApiKey;
            Param.LicenseKey = Properties.Settings.Default.LicenseKey;
            Param.ApiChecked = Properties.Settings.Default.ApiChecked;
            Param.CpuId = GetCpuId();
            Param.ComputerName = System.Environment.MachineName;
            Param.DatabaseName = Properties.Settings.Default.DatabaseName;
            Param.DatabasePassword = Properties.Settings.Default.DatabasePassword;
            Param.ShopId = Properties.Settings.Default.ShopId;
            Param.ShopName = Properties.Settings.Default.ShopName;
            Param.ShopParent = Properties.Settings.Default.ShopParent;
            Param.ShopCustomer = Properties.Settings.Default.ShopCustomer;
            Param.LogoPath = Properties.Settings.Default.LogoPath;
            if (Param.LogoPath.ToString() == "")
            {
                if (!Directory.Exists("Resource/Images")) Directory.CreateDirectory("Resource/Images");
                if (File.Exists(Param.LogoPath)) File.Delete(Param.LogoPath);
                using (var client = new WebClient())
                {
                    client.DownloadFileAsync(new Uri(Param.LogoUrl), Param.LogoPath);
                    Param.LogoPath = "Resource/Images/logo.jpg";
                }
            }
        }

        public static string GetCpuId()
        {
            string cpuid = "";
            try
            {
                ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select ProcessorID From Win32_processor");
                ManagementObjectCollection mbsList = mbs.Get();

                foreach (ManagementObject mo in mbsList)
                {
                    cpuid = mo["ProcessorID"].ToString();
                }
                return cpuid;
            }
            catch (Exception) { return cpuid; }
        }

        public static void ConnectSQLiteDatabase()
        {
            if (!File.Exists(Param.SQLiteFileName))
            {
                SQLiteConnection.CreateFile(Param.SQLiteFileName);
            }
            Param.SQLiteConnection = new SQLiteConnection("Data Source=" + Param.SQLiteFileName + ";Version=3;New=True;Compress=True;");
            Param.SQLiteConnection.Open();
        }

        public static DataTable DBQuery(string sql)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            DataTable dt = new DataTable();
            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, Param.SQLiteConnection);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }
            return dt;
        }
        public static void DBExecute(string sql)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            try
            {
                SQLiteCommand command = new SQLiteCommand(sql, Param.SQLiteConnection);
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }
        }


        public static dynamic LoadAppConfig()
        {
            using (WebClient wc = new WebClient())
            {
                var values = new NameValueCollection();
                values["apiKey"] = Param.ApiKey;
                values["licenseKey"] = Param.LicenseKey;
                values["deviceId"] = Param.CpuId;
                values["deviceName"] = Param.ComputerName;

                try
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    var json = Encoding.UTF8.GetString(wc.UploadValues(Param.ApiUrl + "/shop-application/info", values));

                    var structure = new { success = false };
                    dynamic jsonObject = new JsonSerializer().Deserialize(new StringReader(json), structure.GetType());
                    if (jsonObject.success)
                    {
                        var structureSuccess = new { success = false, shopId = "1234", databaseName = "", databasePassword = "", devicePrefix = "", devicePrinter = "" };
                        jsonObject = new JsonSerializer().Deserialize(new StringReader(json), structureSuccess.GetType());

                        if (Param.ShopId != jsonObject.shopId)
                        {
                            Param.ShopName = "";
                            Param.ShopParent = "";
                            Param.ShopCustomer = "";
                        }

                        Param.DatabaseName = jsonObject.databaseName;
                        Param.DatabasePassword = jsonObject.databasePassword;
                        Param.ShopId = jsonObject.shopId;
                        Param.DevicePrefix = jsonObject.devicePrefix;
                        Param.DevicePrinter = jsonObject.devicePrinter;
                        Param.ApiChecked = true;
                        Properties.Settings.Default.DatabaseName = Param.DatabaseName;
                        Properties.Settings.Default.DatabasePassword = Param.DatabasePassword;
                        Properties.Settings.Default.ShopId = Param.ShopId;
                        Properties.Settings.Default.DevicePrefix = Param.DevicePrefix;
                        Properties.Settings.Default.DevicePrinter = Param.DevicePrinter;
                        Properties.Settings.Default.ApiChecked = true;
                        Properties.Settings.Default.Save();
                        Properties.Settings.Default.Upgrade();
                    }
                    else
                    {
                        var structureFail = new { success = false, error = "1234", errorMessage = "" };
                        jsonObject = new JsonSerializer().Deserialize(new StringReader(json), structureFail.GetType());
                    }
                    return jsonObject;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    return null;
                }
            }
        }
        public static void LoadShopInfo()
        {
            if (Util.CanConnectInternet() && (Param.ShopName == "" || Param.ShopParent == "" || Param.ShopCustomer == ""))
            {
                var azureTable = Param.AzureTableClient.GetTableReference("Shop");
                TableQuery<ShopEntity> query = new TableQuery<ShopEntity>()
                    .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, Param.ShopId));

                foreach (ShopEntity entity in azureTable.ExecuteQuery(query))
                {
                    Param.ShopName = entity.Name;
                    Param.ShopParent = entity.ShopParent;
                    Param.ShopCustomer = entity.ShopCustomer;

                    Properties.Settings.Default.ShopName = Param.ShopName;
                    Properties.Settings.Default.ShopParent = Param.ShopParent;
                    Properties.Settings.Default.ShopCustomer = Param.ShopCustomer;
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.Upgrade();

                    break;
                }
            }

            //Console.WriteLine(Param.shopName);

        }

        public static void LoadConfig()
        {
            if (Util.CanConnectInternet())
            {
                Param.AzureTable = Param.AzureTableClient.GetTableReference("Shop");
                TableOperation retrieveOperation = TableOperation.Retrieve<ShopConfigEntity>(Param.ShopId, "Config");
                TableResult retrievedResult = Param.AzureTable.Execute(retrieveOperation);
                ShopConfigEntity data = (ShopConfigEntity)retrievedResult.Result;

                var json = new StringBuilder();
                if (data != null)
                {
                    json.Append(data.Value);
                }
                Param.SystemConfig = JsonConvert.DeserializeObject(json.ToString());
            }
        }

        public static void UpdateConfig()
        {
            Param.AzureTable = Param.AzureTableClient.GetTableReference("Shop");
            TableOperation retrieveOperation = TableOperation.Retrieve<ShopConfigEntity>(Param.ShopId, "Config");
            TableResult retrievedResult = Param.AzureTable.Execute(retrieveOperation);
            ShopConfigEntity data = (ShopConfigEntity)retrievedResult.Result;
            if (data != null)
            {
                data.Value = JsonConvert.SerializeObject(Param.SystemConfig);
                TableOperation updateOperation = TableOperation.Merge(data);
                Param.AzureTable.Execute(updateOperation);
            }
        }
        public static void GetUserGroup()
        {
            Param.AzureTable = Param.AzureTableClient.GetTableReference("User");
            TableOperation retrieveOperation = TableOperation.Retrieve<UserGroupEntity>(Param.ShopId, "Group");
            TableResult retrievedResult = Param.AzureTable.Execute(retrieveOperation);
            UserGroupEntity data = (UserGroupEntity)retrievedResult.Result;
            var json = new StringBuilder();
            if (data != null)
            {
                json.Append(data.Data);
            }
            Param.UserGroup = JsonConvert.DeserializeObject(json.ToString());

        }

        public static void UpdateUserGroup()
        {
            UserGroupEntity data = new UserGroupEntity(Param.ShopId);
            data.Data = JsonConvert.SerializeObject(Param.UserGroup);
            Param.AzureTable = Param.AzureTableClient.GetTableReference("User");
            TableOperation updateOperation = TableOperation.InsertOrMerge(data);
            Param.AzureTable.Execute(updateOperation);
        }

        public static void AddUser(UserEntity data)
        {
            Param.AzureTable = Param.AzureTableClient.GetTableReference("User");
            TableOperation updateOperation = TableOperation.InsertOrMerge(data);
            Param.AzureTable.Execute(updateOperation);
        }

        public static void ShowScreen(Param.Screen screen)
        {
            if (screen == Param.Screen.Sell && Param.SelectedScreen != (int)Param.Screen.Sell)
            {
                Param.UserControl = new UcSell();
            }
            else if (screen == Param.Screen.ReceiveProduct && Param.SelectedScreen != (int)Param.Screen.ReceiveProduct)
            {
                Param.UserControl = new UcReceiveProduct();
            }
            else if (screen == Param.Screen.Product && Param.SelectedScreen != (int)Param.Screen.Product)
            {
                Param.UserControl = new UcProduct();
            }
            else if (screen == Param.Screen.Category && Param.SelectedScreen != (int)Param.Screen.Category)
            {
                Param.UserControl = new UcCategory();
            }
            else if (screen == Param.Screen.Brand && Param.SelectedScreen != (int)Param.Screen.Brand)
            {
                Param.UserControl = new UcBrand();
            }
            else if (screen == Param.Screen.Customer && Param.SelectedScreen != (int)Param.Screen.Customer)
            {
                Param.UserControl = new UcCustomer();
            }
            else if (screen == Param.Screen.User && Param.SelectedScreen != (int)Param.Screen.User)
            {
                Param.UserControl = new UcUser();
            }
            else if (screen == Param.Screen.Color && Param.SelectedScreen != (int)Param.Screen.Color)
            {
                Param.UserControl = new UcColor();
            }
            else if (screen == Param.Screen.Report && Param.SelectedScreen != (int)Param.Screen.Report)
            {
                Param.UserControl = new UcReport();
            }
            else if (screen == Param.Screen.ShopInfo && Param.SelectedScreen != (int)Param.Screen.ShopInfo)
            {
                Param.UserControl = new UcShopInfo();
            }
            else if (screen == Param.Screen.Config && Param.SelectedScreen != (int)Param.Screen.Config)
            {
                Param.UserControl = new UcConfig();
            }
            else if (screen == Param.Screen.Claim && Param.SelectedScreen != (int)Param.Screen.Claim)
            {
                Param.UserControl = new UcClaim();
            }
            Param.UserControl.Dock = System.Windows.Forms.DockStyle.Fill;

            if (!Param.MainPanel.Contains(Param.UserControl))
            {
                Param.MainPanel.Controls.Clear();
                Param.MainPanel.Controls.Add(Param.UserControl);
            }
        }
        public static void InitialTable(XPTable.Models.Table table)
        {

            table.HeaderRenderer = new GradientHeaderRenderer();
            table.HeaderRenderer.Alignment = ColumnAlignment.Center;
            table.HeaderRenderer.Font = new System.Drawing.Font("MS Sans Serif", 10F);
            table.Font = new System.Drawing.Font("MS Sans Serif", 10F);
            table.ColumnModel.HeaderHeight = 26;

        }

        public static InputLanguage GetInputLanguageByName(string inputName)
        {
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                if (lang.Culture.EnglishName.ToLower().StartsWith(inputName.ToLower()))
                    return lang;
            }
            return null;
        }


        public static void SetKeyboardLayout(InputLanguage layout)
        {
            InputLanguage.CurrentInputLanguage = layout;
        }

        public static void SetStatusMessage(string message, Param.StatusIcon icon)
        {
            Param.lblStatus.Text = message;
            switch (icon)
            {
                case Param.StatusIcon.Loading:
                    Param.lblStatus.Image = global::PowerPOS_Online.Properties.Resources.loading;
                    break;
                case Param.StatusIcon.Success:
                    Param.lblStatus.Image = global::PowerPOS_Online.Properties.Resources.accept;
                    break;
                /*case Param.StatusIcon.Info:
                    Param.lblStatus.Image = global::PowerPOS_Online.Properties.Resources.accept;
                    break;*/
                default:
                    Param.lblStatus.Image = null;
                    break;
            }
        }

        public static string MD5String(string message)
        {
            byte[] encodedPassword = new UTF8Encoding().GetBytes(message);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
        }
        public static bool CanConnectInternet()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead(Param.ApiUrl))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static void WriteErrorLog(string message)
        {
            string filename = "error-" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            StreamWriter sw = new StreamWriter(filename, true);
            sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t" + message);
            sw.Close();
        }

        public static BarcodeEntity RenderBarcodeData(DataRow dr)
        {
            BarcodeEntity data = new BarcodeEntity(Param.ShopId, dr["Barcode"].ToString());
            data.OrderNo = dr["OrderNo"].ToString();
            data.Product = dr["Product"].ToString();
            data.Cost = double.Parse(dr["Cost"].ToString());
            data.OperationCost = double.Parse(dr["OperationCost"].ToString());
            data.SellPrice = double.Parse(dr["SellPrice"].ToString());
            data.ReceivedDate = Convert.ToDateTime(dr["ReceivedDate"].ToString());
            data.ReceivedBy = dr["ReceivedBy"].ToString();
            data.SellNo = dr["SellNo"].ToString();
            data.SellDate = Convert.ToDateTime(dr["SellDate"].ToString());
            data.SellBy = dr["SellBy"].ToString();
            data.SellFinished = dr["SellFinished"].ToString() == "True" ? true : false;
            data.Customer = dr["Customer"].ToString();
            data.ETag = "*";
            return data;
        }

        public static BarcodeNoDateEntity RenderBarcodeNoDateData(DataRow dr)
        {
            BarcodeNoDateEntity data = new BarcodeNoDateEntity(Param.ShopId, dr["Barcode"].ToString());
            data.OrderNo = dr["OrderNo"].ToString();
            data.Product = dr["Product"].ToString();
            data.Cost = double.Parse(dr["Cost"].ToString());
            data.OperationCost = double.Parse(dr["OperationCost"].ToString());
            data.SellPrice = double.Parse(dr["SellPrice"].ToString());
            data.ReceivedBy = dr["ReceivedBy"].ToString();
            data.SellNo = dr["SellNo"].ToString();
            data.SellBy = dr["SellBy"].ToString();
            data.SellFinished = dr["SellFinished"].ToString() == "True" ? true : false;
            data.Customer = dr["Customer"].ToString();
            data.ETag = "*";
            return data;
        }

        public static BarcodeNoReceivedDateEntity RenderBarcodeNoReceivedDateData(DataRow dr)
        {
            BarcodeNoReceivedDateEntity data = new BarcodeNoReceivedDateEntity(Param.ShopId, dr["Barcode"].ToString());
            data.OrderNo = dr["OrderNo"].ToString();
            data.Product = dr["Product"].ToString();
            data.Cost = double.Parse(dr["Cost"].ToString());
            data.OperationCost = double.Parse(dr["OperationCost"].ToString());
            data.SellPrice = double.Parse(dr["SellPrice"].ToString());
            data.ReceivedBy = dr["ReceivedBy"].ToString();
            data.SellNo = dr["SellNo"].ToString();
            data.SellDate = Convert.ToDateTime(dr["SellDate"].ToString());
            data.SellBy = dr["SellBy"].ToString();
            data.SellFinished = dr["SellFinished"].ToString() == "True" ? true : false;
            data.Customer = dr["Customer"].ToString();
            data.ETag = "*";
            return data;
        }

        public static BarcodeNoSellDateEntity RenderBarcodeNoSellDateData(DataRow dr)
        {
            BarcodeNoSellDateEntity data = new BarcodeNoSellDateEntity(Param.ShopId, dr["Barcode"].ToString());
            data.OrderNo = dr["OrderNo"].ToString();
            data.Product = dr["Product"].ToString();
            data.Cost = double.Parse(dr["Cost"].ToString());
            data.OperationCost = double.Parse(dr["OperationCost"].ToString());
            data.SellPrice = double.Parse(dr["SellPrice"].ToString());
            data.ReceivedDate = Convert.ToDateTime(dr["ReceivedDate"].ToString());
            data.ReceivedBy = dr["ReceivedBy"].ToString();
            data.SellNo = dr["SellNo"].ToString();
            data.SellBy = dr["SellBy"].ToString();
            data.SellFinished = dr["SellFinished"].ToString() == "True" ? true : false;
            data.Customer = dr["Customer"].ToString();
            data.ETag = "*";
            return data;
        }

        public static void SyncData()
        {
            DataTable dt;
            //## Product ##//
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                dt = Util.DBQuery("SELECT * FROM Barcode WHERE Sync = 1");

                var azureTable = Param.AzureTableClient.GetTableReference("BarcodeStock");
                TableBatchOperation batchOperation = new TableBatchOperation();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    if (row["ReceivedDate"].ToString() == "" && row["SellDate"].ToString() == "")
                        batchOperation.InsertOrMerge(Util.RenderBarcodeNoDateData(row));
                    else if (row["SellDate"].ToString() == "")
                        batchOperation.InsertOrMerge(Util.RenderBarcodeNoSellDateData(row));
                    else if (row["ReceivedDate"].ToString() == "")
                        batchOperation.InsertOrMerge(Util.RenderBarcodeNoReceivedDateData(row));
                    else
                        batchOperation.InsertOrMerge(Util.RenderBarcodeData(row));

                    Util.DBExecute(string.Format("UPDATE Barcode SET Sync = 0 WHERE Barcode = {0}", row["Barcode"].ToString()));

                    if (batchOperation.Count == 100)
                    {
                        azureTable.ExecuteBatch(batchOperation);
                        batchOperation = new TableBatchOperation();
                    }
                }
                if (batchOperation.Count > 0)
                    azureTable.ExecuteBatch(batchOperation);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }

            //## Customer ##//
            try
            {
                dt = Util.DBQuery("SELECT * FROM Customer WHERE Sync = 1");

                var azureTable = Param.AzureTableClient.GetTableReference("Customer");
                TableBatchOperation batchOperation = new TableBatchOperation();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    dynamic d = new DynamicEntity(Param.ShopId, row["ID"].ToString());
                    //CustomerEntity d = new CustomerEntity(Param.ShopId, row["ID"].ToString());
                    d.Member = row["Member"].ToString();
                    d.Name = row["Firstname"].ToString() + " " + row["Lastname"].ToString();
                    d.Firstname = row["Firstname"].ToString();
                    d.Lastname = row["Lastname"].ToString();
                    d.Nickname = row["Nickname"].ToString();
                    d.CitizenID = row["CitizenID"].ToString();
                    try { d.Birthday = Convert.ToDateTime(row["Birthday"].ToString()); } catch { }
                    d.Sex = row["Sex"].ToString();
                    d.CardNo = row["CardNo"].ToString();
                    d.Mobile = row["Mobile"].ToString();
                    d.Email = row["Email"].ToString();
                    d.ShopName = row["ShopName"].ToString();
                    d.Address = row["Address"].ToString();
                    d.Address2 = row["Address2"].ToString();
                    d.SubDistrict = row["SubDistrict"].ToString();
                    d.District = row["District"].ToString();
                    d.Province = row["Province"].ToString();
                    d.ZipCode = row["ZipCode"].ToString();
                    d.ShopSameAddress = row["ShopSameAddress"].ToString() == "True";
                    d.ShopAddress = row["ShopAddress"].ToString();
                    d.ShopAddress2 = row["ShopAddress2"].ToString();
                    d.ShopSubDistrict = row["ShopSubDistrict"].ToString();
                    d.ShopDistrict = row["ShopDistrict"].ToString();
                    d.ShopProvince = row["ShopProvince"].ToString();
                    d.ShopZipCode = row["ShopZipCode"].ToString();
                    d.SellPrice = int.Parse(row["SellPrice"].ToString());
                    d.DiscountPercent = int.Parse(row["DiscountPercent"].ToString());
                    d.Credit = int.Parse(row["Credit"].ToString());
                    d.Comment = row["Comment"].ToString();
                    try { d.AddDate = Convert.ToDateTime(row["AddDate"].ToString()); } catch { }
                    d.AddBy = row["AddBy"].ToString();
                    try { d.UpdateDate = Convert.ToDateTime(row["UpdateDate"].ToString()); } catch { }
                    d.UpdateBy = row["UpdateBy"].ToString();
                    
                    batchOperation.InsertOrMerge(d);

                    Util.DBExecute(string.Format("UPDATE Customer SET Sync = 0 WHERE ID = '{0}'", row["ID"].ToString()));

                    if (batchOperation.Count == 100)
                    {
                        azureTable.ExecuteBatch(batchOperation);
                        batchOperation = new TableBatchOperation();
                    }
                }
                if (batchOperation.Count > 0)
                    azureTable.ExecuteBatch(batchOperation);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }

            //## SellHeader ##//
            try
            {
                dt = Util.DBQuery("SELECT * FROM SellHeader WHERE Sync = 1");

                var azureTable = Param.AzureTableClient.GetTableReference("SellHeader");
                azureTable.CreateIfNotExists();

                TableBatchOperation batchOperation = new TableBatchOperation();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    dynamic d = new DynamicEntity(Param.ShopId, row["SellNo"].ToString());
                    //SellHeaderEntity d = new SellHeaderEntity(Param.ShopId, row["SellNo"].ToString());
                    d.Customer = row["Customer"].ToString();
                    d.CustomerSex = row["CustomerSex"].ToString();
                    d.CustomerAge = int.Parse(row["CustomerAge"].ToString());
                    d.Credit = int.Parse(row["Credit"].ToString());
                    d.PayType = row["PayType"].ToString();
                    d.Cash = double.Parse(row["Cash"].ToString());
                    d.DiscountPercent = double.Parse(row["DiscountPercent"].ToString());
                    d.DiscountCash = double.Parse(row["DiscountCash"].ToString());
                    d.Paid = row["Paid"].ToString() == "True";
                    d.Profit = double.Parse(row["Profit"].ToString());
                    d.TotalPrice = double.Parse(row["TotalPrice"].ToString());
                    d.PointReceived = int.Parse(row["PointReceived"].ToString());
                    d.PointUse = int.Parse(row["PointUse"].ToString());
                    d.Comment = row["Comment"].ToString();
                    d.SellDate = Convert.ToDateTime(row["SellDate"].ToString());
                    d.SellBy = row["SellBy"].ToString();
                    batchOperation.InsertOrMerge(d);

                    Util.DBExecute(string.Format("UPDATE SellHeader SET Sync = 0 WHERE SellNo = '{0}'", row["SellNo"].ToString()));

                    if (batchOperation.Count == 100)
                    {
                        azureTable.ExecuteBatch(batchOperation);
                        batchOperation = new TableBatchOperation();
                    }
                }
                if (batchOperation.Count > 0)
                    azureTable.ExecuteBatch(batchOperation);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }

            //## SellDetail ##//
            try
            {
                dt = Util.DBQuery("SELECT * FROM SellDetail WHERE Sync = 1");

                var azureTable = Param.AzureTableClient.GetTableReference("SellDetail");
                azureTable.CreateIfNotExists();

                TableBatchOperation batchOperation = new TableBatchOperation();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    dynamic d = new DynamicEntity(Param.ShopId, row["SellNo"].ToString() + "-" + row["Product"].ToString());
                    d.SellPrice = double.Parse(row["SellPrice"].ToString());
                    d.Cost = double.Parse(row["Cost"].ToString());
                    d.Quantity = int.Parse(row["Quantity"].ToString());
                    batchOperation.InsertOrMerge(d);

                    Util.DBExecute(string.Format("UPDATE SellDetail SET Sync = 0 WHERE SellNo = '{0}' AND Product = '{1}'", 
                        row["SellNo"].ToString(), row["Product"].ToString()));

                    if (batchOperation.Count == 100)
                    {
                        azureTable.ExecuteBatch(batchOperation);
                        batchOperation = new TableBatchOperation();
                    }
                }
                if (batchOperation.Count > 0)
                    azureTable.ExecuteBatch(batchOperation);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }
        }




        public static void PrintReceipt(string sellNo)
        {
            DataTable dt = Util.DBQuery(string.Format(@"SELECT COUNT(*) cnt FROM Barcode WHERE SellNo = '{0}'", sellNo));

            var hight = 195 + int.Parse(dt.Rows[0]["cnt"].ToString()) * 13;
            //PaperSize paperSize = new PaperSize("Custom Size", 280, hight);
            //PaperSize paperSize = new PaperSize("Custom Size", 380, hight);
            PaperSize paperSize = new PaperSize("Custom Size", 400, hight);
            paperSize.RawKind = (int)PaperKind.Custom;

            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.PaperSize = paperSize;
            pd.PrintController = new System.Drawing.Printing.StandardPrintController();
            pd.PrinterSettings.PrinterName = Param.DevicePrinter;
            //pd.PrinterSettings.PrinterName = "GP-80250 Series";
            //pd.PrinterSettings.PrinterName = "POS80";

            pd.PrintPage += (_, g) =>
            {
                PrintReceipt(g, sellNo);
            };
            pd.Print();

        }


        private static void PrintReceipt(PrintPageEventArgs g, string sellNo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");


            DataTable dtHeader = Util.DBQuery(string.Format(@"SELECT h.TotalPrice Price, IFNULL(h.Cash,0) Cash, c.Firstname, c.Lastname, c.Mobile,
                    datetime(h.SellDate, 'localtime') SellDate, h.SellBy
                    FROM SellHeader h
                        LEFT JOIN Customer c
                        ON h.Customer = c.ID
                    WHERE h.SellNo = '{0}'"
                , sellNo));

            var width = 280;
            var gab = 5;

            if (Param.SystemConfig.Bill.PrintLogo == "Y")
            {
                if (!File.Exists(Param.LogoPath))
                {
                    if (!Directory.Exists("Resource/Images")) Directory.CreateDirectory("Resource/Images");
                    if (File.Exists(Param.LogoPath)) File.Delete(Param.LogoPath);
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(new Uri(Param.LogoUrl), Param.LogoPath);
                        Param.SystemConfig.Bill.Logo = Param.LogoUrl;
                    }
                }
                Image image = Image.FromFile(Param.LogoPath);
                Rectangle destRect = new Rectangle(0, 0, width, 64);
                //Rectangle destRect = new Rectangle(0, 0, width, image.Height * width / image.Width);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
            }


            SolidBrush brush = new SolidBrush(Color.Black);
            Font stringFont = new Font("Calibri", 6);
            if (Param.SystemConfig.Bill.Logo == Param.LogoUrl && Param.SystemConfig.Bill.PrintLogo == "Y")
            {
                g.Graphics.DrawString("http:// www.", stringFont, brush, new PointF(62, 49));
                g.Graphics.DrawString(".co.th", stringFont, brush, new PointF(193, 49));
                stringFont = new Font("Calibri", 6.5f, FontStyle.Bold);
                g.Graphics.DrawString("R e m a x T h a i l a n d", stringFont, brush, new PointF(109, 48.3f));
            }

            var pX = 0;
            var pY = 65;
            stringFont = new Font("Calibri", 7);
            g.Graphics.DrawString(DateTime.Parse(dtHeader.Rows[0]["SellDate"].ToString()).ToString("dd/MM/yyyy HH:mm") + " : " + dtHeader.Rows[0]["SellBy"].ToString(), stringFont, brush, new PointF(pX, pY + 6));

            stringFont = new Font("DilleniaUPC", 13);
            g.Graphics.DrawString("เลขที่ ", stringFont, brush, new PointF(pX + 188, pY));

            stringFont = new Font("Calibri", 10, FontStyle.Bold);
            string measureString = sellNo;
            SizeF stringSize = g.Graphics.MeasureString(measureString, stringFont);
            g.Graphics.DrawString(sellNo, stringFont, brush, new PointF(width - stringSize.Width + gab, pY + 3));
            pY += 20;

            stringFont = new Font("DilleniaUPC", 17, FontStyle.Bold);
            measureString = Param.SystemConfig.Bill.HeaderName; // "ใบเสร็จรับเงิน";
            stringSize = g.Graphics.MeasureString(measureString, stringFont);
            g.Graphics.DrawString(measureString, stringFont, brush, new PointF((width - stringSize.Width + gab) / 2, pY + 5));
            pY += 30;

            stringFont = new Font("DilleniaUPC", 9);
            DataTable dt = Util.DBQuery(string.Format(@"SELECT Name, ProductCount, SellPrice
                    FROM (SELECT product, SUM(SellPrice) SellPrice, COUNT(*) ProductCount FROM Barcode WHERE SellNo = '{0}' GROUP BY product) b 
                        LEFT JOIN Product p 
                        ON b.Product = p.ID
                        AND p.Shop = '{1}'
                ", sellNo, Param.ShopParent));
            var sumQty = 0;
            var sumPrice = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                g.Graphics.DrawString(int.Parse(dt.Rows[i]["ProductCount"].ToString()).ToString("#,##0"), stringFont, brush, new PointF(pX, pY));
                g.Graphics.DrawString(dt.Rows[i]["Name"].ToString(), stringFont, brush, new PointF(pX + 16, pY));

                g.Graphics.FillRectangle(new SolidBrush(Color.White), pX + 230, pY + 3, 150, 10);
                g.Graphics.DrawString("@" + (int.Parse(dt.Rows[i]["SellPrice"].ToString()) / int.Parse(dt.Rows[i]["ProductCount"].ToString())).ToString("#,##0"),
                    stringFont, brush, new PointF(pX + 232, pY));
                measureString = int.Parse(dt.Rows[i]["SellPrice"].ToString()).ToString("#,##0");
                stringSize = g.Graphics.MeasureString(measureString, stringFont);
                g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                sumQty += int.Parse(dt.Rows[i]["ProductCount"].ToString());
                sumPrice += int.Parse(dt.Rows[i]["SellPrice"].ToString());
                pY += 13;
            }

            pY += 4;
            stringFont = new Font("DilleniaUPC", 12, FontStyle.Bold);
            g.Graphics.DrawString(string.Format("รวม {0} รายการ ({1} ชิ้น)", dt.Rows.Count, sumQty), stringFont, brush, new PointF(pX, pY));
            measureString = "" + sumPrice.ToString("#,##0");
            stringSize = g.Graphics.MeasureString(measureString, stringFont);
            g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
            pY += 17;
            stringFont = new Font("DilleniaUPC", 11);
            g.Graphics.DrawString("เงินสด  " + int.Parse(dtHeader.Rows[0]["Cash"].ToString()).ToString("#,##0"), stringFont, brush, new PointF(pX, pY));
            measureString = "เงินทอน  " + (int.Parse(dtHeader.Rows[0]["Cash"].ToString()) - sumPrice).ToString("#,##0");
            stringSize = g.Graphics.MeasureString(measureString, stringFont);
            g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
            pY += 23;

            g.Graphics.DrawLine(new Pen(Color.Black, 0.25f), pX, pY, pX + width, pY);
            pY += 5;

            stringFont = new Font("DilleniaUPC", 9);
            g.Graphics.DrawString("ชื่อลูกค้า " + dtHeader.Rows[0]["Firstname"].ToString() + " " + dtHeader.Rows[0]["Lastname"].ToString() +
                ((dtHeader.Rows[0]["Mobile"].ToString() != "") ?
                " (" + dtHeader.Rows[0]["Mobile"].ToString().Substring(0, 3) + "-" + dtHeader.Rows[0]["Mobile"].ToString().Substring(3, 4) + "-" + dtHeader.Rows[0]["Mobile"].ToString().Substring(7) + ")"
                : "")
                , stringFont, brush, new PointF(pX, pY));

            /*stringFont = new Font("DilleniaUPC", 11);
            measureString = "แต้มสะสม  " + (34534).ToString("#,##0");
            stringSize = g.Graphics.MeasureString(measureString, stringFont);
            g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY - 2));*/
            pY += 17;

            stringFont = new Font("Calibri", 8, FontStyle.Bold);
            measureString = Param.SystemConfig.Bill.FooterText; // "LINE Official ID : @RemaxThailand";
            stringSize = g.Graphics.MeasureString(measureString, stringFont);
            g.Graphics.DrawString(measureString, stringFont, brush, new PointF((width - stringSize.Width + gab) / 2, pY));

        }


    }
}
