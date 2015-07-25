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

        }
    }
}
