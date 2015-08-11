using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPOS_Online
{
    class ReturnDetailEntity : TableEntity
    {
         public ReturnDetailEntity() { }

        public ReturnDetailEntity(string shopId)
        {
            this.RowKey = shopId;
        }

        public ReturnDetailEntity(string shopKey, string returnNo)
        {
            this.PartitionKey = shopKey;
            this.RowKey = returnNo;
        }

        public string SellNo { get; set; }
        public string Barcode { get; set; }
        public string Product { get; set; }
        public double SellPrice { get; set; }
    }
}
