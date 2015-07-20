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
        public string Member { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Nickname { get; set; }
        public string CitizenID { get; set; }
        public DateTime Birthday { get; set; }
        public string Sex { get; set; }
        public string CardNo { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string ShopName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string SubDistrict { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string ZipCode { get; set; }
        public bool ShopSameAddress { get; set; }
        public string ShopAddress { get; set; }
        public string ShopAddress2 { get; set; }
        public string ShopSubDistrict { get; set; }
        public string ShopDistrict { get; set; }
        public string ShopProvince { get; set; }
        public string ShopZipCode { get; set; }
        public int SellPrice { get; set; }
        public int DiscountPercent { get; set; }
        public int Credit { get; set; }
        public string Comment { get; set; }
        public DateTime AddDate { get; set; }
        public string AddBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateBy { get; set; }
    }
}
