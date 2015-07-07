namespace PowerPOS_Online
{
    partial class UcConfig
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gbxBill = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbxPrinter = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtBillFooter = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtBillHeader = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rdbLogoNotPrint = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.rdbLogoPrint = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.nudPrintCount = new System.Windows.Forms.NumericUpDown();
            this.rdbNotPrint = new System.Windows.Forms.RadioButton();
            this.rdbPrint = new System.Windows.Forms.RadioButton();
            this.rdbAlert = new System.Windows.Forms.RadioButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnSaveBill = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gbxBill.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrintCount)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(914, 39);
            this.panel1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("DilleniaUPC", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "การตั้งค่าระบบ";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gbxBill);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 39);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(6, 6, 6, 0);
            this.panel2.Size = new System.Drawing.Size(327, 441);
            this.panel2.TabIndex = 9;
            // 
            // gbxBill
            // 
            this.gbxBill.Controls.Add(this.btnSaveBill);
            this.gbxBill.Controls.Add(this.groupBox4);
            this.gbxBill.Controls.Add(this.groupBox3);
            this.gbxBill.Controls.Add(this.groupBox2);
            this.gbxBill.Controls.Add(this.label2);
            this.gbxBill.Controls.Add(this.nudPrintCount);
            this.gbxBill.Controls.Add(this.rdbNotPrint);
            this.gbxBill.Controls.Add(this.rdbPrint);
            this.gbxBill.Controls.Add(this.rdbAlert);
            this.gbxBill.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbxBill.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.gbxBill.Location = new System.Drawing.Point(6, 6);
            this.gbxBill.Name = "gbxBill";
            this.gbxBill.Padding = new System.Windows.Forms.Padding(8, 8, 8, 3);
            this.gbxBill.Size = new System.Drawing.Size(315, 373);
            this.gbxBill.TabIndex = 0;
            this.gbxBill.TabStop = false;
            this.gbxBill.Text = "ใบเสร็จรับเงิน";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbxPrinter);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(8, 274);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(8, 3, 8, 8);
            this.groupBox4.Size = new System.Drawing.Size(299, 53);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "เครื่องพิมพ์";
            // 
            // cbxPrinter
            // 
            this.cbxPrinter.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbxPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPrinter.FormattingEnabled = true;
            this.cbxPrinter.Location = new System.Drawing.Point(8, 18);
            this.cbxPrinter.Name = "cbxPrinter";
            this.cbxPrinter.Size = new System.Drawing.Size(283, 24);
            this.cbxPrinter.TabIndex = 9;
            this.cbxPrinter.SelectedIndexChanged += new System.EventHandler(this.cbxPrinter_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtBillFooter);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(8, 201);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(8, 3, 8, 8);
            this.groupBox3.Size = new System.Drawing.Size(299, 73);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ท้ายใบเสร็จ";
            // 
            // txtBillFooter
            // 
            this.txtBillFooter.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtBillFooter.Location = new System.Drawing.Point(8, 43);
            this.txtBillFooter.Name = "txtBillFooter";
            this.txtBillFooter.Size = new System.Drawing.Size(283, 22);
            this.txtBillFooter.TabIndex = 8;
            this.txtBillFooter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBillFooter.TextChanged += new System.EventHandler(this.txtBillFooter_TextChanged);
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(8, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(283, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "ข้อความท้ายใบเสร็จ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtBillHeader);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.panel3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(8, 91);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.groupBox2.Size = new System.Drawing.Size(299, 110);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "หัวใบเสร็จ";
            // 
            // txtBillHeader
            // 
            this.txtBillHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtBillHeader.Location = new System.Drawing.Point(8, 77);
            this.txtBillHeader.Name = "txtBillHeader";
            this.txtBillHeader.Size = new System.Drawing.Size(283, 22);
            this.txtBillHeader.TabIndex = 7;
            this.txtBillHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBillHeader.TextChanged += new System.EventHandler(this.txtBillHeader_TextChanged);
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(8, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(283, 23);
            this.label4.TabIndex = 3;
            this.label4.Text = "ชื่อใบเสร็จ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rdbLogoNotPrint);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.rdbLogoPrint);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(8, 18);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(283, 36);
            this.panel3.TabIndex = 5;
            // 
            // rdbLogoNotPrint
            // 
            this.rdbLogoNotPrint.AutoSize = true;
            this.rdbLogoNotPrint.Location = new System.Drawing.Point(154, 4);
            this.rdbLogoNotPrint.Name = "rdbLogoNotPrint";
            this.rdbLogoNotPrint.Size = new System.Drawing.Size(61, 20);
            this.rdbLogoNotPrint.TabIndex = 6;
            this.rdbLogoNotPrint.TabStop = true;
            this.rdbLogoNotPrint.Text = "ไม่พิมพ์";
            this.rdbLogoNotPrint.UseVisualStyleBackColor = true;
            this.rdbLogoNotPrint.CheckedChanged += new System.EventHandler(this.rdbLogoNotPrint_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "โลโก้";
            // 
            // rdbLogoPrint
            // 
            this.rdbLogoPrint.AutoSize = true;
            this.rdbLogoPrint.Location = new System.Drawing.Point(99, 4);
            this.rdbLogoPrint.Name = "rdbLogoPrint";
            this.rdbLogoPrint.Size = new System.Drawing.Size(49, 20);
            this.rdbLogoPrint.TabIndex = 5;
            this.rdbLogoPrint.TabStop = true;
            this.rdbLogoPrint.Text = "พิมพ์";
            this.rdbLogoPrint.UseVisualStyleBackColor = true;
            this.rdbLogoPrint.CheckedChanged += new System.EventHandler(this.rdbLogoPrint_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(174, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "ใบ";
            // 
            // nudPrintCount
            // 
            this.nudPrintCount.Location = new System.Drawing.Point(127, 43);
            this.nudPrintCount.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudPrintCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPrintCount.Name = "nudPrintCount";
            this.nudPrintCount.Size = new System.Drawing.Size(41, 22);
            this.nudPrintCount.TabIndex = 3;
            this.nudPrintCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudPrintCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPrintCount.ValueChanged += new System.EventHandler(this.nudPrintCount_ValueChanged);
            // 
            // rdbNotPrint
            // 
            this.rdbNotPrint.AutoSize = true;
            this.rdbNotPrint.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdbNotPrint.Location = new System.Drawing.Point(8, 63);
            this.rdbNotPrint.Name = "rdbNotPrint";
            this.rdbNotPrint.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.rdbNotPrint.Size = new System.Drawing.Size(299, 28);
            this.rdbNotPrint.TabIndex = 4;
            this.rdbNotPrint.TabStop = true;
            this.rdbNotPrint.Text = "ไม่พิมพ์";
            this.rdbNotPrint.UseVisualStyleBackColor = true;
            this.rdbNotPrint.CheckedChanged += new System.EventHandler(this.rdbNotPrint_CheckedChanged);
            // 
            // rdbPrint
            // 
            this.rdbPrint.AutoSize = true;
            this.rdbPrint.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdbPrint.Location = new System.Drawing.Point(8, 43);
            this.rdbPrint.Name = "rdbPrint";
            this.rdbPrint.Size = new System.Drawing.Size(299, 20);
            this.rdbPrint.TabIndex = 2;
            this.rdbPrint.TabStop = true;
            this.rdbPrint.Text = "พิมพ์อัตโนมัติครั้งละ";
            this.rdbPrint.UseVisualStyleBackColor = true;
            this.rdbPrint.CheckedChanged += new System.EventHandler(this.rdbPrint_CheckedChanged);
            // 
            // rdbAlert
            // 
            this.rdbAlert.AutoSize = true;
            this.rdbAlert.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdbAlert.Location = new System.Drawing.Point(8, 23);
            this.rdbAlert.Name = "rdbAlert";
            this.rdbAlert.Size = new System.Drawing.Size(299, 20);
            this.rdbAlert.TabIndex = 1;
            this.rdbAlert.TabStop = true;
            this.rdbAlert.Text = "แจ้งเตือนว่าจะพิมพ์หรือไม่";
            this.rdbAlert.UseVisualStyleBackColor = true;
            this.rdbAlert.CheckedChanged += new System.EventHandler(this.rdbAlert_CheckedChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // btnSaveBill
            // 
            this.btnSaveBill.Enabled = false;
            this.btnSaveBill.Image = global::PowerPOS_Online.Properties.Resources.disk_return_black;
            this.btnSaveBill.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveBill.Location = new System.Drawing.Point(205, 334);
            this.btnSaveBill.Name = "btnSaveBill";
            this.btnSaveBill.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnSaveBill.Size = new System.Drawing.Size(99, 28);
            this.btnSaveBill.TabIndex = 14;
            this.btnSaveBill.Text = "บันทึกข้อมูล";
            this.btnSaveBill.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveBill.UseVisualStyleBackColor = true;
            this.btnSaveBill.Click += new System.EventHandler(this.btnSaveBill_Click);
            // 
            // UcConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "UcConfig";
            this.Size = new System.Drawing.Size(914, 480);
            this.Load += new System.EventHandler(this.UcConfig_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.gbxBill.ResumeLayout(false);
            this.gbxBill.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrintCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox gbxBill;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtBillFooter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtBillHeader;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rdbLogoNotPrint;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdbLogoPrint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudPrintCount;
        private System.Windows.Forms.RadioButton rdbNotPrint;
        private System.Windows.Forms.RadioButton rdbPrint;
        private System.Windows.Forms.RadioButton rdbAlert;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cbxPrinter;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnSaveBill;
    }
}
