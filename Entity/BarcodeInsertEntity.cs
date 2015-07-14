using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPOS_Online
{
    public class BarcodeInsertEntity : TableEntity
    {
        public BarcodeInsertEntity() { }

        public BarcodeInsertEntity(string shopId)
        {
            this.PartitionKey = shopId;
        }

        public BarcodeInsertEntity(string shopId, string barcode)
        {
            this.PartitionKey = shopId;
            this.RowKey = barcode;
        }
        public string Product { get; set; }
        public bool CustomerReceived { get; set; }
    }
}
