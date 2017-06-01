using Alb.Omdehsara.UI.MVC.Controllers;
using Alb.Omdehsara.UI.MVC.Controllers;
using Alb.Tools.UI.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alb.Omdehsara.UI.MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : OmdehsaraControllerBase
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }



    }
}
