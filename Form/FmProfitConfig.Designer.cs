namespace PowerPOS_Online
{
    partial class FmProfitConfig
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
            this.btnSave = new System.Windows.Forms.Button();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nudPrice2 = new System.Windows.Forms.NumericUpDown();
            this.nudPrice1 = new System.Windows.Forms.NumericUpDown();
            this.nudPrice = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudPrice4 = new System.Windows.Forms.NumericUpDown();
            this.nudPrice3 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice3)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(278, 80);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 32);
            this.btnSave.TabIndex = 27;
            this.btnSave.Text = "บันทึกข้อมูล";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbCategory
            // 
            this.cbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Items.AddRange(new object[] {
            "เลขที่ใบสั่งซื้อ"});
            this.cbCategory.Location = new System.Drawing.Point(11, 9);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(354, 24);
            this.cbCategory.TabIndex = 26;
            this.cbCategory.SelectedIndexChanged += new System.EventHandler(this.cbCategory_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(149, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "ส่ง 2";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(80, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "ส่ง 1";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "ปลีก";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudPrice2
            // 
            this.nudPrice2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPrice2.Location = new System.Drawing.Point(155, 52);
            this.nudPrice2.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudPrice2.Name = "nudPrice2";
            this.nudPrice2.Size = new System.Drawing.Size(66, 22);
            this.nudPrice2.TabIndex = 22;
            this.nudPrice2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudPrice2.ThousandsSeparator = true;
            this.nudPrice2.ValueChanged += new System.EventHandler(this.nudPrice_ValueChanged);
            // 
            // nudPrice1
            // 
            this.nudPrice1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPrice1.Location = new System.Drawing.Point(83, 52);
            this.nudPrice1.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudPrice1.Name = "nudPrice1";
            this.nudPrice1.Size = new System.Drawing.Size(66, 22);
            this.nudPrice1.TabIndex = 21;
            this.nudPrice1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudPrice1.ThousandsSeparator = true;
            this.nudPrice1.ValueChanged += new System.EventHandler(this.nudPrice_ValueChanged);
            // 
            // nudPrice
            // 
            this.nudPrice.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPrice.Location = new System.Drawing.Point(11, 52);
            this.nudPrice.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudPrice.Name = "nudPrice";
            this.nudPrice.Size = new System.Drawing.Size(66, 22);
            this.nudPrice.TabIndex = 20;
            this.nudPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudPrice.ThousandsSeparator = true;
            this.nudPrice.ValueChanged += new System.EventHandler(this.nudPrice_ValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(293, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "ส่ง 4";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(224, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "ส่ง 3";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudPrice4
            // 
            this.nudPrice4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPrice4.Location = new System.Drawing.Point(299, 52);
            this.nudPrice4.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudPrice4.Name = "nudPrice4";
            this.nudPrice4.Size = new System.Drawing.Size(66, 22);
            this.nudPrice4.TabIndex = 29;
            this.nudPrice4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudPrice4.ThousandsSeparator = true;
            // 
            // nudPrice3
            // 
            this.nudPrice3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPrice3.Location = new System.Drawing.Point(227, 52);
            this.nudPrice3.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudPrice3.Name = "nudPrice3";
            this.nudPrice3.Size = new System.Drawing.Size(66, 22);
            this.nudPrice3.TabIndex = 28;
            this.nudPrice3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudPrice3.ThousandsSeparator = true;
            // 
            // FmProfitConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 118);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudPrice4);
            this.Controls.Add(this.nudPrice3);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbCategory);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudPrice2);
            this.Controls.Add(this.nudPrice1);
            this.Controls.Add(this.nudPrice);
            this.Name = "FmProfitConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "เปอร์เซ็นต์กำไร";
            this.Load += new System.EventHandler(this.FmProfitConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbCategory;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudPrice2;
        private System.Windows.Forms.NumericUpDown nudPrice1;
        private System.Windows.Forms.NumericUpDown nudPrice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudPrice4;
        private System.Windows.Forms.NumericUpDown nudPrice3;
    }
}