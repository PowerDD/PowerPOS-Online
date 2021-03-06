﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XPTable.Models;

namespace PowerPOS_Online
{
    public partial class FmSelectProduct : Form
    {
        public static string product;
        public static string amount;
        public FmSelectProduct()
        {
            InitializeComponent();
        }

        private void FmSelectProduct_Load(object sender, EventArgs e)
        {
            Util.InitialTable(table1);

            DataTable dt = Util.DBQuery(string.Format(@"SELECT ID, Name FROM Product WHERE Barcode LIKE '%{0}%' AND shop = '{1}' ORDER BY Name ", Param.BarcodeNo, Param.ShopParent));

            table1.BeginUpdate();
            tableModel1.Rows.Clear();
            tableModel1.RowHeight = 22;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                    tableModel1.Rows.Add(new Row(
                    new Cell[] {
                        new Cell("" + (i+1)),
                        new Cell(dt.Rows[i]["ID"].ToString()),
                        new Cell(dt.Rows[i]["Name"].ToString())
                    }));
            }
            table1.EndUpdate();
        }

        private void table1_CellClick(object sender, XPTable.Events.CellMouseEventArgs e)
        {
            if (table1.TableModel.Rows.Count > 0)
            {
                try
                {
                    var row = e.Row;
                    lblProductName.Text = table1.TableModel.Rows[row].Cells[2].Text;
                    product = table1.TableModel.Rows[row].Cells[1].Text;

                    if (Param.status == "Return")
                    {
                        FmReturnSellDetail frm = new FmReturnSellDetail();
                        var result = frm.ShowDialog(this);
                        //if (result == System.Windows.Forms.DialogResult.OK)
                        //{
                        //}
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblProductName.Text == "กรุณาเลือกสินค้าที่ต้องการ")
                {
                    MessageBox.Show("กรุณาเลือกสินค้าที่ต้องการ", "แจ้งเตือน");
                }
                else
                {
                    if (Param.status == "Received")
                    {
                        DataTable dt = Util.DBQuery(string.Format(@"SELECT Product, Quantity FROM PurchaseOrder WHERE product = '{0}' AND OrderNo = '{1}'", product, UcReceiveProduct.OrderNo));
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("ไม่พบข้อมูลสินค้าชิ้นนี้ในระบบ", "แจ้งเตือน");
                        }
                        else
                        {
                            if (txtAmount.Text.Trim() == "")
                            {
                                txtAmount.Focus();
                            }
                            else
                            {
                                if (Convert.ToInt32(dt.Rows[0]["Quantity"].ToString()) >= Convert.ToInt32(txtAmount.Text))
                                {
                                    if (MessageBox.Show("คุณแน่ใจหรือไม่ ที่จะทำการรับสินค้านี้ ?", "ยืนยันข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                                        dt = Util.DBQuery(string.Format(@"SELECT Quantity, ReceivedQty FROM PurchaseOrder WHERE Product = '{0}' AND OrderNo = '{1}'", product, UcReceiveProduct.OrderNo));

                                        int Received = Convert.ToInt32(dt.Rows[0]["ReceivedQty"].ToString()) + Convert.ToInt32(txtAmount.Text);

                                        if (Convert.ToInt32(dt.Rows[0]["Quantity"].ToString()) >= Received)
                                        {
                                            Util.DBExecute(string.Format(@"UPDATE PurchaseOrder SET ReceivedQty = '{0}', ReceivedDate = '{1}', ReceivedBy = '{2}', Sync = 1 WHERE product = '{3}' AND OrderNo = '{4}'",
                                                Received, DateTime.Now.ToString("yyyy-MM-dd"), Param.UserId, product, UcReceiveProduct.OrderNo));
                                            dt = Util.DBQuery(string.Format(@"SELECT id,IFNULL(Quantity,0) Quantity FROM Product WHERE id = '{0}' AND shop = '{1}'", product, Param.ShopId));

                                            int Amount = Convert.ToInt32(dt.Rows[0]["Quantity"].ToString()) + Convert.ToInt32(txtAmount.Text);

                                            dt = Util.DBQuery(string.Format(@"SELECT PriceCost FROM PurchaseOrder WHERE Product = '{0}'", product));

                                            Util.DBExecute(string.Format(@"UPDATE Product SET Quantity = '{0}', Cost = {3}, Sync = 1 WHERE id = '{1}' AND shop = '{2}'", Amount.ToString(), product, Param.ShopId, dt.Rows[0]["PriceCost"].ToString()));

                                            this.DialogResult = DialogResult.OK;
                                            this.Dispose();
                                        }
                                        else
                                        {
                                            MessageBox.Show("กรุณาตรวจสอบจำนวนที่ต้องการรับอีกครั้ง", "แจ้งเตือน");
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("กรุณาตรวจสอบจำนวนที่รับอีกครั้ง", "แจ้งเตือน");
                                }
                            }
                        }
                    }
                    else if (Param.status == "Sell")
                    {
                        DataTable dt = Util.DBQuery(string.Format(@"SELECT id, Quantity, Name FROM Product WHERE id = '{0}' AND shop = '{1}' GROUP BY id", product, Param.ShopId));

                        if (dt.Rows[0]["Quantity"].ToString() == "0")
                        {
                            MessageBox.Show("ไม่พบข้อมูลสินค้าชิ้นนี้ในระบบ", "แจ้งเตือน");
                        }
                        else
                        {
                            if (txtAmount.Text.Trim() == "")
                            {
                                txtAmount.Focus();
                            }
                            else
                            {

                                if (Convert.ToInt32(dt.Rows[0]["Quantity"].ToString()) >= Convert.ToInt32(txtAmount.Text))
                                {
                                    if (MessageBox.Show("คุณแน่ใจหรือไม่ ที่จะยืนยันการขายนี้ ?", "ยืนยันข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        dt = Util.DBQuery(string.Format(@"SELECT id, Name, Quantity, Price{2}, Cost FROM Product WHERE id = '{0}' AND shop = '{1}'", product, Param.ShopId, Param.SelectCustomerSellPrice == 0 ? "" : "" + Param.SelectCustomerSellPrice));

                                        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                                        Util.DBExecute(string.Format(@"INSERT INTO SellTemp (Product, ProductName, Price, Amount, TotalPrice, PriceCost) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                                            product, dt.Rows[0]["Name"].ToString(), dt.Rows[0][3].ToString(), txtAmount.Text.Trim(), Convert.ToInt32(dt.Rows[0][3].ToString()) * Convert.ToInt32(txtAmount.Text), dt.Rows[0]["Cost"].ToString()));

                                        int Amount = Convert.ToInt32(dt.Rows[0]["Quantity"].ToString()) - Convert.ToInt32(txtAmount.Text);

                                        Util.DBExecute(string.Format(@"UPDATE Product SET Quantity = '{0}', Sync = 1 WHERE id = '{1}' AND shop = '{2}'", Amount.ToString(), product, Param.ShopId));

                                        this.DialogResult = DialogResult.OK;
                                        this.Dispose();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("กรุณาตรวจสอบจำนวนที่ขายอีกครั้ง", "แจ้งเตือน");
                                }
                            }
                        }
                    }
                    else if (Param.status == "Return")
                    {
                        DataTable dt = Util.DBQuery(string.Format(@"SELECT Product, Quantity FROM SellDetail WHERE Product = '{0}' GROUP BY Product", product));

                        if (dt.Rows[0]["Quantity"].ToString() == "0")
                        {
                            MessageBox.Show("ไม่พบข้อมูลสินค้าชิ้นนี้ในระบบ", "แจ้งเตือน");
                        }
                        else
                        {
                            if (txtAmount.Text.Trim() == "")
                            {
                                txtAmount.Focus();
                            }
                            else
                            {
                                if (Convert.ToInt32(dt.Rows[0]["Quantity"].ToString()) >= Convert.ToInt32(txtAmount.Text))
                                {
                                    amount = txtAmount.Text;

                                    this.DialogResult = DialogResult.OK;
                                    this.Dispose();
                                }
                                else
                                {
                                    MessageBox.Show("กรุณาตรวจสอบจำนวนที่คืนอีกครั้ง", "แจ้งเตือน");
                                }
                            }
                        }
                    }
                    else if (Param.status == "Cancel")
                    {
                        DataTable dt = Util.DBQuery(string.Format(@"SELECT Product, Amount, Price FROM SellTemp WHERE product = '{0}' ", product, Param.ShopId));

                        if (dt.Rows[0]["Amount"].ToString() == "0")
                        {
                            MessageBox.Show("ไม่พบข้อมูลสินค้าชิ้นนี้ในรายการขาย", "แจ้งเตือน");
                        }
                        else
                        {
                            if (txtAmount.Text.Trim() == "")
                            {
                                txtAmount.Focus();
                            }
                            else
                            {
                                if (Convert.ToInt32(dt.Rows[0]["Amount"].ToString()) >= Convert.ToInt32(txtAmount.Text))
                                {
                                    int qty = Convert.ToInt32(dt.Rows[0]["Amount"].ToString()) - Convert.ToInt32(txtAmount.Text);
                                    int price = Convert.ToInt32(dt.Rows[0]["Price"].ToString()) * Convert.ToInt32(txtAmount.Text);

                                    if (qty > 0)
                                    {
                                        Util.DBExecute(string.Format(@"UPDATE SellTemp SET Amount = '{0}', TotalPrice = '{2}' WHERE product = '{1}'", qty, product, price));
                                    }
                                    else
                                    {
                                        Util.DBExecute(string.Format(@"DELETE FROM SellTemp WHERE Product = '{0}'", product));
                                    }

                                    this.DialogResult = DialogResult.OK;
                                    this.Dispose();
                                }
                                else
                                {
                                    MessageBox.Show("กรุณาตรวจสอบจำนวนที่ยกเลิกอีกครั้ง", "แจ้งเตือน");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    btnSave_Click(sender, (e));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void txtAmount_Enter(object sender, EventArgs e)
        {
            Util.SetKeyboardLayout(Util.GetInputLanguageByName("US"));
        }
    }
}
