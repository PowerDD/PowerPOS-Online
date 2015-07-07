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

namespace PowerPOS_Online
{
    public partial class UcConfig : UserControl
    {
        bool _FIRST_LOAD = true;
        public UcConfig()
        {
            InitializeComponent();
        }

        private void UcConfig_Load(object sender, EventArgs e)
        {
            if( Param.systemConfig.Bill == null ){
                var json = JsonConvert.SerializeObject(Param.systemConfig);
                json = json.Substring(0, json.Length - 1) + ", \"Bill\":{\"PrintCount\":2,\"PrintType\":\"Y\",\"PrintLogo\":\"Y\",\"HeaderName\":\"ใบส่งของ\",\"FooterText\":\"Line Official ID : @RemaxThailand\",\"Printer\":\"Bullzip PDF Printer\"}";
                Param.systemConfig = JsonConvert.DeserializeObject(json.ToString());
                backgroundWorker1.RunWorkerAsync();
                //var json = "{\"Bill\":{\"PrintCount\":1,\"PrintType\":\"Y\",\"PrintLogo\":\"Y\",\"HeaderName\":\"ใบส่งของ\",\"Line Official ID : @RemaxThailand\":\"H\",\"Printer\":\"\"}";

                //Param.systemConfig = Tuple.Create(Param.systemConfig, Bill);
                //Param.systemConfig = Tuple.Create(Param.systemConfig.Item1, Param.systemConfig.Item2);
            }

            nudPrintCount.Value = Param.systemConfig.Bill.PrintCount;
            rdbPrint.Checked = "" + Param.systemConfig.Bill.PrintType == "Y";
            rdbNotPrint.Checked = "" + Param.systemConfig.Bill.PrintType == "N";
            rdbAlert.Checked = "" + Param.systemConfig.Bill.PrintType == "A";
            rdbLogoPrint.Checked = "" + Param.systemConfig.Bill.PrintLogo == "Y";
            rdbLogoNotPrint.Checked = "" + Param.systemConfig.Bill.PrintLogo == "N";
            txtBillHeader.Text = "" + Param.systemConfig.Bill.HeaderName;
            txtBillFooter.Text = "" + Param.systemConfig.Bill.FooterText;

            GetPrinter();
            _FIRST_LOAD = false;

        }

        private void GetPrinter()
        {
            var index = 0;
            var i = 0;
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cbxPrinter.Items.Add(printer);
                if (Param.systemConfig.Bill.Printer == printer) index = i;
                i++;
            }
            cbxPrinter.SelectedIndex = index;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Util.UpdateConfig();
        }

        private void rdbAlert_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbAlert.Checked && !_FIRST_LOAD)
            {
                Param.systemConfig.Bill.PrintType = "A";
                btnSaveBill.Enabled = true;
            }
        }

        private void rdbPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbPrint.Checked && !_FIRST_LOAD)
            {
                Param.systemConfig.Bill.PrintType = "Y";
                btnSaveBill.Enabled = true;
            }
        }

        private void rdbNotPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbNotPrint.Checked && !_FIRST_LOAD)
            {
                Param.systemConfig.Bill.PrintType = "N";
                btnSaveBill.Enabled = true;
            }
        }

        private void nudPrintCount_ValueChanged(object sender, EventArgs e)
        {
            if (!_FIRST_LOAD)
            {
                Param.systemConfig.Bill.PrintCount = (int)nudPrintCount.Value;
                btnSaveBill.Enabled = true;
            }
        }

        private void rdbLogoPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbLogoPrint.Checked && !_FIRST_LOAD)
            {
                Param.systemConfig.Bill.PrintLogo = "Y";
                btnSaveBill.Enabled = true;
            }
        }

        private void rdbLogoNotPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbLogoNotPrint.Checked && !_FIRST_LOAD)
            {
                Param.systemConfig.Bill.PrintLogo = "N";
                btnSaveBill.Enabled = true;
            }
        }

        private void txtBillHeader_TextChanged(object sender, EventArgs e)
        {
            if (!_FIRST_LOAD)
            {
                Param.systemConfig.Bill.HeaderName = txtBillHeader.Text;
                btnSaveBill.Enabled = true;
            }
        }

        private void txtBillFooter_TextChanged(object sender, EventArgs e)
        {
            if (!_FIRST_LOAD)
            {
                Param.systemConfig.Bill.FooterText = txtBillFooter.Text;
                btnSaveBill.Enabled = true;
            }
        }

        private void cbxPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_FIRST_LOAD)
            {
                Param.systemConfig.Bill.Printer = cbxPrinter.SelectedItem.ToString();
                btnSaveBill.Enabled = true;
            }
        }

        private void btnSaveBill_Click(object sender, EventArgs e)
        {
            btnSaveBill.Enabled = false;
            gbxBill.Enabled = false;

            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            while (backgroundWorker1.IsBusy)
            {

            }
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnSaveBill.Enabled = false;
            gbxBill.Enabled = true;
        }
    }
}
