using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Dynamic;
using Newtonsoft.Json;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Net;
using Microsoft.WindowsAzure.Storage.Table;

namespace PowerPOS_Online
{
    public partial class UcConfig : UserControl
    {
        bool _FIRST_LOAD = true;
        string _LOGO_URL = string.Empty;

        public UcConfig()
        {
            InitializeComponent();
        }

        private void UcConfig_Load(object sender, EventArgs e)
        {
            if( Param.SystemConfig.Bill == null ){
                var json = JsonConvert.SerializeObject(Param.SystemConfig);
                json = json.Substring(0, json.Length - 1) + ", \"Bill\":{\"PrintCount\":2,\"PrintType\":\"Y\",\"PrintLogo\":\"Y\",\"HeaderName\":\"ใบส่งของ\",\"FooterText\":\"Line Official ID : @RemaxThailand\",\"Logo\":\"" + Param.LogoUrl + "\"}";
                Param.SystemConfig = JsonConvert.DeserializeObject(json.ToString());
                bwUpdateConfig.RunWorkerAsync();
                //var json = "{\"Bill\":{\"PrintCount\":1,\"PrintType\":\"Y\",\"PrintLogo\":\"Y\",\"HeaderName\":\"ใบส่งของ\",\"Line Official ID : @RemaxThailand\":\"H\",\"Printer\":\"\"}";

                //Param.systemConfig = Tuple.Create(Param.systemConfig, Bill);
                //Param.systemConfig = Tuple.Create(Param.systemConfig.Item1, Param.systemConfig.Item2);
            }

            nudPrintCount.Value = Param.SystemConfig.Bill.PrintCount;
            rdbPrint.Checked = "" + Param.SystemConfig.Bill.PrintType == "Y";
            rdbNotPrint.Checked = "" + Param.SystemConfig.Bill.PrintType == "N";
            rdbAlert.Checked = "" + Param.SystemConfig.Bill.PrintType == "A";
            rdbLogoPrint.Checked = "" + Param.SystemConfig.Bill.PrintLogo == "Y";
            rdbLogoNotPrint.Checked = "" + Param.SystemConfig.Bill.PrintLogo == "N";
            txtBillHeader.Text = "" + Param.SystemConfig.Bill.HeaderName;
            txtBillFooter.Text = "" + Param.SystemConfig.Bill.FooterText;

            ptbLogo.ImageLocation = File.Exists(Param.LogoPath) ? Param.LogoPath : Param.SystemConfig.Bill.Logo;

            GetPrinter();
            _FIRST_LOAD = false;

            CheckLogoUrl();

        }

        private void GetPrinter()
        {
            var index = 0;
            var i = 0;
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cbxPrinter.Items.Add(printer);
                if (Param.DevicePrinter == printer) index = i;
                i++;
            }
            cbxPrinter.SelectedIndex = index;
        }

        private void bwUpdateConfig_DoWork(object sender, DoWorkEventArgs e)
        {
            dynamic d = new DynamicEntity(Param.ShopId, "Config");
            d.Value = JsonConvert.SerializeObject(Param.SystemConfig);
            var azureTable = Param.AzureTableClient.GetTableReference("Shop");
            TableBatchOperation batchOperation = new TableBatchOperation();
            batchOperation.InsertOrMerge(d);
            azureTable.ExecuteBatch(batchOperation);


            d = new DynamicEntity(Param.ShopId, Param.LicenseKey);
            d.DevicePrinter = Param.DevicePrinter;
            azureTable = Param.AzureTableClient.GetTableReference("ShopApplication");
            batchOperation = new TableBatchOperation();
            batchOperation.InsertOrMerge(d);
            azureTable.ExecuteBatch(batchOperation);

            Properties.Settings.Default.DevicePrinter = Param.DevicePrinter;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();

        }

