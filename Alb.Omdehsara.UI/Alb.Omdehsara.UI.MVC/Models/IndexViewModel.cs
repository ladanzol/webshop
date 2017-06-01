using Alb.Omdehsara.Common;
using Alb.Omdehsara.UI.MVC.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alb.Omdehsara.UI.MVC.Models
{
    public class IndexViewModel
    {
        public ProductSearchProperty ProductSearchProperty { get; set; }
        public IEnumerable<ProductTypeViewModel> ProductList { get; set; }
        //public bool? Tabular { get; set; }
        public string SearchTitle { get; set; }
        //public string TabularUrl { get; set; }
        //public string BasicUrl { get; set; }
    }
}