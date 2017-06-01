using Alb.Omdehsara.Common;
using Alb.Omdehsara.Common.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alb.Omdehsara.UI.MVC.Api.Models
{
    public class ProductTypeViewModel
    {
        public ProductTypeView Product { get; set; }
        public IEnumerable<TblProductProperty> ProductProperties { get; set; }
        public IEnumerable<string> ProductImages { get; set; }

        public IEnumerable<SizeProduct> AvailableSizes { get; set; }

        public IEnumerable<TblColor> AvailableColors { get; set; }
    }
}