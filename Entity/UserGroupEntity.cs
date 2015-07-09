using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPOS_Online
{
    class UserGroupEntity : TableEntity
    {
        public UserGroupEntity() { }

        public UserGroupEntity(string shopId)
        {
            this.PartitionKey = shopId;
            this.RowKey = "Group";
        }

        public string Data { get; set; }
    }
}
