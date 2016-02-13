using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPOS_Online
{
    class PurchaseOrderEntity : TableEntity
    {
        public PurchaseOrderEntity() { }

        public PurchaseOrderEntity(string shopId)
        {
            this.PartitionKey = shopId;
        }

        public PurchaseOrderEntity(string shopId, string orderNo)
        {
            this.PartitionKey = shopId;
            this.RowKey = orderNo;
        }

        public string OrderNo { get; set; }
        public string Product { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string ReceivedBy { get; set; }
        public DateTime OrderDate { get; set; }
        public double ReceivedQty { get; set; }
        public double PriceCost { get; set; }
        public double PriceTotal { get; set; }
        public double Quantity { get; set; }


    }
}
