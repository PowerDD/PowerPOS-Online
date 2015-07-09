using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerPOS_Online
{
    public partial class UcSell : UserControl
    {
        public UcSell()
        {
            InitializeComponent();
        }

        private void UcSell_Load(object sender, EventArgs e)
        {
            Util.InitialTable(table1);
        }

        private void btnMan_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnMan);
            SetDefaultButton(btnWoman);
            Param.SelectCustomerSex = 1;
        }

        private void btnWoman_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnWoman);
            SetDefaultButton(btnMan);
            Param.SelectCustomerSex = 0;
        }

        private void btnAge1_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnAge1);
            SetDefaultButton(btnAge2);
            SetDefaultButton(btnAge3);
            SetDefaultButton(btnAge4);
            SetDefaultButton(btnAge5);
            SetDefaultButton(btnAge6);
            if (Param.SelectCustomerId == "0") Param.SelectCustomerAge = 1;
        }

        private void btnAge2_Click(object sender, EventArgs e)
        {
            SetDefaultButton(btnAge1);
            SetActiveButton(btnAge2);
            SetDefaultButton(btnAge3);
            SetDefaultButton(btnAge4);
            SetDefaultButton(btnAge5);
            SetDefaultButton(btnAge6);
            if (Param.SelectCustomerId == "0") Param.SelectCustomerAge = 13;
        }

        private void btnAge3_Click(object sender, EventArgs e)
        {
            SetDefaultButton(btnAge1);
            SetDefaultButton(btnAge2);
            SetActiveButton(btnAge3);
            SetDefaultButton(btnAge4);
            SetDefaultButton(btnAge5);
            SetDefaultButton(btnAge6);
            if (Param.SelectCustomerId == "0") Param.SelectCustomerAge = 19;
        }

        private void btnAge4_Click(object sender, EventArgs e)
        {
            SetDefaultButton(btnAge1);
            SetDefaultButton(btnAge2);
            SetDefaultButton(btnAge3);
            SetActiveButton(btnAge4);
            SetDefaultButton(btnAge5);
            SetDefaultButton(btnAge6);
            if (Param.SelectCustomerId == "0") Param.SelectCustomerAge = 26;
        }

        private void btnAge5_Click(object sender, EventArgs e)
        {
            SetDefaultButton(btnAge1);
            SetDefaultButton(btnAge2);
            SetDefaultButton(btnAge3);
            SetDefaultButton(btnAge4);
            SetActiveButton(btnAge5);
            SetDefaultButton(btnAge6);
            if (Param.SelectCustomerId == "0") Param.SelectCustomerAge = 41;
        }

        private void btnAge6_Click(object sender, EventArgs e)
        {
            SetDefaultButton(btnAge1);
            SetDefaultButton(btnAge2);
            SetDefaultButton(btnAge3);
            SetDefaultButton(btnAge4);
            SetDefaultButton(btnAge5);
            SetActiveButton(btnAge6);
            if (Param.SelectCustomerId == "0") Param.SelectCustomerAge = 61;
        }

        private void SetActiveButton(Button button)
        {
            button.ForeColor = Color.White;
            button.BackColor = Color.SteelBlue;
            //button.FlatStyle = FlatStyle.Flat;
        }

        private void SetDefaultButton(Button button)
        {
            button.ForeColor = SystemColors.ControlText;
            button.BackColor = SystemColors.Control;
            //button.FlatStyle = FlatStyle.Standard;
        }

        private void txtBarcode_Enter(object sender, EventArgs e)
        {
            Util.SetKeyboardLayout(Util.GetInputLanguageByName("US"));
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            lblStatus.Visible = false;

            if (e.KeyCode == Keys.Return)
            {
            }
        }
    }
}
