using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
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
            Param.apiKey = Properties.Settings.Default.ApiKey;
            Param.apiUrl = Properties.Settings.Default.ApiUrl;
            Param.licenseKey = Properties.Settings.Default.LicenseKey;
            Param.apiChecked = Properties.Settings.Default.ApiChecked;
            Param.cpuId = GetCpuId();
            Param.computerName = System.Environment.MachineName;
            Param.databaseName = Properties.Settings.Default.DatabaseName;
            Param.databasePassword = Properties.Settings.Default.DatabasePassword;
            Param.shopId = Properties.Settings.Default.ShopId;
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
        public static dynamic DownloadAppConfig()
        {
            using (WebClient wc = new WebClient())
            {
                var values = new NameValueCollection();
                values["apiKey"] = Param.apiKey;
                values["licenseKey"] = Param.licenseKey;
                values["deviceId"] = Param.cpuId;
                values["deviceName"] = Param.computerName;

                try
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    var json = Encoding.UTF8.GetString(wc.UploadValues(Param.apiUrl + "/shop-application/info", values));

                    var structure = new { success = false };
                    dynamic jsonObject = new JsonSerializer().Deserialize(new StringReader(json), structure.GetType());
                    if (jsonObject.success)
                    {
                        var structureSuccess = new { success = false, shopId = "1234", databaseName = "", databasePassword = "" };
                        jsonObject = new JsonSerializer().Deserialize(new StringReader(json), structureSuccess.GetType());

                        Param.databaseName = jsonObject.databaseName;
                        Param.databasePassword = jsonObject.databasePassword;
                        Param.shopId = jsonObject.shopId;
                        Param.apiChecked = true;
                        Properties.Settings.Default.DatabaseName = Param.databaseName;
                        Properties.Settings.Default.DatabasePassword = Param.databasePassword;
                        Properties.Settings.Default.ShopId = Param.shopId;
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
        public static void GetShopInfo()
        {
            Param.azureTable = Param.azureTableClient.GetTableReference("Shop");
            TableQuery<ShopEntity> query = new TableQuery<ShopEntity>()
                .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, Param.shopId));

            foreach (ShopEntity entity in Param.azureTable.ExecuteQuery(query))
            {
                Param.shopName = entity.Name;
            }

            Console.WriteLine(Param.shopName);

        }

        public static void GetConfig()
        {
            Param.azureTable = Param.azureTableClient.GetTableReference("Shop");
            TableOperation retrieveOperation = TableOperation.Retrieve<ShopConfigEntity>(Param.shopId, "Config");
            TableResult retrievedResult = Param.azureTable.Execute(retrieveOperation);
            ShopConfigEntity data = (ShopConfigEntity)retrievedResult.Result;

            var json = new StringBuilder();
            if (data != null)
            {
                json.Append(data.Name);
            }
            Param.systemConfig = JsonConvert.DeserializeObject(json.ToString());
        }

        public static void GetBarcode()
        {
            /*Param.azureTable = Param.azureTableClient.GetTableReference("Barcode");
            TableQuery<BarcodeEntity> query = new TableQuery<BarcodeEntity>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, Param.shopId),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("SellNo", QueryComparisons.NotEqual, "7006")
            ));

            int i = 0;
            foreach (BarcodeEntity entity in Param.azureTable.ExecuteQuery(query))
            {
                Console.WriteLine(entity.RowKey);
                i++;
                if (i >= 1000) break;
                //Param.shopName = entity.RowKey;
            }*/
            Parallel.For(0, 1000, i =>
            {
                Console.WriteLine(i);
            });
        }

        public static void UpdateConfig()
        {
            Param.azureTable = Param.azureTableClient.GetTableReference("Shop");
            TableOperation retrieveOperation = TableOperation.Retrieve<ShopConfigEntity>(Param.shopId, "Config");
            TableResult retrievedResult = Param.azureTable.Execute(retrieveOperation);
            ShopConfigEntity data = (ShopConfigEntity)retrievedResult.Result;
            if (data != null)
            {
                data.Name = JsonConvert.SerializeObject(Param.systemConfig);
                TableOperation updateOperation = TableOperation.Merge(data);
                Param.azureTable.Execute(updateOperation);
            }
        }

        public static void ShowScreen(Param.Screen screen)
        {
            if (screen == Param.Screen.Sell && Param.selectedScreen != (int)Param.Screen.Sell)
            {
                Param.userControl = new UcSell();
            }
            else if (screen == Param.Screen.ReceiveProduct && Param.selectedScreen != (int)Param.Screen.ReceiveProduct)
            {
                Param.userControl = new UcReceiveProduct();
            }
            else if (screen == Param.Screen.Product && Param.selectedScreen != (int)Param.Screen.Product)
            {
                Param.userControl = new UcProduct();
            }
            else if (screen == Param.Screen.Category && Param.selectedScreen != (int)Param.Screen.Category)
            {
                Param.userControl = new UcCategory();
            }
            else if (screen == Param.Screen.Brand && Param.selectedScreen != (int)Param.Screen.Brand)
            {
                Param.userControl = new UcBrand();
            }
            else if (screen == Param.Screen.Customer && Param.selectedScreen != (int)Param.Screen.Customer)
            {
                Param.userControl = new UcCustomer();
            }
            else if (screen == Param.Screen.User && Param.selectedScreen != (int)Param.Screen.User)
            {
                Param.userControl = new UcUser();
            }
            else if (screen == Param.Screen.Color && Param.selectedScreen != (int)Param.Screen.Color)
            {
                Param.userControl = new UcColor();
            }
            else if (screen == Param.Screen.Report && Param.selectedScreen != (int)Param.Screen.Report)
            {
                Param.userControl = new UcReport();
            }
            else if (screen == Param.Screen.ShopInfo && Param.selectedScreen != (int)Param.Screen.ShopInfo)
            {
                Param.userControl = new UcShopInfo();
            }
            else if (screen == Param.Screen.Config && Param.selectedScreen != (int)Param.Screen.Config)
            {
                Param.userControl = new UcConfig();
            }
            Param.userControl.Dock = System.Windows.Forms.DockStyle.Fill;

            if (!Param.mainPanel.Contains(Param.userControl))
            {
                Param.mainPanel.Controls.Clear();
                Param.mainPanel.Controls.Add(Param.userControl);
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



    }
}
