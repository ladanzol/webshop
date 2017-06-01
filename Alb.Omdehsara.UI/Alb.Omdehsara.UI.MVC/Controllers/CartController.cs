using Alb.Common.Common;
using Alb.Common.DataAccess;
using Alb.Tools.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alb.Tools.UI.MVC.Security;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Security;
using Alb.Omdehsara.Common;
namespace Alb.Omdehsara.UI.MVC.Controllers
{
    public class CartController : OmdehsaraControllerBase
    {
        // GET: /Home/
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult Address()
        {
            return View();
        }
    }
}
