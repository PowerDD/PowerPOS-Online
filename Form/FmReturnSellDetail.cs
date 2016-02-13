using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XPTable.Models;

namespace PowerPOS_Online
{
    public partial class FmReturnSellDetail : Form
    {
        public static string Pid;
        public static string sellN;
        public static string sellP;
        public static string costProduct;
        public static string costPrice;
        public static string customer;
        public static string sellDate;
        int row = -1;
        DataTable dt;

        public FmReturnSellDetail()
        {
            InitializeComponent();
        }

        private void ReturnSellDetail_Load(object sender, EventArgs e)
        {
            try
            {
                Util.InitialTable(table1);


                dt = Util.DBQuery(string.Format(@"SELECT sd.SellNo, sh.SellDate , c.firstname|| ' ' || c.lastname Customer, sd.Quantity, sd.SellPrice , sd.SellPrice/sd.Quantity Price, 
                    sd.Cost, sd.Cost/sd.Quantity PriceCost, sd.Product, p.Name
                    FROM SellDetail sd
                    LEFT JOIN SellHeader sh
                    ON sd.SellNo = sh.SellNo
                    LEFT JOIN Customer c
                    ON c.id = sh.Customer
                    LEFT JOIN Product p
                    ON p.id = sd.Product
                    WHERE sd.Product = '{0}' 
                    AND sd.Quantity <> 0 
                    AND sh.SellDate BETWEEN '{2}' AND '{3}'
                    GROUP BY sd.Product, sd.SellNo ORDER BY sd.SellNo DESC", FmSelectProduct.product, Param.ShopParent, 
                    DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")));

                lblProduct.Text = dt.Rows[0]["Product"].ToString();
                lblName.Text = dt.Rows[0]["Name"].ToString();

                table1.BeginUpdate();
                tableModel1.Rows.Clear();
                tableModel1.RowHeight = 22;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tableModel1.Rows.Add(new Row(
                    new Cell[] {
                        new Cell("" + (i+1)),
                        new Cell(dt.Rows[i]["SellNo"].ToString()),
                        new Cell(dt.Rows[i]["SellDate"].ToString()),
                        new Cell(dt.Rows[i]["Customer"].ToString()),
                        new Cell(Convert.ToInt32(dt.Rows[i]["Price"].ToString())),
                        new Cell(Convert.ToInt32(dt.Rows[i]["Quantity"].ToString())),
                        new Cell(Convert.ToInt32(dt.Rows[i]["SellPrice"].ToString())),
                        new Cell(Convert.ToInt32(dt.Rows[i]["Cost"].ToString())),
                        new Cell(Convert.ToInt32(dt.Rows[i]["PriceCost"].ToString()))
                    }));
                }
                table1.EndUpdate();
            }
            catch
            { }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void table1_CellDoubleClick(object sender, XPTable.Events.CellMouseEventArgs e)
        {
            if (e.Row < table1.RowCount)
            {
                row = e.Row;
            }
            if (row != -1)
            {
                Pid = lblProduct.Text;
                sellN = tableModel1.Rows[row].Cells[1].Text;

                dt = Util.DBQuery(string.Format(@"SELECT sd.SellNo, sh.SellDate , c.firstname|| ' ' || c.lastname Customer, sd.Quantity, sd.SellPrice , sd.SellPrice/sd.Quantity Price, 
                    sd.Cost, sd.Cost/sd.Quantity PriceCost, sd.Product, p.Name
                    FROM SellDetail sd
                    LEFT JOIN SellHeader sh
                    ON sd.SellNo = sh.SellNo
                    LEFT JOIN Customer c
                    ON c.id = sh.Customer
                    LEFT JOIN Product p
                    ON p.id = sd.Product
                    WHERE sd.Product = '{0}' 
                    AND sd.Quantity <> 0 
                    AND sd.SellNo = '{1}'
                    GROUP BY sd.Product, sd.SellNo ORDER BY sd.SellNo DESC", Pid, sellN));

                sellP = dt.Rows[0]["Price"].ToString();
                costProduct = dt.Rows[0]["Cost"].ToString();
                costPrice = dt.Rows[0]["PriceCost"].ToString();
                customer = dt.Rows[0]["Customer"].ToString();
                sellDate = dt.Rows[0]["SellDate"].ToString();
                this.Close();
            }
            else
            {
                MessageBox.Show("กรุณาเลือกรายการที่ต้องการ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
