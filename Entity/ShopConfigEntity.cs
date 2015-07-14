using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPOS_Online
{
    public class ShopConfigEntity : TableEntity
    {
        public ShopConfigEntity()
        {
            this.RowKey = "Config";
        }

        public ShopConfigEntity(string shopId)
        {
            this.PartitionKey = shopId;
            this.RowKey = "Config";
        }
        public string Value { get; set; }
    }
}
