using Microsoft.WindowsAzure;
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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerPOS_Online
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            Param.UserId = "0000";
            Util.ConnectSQLiteDatabase();
            this.Opacity = 0;
            this.ShowInTaskbar = false;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Param.InitialFinished = false;
            InitialCloudData();
        }

        private void InitialCloudData()
        {
            this.Opacity = 0;
            this.ShowInTaskbar = false;
            Param.MainPanel = this.pnlMain;

            FmInitialData fm = new FmInitialData();
            var result = fm.ShowDialog(this);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.Text = string.Format("Power POS - ร้าน {0} ({1})", Param.ShopName, Param.ComputerName);
                menuStrip1.Enabled = true;
                toolStrip1.Enabled = true;
                this.Opacity = 100;
                this.ShowInTaskbar = true;
                Param.InitialFinished = true;

                Util.ShowScreen(Param.Screen.Sell);
                Param.SelectedScreen = (int)Param.Screen.Sell;
            }
            else
            {
                this.Dispose();
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
            Param.SelectedScreen = (int)Param.Screen.Product;
        }

        private void mniCategory_Click(object sender, EventArgs e)
        {
            Util.ShowScreen(Param.Screen.Category);
            Param.SelectedScreen = (int)Param.Screen.Category;
        }

        private void mniBrand_Click(object sender, EventArgs e)
        {
            Util.ShowScreen(Param.Screen.Brand);
            Param.SelectedScreen = (int)Param.Screen.Brand;
        }

        private void mniColor_Click(object sender, EventArgs e)
        {
            Util.ShowScreen(Param.Screen.Color);
            Param.SelectedScreen = (int)Param.Screen.Color;
        }

        private void mniCustomer_Click(object sender, EventArgs e)
        {
            Util.ShowScreen(Param.Screen.Customer);
            Param.SelectedScreen = (int)Param.Screen.Customer;
        }

        private void mniUser_Click(object sender, EventArgs e)
        {
            Util.ShowScreen(Param.Screen.User);
            Param.SelectedScreen = (int)Param.Screen.User;
        }

        private void mniShop_Click(object sender, EventArgs e)
        {
            Util.ShowScreen(Param.Screen.ShopInfo);
            Param.SelectedScreen = (int)Param.Screen.ShopInfo;
        }

        private void mniReceive_Click(object sender, EventArgs e)
        {
            Util.ShowScreen(Param.Screen.ReceiveProduct);
            Param.SelectedScreen = (int)Param.Screen.ReceiveProduct;
        }

        private void mniSell_Click(object sender, EventArgs e)
        {
            Util.ShowScreen(Param.Screen.Sell);
            Param.SelectedScreen = (int)Param.Screen.Sell;
        }

        private void mniReportSell_Click(object sender, EventArgs e)
        {
            Util.ShowScreen(Param.Screen.Report);
            Param.SelectedScreen = (int)Param.Screen.Report;
        }

        private void mniRegister_Click(object sender, EventArgs e)
        {
            Param.ApiChecked = false;
            while (!Param.ApiChecked)
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
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            if (opdImportBarcode.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                DataTable dt = Util.DBQuery(string.Format(@"SELECT IFNULL(SUBSTR(MAX(SellNo), 1, 5)||SUBSTR('0000'||(SUBSTR(MAX(SellNo), 6, 4)+1), -4, 4), SUBSTR(STRFTIME('%Y%m{0}'), 3)||'0001') SellNo
                    FROM Barcode
                    WHERE SUBSTR(SellNo, 1, 4) = SUBSTR(STRFTIME('%Y%m'), 3, 4)
                    AND SUBSTR(SellNo, 5, 1) = '{0}'", "Z"));
                var SellNo = dt.Rows[0]["SellNo"].ToString();

                FileInfo f = new FileInfo(opdImportBarcode.FileName);
                string date = f.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                Console.WriteLine(date);

                StreamReader sr = new StreamReader(opdImportBarcode.FileName);
                string barcode = sr.ReadLine();
                while( barcode != null)
                {
                    Util.DBExecute(string.Format(@"UPDATE Barcode SET
                        SellDate= '{0}', 
                        SellBy = '0000', 
                        SellNo ='{2}',
                        SellFinished = 1 ,
                        Sync = 1,
                        Customer = '000000',
                        SellPrice = (SELECT  Price  FROM Product WHERE shop = '00000002' AND id = (SELECT Product FROM Barcode  WHERE Barcode   = {1}))
                        WHERE Barcode = {1}
                    ",date,barcode,SellNo));

                    Console.WriteLine(barcode);
                    barcode = sr.ReadLine();
                }
                dt = Util.DBQuery(string.Format(@"SELECT COUNT(*) count FROM Barcode WHERE SellNo = '{0}'", SellNo));
                if (Convert.ToInt32(dt.Rows[0]["count"].ToString()) > 0)
                {

                    Util.DBExecute(string.Format(@"INSERT INTO SellHeader (SellNo, Profit, TotalPrice, Cash, Customer, CustomerSex, CustomerAge, SellDate, SellBy,Sync)
                    SELECT '{0}', (SELECT SUM(SellPrice-Cost-OperationCost) FROM Barcode WHERE SellNo = '{0}'),
                    (SELECT SUM(SellPrice) FROM Barcode WHERE SellNo = '{0}'),(SELECT SUM(SellPrice) FROM Barcode WHERE SellNo = '{0}'), '000000', '', 0, '{1}', '{2}',1",
                       SellNo, date, Param.UserId));

                    Util.DBExecute(string.Format(@"INSERT INTO SellDetail (SellNo, Product, SellPrice, Cost, Quantity, Sync)
                    SELECT '{0}', Product, SUM(SellPrice), SUM(Cost), COUNT(*), 1 FROM Barcode WHERE SellNo = '{0}' GROUP BY Product",
                        SellNo));
                    MessageBox.Show("นำข้อมูลเข้าระบบเรียบร้อยแล้ว จำนวน : " + dt.Rows[0]["count"].ToString() + " Records (ไฟล์ : " + opdImportBarcode.FileName + ")", "ยืนยันการนำข้อมูลเข้าระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
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
            Param.SelectedScreen = (int)Param.Screen.Config;
        }

        private void mniClaim_Click(object sender, EventArgs e)
        {
            Util.ShowScreen(Param.Screen.Claim);
            Param.SelectedScreen = (int)Param.Screen.Claim;
        }

        private void mniReturn_Click(object sender, EventArgs e)
        {
            Util.ShowScreen(Param.Screen.Return);
            Param.SelectedScreen = (int)Param.Screen.Return;
        }

        private void btnUpdateData_Click(object sender, EventArgs e)
        {
            InitialCloudData();
        }

        private void tmSync_Tick(object sender, EventArgs e)
        {
            if (!bwSync.IsBusy && Param.InitialFinished)
            {
                bwSync.RunWorkerAsync();
                lblStatus.Visible = true;
                lblStatus.Text = "กำลัง Sync ข้อมูลเข้าระบบ Cloud";
            }
        }

        private void bwSync_DoWork(object sender, DoWorkEventArgs e)
        {
            //## Product Data ##//
            Util.SyncData();
        }

        private void bwSync_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblStatus.Visible = false;
            lblStatus.Text = "";
        }

    }
}
