using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPOS_Online
{
    class ReturnEntity : TableEntity
    {
        public ReturnEntity() { }

        public ReturnEntity(string shopId)
        {
            this.RowKey = shopId;
        }

        public ReturnEntity(string shopKey, string sellNo)
        {
            this.PartitionKey = shopKey;
            this.RowKey = sellNo;
        }
        public string SellNo {get;set;}
        public DateTime ReturnDate { get; set; }
        public string Barcode { get; set; }
        public string Product { get; set; }
        public double SellPrice { get; set; }
        public string ReturnBy { get; set; }
    }

}
