namespace PowerPOS_Online
{
    partial class FmReturnSellDetail
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.table1 = new XPTable.Models.Table();
            this.columnModel1 = new XPTable.Models.ColumnModel();
            this.No = new XPTable.Models.TextColumn();
            this.SellNo = new XPTable.Models.TextColumn();
            this.Customer = new XPTable.Models.TextColumn();
            this.Price = new XPTable.Models.NumberColumn();
            this.Quantity = new XPTable.Models.NumberColumn();
            this.SellPrice = new XPTable.Models.NumberColumn();
            this.tableModel1 = new XPTable.Models.TableModel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbHeader = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.SellDate = new XPTable.Models.TextColumn();
            this.cost = new XPTable.Models.NumberColumn();
            this.PriceCost = new XPTable.Models.NumberColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.table1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.table1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 58);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(799, 365);
            this.panel2.TabIndex = 3;
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
            this.table1.Location = new System.Drawing.Point(0, 0);
            this.table1.Name = "table1";
            this.table1.NoItemsText = "";
            this.table1.SelectionStyle = XPTable.Models.SelectionStyle.Grid;
            this.table1.Size = new System.Drawing.Size(799, 323);
            this.table1.TabIndex = 15;
            this.table1.TableModel = this.tableModel1;
            this.table1.Text = "table1";
            this.table1.UnfocusedBorderColor = System.Drawing.Color.Black;
            this.table1.CellDoubleClick += new XPTable.Events.CellMouseEventHandler(this.table1_CellDoubleClick);
            // 
            // columnModel1
            // 
            this.columnModel1.Columns.AddRange(new XPTable.Models.Column[] {
            this.No,
            this.SellNo,
            this.SellDate,
            this.Customer,
            this.Price,
            this.Quantity,
            this.SellPrice,
            this.cost,
            this.PriceCost});
            // 
            // No
            // 
            this.No.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.No.Editable = false;
            this.No.IsTextTrimmed = false;
            this.No.Text = "ที่";
            this.No.Width = 40;
            // 
            // SellNo
            // 
            this.SellNo.Editable = false;
            this.SellNo.IsTextTrimmed = false;
            this.SellNo.Text = "เลขที่ขาย";
            this.SellNo.Width = 100;
            // 
            // Customer
            // 
            this.Customer.Editable = false;
            this.Customer.IsTextTrimmed = false;
            this.Customer.Text = "ลูกค้า";
            this.Customer.Width = 130;
            // 
            // Price
            // 
            this.Price.Alignment = XPTable.Models.ColumnAlignment.Right;
            this.Price.Editable = false;
            this.Price.IsTextTrimmed = false;
            this.Price.Text = "ราคา";
            this.Price.Width = 100;
            // 
            // Quantity
            // 
            this.Quantity.Alignment = XPTable.Models.ColumnAlignment.Right;
            this.Quantity.Editable = false;
            this.Quantity.IsTextTrimmed = false;
            this.Quantity.Text = "จำนวน";
            this.Quantity.Width = 80;
            // 
            // SellPrice
            // 
            this.SellPrice.Alignment = XPTable.Models.ColumnAlignment.Right;
            this.SellPrice.Editable = false;
            this.SellPrice.IsTextTrimmed = false;
            this.SellPrice.Text = "ราคาขาย";
            this.SellPrice.Width = 100;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 323);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(799, 42);
            this.panel3.TabIndex = 14;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.Image = global::PowerPOS_Online.Properties.Resources.arrow_circle_225_left;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(724, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 32);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbHeader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(799, 58);
            this.panel1.TabIndex = 2;
            // 
            // gbHeader
            // 
            this.gbHeader.Controls.Add(this.label1);
            this.gbHeader.Controls.Add(this.label2);
            this.gbHeader.Controls.Add(this.lblProduct);
            this.gbHeader.Controls.Add(this.lblName);
            this.gbHeader.Location = new System.Drawing.Point(9, 3);
            this.gbHeader.Name = "gbHeader";
            this.gbHeader.Size = new System.Drawing.Size(781, 46);
            this.gbHeader.TabIndex = 8;
            this.gbHeader.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "รหัสสินค้า :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(191, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "ชื่อสินค้า :";
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblProduct.Location = new System.Drawing.Point(88, 16);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(74, 18);
            this.lblProduct.TabIndex = 3;
            this.lblProduct.Text = "lblProduct";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblName.Location = new System.Drawing.Point(260, 16);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(62, 18);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "lblName";
            // 
            // SellDate
            // 
            this.SellDate.Editable = false;
            this.SellDate.IsTextTrimmed = false;
            this.SellDate.Text = "วันที่ขาย";
            this.SellDate.Width = 150;
            // 
            // cost
            // 
            this.cost.IsTextTrimmed = false;
            this.cost.Visible = false;
            // 
            // PriceCost
            // 
            this.PriceCost.IsTextTrimmed = false;
            this.PriceCost.Visible = false;
            // 
            // FmReturnSellDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 423);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FmReturnSellDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "เลือกบิลที่ต้องการรับคืน";
            this.Load += new System.EventHandler(this.ReturnSellDetail_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.table1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.gbHeader.ResumeLayout(false);
            this.gbHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lblName;
        private XPTable.Models.Table table1;
        private XPTable.Models.ColumnModel columnModel1;
        private XPTable.Models.TableModel tableModel1;
        private XPTable.Models.TextColumn No;
        private XPTable.Models.TextColumn SellNo;
        private XPTable.Models.TextColumn Customer;
        private XPTable.Models.NumberColumn Price;
        private XPTable.Models.NumberColumn Quantity;
        private XPTable.Models.NumberColumn SellPrice;
        private XPTable.Models.TextColumn SellDate;
        private XPTable.Models.NumberColumn cost;
        private XPTable.Models.NumberColumn PriceCost;
    }
}