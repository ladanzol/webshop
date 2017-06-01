using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alb.Omdehsara.Common.Orders
{
    public class TblOrder
    {
        public long ID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<System.DateTime> RequiredDate { get; set; }
        public Nullable<System.DateTime> ShippedDate { get; set; }
        public Nullable<System.DateTime> ReceivedDate { get; set; }
        public Nullable<int> Freight { get; set; }
        public Nullable<int> CityID { get; set; }
        public string Address { get; set; }
        public string OrderPhone { get; set; }
        public Nullable<int> OrderPrice { get; set; }
        public Nullable<int> OrderFinalPrice { get; set; }
        public Nullable<int> OrderDiscount { get; set; }
        public Nullable<int> TransportPrice { get; set; }
        public string PostalCode { get; set; }
        public Nullable<byte> Status { get; set; }


        public string ClientID { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
    }
}
