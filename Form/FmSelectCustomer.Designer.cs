namespace PowerPOS_Online
{
    partial class FmSelectCustomer
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            XPTable.Models.DataSourceColumnBinder dataSourceColumnBinder1 = new XPTable.Models.DataSourceColumnBinder();
            XPTable.Renderers.DragDropRenderer dragDropRenderer1 = new XPTable.Renderers.DragDropRenderer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.table1 = new XPTable.Models.Table();
            this.columnModel1 = new XPTable.Models.ColumnModel();
            this.clID = new XPTable.Models.TextColumn();
            this.clNo = new XPTable.Models.TextColumn();
            this.clFirstname = new XPTable.Models.TextColumn();
            this.clLastname = new XPTable.Models.TextColumn();
            this.clNickname = new XPTable.Models.TextColumn();
            this.clCardNo = new XPTable.Models.TextColumn();
            this.clMobile = new XPTable.Models.TextColumn();
            this.clCitizenID = new XPTable.Models.TextColumn();
            this.clBirthday = new XPTable.Models.TextColumn();
            this.clSex = new XPTable.Models.TextColumn();
            this.clSellPrice = new XPTable.Models.TextColumn();
            this.tableModel1 = new XPTable.Models.TableModel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.table1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(665, 43);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.txtSearch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(469, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(196, 43);
            this.panel2.TabIndex = 7;
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSearch.Image = global::PowerPOS_Online.Properties.Resources.magnifier_left;
            this.btnSearch.Location = new System.Drawing.Point(159, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(31, 31);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.Azure;
            this.txtSearch.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.MidnightBlue;
            this.txtSearch.Location = new System.Drawing.Point(5, 7);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(148, 29);
            this.txtSearch.TabIndex = 5;
            this.txtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
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
            this.table1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.table1.Location = new System.Drawing.Point(0, 43);
            this.table1.Name = "table1";
            this.table1.NoItemsText = "";
            this.table1.SelectionStyle = XPTable.Models.SelectionStyle.Grid;
            this.table1.Size = new System.Drawing.Size(665, 251);
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
            this.clID,
            this.clNo,
            this.clFirstname,
            this.clLastname,
            this.clNickname,
            this.clCardNo,
            this.clMobile,
            this.clCitizenID,
            this.clBirthday,
            this.clSex,
            this.clSellPrice});
            // 
            // clID
            // 
            this.clID.IsTextTrimmed = false;
            this.clID.Visible = false;
            // 
            // clNo
            // 
            this.clNo.Editable = false;
            this.clNo.IsTextTrimmed = false;
            this.clNo.Text = "ที่";
            this.clNo.Width = 40;
            // 
            // clFirstname
            // 
            this.clFirstname.Editable = false;
            this.clFirstname.IsTextTrimmed = false;
            this.clFirstname.Text = "ชื่อ";
            this.clFirstname.Width = 100;
            // 
            // clLastname
            // 
            this.clLastname.Editable = false;
            this.clLastname.IsTextTrimmed = false;
            this.clLastname.Text = "นามสกุล";
            this.clLastname.Width = 100;
            // 
            // clNickname
            // 
            this.clNickname.Editable = false;
            this.clNickname.IsTextTrimmed = false;
            this.clNickname.Text = "ชื่อเล่น";
            this.clNickname.Width = 65;
            // 
            // clCardNo
            // 
            this.clCardNo.Editable = false;
            this.clCardNo.IsTextTrimmed = false;
            this.clCardNo.Text = "เลขที่บัตรสมาชิก";
            this.clCardNo.Width = 95;
            // 
            // clMobile
            // 
            this.clMobile.Editable = false;
            this.clMobile.IsTextTrimmed = false;
            this.clMobile.Text = "มือถือ";
            this.clMobile.Width = 100;
            // 
            // clCitizenID
            // 
            this.clCitizenID.Editable = false;
            this.clCitizenID.IsTextTrimmed = false;
            this.clCitizenID.Text = "เลขที่บัตรประชาชน";
            this.clCitizenID.Width = 140;
            // 
            // clBirthday
            // 
            this.clBirthday.IsTextTrimmed = false;
            this.clBirthday.Visible = false;
            // 
            // clSex
            // 
            this.clSex.IsTextTrimmed = false;
            this.clSex.Visible = false;
            // 
            // clSellPrice
            // 
            this.clSellPrice.IsTextTrimmed = false;
            this.clSellPrice.Visible = false;
            // 
            // FmSelectCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 294);
            this.Controls.Add(this.table1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FmSelectCustomer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ค้นหาข้อมูลลูกค้า";
            this.Load += new System.EventHandler(this.FmSelectCustomer_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.table1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private XPTable.Models.Table table1;
        private XPTable.Models.ColumnModel columnModel1;
        private XPTable.Models.TextColumn clID;
        private XPTable.Models.TextColumn clNo;
        private XPTable.Models.TextColumn clFirstname;
        private XPTable.Models.TextColumn clLastname;
        private XPTable.Models.TextColumn clNickname;
        private XPTable.Models.TextColumn clCardNo;
        private XPTable.Models.TextColumn clMobile;
        private XPTable.Models.TextColumn clCitizenID;
        private XPTable.Models.TextColumn clSex;
        private XPTable.Models.TableModel tableModel1;
        private XPTable.Models.TextColumn clBirthday;
        private XPTable.Models.TextColumn clSellPrice;
    }
}