        private void rdbAlert_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbAlert.Checked && !_FIRST_LOAD)
            {
                Param.SystemConfig.Bill.PrintType = "A";
                btnSaveBill.Enabled = true;
            }
        }

        private void rdbPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbPrint.Checked && !_FIRST_LOAD)
            {
                Param.SystemConfig.Bill.PrintType = "Y";
                btnSaveBill.Enabled = true;
            }
        }

        private void rdbNotPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbNotPrint.Checked && !_FIRST_LOAD)
            {
                Param.SystemConfig.Bill.PrintType = "N";
                btnSaveBill.Enabled = true;
            }
        }

        private void nudPrintCount_ValueChanged(object sender, EventArgs e)
        {
            if (!_FIRST_LOAD)
            {
                Param.SystemConfig.Bill.PrintCount = (int)nudPrintCount.Value;
                btnSaveBill.Enabled = true;
            }
        }

        private void rdbLogoPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbLogoPrint.Checked && !_FIRST_LOAD)
            {
                Param.SystemConfig.Bill.PrintLogo = "Y";
                btnSaveBill.Enabled = true;
            }
        }

        private void rdbLogoNotPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbLogoNotPrint.Checked && !_FIRST_LOAD)
            {
                Param.SystemConfig.Bill.PrintLogo = "N";
                btnSaveBill.Enabled = true;
            }
        }

        private void txtBillHeader_TextChanged(object sender, EventArgs e)
        {
            if (!_FIRST_LOAD)
            {
                Param.SystemConfig.Bill.HeaderName = txtBillHeader.Text;
                btnSaveBill.Enabled = true;
            }
        }

        private void txtBillFooter_TextChanged(object sender, EventArgs e)
        {
            if (!_FIRST_LOAD)
            {
                Param.SystemConfig.Bill.FooterText = txtBillFooter.Text;
                btnSaveBill.Enabled = true;
            }
        }

        private void cbxPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_FIRST_LOAD)
            {
                Param.DevicePrinter = cbxPrinter.SelectedItem.ToString();
                btnSaveBill.Enabled = true;
            }
        }

        private void btnSaveBill_Click(object sender, EventArgs e)
        {
            btnSaveBill.Enabled = false;
            gbxBill.Enabled = false;

            if (bwUpdateConfig.IsBusy)
            {
                bwUpdateConfig.CancelAsync();
            }
            while (bwUpdateConfig.IsBusy)
            {
            }

            Param.DevicePrinter = cbxPrinter.SelectedItem.ToString();
            bwUpdateConfig.RunWorkerAsync();
        }

        private void bwUpdateConfig_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnSaveBill.Enabled = false;
            gbxBill.Enabled = true;
        }

        private void ptbLogo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChangeLogo();
        }

        private void mniChangeLogo_Click(object sender, EventArgs e)
        {
            ChangeLogo();
        }

        private void ChangeLogo()
        {
            var result = openFileDialog1.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                ptbLogo.ImageLocation = openFileDialog1.FileName;
                if (!Directory.Exists(@"Resource/Images/")) Directory.CreateDirectory(@"Resource/Images/");
                var filename = @"Resource/Images/logo" + Path.GetExtension(openFileDialog1.FileName).ToLower();
                if (File.Exists(filename)) File.Delete(filename);
                File.Copy(openFileDialog1.FileName, filename);
                Param.LogoPath = filename;

                Properties.Settings.Default.LogoPath = Param.LogoPath;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Upgrade();

                gbxBill.Enabled = false;
                _LOGO_URL = "https://powerdb.blob.core.windows.net/images/POS/Logo/" + Param.ShopId + "/logo" + Path.GetExtension(openFileDialog1.FileName).ToLower();
                bwUploadLogo.RunWorkerAsync();
            }
        }

        private void CheckLogoUrl()
        {
            mniResetLogo.Visible = Param.SystemConfig.Bill.Logo != Param.LogoUrl;
        }

        private void bwUploadLogo_DoWork(object sender, DoWorkEventArgs e)
        {
            CloudBlobClient blobClient = Param.AzureStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("images");
            try
            {
                CloudBlockBlob blockBlob = container.GetBlockBlobReference("POS/Logo/" + Param.ShopId + Param.LogoPath.Replace("Resource/Images", ""));
                using (var fileStream = File.OpenRead(openFileDialog1.FileName))
                {
                    blockBlob.UploadFromStream(fileStream);
                    Param.SystemConfig.Bill.Logo = _LOGO_URL;
                    btnSaveBill_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error upload image {0}", ex.Message);
            }
        }

        private void bwUploadLogo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            gbxBill.Enabled = true;
            CheckLogoUrl();
        }

        private void mniResetLogo_Click(object sender, EventArgs e)
        {
            if (Param.SystemConfig.Bill.Logo != Param.LogoUrl)
            {
                ptbLogo.ImageLocation = Param.LogoUrl;
                if (!Directory.Exists(@"Resource/Images/")) Directory.CreateDirectory(@"Resource/Images/");
                if (File.Exists(Param.LogoPath)) File.Delete(Param.LogoPath);
                Param.LogoPath = @"Resource/Images/logo.jpg";

                Properties.Settings.Default.LogoPath = Param.LogoPath;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Upgrade();

                gbxBill.Enabled = false;
                bwDownloadLogo.RunWorkerAsync();
            }
        }

        private void bwDownloadLogo_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!Directory.Exists("Resource/Images")) Directory.CreateDirectory("Resource/Images");
            if (File.Exists(Param.LogoPath)) File.Delete(Param.LogoPath);
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri(Param.LogoUrl), Param.LogoPath);
                Param.SystemConfig.Bill.Logo = Param.LogoUrl;
                btnSaveBill_Click(sender, e);
            }
        }

        private void bwDownloadLogo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            gbxBill.Enabled = true;
            CheckLogoUrl();
        }
    }
}
