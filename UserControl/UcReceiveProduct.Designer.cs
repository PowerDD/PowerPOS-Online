namespace PowerPOS_Online
{
    partial class UcReceiveProduct
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
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.gbOrder = new System.Windows.Forms.GroupBox();
            this.cbbOrder = new System.Windows.Forms.ComboBox();
            this.pnlBarcode = new System.Windows.Forms.Panel();
            this.ptbProduct = new System.Windows.Forms.PictureBox();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.gbCost = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.nudCost = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnShip = new System.Windows.Forms.Panel();
            this.rdbShip = new System.Windows.Forms.RadioButton();
            this.rdbNonShip = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.gbOrderNo = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.cbbOrderNo = new System.Windows.Forms.ComboBox();
            this.table1 = new XPTable.Models.Table();
            this.columnModel1 = new XPTable.Models.ColumnModel();
            this.clNo = new XPTable.Models.TextColumn();
            this.clSku = new XPTable.Models.TextColumn();
            this.clName = new XPTable.Models.TextColumn();
            this.clQty = new XPTable.Models.TextColumn();
            this.clReceived = new XPTable.Models.TextColumn();
            this.clProgress = new XPTable.Models.ProgressBarColumn();
            this.tableModel1 = new XPTable.Models.TableModel();
            this.panel1.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.gbOrder.SuspendLayout();
            this.pnlBarcode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbProduct)).BeginInit();
            this.gbCost.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCost)).BeginInit();
            this.pnShip.SuspendLayout();
            this.gbOrderNo.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(999, 39);
            this.panel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("DilleniaUPC", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "รับสินค้าเข้าระบบ";
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.gbOrder);
            this.pnlLeft.Controls.Add(this.pnlBarcode);
            this.pnlLeft.Controls.Add(this.gbCost);
            this.pnlLeft.Controls.Add(this.gbOrderNo);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 39);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(7, 10, 7, 0);
            this.pnlLeft.Size = new System.Drawing.Size(247, 709);
            this.pnlLeft.TabIndex = 6;
            // 
            // gbOrder
            // 
            this.gbOrder.Controls.Add(this.cbbOrder);
            this.gbOrder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbOrder.Location = new System.Drawing.Point(7, 657);
            this.gbOrder.Name = "gbOrder";
            this.gbOrder.Size = new System.Drawing.Size(233, 52);
            this.gbOrder.TabIndex = 7;
            this.gbOrder.TabStop = false;
            this.gbOrder.Text = "ข้อมูลใบสั่งซื้อย้อนหลัง";
            // 
            // cbbOrder
            // 
            this.cbbOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbOrder.FormattingEnabled = true;
            this.cbbOrder.Items.AddRange(new object[] {
            "เลขที่ใบสั่งซื้อ"});
            this.cbbOrder.Location = new System.Drawing.Point(14, 20);
            this.cbbOrder.Name = "cbbOrder";
            this.cbbOrder.Size = new System.Drawing.Size(204, 24);
            this.cbbOrder.TabIndex = 0;
            this.cbbOrder.SelectedIndexChanged += new System.EventHandler(this.cbbOrder_SelectedIndexChanged);
            // 
            // pnlBarcode
            // 
            this.pnlBarcode.Controls.Add(this.ptbProduct);
            this.pnlBarcode.Controls.Add(this.txtBarcode);
            this.pnlBarcode.Controls.Add(this.label2);
            this.pnlBarcode.Controls.Add(this.lblStatus);
            this.pnlBarcode.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBarcode.Location = new System.Drawing.Point(7, 184);
            this.pnlBarcode.Name = "pnlBarcode";
            this.pnlBarcode.Size = new System.Drawing.Size(233, 353);
            this.pnlBarcode.TabIndex = 6;
            // 
            // ptbProduct
            // 
            this.ptbProduct.ImageLocation = "";
            this.ptbProduct.Location = new System.Drawing.Point(16, 77);
            this.ptbProduct.Name = "ptbProduct";
            this.ptbProduct.Size = new System.Drawing.Size(200, 200);
            this.ptbProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbProduct.TabIndex = 7;
            this.ptbProduct.TabStop = false;
            // 
            // txtBarcode
            // 
            this.txtBarcode.BackColor = System.Drawing.Color.Azure;
            this.txtBarcode.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarcode.ForeColor = System.Drawing.Color.MidnightBlue;
            this.txtBarcode.Location = new System.Drawing.Point(14, 21);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(204, 29);
            this.txtBarcode.TabIndex = 1;
            this.txtBarcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBarcode.Enter += new System.EventHandler(this.txtBarcode_Enter);
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "บาร์โค้ด";
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Green;
            this.lblStatus.Location = new System.Drawing.Point(14, 53);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(204, 23);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "ไม่พบข้อมูลสินค้าชิ้นนี้";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatus.Visible = false;
            // 
            // gbCost
            // 
            this.gbCost.Controls.Add(this.panel2);
            this.gbCost.Controls.Add(this.pnShip);
            this.gbCost.Controls.Add(this.label3);
            this.gbCost.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbCost.Location = new System.Drawing.Point(7, 89);
            this.gbCost.Name = "gbCost";
            this.gbCost.Size = new System.Drawing.Size(233, 95);
            this.gbCost.TabIndex = 5;
            this.gbCost.TabStop = false;
            this.gbCost.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.nudCost);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 52);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(227, 40);
            this.panel2.TabIndex = 10;
            // 
            // nudCost
            // 
            this.nudCost.BackColor = System.Drawing.Color.Azure;
            this.nudCost.Enabled = false;
            this.nudCost.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.nudCost.ForeColor = System.Drawing.Color.MidnightBlue;
            this.nudCost.Location = new System.Drawing.Point(3, 4);
            this.nudCost.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudCost.Name = "nudCost";
            this.nudCost.Size = new System.Drawing.Size(120, 29);
            this.nudCost.TabIndex = 6;
            this.nudCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudCost.ThousandsSeparator = true;
            // 
            // btnSave
            // 
            this.btnSave.Image = global::PowerPOS_Online.Properties.Resources.disk_return_black;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(136, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnSave.Size = new System.Drawing.Size(75, 31);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "บันทึก";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnShip
            // 
            this.pnShip.Controls.Add(this.rdbShip);
            this.pnShip.Controls.Add(this.rdbNonShip);
            this.pnShip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnShip.Location = new System.Drawing.Point(3, 29);
            this.pnShip.Name = "pnShip";
            this.pnShip.Size = new System.Drawing.Size(227, 23);
            this.pnShip.TabIndex = 9;
            // 
            // rdbShip
            // 
            this.rdbShip.AutoSize = true;
            this.rdbShip.Location = new System.Drawing.Point(14, 4);
            this.rdbShip.Name = "rdbShip";
            this.rdbShip.Size = new System.Drawing.Size(69, 17);
            this.rdbShip.TabIndex = 7;
            this.rdbShip.TabStop = true;
            this.rdbShip.Text = "มีค่าขนส่ง";
            this.rdbShip.UseVisualStyleBackColor = true;
            this.rdbShip.CheckedChanged += new System.EventHandler(this.checkRadio);
            // 
            // rdbNonShip
            // 
            this.rdbNonShip.AutoSize = true;
            this.rdbNonShip.Location = new System.Drawing.Point(123, 5);
            this.rdbNonShip.Name = "rdbNonShip";
            this.rdbNonShip.Size = new System.Drawing.Size(82, 17);
            this.rdbNonShip.TabIndex = 8;
            this.rdbNonShip.TabStop = true;
            this.rdbNonShip.Text = "ไม่มีค่าขนส่ง";
            this.rdbNonShip.UseVisualStyleBackColor = true;
            this.rdbNonShip.CheckedChanged += new System.EventHandler(this.checkRadio);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(3, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(227, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "ค่าขนส่ง, ค่าดำเนินการต่างๆ สำหรับใบสั่งซื้อนี้";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // gbOrderNo
            // 
            this.gbOrderNo.Controls.Add(this.progressBar1);
            this.gbOrderNo.Controls.Add(this.cbbOrderNo);
            this.gbOrderNo.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbOrderNo.Location = new System.Drawing.Point(7, 10);
            this.gbOrderNo.Name = "gbOrderNo";
            this.gbOrderNo.Size = new System.Drawing.Size(233, 79);
            this.gbOrderNo.TabIndex = 0;
            this.gbOrderNo.TabStop = false;
            this.gbOrderNo.Text = "ข้อมูลใบสั่งซื้อ";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(14, 49);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(204, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 9;
            // 
            // cbbOrderNo
            // 
            this.cbbOrderNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbOrderNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbOrderNo.FormattingEnabled = true;
            this.cbbOrderNo.Items.AddRange(new object[] {
            "เลขที่ใบสั่งซื้อ"});
            this.cbbOrderNo.Location = new System.Drawing.Point(14, 19);
            this.cbbOrderNo.Name = "cbbOrderNo";
            this.cbbOrderNo.Size = new System.Drawing.Size(204, 24);
            this.cbbOrderNo.TabIndex = 0;
            this.cbbOrderNo.SelectedIndexChanged += new System.EventHandler(this.cbbOrderNo_SelectedIndexChanged);
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
            this.table1.Size = new System.Drawing.Size(752, 709);
            this.table1.TabIndex = 7;
            this.table1.TableModel = this.tableModel1;
            this.table1.Text = "table1";
            this.table1.UnfocusedBorderColor = System.Drawing.Color.Black;
            this.table1.CellDoubleClick += new XPTable.Events.CellMouseEventHandler(this.table1_CellDoubleClick);
            this.table1.EndSort += new XPTable.Events.ColumnEventHandler(this.table1_EndSort);
            // 
            // columnModel1
            // 
            this.columnModel1.Columns.AddRange(new XPTable.Models.Column[] {
            this.clNo,
            this.clSku,
            this.clName,
            this.clQty,
            this.clReceived,
            this.clProgress});
            // 
            // clNo
            // 
            this.clNo.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clNo.Editable = false;
            this.clNo.IsTextTrimmed = false;
            this.clNo.Resizable = false;
            this.clNo.Selectable = false;
            this.clNo.Text = "ที่";
            this.clNo.Width = 40;
            // 
            // clSku
            // 
            this.clSku.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clSku.Editable = false;
            this.clSku.IsTextTrimmed = false;
            this.clSku.Text = "รหัสสินค้า";
            this.clSku.Width = 80;
            // 
            // clName
            // 
            this.clName.Editable = false;
            this.clName.IsTextTrimmed = false;
            this.clName.Text = "ชื่อสินค้า";
            this.clName.Width = 450;
            // 
            // clQty
            // 
            this.clQty.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clQty.Editable = false;
            this.clQty.IsTextTrimmed = false;
            this.clQty.Text = "จำนวน";
            this.clQty.Width = 70;
            // 
            // clReceived
            // 
            this.clReceived.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clReceived.Editable = false;
            this.clReceived.IsTextTrimmed = false;
            this.clReceived.Text = "รับแล้ว";
            this.clReceived.Width = 70;
            // 
            // clProgress
            // 
            this.clProgress.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clProgress.IsTextTrimmed = false;
            this.clProgress.Resizable = false;
            this.clProgress.Width = 100;
            // 
            // tableModel1
            // 
            this.tableModel1.SelectionChanged += new XPTable.Events.SelectionEventHandler(this.tableModel1_SelectionChanged);
            // 
            // UcReceiveProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.table1);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.panel1);
            this.Name = "UcReceiveProduct";
            this.Size = new System.Drawing.Size(999, 748);
            this.Load += new System.EventHandler(this.UcReceiveProduct_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.gbOrder.ResumeLayout(false);
            this.pnlBarcode.ResumeLayout(false);
            this.pnlBarcode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbProduct)).EndInit();
            this.gbCost.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudCost)).EndInit();
            this.pnShip.ResumeLayout(false);
            this.pnShip.PerformLayout();
            this.gbOrderNo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.table1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlBarcode;
        private System.Windows.Forms.PictureBox ptbProduct;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox gbCost;
        private System.Windows.Forms.NumericUpDown nudCost;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbOrderNo;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ComboBox cbbOrderNo;
        private XPTable.Models.Table table1;
        private XPTable.Models.ColumnModel columnModel1;
        private XPTable.Models.TextColumn clNo;
        private XPTable.Models.TextColumn clSku;
        private XPTable.Models.TextColumn clName;
        private XPTable.Models.TextColumn clQty;
        private XPTable.Models.TextColumn clReceived;
        private XPTable.Models.ProgressBarColumn clProgress;
        private XPTable.Models.TableModel tableModel1;
        private System.Windows.Forms.GroupBox gbOrder;
        private System.Windows.Forms.ComboBox cbbOrder;
        private System.Windows.Forms.RadioButton rdbNonShip;
        private System.Windows.Forms.RadioButton rdbShip;
        private System.Windows.Forms.Panel pnShip;
        private System.Windows.Forms.Panel panel2;
    }
}
