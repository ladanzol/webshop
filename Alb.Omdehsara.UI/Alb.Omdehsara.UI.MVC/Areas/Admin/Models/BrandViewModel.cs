using Alb.Omdehsara.Common;
using Alb.Omdehsara.Common.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alb.Omdehsara.UI.MVC.Areas.Admin.Models
{
    public class BrandViewModel
    {
        public TblBrand Brand { get; set; }
        public IEnumerable<TblBrand> Brands { get; set; }
        public IEnumerable<TblCategory> SelectedCategories { get; set; }
        public IEnumerable<long> PostedCategories { get; set; }
        public IEnumerable<TblCategory> Cateogries { get; set; }
    }
}