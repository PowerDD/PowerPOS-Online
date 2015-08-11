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
    public partial class FmOrderDetail : Form
    {
        int row = -1;
        private bool _FIRST_LOAD;

        public FmOrderDetail()
        {
            InitializeComponent();
        }

        private void FmOrderDetail_Load(object sender, EventArgs e)
        {
            Util.InitialTable(table1);
            _FIRST_LOAD = true;
            LoadData();
        }

        private void LoadData()
        {
            lblOrderNo.Text = UcReceiveProduct.OrderNo;
            lblProduct.Text = UcReceiveProduct.productNo;
            lblProductName.Text = UcReceiveProduct.ProductName;
         
            _FIRST_LOAD = false;
            SearchData();
        }

        private void SearchData()
        {
            if (!_FIRST_LOAD)
            {
                DataTable dt = Util.DBQuery(string.Format(@"
                    SELECT Barcode,ReceivedDate
                    FROM Barcode 
                    WHERE OrderNo = '{0}'
                        AND Product = '{1}'
                ", UcReceiveProduct.OrderNo,UcReceiveProduct.productNo));
                table1.BeginUpdate();
                tableModel1.Rows.Clear();
                tableModel1.RowHeight = 22;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ReceivedDate"].ToString() == "")
                    {
                        tableModel1.Rows.Add(new Row(
                            new Cell[] {
                        new Cell("",global::PowerPOS_Online.Properties.Resources.clock),
                        new Cell("" + (i+1)),
                        new Cell(dt.Rows[i]["Barcode"].ToString()),
                        }));
                    }
                    else 
                    {
                        tableModel1.Rows.Add(new Row(
                            new Cell[] {
                        new Cell("",global::PowerPOS_Online.Properties.Resources.accept),
                        new Cell("" + (i+1)),
                        new Cell(dt.Rows[i]["Barcode"].ToString()),
                        }));
                    }
                }
                table1.EndUpdate();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
