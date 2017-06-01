using Alb.Omdehsara.Common;
using Alb.Omdehsara.DataAccess;
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
    public class CategoryController : OmdehsaraControllerBase
    {
        public ActionResult ManageCategory()
        {
            return View();
        }
        // GET: Admin/Category
        public JsonResult Index(int pageIndex = 1, int pageSize = 20, int? ParentCategoryId = null)
        {
            int TotalCount;
            IEnumerable<TblCategory> consts = TblCategoryDA.GetCategories(pageIndex, pageSize, ParentCategoryId, out TotalCount);
            return Json(new { Data = consts, TotalCount = TotalCount }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Create(TblCategory models)
        {
            try
            {
                TblCategoryDA.InsertCategory(models);
            }
            catch (Exception ex)
            {
                
            }
            return Json(new { Data = models });
        }
        // POST: Admin/Category/Edit/5
        [HttpPut]
        public JsonResult Edit(TblCategory models)
        {
            TblCategoryDA.UpdateCategory(models);
            return Json(new { Data = models });
        }

        // GET: Admin/Category/Delete/5
        [HttpDelete]
        public JsonResult Delete(TblCategory models)
        {
            TblCategoryDA.DeleteCategory(models);
            return Json(new { Data = models });
        }
    }
}
