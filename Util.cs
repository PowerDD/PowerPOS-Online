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
        public static void GetShopName()
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



    }
}
