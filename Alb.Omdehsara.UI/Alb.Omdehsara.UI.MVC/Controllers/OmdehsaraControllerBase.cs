
using Alb.Common.UI.MVC;
using Alb.Omdehsara.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alb.Omdehsara.UI.MVC.Controllers
{
    public class OmdehsaraControllerBase : AlbCommonController
    {
        public ActionResult Header()
        {
            return PartialView("_Header", TblCategoryDA.GetCategoryList());
        }
        public string TopMenu()
        {
            return ConstantDA.ConstantList.Where(c=>c.Subject =="TopMenu").First().Text;
        }
        public string Footer()
        {
            return ConstantDA.ConstantList.Where(c => c.Subject == "Footer").First().Text;
        }
    }
}
