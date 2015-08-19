namespace PowerPOS_Online
{
    partial class UcClaim
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ptbProduct = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnClaim = new System.Windows.Forms.Button();
            this.lblWarrantyStatus = new System.Windows.Forms.Label();
            this.lblWarranty = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.pnlBarcode = new System.Windows.Forms.Panel();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.bwSearch = new System.ComponentModel.BackgroundWorker();
            this.table1 = new XPTable.Models.Table();
            this.columnModel1 = new XPTable.Models.ColumnModel();
            this.clNo = new XPTable.Models.TextColumn();
            this.clSellDate = new XPTable.Models.TextColumn();
            this.clShop = new XPTable.Models.TextColumn();
            this.clReceivedDate = new XPTable.Models.TextColumn();
            this.clCustomer = new XPTable.Models.TextColumn();
            this.tableModel1 = new XPTable.Models.TableModel();
            this.bwGetProduct = new System.ComponentModel.BackgroundWorker();
            this.bwDownloadImage = new System.ComponentModel.BackgroundWorker();
            this.bwGetShopName = new System.ComponentModel.BackgroundWorker();
            this.bwGetCustomerName = new System.ComponentModel.BackgroundWorker();
            this.btnReceivedClaim = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.clBarcodeClaim = new XPTable.Models.TextColumn();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbProduct)).BeginInit();
            this.pnlBarcode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.table1)).BeginInit();
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
            this.panel1.Size = new System.Drawing.Size(953, 39);
            this.panel1.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("DilleniaUPC", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "เคลมสินค้า";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.pnlBarcode);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 39);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(7, 7, 7, 0);
            this.panel3.Size = new System.Drawing.Size(247, 470);
            this.panel3.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ptbProduct);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.btnClaim);
            this.panel2.Controls.Add(this.lblWarrantyStatus);
            this.panel2.Controls.Add(this.lblWarranty);
            this.panel2.Controls.Add(this.lblName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.panel2.Location = new System.Drawing.Point(7, 89);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(233, 325);
            this.panel2.TabIndex = 9;
            // 
            // ptbProduct
            // 
            this.ptbProduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.ptbProduct.Location = new System.Drawing.Point(0, 132);
            this.ptbProduct.Name = "ptbProduct";
            this.ptbProduct.Size = new System.Drawing.Size(233, 230);
            this.ptbProduct.TabIndex = 3;
            this.ptbProduct.TabStop = false;
            this.ptbProduct.Visible = false;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 127);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(233, 5);
            this.panel4.TabIndex = 5;
            // 
            // btnClaim
            // 
            this.btnClaim.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnClaim.Image = global::PowerPOS_Online.Properties.Resources.wrench_screwdriver;
            this.btnClaim.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClaim.Location = new System.Drawing.Point(0, 94);
            this.btnClaim.Name = "btnClaim";
            this.btnClaim.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.btnClaim.Size = new System.Drawing.Size(233, 33);
            this.btnClaim.TabIndex = 4;
            this.btnClaim.Text = "เคลมสินค้า";
            this.btnClaim.UseVisualStyleBackColor = true;
            this.btnClaim.Visible = false;
            this.btnClaim.Click += new System.EventHandler(this.btnClaim_Click);
            // 
            // lblWarrantyStatus
            // 
            this.lblWarrantyStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblWarrantyStatus.Font = new System.Drawing.Font("DilleniaUPC", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblWarrantyStatus.ForeColor = System.Drawing.Color.Red;
            this.lblWarrantyStatus.Location = new System.Drawing.Point(0, 58);
            this.lblWarrantyStatus.Name = "lblWarrantyStatus";
            this.lblWarrantyStatus.Size = new System.Drawing.Size(233, 36);
            this.lblWarrantyStatus.TabIndex = 2;
            this.lblWarrantyStatus.Text = "หมดประกันแล้ว";
            this.lblWarrantyStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWarrantyStatus.Visible = false;
            // 
            // lblWarranty
            // 
            this.lblWarranty.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblWarranty.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblWarranty.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblWarranty.Location = new System.Drawing.Point(0, 38);
            this.lblWarranty.Name = "lblWarranty";
            this.lblWarranty.Size = new System.Drawing.Size(233, 20);
            this.lblWarranty.TabIndex = 1;
            this.lblWarranty.Text = "ประกัน 365 วัน (เหลือประกันอีก 5 วัน)";
            this.lblWarranty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWarranty.Visible = false;
            // 
            // lblName
            // 
            this.lblName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(233, 38);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Cable Charger for iPhone5/5s/6/6 PLUS (SCALE) Yellow - REMAX";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblName.Visible = false;
            // 
            // pnlBarcode
            // 
            this.pnlBarcode.Controls.Add(this.txtBarcode);
            this.pnlBarcode.Controls.Add(this.label2);
            this.pnlBarcode.Controls.Add(this.lblStatus);
            this.pnlBarcode.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBarcode.Location = new System.Drawing.Point(7, 7);
            this.pnlBarcode.Name = "pnlBarcode";
            this.pnlBarcode.Size = new System.Drawing.Size(233, 82);
            this.pnlBarcode.TabIndex = 8;
            // 
            // txtBarcode
            // 
            this.txtBarcode.BackColor = System.Drawing.Color.Azure;
            this.txtBarcode.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarcode.ForeColor = System.Drawing.Color.MidnightBlue;
            this.txtBarcode.Location = new System.Drawing.Point(0, 24);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(233, 29);
            this.txtBarcode.TabIndex = 1;
            this.txtBarcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "บาร์โค้ด";
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Green;
            this.lblStatus.Location = new System.Drawing.Point(14, 56);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(204, 23);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "ไม่พบข้อมูลสินค้าชิ้นนี้";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatus.Visible = false;
            // 
            // bwSearch
            // 
            this.bwSearch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwSearch_DoWork);
            this.bwSearch.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwSearch_RunWorkerCompleted);
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
            this.table1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.table1.Location = new System.Drawing.Point(247, 39);
            this.table1.Name = "table1";
            this.table1.NoItemsText = "";
            this.table1.SelectionStyle = XPTable.Models.SelectionStyle.Grid;
            this.table1.Size = new System.Drawing.Size(706, 470);
            this.table1.TabIndex = 14;
            this.table1.TableModel = this.tableModel1;
            this.table1.Text = "table1";
            this.table1.UnfocusedBorderColor = System.Drawing.Color.Black;
            // 
            // columnModel1
            // 
            this.columnModel1.Columns.AddRange(new XPTable.Models.Column[] {
            this.clNo,
            this.clSellDate,
            this.clShop,
            this.clReceivedDate,
            this.clCustomer});
            // 
            // clNo
            // 
            this.clNo.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clNo.Editable = false;
            this.clNo.IsTextTrimmed = false;
            this.clNo.Text = "ที่";
            this.clNo.Width = 40;
            // 
            // clSellDate
            // 
            this.clSellDate.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clSellDate.Editable = false;
            this.clSellDate.IsTextTrimmed = false;
            this.clSellDate.Text = "วันที่ขายสินค้า";
            this.clSellDate.Width = 180;
            // 
            // clShop
            // 
            this.clShop.Editable = false;
            this.clShop.IsTextTrimmed = false;
            this.clShop.Text = "ชื่อร้าน";
            this.clShop.Width = 240;
            // 
            // clReceivedDate
            // 
            this.clReceivedDate.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clReceivedDate.Editable = false;
            this.clReceivedDate.IsTextTrimmed = false;
            this.clReceivedDate.Text = "วันที่รับสินค้าเข้า";
            this.clReceivedDate.Width = 180;
            // 
            // clCustomer
            // 
            this.clCustomer.Editable = false;
            this.clCustomer.IsTextTrimmed = false;
            this.clCustomer.Text = "ชื่อลูกค้า";
            this.clCustomer.Width = 200;
            // 
            // bwGetProduct
            // 
            this.bwGetProduct.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwGetProduct_DoWork);
            this.bwGetProduct.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwGetProduct_RunWorkerCompleted);
            // 
            // bwDownloadImage
            // 
            this.bwDownloadImage.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwDownloadImage_DoWork);
            // 
            // bwGetShopName
            // 
            this.bwGetShopName.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwGetShopName_DoWork);
            this.bwGetShopName.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwGetShopName_RunWorkerCompleted);
            // 
            // bwGetCustomerName
            // 
            this.bwGetCustomerName.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwGetCustomerName_DoWork);
            this.bwGetCustomerName.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwGetCustomerName_RunWorkerCompleted);
            // 
            // btnReceivedClaim
            // 
            this.btnReceivedClaim.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnReceivedClaim.Location = new System.Drawing.Point(1, 5);
            this.btnReceivedClaim.Name = "btnReceivedClaim";
            this.btnReceivedClaim.Size = new System.Drawing.Size(233, 33);
            this.btnReceivedClaim.TabIndex = 6;
            this.btnReceivedClaim.Text = "รับเข้าสินค้าเคลม";
            this.btnReceivedClaim.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnReceivedClaim);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(7, 427);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(233, 43);
            this.panel5.TabIndex = 10;
            // 
            // clBarcodeClaim
            // 
            this.clBarcodeClaim.IsTextTrimmed = false;
            this.clBarcodeClaim.Text = "เปลี่ยนสินค้าจาก";
            // 
            // UcClaim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.table1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "UcClaim";
            this.Size = new System.Drawing.Size(953, 509);
            this.Load += new System.EventHandler(this.UcClaim_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ptbProduct)).EndInit();
            this.pnlBarcode.ResumeLayout(false);
            this.pnlBarcode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.table1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlBarcode;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblStatus;
        private System.ComponentModel.BackgroundWorker bwSearch;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblName;
        private XPTable.Models.Table table1;
        private XPTable.Models.ColumnModel columnModel1;
        private XPTable.Models.TableModel tableModel1;
        private XPTable.Models.TextColumn clNo;
        private XPTable.Models.TextColumn clShop;
        private XPTable.Models.TextColumn clCustomer;
        private System.Windows.Forms.PictureBox ptbProduct;
        private System.Windows.Forms.Label lblWarrantyStatus;
        private System.Windows.Forms.Label lblWarranty;
        private System.Windows.Forms.Button btnClaim;
        private System.Windows.Forms.Panel panel4;
        private System.ComponentModel.BackgroundWorker bwGetProduct;
        private System.ComponentModel.BackgroundWorker bwDownloadImage;
        private System.ComponentModel.BackgroundWorker bwGetShopName;
        private System.ComponentModel.BackgroundWorker bwGetCustomerName;
        private XPTable.Models.TextColumn clSellDate;
        private XPTable.Models.TextColumn clReceivedDate;
        private System.Windows.Forms.Button btnReceivedClaim;
        private System.Windows.Forms.Panel panel5;
        private XPTable.Models.TextColumn clBarcodeClaim;
    }
}
