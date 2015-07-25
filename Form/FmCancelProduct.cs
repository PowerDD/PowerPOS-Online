﻿using System;
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
    public partial class FmCancelProduct : Form
    {
        public FmCancelProduct()
        {
            InitializeComponent();
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                lblStatus.Visible = false;
                DataTable dt = Util.DBQuery(string.Format(@"SELECT p.Name, p.ID, IFNULL(p.Price, 0) Price, IFNULL(p.Price1, 0) Price1, IFNULL(p.Price2, 0) Price2, ReceivedDate, ReceivedBy, SellDate, SellBy 
                    FROM Barcode b 
                        LEFT JOIN Product p 
                        ON b.Product = p.ID 
                    WHERE Barcode = '{0}' AND Shop = '{1}' AND SellBy <> '' ", txtBarcode.Text, Param.ShopId));

                if (dt.Rows.Count == 1)
                {
                    Util.DBExecute(string.Format(@"UPDATE Barcode SET SellBy = '', Sync = 1 WHERE SellBy = '{0}' AND Barcode = '{1}'", Param.CpuId, txtBarcode.Text));
                    Param.UcSell.LoadData();
                    txtBarcode.Text = "";

                }
                else
                {
                    txtBarcode.Text = "";
                    lblStatus.Visible = true;           
                }
            }
        }

        private void FmCancelProduct_Load(object sender, EventArgs e)
        {
            lblStatus.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
