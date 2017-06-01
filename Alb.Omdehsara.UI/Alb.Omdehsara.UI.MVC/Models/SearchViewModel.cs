using Alb.Omdehsara.Common.Product;
using Alb.Omdehsara.UI.MVC.Api.Models;
using Alb.Tools.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alb.Omdehsara.UI.MVC.Models
{
    public class SearchViewModel:Paged
    {
        public IEnumerable<ProductTypeViewModel> ProductList { get; set; }

        public string Title { get; set; }
    }
}