using Alb.Omdehsara.Common;
using Alb.Omdehsara.Common.Product;
using Alb.Omdehsara.DataAccess;
using Alb.Omdehsara.UI.MVC.Areas.Admin.Models;
using Alb.Omdehsara.UI.MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alb.Omdehsara.UI.MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UnitController : OmdehsaraControllerBase
    {
        [HttpGet]
        public ActionResult Index(long? categoryId = null,int? Id = null)
        {
            UnitViewModel unitModel = FillModel(Id);
            return View(unitModel);
        }

        private static UnitViewModel FillModel(int? Id)
        {
            UnitViewModel unitModel = new UnitViewModel();
            if (Id.HasValue && Id !=0)
            {
                var unit = TblUnitDA.GetUnit(fromCache: false).Where(c => c.ID == Id.Value).FirstOrDefault();
                unitModel.Unit = unit;
            }
            unitModel.Units = TblUnitDA.GetUnit();
            return unitModel;
        }
        [HttpPost]
        public ActionResult AddUnit(UnitViewModel model)
        {
            if (model.Unit.ID > 0)
            {
                TblUnitDA.UpdateUnit(model.Unit);
                ShowMessage("ویرایش انجام شد", Tools.UI.MVC.MessageTypes.Success);
            }
            else
            {
                TblUnitDA.AddUnit(model.Unit);
                ShowMessage("واحد اضافه شد", Tools.UI.MVC.MessageTypes.Success);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int Id)
        {
            try
            {
                TblUnitDA.DeleteUnit(Id);
                ShowMessage("واحد حذف شد", Tools.UI.MVC.MessageTypes.Success);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ShowMessage("این واحد در هنگام ثبت محصول استفاده شده است و امکان حذف ندارد", Tools.UI.MVC.MessageTypes.Error);
                return RedirectToAction("Index");
            }
        }
    }
}