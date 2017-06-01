using Alb.Omdehsara.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alb.Omdehsara.UI.MVC.Areas.Cms.Models
{
    public class PageViewModel
    {
        public TblPage Page { get; set; }
        public HttpPostedFileBase MainImage { get; set; }

    }
}