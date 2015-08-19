using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XPTable.Models;

namespace PowerPOS_Online
{
    public partial class FmStockDetail : Form
    {
        private bool _FIRST_LOAD;

        public FmStockDetail()
        {
            InitializeComponent();
        }

        private void FmStockDetail_Load(object sender, EventArgs e)
        {
            Util.InitialTable(table1);
            _FIRST_LOAD = true;
            LoadData();
        }

        private void LoadData()
        {
            lblProduct.Text = UcStock.productNo;
            lblProductName.Text = UcStock.ProductName;
            _FIRST_LOAD = false;
            SearchData();
        }

        private void SearchData()
        {
            if (!_FIRST_LOAD)
            {
                DataTable dt = Util.DBQuery(string.Format(@"SELECT Barcode,OrderNo,Stock,SellDate
                    FROM Barcode 
                    WHERE (SellDate IS NULL OR SellDate = '') AND Product = '{0}'
                    ORDER BY OrderNo
                ", UcStock.productNo));
                table1.BeginUpdate();
                tableModel1.Rows.Clear();
                tableModel1.RowHeight = 22;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Stock"].ToString() == "0" || dt.Rows[i]["Stock"].ToString() == "")
                    {
                        tableModel1.Rows.Add(new Row(
                            new Cell[] {
                        new Cell("",global::PowerPOS_Online.Properties.Resources.clock),
                        new Cell("" + (i+1)),
                        new Cell(dt.Rows[i]["Barcode"].ToString()),
                        new Cell(dt.Rows[i]["OrderNo"].ToString())
                        }));
                    }
                    else
                    {
                        tableModel1.Rows.Add(new Row(
                            new Cell[] {
                        new Cell("",global::PowerPOS_Online.Properties.Resources.accept),
                        new Cell("" + (i+1)),
                        new Cell(dt.Rows[i]["Barcode"].ToString()),
                        new Cell(dt.Rows[i]["OrderNo"].ToString())
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
