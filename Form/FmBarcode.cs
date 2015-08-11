using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerPOS_Online
{
    public partial class FmBarcode : Form
    {
        public FmBarcode()
        {
            InitializeComponent();
        }

        private void DownloadImage(string url, string savePath, string fileName)
        {
            ptbProduct.ImageLocation = url;
            DownloadImage d = new DownloadImage();
            Thread thread = new Thread(() => d.Download(url, savePath, fileName));
            thread.Start();
        }

        private void FmBarcode_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            lblBarcode.Text = UcProduct.barcode;
            DataTable dt = Util.DBQuery(string.Format("SELECT b.Barcode,p.Name,p.ID,b.ReceivedDate,b.SellDate,b.Cost,b.SellPrice,b.OrderNo,p.Warranty FROM Barcode b LEFT JOIN Product p ON b.Product = p.ID WHERE b.Barcode = '{0}' AND p.Shop = {1}", lblBarcode.Text, Param.ShopParent));

            lblProductId.Text = dt.Rows[0]["ID"].ToString();
            lblProductName.Text = dt.Rows[0]["Name"].ToString();
            lblOrderNo.Text = dt.Rows[0]["OrderNo"].ToString();

            if (dt.Rows[0]["ReceivedDate"].ToString() != "")
            {
                lblOrderNo.Text = dt.Rows[0]["OrderNo"].ToString();
                lblReceivedDate.Text = Convert.ToDateTime(dt.Rows[0]["ReceivedDate"]).ToLocalTime().ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("th-TH"));
                lblCost.Text = dt.Rows[0]["Cost"].ToString() + " บาท";

                if (dt.Rows[0]["SellDate"].ToString() == "")
                {
                    lblStatus.Text = "ยังไม่ทำการขายสินค้าชิ้นนี้";
                    lblStatus.ForeColor = Color.Red;
                    lblPrice.Text = "-";
                    lblSellDate.Text = "-";
                    lblWar.Text = "-";
                }
                else
                {
                    lblStatus.Text = "ทำการขายสินค้าชิ้นนี้แล้ว";
                    lblStatus.ForeColor = Color.Green;
                    lblPrice.Text = dt.Rows[0]["SellPrice"].ToString() + " บาท";
                    lblSellDate.Text = Convert.ToDateTime(dt.Rows[0]["SellDate"]).ToLocalTime().ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("th-TH"));
                    var remain = (dt.Rows[0]["Warranty"].ToString() == "0") ? 0 : Convert.ToInt32(dt.Rows[0]["Warranty"]) - (DateTime.Now - Convert.ToDateTime(dt.Rows[0]["SellDate"])).TotalDays;
                    lblWar.Text = Convert.ToInt32(remain).ToString() + " วัน";
                }
                lblWarranty.Text = dt.Rows[0]["Warranty"].ToString() + " วัน";
            }
            else
            {
                lblStatus.Text = "สินค้าชิ้นนี้ยังไม่ได้รับเข้าระบบ";
                lblStatus.ForeColor = Color.Red;
                lblPrice.Text = "-";
                lblSellDate.Text = "-";
                lblWar.Text = "-";
                lblWarranty.Text = "-";
                lblReceivedDate.Text = "-"; 
                lblCost.Text = "-";
            }
            LoadImage();
        }

        private void LoadImage()
        {
            var filename = @"Resource/Images/Product/" + lblProductId.Text + ".jpg";
            DataTable dt = Util.DBQuery(string.Format("SELECT CoverImage FROM Product WHERE ID = '{0}' AND Shop = {1}", lblProductId.Text, Param.ShopParent));

            if (!File.Exists(filename))
            {
                if (dt.Rows.Count > 0 && dt.Rows[0]["CoverImage"].ToString() != "")
                {
                    DownloadImage(dt.Rows[0]["CoverImage"].ToString(), @"Resource/Images/Product/", lblProductId.Text + ".jpg");
                }
            }
            else
            {
                try { ptbProduct.Image = Image.FromFile(filename); }
                catch
                {
                    if (dt.Rows.Count > 0 && dt.Rows[0]["CoverImage"].ToString() != "")
                    {
                        DownloadImage(dt.Rows[0]["CoverImage"].ToString(), @"Resource/Images/Product/", lblProductId.Text + ".jpg");
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
