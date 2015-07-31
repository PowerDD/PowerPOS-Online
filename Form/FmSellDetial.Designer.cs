namespace PowerPOS_Online
{
    partial class FmSellDetial
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
            this.lblTotal = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.lblSellDate = new System.Windows.Forms.Label();
            this.lblSellNo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.table1 = new XPTable.Models.Table();
            this.columnModel1 = new XPTable.Models.ColumnModel();
            this.No = new XPTable.Models.TextColumn();
            this.ProductCode = new XPTable.Models.TextColumn();
            this.ProductName = new XPTable.Models.TextColumn();
            this.Price = new XPTable.Models.NumberColumn();
            this.Quantity = new XPTable.Models.NumberColumn();
            this.Total = new XPTable.Models.NumberColumn();
            this.tableModel1 = new XPTable.Models.TableModel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.table1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTotal);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblCustomer);
            this.panel1.Controls.Add(this.lblSellDate);
            this.panel1.Controls.Add(this.lblSellNo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(619, 71);
            this.panel1.TabIndex = 0;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblTotal.Location = new System.Drawing.Point(380, 37);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(55, 18);
            this.lblTotal.TabIndex = 7;
            this.lblTotal.Text = "lblTotal";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(311, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 18);
            this.label5.TabIndex = 6;
            this.label5.Text = "ยอดรวม :";
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblCustomer.Location = new System.Drawing.Point(63, 37);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(88, 18);
            this.lblCustomer.TabIndex = 5;
            this.lblCustomer.Text = "lblCustomer";
            // 
            // lblSellDate
            // 
            this.lblSellDate.AutoSize = true;
            this.lblSellDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblSellDate.Location = new System.Drawing.Point(380, 13);
            this.lblSellDate.Name = "lblSellDate";
            this.lblSellDate.Size = new System.Drawing.Size(77, 18);
            this.lblSellDate.TabIndex = 4;
            this.lblSellDate.Text = "lblSellDate";
            // 
            // lblSellNo
            // 
            this.lblSellNo.AutoSize = true;
            this.lblSellNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblSellNo.Location = new System.Drawing.Point(98, 13);
            this.lblSellNo.Name = "lblSellNo";
            this.lblSellNo.Size = new System.Drawing.Size(66, 18);
            this.lblSellNo.TabIndex = 3;
            this.lblSellNo.Text = "lblSellNo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(12, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "ลูกค้า :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(311, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "วันที่ขาย :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "เลขที่การขาย :";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.table1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 71);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(619, 323);
            this.panel2.TabIndex = 1;
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
            this.table1.Location = new System.Drawing.Point(0, 0);
            this.table1.Name = "table1";
            this.table1.NoItemsText = "";
            this.table1.SelectionStyle = XPTable.Models.SelectionStyle.Grid;
            this.table1.Size = new System.Drawing.Size(619, 323);
            this.table1.TabIndex = 13;
            this.table1.TableModel = this.tableModel1;
            this.table1.Text = "table1";
            this.table1.UnfocusedBorderColor = System.Drawing.Color.Black;
            // 
            // columnModel1
            // 
            this.columnModel1.Columns.AddRange(new XPTable.Models.Column[] {
            this.No,
            this.ProductCode,
            this.ProductName,
            this.Price,
            this.Quantity,
            this.Total});
            // 
            // No
            // 
            this.No.IsTextTrimmed = false;
            this.No.Text = "ที่";
            this.No.Width = 40;
            // 
            // ProductCode
            // 
            this.ProductCode.IsTextTrimmed = false;
            this.ProductCode.Text = "รหัสสินค้า";
            this.ProductCode.Width = 90;
            // 
            // ProductName
            // 
            this.ProductName.IsTextTrimmed = false;
            this.ProductName.Text = "ชื่อสินค้า";
            this.ProductName.Width = 250;
            // 
            // Price
            // 
            this.Price.Alignment = XPTable.Models.ColumnAlignment.Right;
            this.Price.IsTextTrimmed = false;
            this.Price.Text = "ราคา";
            // 
            // Quantity
            // 
            this.Quantity.Alignment = XPTable.Models.ColumnAlignment.Right;
            this.Quantity.IsTextTrimmed = false;
            this.Quantity.Text = "จำนวน";
            // 
            // Total
            // 
            this.Total.Alignment = XPTable.Models.ColumnAlignment.Right;
            this.Total.IsTextTrimmed = false;
            this.Total.Text = "รวม";
            // 
            // FmSellDetial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 394);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FmSellDetial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "รายละเอียดการขาย";
            this.Load += new System.EventHandler(this.FmSellDetial_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.table1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private XPTable.Models.ColumnModel columnModel1;
        private XPTable.Models.TableModel tableModel1;
        private XPTable.Models.Table table1;
        private XPTable.Models.TextColumn No;
        private XPTable.Models.TextColumn ProductCode;
        private XPTable.Models.TextColumn ProductName;
        private XPTable.Models.NumberColumn Price;
        private XPTable.Models.NumberColumn Quantity;
        private XPTable.Models.NumberColumn Total;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Label lblSellDate;
        private System.Windows.Forms.Label lblSellNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label5;
    }
}