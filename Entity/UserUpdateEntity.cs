using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPOS_Online
{
    public class UserUpdateEntity : TableEntity
    {

        public UserUpdateEntity(string shopId, string username)
        {
            this.PartitionKey = shopId;
            this.RowKey = username;
        }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Nickname { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string UserGroup { get; set; }
        public bool Active { get; set; }
    }
}
