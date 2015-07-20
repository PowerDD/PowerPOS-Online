using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPOS_Online
{
    public class ProvinceEntity : TableEntity
    {
        public ProvinceEntity() { }

        public ProvinceEntity(string country)
        {
            this.PartitionKey = country;
        }

        public ProvinceEntity(string country, string id)
        {
            this.PartitionKey = country;
            this.RowKey = id;
        }

        public string Th { get; set; }
        public string Zipcode { get; set; }
    }
}
