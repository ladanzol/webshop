using Alb.Omdehsara.Common;
using Alb.Omdehsara.Common.Orders;
using Alb.Omdehsara.DataAccess;
using Alb.Omdehsara.DataAccess.Orders;
using Alb.Omdehsara.UI.MVC.Areas.Admin.Models;
using Alb.Omdehsara.UI.MVC.Controllers;
using Alb.Omdehsara.UI.MVC.Models;
using Alb.Tools.UI.MVC;
using Alb.Tools.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Alb.Omdehsara.UI.MVC.Areas.Admin.Controllers
{
    [AuthorizeEnum(ProjectRoles.Admin)]
    public class CreditController : OmdehsaraControllerBase
    {
        public ActionResult Index(int pageIndex = 1)
        {
            CreditSearchViewModel model = new CreditSearchViewModel();
            int totalRecords = 0;
            model.PageSize = 15;
            model.Credits = CreditDA.GetCredits(pageIndex,null, model.PageSize, out totalRecords);
            model.PageIndex = pageIndex;
            model.TotalRecords = totalRecords;
            return View(model);
        }
        public ActionResult Search(CreditSearchViewModel model, int pageIndex = 1)
        {
            int totalRecords = 0;
            model.PageSize = 15;
            model.Credits = CreditDA.GetCredits(pageIndex, model.UserName, model.PageSize, out totalRecords);
            model.PageIndex = pageIndex;
            model.TotalRecords = totalRecords;
            return View("Index",model);
        }
        [HttpGet]
        public ActionResult ChangeStatus(int Id)
        {
            CreditDA.ChangeStatus(Id);
            ShowMessage("تایید انجام شد", MessageTypes.Success);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            CreditDA.Delete(Id);
            ShowMessage("حذف انجام شد", MessageTypes.Success);
            return RedirectToAction("Index");
        }
    }
}