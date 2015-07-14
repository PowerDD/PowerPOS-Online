using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPOS_Online
{
    class ProductUpdateEntity : TableEntity
    {
        public ProductUpdateEntity()
        {
        }

        public ProductUpdateEntity(string shopId, string productId)
        {
            this.PartitionKey = shopId;
            this.RowKey = productId;
        }
        public string Brand { get; set; }
        public string Category { get; set; }
        public double Cost { get; set; }
        public string CoverImage { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Price1 { get; set; }
        public double Price2 { get; set; }
        public double Price3 { get; set; }
        public double Price4 { get; set; }
        public double Price5 { get; set; }
        public int Warranty { get; set; }
    }
}
