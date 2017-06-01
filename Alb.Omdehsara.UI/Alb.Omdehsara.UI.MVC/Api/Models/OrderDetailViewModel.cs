using Alb.Omdehsara.Common.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alb.Omdehsara.UI.MVC.Api.Models
{
    public class OrderDetailViewModel : OrderDetail
    {
        public string ClientID { get; set; }
    }
}