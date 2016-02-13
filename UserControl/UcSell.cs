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
        int row = -1;
        public static string productNo;
        public static string OrderNo;
        public static string barcode;
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
            txtBarcode.Focus();
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
            try
            {
                //if (txtBarcode.Text != "")
                //{
                    lblStatus.Visible = false;
                    Param.BarcodeNo = txtBarcode.Text;
                    if (e.KeyCode == Keys.Return)
                    {
                        barcode = txtBarcode.Text;

                        DataTable dt = Util.DBQuery(string.Format(@"SELECT p.Name, p.ID, IFNULL(p.Price, 0) Price, IFNULL(p.Price1, 0) Price1, IFNULL(p.Price2, 0) Price2, ReceivedDate, ReceivedBy, SellDate, SellBy, Comment 
                    FROM Barcode b 
                        LEFT JOIN Product p 
                        ON b.Product = p.ID 
                    WHERE b.Barcode = '{0}' AND p.Shop = '{1}'", txtBarcode.Text, Param.ShopId));
                        lblStatus.Visible = true;

                        //string[] claim = dt.Rows[0]["Comment"].ToString().Split('(');
                        //string[] change = dt.Rows[0]["Comment"].ToString().Split('[');


                        if (dt.Rows.Count == 0)
                        {
                            dt = Util.DBQuery(string.Format(@"SELECT Barcode FROM Product WHERE Barcode LIKE '%{0}%'", txtBarcode.Text));
                            Console.WriteLine(txtBarcode.Text + "" + Param.BarcodeNo + "" + dt.Rows.Count.ToString());
                            if (dt.Rows.Count == 0)
                            {
                                lblStatus.Text = "ไม่พบข้อมูลสินค้าชิ้นนี้ในระบบ";
                                lblStatus.ForeColor = Color.Red;
                            }
                            else
                            {
                                Param.status = "Sell";
                                FmSelectProduct frm = new FmSelectProduct();
                                var result = frm.ShowDialog(this);
                                if (result == System.Windows.Forms.DialogResult.OK)
                                {
                                    LoadData();
                                }

                            }
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
                        //else if (claim[0].ToString() == "เคลมสินค้า" || change[0].ToString() == "เปลี่ยนสินค้า")
                        //{
                        //    lblStatus.Text = "สินค้าชิ้นนี้ได้ทำการเคลมแล้ว";
                        //    lblStatus.ForeColor = Color.Red;
                        //}
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
                            Util.DBExecute(string.Format(@"UPDATE Barcode SET SellPrice = (SELECT p.Price{3} FROM Product p
                            WHERE Barcode.product = p.ID AND p.shop = '{2}'), SellBy = '{0}', Sync = 1 WHERE Barcode = '{1}'", Param.CpuId, txtBarcode.Text, Param.ShopId, Param.SelectCustomerSellPrice == 0 ? "" : "" + Param.SelectCustomerSellPrice));
                            LoadData();
                            txtBarcode.Focus();
                            lblStatus.Text = "เพิ่มสินค้าในรายการขายแล้ว";
                            lblStatus.ForeColor = Color.Green;
                        }

                        txtBarcode.Text = "";
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        btnConfirm_Click(sender, (e));
                    }
                    else if (e.KeyCode == Keys.F11)
                    {
                        btnCancelProduct_Click(sender, (e));
                    }
                    else if (e.KeyCode == Keys.F12)
                    {
                        btnCancel_Click(sender, (e));
                    }
                //}
                //else
                //{
                //    MessageBox.Show("กรุณากรอกข้อมูลบาร์โค้ด", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void LoadData()
        {
            Util.DBExecute(string.Format(@"UPDATE Barcode SET SellPrice = (SELECT p.Price{3} FROM Product p
                            WHERE Barcode.product = p.ID AND p.shop = '{2}'), Sync = 1 WHERE SellBy = '{0}'", Param.CpuId, barcode, Param.ShopId, Param.SelectCustomerSellPrice == 0 ? "" : "" + Param.SelectCustomerSellPrice));


            DataTable dt = Util.DBQuery(@"SELECT product, productName, price, amount FROM sellTemp");
            if (dt.Rows.Count > 0)
            {

                Util.DBExecute(string.Format(@"UPDATE sellTemp  SET
                    Price = (SELECT p.Price{0} FROM Product p
                    WHERE sellTemp.product = p.ID
                    AND p.shop = '{1}'),
                    TotalPrice = (SELECT p.Price{0} FROM Product p
                    WHERE sellTemp.product = p.ID
                    AND p.shop = '{1}') * sellTemp.Amount,
                    PriceCost = (SELECT p.Cost FROM Product p
                    WHERE sellTemp.product = p.ID
                    AND p.shop = '{1}') * sellTemp.Amount", Param.SelectCustomerSellPrice == 0 ? "" : "" + Param.SelectCustomerSellPrice, Param.ShopId));
            }

            dt = Util.DBQuery(string.Format(@"SELECT p.ID, p.Name, b.sellPrice Price, b.ProductCount
                    FROM (SELECT Product, COUNT(*) ProductCount, sellPrice FROM Barcode WHERE SellBy = '{0}' GROUP BY Product,Sellprice) b 
                        LEFT JOIN Product p 
                        ON b.Product = p.ID
                    WHERE Shop = '{1}'
                       UNION ALL
                    SELECT product, productName, price, amount FROM sellTemp", Param.CpuId, Param.ShopId, Param.SelectCustomerSellPrice == 0 ? "" : "" + Param.SelectCustomerSellPrice));

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
            try
            {
                if (table1.RowCount > 0)
                {
                    var confirm = MessageBox.Show("คุณแน่ใจหรือไม่\nที่จะยกเลิกรายการขายนี้", "ยืนยันการทำงาน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
                    if (confirm)
                    {
                        DataTable dt = Util.DBQuery(@"SELECT Product FROM SellTemp");
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                Util.DBExecute(string.Format(@"UPDATE Product SET Quantity = (SELECT st.Amount FROM SellTemp st WHERE st.Product = Product.id ) + Quantity, Sync = 1  WHERE shop = '{0}' AND ID = '{1}'", Param.ShopId, dt.Rows[i][0].ToString()));
                            }
                        }
                        Util.DBExecute(string.Format(@"UPDATE Barcode SET SellPrice = '0',SellBy = '', Sync = 1 WHERE SellBy = '{0}'", Param.CpuId));
                        Util.DBExecute(string.Format(@"DELETE FROM SellTemp"));
                        LoadData();

                    }
                    else
                    {
                        MessageBox.Show("ไม่มีสินค้าในรายการขาย", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }                
        }

        private void btnCancelProduct_Click(object sender, EventArgs e)
        {
            if (table1.RowCount > 0)
            {
                FmCancelProduct frm = new FmCancelProduct();
                frm.Show();
                lblStatus.Visible = false;
            }
            else
            {
                MessageBox.Show("ไม่มีสินค้าในรายการขาย", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (table1.RowCount > 0)
            {
                string total = lblPrice.Text.Replace(",", "");
                if (MessageBox.Show("คุณแน่ใจหรือไม่ ที่จะยืนยันการขายนี้ ?", "ยืนยันข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    DataTable dt = Util.DBQuery(string.Format(@"SELECT IFNULL(SUBSTR(MAX(SellNo), 1, 5)||SUBSTR('0000'||(SUBSTR(MAX(SellNo), 6, 4)+1), -4, 4), SUBSTR(STRFTIME('%Y%m{0}'), 3)||'0001') SellNo
                    FROM SellHeader
                    WHERE SUBSTR(SellNo, 1, 4) = SUBSTR(STRFTIME('%Y%m'), 3, 4)
                    AND SUBSTR(SellNo, 5, 1) = '{0}'", Param.DevicePrefix));
                    var SellNo = dt.Rows[0]["SellNo"].ToString();

                    //dt = Util.DBQuery(string.Format(@"SELECT b.Barcode, b.SellNo, p.Price{0} Price
                    //FROM Barcode b
                    //    LEFT JOIN Product p
                    //    ON b.product = p.id
                    //WHERE p.Shop = '{1}' AND b.sellBy = '{2}'", Param.SelectCustomerSellPrice == 0 ? "" : "" + Param.SelectCustomerSellPrice, Param.ShopId, Param.CpuId));

                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    Util.DBExecute(string.Format(@"UPDATE Barcode
                    //    SET SellBy = '{0}', SellDate = STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), SellNo = '{1}', Sync = 1, SellPrice = {2}, Customer = '{3}', SellFinished = 1
                    //    WHERE Barcode = '{4}' AND SellNo = '{5}'",
                    //        Param.UserId, SellNo, dt.Rows[i]["Price"].ToString(), Param.SelectCustomerId,
                    //        dt.Rows[i]["Barcode"].ToString(), dt.Rows[i]["SellNo"].ToString()));
                    //}

                    //DataTable dtP = Util.DBQuery(string.Format(@"SELECT IFNULL(SUM(SellPrice),0) SellPrice, IFNULL(SUM(Cost),0) Cost, IFNULL(SUM(OperationCost),0) OperationCost FROM Barcode WHERE SellNo = '{0}'", SellNo));
                    //DataTable dtS = Util.DBQuery(string.Format(@"SELECT IFNULL(SUM(Price),0) SellPrice, IFNULL(SUM(PriceCost),0) Cost FROM sellTemp"));

                    //int val = ((Convert.ToInt32(dtP.Rows[0]["SellPrice"].ToString()) + Convert.ToInt32(dtS.Rows[0]["SellPrice"].ToString())) - (Convert.ToInt32(dtP.Rows[0]["Cost"].ToString()) + Convert.ToInt32(dtS.Rows[0]["Cost"].ToString()))) - Convert.ToInt32(dtP.Rows[0]["OperationCost"].ToString());

                    //Util.DBExecute(string.Format(@"INSERT INTO SellHeader (SellNo, Profit, TotalPrice, Customer, CustomerSex, CustomerAge, SellDate, SellBy)
                    //SELECT '{0}', '{6}','{5}', '{1}', '{2}', {3}, STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), '{4}'",
                    //    SellNo, Param.SelectCustomerId, Param.SelectCustomerSex, Param.SelectCustomerAge, Param.UserId, total, val));


                    //Util.DBExecute(string.Format(@"INSERT INTO SellDetail (SellNo, Product, SellPrice, Cost, Quantity, Sync)
                    //SELECT sellNo, Product, SUM(TotalPrice)TotalPrice, SUM(PriceCost) PriceCost, SUM(Amount) Amount,1 FROM(
                    //   SELECT  '{0}' sellNo, Product, SUM(SellPrice) TotalPrice, SUM(Cost) PriceCost, COUNT(*) Amount, 1 FROM Barcode WHERE SellNo = '{0}' GROUP BY Product
                    //   Union ALL SELECT  '{0}', Product, TotalPrice, PriceCost, Amount, 1 FROM SellTemp) GROUP BY sellNo, Product", SellNo));


                    //Util.DBExecute(string.Format(@"INSERT INTO SellDetail (SellNo, Product, SellPrice, Quantity, Cost, Sync)
                    //SELECT '{0}', Product, TotalPrice, Amount, PriceCost, 1 FROM SellTemp", SellNo));


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

                    //Param.SelectCustomerId = "000000";
                    //Param.SelectCustomerName = "ลูกค้าทั่วไป";
                    //Param.SelectCustomerSex = "";
                    //Param.SelectCustomerAge = 0;
                    //Param.SelectCustomerSellPrice = 0;
                    lblStatus.Text = "";
                    //if (Param.SystemConfig.Bill.PrintType == "Y")
                    //{
                    //    var cnt = int.Parse(Param.SystemConfig.Bill.PrintCount.ToString());
                    //    for (int i = 1; i <= cnt; i++)
                    //        Util.PrintReceipt(SellNo);
                    //}
                    //else if (Param.SystemConfig.Bill.PrintType == "A")
                    //{
                    //    if (MessageBox.Show("คุณต้องการพิมพ์ใบเสร็จรับเงินหรือไม่ ?", "ยืนยันการพิมพ์", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //        Util.PrintReceipt(SellNo);
                    //}

                    lblCustomerName.Text = "ลูกค้าทั่วไป";
                    Param.SelectCustomerSex = "";
                    Param.SelectCustomerAge = 0;
                    Param.SelectCustomerSellPrice = 0;
                    SelectCustomer(sender, (e));
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("ไม่มีสินค้าในรายการขาย", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void table1_CellDoubleClick(object sender, XPTable.Events.CellMouseEventArgs e)
        {
            if (e.Row < table1.RowCount)
            {
                row = e.Row;
            }
            if (row != -1)
            {

                productNo = tableModel1.Rows[row].Cells[1].Text;
                //if (cbbOrder.SelectedIndex == 0) { OrderNo = cbbOrderNo.SelectedItem.ToString(); } else { OrderNo = cbbOrder.SelectedItem.ToString(); }
                //ProductName = tableModel1.Rows[row].Cells[2].Text;
                FmChangeSellPrice frm = new FmChangeSellPrice();
                var result = frm.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("กรุณาเลือกรายการที่ต้องการ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
