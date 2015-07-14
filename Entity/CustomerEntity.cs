using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPOS_Online
{
    public class CustomerEntity : TableEntity
    {
        public CustomerEntity() { }

        public CustomerEntity(string shopId)
        {
            this.PartitionKey = shopId;
        }

        public CustomerEntity(string shopId, string customerId)
        {
            this.PartitionKey = shopId;
            this.RowKey = customerId;
        }

        public string Name { get; set; }
    }
}
