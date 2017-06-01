using Alb.Omdehsara.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alb.Omdehsara.UI.MVC.Areas.Cms.Models
{
    public class PageLinksViewModel
    {
        public IEnumerable<TblPage> Pages { get; set; }
        public int? CategoryId { get; set; }
    }
}