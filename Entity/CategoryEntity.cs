using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPOS_Online
{
    class CategoryEntity : TableEntity
    {
        public CategoryEntity() { }

        public CategoryEntity(string shopId)
        {
            this.PartitionKey = shopId;
        }

        public CategoryEntity(string shopId, string id)
        {
            this.PartitionKey = shopId;
            this.RowKey = id;
        }

        public string Name { get; set; }
        public bool Active { get; set; }
        public int Priority { get; set; }
    }
}
