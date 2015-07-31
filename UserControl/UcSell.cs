using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XPTable.Models;

namespace PowerPOS_Online
{
    public partial class UcSell : UserControl
    {
        public UcSell()
        {
            InitializeComponent();
        }

        private void UcSell_Load(object sender, EventArgs e)
        {
            
            Util.InitialTable(table1);
            Param.SelectCustomerId = "000000";
            Param.SelectCustomerName = "ลูกค้าทั่วไป";
            Param.SelectCustomerSex = "";
            Param.SelectCustomerAge = 0;
            Param.SelectCustomerSellPrice = 0;
            Param.UcSell = this;
            LoadData();
            SelectCustomer(sender, e);
        }

        private void btnMan_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnMan);
            SetDefaultButton(btnWoman);
            Param.SelectCustomerSex = "M";
        }

        private void btnWoman_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnWoman);
            SetDefaultButton(btnMan);
            Param.SelectCustomerSex = "F";
        }

        private void btnAge1_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnAge1);
            SetDefaultButton(btnAge2);
            SetDefaultButton(btnAge3);
            SetDefaultButton(btnAge4);
            SetDefaultButton(btnAge5);
            SetDefaultButton(btnAge6);
            if (Param.SelectCustomerId == "000000") Param.SelectCustomerAge = 1;
        }

        private void btnAge2_Click(object sender, EventArgs e)
        {
            SetDefaultButton(btnAge1);
            SetActiveButton(btnAge2);
            SetDefaultButton(btnAge3);
            SetDefaultButton(btnAge4);
            SetDefaultButton(btnAge5);
            SetDefaultButton(btnAge6);
            if (Param.SelectCustomerId == "000000") Param.SelectCustomerAge = 13;
        }

        private void btnAge3_Click(object sender, EventArgs e)
        {
            SetDefaultButton(btnAge1);
            SetDefaultButton(btnAge2);
            SetActiveButton(btnAge3);
            SetDefaultButton(btnAge4);
            SetDefaultButton(btnAge5);
            SetDefaultButton(btnAge6);
            if (Param.SelectCustomerId == "000000") Param.SelectCustomerAge = 19;
        }

        private void btnAge4_Click(object sender, EventArgs e)
        {
            SetDefaultButton(btnAge1);
            SetDefaultButton(btnAge2);
            SetDefaultButton(btnAge3);
            SetActiveButton(btnAge4);
            SetDefaultButton(btnAge5);
            SetDefaultButton(btnAge6);
            if (Param.SelectCustomerId == "000000") Param.SelectCustomerAge = 26;
        }

        private void btnAge5_Click(object sender, EventArgs e)
        {
            SetDefaultButton(btnAge1);
            SetDefaultButton(btnAge2);
            SetDefaultButton(btnAge3);
            SetDefaultButton(btnAge4);
            SetActiveButton(btnAge5);
            SetDefaultButton(btnAge6);
            if (Param.SelectCustomerId == "000000") Param.SelectCustomerAge = 41;
        }

        private void btnAge6_Click(object sender, EventArgs e)
        {
            SetDefaultButton(btnAge1);
            SetDefaultButton(btnAge2);
            SetDefaultButton(btnAge3);
            SetDefaultButton(btnAge4);
            SetDefaultButton(btnAge5);
            SetActiveButton(btnAge6);
            if (Param.SelectCustomerId == "000000") Param.SelectCustomerAge = 61;
        }

        private void SetActiveButton(Button button)
        {
            button.ForeColor = Color.White;
            button.BackColor = Color.SteelBlue;
        }

        private void SetDefaultButton(Button button)
        {
            button.ForeColor = SystemColors.ControlText;
            button.UseVisualStyleBackColor = true;
        }

        private void txtBarcode_Enter(object sender, EventArgs e)
        {
            Util.SetKeyboardLayout(Util.GetInputLanguageByName("US"));
            lblStatus.Text = "";
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            lblStatus.Visible = false;

            if (e.KeyCode == Keys.Return)
            {
                DataTable dt = Util.DBQuery(string.Format(@"SELECT p.Name, p.ID, IFNULL(p.Price, 0) Price, IFNULL(p.Price1, 0) Price1, IFNULL(p.Price2, 0) Price2, ReceivedDate, ReceivedBy, SellDate, SellBy 
                    FROM Barcode b 
                        LEFT JOIN Product p 
                        ON b.Product = p.ID 
                    WHERE Barcode = '{0}' AND Shop = '{1}'", txtBarcode.Text, Param.ShopId));
                lblStatus.Visible = true;

                if (dt.Rows.Count == 0)
                {
                    lblStatus.Text = "ไม่พบข้อมูลสินค้าชิ้นนี้ในระบบ";
                    lblStatus.ForeColor = Color.Red;
                }
                else if (dt.Rows[0]["ReceivedDate"].ToString() == "")
                {
                    lblStatus.Text = "สินค้าชิ้นนี้ยังไม่ได้รับเข้าระบบ";
                    lblStatus.ForeColor = Color.Red;
                    var confirm = MessageBox.Show("สินค้าชิ้นนี้ยังไม่ได้รับเข้าระบบ\nคุณต้องการไปที่หน้าจอรับสินค้าเข้าระบบหรือไม่ ?", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                    if (confirm)
                    {
                        Util.ShowScreen(Param.Screen.ReceiveProduct);
                        Param.SelectedScreen = (int)Param.Screen.ReceiveProduct;
                    }
                }
                else if (dt.Rows[0]["ReceivedBy"].ToString() == "")
                {
                    lblStatus.Text = "สินค้าชิ้นนี้ยังไม่ได้กำหนดต้นทุน";
                    lblStatus.ForeColor = Color.Red;
                    var confirm = MessageBox.Show("สินค้าชิ้นนี้ยังไม่ได้กำหนดต้นทุน\nคุณต้องการไปที่หน้าจอรับสินค้าเข้าระบบหรือไม่ ?", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                    if (confirm)
                    {
                        Util.ShowScreen(Param.Screen.ReceiveProduct);
                        Param.SelectedScreen = (int)Param.Screen.ReceiveProduct;
                    }
                }
                else if (dt.Rows[0]["SellDate"].ToString() != "")
                {
                    lblStatus.Text = "สินค้าชิ้นนี้ได้ขายออกจากระบบไปแล้ว";
                    lblStatus.ForeColor = Color.Red;
                }
                else if (dt.Rows[0]["SellBy"].ToString() == Param.CpuId)
                {
                    lblStatus.Text = "มีสินค้าชิ้นนี้ในรายการขายแล้ว";
                    lblStatus.ForeColor = Color.Red;
                }
                else if (int.Parse(dt.Rows[0]["Price"].ToString()) == 0 || int.Parse(dt.Rows[0]["Price1"].ToString()) == 0)
                {
                    lblStatus.Text = "สินค้าชิ้นนี้ยังไม่ได้กำหนดราคาขาย";
                    lblStatus.ForeColor = Color.Red;
                    var confirm = MessageBox.Show("สินค้าชิ้นนี้ยังไม่ได้กำหนดราคาขาย\nคุณต้องการไปที่หน้าจอข้อมูลสินค้าหรือไม่ ?", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                    if (confirm)
                    {
                        Util.ShowScreen(Param.Screen.Product);
                        Param.SelectedScreen = (int)Param.Screen.Product;
                    }
                }
                else
                {
                    Util.DBExecute(string.Format(@"UPDATE Barcode SET SellBy = '{0}', Sync = 1 WHERE Barcode = '{1}'", Param.CpuId, txtBarcode.Text));
                    LoadData();
                    txtBarcode.Focus();
                    lblStatus.Text = "เพิ่มสินค้าในรายการขายแล้ว";
                    lblStatus.ForeColor = Color.Green;
                }

                txtBarcode.Text = "";
            }
        }

        public void LoadData()
        {
            DataTable dt = Util.DBQuery(string.Format(@"SELECT p.ID, p.Name, p.Price{2} Price, ProductCount
                    FROM (SELECT Product, COUNT(*) ProductCount FROM Barcode WHERE SellBy = '{0}' GROUP BY Product) b 
                        LEFT JOIN Product p 
                        ON b.Product = p.ID
                    WHERE Shop = '{1}'", Param.CpuId, Param.ShopId, Param.SelectCustomerSellPrice == 0 ? "" : "" + Param.SelectCustomerSellPrice));

            var sumPrice = 0;
            table1.BeginUpdate();
            tableModel1.Rows.Clear();
            tableModel1.RowHeight = 22;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["ID"].ToString() != "")
                {
                    var total = int.Parse(dt.Rows[i]["ProductCount"].ToString()) * int.Parse(dt.Rows[i]["Price"].ToString());
                    tableModel1.Rows.Add(new Row(
                        new Cell[] {
                        new Cell("" + (i + 1)),
                        new Cell(dt.Rows[i]["ID"].ToString()),
                        new Cell(dt.Rows[i]["Name"].ToString()),
                        new Cell(int.Parse(dt.Rows[i]["Price"].ToString())),
                        new Cell(int.Parse(dt.Rows[i]["ProductCount"].ToString())),
                        new Cell(total)
                       }));
                    sumPrice += total;
                }
            }
            table1.EndUpdate();

            lblPrice.Text = sumPrice.ToString("#,##0");

            btnCancel.Enabled = sumPrice > 0;
            btnCancelProduct.Enabled = sumPrice > 0;
            btnConfirm.Enabled = sumPrice > 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("คุณแน่ใจหรือไม่\nที่จะยกเลิกรายการขายนี้", "ยืนยันการทำงาน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
            if (confirm)
            {
                Util.DBExecute(string.Format(@"UPDATE Barcode SET SellBy = '', Sync = 1 WHERE SellBy = '{0}'", Param.CpuId));
                LoadData();
            }
        }

        private void btnCancelProduct_Click(object sender, EventArgs e)
        {
            FmCancelProduct frm = new FmCancelProduct();
            frm.Show();
            lblStatus.Visible = false;
           
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณแน่ใจหรือไม่ ที่จะยืนยันการขายนี้ ?", "ยืนยันข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                DataTable dt = Util.DBQuery(string.Format(@"SELECT IFNULL(SUBSTR(MAX(SellNo), 1, 5)||SUBSTR('0000'||(SUBSTR(MAX(SellNo), 6, 4)+1), -4, 4), SUBSTR(STRFTIME('%Y%m{0}'), 3)||'0001') SellNo
                    FROM SellHeader
                    WHERE SUBSTR(SellNo, 1, 4) = SUBSTR(STRFTIME('%Y%m'), 3, 4)
                    AND SUBSTR(SellNo, 5, 1) = '{0}'", Param.DevicePrefix));
                var SellNo = dt.Rows[0]["SellNo"].ToString();

                dt = Util.DBQuery(string.Format(@"SELECT b.Barcode, b.SellNo, p.Price{0} Price
                    FROM Barcode b
                        LEFT JOIN Product p
                        ON b.product = p.id
                    WHERE p.Shop = '{1}' AND b.sellBy = '{2}'", Param.SelectCustomerSellPrice == 0 ? "" : "" + Param.SelectCustomerSellPrice, Param.ShopId, Param.CpuId));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Util.DBExecute(string.Format(@"UPDATE Barcode
                        SET SellBy = '{0}', SellDate = STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), SellNo = '{1}', Sync = 1, SellPrice = {2}, Customer = '{3}', SellFinished = 1
                        WHERE Barcode = '{4}' AND SellNo = '{5}'",
                        Param.UserId, SellNo, dt.Rows[i]["Price"].ToString(), Param.SelectCustomerId,
                        dt.Rows[i]["Barcode"].ToString(), dt.Rows[i]["SellNo"].ToString()));
                }

                Util.DBExecute(string.Format(@"INSERT INTO SellHeader (SellNo, Profit, TotalPrice, Customer, CustomerSex, CustomerAge, SellDate, SellBy)
                    SELECT '{0}', (SELECT SUM(SellPrice-Cost-OperationCost) FROM Barcode WHERE SellNo = '{0}'),
                    (SELECT SUM(SellPrice) FROM Barcode WHERE SellNo = '{0}'), '{1}', '{2}', {3}, STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), '{4}'",
                    SellNo, Param.SelectCustomerId, Param.SelectCustomerSex, Param.SelectCustomerAge, Param.UserId));

                Util.DBExecute(string.Format(@"INSERT INTO SellDetail (SellNo, Product, SellPrice, Cost, Quantity, Sync)
                    SELECT '{0}', Product, SUM(SellPrice), SUM(Cost), COUNT(*), 1 FROM Barcode WHERE SellNo = '{0}' GROUP BY Product",
                    SellNo));
                
                FmCashReceive dialog = new FmCashReceive();
                dialog.lblPrice.Text = lblPrice.Text;
                dialog.lblChange.Text = "0";
                dialog._TOTAL = int.Parse(lblPrice.Text.Replace(",", ""));
                dialog._SELL_NO = SellNo;
                var result = dialog.ShowDialog(this);
                if (result != DialogResult.OK)
                {
                    Util.DBExecute(string.Format(@"UPDATE SellHeader SET Cash = 0, PayType = 0, Paid = 0, Sync = 1 WHERE SellNo = '{0}'", SellNo));
                }

                Param.SelectCustomerId = "000000";
                Param.SelectCustomerName = "ลูกค้าทั่วไป";
                Param.SelectCustomerSex = "";
                Param.SelectCustomerAge = 0;
                Param.SelectCustomerSellPrice = 0;
                lblStatus.Text = "";
                if (Param.SystemConfig.Bill.PrintType == "Y")
                {
                    var cnt = int.Parse(Param.SystemConfig.Bill.PrintCount.ToString());
                    for (int i = 1; i <= cnt; i++)
                        Util.PrintReceipt(SellNo);
                }
                else if (Param.SystemConfig.Bill.PrintType == "A")
                {
                    if (MessageBox.Show("คุณต้องการพิมพ์ใบเสร็จรับเงินหรือไม่ ?", "การพิมพ์", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        Util.PrintReceipt(SellNo);
                }

                lblCustomerName.Text = "ลูกค้าทั่วไป";
                Param.SelectCustomerSex = "";
                Param.SelectCustomerAge = 0;
                Param.SelectCustomerSellPrice = 0;
                SelectCustomer(sender, (e));
                LoadData();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Param.SelectCustomerId = "000000";
            Param.SelectCustomerName = "ลูกค้าทั่วไป";
            Param.SelectCustomerSex = "";
            Param.SelectCustomerAge = 0;
            Param.SelectCustomerSellPrice = 0;
            FmSelectCustomer ul = new FmSelectCustomer();
            var result = ul.ShowDialog(this);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                SelectCustomer(sender, e);
            }
        }

        private void SelectCustomer(object sender, EventArgs e)
        {
            lblCustomerName.Text = Param.SelectCustomerName;
            if (Param.SelectCustomerSex != "")
            {
                if (Param.SelectCustomerSex == "M")
                    btnMan_Click(sender, e);
                else
                    btnWoman_Click(sender, e);
                btnMan.Enabled = false;
                btnWoman.Enabled = false;
            }
            else
            {
                SetDefaultButton(btnMan);
                SetDefaultButton(btnWoman);
                btnMan.Enabled = true;
                btnWoman.Enabled = true;
            }

            if (Param.SelectCustomerAge != 0)
            {
                if (Param.SelectCustomerAge > 60)
                    btnAge6_Click(sender, e);
                else if (Param.SelectCustomerAge > 40)
                    btnAge5_Click(sender, e);
                else if (Param.SelectCustomerAge > 25)
                    btnAge4_Click(sender, e);
                else if (Param.SelectCustomerAge > 18)
                    btnAge3_Click(sender, e);
                else if (Param.SelectCustomerAge > 12)
                    btnAge2_Click(sender, e);
                else
                    btnAge1_Click(sender, e);
                btnAge1.Enabled = false;
                btnAge2.Enabled = false;
                btnAge3.Enabled = false;
                btnAge4.Enabled = false;
                btnAge5.Enabled = false;
                btnAge6.Enabled = false;
            }
            else
            {
                SetDefaultButton(btnAge1);
                SetDefaultButton(btnAge2);
                SetDefaultButton(btnAge3);
                SetDefaultButton(btnAge4);
                SetDefaultButton(btnAge5);
                SetDefaultButton(btnAge6);
                btnAge1.Enabled = true;
                btnAge2.Enabled = true;
                btnAge3.Enabled = true;
                btnAge4.Enabled = true;
                btnAge5.Enabled = true;
                btnAge6.Enabled = true;
            }
            LoadData();
        }
    }
}
