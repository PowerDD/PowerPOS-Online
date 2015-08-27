namespace PowerPOS_Online
{
    partial class FmClaimHistory
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.table1 = new XPTable.Models.Table();
            this.columnModel1 = new XPTable.Models.ColumnModel();
            this.clNo = new XPTable.Models.TextColumn();
            this.clClaimNo = new XPTable.Models.TextColumn();
            this.clClaimDate = new XPTable.Models.TextColumn();
            this.clBarcode = new XPTable.Models.TextColumn();
            this.clBarcodeClaim = new XPTable.Models.TextColumn();
            this.clPriceClaim = new XPTable.Models.NumberColumn();
            this.clOffice = new XPTable.Models.ImageColumn();
            this.tableModel1 = new XPTable.Models.TableModel();
            this.bwSearch = new System.ComponentModel.BackgroundWorker();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.table1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.table1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(790, 344);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 301);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(790, 43);
            this.panel3.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.Image = global::PowerPOS_Online.Properties.Resources.arrow_circle_225_left;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(704, 5);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancel.Size = new System.Drawing.Size(76, 32);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.table1.Location = new System.Drawing.Point(0, 0);
            this.table1.Name = "table1";
            this.table1.NoItemsText = "";
            this.table1.SelectionStyle = XPTable.Models.SelectionStyle.Grid;
            this.table1.Size = new System.Drawing.Size(790, 301);
            this.table1.TabIndex = 15;
            this.table1.TableModel = this.tableModel1;
            this.table1.Text = "table1";
            this.table1.UnfocusedBorderColor = System.Drawing.Color.Black;
            // 
            // columnModel1
            // 
            this.columnModel1.Columns.AddRange(new XPTable.Models.Column[] {
            this.clNo,
            this.clClaimNo,
            this.clClaimDate,
            this.clBarcode,
            this.clBarcodeClaim,
            this.clPriceClaim,
            this.clOffice});
            // 
            // clNo
            // 
            this.clNo.IsTextTrimmed = false;
            this.clNo.Text = "ที่";
            this.clNo.Width = 30;
            // 
            // clClaimNo
            // 
            this.clClaimNo.IsTextTrimmed = false;
            this.clClaimNo.Text = "เลขที่การเคลม";
            this.clClaimNo.Width = 100;
            // 
            // clClaimDate
            // 
            this.clClaimDate.IsTextTrimmed = false;
            this.clClaimDate.Text = "วันที่เคลมสินค้า";
            this.clClaimDate.Width = 170;
            // 
            // clBarcode
            // 
            this.clBarcode.IsTextTrimmed = false;
            this.clBarcode.Text = "บาร์โค้ด";
            this.clBarcode.Width = 150;
            // 
            // clBarcodeClaim
            // 
            this.clBarcodeClaim.IsTextTrimmed = false;
            this.clBarcodeClaim.Text = "บาร์โค้ดเคลม";
            this.clBarcodeClaim.Width = 150;
            // 
            // clPriceClaim
            // 
            this.clPriceClaim.Alignment = XPTable.Models.ColumnAlignment.Right;
            this.clPriceClaim.IsTextTrimmed = false;
            this.clPriceClaim.Text = "คืนเงิน";
            this.clPriceClaim.Width = 80;
            // 
            // clOffice
            // 
            this.clOffice.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clOffice.DrawText = false;
            this.clOffice.ImageOnRight = true;
            this.clOffice.IsTextTrimmed = false;
            this.clOffice.Text = "ส่งคืนสำนักงาน";
            this.clOffice.Width = 100;
            // 
            // bwSearch
            // 
            this.bwSearch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwSearch_DoWork);
            this.bwSearch.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwSearch_RunWorkerCompleted);
            // 
            // FmClaimHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 344);
            this.Controls.Add(this.panel2);
            this.Name = "FmClaimHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ประวัติการเคลมสินค้า";
            this.Load += new System.EventHandler(this.FmClaimHistory_Load);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.table1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private XPTable.Models.ColumnModel columnModel1;
        private XPTable.Models.TextColumn clNo;
        private XPTable.Models.TextColumn clClaimDate;
        private XPTable.Models.TextColumn clBarcode;
        private XPTable.Models.TextColumn clBarcodeClaim;
        private XPTable.Models.TableModel tableModel1;
        private XPTable.Models.Table table1;
        private XPTable.Models.TextColumn clClaimNo;
        private System.Windows.Forms.Panel panel3;
        private System.ComponentModel.BackgroundWorker bwSearch;
        private XPTable.Models.NumberColumn clPriceClaim;
        public System.Windows.Forms.Button btnCancel;
        private XPTable.Models.ImageColumn clOffice;
    }
}