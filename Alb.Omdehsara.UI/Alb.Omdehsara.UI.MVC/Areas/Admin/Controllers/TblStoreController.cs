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
    public class StoreController : OmdehsaraControllerBase
    {
        public ActionResult ManageStore()
        {
            return View();
        }
        // GET: Admin/Store
        public JsonResult Index(int pageIndex = 1, int pageSize = 20)
        {
            int TotalCount;
            IEnumerable<TblStore> consts = TblStoreDA.GetStores(pageIndex, pageSize, out TotalCount);
            return Json(new { Data = consts, TotalCount = TotalCount }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Create(TblStore models)
        {
            try
            {
                TblStoreDA.InsertStore(models);
            }
            catch (Exception ex)
            {
                
            }
            return Json(new { Data = models });
        }
        // POST: Admin/Store/Edit/5
        [HttpPut]
        public JsonResult Edit(TblStore models)
        {
            TblStoreDA.UpdateStore(models);
            return Json(new { Data = models });
        }

        // GET: Admin/Store/Delete/5
        [HttpDelete]
        public JsonResult Delete(TblStore models)
        {
            TblStoreDA.DeleteStore(models);
            return Json(new { Data = models });
        }

    }
}
