using Alb.Omdehsara.Common;
using Alb.Omdehsara.Common.Product;
using Alb.Omdehsara.DataAccess.Product;
using Alb.Omdehsara.UI.MVC.Controllers;
using Alb.Tools.UI.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alb.Omdehsara.UI.MVC.Areas.Product.Controllers
{
    [AuthorizeEnum(ProjectRoles.Admin)]
    public class ManageController : OmdehsaraControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
    }
}