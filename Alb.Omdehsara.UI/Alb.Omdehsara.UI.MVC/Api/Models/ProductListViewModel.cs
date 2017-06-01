using Alb.Omdehsara.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alb.Tools.Utility;
using Alb.Omdehsara.UI.MVC.Api.Models;

namespace Alb.Omdehsara.UI.MVC.Models
{
    public abstract class ProductListViewModel 
    {
        public IEnumerable<ProductImage> ImageList
        {
            get;
            set;
        }
        public abstract string ProductUrl { get; }
        public abstract string ImageUrl { get; }
    }
}