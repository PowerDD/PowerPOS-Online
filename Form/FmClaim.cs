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
    public partial class FmClaim : Form
    {
        public FmClaim()
        {
            InitializeComponent();
            this.Height = 317;
        }

        private void checkRadio(object sender, EventArgs e)
        {
            txtBarcode.Enabled = rdbSwap.Checked;
            txtCash.Enabled = rdbCash.Checked;
            gbxCustomer.Enabled = rdbHq.Checked;
            if (rdbSwap.Checked) txtBarcode.Focus();
            else if (rdbCash.Checked) txtCash.Focus();
            else if (rdbHq.Checked) txtMobile.Focus();

            gbxCustomer.Visible = rdbHq.Checked;
            this.Height = (gbxCustomer.Visible) ? 495 : 317;
        }
    }
}
