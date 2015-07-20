namespace PowerPOS_Online
{
    partial class UcCustomer
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
            this.lblRecords = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnAddCustomer = new System.Windows.Forms.Button();
            this.gbOrderNo = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.tableModel1 = new XPTable.Models.TableModel();
            this.columnModel1 = new XPTable.Models.ColumnModel();
            this.clID = new XPTable.Models.TextColumn();
            this.clName = new XPTable.Models.TextColumn();
            this.clLastname = new XPTable.Models.TextColumn();
            this.clNickname = new XPTable.Models.TextColumn();
            this.clShopName = new XPTable.Models.TextColumn();
            this.clSellPrice = new XPTable.Models.TextColumn();
            this.clCredit = new XPTable.Models.TextColumn();
            this.clCardNo = new XPTable.Models.TextColumn();
            this.clMobile = new XPTable.Models.TextColumn();
            this.clCitizendId = new XPTable.Models.TextColumn();
            this.table1 = new XPTable.Models.Table();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.gbOrderNo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.table1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.lblRecords);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(965, 39);
            this.panel1.TabIndex = 6;
            // 
            // lblRecords
            // 
            this.lblRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblRecords.ForeColor = System.Drawing.Color.White;
            this.lblRecords.Location = new System.Drawing.Point(196, 13);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(42, 16);
            this.lblRecords.TabIndex = 9;
            this.lblRecords.Text = "0";
            this.lblRecords.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(236, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "รายการ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(101, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "ค้นพบข้อมูลทั้งหมด";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("DilleniaUPC", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "ข้อมูลลูกค้า";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.gbOrderNo);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 39);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(7, 10, 7, 0);
            this.panel3.Size = new System.Drawing.Size(247, 384);
            this.panel3.TabIndex = 9;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnAddCustomer);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(7, 73);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.panel6.Size = new System.Drawing.Size(233, 256);
            this.panel6.TabIndex = 11;
            // 
            // btnAddCustomer
            // 
            this.btnAddCustomer.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddCustomer.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnAddCustomer.Image = global::PowerPOS_Online.Properties.Resources.plus_circle;
            this.btnAddCustomer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddCustomer.Location = new System.Drawing.Point(0, 10);
            this.btnAddCustomer.Name = "btnAddCustomer";
            this.btnAddCustomer.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnAddCustomer.Size = new System.Drawing.Size(233, 38);
            this.btnAddCustomer.TabIndex = 11;
            this.btnAddCustomer.Text = "เพิ่มข้อมูลลูกค้า";
            this.btnAddCustomer.UseVisualStyleBackColor = true;
            this.btnAddCustomer.Click += new System.EventHandler(this.btnAddCustomer_Click);
            // 
            // gbOrderNo
            // 
            this.gbOrderNo.Controls.Add(this.btnSearch);
            this.gbOrderNo.Controls.Add(this.txtSearch);
            this.gbOrderNo.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbOrderNo.Location = new System.Drawing.Point(7, 10);
            this.gbOrderNo.Name = "gbOrderNo";
            this.gbOrderNo.Size = new System.Drawing.Size(233, 63);
            this.gbOrderNo.TabIndex = 0;
            this.gbOrderNo.TabStop = false;
            this.gbOrderNo.Text = "ค้นหาข้อมูล";
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSearch.Image = global::PowerPOS_Online.Properties.Resources.magnifier_left;
            this.btnSearch.Location = new System.Drawing.Point(190, 19);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(31, 31);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.Azure;
            this.txtSearch.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.MidnightBlue;
            this.txtSearch.Location = new System.Drawing.Point(14, 20);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(170, 29);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
            // 
            // columnModel1
            // 
            this.columnModel1.Columns.AddRange(new XPTable.Models.Column[] {
            this.clID,
            this.clName,
            this.clLastname,
            this.clNickname,
            this.clShopName,
            this.clSellPrice,
            this.clCredit,
            this.clCardNo,
            this.clMobile,
            this.clCitizendId});
            // 
            // clID
            // 
            this.clID.IsTextTrimmed = false;
            this.clID.Visible = false;
            // 
            // clName
            // 
            this.clName.Editable = false;
            this.clName.IsTextTrimmed = false;
            this.clName.Text = "ชื่อ";
            this.clName.Width = 100;
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
            this.clNickname.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clNickname.Editable = false;
            this.clNickname.IsTextTrimmed = false;
            this.clNickname.Text = "ชื่อเล่น";
            this.clNickname.Width = 65;
            // 
            // clShopName
            // 
            this.clShopName.Editable = false;
            this.clShopName.IsTextTrimmed = false;
            this.clShopName.Text = "ชื่อร้าน";
            this.clShopName.Width = 150;
            // 
            // clSellPrice
            // 
            this.clSellPrice.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clSellPrice.Editable = false;
            this.clSellPrice.IsTextTrimmed = false;
            this.clSellPrice.Text = "ราคาขาย";
            // 
            // clCredit
            // 
            this.clCredit.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clCredit.Editable = false;
            this.clCredit.IsTextTrimmed = false;
            this.clCredit.Text = "เครดิต";
            this.clCredit.Width = 60;
            // 
            // clCardNo
            // 
            this.clCardNo.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clCardNo.Editable = false;
            this.clCardNo.IsTextTrimmed = false;
            this.clCardNo.Text = "เลขที่บัตรสมาชิก";
            this.clCardNo.Width = 115;
            // 
            // clMobile
            // 
            this.clMobile.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clMobile.Editable = false;
            this.clMobile.IsTextTrimmed = false;
            this.clMobile.Text = "มือถือ";
            this.clMobile.Width = 100;
            // 
            // clCitizendId
            // 
            this.clCitizendId.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clCitizendId.Editable = false;
            this.clCitizendId.IsTextTrimmed = false;
            this.clCitizendId.Text = "เลขที่บัตรประชาชน";
            this.clCitizendId.Width = 140;
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
            this.table1.Location = new System.Drawing.Point(247, 39);
            this.table1.Name = "table1";
            this.table1.NoItemsText = "";
            this.table1.SelectionStyle = XPTable.Models.SelectionStyle.Grid;
            this.table1.Size = new System.Drawing.Size(718, 384);
            this.table1.TabIndex = 14;
            this.table1.TableModel = this.tableModel1;
            this.table1.TabStop = false;
            this.table1.Text = "table1";
            this.table1.UnfocusedBorderColor = System.Drawing.Color.Black;
            this.table1.CellDoubleClick += new XPTable.Events.CellMouseEventHandler(this.table1_CellDoubleClick);
            // 
            // UcCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.table1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "UcCustomer";
            this.Size = new System.Drawing.Size(965, 423);
            this.Load += new System.EventHandler(this.UcCustomer_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.gbOrderNo.ResumeLayout(false);
            this.gbOrderNo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.table1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnAddCustomer;
        private System.Windows.Forms.GroupBox gbOrderNo;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private XPTable.Models.TableModel tableModel1;
        private XPTable.Models.ColumnModel columnModel1;
        private XPTable.Models.Table table1;
        private XPTable.Models.TextColumn clID;
        private XPTable.Models.TextColumn clName;
        private XPTable.Models.TextColumn clLastname;
        private XPTable.Models.TextColumn clNickname;
        private XPTable.Models.TextColumn clCardNo;
        private XPTable.Models.TextColumn clMobile;
        private XPTable.Models.TextColumn clCitizendId;
        private XPTable.Models.TextColumn clCredit;
        private XPTable.Models.TextColumn clSellPrice;
        private XPTable.Models.TextColumn clShopName;
    }
}
