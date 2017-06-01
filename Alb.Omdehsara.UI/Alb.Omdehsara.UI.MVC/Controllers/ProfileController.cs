using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alb.Omdehsara.UI.MVC.Controllers
{
    [Authorize]
    public class ProfileController : OmdehsaraControllerBase
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }
    }
}