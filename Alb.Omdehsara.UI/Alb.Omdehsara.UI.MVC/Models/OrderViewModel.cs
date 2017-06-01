using Alb.Omdehsara.Common.Orders;
using Alb.Tools.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alb.Omdehsara.UI.MVC.Models
{
    public class OrderViewModel:OrderView
    {
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
     
    }
}