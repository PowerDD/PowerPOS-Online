using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPOS_Online
{
    class SellDetailEntity : TableEntity
    {
         public SellDetailEntity() { }

        public SellDetailEntity(string shopId)
        {
            this.RowKey = shopId;
        }

        public SellDetailEntity(string shopKey, string sellNo)
        {
            this.PartitionKey = shopKey;
            this.RowKey = sellNo;
        }

        public string Product { get; set; }
        public double SellPrice { get; set; }
        public double Cost { get; set; }
        public int Quantity { get; set; }
    }
}
