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
    public partial class FmCashReceive : Form
    {
        public int _TOTAL = 0;
        public string _SELL_NO;

        public FmCashReceive()
        {
            InitializeComponent();
        }

        private void FmCashReceive_Load(object sender, EventArgs e)
        {
            txtCash.Focus();
        }

        private void txtCash_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                lblChange.Text = (int.Parse(txtCash.Text) - _TOTAL).ToString("#,##0");
            }
            catch
            {
                lblChange.Text = _TOTAL.ToString("#,##0");
            }
        }

        private void txtCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var cash = int.Parse(txtCash.Text);
            if (cash != 0)
            {
                Util.DBExecute(string.Format(@"UPDATE SellHeader SET Cash = {0}, PayType = 1, Paid = 1, Sync = 1 WHERE SellNo = '{1}'", cash, _SELL_NO));
            }
            else
            {
                Util.DBExecute(string.Format(@"UPDATE SellHeader SET Cash = 0, PayType = 0, Paid = 0, Sync = 1 WHERE SellNo = '{0}'", _SELL_NO));
            }
            //Util.PrintReceipt(_SELL_NO);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void txtCash_Enter(object sender, EventArgs e)
        {
            Util.SetKeyboardLayout(Util.GetInputLanguageByName("US"));
        }
    }
}
