using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPOS_Online
{
    class ClaimEntity : TableEntity
    {
        public ClaimEntity() { }

        public ClaimEntity(string shopId)
        {
            this.RowKey = shopId;
        }

        public ClaimEntity(string shopKey, string ClaimNo)
        {
            this.PartitionKey = shopKey;
            this.RowKey = ClaimNo;
        }
        public string ClaimNo { get; set; }
        public string ClaimType { get; set; }
        public string Barcode { get; set; }
        public string BarcodeClaim { get; set; }
        public string Product { get; set; }
        public string Description { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Nickname { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string ClaimBy { get; set; }
        public DateTime ClaimDate { get; set; }
        public double Price { get; set; }
    }
}
