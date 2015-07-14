using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPOS_Online
{
    public class ShopEntity : TableEntity
    {
        public ShopEntity() { }

        public ShopEntity(string shopId)
        {
            this.RowKey = shopId;
        }

        public ShopEntity(string shopKey, string shopId)
        {
            this.PartitionKey = shopKey;
            this.RowKey = shopId;
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public string ShopCustomer { get; set; }
        public string ShopParent { get; set; }
        public int ShopCost { get; set; }

    }
}
