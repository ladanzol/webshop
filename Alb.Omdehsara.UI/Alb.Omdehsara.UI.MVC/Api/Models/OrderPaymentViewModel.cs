using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alb.Omdehsara.UI.MVC.Api.Models
{
    public class OrderPaymentViewModel
    {
        public long OrderID { get; set; }
        public string PaymentRef { get; set; }
        public string OrderPrice { get; set; }
        public string PaymentType { get; set; }
        public string BankID { get; set; }
    }
}