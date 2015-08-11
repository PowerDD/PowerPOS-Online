namespace PowerPOS_Online
{
    partial class FmInitialData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FmInitialData));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pgbStatus = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bwDownloadGif = new System.ComponentModel.BackgroundWorker();
            this.bwLoadShopInfo = new System.ComponentModel.BackgroundWorker();
            this.bwLoadShopConfig = new System.ComponentModel.BackgroundWorker();
            this.bwLoadBarcode = new System.ComponentModel.BackgroundWorker();
            this.bwInitialShopProduct = new System.ComponentModel.BackgroundWorker();
            this.bwLoadProduct = new System.ComponentModel.BackgroundWorker();
            this.bwCheckLicense = new System.ComponentModel.BackgroundWorker();
            this.bwSyncData = new System.ComponentModel.BackgroundWorker();
            this.bwLoadCustomer = new System.ComponentModel.BackgroundWorker();
            this.bwLoadSell = new System.ComponentModel.BackgroundWorker();
            this.bwLoadProvince = new System.ComponentModel.BackgroundWorker();
            this.bwLoadReturnProduct = new System.ComponentModel.BackgroundWorker();
            this.bwLoadClaim = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Controls.Add(this.pgbStatus);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(508, 140);
            this.panel1.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatus.Location = new System.Drawing.Point(141, 90);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(354, 23);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "โหลดข้อมูลสินค้าเสร็จแล้ว";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pgbStatus
            // 
            this.pgbStatus.Location = new System.Drawing.Point(141, 65);
            this.pgbStatus.Name = "pgbStatus";
            this.pgbStatus.Size = new System.Drawing.Size(356, 23);
            this.pgbStatus.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgbStatus.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("DilleniaUPC", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(141, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(354, 49);
            this.label1.TabIndex = 1;
            this.label1.Text = "กำลังโหลดข้อมูลจาก Server กรุณารอสักครู่ค่ะ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PowerPOS_Online.Properties.Resources.loading;
            this.pictureBox1.Location = new System.Drawing.Point(5, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 128);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // bwDownloadGif
            // 
            this.bwDownloadGif.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwDownloadGif_DoWork);
            // 
            // bwLoadShopInfo
            // 
            this.bwLoadShopInfo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadShopInfo_DoWork);
            this.bwLoadShopInfo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadShopInfo_RunWorkerCompleted);
            // 
            // bwLoadShopConfig
            // 
            this.bwLoadShopConfig.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadShopConfig_DoWork);
            this.bwLoadShopConfig.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadShopConfig_RunWorkerCompleted);
            // 
            // bwLoadBarcode
            // 
            this.bwLoadBarcode.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadBarcode_DoWork);
            this.bwLoadBarcode.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadBarcode_RunWorkerCompleted);
            // 
            // bwInitialShopProduct
            // 
            this.bwInitialShopProduct.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwInitialShopProduct_DoWork);
            this.bwInitialShopProduct.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwInitialShopProduct_RunWorkerCompleted);
            // 
            // bwLoadProduct
            // 
            this.bwLoadProduct.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadProduct_DoWork);
            this.bwLoadProduct.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadProduct_RunWorkerCompleted);
            // 
            // bwCheckLicense
            // 
            this.bwCheckLicense.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwCheckLicense_DoWork);
            this.bwCheckLicense.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwCheckLicense_RunWorkerCompleted);
            // 
            // bwSyncData
            // 
            this.bwSyncData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwSyncData_DoWork);
            this.bwSyncData.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwSyncData_RunWorkerCompleted);
            // 
            // bwLoadCustomer
            // 
            this.bwLoadCustomer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadCustomer_DoWork);
            this.bwLoadCustomer.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadCustomer_RunWorkerCompleted);
            // 
            // bwLoadSell
            // 
            this.bwLoadSell.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadSell_DoWork);
            this.bwLoadSell.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadSell_RunWorkerCompleted);
            // 
            // bwLoadProvince
            // 
            this.bwLoadProvince.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadProvince_DoWork);
            this.bwLoadProvince.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadProvince_RunWorkerCompleted);
            // 
            // bwLoadReturnProduct
            // 
            this.bwLoadReturnProduct.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadReturnProduct_DoWork);
            this.bwLoadReturnProduct.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadReturnProduct_RunWorkerCompleted);
            // 
            // bwLoadClaim
            // 
            this.bwLoadClaim.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadClaim_DoWork);
            this.bwLoadClaim.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadClaim_RunWorkerCompleted);
            // 
            // FmInitialData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 140);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FmInitialData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "กำลังโหลดข้อมูล";
            this.Load += new System.EventHandler(this.FmInitialData_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.ComponentModel.BackgroundWorker bwDownloadGif;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar pgbStatus;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker bwLoadShopInfo;
        private System.ComponentModel.BackgroundWorker bwLoadShopConfig;
        private System.ComponentModel.BackgroundWorker bwLoadBarcode;
        private System.ComponentModel.BackgroundWorker bwInitialShopProduct;
        private System.ComponentModel.BackgroundWorker bwLoadProduct;
        private System.ComponentModel.BackgroundWorker bwCheckLicense;
        private System.ComponentModel.BackgroundWorker bwSyncData;
        private System.ComponentModel.BackgroundWorker bwLoadCustomer;
        private System.ComponentModel.BackgroundWorker bwLoadSell;
        private System.ComponentModel.BackgroundWorker bwLoadProvince;
        private System.ComponentModel.BackgroundWorker bwLoadReturnProduct;
        private System.ComponentModel.BackgroundWorker bwLoadClaim;
    }
}