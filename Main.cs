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
            Param.mainPanel = pnlMain;
            Param.lblStatus = lblStatus;
            Util.SetStatusMessage("กำลังโหลดข้อมูลการตั้งค่าระบบ");

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
                Util.SetStatusMessage("กำลังโหลดข้อมูลรายละเอียดของร้านค้า");
                bwGetShopInfo.RunWorkerAsync();
            }

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
            Util.ShowScreen(Param.Screen.Product);
            Param.selectedScreen = (int)Param.Screen.Product;
        }

        private void mniCategory_Click(object sender, EventArgs e)
        {
            Util.ShowScreen(Param.Screen.Category);
            Param.selectedScreen = (int)Param.Screen.Category;
        }

        private void mniBrand_Click(object sender, EventArgs e)
        {
            Util.ShowScreen(Param.Screen.Brand);
            Param.selectedScreen = (int)Param.Screen.Brand;
        }

        private void mniColor_Click(object sender, EventArgs e)
        {
            Util.ShowScreen(Param.Screen.Color);
            Param.selectedScreen = (int)Param.Screen.Color;
        }

        private void mniCustomer_Click(object sender, EventArgs e)
        {
            Util.ShowScreen(Param.Screen.Customer);
            Param.selectedScreen = (int)Param.Screen.Customer;
        }

        private void mniUser_Click(object sender, EventArgs e)
        {
            Util.ShowScreen(Param.Screen.User);
            Param.selectedScreen = (int)Param.Screen.User;
        }

        private void mniShop_Click(object sender, EventArgs e)
        {
            Util.ShowScreen(Param.Screen.ShopInfo);
            Param.selectedScreen = (int)Param.Screen.ShopInfo;
        }

        private void mniReceive_Click(object sender, EventArgs e)
        {
            Util.ShowScreen(Param.Screen.ReceiveProduct);
            Param.selectedScreen = (int)Param.Screen.ReceiveProduct;
        }

        private void mniSell_Click(object sender, EventArgs e)
        {
            Util.ShowScreen(Param.Screen.Sell);
            Param.selectedScreen = (int)Param.Screen.Sell;
        }

        private void mniReportSell_Click(object sender, EventArgs e)
        {
            Util.ShowScreen(Param.Screen.Report);
            Param.selectedScreen = (int)Param.Screen.Report;
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

        private void mniChangePassword_Click(object sender, EventArgs e)
        {
            new FmChangePassword().ShowDialog(this);
        }

        private void mniConfig_Click(object sender, EventArgs e)
        {
            Util.ShowScreen(Param.Screen.Config);
            Param.selectedScreen = (int)Param.Screen.Config;
        }

        private void mniClaim_Click(object sender, EventArgs e)
        {
            Util.ShowScreen(Param.Screen.Claim);
            Param.selectedScreen = (int)Param.Screen.Claim;
        }

        private void bwGetShopInfo_DoWork(object sender, DoWorkEventArgs e)
        {
            Param.azureStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=" + Param.databaseName + ";AccountKey=" + Param.databasePassword);
            Param.azureTableClient = Param.azureStorageAccount.CreateCloudTableClient();
            Util.GetShopInfo();
        }

        private void bwGetShopInfo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Text = string.Format("Power POS - ร้าน {0} ({1})", Param.shopName, Param.computerName);
            lblStatus.Text = "";
            menuStrip1.Enabled = true;
            toolStrip1.Enabled = true;

            Util.SetStatusMessage("กำลังโหลดข้อมูลการตั้งค่าระบบ");
            Util.GetConfig();
            lblStatus.Text = "";

            mniSell_Click(sender, e);

            //Util.GetBarcode();
        }
    }
}
