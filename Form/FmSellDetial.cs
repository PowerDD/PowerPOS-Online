using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
            Util.InitialTable(table1);

            DataTable dt = Util.DBQuery(string.Format(@" SELECT p.ID, p.Name, sd.Quantity, sd.SellPrice, sh.SellNo, sh.SellDate, sh.TotalPrice, c.Firstname, c.Lastname
                     FROM  SellDetail sd
                        LEFT JOIN Product p 
                        ON sd.Product = p.ID
                         LEFT JOIN SellHeader sh
                        ON sd.SellNo = sh.SellNo
                        LEFT JOIN Customer c
                        ON sh.Customer = c.ID
                    WHERE Shop = '{0}' AND sd.SellNo = '{1}'", Param.ShopId, UcReport.sellNo));

            var sumPrice = 0;
            lblCustomer.Text = dt.Rows[0]["Firstname"].ToString() + " " + dt.Rows[0]["Lastname"].ToString();
            lblSellDate.Text = Convert.ToDateTime(dt.Rows[0]["SellDate"].ToString()).ToLocalTime().ToString("dd-MM-yyyy HH:mm:ss");
            lblSellNo.Text = dt.Rows[0]["SellNo"].ToString();
            lblTotal.Text = double.Parse(dt.Rows[0]["TotalPrice"].ToString()).ToString("#,##0.00");
            table1.BeginUpdate();
            tableModel1.Rows.Clear();
            tableModel1.RowHeight = 22;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["ID"].ToString() != "")
                {
                    if (int.Parse(dt.Rows[i]["Quantity"].ToString()) != 0)
                        {
                        var total = int.Parse(dt.Rows[i]["SellPrice"].ToString()) / int.Parse(dt.Rows[i]["Quantity"].ToString());
                        tableModel1.Rows.Add(new Row(
                            new Cell[] {
                        new Cell("" + (i + 1)),
                        new Cell(dt.Rows[i]["ID"].ToString()),
                        new Cell(dt.Rows[i]["Name"].ToString()),
                        new Cell(total),
                        new Cell(int.Parse(dt.Rows[i]["Quantity"].ToString())),
                        new Cell(int.Parse(dt.Rows[i]["SellPrice"].ToString()))}));
                        sumPrice += total;
                    }
                }
            }
            table1.EndUpdate();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
