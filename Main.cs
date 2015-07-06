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
                lblStatus.Text = "";
                menuStrip1.Enabled = true;
                toolStrip1.Enabled = true;
            }
            /*CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=powerddtest;AccountKey=6b+dBUr/E6uC1l+jVFT4ZgXgQSXGJwrqlF1UR3NFTcpyScMLZgCGV9C2612oZB+/DvVH0R1QrhUdooiLprgSxQ==");
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("people");
            table.CreateIfNotExists();*/
            //Console.WriteLine( Properties.Settings.Default.ApiUrl );
        }

        private void mniLogin_Click(object sender, EventArgs e)
        {

        }

        private void mniLogout_Click(object sender, EventArgs e)
        {

        }

        private void mniExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void mniProduct_Click(object sender, EventArgs e)
        {

        }

        private void mniCategory_Click(object sender, EventArgs e)
        {

        }

        private void mniBrand_Click(object sender, EventArgs e)
        {

        }

        private void mniColor_Click(object sender, EventArgs e)
        {

        }

        private void mniCustomer_Click(object sender, EventArgs e)
        {

        }

        private void mniUser_Click(object sender, EventArgs e)
        {

        }

        private void mniShop_Click(object sender, EventArgs e)
        {

        }

        private void mniReceive_Click(object sender, EventArgs e)
        {

        }

        private void mniSell_Click(object sender, EventArgs e)
        {

        }

        private void mniReportSell_Click(object sender, EventArgs e)
        {

        }

        private void mniRegister_Click(object sender, EventArgs e)
        {
            Param.apiChecked = false;
            while (!Param.apiChecked)
            {
                FmLicense fm = new FmLicense();
                var result = fm.ShowDialog(this);
                if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    break;
                }
            }
        }

        private void mniAbout_Click(object sender, EventArgs e)
        {
            new FmAbout().ShowDialog(this);
        }

        private void mniImportData_Click(object sender, EventArgs e)
        {

        }

        private void mniImportBarcode_Click(object sender, EventArgs e)
        {

        }

        private void mniImportExcel_Click(object sender, EventArgs e)
        {

        }
    }
}
