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
    public class TransportTypeController : OmdehsaraControllerBase
    {
        public ActionResult TransportType()
        {
            return View();
        }

        // GET: Admin/Category
        public JsonResult Index()
        {
            IEnumerable<TblTransportType> consts = TblTransportTypeDA.GetTransportTypes();
            return Json(new { Data = consts }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(TblTransportType models)
        {
            models.ID = TblTransportTypeDA.InsertTblTransportType(models);
            return Json(new { Data = models });
        }

        // POST: Admin/Category/Edit/5
        [HttpPut]
        public JsonResult Edit(TblTransportType models)
        {
            TblTransportTypeDA.UpdateTransportType(models);
            return Json(new { Data = models });
        }

        // GET: Admin/Category/Delete/5
        [HttpDelete]
        public JsonResult Delete(TblTransportType models)
        {
            TblTransportTypeDA.DeleteTransportType(models);
            return Json(new { });
        }
    }
}
