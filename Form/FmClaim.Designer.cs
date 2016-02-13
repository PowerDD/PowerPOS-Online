namespace PowerPOS_Online
{
    partial class FmClaim
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbxDetail = new System.Windows.Forms.GroupBox();
            this.rtbDetail = new System.Windows.Forms.RichTextBox();
            this.rdbHq = new System.Windows.Forms.RadioButton();
            this.txtCash = new System.Windows.Forms.TextBox();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.rdbCash = new System.Windows.Forms.RadioButton();
            this.rdbSwap = new System.Windows.Forms.RadioButton();
            this.gbxCustomer = new System.Windows.Forms.GroupBox();
            this.btnCustomer = new System.Windows.Forms.Button();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.txtNickname = new System.Windows.Forms.TextBox();
            this.txtLastname = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnPrice = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.lblSellPrice = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.gbxDetail.SuspendLayout();
            this.gbxCustomer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnPrice.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gbxDetail);
            this.groupBox1.Controls.Add(this.rdbHq);
            this.groupBox1.Controls.Add(this.txtCash);
            this.groupBox1.Controls.Add(this.txtBarcode);
            this.groupBox1.Controls.Add(this.rdbCash);
            this.groupBox1.Controls.Add(this.rdbSwap);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(11, 5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(305, 226);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "เลือกวิธีการเคลมสินค้า";
            // 
            // gbxDetail
            // 
            this.gbxDetail.Controls.Add(this.rtbDetail);
            this.gbxDetail.ForeColor = System.Drawing.Color.Red;
            this.gbxDetail.Location = new System.Drawing.Point(13, 107);
            this.gbxDetail.Name = "gbxDetail";
            this.gbxDetail.Padding = new System.Windows.Forms.Padding(5, 3, 5, 5);
            this.gbxDetail.Size = new System.Drawing.Size(277, 106);
            this.gbxDetail.TabIndex = 7;
            this.gbxDetail.TabStop = false;
            this.gbxDetail.Text = "อาการเสีย";
            // 
            // rtbDetail
            // 
            this.rtbDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDetail.Location = new System.Drawing.Point(5, 18);
            this.rtbDetail.Name = "rtbDetail";
            this.rtbDetail.Size = new System.Drawing.Size(267, 83);
            this.rtbDetail.TabIndex = 5;
            this.rtbDetail.Text = "";
            // 
            // rdbHq
            // 
            this.rdbHq.AutoSize = true;
            this.rdbHq.Location = new System.Drawing.Point(13, 80);
            this.rdbHq.Name = "rdbHq";
            this.rdbHq.Size = new System.Drawing.Size(135, 20);
            this.rdbHq.TabIndex = 4;
            this.rdbHq.Text = "ส่งเคลมที่สำนักงานใหญ่";
            this.rdbHq.UseVisualStyleBackColor = true;
            this.rdbHq.CheckedChanged += new System.EventHandler(this.checkRadio);
            // 
            // txtCash
            // 
            this.txtCash.Enabled = false;
            this.txtCash.Location = new System.Drawing.Point(140, 50);
            this.txtCash.Name = "txtCash";
            this.txtCash.Size = new System.Drawing.Size(145, 22);
            this.txtCash.TabIndex = 4;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(140, 21);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(145, 22);
            this.txtBarcode.TabIndex = 1;
            // 
            // rdbCash
            // 
            this.rdbCash.AutoSize = true;
            this.rdbCash.Location = new System.Drawing.Point(13, 51);
            this.rdbCash.Name = "rdbCash";
            this.rdbCash.Size = new System.Drawing.Size(121, 20);
            this.rdbCash.TabIndex = 3;
            this.rdbCash.Text = "คืนเงิน จำนวน (บาท)";
            this.rdbCash.UseVisualStyleBackColor = true;
            this.rdbCash.CheckedChanged += new System.EventHandler(this.checkRadio);
            // 
            // rdbSwap
            // 
            this.rdbSwap.AutoSize = true;
            this.rdbSwap.Checked = true;
            this.rdbSwap.Location = new System.Drawing.Point(13, 22);
            this.rdbSwap.Name = "rdbSwap";
            this.rdbSwap.Size = new System.Drawing.Size(121, 20);
            this.rdbSwap.TabIndex = 2;
            this.rdbSwap.TabStop = true;
            this.rdbSwap.Text = "เปลี่ยนสินค้า บาร์โค้ด";
            this.rdbSwap.UseVisualStyleBackColor = true;
            this.rdbSwap.CheckedChanged += new System.EventHandler(this.checkRadio);
            // 
            // gbxCustomer
            // 
            this.gbxCustomer.Controls.Add(this.btnCustomer);
            this.gbxCustomer.Controls.Add(this.txtEmail);
            this.gbxCustomer.Controls.Add(this.txtMobile);
            this.gbxCustomer.Controls.Add(this.txtNickname);
            this.gbxCustomer.Controls.Add(this.txtLastname);
            this.gbxCustomer.Controls.Add(this.txtName);
            this.gbxCustomer.Controls.Add(this.label5);
            this.gbxCustomer.Controls.Add(this.label4);
            this.gbxCustomer.Controls.Add(this.label3);
            this.gbxCustomer.Controls.Add(this.label2);
            this.gbxCustomer.Controls.Add(this.label1);
            this.gbxCustomer.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbxCustomer.Enabled = false;
            this.gbxCustomer.Location = new System.Drawing.Point(11, 231);
            this.gbxCustomer.Name = "gbxCustomer";
            this.gbxCustomer.Size = new System.Drawing.Size(305, 205);
            this.gbxCustomer.TabIndex = 3;
            this.gbxCustomer.TabStop = false;
            this.gbxCustomer.Text = "ข้อมูลลูกค้า";
            this.gbxCustomer.Visible = false;
            // 
            // btnCustomer
            // 
            this.btnCustomer.Location = new System.Drawing.Point(161, 171);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Size = new System.Drawing.Size(124, 28);
            this.btnCustomer.TabIndex = 15;
            this.btnCustomer.Text = "ค้นหาข้อมูลลูกค้า";
            this.btnCustomer.UseVisualStyleBackColor = true;
            this.btnCustomer.Click += new System.EventHandler(this.btnCustomer_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(100, 142);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(185, 22);
            this.txtEmail.TabIndex = 10;
            // 
            // txtMobile
            // 
            this.txtMobile.Location = new System.Drawing.Point(100, 112);
            this.txtMobile.Margin = new System.Windows.Forms.Padding(4);
            this.txtMobile.MaxLength = 10;
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(185, 22);
            this.txtMobile.TabIndex = 9;
            // 
            // txtNickname
            // 
            this.txtNickname.Location = new System.Drawing.Point(100, 82);
            this.txtNickname.Margin = new System.Windows.Forms.Padding(4);
            this.txtNickname.Name = "txtNickname";
            this.txtNickname.Size = new System.Drawing.Size(185, 22);
            this.txtNickname.TabIndex = 8;
            // 
            // txtLastname
            // 
            this.txtLastname.Location = new System.Drawing.Point(100, 52);
            this.txtLastname.Margin = new System.Windows.Forms.Padding(4);
            this.txtLastname.Name = "txtLastname";
            this.txtLastname.Size = new System.Drawing.Size(185, 22);
            this.txtLastname.TabIndex = 7;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(100, 22);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(185, 22);
            this.txtName.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(62, 145);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 16);
            this.label5.TabIndex = 14;
            this.label5.Text = "อีเมล";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(15, 115);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 13;
            this.label4.Text = "เบอร์โทรศัพท์ *";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 55);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "นามสกุล";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 85);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "ชื่อเล่น";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "ชื่อ";
            // 
            // btnSave
            // 
            this.btnSave.Image = global::PowerPOS_Online.Properties.Resources.disk_return_black;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(199, 6);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(102, 33);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "บันทึกข้อมูล";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::PowerPOS_Online.Properties.Resources.arrow_circle_225_left;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(114, 6);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancel.Size = new System.Drawing.Size(77, 33);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(11, 490);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(305, 42);
            this.panel1.TabIndex = 13;
            // 
            // pnPrice
            // 
            this.pnPrice.Controls.Add(this.label6);
            this.pnPrice.Controls.Add(this.label7);
            this.pnPrice.Controls.Add(this.lblBarcode);
            this.pnPrice.Controls.Add(this.lblSellPrice);
            this.pnPrice.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnPrice.Location = new System.Drawing.Point(11, 464);
            this.pnPrice.Name = "pnPrice";
            this.pnPrice.Size = new System.Drawing.Size(305, 26);
            this.pnPrice.TabIndex = 16;
            this.pnPrice.Visible = false;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.ForeColor = System.Drawing.Color.Green;
            this.label6.Location = new System.Drawing.Point(174, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 18);
            this.label6.TabIndex = 13;
            this.label6.Text = "ราคาขาย";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.ForeColor = System.Drawing.Color.Green;
            this.label7.Location = new System.Drawing.Point(2, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 14);
            this.label7.TabIndex = 18;
            this.label7.Text = "บาร์โค้ด";
            // 
            // lblBarcode
            // 
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblBarcode.ForeColor = System.Drawing.Color.Green;
            this.lblBarcode.Location = new System.Drawing.Point(51, 5);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(62, 14);
            this.lblBarcode.TabIndex = 17;
            this.lblBarcode.Text = "lblBarcode";
            this.lblBarcode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSellPrice
            // 
            this.lblSellPrice.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblSellPrice.ForeColor = System.Drawing.Color.Green;
            this.lblSellPrice.Location = new System.Drawing.Point(227, 5);
            this.lblSellPrice.Name = "lblSellPrice";
            this.lblSellPrice.Size = new System.Drawing.Size(76, 16);
            this.lblSellPrice.TabIndex = 15;
            this.lblSellPrice.Text = "lblSellPrice";
            this.lblSellPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FmClaim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 543);
            this.Controls.Add(this.pnPrice);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbxCustomer);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FmClaim";
            this.Padding = new System.Windows.Forms.Padding(11, 5, 11, 11);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "เคลมสินค้า";
            this.Load += new System.EventHandler(this.FmClaim_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbxDetail.ResumeLayout(false);
            this.gbxCustomer.ResumeLayout(false);
            this.gbxCustomer.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnPrice.ResumeLayout(false);
            this.pnPrice.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbCash;
        private System.Windows.Forms.RadioButton rdbSwap;
        private System.Windows.Forms.TextBox txtCash;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.GroupBox gbxDetail;
        private System.Windows.Forms.RichTextBox rtbDetail;
        private System.Windows.Forms.RadioButton rdbHq;
        private System.Windows.Forms.GroupBox gbxCustomer;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.TextBox txtNickname;
        private System.Windows.Forms.TextBox txtLastname;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCustomer;
        private System.Windows.Forms.Panel pnPrice;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.Label lblSellPrice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}