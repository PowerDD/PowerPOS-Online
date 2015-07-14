namespace PowerPOS_Online
{
    partial class UcProduct
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gbOrderNo = new System.Windows.Forms.GroupBox();
            this.cbNoPrice = new System.Windows.Forms.CheckBox();
            this.cbNoStock = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cbbBrand = new System.Windows.Forms.ComboBox();
            this.cbbCategory = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.pnlPrice = new System.Windows.Forms.Panel();
            this.lblCategory = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblCost = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnConfig = new System.Windows.Forms.Button();
            this.btnUsePercentPrice = new System.Windows.Forms.Button();
            this.btnUseWebPrice = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPercent2 = new System.Windows.Forms.TextBox();
            this.txtPercent1 = new System.Windows.Forms.TextBox();
            this.txtPrice2 = new System.Windows.Forms.TextBox();
            this.txtPercent = new System.Windows.Forms.TextBox();
            this.txtPrice1 = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.nudPrice2 = new System.Windows.Forms.NumericUpDown();
            this.nudPrice1 = new System.Windows.Forms.NumericUpDown();
            this.nudPrice = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ptbProduct = new System.Windows.Forms.PictureBox();
            this.columnModel1 = new XPTable.Models.ColumnModel();
            this.clNo = new XPTable.Models.TextColumn();
            this.clSku = new XPTable.Models.TextColumn();
            this.clName = new XPTable.Models.TextColumn();
            this.clQty = new XPTable.Models.NumberColumn();
            this.clCategory = new XPTable.Models.TextColumn();
            this.clBrand = new XPTable.Models.TextColumn();
            this.clWarranty = new XPTable.Models.TextColumn();
            this.clCost = new XPTable.Models.NumberColumn();
            this.clPrice = new XPTable.Models.NumberColumn();
            this.clPrice1 = new XPTable.Models.NumberColumn();
            this.clPrice2 = new XPTable.Models.NumberColumn();
            this.clWebPrice = new XPTable.Models.NumberColumn();
            this.clWebPrice1 = new XPTable.Models.NumberColumn();
            this.clWebPrice2 = new XPTable.Models.NumberColumn();
            this.tableModel1 = new XPTable.Models.TableModel();
            this.table1 = new XPTable.Models.Table();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.gbOrderNo.SuspendLayout();
            this.pnlPrice.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.table1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.lblRecords);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 39);
            this.panel1.TabIndex = 7;
            // 
            // lblRecords
            // 
            this.lblRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblRecords.ForeColor = System.Drawing.Color.White;
            this.lblRecords.Location = new System.Drawing.Point(261, 13);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(42, 16);
            this.lblRecords.TabIndex = 6;
            this.lblRecords.Text = "0";
            this.lblRecords.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("DilleniaUPC", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "ข้อมูลสินค้าในระบบ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(301, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "รายการ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(166, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "ค้นพบข้อมูลทั้งหมด";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gbOrderNo);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 39);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(7, 10, 7, 0);
            this.panel3.Size = new System.Drawing.Size(247, 635);
            this.panel3.TabIndex = 8;
            // 
            // gbOrderNo
            // 
            this.gbOrderNo.Controls.Add(this.cbNoPrice);
            this.gbOrderNo.Controls.Add(this.cbNoStock);
            this.gbOrderNo.Controls.Add(this.btnSearch);
            this.gbOrderNo.Controls.Add(this.cbbBrand);
            this.gbOrderNo.Controls.Add(this.cbbCategory);
            this.gbOrderNo.Controls.Add(this.txtSearch);
            this.gbOrderNo.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbOrderNo.Location = new System.Drawing.Point(7, 10);
            this.gbOrderNo.Name = "gbOrderNo";
            this.gbOrderNo.Size = new System.Drawing.Size(233, 156);
            this.gbOrderNo.TabIndex = 0;
            this.gbOrderNo.TabStop = false;
            this.gbOrderNo.Text = "ค้นหาข้อมูล";
            // 
            // cbNoPrice
            // 
            this.cbNoPrice.AutoSize = true;
            this.cbNoPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cbNoPrice.Location = new System.Drawing.Point(14, 111);
            this.cbNoPrice.Name = "cbNoPrice";
            this.cbNoPrice.Size = new System.Drawing.Size(125, 20);
            this.cbNoPrice.TabIndex = 6;
            this.cbNoPrice.Text = "ยังไม่กำหนดราคาขาย";
            this.cbNoPrice.UseVisualStyleBackColor = true;
            this.cbNoPrice.CheckedChanged += new System.EventHandler(this.btnSearch_Click);
            // 
            // cbNoStock
            // 
            this.cbNoStock.AutoSize = true;
            this.cbNoStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cbNoStock.Location = new System.Drawing.Point(14, 133);
            this.cbNoStock.Name = "cbNoStock";
            this.cbNoStock.Size = new System.Drawing.Size(103, 20);
            this.cbNoStock.TabIndex = 5;
            this.cbNoStock.Text = "ไม่มีในคลังสินค้า";
            this.cbNoStock.UseVisualStyleBackColor = true;
            this.cbNoStock.CheckedChanged += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSearch.Image = global::PowerPOS_Online.Properties.Resources.magnifier_left;
            this.btnSearch.Location = new System.Drawing.Point(187, 78);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(31, 31);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cbbBrand
            // 
            this.cbbBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbBrand.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbBrand.FormattingEnabled = true;
            this.cbbBrand.Items.AddRange(new object[] {
            "เลขที่ใบสั่งซื้อ"});
            this.cbbBrand.Location = new System.Drawing.Point(14, 49);
            this.cbbBrand.Name = "cbbBrand";
            this.cbbBrand.Size = new System.Drawing.Size(204, 24);
            this.cbbBrand.TabIndex = 3;
            this.cbbBrand.SelectedIndexChanged += new System.EventHandler(this.btnSearch_Click);
            // 
            // cbbCategory
            // 
            this.cbbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbCategory.FormattingEnabled = true;
            this.cbbCategory.Items.AddRange(new object[] {
            "เลขที่ใบสั่งซื้อ"});
            this.cbbCategory.Location = new System.Drawing.Point(14, 19);
            this.cbbCategory.Name = "cbbCategory";
            this.cbbCategory.Size = new System.Drawing.Size(204, 24);
            this.cbbCategory.TabIndex = 0;
            this.cbbCategory.SelectedIndexChanged += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.Azure;
            this.txtSearch.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.MidnightBlue;
            this.txtSearch.Location = new System.Drawing.Point(14, 79);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(167, 29);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // pnlPrice
            // 
            this.pnlPrice.Controls.Add(this.lblCategory);
            this.pnlPrice.Controls.Add(this.groupBox1);
            this.pnlPrice.Controls.Add(this.panel2);
            this.pnlPrice.Controls.Add(this.ptbProduct);
            this.pnlPrice.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPrice.Location = new System.Drawing.Point(247, 39);
            this.pnlPrice.Name = "pnlPrice";
            this.pnlPrice.Padding = new System.Windows.Forms.Padding(5);
            this.pnlPrice.Size = new System.Drawing.Size(777, 129);
            this.pnlPrice.TabIndex = 9;
            this.pnlPrice.Visible = false;
            // 
            // lblCategory
            // 
            this.lblCategory.Location = new System.Drawing.Point(5, 106);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(137, 12);
            this.lblCategory.TabIndex = 10;
            this.lblCategory.Text = "Category";
            this.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCategory.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Controls.Add(this.btnConfig);
            this.groupBox1.Controls.Add(this.btnUsePercentPrice);
            this.groupBox1.Controls.Add(this.btnUseWebPrice);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtPercent2);
            this.groupBox1.Controls.Add(this.txtPercent1);
            this.groupBox1.Controls.Add(this.txtPrice2);
            this.groupBox1.Controls.Add(this.txtPercent);
            this.groupBox1.Controls.Add(this.txtPrice1);
            this.groupBox1.Controls.Add(this.txtPrice);
            this.groupBox1.Controls.Add(this.nudPrice2);
            this.groupBox1.Controls.Add(this.nudPrice1);
            this.groupBox1.Controls.Add(this.nudPrice);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(129, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(484, 119);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ราคาขาย";
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSave.Image = global::PowerPOS_Online.Properties.Resources.disk_return_black;
            this.btnSave.Location = new System.Drawing.Point(448, 53);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(29, 24);
            this.btnSave.TabIndex = 18;
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblCost);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Location = new System.Drawing.Point(298, 24);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(146, 29);
            this.panel4.TabIndex = 17;
            // 
            // lblCost
            // 
            this.lblCost.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCost.Location = new System.Drawing.Point(39, 1);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(79, 22);
            this.lblCost.TabIndex = 19;
            this.lblCost.Text = "0.00";
            this.lblCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label11.Location = new System.Drawing.Point(118, 5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 16);
            this.label11.TabIndex = 18;
            this.label11.Text = "บาท";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label10.Location = new System.Drawing.Point(3, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 16);
            this.label10.TabIndex = 17;
            this.label10.Text = "ต้นทุน";
            // 
            // btnConfig
            // 
            this.btnConfig.Enabled = false;
            this.btnConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnConfig.Image = global::PowerPOS_Online.Properties.Resources.gear;
            this.btnConfig.Location = new System.Drawing.Point(448, 83);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(29, 24);
            this.btnConfig.TabIndex = 13;
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // btnUsePercentPrice
            // 
            this.btnUsePercentPrice.Enabled = false;
            this.btnUsePercentPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnUsePercentPrice.Location = new System.Drawing.Point(298, 83);
            this.btnUsePercentPrice.Name = "btnUsePercentPrice";
            this.btnUsePercentPrice.Size = new System.Drawing.Size(146, 24);
            this.btnUsePercentPrice.TabIndex = 13;
            this.btnUsePercentPrice.Text = "ใช้ % กำไรที่กำหนดไว้";
            this.btnUsePercentPrice.UseVisualStyleBackColor = true;
            this.btnUsePercentPrice.Click += new System.EventHandler(this.btnUsePercentPrice_Click);
            // 
            // btnUseWebPrice
            // 
            this.btnUseWebPrice.Enabled = false;
            this.btnUseWebPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnUseWebPrice.Location = new System.Drawing.Point(298, 53);
            this.btnUseWebPrice.Name = "btnUseWebPrice";
            this.btnUseWebPrice.Size = new System.Drawing.Size(146, 24);
            this.btnUseWebPrice.TabIndex = 12;
            this.btnUseWebPrice.Text = "ใช้ราคาหน้าเว็บ";
            this.btnUseWebPrice.UseVisualStyleBackColor = true;
            this.btnUseWebPrice.Click += new System.EventHandler(this.btnUseWebPrice_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(29, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "กำไร (%)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "ราคาหน้าร้าน";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(223, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "ส่ง 2";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(154, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "ส่ง 1";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(82, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "ปลีก";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPercent2
            // 
            this.txtPercent2.Font = new System.Drawing.Font("Arial", 9.75F);
            this.txtPercent2.Location = new System.Drawing.Point(226, 84);
            this.txtPercent2.Name = "txtPercent2";
            this.txtPercent2.ReadOnly = true;
            this.txtPercent2.Size = new System.Drawing.Size(66, 22);
            this.txtPercent2.TabIndex = 6;
            this.txtPercent2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPercent1
            // 
            this.txtPercent1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.txtPercent1.Location = new System.Drawing.Point(154, 84);
            this.txtPercent1.Name = "txtPercent1";
            this.txtPercent1.ReadOnly = true;
            this.txtPercent1.Size = new System.Drawing.Size(66, 22);
            this.txtPercent1.TabIndex = 5;
            this.txtPercent1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPrice2
            // 
            this.txtPrice2.Font = new System.Drawing.Font("Arial", 9.75F);
            this.txtPrice2.Location = new System.Drawing.Point(226, 26);
            this.txtPrice2.Name = "txtPrice2";
            this.txtPrice2.ReadOnly = true;
            this.txtPrice2.Size = new System.Drawing.Size(66, 22);
            this.txtPrice2.TabIndex = 6;
            this.txtPrice2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPercent
            // 
            this.txtPercent.Font = new System.Drawing.Font("Arial", 9.75F);
            this.txtPercent.Location = new System.Drawing.Point(82, 84);
            this.txtPercent.Name = "txtPercent";
            this.txtPercent.ReadOnly = true;
            this.txtPercent.Size = new System.Drawing.Size(66, 22);
            this.txtPercent.TabIndex = 4;
            this.txtPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPrice1
            // 
            this.txtPrice1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.txtPrice1.Location = new System.Drawing.Point(154, 26);
            this.txtPrice1.Name = "txtPrice1";
            this.txtPrice1.ReadOnly = true;
            this.txtPrice1.Size = new System.Drawing.Size(66, 22);
            this.txtPrice1.TabIndex = 5;
            this.txtPrice1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPrice
            // 
            this.txtPrice.Font = new System.Drawing.Font("Arial", 9.75F);
            this.txtPrice.Location = new System.Drawing.Point(82, 26);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.ReadOnly = true;
            this.txtPrice.Size = new System.Drawing.Size(66, 22);
            this.txtPrice.TabIndex = 4;
            this.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nudPrice2
            // 
            this.nudPrice2.Enabled = false;
            this.nudPrice2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPrice2.Location = new System.Drawing.Point(226, 54);
            this.nudPrice2.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudPrice2.Name = "nudPrice2";
            this.nudPrice2.Size = new System.Drawing.Size(66, 22);
            this.nudPrice2.TabIndex = 3;
            this.nudPrice2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudPrice2.ThousandsSeparator = true;
            this.nudPrice2.ValueChanged += new System.EventHandler(this.nudPrice2_ValueChanged);
            // 
            // nudPrice1
            // 
            this.nudPrice1.Enabled = false;
            this.nudPrice1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPrice1.Location = new System.Drawing.Point(154, 54);
            this.nudPrice1.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudPrice1.Name = "nudPrice1";
            this.nudPrice1.Size = new System.Drawing.Size(66, 22);
            this.nudPrice1.TabIndex = 2;
            this.nudPrice1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudPrice1.ThousandsSeparator = true;
            this.nudPrice1.ValueChanged += new System.EventHandler(this.nudPrice1_ValueChanged);
            // 
            // nudPrice
            // 
            this.nudPrice.Enabled = false;
            this.nudPrice.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPrice.Location = new System.Drawing.Point(82, 54);
            this.nudPrice.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudPrice.Name = "nudPrice";
            this.nudPrice.Size = new System.Drawing.Size(66, 22);
            this.nudPrice.TabIndex = 1;
            this.nudPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudPrice.ThousandsSeparator = true;
            this.nudPrice.ValueChanged += new System.EventHandler(this.nudPrice_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "ราคาหน้าเว็บ";
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(124, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(5, 119);
            this.panel2.TabIndex = 9;
            // 
            // ptbProduct
            // 
            this.ptbProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ptbProduct.Dock = System.Windows.Forms.DockStyle.Left;
            this.ptbProduct.ImageLocation = "";
            this.ptbProduct.Location = new System.Drawing.Point(5, 5);
            this.ptbProduct.Name = "ptbProduct";
            this.ptbProduct.Size = new System.Drawing.Size(119, 119);
            this.ptbProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbProduct.TabIndex = 8;
            this.ptbProduct.TabStop = false;
            // 
            // columnModel1
            // 
            this.columnModel1.Columns.AddRange(new XPTable.Models.Column[] {
            this.clNo,
            this.clSku,
            this.clName,
            this.clQty,
            this.clCategory,
            this.clBrand,
            this.clWarranty,
            this.clCost,
            this.clPrice,
            this.clPrice1,
            this.clPrice2,
            this.clWebPrice,
            this.clWebPrice1,
            this.clWebPrice2});
            // 
            // clNo
            // 
            this.clNo.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clNo.Editable = false;
            this.clNo.IsTextTrimmed = false;
            this.clNo.Resizable = false;
            this.clNo.Sortable = false;
            this.clNo.Text = "ที่";
            this.clNo.Width = 35;
            // 
            // clSku
            // 
            this.clSku.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clSku.Editable = false;
            this.clSku.IsTextTrimmed = false;
            this.clSku.Text = "รหัสสินค้า";
            // 
            // clName
            // 
            this.clName.Editable = false;
            this.clName.IsTextTrimmed = false;
            this.clName.Text = "ชื่อสินค้า";
            this.clName.Width = 400;
            // 
            // clQty
            // 
            this.clQty.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clQty.Editable = false;
            this.clQty.Format = "#,##0";
            this.clQty.IsTextTrimmed = false;
            this.clQty.Text = "จำนวน";
            this.clQty.Width = 65;
            // 
            // clCategory
            // 
            this.clCategory.Editable = false;
            this.clCategory.IsTextTrimmed = false;
            this.clCategory.Text = "หมวดหมู่";
            this.clCategory.Width = 150;
            // 
            // clBrand
            // 
            this.clBrand.Editable = false;
            this.clBrand.IsTextTrimmed = false;
            this.clBrand.Text = "ยี่ห้อ";
            // 
            // clWarranty
            // 
            this.clWarranty.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.clWarranty.Editable = false;
            this.clWarranty.IsTextTrimmed = false;
            this.clWarranty.Text = "ประกัน";
            this.clWarranty.Width = 65;
            // 
            // clCost
            // 
            this.clCost.Alignment = XPTable.Models.ColumnAlignment.Right;
            this.clCost.Editable = false;
            this.clCost.Format = "#,##0.00";
            this.clCost.IsTextTrimmed = false;
            this.clCost.Text = "ทุน";
            this.clCost.Width = 60;
            // 
            // clPrice
            // 
            this.clPrice.Alignment = XPTable.Models.ColumnAlignment.Right;
            this.clPrice.Editable = false;
            this.clPrice.Format = "#,###";
            this.clPrice.IsTextTrimmed = false;
            this.clPrice.Text = "ราคาปลีก";
            // 
            // clPrice1
            // 
            this.clPrice1.Alignment = XPTable.Models.ColumnAlignment.Right;
            this.clPrice1.Editable = false;
            this.clPrice1.Format = "#,###";
            this.clPrice1.IsTextTrimmed = false;
            this.clPrice1.Text = "ราคาส่ง";
            // 
            // clPrice2
            // 
            this.clPrice2.Alignment = XPTable.Models.ColumnAlignment.Right;
            this.clPrice2.Editable = false;
            this.clPrice2.Format = "#,###";
            this.clPrice2.IsTextTrimmed = false;
            this.clPrice2.Text = "ราคาส่ง 2";
            this.clPrice2.Visible = false;
            // 
            // clWebPrice
            // 
            this.clWebPrice.IsTextTrimmed = false;
            this.clWebPrice.Visible = false;
            // 
            // clWebPrice1
            // 
            this.clWebPrice1.IsTextTrimmed = false;
            this.clWebPrice1.Visible = false;
            // 
            // clWebPrice2
            // 
            this.clWebPrice2.IsTextTrimmed = false;
            this.clWebPrice2.Visible = false;
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
            this.table1.Location = new System.Drawing.Point(247, 168);
            this.table1.Name = "table1";
            this.table1.NoItemsText = "";
            this.table1.SelectionStyle = XPTable.Models.SelectionStyle.Grid;
            this.table1.Size = new System.Drawing.Size(777, 506);
            this.table1.TabIndex = 10;
            this.table1.TableModel = this.tableModel1;
            this.table1.Text = "table1";
            this.table1.UnfocusedBorderColor = System.Drawing.Color.Black;
            this.table1.EndSort += new XPTable.Events.ColumnEventHandler(this.table1_EndSort);
            this.table1.SelectionChanged += new XPTable.Events.SelectionEventHandler(this.table1_SelectionChanged);
            // 
            // UcProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.table1);
            this.Controls.Add(this.pnlPrice);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "UcProduct";
            this.Size = new System.Drawing.Size(1024, 674);
            this.Load += new System.EventHandler(this.UcProduct_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.gbOrderNo.ResumeLayout(false);
            this.gbOrderNo.PerformLayout();
            this.pnlPrice.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.table1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox gbOrderNo;
        private System.Windows.Forms.CheckBox cbNoPrice;
        private System.Windows.Forms.CheckBox cbNoStock;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cbbBrand;
        private System.Windows.Forms.ComboBox cbbCategory;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel pnlPrice;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblCost;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Button btnUsePercentPrice;
        private System.Windows.Forms.Button btnUseWebPrice;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPercent2;
        private System.Windows.Forms.TextBox txtPercent1;
        private System.Windows.Forms.TextBox txtPrice2;
        private System.Windows.Forms.TextBox txtPercent;
        private System.Windows.Forms.TextBox txtPrice1;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.NumericUpDown nudPrice2;
        private System.Windows.Forms.NumericUpDown nudPrice1;
        private System.Windows.Forms.NumericUpDown nudPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox ptbProduct;
        private XPTable.Models.ColumnModel columnModel1;
        private XPTable.Models.TextColumn clNo;
        private XPTable.Models.TextColumn clSku;
        private XPTable.Models.TextColumn clName;
        private XPTable.Models.TextColumn clCategory;
        private XPTable.Models.TextColumn clBrand;
        private XPTable.Models.TextColumn clWarranty;
        private XPTable.Models.NumberColumn clQty;
        private XPTable.Models.NumberColumn clCost;
        private XPTable.Models.NumberColumn clPrice;
        private XPTable.Models.NumberColumn clPrice1;
        private XPTable.Models.NumberColumn clPrice2;
        private XPTable.Models.NumberColumn clWebPrice;
        private XPTable.Models.NumberColumn clWebPrice1;
        private XPTable.Models.NumberColumn clWebPrice2;
        private XPTable.Models.TableModel tableModel1;
        private XPTable.Models.Table table1;
    }
}
