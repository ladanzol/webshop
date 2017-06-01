using Alb.Omdehsara.Common.Orders;
using Alb.Tools.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alb.Omdehsara.UI.MVC.Models
{
    public class OrderListViewModel:Paged
    {
        public IEnumerable<OrderViewModel> Orders { get; set; }
        public OrderStatus? Status { get; set; }
    }
}