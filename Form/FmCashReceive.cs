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
    public partial class FmCashReceive : Form
    {
        public int _TOTAL = 0;
        public string _SELL_NO;

        public FmCashReceive()
        {
            InitializeComponent();
        }

        private void FmCashReceive_Load(object sender, EventArgs e)
        {
            txtCash.Focus();
        }

        private void txtCash_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                lblChange.Text = (int.Parse(txtCash.Text) - _TOTAL).ToString("#,##0");
            }
            catch
            {
                lblChange.Text = _TOTAL.ToString("#,##0");
            }
        }

        private void txtCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt;
                dt = Util.DBQuery(string.Format(@"SELECT b.Barcode, b.SellNo, b.SellPrice Price
                    FROM Barcode b WHERE b.sellBy = '{0}'", Param.CpuId));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Util.DBExecute(string.Format(@"UPDATE Barcode
                        SET SellBy = '{0}', SellDate = STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), SellNo = '{1}', Sync = 1, SellPrice = {2}, Customer = '{3}', SellFinished = 1
                        WHERE Barcode = '{4}' AND SellNo = '{5}'",
                        Param.UserId, _SELL_NO, dt.Rows[i]["Price"].ToString(), Param.SelectCustomerId,
                        dt.Rows[i]["Barcode"].ToString(), dt.Rows[i]["SellNo"].ToString()));
                }

                //DataTable dtP = Util.DBQuery(string.Format(@"SELECT IFNULL(SUM(SellPrice),0) SellPrice, IFNULL(SUM(Cost),0) Cost, IFNULL(SUM(OperationCost),0) OperationCost FROM Barcode WHERE SellNo = '{0}'", _SELL_NO));
                //DataTable dtS = Util.DBQuery(string.Format(@"SELECT IFNULL(SUM(Price),0) SellPrice, IFNULL(SUM(PriceCost),0) Cost FROM sellTemp"));

                //int val = ((Convert.ToInt32(dtP.Rows[0]["SellPrice"].ToString()) + Convert.ToInt32(dtS.Rows[0]["SellPrice"].ToString())) - (Convert.ToInt32(dtP.Rows[0]["Cost"].ToString()) + Convert.ToInt32(dtS.Rows[0]["Cost"].ToString()))) - Convert.ToInt32(dtP.Rows[0]["OperationCost"].ToString());

                DataTable dtF = Util.DBQuery(string.Format(@"SELECT SUM(SellPrice) SellPrice, SUM(Cost) Cost, SUM(OperationCost) OP, (SUM(SellPrice) - SUM(Cost) ) Profit FROM (
                    SELECT IFNULL(SUM(SellPrice),0) SellPrice, IFNULL(SUM(Cost),0) Cost, IFNULL(SUM(OperationCost),0) OperationCost FROM Barcode WHERE SellNo = '{0}'
                    UNION
                    SELECT IFNULL(SUM(TotalPrice),0) SellPrice, IFNULL(SUM(PriceCost),0) Cost, 0 FROM sellTemp
                    ) aa", _SELL_NO));

                Util.DBExecute(string.Format(@"INSERT INTO SellHeader (SellNo, Profit, TotalPrice, Customer, CustomerSex, CustomerAge, SellDate, SellBy)
                    SELECT '{0}', '{6}','{5}', '{1}', '{2}', {3}, STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), '{4}'",
                    _SELL_NO, Param.SelectCustomerId, Param.SelectCustomerSex, Param.SelectCustomerAge, Param.UserId, _TOTAL, dtF.Rows[0]["Profit"].ToString()));


                Util.DBExecute(string.Format(@"INSERT INTO SellDetail (SellNo, Product, SellPrice, Cost, Quantity, Sync)
                    SELECT sellNo, Product, SUM(TotalPrice)TotalPrice, SUM(PriceCost) PriceCost, SUM(Amount) Amount,1 FROM(
                       SELECT  '{0}' sellNo, Product, SUM(SellPrice) TotalPrice, SUM(Cost) PriceCost, COUNT(*) Amount, 1 FROM Barcode WHERE SellNo = '{0}' GROUP BY Product
                       Union ALL SELECT  '{0}', Product, TotalPrice, PriceCost, Amount, 1 FROM SellTemp) GROUP BY sellNo, Product", _SELL_NO));

                var cash = int.Parse(txtCash.Text);
                if ((cash >= double.Parse(lblPrice.Text)) || cash == 0)
                {
                    if (cash != 0)
                    {
                        Util.DBExecute(string.Format(@"UPDATE SellHeader SET Cash = {0}, PayType = 1, Paid = 1, Sync = 1 WHERE SellNo = '{1}'", cash, _SELL_NO));
                    }
                    else
                    {
                        Util.DBExecute(string.Format(@"UPDATE SellHeader SET Cash = 0, PayType = 0, Paid = 0, Sync = 1 WHERE SellNo = '{0}'", _SELL_NO));
                    }

                    Util.DBExecute(string.Format(@"DELETE FROM SellTemp"));


                    //Util.PrintReceipt(_SELL_NO);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;

                    Param.SelectCustomerId = "000000";
                    Param.SelectCustomerName = "ลูกค้าทั่วไป";
                    Param.SelectCustomerSex = "";
                    Param.SelectCustomerAge = 0;
                    Param.SelectCustomerSellPrice = 0;
                    if (Param.SystemConfig.Bill.PrintType == "Y")
                    {
                        var cnt = int.Parse(Param.SystemConfig.Bill.PrintCount.ToString());
                        for (int i = 1; i <= cnt; i++)
                            Util.PrintReceipt(_SELL_NO);
                    }
                    else if (Param.SystemConfig.Bill.PrintType == "A")
                    {
                        if (MessageBox.Show("คุณต้องการพิมพ์ใบเสร็จรับเงินหรือไม่ ?", "ยืนยันการพิมพ์", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            Util.PrintReceipt(_SELL_NO);
                    }
                }
                else
                {
                    MessageBox.Show("กรุณาตรวจสอบจำนวนเงินที่รับมาอีกครั้ง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { }
        }

        private void txtCash_Enter(object sender, EventArgs e)
        {
            Util.SetKeyboardLayout(Util.GetInputLanguageByName("US"));
        }

        private void txtCash_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Return))
            {
                btnSave_Click(sender, (e));
            }
        }
    }
}
