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
    public partial class FmSellDetial : Form
    {
        public FmSellDetial()
        {
            InitializeComponent();
        }

        private void FmSellDetial_Load(object sender, EventArgs e)
        {
            DataTable dt = Util.DBQuery(string.Format(@"SELECT p.ID, p.Name, p.Price{2} Price, ProductCount, sd.SellPrice, sh.SellNo, sh.SellDate, sh.TotalPrice, c.Firstname, c.Lastname
                    FROM (SELECT Product, COUNT(*) ProductCount,SellNo FROM Barcode WHERE SellBy = '{0}' AND SellNo = '{3}' GROUP BY Product,SellNo) b 
                        LEFT JOIN Product p 
                        ON b.Product = p.ID
                        LEFT JOIN SellDetail sd
                        ON b.Product = sd.Product
                        LEFT JOIN SellHeader sh
                        ON sd.SellNo = sh.SellNo
                        LEFT JOIN Customer c
                        ON sh.Customer = c.ID
                    WHERE Shop = '{1}' AND sd.SellNo = '{3}'", Param.UserId, Param.ShopId, Param.SelectCustomerSellPrice == 0 ? "" : "" + Param.SelectCustomerSellPrice, UcReport.sellNo));

            var sumPrice = 0;
            lblCustomer.Text = dt.Rows[0]["Firstname"].ToString() + " " + dt.Rows[0]["Lastname"].ToString();
            lblSellDate.Text = dt.Rows[0]["SellDate"].ToString();
            lblSellNo.Text = dt.Rows[0]["SellNo"].ToString();
            lblTotal.Text = dt.Rows[0]["TotalPrice"].ToString();
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

        }
    }
}
