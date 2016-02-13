using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPOS_Online
{
    class ChangePriceEntity : TableEntity
    {
        public ChangePriceEntity() { }

        public ChangePriceEntity(string shopId)
        {
            this.PartitionKey = shopId;
        }

        public ChangePriceEntity(string shopId, string sellNo)
        {
            this.PartitionKey = shopId;
            this.RowKey = sellNo;
        }

        public string Product { get; set; }
        public double ChangePrice { get; set; }
        public string ChangeBy { get; set; }
        public double Price { get; set; }
    }
}
