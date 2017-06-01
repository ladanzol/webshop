using Alb.Omdehsara.Common;
using Alb.Omdehsara.Common.Polling;
using Alb.Omdehsara.Common.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alb.Omdehsara.UI.MVC.Areas.Polling.Models
{
    public class OptionViewModel
    {
        public TblOption Option { get; set; }
        public IEnumerable<TblOption> Options { get; set; }
    }
}