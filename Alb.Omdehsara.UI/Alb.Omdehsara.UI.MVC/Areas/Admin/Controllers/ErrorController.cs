using Alb.Common.DataAccess;
using Alb.Omdehsara.Common;
using Alb.Omdehsara.UI.MVC.Controllers;
using Alb.Tools.UI.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alb.Omdehsara.UI.MVC.Areas.Admin.Controllers
{
    [AuthorizeEnum(OmdehsaraRoles.Admin)]
    public class ErrorController : OmdehsaraControllerBase
    {
        public ActionResult List(int pageIndex = 1)
        {
            int totalRecords;
            ViewBag.PageSize = 20;
            ViewBag.PageIndex = pageIndex;
            var model = ErrorDA.GetErrorList(pageIndex, (int)ViewBag.PageSize, out totalRecords);
            ViewBag.TotalRecords = totalRecords;
            return View(model);
        }
        public ActionResult Delete()
        {
            ErrorDA.DeleteAllErrors();
            ShowMessage("همه خطاها حذف شدند.", Tools.UI.MVC.MessageTypes.Success);
            return RedirectToAction("List");
        }
    }
}