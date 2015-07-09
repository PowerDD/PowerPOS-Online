using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPOS_Online
{
    class ProductEntity : TableEntity
    {
        public ProductEntity()
        {
        }

        public ProductEntity(string shopId, string productId)
        {
            this.PartitionKey = shopId;
            this.RowKey = productId;
        }

        public string Name { get; set; }
        public string CoverImage { get; set; }
        public int Warranty { get; set; }
    }
}
