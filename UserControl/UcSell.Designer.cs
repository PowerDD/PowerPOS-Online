namespace PowerPOS_Online
{
    partial class UcSell
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
            this.lblStatus = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblPrice = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.btnAge6 = new System.Windows.Forms.Button();
            this.clNo = new XPTable.Models.TextColumn();
            this.clSku = new XPTable.Models.TextColumn();
            this.clName = new XPTable.Models.TextColumn();
            this.btnAge5 = new System.Windows.Forms.Button();
            this.btnAge4 = new System.Windows.Forms.Button();
            this.btnAge3 = new System.Windows.Forms.Button();
            this.btnAge2 = new System.Windows.Forms.Button();
            this.btnAge1 = new System.Windows.Forms.Button();
            this.btnWoman = new System.Windows.Forms.Button();
            this.btnMan = new System.Windows.Forms.Button();
            this.clPrice = new XPTable.Models.NumberColumn();
            this.clQty = new XPTable.Models.NumberColumn();
            this.clTotal = new XPTable.Models.NumberColumn();
            this.columnModel1 = new XPTable.Models.ColumnModel();
            this.table1 = new XPTable.Models.Table();
            this.tableModel1 = new XPTable.Models.TableModel();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.pnlBarcode = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnCancelProduct = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.table1)).BeginInit();
            this.pnlBarcode.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.lblPrice);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(233, 64);
            this.panel4.TabIndex = 0;
            // 
            // lblPrice
            // 
            this.lblPrice.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblPrice.Font = new System.Drawing.Font("Arial", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.ForeColor = System.Drawing.Color.Lime;
            this.lblPrice.Location = new System.Drawing.Point(0, 16);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(229, 44);
            this.lblPrice.TabIndex = 1;
            this.lblPrice.Text = "9,999,999";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Lime;
            this.label4.Location = new System.Drawing.Point(155, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "หน่วย = บาท";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Lime;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "ราคา";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(7, 7);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(233, 67);
            this.panel2.TabIndex = 7;
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblCustomerName.Location = new System.Drawing.Point(47, 26);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(0, 19);
            this.lblCustomerName.TabIndex = 7;
            // 
            // btnAge6
            // 
            this.btnAge6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnAge6.Location = new System.Drawing.Point(142, 153);
            this.btnAge6.Name = "btnAge6";
            this.btnAge6.Size = new System.Drawing.Size(85, 26);
            this.btnAge6.TabIndex = 10;
            this.btnAge6.Text = "61 ปี ขึ้นไป";
            this.btnAge6.UseVisualStyleBackColor = true;
            this.btnAge6.Click += new System.EventHandler(this.btnAge6_Click);
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
            // clSku
            // 
            this.clSku.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clSku.Editable = false;
            this.clSku.IsTextTrimmed = false;
            this.clSku.Resizable = false;
            this.clSku.Sortable = false;
            this.clSku.Text = "รหัสสินค้า";
            this.clSku.Width = 80;
            // 
            // clName
            // 
            this.clName.Editable = false;
            this.clName.IsTextTrimmed = false;
            this.clName.Sortable = false;
            this.clName.Text = "ชื่อสินค้า";
            this.clName.Width = 400;
            // 
            // btnAge5
            // 
            this.btnAge5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnAge5.Location = new System.Drawing.Point(142, 121);
            this.btnAge5.Name = "btnAge5";
            this.btnAge5.Size = new System.Drawing.Size(85, 26);
            this.btnAge5.TabIndex = 9;
            this.btnAge5.Text = "41-60 ปี";
            this.btnAge5.UseVisualStyleBackColor = true;
            this.btnAge5.Click += new System.EventHandler(this.btnAge5_Click);
            // 
            // btnAge4
            // 
            this.btnAge4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnAge4.Location = new System.Drawing.Point(142, 89);
            this.btnAge4.Name = "btnAge4";
            this.btnAge4.Size = new System.Drawing.Size(85, 26);
            this.btnAge4.TabIndex = 8;
            this.btnAge4.Text = "26-40 ปี";
            this.btnAge4.UseVisualStyleBackColor = true;
            this.btnAge4.Click += new System.EventHandler(this.btnAge4_Click);
            // 
            // btnAge3
            // 
            this.btnAge3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnAge3.Location = new System.Drawing.Point(51, 153);
            this.btnAge3.Name = "btnAge3";
            this.btnAge3.Size = new System.Drawing.Size(85, 26);
            this.btnAge3.TabIndex = 7;
            this.btnAge3.Text = "19-25 ปี";
            this.btnAge3.UseVisualStyleBackColor = true;
            this.btnAge3.Click += new System.EventHandler(this.btnAge3_Click);
            // 
            // btnAge2
            // 
            this.btnAge2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnAge2.Location = new System.Drawing.Point(51, 121);
            this.btnAge2.Name = "btnAge2";
            this.btnAge2.Size = new System.Drawing.Size(85, 26);
            this.btnAge2.TabIndex = 6;
            this.btnAge2.Text = "13-18 ปี";
            this.btnAge2.UseVisualStyleBackColor = true;
            this.btnAge2.Click += new System.EventHandler(this.btnAge2_Click);
            // 
            // btnAge1
            // 
            this.btnAge1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnAge1.Location = new System.Drawing.Point(51, 89);
            this.btnAge1.Name = "btnAge1";
            this.btnAge1.Size = new System.Drawing.Size(85, 26);
            this.btnAge1.TabIndex = 5;
            this.btnAge1.Text = "1-12 ปี";
            this.btnAge1.UseVisualStyleBackColor = true;
            this.btnAge1.Click += new System.EventHandler(this.btnAge1_Click);
            // 
            // btnWoman
            // 
            this.btnWoman.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnWoman.Location = new System.Drawing.Point(142, 57);
            this.btnWoman.Name = "btnWoman";
            this.btnWoman.Size = new System.Drawing.Size(85, 26);
            this.btnWoman.TabIndex = 4;
            this.btnWoman.Text = "หญิง";
            this.btnWoman.UseVisualStyleBackColor = true;
            this.btnWoman.Click += new System.EventHandler(this.btnWoman_Click);
            // 
            // btnMan
            // 
            this.btnMan.BackColor = System.Drawing.Color.SteelBlue;
            this.btnMan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnMan.ForeColor = System.Drawing.Color.White;
            this.btnMan.Location = new System.Drawing.Point(51, 57);
            this.btnMan.Name = "btnMan";
            this.btnMan.Size = new System.Drawing.Size(85, 26);
            this.btnMan.TabIndex = 3;
            this.btnMan.Text = "ชาย";
            this.btnMan.UseVisualStyleBackColor = false;
            this.btnMan.Click += new System.EventHandler(this.btnMan_Click);
            // 
            // clPrice
            // 
            this.clPrice.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clPrice.Editable = false;
            this.clPrice.Format = "#,###";
            this.clPrice.IsTextTrimmed = false;
            this.clPrice.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.clPrice.Sortable = false;
            this.clPrice.Text = "ราคา";
            // 
            // clQty
            // 
            this.clQty.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clQty.Editable = false;
            this.clQty.Format = "#,###";
            this.clQty.IsTextTrimmed = false;
            this.clQty.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.clQty.Sortable = false;
            this.clQty.Text = "จำนวน";
            // 
            // clTotal
            // 
            this.clTotal.Alignment = XPTable.Models.ColumnAlignment.Right;
            this.clTotal.Editable = false;
            this.clTotal.Format = "#,###";
            this.clTotal.IsTextTrimmed = false;
            this.clTotal.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.clTotal.Sortable = false;
            this.clTotal.Text = "รวม";
            // 
            // columnModel1
            // 
            this.columnModel1.Columns.AddRange(new XPTable.Models.Column[] {
            this.clNo,
            this.clSku,
            this.clName,
            this.clPrice,
            this.clQty,
            this.clTotal});
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
            this.table1.Location = new System.Drawing.Point(247, 39);
            this.table1.Name = "table1";
            this.table1.NoItemsText = "";
            this.table1.SelectionStyle = XPTable.Models.SelectionStyle.Grid;
            this.table1.Size = new System.Drawing.Size(555, 478);
            this.table1.TabIndex = 12;
            this.table1.TableModel = this.tableModel1;
            this.table1.TabStop = false;
            this.table1.Text = "table1";
            this.table1.UnfocusedBorderColor = System.Drawing.Color.Black;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label8.Location = new System.Drawing.Point(15, 94);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 16);
            this.label8.TabIndex = 2;
            this.label8.Text = "อายุ :";
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
            this.txtBarcode.Click += new System.EventHandler(this.txtBarcode_Enter);
            this.txtBarcode.Enter += new System.EventHandler(this.txtBarcode_Enter);
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            // 
            // pnlBarcode
            // 
            this.pnlBarcode.Controls.Add(this.txtBarcode);
            this.pnlBarcode.Controls.Add(this.label2);
            this.pnlBarcode.Controls.Add(this.lblStatus);
            this.pnlBarcode.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBarcode.Location = new System.Drawing.Point(7, 74);
            this.pnlBarcode.Name = "pnlBarcode";
            this.pnlBarcode.Size = new System.Drawing.Size(233, 82);
            this.pnlBarcode.TabIndex = 7;
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.Location = new System.Drawing.Point(16, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 16);
            this.label7.TabIndex = 1;
            this.label7.Text = "ชื่อ : ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label6.Location = new System.Drawing.Point(14, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "เพศ :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.lblCustomerName);
            this.groupBox1.Controls.Add(this.btnAge6);
            this.groupBox1.Controls.Add(this.btnAge5);
            this.groupBox1.Controls.Add(this.btnAge4);
            this.groupBox1.Controls.Add(this.btnAge3);
            this.groupBox1.Controls.Add(this.btnAge2);
            this.groupBox1.Controls.Add(this.btnAge1);
            this.groupBox1.Controls.Add(this.btnWoman);
            this.groupBox1.Controls.Add(this.btnMan);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 188);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ข้อมูลลูกค้า";
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnSearch.Image = global::PowerPOS_Online.Properties.Resources.magnifier_left;
            this.btnSearch.Location = new System.Drawing.Point(196, 24);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(31, 26);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.groupBox1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(7, 156);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(233, 188);
            this.panel5.TabIndex = 8;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnConfirm.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnConfirm.Location = new System.Drawing.Point(0, 102);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(233, 38);
            this.btnConfirm.TabIndex = 11;
            this.btnConfirm.Text = "ยืนยันการขาย";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // panel7
            // 
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 48);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(233, 8);
            this.panel7.TabIndex = 12;
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.btnCancel.Location = new System.Drawing.Point(0, 56);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(233, 38);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "ยกเลิกการขายทั้งบิล";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnConfirm);
            this.panel6.Controls.Add(this.panel8);
            this.panel6.Controls.Add(this.btnCancel);
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Controls.Add(this.btnCancelProduct);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(7, 344);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.panel6.Size = new System.Drawing.Size(233, 154);
            this.panel6.TabIndex = 10;
            // 
            // panel8
            // 
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 94);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(233, 8);
            this.panel8.TabIndex = 13;
            // 
            // btnCancelProduct
            // 
            this.btnCancelProduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCancelProduct.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.btnCancelProduct.Location = new System.Drawing.Point(0, 10);
            this.btnCancelProduct.Name = "btnCancelProduct";
            this.btnCancelProduct.Size = new System.Drawing.Size(233, 38);
            this.btnCancelProduct.TabIndex = 14;
            this.btnCancelProduct.Text = "ยกเลิกการขายทีละรายการ";
            this.btnCancelProduct.UseVisualStyleBackColor = true;
            this.btnCancelProduct.Click += new System.EventHandler(this.btnCancelProduct_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.pnlBarcode);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 39);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(7, 7, 7, 0);
            this.panel3.Size = new System.Drawing.Size(247, 478);
            this.panel3.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("DilleniaUPC", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "ขายสินค้า";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(802, 39);
            this.panel1.TabIndex = 10;
            // 
            // UcSell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.table1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "UcSell";
            this.Size = new System.Drawing.Size(802, 517);
            this.Load += new System.EventHandler(this.UcSell_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.table1)).EndInit();
            this.pnlBarcode.ResumeLayout(false);
            this.pnlBarcode.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Button btnAge6;
        private XPTable.Models.TextColumn clNo;
        private XPTable.Models.TextColumn clSku;
        private XPTable.Models.TextColumn clName;
        private System.Windows.Forms.Button btnAge5;
        private System.Windows.Forms.Button btnAge4;
        private System.Windows.Forms.Button btnAge3;
        private System.Windows.Forms.Button btnAge2;
        private System.Windows.Forms.Button btnAge1;
        private System.Windows.Forms.Button btnWoman;
        private System.Windows.Forms.Button btnMan;
        private XPTable.Models.NumberColumn clPrice;
        private XPTable.Models.NumberColumn clQty;
        private XPTable.Models.NumberColumn clTotal;
        private XPTable.Models.ColumnModel columnModel1;
        private XPTable.Models.Table table1;
        private XPTable.Models.TableModel tableModel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pnlBarcode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btnCancelProduct;
        private System.Windows.Forms.TextBox txtBarcode;
    }
}
