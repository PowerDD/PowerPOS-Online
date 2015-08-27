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
using System.Drawing.Imaging;
using System.Threading;
using System.Globalization;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace PowerPOS_Online
{
    public partial class UcReport : UserControl
    {
        public static string sellNo;
        int row = -1;
        public UcReport()
        {
            InitializeComponent();
        }

        private void UcReport_Load(object sender, EventArgs e)
        {
            Util.InitialTable(table1);
            cbbReportType.SelectedIndex = 0;
            LoadData();
        }

        private void LoadData()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            DataTable dt = Util.DBQuery(string.Format(@"
                SELECT h.SellDate, h.SellNo, c.Firstname, c.Lastname, c.Mobile, h.Profit, h.TotalPrice, h.Paid
                FROM SellHeader h
                LEFT JOIN Customer c
                ON h.Customer = c.ID
                WHERE SellDate LIKE '{0}%'
                  AND h.Customer <> 'Z000000'
                  AND (h.Comment <> 'คืนสินค้า' OR h.Comment IS Null)   
                ORDER BY SellDate DESC
            ", dtpStartDate.Value.ToString("yyyy-MM-dd") ));


            table1.BeginUpdate();
            tableModel1.Rows.Clear();
            tableModel1.RowHeight = 22;
            var sumPrice = 0.0;
            var sumProfit = 0.0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var mobile = (dt.Rows[i]["Mobile"].ToString().Length == 10) ? dt.Rows[i]["Mobile"].ToString().Substring(0, 3) + "-" +
                    dt.Rows[i]["Mobile"].ToString().Substring(3, 4) + "-" +
                    dt.Rows[i]["Mobile"].ToString().Substring(7, 3)
                    : dt.Rows[i]["Mobile"].ToString();

                tableModel1.Rows.Add(new Row(
                    new Cell[] {
                        new Cell("" + (i+1)),
                        new Cell(Convert.ToDateTime(dt.Rows[i]["SellDate"].ToString()).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")),
                        new Cell(dt.Rows[i]["SellNo"].ToString()),
                        new Cell(dt.Rows[i]["Firstname"].ToString()+" "+dt.Rows[i]["Lastname"].ToString()),
                        new Cell(mobile == "" ? "-" : mobile),
                        new Cell(int.Parse(dt.Rows[i]["TotalPrice"].ToString()))
                        }));
                sumPrice += double.Parse(dt.Rows[i]["TotalPrice"].ToString());
                sumProfit += double.Parse(dt.Rows[i]["Profit"].ToString());
            }
            table1.EndUpdate();
            try
            {
                dt = Util.DBQuery(string.Format(@"SELECT SUM(TotalPrice) Total,COUNT(DISTINCT DATE(SellDate)) CountDate,(SUM(TotalPrice) /COUNT(DISTINCT DATE(SellDate)) ) AVG
                FROM SellHeader
                WHERE SellDate BETWEEN '{0}' AND '{1}'
            ", dtpStartDate.Value.AddDays(-30).ToString("yyyy-MM-dd"), dtpStartDate.Value.AddDays(1).ToString("yyyy-MM-dd")));
                var avg30 = double.Parse(dt.Rows[0]["AVG"].ToString());
            
            dt = Util.DBQuery(string.Format(@"
                SELECT IFNULL(SUM(TotalPrice),0) TotalPrice FROM SellHeader WHERE SUBSTR(SellDate, 1, 10) = '{0}'
                UNION ALL SELECT IFNULL(SUM(TotalPrice),0) TotalPrice FROM SellHeader WHERE SUBSTR(SellDate, 1, 10) = '{1}'
                UNION ALL SELECT IFNULL(SUM(TotalPrice),0) TotalPrice FROM SellHeader WHERE SUBSTR(SellDate, 1, 10) = '{2}'
                UNION ALL SELECT IFNULL(SUM(TotalPrice),0) TotalPrice FROM SellHeader WHERE SUBSTR(SellDate, 1, 10) = '{3}'
                UNION ALL SELECT IFNULL(SUM(TotalPrice),0) TotalPrice FROM SellHeader WHERE SUBSTR(SellDate, 1, 10) = '{4}'
                UNION ALL SELECT IFNULL(SUM(TotalPrice),0) TotalPrice FROM SellHeader WHERE SUBSTR(SellDate, 1, 10) = '{5}'
                UNION ALL SELECT IFNULL(SUM(TotalPrice),0) TotalPrice FROM SellHeader WHERE SUBSTR(SellDate, 1, 10) = '{6}'
                UNION ALL SELECT IFNULL(SUM(TotalPrice),0) TotalPrice FROM SellHeader WHERE SUBSTR(SellDate, 1, 10) = '{7}'
                UNION ALL SELECT IFNULL(SUM(TotalPrice),0) TotalPrice FROM SellHeader WHERE SUBSTR(SellDate, 1, 10) = '{8}'
                UNION ALL SELECT IFNULL(SUM(TotalPrice),0) TotalPrice FROM SellHeader WHERE SUBSTR(SellDate, 1, 10) = '{9}'
                UNION ALL SELECT IFNULL(SUM(TotalPrice),0) TotalPrice FROM SellHeader WHERE SUBSTR(SellDate, 1, 10) = '{10}'
                UNION ALL SELECT IFNULL(SUM(TotalPrice),0) TotalPrice FROM SellHeader WHERE SUBSTR(SellDate, 1, 10) = '{11}'
                UNION ALL SELECT IFNULL(SUM(TotalPrice),0) TotalPrice FROM SellHeader WHERE SUBSTR(SellDate, 1, 10) = '{12}'
                UNION ALL SELECT IFNULL(SUM(TotalPrice),0) TotalPrice FROM SellHeader WHERE SUBSTR(SellDate, 1, 10) = '{13}'
            ", dtpStartDate.Value.AddDays(-13).ToString("yyyy-MM-dd"), dtpStartDate.Value.AddDays(-12).ToString("yyyy-MM-dd"), dtpStartDate.Value.AddDays(-11).ToString("yyyy-MM-dd")
             , dtpStartDate.Value.AddDays(-10).ToString("yyyy-MM-dd"), dtpStartDate.Value.AddDays(-9).ToString("yyyy-MM-dd"), dtpStartDate.Value.AddDays(-8).ToString("yyyy-MM-dd")
             , dtpStartDate.Value.AddDays(-7).ToString("yyyy-MM-dd"), dtpStartDate.Value.AddDays(-6).ToString("yyyy-MM-dd"), dtpStartDate.Value.AddDays(-5).ToString("yyyy-MM-dd")
             , dtpStartDate.Value.AddDays(-4).ToString("yyyy-MM-dd"), dtpStartDate.Value.AddDays(-3).ToString("yyyy-MM-dd"), dtpStartDate.Value.AddDays(-2).ToString("yyyy-MM-dd")
             , dtpStartDate.Value.AddDays(-1).ToString("yyyy-MM-dd"), dtpStartDate.Value.ToString("yyyy-MM-dd") ));

            double max = 0;
            List<double> chart = new List<double>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                chart.Add(double.Parse(dt.Rows[i]["TotalPrice"].ToString()));
                if (chart[i] > max) max = chart[i];
            }

            DrawImage(sumPrice, sumProfit, avg30, max, chart);
            }
            catch { }
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void DrawImage(double sumPrice, double sumProfit, double avgPrice, double max, List<double> chart)
        {
            pictureBox1.Image = new Bitmap(Properties.Resources.daily);
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");

                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality; 
                g.TextRenderingHint = TextRenderingHint.AntiAlias;

                Font stringFont = new Font("DilleniaUPC", 50, FontStyle.Bold);
                SolidBrush drawBrush = new SolidBrush(ColorTranslator.FromHtml("#ffffff"));

                string measureString = Param.ShopName;
                SizeF stringSize = g.MeasureString(measureString, stringFont);
                g.DrawString(measureString, stringFont, drawBrush, (pictureBox1.Image.Width - stringSize.Width) / 2, 17);

                stringFont = new Font("DilleniaUPC", 60, FontStyle.Bold);
                drawBrush = new SolidBrush(ColorTranslator.FromHtml("#263e74"));
                measureString = "วันที่ "+dtpStartDate.Value.ToString("dd MMMM yyyy");
                stringSize = g.MeasureString(measureString, stringFont);
                g.DrawString(measureString, stringFont, drawBrush, (pictureBox1.Image.Width - stringSize.Width) / 2, 230);


                stringFont = new Font("DilleniaUPC", 80, FontStyle.Bold);
                drawBrush = new SolidBrush(ColorTranslator.FromHtml("#f40c43"));
                measureString = sumPrice.ToString("#,##0");
                stringSize = g.MeasureString(measureString, stringFont);
                g.DrawString(measureString, stringFont, drawBrush, (pictureBox1.Image.Width - stringSize.Width - 50) , 334);

                if (Param.SystemConfig.SellPrice == null)
                {
                    measureString = sumProfit.ToString("#,##0");
                    stringSize = g.MeasureString(measureString, stringFont);
                    g.DrawString(measureString, stringFont, drawBrush, (pictureBox1.Image.Width - stringSize.Width - 50), 428);
                }

                drawBrush = new SolidBrush(ColorTranslator.FromHtml("#fa3711"));
                measureString = avgPrice.ToString("#,##0");
                stringSize = g.MeasureString(measureString, stringFont);
                g.DrawString(measureString, stringFont, drawBrush, (pictureBox1.Image.Width - stringSize.Width) / 2, 618);

                var startX = 48;
                var startY = 780;
                var width = 40;
                var gab = 5;
                var maxHeight = 250;
                var whiteBrush = new SolidBrush(ColorTranslator.FromHtml("#ffa1d6"));
                for (int i = 0; i < chart.Count; i++)
                {
                    drawBrush = new SolidBrush(ColorTranslator.FromHtml(i != chart.Count - 1 ? "#af18b1" : ((chart[i] > avgPrice) ? "#207a2b" : "#d31a1a")));
                    var h = (int)(chart[i] * maxHeight / max);
                    g.FillRectangle(drawBrush, new Rectangle(startX + ((width + gab) * i), startY + maxHeight - h, width, h));
                    g.DrawRectangle(new Pen(whiteBrush), new Rectangle(startX + ((width + gab) * i), startY + maxHeight - h, width, h));
                }
                if (avgPrice > 0)
                {
                    var y = (int)(avgPrice * maxHeight / max);
                    g.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#ffffff")), 1.0f), startX, startY + maxHeight - y, 672, startY + maxHeight - y);
                }
                //g.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#ffffff")), 1.0f), startX, startY + maxHeight, 672, startY + maxHeight);

            }
        }

        private void mniSaveImage_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            saveFileDialog1.FileName = "ยอดขายรายวัน " + dtpStartDate.Value.ToString("dd MMMM yyyy")+".png";
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                pictureBox1.Image.Save(saveFileDialog1.FileName, ImageFormat.Png);
            }
        }

        private void table1_CellClick(object sender, XPTable.Events.CellMouseEventArgs e)
        {
            if (e.Row < table1.RowCount)
            {
                row = e.Row;
            }
        }

        private void miPrintReceipt_Click(object sender, EventArgs e)
        {
            if (row != -1)
            {
                sellNo = tableModel1.Rows[row].Cells[2].Text;
                if (Param.SystemConfig.Bill.PrintType == "Y")
                {
                    var cnt = int.Parse(Param.SystemConfig.Bill.PrintCount.ToString());
                    for (int i = 1; i <= cnt; i++)
                        Util.PrintReceipt(sellNo);
                }
                else if (Param.SystemConfig.Bill.PrintType == "A")
                {
                    if (MessageBox.Show("คุณต้องการพิมพ์ใบเสร็จรับเงินหรือไม่ ?", "การพิมพ์", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        Util.PrintReceipt(sellNo);
                }
            }
            else
            {
                MessageBox.Show("กรุณาเลือกรายการที่ต้องการพิมพ์ใบเสร็จรับเงิน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void miDetail_Click(object sender, EventArgs e)
        {
            if (row != -1)
            {
                sellNo = tableModel1.Rows[row].Cells[2].Text;
                FmSellDetial frm = new FmSellDetial();
                frm.Show();
            }
            else
            {
                MessageBox.Show("กรุณาเลือกรายการที่ต้องการดูรายละเอียดการขาย", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void table1_CellDoubleClick(object sender, XPTable.Events.CellMouseEventArgs e)
        {
            miDetail_Click((sender), e);
        }
    }
}
