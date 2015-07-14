using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPOS_Online
{
    public class BarcodeNoSellDateEntity : TableEntity
    {
        public BarcodeNoSellDateEntity() { }

        public BarcodeNoSellDateEntity(string shopId)
        {
            this.PartitionKey = shopId;
        }

        public BarcodeNoSellDateEntity(string shopId, string barcode)
        {
            this.PartitionKey = shopId;
            this.RowKey = barcode;
        }

        public string SellNo { get; set; }
        public string Product { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string ReceivedBy { get; set; }
        public string SellBy { get; set; }
        public bool SellFinished { get; set; }
        public string OrderNo { get; set; }
        public string Customer { get; set; }
        public double Cost { get; set; }
        public double OperationCost { get; set; }
        public double SellPrice { get; set; }

    }
}
