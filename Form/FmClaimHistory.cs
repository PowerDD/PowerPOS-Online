using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XPTable.Models;

namespace PowerPOS_Online
{
    public partial class FmClaimHistory : Form
    {
        private List<ClaimEntity> claimEntityList = new List<ClaimEntity>();
        string barcode;
        Hashtable hashtable = new Hashtable();


        public FmClaimHistory()
        {
            InitializeComponent();
        }

        private void FmClaimHistory_Load(object sender, EventArgs e)
        {
            Util.InitialTable(table1);
            barcode = UcClaim.barcode;
            bwSearch.RunWorkerAsync();
        }

        private void bwSearch_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (barcode != "")
                {
                    var azureTable = Param.AzureTableClient.GetTableReference("Claim");
                    TableQuery<ClaimEntity> query = new TableQuery<ClaimEntity>().Where(TableQuery.CombineFilters(
                        TableQuery.GenerateFilterCondition("Barcode", QueryComparisons.Equal, barcode), TableOperators.Or, TableQuery.GenerateFilterCondition("BarcodeClaim", QueryComparisons.Equal, barcode)
                    ));

                    List<ClaimEntity> Barcode = new List<ClaimEntity>();

                    foreach (ClaimEntity entity in azureTable.ExecuteQuery(query))
                    {
                        Barcode.Add(entity);
                        for (int i = 0; i < Barcode.Count; i++)
                        {
                            if (!hashtable.ContainsKey(Barcode[i].RowKey))
                            {
                                hashtable.Add(Barcode[i].RowKey, 0);
                            }

                            
                               
                            if (hashtable.ContainsValue(0))
                            {
                                hashtable[Barcode[i].RowKey] = 1;
                                barcode = Barcode[i].Barcode.ToString();
                                claimEntityList = claimEntityList.OrderByDescending(d => d.ClaimDate).ToList();
                                claimEntityList.Add(entity);
                            }
                        }
                    }
                    Barcode = Barcode.OrderByDescending(o => o.ClaimDate).ToList();

                    if (hashtable.Count >= 2)
                    {
                        barcode = Barcode[1].Barcode.ToString();
                    }
                    else
                    {
                        barcode = "";
                    }


                }
                else
                {
                    break;
                }
            }
            //claimEntityList = claimEntityList.OrderByDescending(d => d.ClaimDate).ToList();

        }

        private void bwSearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            table1.BeginUpdate();
            tableModel1.Rows.Clear();
            tableModel1.RowHeight = 22;
            int Num = 0;
            for (int i = 0; i < claimEntityList.Count; i++)
            {

                if (claimEntityList[i].ClaimType.ToString() != "3")
                {
                    tableModel1.Rows.Add(new Row(
                    new Cell[] {
                    new Cell("" + (Num+1)),
                    new Cell(claimEntityList[i].RowKey),
                    new Cell(claimEntityList[i].ClaimDate.ToLocalTime().ToString("dd MMMM yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("th-TH"))),
                    new Cell(claimEntityList[i].Barcode),
                    new Cell(claimEntityList[i].BarcodeClaim),
                    new Cell(claimEntityList[i].PriceClaim),
                    new Cell("")
                    }));
                }
                else if (claimEntityList[i].ClaimType.ToString() == "3")
                {
                    tableModel1.Rows.Add(new Row(
                    new Cell[] {
                    new Cell("" + (Num+1)),
                    new Cell(claimEntityList[i].RowKey),
                    new Cell(claimEntityList[i].ClaimDate.ToLocalTime().ToString("dd MMMM yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("th-TH"))),
                    new Cell(claimEntityList[i].Barcode),
                    new Cell(claimEntityList[i].BarcodeClaim),
                    new Cell(claimEntityList[i].PriceClaim),
                    new Cell("",global::PowerPOS_Online.Properties.Resources.accept)
                    }));
                }
                Num++;
            }
            table1.EndUpdate();



            //for (int i = 0; i < claimEntityList2.Count; i++)
            //{
            //    if (claimEntityList2[i].ClaimType.ToString() != "3")
            //    {
            //        tableModel1.Rows.Add(new Row(
            //        new Cell[] {
            //        new Cell("" + (Num+1)),
            //        new Cell(claimEntityList2[i].RowKey),
            //        new Cell(claimEntityList2[i].ClaimDate.ToLocalTime().ToString("dd MMMM yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("th-TH"))),
            //        new Cell(claimEntityList2[i].Barcode),
            //        new Cell(claimEntityList2[i].BarcodeClaim),
            //        new Cell(claimEntityList2[i].PriceClaim),
            //        new Cell("")
            //        }));
            //    }
            //    else if(claimEntityList2[i].ClaimType.ToString() == "3")
            //    {
            //        tableModel1.Rows.Add(new Row(
            //        new Cell[] {
            //        new Cell("" + (Num+1)),
            //        new Cell(claimEntityList2[i].RowKey),
            //        new Cell(claimEntityList2[i].ClaimDate.ToLocalTime().ToString("dd MMMM yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("th-TH"))),
            //        new Cell(claimEntityList2[i].Barcode),
            //        new Cell(claimEntityList2[i].BarcodeClaim),
            //        new Cell(claimEntityList2[i].PriceClaim),
            //        new Cell("",global::PowerPOS_Online.Properties.Resources.accept)
            //        }));
            //    }
            //    Num++;
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
