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
    public partial class FmUserDetail : Form
    {
        public string data = string.Empty;
        public FmUserDetail()
        {
            InitializeComponent();
            foreach (var entry in Param.userGroup)
            {
                cbxUserGroup.Items.Add(entry.Name);
            }
            cbxUserGroup.SelectedIndex = 0;
        }

        private void FmUserDetail_Load(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "คุณแน่ใจหรือไม่ ?\nที่จะลบผู้ใช้นี้ออกจากระบบ", "ยืนยันการทำงาน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.No;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        public void CheckInput(object sender, EventArgs e)
        {
            btnSave.Enabled = data != (txtName.Text + txtLastname.Text + txtNickname.Text + txtMobile.Text +
                txtEmail.Text + cbxUserGroup.SelectedItem.ToString() + rdbActive.Checked.ToString()) 
                && txtNickname.Text.Trim() != "";
        }
    }
}
