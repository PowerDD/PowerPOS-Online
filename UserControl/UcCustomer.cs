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
    public partial class UcCustomer : UserControl
    {
        public UcCustomer()
        {
            InitializeComponent();
        }

        private void UcCustomer_Load(object sender, EventArgs e)
        {
            Util.InitialTable(table1);
            LoadData();
        }

        private void LoadData()
        {
            DataTable dt = Util.DBQuery(string.Format(@"SELECT * FROM Customer
                WHERE Firstname LIKE '%{0}%'
                OR Lastname LIKE '%{0}%'
                OR Nickname LIKE '%{0}%'
                OR CitizenID LIKE '%{0}%'
                OR CardNo LIKE '%{0}%'
                OR Mobile LIKE '%{0}%'
                OR ShopName LIKE '%{0}%'
                ", 
                txtSearch.Text.Trim()));

            table1.BeginUpdate();
            tableModel1.Rows.Clear();
            tableModel1.RowHeight = 22;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["ID"].ToString() != "")
                {
                    var mobile = (dt.Rows[i]["Mobile"].ToString().Length == 10) ? dt.Rows[i]["Mobile"].ToString().Substring(0, 3) + "-" +
                        dt.Rows[i]["Mobile"].ToString().Substring(3, 4) + "-" +
                        dt.Rows[i]["Mobile"].ToString().Substring(7, 3)
                        : dt.Rows[i]["Mobile"].ToString();
                    var citizen = (dt.Rows[i]["CitizenID"].ToString().Length == 13) ? dt.Rows[i]["CitizenID"].ToString().Substring(0, 1) + "-" +
                        dt.Rows[i]["CitizenID"].ToString().Substring(1, 4) + "-" +
                        dt.Rows[i]["CitizenID"].ToString().Substring(5, 5) + "-" +
                        dt.Rows[i]["CitizenID"].ToString().Substring(10, 2) + "-" +
                        dt.Rows[i]["CitizenID"].ToString().Substring(12, 1)
                        : dt.Rows[i]["CitizenID"].ToString();

                    tableModel1.Rows.Add(new Row(
                        new Cell[] {
                        new Cell(dt.Rows[i]["ID"].ToString()),
                        new Cell(dt.Rows[i]["Firstname"].ToString()),
                        new Cell(dt.Rows[i]["Lastname"].ToString()),
                        new Cell(dt.Rows[i]["Nickname"].ToString()),
                        new Cell(dt.Rows[i]["ShopName"].ToString()),
                        new Cell(dt.Rows[i]["SellPrice"].ToString() == "0" ? "ปลีก" : "ส่ง "+dt.Rows[i]["SellPrice"].ToString()),
                        new Cell(dt.Rows[i]["Credit"].ToString() == "0" ? "-" : dt.Rows[i]["Credit"].ToString()+" วัน"),
                        new Cell(dt.Rows[i]["CardNo"].ToString()),
                        new Cell(mobile),
                        new Cell(citizen)
                        }));
                }
            }
            table1.EndUpdate();

            lblRecords.Text = dt.Rows.Count.ToString("#,##0");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            LoadData();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            FmAddCustomer ul = new FmAddCustomer();
            var result = ul.ShowDialog(this);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                LoadData();
            }
        }

        private void table1_CellDoubleClick(object sender, XPTable.Events.CellMouseEventArgs e)
        {
            if(e.Row < table1.RowCount){

                FmAddCustomer ul = new FmAddCustomer();
                ul.LoadCustomerData(sender, e, "Mobile", tableModel1.Rows[e.Row].Cells[8].Text.Replace("-", ""));
                var result = ul.ShowDialog(this);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
    }
}
