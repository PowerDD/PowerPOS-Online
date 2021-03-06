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
    public partial class FmProfitConfig : Form
    {
        public FmProfitConfig()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Util.DBExecute(string.Format(@"INSERT OR REPLACE INTO CategoryProfit (id, price, price1, price2, Sync) VALUES ((SELECT id FROM Category WHERE LOWER(name) = '{3}'), {0}, {1}, {2}, 1)",
                nudPrice.Value, nudPrice1.Value, nudPrice2.Value, cbCategory.SelectedItem.ToString().ToLower()));
            btnSave.Enabled = false;
        }

        private void nudPrice_ValueChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            nudPrice.Enabled = cbCategory.SelectedIndex != 0;
            nudPrice1.Enabled = cbCategory.SelectedIndex != 0;
            nudPrice2.Enabled = cbCategory.SelectedIndex != 0;

            if (cbCategory.SelectedIndex == 0)
            {
                nudPrice.Value = 0;
                nudPrice1.Value = 0;
                nudPrice2.Value = 0;
            }
            else
            {
                DataTable dt = Util.DBQuery(@"SELECT IFNULL(price,0) price, IFNULL(price1,0) price1, IFNULL(price2,0) price2 FROM Category c LEFT JOIN CategoryProfit p ON c.id = p.id WHERE LOWER(name) = '" + cbCategory.SelectedItem.ToString().ToLower() + "'");
                nudPrice.Value = int.Parse(dt.Rows[0]["price"].ToString());
                nudPrice1.Value = int.Parse(dt.Rows[0]["price1"].ToString());
                nudPrice2.Value = int.Parse(dt.Rows[0]["price2"].ToString());
                //nudPrice_ValueChanged(sender, e);
            }
            btnSave.Enabled = false;
        }

        private void FmProfitConfig_Load(object sender, EventArgs e)
        {
            DataTable dt = Util.DBQuery(@"SELECT DISTINCT name FROM  Category WHERE Shop = '" + Param.ShopId + "' ORDER BY Name");
            cbCategory.Items.Clear();
            cbCategory.Items.Add("หมวดหมู่");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbCategory.Items.Add(dt.Rows[i]["name"].ToString());
                if (Param.CategoryName != null && Param.CategoryName == dt.Rows[i]["name"].ToString())
                    cbCategory.SelectedIndex = i + 1;
            }
            if (Param.CategoryName == null)
                cbCategory.SelectedIndex = 0;
            Param.CategoryName = null;
        }
    }
}
