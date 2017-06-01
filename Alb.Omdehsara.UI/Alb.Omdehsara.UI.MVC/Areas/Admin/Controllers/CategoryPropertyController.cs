using Alb.Omdehsara.Common;
using Alb.Omdehsara.DataAccess;
using Alb.Omdehsara.UI.MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alb.Omdehsara.UI.MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryPropertyController : OmdehsaraControllerBase
    {
        public ActionResult ManageCategoryProperty()
        {
            return View();
        }

        // GET: Admin/Category
        public JsonResult Index(int pageIndex = 1, int pageSize = 20,int? CategoryID= null, int? ID = null)
        {
            int TotalCount;
            IEnumerable<TblCategoryProperty> consts = CategoryPropertyDA.GetCategoryProperty(pageIndex, pageSize, CategoryID, ID, out TotalCount);
            return Json(new { Data = consts, TotalCount = TotalCount }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(TblCategoryProperty models)
        {
            models.ID =  CategoryPropertyDA.InsertCategoryProperty(models);
            return Json(new { Data = models });
        }

        // POST: Admin/Category/Edit/5
        [HttpPut]
        public JsonResult Edit(TblCategoryProperty models)
        {
            CategoryPropertyDA.UpdateCategoryProperty(models);
            return Json(new { Data = models });
        }

        // GET: Admin/Category/Delete/5
        [HttpDelete]
        public JsonResult Delete(TblCategoryProperty models)
        {
            CategoryPropertyDA.DeleteCategoryProperty(models);
            return Json(new { });
        }
    }
}
