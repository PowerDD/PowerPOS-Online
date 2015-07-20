using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPOS_Online
{
    public class SellHeaderEntity : TableEntity
    {
        public SellHeaderEntity() { }

        public SellHeaderEntity(string shopId)
        {
            this.RowKey = shopId;
        }

        public SellHeaderEntity(string shopKey, string sellNo)
        {
            this.PartitionKey = shopKey;
            this.RowKey = sellNo;
        }

        public string Customer { get; set; }
        public string CustomerSex { get; set; }
        public int CustomerAge { get; set; }
        public int Credit { get; set; }
        public string PayType { get; set; }
        public double Cash { get; set; }
        public double DiscountPercent { get; set; }
        public double DiscountCash { get; set; }
        public bool Paid { get; set; }
        public double Profit { get; set; }
        public double TotalPrice { get; set; }
        public int PointReceived { get; set; }
        public int PointUse { get; set; }
        public string Comment { get; set; }
        public DateTime SellDate { get; set; }
        public string SellBy { get; set; }
    }
}
