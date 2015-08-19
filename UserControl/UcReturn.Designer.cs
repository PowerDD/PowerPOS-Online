namespace PowerPOS_Online
{
    partial class UcReturn
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
            this.bwSearch = new System.ComponentModel.BackgroundWorker();
            this.columnModel1 = new XPTable.Models.ColumnModel();
            this.cNo = new XPTable.Models.TextColumn();
            this.cSellDate = new XPTable.Models.TextColumn();
            this.cCustomerName = new XPTable.Models.TextColumn();
            this.cSellPrice = new XPTable.Models.TextColumn();
            this.tableModel1 = new XPTable.Models.TableModel();
            this.bwGetProduct = new System.ComponentModel.BackgroundWorker();
            this.bwDownloadImage = new System.ComponentModel.BackgroundWorker();
            this.bwGetShopName = new System.ComponentModel.BackgroundWorker();
            this.bwGetCustomerName = new System.ComponentModel.BackgroundWorker();
            this.btnReturn = new System.Windows.Forms.Button();
            this.clCustomer = new XPTable.Models.TextColumn();
            this.clReceivedDate = new XPTable.Models.TextColumn();
            this.clShop = new XPTable.Models.TextColumn();
            this.clSellDate = new XPTable.Models.TextColumn();
            this.clNo = new XPTable.Models.TextColumn();
            this.ptbProduct = new System.Windows.Forms.PictureBox();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblWarrantyStatus = new System.Windows.Forms.Label();
            this.lblWarranty = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlBarcode = new System.Windows.Forms.Panel();
            this.table1 = new XPTable.Models.Table();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbProduct)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlBarcode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.table1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(970, 39);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("DilleniaUPC", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 38);
            this.label1.TabIndex = 1;
            this.label1.Text = "รับคืนสินค้า";
            // 
            // bwSearch
            // 
            this.bwSearch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwSearch_DoWork);
            this.bwSearch.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwSearch_RunWorkerCompleted);
            // 
            // columnModel1
            // 
            this.columnModel1.Columns.AddRange(new XPTable.Models.Column[] {
            this.cNo,
            this.cSellDate,
            this.cCustomerName,
            this.cSellPrice});
            // 
            // cNo
            // 
            this.cNo.IsTextTrimmed = false;
            this.cNo.Text = "ที่";
            this.cNo.Width = 40;
            // 
            // cSellDate
            // 
            this.cSellDate.IsTextTrimmed = false;
            this.cSellDate.Text = "วันที่ขายสินค้า";
            this.cSellDate.Width = 150;
            // 
            // cCustomerName
            // 
            this.cCustomerName.IsTextTrimmed = false;
            this.cCustomerName.Text = "ชื่อลูกค้า";
            this.cCustomerName.Width = 250;
            // 
            // cSellPrice
            // 
            this.cSellPrice.Alignment = XPTable.Models.ColumnAlignment.Right;
            this.cSellPrice.IsTextTrimmed = false;
            this.cSellPrice.Text = "ราคาขาย";
            this.cSellPrice.Width = 100;
            // 
            // bwGetProduct
            // 
            this.bwGetProduct.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwGetProduct_DoWork);
            this.bwGetProduct.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwGetProduct_RunWorkerCompleted);
            // 
            // bwGetCustomerName
            // 
            this.bwGetCustomerName.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwGetCustomerName_DoWork);
            this.bwGetCustomerName.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwGetCustomerName_RunWorkerCompleted);
            // 
            // btnReturn
            // 
            this.btnReturn.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReturn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReturn.Location = new System.Drawing.Point(0, 94);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.btnReturn.Size = new System.Drawing.Size(233, 33);
            this.btnReturn.TabIndex = 4;
            this.btnReturn.Text = "รับคืนสินค้า";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Visible = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // clCustomer
            // 
            this.clCustomer.Editable = false;
            this.clCustomer.IsTextTrimmed = false;
            this.clCustomer.Text = "ชื่อลูกค้า";
            this.clCustomer.Width = 200;
            // 
            // clReceivedDate
            // 
            this.clReceivedDate.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clReceivedDate.Editable = false;
            this.clReceivedDate.IsTextTrimmed = false;
            this.clReceivedDate.Text = "วันที่รับสินค้าเข้า";
            this.clReceivedDate.Width = 180;
            // 
            // clShop
            // 
            this.clShop.Editable = false;
            this.clShop.IsTextTrimmed = false;
            this.clShop.Text = "ชื่อร้าน";
            this.clShop.Width = 240;
            // 
            // clSellDate
            // 
            this.clSellDate.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clSellDate.Editable = false;
            this.clSellDate.IsTextTrimmed = false;
            this.clSellDate.Text = "วันที่ขายสินค้า";
            this.clSellDate.Width = 180;
            // 
            // clNo
            // 
            this.clNo.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clNo.Editable = false;
            this.clNo.IsTextTrimmed = false;
            this.clNo.Text = "ที่";
            this.clNo.Width = 40;
            // 
            // ptbProduct
            // 
            this.ptbProduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.ptbProduct.Location = new System.Drawing.Point(0, 132);
            this.ptbProduct.Name = "ptbProduct";
            this.ptbProduct.Size = new System.Drawing.Size(233, 233);
            this.ptbProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbProduct.TabIndex = 3;
            this.ptbProduct.TabStop = false;
            this.ptbProduct.Visible = false;
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
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 127);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(233, 5);
            this.panel4.TabIndex = 5;
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
            this.lblWarranty.Text = "รับคืนภายใน 7 วัน (เหลืออีก 5 วัน)";
            this.lblWarranty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWarranty.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ptbProduct);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.btnReturn);
            this.panel2.Controls.Add(this.lblWarrantyStatus);
            this.panel2.Controls.Add(this.lblWarranty);
            this.panel2.Controls.Add(this.lblName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.panel2.Location = new System.Drawing.Point(7, 89);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(233, 378);
            this.panel2.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.pnlBarcode);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 39);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(7, 7, 7, 0);
            this.panel3.Size = new System.Drawing.Size(247, 530);
            this.panel3.TabIndex = 15;
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
            this.table1.Size = new System.Drawing.Size(723, 530);
            this.table1.TabIndex = 17;
            this.table1.TableModel = this.tableModel1;
            this.table1.Text = "table1";
            this.table1.UnfocusedBorderColor = System.Drawing.Color.Black;
            // 
            // UcReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.table1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "UcReturn";
            this.Size = new System.Drawing.Size(970, 569);
            this.Load += new System.EventHandler(this.UcReturn_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbProduct)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pnlBarcode.ResumeLayout(false);
            this.pnlBarcode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.table1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker bwSearch;
        private XPTable.Models.ColumnModel columnModel1;
        private XPTable.Models.TableModel tableModel1;
        private System.ComponentModel.BackgroundWorker bwGetProduct;
        private System.ComponentModel.BackgroundWorker bwDownloadImage;
        private System.ComponentModel.BackgroundWorker bwGetShopName;
        private System.ComponentModel.BackgroundWorker bwGetCustomerName;
        private System.Windows.Forms.Button btnReturn;
        private XPTable.Models.TextColumn clCustomer;
        private XPTable.Models.TextColumn clReceivedDate;
        private XPTable.Models.TextColumn clShop;
        private XPTable.Models.TextColumn clSellDate;
        private XPTable.Models.TextColumn clNo;
        private System.Windows.Forms.PictureBox ptbProduct;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblWarrantyStatus;
        private System.Windows.Forms.Label lblWarranty;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlBarcode;
        private XPTable.Models.TextColumn cNo;
        private XPTable.Models.TextColumn cSellDate;
        private XPTable.Models.TextColumn cCustomerName;
        private XPTable.Models.Table table1;
        private XPTable.Models.TextColumn cSellPrice;
    }
}
