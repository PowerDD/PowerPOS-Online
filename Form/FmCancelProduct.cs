using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerPOS_Online
{
    public partial class FmCancelProduct : Form
    {
        public FmCancelProduct()
        {
            InitializeComponent();
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    Param.BarcodeNo = txtBarcode.Text;

                    DataTable dt = Util.DBQuery(string.Format(@"SELECT b.OrderNo, p.ID, p.CoverImage, IFNULL(b.ReceivedDate, '') ReceivedDate 
                    FROM Barcode b LEFT JOIN Product p ON b.product = p.id WHERE b.Barcode = '{0}'", txtBarcode.Text));

                    if (dt.Rows.Count == 0)
                    {
                        dt = Util.DBQuery(string.Format(@"SELECT Barcode FROM Product WHERE Barcode LIKE '%{0}%'", txtBarcode.Text));
                        Console.WriteLine(txtBarcode.Text + " " + Param.BarcodeNo + " " + dt.Rows.Count.ToString());
                        if (dt.Rows.Count == 0)
                        {
                            lblStatus.Text = "ไม่พบข้อมูลสินค้าชิ้นนี้ในระบบ";
                            lblStatus.ForeColor = Color.Red;
                        }
                        else
                        {
                            Param.status = "Cancel";
                            FmSelectProduct frm = new FmSelectProduct();
                            var result = frm.ShowDialog(this);
                            if (result == System.Windows.Forms.DialogResult.OK)
                            {
                                Param.UcSell.LoadData();
                                txtBarcode.Text = "";
                            }

                        }
                    }
                    else
                    {
                        lblStatus.Visible = false;
                        dt = Util.DBQuery(string.Format(@"SELECT p.Name, p.ID, IFNULL(p.Price, 0) Price, IFNULL(p.Price1, 0) Price1, IFNULL(p.Price2, 0) Price2, ReceivedDate, ReceivedBy, SellDate, SellBy 
                        FROM Barcode b LEFT JOIN Product p ON b.Product = p.ID 
                        WHERE b.Barcode = '{0}' AND p.Shop = '{1}' AND SellBy <> '' ", txtBarcode.Text, Param.ShopId));

                        if (dt.Rows.Count == 1)
                        {
                            Util.DBExecute(string.Format(@"UPDATE Barcode SET SellPrice = '0',SellBy = '', Sync = 1 WHERE SellBy = '{0}' AND Barcode = '{1}'", Param.CpuId, txtBarcode.Text));
                            Param.UcSell.LoadData();
                            txtBarcode.Text = "";
                        }
                        else
                        {
                            txtBarcode.Text = "";
                            lblStatus.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void FmCancelProduct_Load(object sender, EventArgs e)
        {
            lblStatus.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
