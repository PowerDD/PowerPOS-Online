﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.Storage.Table;
using XPTable.Models;

namespace PowerPOS_Online
{
    public partial class UcUser : UserControl
    {
        private FmAddUser fmAddUser;
        private int _SELECTED_ROW = -1;
        public UcUser()
        {
            InitializeComponent();
        }

        private void UcUser_Load(object sender, EventArgs e)
        {
            Util.InitialTable(table1);
            cbxUserGroup.SelectedIndex = 0;
            fmAddUser = new FmAddUser();
            bwGetUserGroup.RunWorkerAsync();
            bwLoadData.RunWorkerAsync();
        }

        private void bwGetUserGroup_DoWork(object sender, DoWorkEventArgs e)
        {
            Util.GetUserGroup();
        }

        private void bwGetUserGroup_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (var entry in Param.userGroup)
            {
                cbxUserGroup.Items.Add( entry.Name );
            }
        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            if (cbxUserGroup.Visible)
            {
                cbxUserGroup.Visible = false;
                txtUserGroup.Visible = true;
                btnCancelAddGroup.Visible = true;
                btnDeleteGroup.Visible = false;
                btnAddGroup.Enabled = txtUserGroup.Text.Trim() != "";
                btnAddGroup.Image = global::PowerPOS_Online.Properties.Resources.disk_return_black;
            }
            else
            {
                var json = JsonConvert.SerializeObject(Param.userGroup);
                json = json.Substring(0, json.Length - 1) + ", \"" + txtUserGroup.Text.Trim() + "\":{\"Screen\":\"|\"}}";
                Param.userGroup = JsonConvert.DeserializeObject(json.ToString());
                Util.SetStatusMessage("กำลังเพิ่มข้อมูล");
                bwUpdateUserGroup.RunWorkerAsync();
            }
        }

        private void btnCancelAddGroup_Click(object sender, EventArgs e)
        {
            cbxUserGroup.Visible = true;
            txtUserGroup.Visible = false;
            btnCancelAddGroup.Visible = false;
            btnAddGroup.Enabled = true;
            btnDeleteGroup.Visible = true;
            btnAddGroup.Image = global::PowerPOS_Online.Properties.Resources.plus_white;
        }

        private void txtUserGroup_TextChanged(object sender, EventArgs e)
        {
            btnAddGroup.Enabled = txtUserGroup.Text.Trim() != "";
        }

        private void bwUpdateUserGroup_DoWork(object sender, DoWorkEventArgs e)
        {
            Util.UpdateUserGroup();
        }

