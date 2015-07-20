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
            txtLicenseKey.Text = Properties.Settings.Default.LicenseKey;
            lblDeviceID.Text = Param.CpuId;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Param.LicenseKey = txtLicenseKey.Text.Trim();
            Properties.Settings.Default.LicenseKey = Param.LicenseKey;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();

            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            txtLicenseKey.Enabled = false;
            dynamic jsonObject = Util.LoadAppConfig();
            if (!jsonObject.success)
            {
                MessageBox.Show(jsonObject.errorMessage, "Error " + jsonObject.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                txtLicenseKey.Enabled = true;
                lblDeviceID.Visible = true;
            }
            else
            {
                lblDeviceID.Visible = false;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
