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
    public partial class FmLicense : Form
    {
        public FmLicense()
        {
            InitializeComponent();
        }

        private void FmLicense_Load(object sender, EventArgs e)
        {
            txtApiKey.Text = Properties.Settings.Default.ApiKey;
            txtLicenseKey.Text = Properties.Settings.Default.LicenseKey;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Param.apiKey = txtApiKey.Text.Trim();
            Param.licenseKey = txtLicenseKey.Text.Trim();
            Properties.Settings.Default.ApiKey = Param.apiKey;
            Properties.Settings.Default.LicenseKey = Param.licenseKey;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();

            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            txtApiKey.Enabled = false;
            txtLicenseKey.Enabled = false;
            dynamic jsonObject = Util.DownloadAppConfig();
            if (!jsonObject.success)
            {
                MessageBox.Show(jsonObject.errorMessage, "Error " + jsonObject.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                txtApiKey.Enabled = true;
                txtLicenseKey.Enabled = true;
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
