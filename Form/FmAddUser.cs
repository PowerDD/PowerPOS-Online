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
    public partial class FmAddUser : Form
    {
        public FmAddUser()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void FmAddUser_Load(object sender, EventArgs e)
        {
            foreach (var entry in Param.userGroup)
            {
                cbxUserGroup.Items.Add(entry.Name);
            }
            cbxUserGroup.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;

            Param.userEntity = new UserEntity(Param.shopId, txtUsername.Text.Trim().ToLower());
            Param.userEntity.Username = txtUsername.Text.Trim();
            Param.userEntity.Firstname = txtName.Text.Trim();
            Param.userEntity.Lastname = txtLastname.Text.Trim();
            Param.userEntity.Nickname = txtNickname.Text.Trim();
            Param.userEntity.Mobile = txtMobile.Text.Trim();
            Param.userEntity.Email = txtEmail.Text.Trim();
            Param.userEntity.Password = Util.EncodeString(txtPassword.Text.Trim());
            Param.userEntity.UserGroup = cbxUserGroup.SelectedItem.ToString();
            Param.userEntity.Active = true;
            //Param.userEntity.LastLogin = Convert.ToDateTime("1982/08/09");

            bwAddUser.RunWorkerAsync();
        }

        private void checkInput(object sender, EventArgs e)
        {
            btnSave.Enabled = txtNickname.Text.Trim() != "" &&
                txtUsername.Text.Trim() != "" &&
                txtPassword.Text != "" &&
                txtPassword.Text == txtPassword2.Text;
        }

        private void bwAddUser_DoWork(object sender, DoWorkEventArgs e)
        {
            Util.AddUser(Param.userEntity);
        }

        private void bwAddUser_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnSave.Enabled = true;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void SetThaiKeyboard(object sender, EventArgs e)
        {
            Util.SetKeyboardLayout(Util.GetInputLanguageByName("TH"));
        }

        private void SetEnglishKeyboard(object sender, EventArgs e)
        {
            Util.SetKeyboardLayout(Util.GetInputLanguageByName("EN"));
        }

        private void txtMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
