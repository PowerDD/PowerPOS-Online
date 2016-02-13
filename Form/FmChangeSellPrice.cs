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
    public partial class FmChangeSellPrice : Form
    {
        public FmChangeSellPrice()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string code = Param.DevicePrefix + Param.ShopId.Substring(Math.Max(0, Param.ShopId.Length - 4));
            if (txtCode.Text == code)
            {
                DataTable dt;
                dt = Util.DBQuery(string.Format(@"SELECT Product, Cost, sellPrice FROM Barcode WHERE SellBy = '{0}' AND Product = '{1}'", Param.CpuId, UcSell.productNo));

                var SellPrice = int.Parse(txtSellPrice.Text);
                if ((SellPrice >= double.Parse(dt.Rows[0]["Cost"].ToString())))
                {
                    if (MessageBox.Show("คุณแน่ใจหรือไม่ ที่จะแก้ไขราคาขายนี้ ?", "ยืนยันข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Util.DBExecute(string.Format(@"UPDATE Barcode SET SellPrice = '{2}', Sync = 1 WHERE SellBy = '{0}' AND Product = '{1}'", Param.CpuId, UcSell.productNo, SellPrice));
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                }
                else
                {
                    MessageBox.Show("กรุณาตรวจสอบราคาขายอีกครั้ง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("กรุณาตรวจสอบ Code อีกครั้ง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Return))
            {
                btnSave_Click(sender, (e));
            }
        }

        private void txtCode_Enter(object sender, EventArgs e)
        {
            Util.SetKeyboardLayout(Util.GetInputLanguageByName("US"));
        }
    }
}
