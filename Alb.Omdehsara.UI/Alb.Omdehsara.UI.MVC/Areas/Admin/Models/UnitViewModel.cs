using Alb.Omdehsara.Common;
using Alb.Omdehsara.Common.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alb.Omdehsara.UI.MVC.Areas.Admin.Models
{
    public class UnitViewModel
    {
        public TblUnit Unit { get; set; }
        public IEnumerable<TblUnit> Units { get; set; }
    }
}