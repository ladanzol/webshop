using Alb.Omdehsara.Common;
using Alb.Omdehsara.DataAccess;
using Alb.Omdehsara.UI.MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alb.Tools.Utility;
namespace Alb.Omdehsara.UI.MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ConstantController : OmdehsaraControllerBase
    {
        public ActionResult ManageConstant()
        {
            return View();
        }
        // GET: Admin/Constant
        public JsonResult Index(int pageIndex = 1, int pageSize = 20, string subject = null)
        {
            int TotalCount;
            IEnumerable<TblConstant> consts = ConstantDA.GetConstant(pageIndex, pageSize, subject, out TotalCount)
                .Select(c => new TblConstant()
                {
                    ID = c.ID,
                    ParentSubject = c.ParentSubject,
                    SortOrder = c.SortOrder,
                    Subject = c.Subject
                    ,
                    Text = c.Text.SubstringSafe(400)
                });
            return Json(new { Data = consts, TotalCount = TotalCount }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create(TblConstant models)
        {
            ConstantDA.InsertConstant(models);
            return Json(new { Data = models });
        }
        // POST: Admin/Constant/Edit/5
        [HttpPut]
        [ValidateInput(false)]
        public JsonResult Edit(TblConstant models)
        {
            ConstantDA.UpdateConstant(models);
            return Json(new { Data = models });
        }

        // GET: Admin/Constant/Delete/5
        [HttpDelete]
        public JsonResult Delete(TblConstant models)
        {
            ConstantDA.DeleteConstant(models);
            return Json(new { Data = models });
        }
    }
}
