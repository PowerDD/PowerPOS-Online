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
    public partial class FmClaim : Form
    {
        public FmClaim()
        {
            InitializeComponent();
            this.Height = 317;
        }

        private void checkRadio(object sender, EventArgs e)
        {
            txtBarcode.Enabled = rdbSwap.Checked;
            txtCash.Enabled = rdbCash.Checked;
            gbxCustomer.Enabled = rdbHq.Checked;
            if (rdbSwap.Checked) txtBarcode.Focus();
            else if (rdbCash.Checked) txtCash.Focus();
            else if (rdbHq.Checked) txtMobile.Focus();

            gbxCustomer.Visible = rdbHq.Checked;
            this.Height = (gbxCustomer.Visible) ? 495 : 317;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
//            if (MessageBox.Show("คุณแน่ใจหรือไม่ ที่จะทำการเคลมสินค้านี้ ?", "ยืนยันข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
//            {
//                DataTable dt = Util.DBQuery(string.Format(@"SELECT IFNULL(SUBSTR(MAX(ClaimNo), 1, 5)||SUBSTR('0000'||(SUBSTR(MAX(ClaimNo), 6, 4)+1), -4, 4), SUBSTR(STRFTIME('%Y%m{0}'), 3)||'0001') ClaimNo
//                    FROM ClaimNo
//                    WHERE SUBSTR(ClaimNo, 1, 4) = SUBSTR(STRFTIME('%Y%m'), 3, 4)
//                    AND SUBSTR(ClaimNo, 5, 1) = '{0}'", Param.DevicePrefix));
//                var ClaimNo = dt.Rows[0]["ClaimNo"].ToString();

//                dt = Util.DBQuery(string.Format(@"SELECT b.Barcode, b.SellNo, p.Price{0} Price
//                    FROM Barcode b
//                        LEFT JOIN Product p
//                        ON b.product = p.id
//                    WHERE p.Shop = '{1}' AND b.sellBy = '{2}'", Param.SelectCustomerSellPrice == 0 ? "" : "" + Param.SelectCustomerSellPrice, Param.ShopId, Param.CpuId));

//                for (int i = 0; i < dt.Rows.Count; i++)
//                {
//                    Util.DBExecute(string.Format(@"UPDATE Barcode
//                        SET SellBy = '{0}', SellDate = STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), SellNo = '{1}', Sync = 1, SellPrice = {2}, Customer = '{3}', SellFinished = 1
//                        WHERE Barcode = '{4}' AND SellNo = '{5}'",
//                        Param.UserId, SellNo, dt.Rows[i]["Price"].ToString(), Param.SelectCustomerId,
//                        dt.Rows[i]["Barcode"].ToString(), dt.Rows[i]["SellNo"].ToString()));
//                }

//                Util.DBExecute(string.Format(@"INSERT INTO SellHeader (SellNo, Profit, TotalPrice, Customer, CustomerSex, CustomerAge, SellDate, SellBy)
//                    SELECT '{0}', (SELECT SUM(SellPrice-Cost-OperationCost) FROM Barcode WHERE SellNo = '{0}'),
//                    (SELECT SUM(SellPrice) FROM Barcode WHERE SellNo = '{0}'), '{1}', '{2}', {3}, STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), '{4}'",
//                    SellNo, Param.SelectCustomerId, Param.SelectCustomerSex, Param.SelectCustomerAge, Param.UserId));

//                Util.DBExecute(string.Format(@"INSERT INTO SellDetail (SellNo, Product, SellPrice, Cost, Quantity, Sync)
//                    SELECT '{0}', Product, SUM(SellPrice), SUM(Cost), COUNT(*), 1 FROM Barcode WHERE SellNo = '{0}' GROUP BY Product",
//                    SellNo));

//                FmCashReceive dialog = new FmCashReceive();
//                dialog.lblPrice.Text = lblPrice.Text;
//                dialog.lblChange.Text = "0";
//                dialog._TOTAL = int.Parse(lblPrice.Text.Replace(",", ""));
//                dialog._SELL_NO = SellNo;
//                var result = dialog.ShowDialog(this);
//                if (result != DialogResult.OK)
//                {
//                    Util.DBExecute(string.Format(@"UPDATE SellHeader SET Cash = 0, PayType = 0, Paid = 0, Sync = 1 WHERE SellNo = '{0}'", SellNo));
//                }

//                Param.SelectCustomerId = "000000";
//                Param.SelectCustomerName = "ลูกค้าทั่วไป";
//                Param.SelectCustomerSex = "";
//                Param.SelectCustomerAge = 0;
//                Param.SelectCustomerSellPrice = 0;
//                lblStatus.Text = "";
//                if (Param.SystemConfig.Bill.PrintType == "Y")
//                {
//                    var cnt = int.Parse(Param.SystemConfig.Bill.PrintCount.ToString());
//                    for (int i = 1; i <= cnt; i++)
//                        Util.PrintReceipt(SellNo);
//                }
//                else if (Param.SystemConfig.Bill.PrintType == "A")
//                {
//                    if (MessageBox.Show("คุณต้องการพิมพ์ใบเสร็จรับเงินหรือไม่ ?", "การพิมพ์", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
//                        Util.PrintReceipt(SellNo);
//                }

//                lblCustomerName.Text = "ลูกค้าทั่วไป";
//                Param.SelectCustomerSex = "";
//                Param.SelectCustomerAge = 0;
//                Param.SelectCustomerSellPrice = 0;
//                SelectCustomer(sender, (e));
//                LoadData();
//            }
        }


    }
}
