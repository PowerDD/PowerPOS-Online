using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Security.Cryptography;
using System.Text;
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
                        var structureSuccess = new { success = false, shopId = "1234", databaseName = "", databasePassword = "" };
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
                        Param.ApiChecked = true;
                        Properties.Settings.Default.DatabaseName = Param.DatabaseName;
                        Properties.Settings.Default.DatabasePassword = Param.DatabasePassword;
                        Properties.Settings.Default.ShopId = Param.ShopId;
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
                default :
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
            data.ReceivedDate = DateTime.Parse(dr["ReceivedDate"].ToString());
            data.ReceivedBy = dr["ReceivedBy"].ToString();
            data.SellNo = dr["SellNo"].ToString();
            data.SellDate = DateTime.Parse(dr["SellDate"].ToString());
            data.SellBy = dr["SellBy"].ToString();
            data.SellFinished = dr["SellFinished"].ToString() == "1" ? true : false;
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
            data.SellFinished = dr["SellFinished"].ToString() == "1" ? true : false;
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
            data.SellDate = DateTime.Parse(dr["SellDate"].ToString());
            data.SellBy = dr["SellBy"].ToString();
            data.SellFinished = dr["SellFinished"].ToString() == "1" ? true : false;
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
            data.ReceivedDate = DateTime.Parse(dr["ReceivedDate"].ToString());
            data.ReceivedBy = dr["ReceivedBy"].ToString();
            data.SellNo = dr["SellNo"].ToString();
            data.SellBy = dr["SellBy"].ToString();
            data.SellFinished = dr["SellFinished"].ToString() == "1" ? true : false;
            data.Customer = dr["Customer"].ToString();
            data.ETag = "*";
            return data;
        }

        public static void SyncData()
        {
            //## Product ##//
            DataTable dt = Util.DBQuery("SELECT * FROM Barcode WHERE Sync = 1");

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



    }
}