        private void bwUpdateUserGroup_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Util.SetStatusMessage("");
            cbxUserGroup.Items.Add(txtUserGroup.Text.Trim());
            txtUserGroup.Text = "";
            cbxUserGroup.SelectedIndex = cbxUserGroup.Items.Count - 1;
            btnCancelAddGroup_Click(sender, e);
        }

        private void cbxUserGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDeleteGroup.Enabled = cbxUserGroup.SelectedIndex > 1 
                && Param.userGroup[cbxUserGroup.SelectedItem.ToString()].Screen == "|";
        }

        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "คุณแน่ใจหรือไม่ ?\nที่จะลบหมวดหมู่นี้ออกจากระบบ", "ยืนยันการทำงาน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Param.userGroup.Remove(cbxUserGroup.SelectedItem.ToString());
                cbxUserGroup.Items.RemoveAt(cbxUserGroup.SelectedIndex);
                bwDeleteUserGroup.RunWorkerAsync();
            }
        }

        private void bwDeleteUserGroup_DoWork(object sender, DoWorkEventArgs e)
        {
            Util.UpdateUserGroup();
        }

        private void bwDeleteUserGroup_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Util.SetStatusMessage("");
            cbxUserGroup.SelectedIndex = 0;
            btnCancelAddGroup_Click(sender, e);
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            fmAddUser.ShowDialog(this);
        }

        private void bwLoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            Param.azureTable = Param.azureTableClient.GetTableReference("User");
            TableQuery<UserEntity> query = new TableQuery<UserEntity>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, Param.shopId),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.NotEqual, "Group")
            ));

            Param.userEntityList = new List<UserEntity>();
            foreach (UserEntity entity in Param.azureTable.ExecuteQuery(query))
            {
                Param.userEntityList.Add(entity);
            }
        }

        private void bwLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            table1.BeginUpdate();
            tableModel1.Rows.Clear();
            tableModel1.RowHeight = 22;
            for (int i = 0; i < Param.userEntityList.Count; i++)
            {
                var mobile = Param.userEntityList[i].Mobile.Length == 10 ?
                    Param.userEntityList[i].Mobile.Substring(0, 3) + "-" + Param.userEntityList[i].Mobile.Substring(3, 4) + "-" + Param.userEntityList[i].Mobile.Substring(7, 3) :
                    Param.userEntityList[i].Mobile;

                tableModel1.Rows.Add(new Row(
                    new Cell[] {
                    new Cell("" + (i+1)),
                    new Cell(Param.userEntityList[i].Active ? "1" : "0", Param.userEntityList[i].Active ? global::PowerPOS_Online.Properties.Resources.accept : global::PowerPOS_Online.Properties.Resources.exclamation),
                    new Cell(Param.userEntityList[i].Firstname),
                    new Cell(Param.userEntityList[i].Lastname),
                    new Cell(Param.userEntityList[i].Nickname),
                    new Cell(Param.userEntityList[i].UserGroup),
                    new Cell(Param.userEntityList[i].Username),
                    new Cell(mobile),
                    new Cell(Param.userEntityList[i].Email)
                    //,
                    //new Cell(Param.userEntityList[i].LastLogin.ToString("dd/MM/yyyy") == "01/01/0544" ? "-" : Param.userEntityList[i].LastLogin.ToString("dd/MM/yyyy hh:mm:ss")),
                    })
                );
            }
            table1.EndUpdate();
        }

        private void table1_CellDoubleClick(object sender, XPTable.Events.CellMouseEventArgs e)
        {
            _SELECTED_ROW = e.Row;

            FmUserDetail fm = new FmUserDetail();
            fm.txtName.Text = tableModel1.Rows[e.Row].Cells[2].Text;
            fm.txtLastname.Text = tableModel1.Rows[e.Row].Cells[3].Text;
            fm.txtNickname.Text = tableModel1.Rows[e.Row].Cells[4].Text;
            fm.txtMobile.Text = tableModel1.Rows[e.Row].Cells[7].Text.Replace("-", "");
            fm.txtEmail.Text = tableModel1.Rows[e.Row].Cells[8].Text;
            fm.cbxUserGroup.Text = tableModel1.Rows[e.Row].Cells[5].Text;
            fm.rdbActive.Checked = tableModel1.Rows[e.Row].Cells[1].Text == "1";
            fm.rdbDisable.Checked = tableModel1.Rows[e.Row].Cells[1].Text == "0";

            fm.data = fm.txtName.Text + fm.txtLastname.Text + fm.txtNickname.Text + fm.txtMobile.Text + 
                fm.txtEmail.Text + fm.cbxUserGroup.SelectedItem.ToString() + fm.rdbActive.Checked.ToString();
            fm.CheckInput(sender, e);
            var result = fm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                Param.userUpdateEntity = new UserUpdateEntity(Param.shopId, tableModel1.Rows[e.Row].Cells[6].Text.ToLower());
                Param.userUpdateEntity.Firstname = fm.txtName.Text.Trim();
                Param.userUpdateEntity.Lastname = fm.txtLastname.Text.Trim();
                Param.userUpdateEntity.Nickname = fm.txtNickname.Text.Trim();
                Param.userUpdateEntity.Mobile = fm.txtMobile.Text.Trim();
                Param.userUpdateEntity.Email = fm.txtEmail.Text.Trim();
                Param.userUpdateEntity.UserGroup = fm.cbxUserGroup.SelectedItem.ToString();
                Param.userUpdateEntity.Active = fm.rdbActive.Checked;
                bwUpdateData.RunWorkerAsync();
            }
            else if (result == DialogResult.No)
            {
                Param.userUpdateEntity = new UserUpdateEntity(Param.shopId, tableModel1.Rows[e.Row].Cells[6].Text.ToLower());
                Param.userUpdateEntity.ETag = "*";
                bwDeleteData.RunWorkerAsync();
            }
        }

        private void bwUpdateData_DoWork(object sender, DoWorkEventArgs e)
        {
            Param.azureTable = Param.azureTableClient.GetTableReference("User");
            TableOperation updateOperation = TableOperation.InsertOrMerge(Param.userUpdateEntity);
            Param.azureTable.Execute(updateOperation);
        }

        private void bwUpdateData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var mobile = Param.userUpdateEntity.Mobile.Length == 10 ?
                Param.userUpdateEntity.Mobile.Substring(0, 3) + "-" + Param.userUpdateEntity.Mobile.Substring(3, 4) + "-" + Param.userUpdateEntity.Mobile.Substring(7, 3) :
                Param.userUpdateEntity.Mobile.Trim();

            tableModel1.Rows[_SELECTED_ROW].Cells[2].Text = Param.userUpdateEntity.Firstname;
            tableModel1.Rows[_SELECTED_ROW].Cells[3].Text = Param.userUpdateEntity.Lastname;
            tableModel1.Rows[_SELECTED_ROW].Cells[4].Text = Param.userUpdateEntity.Nickname;
            tableModel1.Rows[_SELECTED_ROW].Cells[7].Text = mobile;
            tableModel1.Rows[_SELECTED_ROW].Cells[8].Text = Param.userUpdateEntity.Email;
            tableModel1.Rows[_SELECTED_ROW].Cells[5].Text = Param.userUpdateEntity.UserGroup;
            tableModel1.Rows[_SELECTED_ROW].Cells[1].Image = (Param.userUpdateEntity.Active) ? global::PowerPOS_Online.Properties.Resources.accept : global::PowerPOS_Online.Properties.Resources.exclamation;
            tableModel1.Rows[_SELECTED_ROW].Cells[1].Text = (Param.userUpdateEntity.Active) ? "1" : "0";
        }

        private void bwDeleteData_DoWork(object sender, DoWorkEventArgs e)
        {
            Param.azureTable = Param.azureTableClient.GetTableReference("User");
            TableOperation updateOperation = TableOperation.Delete(Param.userUpdateEntity);
            Param.azureTable.Execute(updateOperation);
        }

        private void bwDeleteData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            table1.BeginUpdate();
            tableModel1.Rows.RemoveAt(_SELECTED_ROW);
            table1.EndUpdate();
        }
    }
}
