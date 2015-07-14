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
            foreach (var entry in Param.UserGroup)
            {
                cbxUserGroup.Items.Add(entry.Name);
            }
            cbxUserGroup.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;

            Param.UserEntity = new UserEntity(Param.ShopId, txtUsername.Text.Trim().ToLower());
            Param.UserEntity.Username = txtUsername.Text.Trim();
            Param.UserEntity.Firstname = txtName.Text.Trim();
            Param.UserEntity.Lastname = txtLastname.Text.Trim();
            Param.UserEntity.Nickname = txtNickname.Text.Trim();
            Param.UserEntity.Mobile = txtMobile.Text.Trim();
            Param.UserEntity.Email = txtEmail.Text.Trim();
            Param.UserEntity.Password = Util.MD5String(txtPassword.Text.Trim());
            Param.UserEntity.UserGroup = cbxUserGroup.SelectedItem.ToString();
            Param.UserEntity.Active = true;
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
            Util.AddUser(Param.UserEntity);
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
