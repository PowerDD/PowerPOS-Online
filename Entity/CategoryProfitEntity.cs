using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPOS_Online
{
    class CategoryProfitEntity : TableEntity
    {
        public CategoryProfitEntity() { }

        public CategoryProfitEntity(string shopId)
        {
            this.PartitionKey = shopId;
        }

        public CategoryProfitEntity(string shopId, string id)
        {
            this.PartitionKey = shopId;
            this.RowKey = id;
        }

        public double Price { get; set; }
        public double Price1 { get; set; }
        public double Price2 { get; set; }
        public double Price3 { get; set; }
        public double Price4 { get; set; }
        public double Price5 { get; set; }
    }
}
