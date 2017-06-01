using Alb.Omdehsara.Common;
using Alb.Omdehsara.Common.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alb.Omdehsara.UI.MVC.Models
{
    public class PaymentViewModel
    {
        public SelectList Banks { get;set; }
        public TblCredit Payment{get;set;}
        public int SelectedBankID { get; set; }
    }
}