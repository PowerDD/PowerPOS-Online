namespace PowerPOS_Online
{
    partial class UcUser
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            XPTable.Models.DataSourceColumnBinder dataSourceColumnBinder1 = new XPTable.Models.DataSourceColumnBinder();
            XPTable.Renderers.DragDropRenderer dragDropRenderer1 = new XPTable.Renderers.DragDropRenderer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.table1 = new XPTable.Models.Table();
            this.columnModel1 = new XPTable.Models.ColumnModel();
            this.clNo = new XPTable.Models.TextColumn();
            this.clStatus = new XPTable.Models.ImageColumn();
            this.clName = new XPTable.Models.TextColumn();
            this.clLastname = new XPTable.Models.TextColumn();
            this.clNickname = new XPTable.Models.TextColumn();
            this.clUserGroup = new XPTable.Models.TextColumn();
            this.clUsername = new XPTable.Models.TextColumn();
            this.clMobile = new XPTable.Models.TextColumn();
            this.clEmail = new XPTable.Models.TextColumn();
            this.tableModel1 = new XPTable.Models.TableModel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCancelAddGroup = new System.Windows.Forms.Button();
            this.btnDeleteGroup = new System.Windows.Forms.Button();
            this.btnAddGroup = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.cbxUserGroup = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserGroup = new System.Windows.Forms.TextBox();
            this.bwGetUserGroup = new System.ComponentModel.BackgroundWorker();
            this.bwUpdateUserGroup = new System.ComponentModel.BackgroundWorker();
            this.bwDeleteUserGroup = new System.ComponentModel.BackgroundWorker();
            this.bwLoadData = new System.ComponentModel.BackgroundWorker();
            this.bwUpdateData = new System.ComponentModel.BackgroundWorker();
            this.bwDeleteData = new System.ComponentModel.BackgroundWorker();
            this.bwAddData = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.table1)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1071, 39);
            this.panel1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("DilleniaUPC", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "ข้อมูลผู้ใช้งานระบบ";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.table1);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1071, 468);
            this.panel2.TabIndex = 9;
            // 
            // table1
            // 
            this.table1.BorderColor = System.Drawing.Color.Black;
            this.table1.ColumnModel = this.columnModel1;
            this.table1.DataMember = null;
            this.table1.DataSourceColumnBinder = dataSourceColumnBinder1;
            this.table1.Dock = System.Windows.Forms.DockStyle.Fill;
            dragDropRenderer1.ForeColor = System.Drawing.Color.Red;
            this.table1.DragDropRenderer = dragDropRenderer1;
            this.table1.EnableHeaderContextMenu = false;
            this.table1.FullRowSelect = true;
            this.table1.GridLines = XPTable.Models.GridLines.Both;
            this.table1.GridLinesContrainedToData = false;
            this.table1.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.table1.Location = new System.Drawing.Point(0, 44);
            this.table1.Name = "table1";
            this.table1.NoItemsText = "";
            this.table1.SelectionStyle = XPTable.Models.SelectionStyle.Grid;
            this.table1.Size = new System.Drawing.Size(1071, 424);
            this.table1.TabIndex = 13;
            this.table1.TableModel = this.tableModel1;
            this.table1.TabStop = false;
            this.table1.Text = "table1";
            this.table1.UnfocusedBorderColor = System.Drawing.Color.Black;
            this.table1.CellDoubleClick += new XPTable.Events.CellMouseEventHandler(this.table1_CellDoubleClick);
            // 
            // columnModel1
            // 
            this.columnModel1.Columns.AddRange(new XPTable.Models.Column[] {
            this.clNo,
            this.clStatus,
            this.clName,
            this.clLastname,
            this.clNickname,
            this.clUserGroup,
            this.clUsername,
            this.clMobile,
            this.clEmail});
            // 
            // clNo
            // 
            this.clNo.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clNo.Editable = false;
            this.clNo.IsTextTrimmed = false;
            this.clNo.Resizable = false;
            this.clNo.Sortable = false;
            this.clNo.Text = "ที่";
            this.clNo.Width = 40;
            // 
            // clStatus
            // 
            this.clStatus.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clStatus.DrawText = false;
            this.clStatus.IsTextTrimmed = false;
            this.clStatus.Resizable = false;
            this.clStatus.Sortable = false;
            this.clStatus.Width = 24;
            // 
            // clName
            // 
            this.clName.Editable = false;
            this.clName.IsTextTrimmed = false;
            this.clName.Text = "ชื่อ";
            this.clName.Width = 150;
            // 
            // clLastname
            // 
            this.clLastname.Editable = false;
            this.clLastname.IsTextTrimmed = false;
            this.clLastname.Text = "นามสกุล";
            this.clLastname.Width = 150;
            // 
            // clNickname
            // 
            this.clNickname.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clNickname.Editable = false;
            this.clNickname.IsTextTrimmed = false;
            this.clNickname.Text = "ชื่อเล่น";
            this.clNickname.Width = 80;
            // 
            // clUserGroup
            // 
            this.clUserGroup.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clUserGroup.Editable = false;
            this.clUserGroup.IsTextTrimmed = false;
            this.clUserGroup.Text = "ประเภทผู้ใช้งาน";
            this.clUserGroup.Width = 150;
            // 
            // clUsername
            // 
            this.clUsername.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clUsername.Editable = false;
            this.clUsername.IsTextTrimmed = false;
            this.clUsername.Text = "ชื่อผู้ใช้";
            this.clUsername.Width = 100;
            // 
            // clMobile
            // 
            this.clMobile.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clMobile.Editable = false;
            this.clMobile.IsTextTrimmed = false;
            this.clMobile.Text = "เบอร์โทรศัพท์";
            this.clMobile.Width = 115;
            // 
            // clEmail
            // 
            this.clEmail.Editable = false;
            this.clEmail.IsTextTrimmed = false;
            this.clEmail.Text = "อีเมล";
            this.clEmail.Width = 240;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.checkBox1);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.checkBox2);
            this.panel4.Controls.Add(this.cbxUserGroup);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.txtUserGroup);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1071, 44);
            this.panel4.TabIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(346, 13);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 20);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "เข้าระบบได้";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnCancelAddGroup);
            this.panel3.Controls.Add(this.btnDeleteGroup);
            this.panel3.Controls.Add(this.btnAddGroup);
            this.panel3.Location = new System.Drawing.Point(277, 9);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(87, 26);
            this.panel3.TabIndex = 9;
            // 
            // btnCancelAddGroup
            // 
            this.btnCancelAddGroup.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCancelAddGroup.Image = global::PowerPOS_Online.Properties.Resources.arrow_circle_225_left;
            this.btnCancelAddGroup.Location = new System.Drawing.Point(56, 0);
            this.btnCancelAddGroup.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.btnCancelAddGroup.Name = "btnCancelAddGroup";
            this.btnCancelAddGroup.Size = new System.Drawing.Size(28, 26);
            this.btnCancelAddGroup.TabIndex = 8;
            this.btnCancelAddGroup.UseVisualStyleBackColor = true;
            this.btnCancelAddGroup.Visible = false;
            this.btnCancelAddGroup.Click += new System.EventHandler(this.btnCancelAddGroup_Click);
            // 
            // btnDeleteGroup
            // 
            this.btnDeleteGroup.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDeleteGroup.Enabled = false;
            this.btnDeleteGroup.Image = global::PowerPOS_Online.Properties.Resources.minus_circle;
            this.btnDeleteGroup.Location = new System.Drawing.Point(28, 0);
            this.btnDeleteGroup.Name = "btnDeleteGroup";
            this.btnDeleteGroup.Size = new System.Drawing.Size(28, 26);
            this.btnDeleteGroup.TabIndex = 9;
            this.btnDeleteGroup.UseVisualStyleBackColor = true;
            this.btnDeleteGroup.Click += new System.EventHandler(this.btnDeleteGroup_Click);
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAddGroup.Image = global::PowerPOS_Online.Properties.Resources.plus_white;
            this.btnAddGroup.Location = new System.Drawing.Point(0, 0);
            this.btnAddGroup.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(28, 26);
            this.btnAddGroup.TabIndex = 2;
            this.btnAddGroup.UseVisualStyleBackColor = true;
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnAddUser);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(909, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(162, 44);
            this.panel5.TabIndex = 6;
            // 
            // btnAddUser
            // 
            this.btnAddUser.Image = global::PowerPOS_Online.Properties.Resources.plus_circle;
            this.btnAddUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddUser.Location = new System.Drawing.Point(11, 6);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnAddUser.Size = new System.Drawing.Size(145, 33);
            this.btnAddUser.TabIndex = 5;
            this.btnAddUser.Text = "เพิ่มข้อมูลผู้ใช้งานใหม่";
            this.btnAddUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(432, 13);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(101, 20);
            this.checkBox2.TabIndex = 4;
            this.checkBox2.Text = "ยกเลิกการใช้งาน";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // cbxUserGroup
            // 
            this.cbxUserGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUserGroup.FormattingEnabled = true;
            this.cbxUserGroup.Items.AddRange(new object[] {
            "ทั้งหมด"});
            this.cbxUserGroup.Location = new System.Drawing.Point(93, 10);
            this.cbxUserGroup.Name = "cbxUserGroup";
            this.cbxUserGroup.Size = new System.Drawing.Size(182, 24);
            this.cbxUserGroup.TabIndex = 1;
            this.cbxUserGroup.SelectedIndexChanged += new System.EventHandler(this.cbxUserGroup_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "ประเภทผู้ใช้งาน";
            // 
            // txtUserGroup
            // 
            this.txtUserGroup.Location = new System.Drawing.Point(93, 11);
            this.txtUserGroup.Name = "txtUserGroup";
            this.txtUserGroup.Size = new System.Drawing.Size(182, 22);
            this.txtUserGroup.TabIndex = 7;
            this.txtUserGroup.Visible = false;
            this.txtUserGroup.TextChanged += new System.EventHandler(this.txtUserGroup_TextChanged);
            // 
            // bwGetUserGroup
            // 
            this.bwGetUserGroup.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwGetUserGroup_DoWork);
            this.bwGetUserGroup.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwGetUserGroup_RunWorkerCompleted);
            // 
            // bwUpdateUserGroup
            // 
            this.bwUpdateUserGroup.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwUpdateUserGroup_DoWork);
            this.bwUpdateUserGroup.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwUpdateUserGroup_RunWorkerCompleted);
            // 
            // bwDeleteUserGroup
            // 
            this.bwDeleteUserGroup.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwDeleteUserGroup_DoWork);
            this.bwDeleteUserGroup.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwDeleteUserGroup_RunWorkerCompleted);
            // 
            // bwLoadData
            // 
            this.bwLoadData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadData_DoWork);
            this.bwLoadData.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadData_RunWorkerCompleted);
            // 
            // bwUpdateData
            // 
            this.bwUpdateData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwUpdateData_DoWork);
            this.bwUpdateData.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwUpdateData_RunWorkerCompleted);
            // 
            // bwDeleteData
            // 
            this.bwDeleteData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwDeleteData_DoWork);
            this.bwDeleteData.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwDeleteData_RunWorkerCompleted);
            // 
            // UcUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "UcUser";
            this.Size = new System.Drawing.Size(1071, 507);
            this.Load += new System.EventHandler(this.UcUser_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.table1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnAddGroup;
        private System.Windows.Forms.ComboBox cbxUserGroup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private XPTable.Models.Table table1;
        private XPTable.Models.ColumnModel columnModel1;
        private XPTable.Models.TextColumn clNo;
        private XPTable.Models.TextColumn clName;
        private XPTable.Models.TextColumn clLastname;
        private XPTable.Models.TextColumn clNickname;
        private XPTable.Models.TextColumn clUserGroup;
        private XPTable.Models.TextColumn clUsername;
        private XPTable.Models.TableModel tableModel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnAddUser;
        private System.ComponentModel.BackgroundWorker bwGetUserGroup;
        private System.Windows.Forms.TextBox txtUserGroup;
        private System.Windows.Forms.Button btnCancelAddGroup;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnDeleteGroup;
        private System.ComponentModel.BackgroundWorker bwUpdateUserGroup;
        private System.ComponentModel.BackgroundWorker bwDeleteUserGroup;
        private XPTable.Models.TextColumn clMobile;
        private XPTable.Models.TextColumn clEmail;
        private System.ComponentModel.BackgroundWorker bwLoadData;
        private XPTable.Models.ImageColumn clStatus;
        private System.ComponentModel.BackgroundWorker bwUpdateData;
        private System.ComponentModel.BackgroundWorker bwDeleteData;
        private System.ComponentModel.BackgroundWorker bwAddData;
    }
}
