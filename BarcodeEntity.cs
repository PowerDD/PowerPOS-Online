using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPOS_Online
{
    class BarcodeEntity : TableEntity
    {
        public BarcodeEntity() { }

        public BarcodeEntity(string shopId)
        {
            this.PartitionKey = shopId;
        }

        public BarcodeEntity(string shopId, string barcode)
        {
            this.PartitionKey = shopId;
            this.RowKey = barcode;
        }

        public string SellNo { get; set; }
        public string Product { get; set; }
        public string ReceivedDate { get; set; }
        public string SellDate { get; set; }
        public string DocNo { get; set; }
        public string OrderNo { get; set; }
        public string Customer { get; set; }

    }
}
