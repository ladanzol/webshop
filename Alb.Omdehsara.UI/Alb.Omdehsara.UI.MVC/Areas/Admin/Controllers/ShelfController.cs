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
    public class ShelfController : OmdehsaraControllerBase
    {
        public ActionResult ManageShelf()
        {
            return View();
        }
        // GET: Admin/Shelf
        public JsonResult Index(int pageIndex = 1, int pageSize = 20,int? store=null)
        {
            int TotalCount;
            IEnumerable<TblShelf> consts = TblShelfDA.GetShelfs(store, pageIndex, pageSize, out TotalCount);
            return Json(new { Data = consts, TotalCount = TotalCount }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Create(TblShelf models)
        {
            TblShelfDA.InsertShelf(models);
            return Json(new { Data = models });
        }
        [HttpPut]
        public JsonResult Edit(TblShelf models)
        {
            TblShelfDA.UpdateShelf(models);
            return Json(new { Data = models });
        }

        // GET: Admin/Shelf/Delete/5
        [HttpDelete]
        public JsonResult Delete(TblShelf models)
        {
            TblShelfDA.DeleteShelf(models);
            return Json(new { Data = models });
        }

    }
}
