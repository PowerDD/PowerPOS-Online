using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerPOS_Online
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Util.GetApiConfig();
            if (Param.apiChecked)
            {
                dynamic jsonObject = Util.DownloadAppConfig();
                if (!jsonObject.success)
                    Param.apiChecked = false;
            }

            while (!Param.apiChecked)
            {
                FmLicense fm = new FmLicense();
                var result = fm.ShowDialog(this);
                if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    break;
                }
            }

            if (!Param.apiChecked)
            {
                this.Dispose();
            }
            else
            {
                Param.azureStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=" + Param.databaseName + ";AccountKey=" + Param.databasePassword);
                Param.azureTableClient = Param.azureStorageAccount.CreateCloudTableClient();
                Util.GetShopName();
                this.Text = string.Format("Power POS - ร้าน {0} ({1})", Param.shopName, Param.computerName);
            }
            /*CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=powerddtest;AccountKey=6b+dBUr/E6uC1l+jVFT4ZgXgQSXGJwrqlF1UR3NFTcpyScMLZgCGV9C2612oZB+/DvVH0R1QrhUdooiLprgSxQ==");
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("people");
            table.CreateIfNotExists();*/
            //Console.WriteLine( Properties.Settings.Default.ApiUrl );
        }
    }
}
