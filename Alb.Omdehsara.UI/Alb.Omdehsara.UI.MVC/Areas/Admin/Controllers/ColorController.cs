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
    public class ColorController : OmdehsaraControllerBase
    {
        [HttpGet]
        public ActionResult Index(long? categoryId = null,int? Id = null)
        {
            ColorViewModel colorModel = FillModel(Id);
            return View(colorModel);
        }

        private static ColorViewModel FillModel(int? Id)
        {
            ColorViewModel colorModel = new ColorViewModel();
            if (Id.HasValue && Id !=0)
            {
                var color = TblColorDA.GetColor(fromCache: false).Where(c => c.ID == Id.Value).FirstOrDefault();
                colorModel.Color = color;
                colorModel.SelectedCategories = color.Categoryies;
            }
            else
            {
                colorModel.SelectedCategories = new List<TblCategory>();
            }
            colorModel.Colors = TblColorDA.GetColor();
            colorModel.Cateogries = TblCategoryDA.GetCategoryList().Where(c => c.ID < 100);
            return colorModel;
        }
        [HttpPost]
        public ActionResult AddColor(ColorViewModel model)
        {
            if (model.PostedCategories == null || model.PostedCategories.Count() == 0)
            {
                ShowMessage("لطفا دست کم یک گروه را انتخاب کنید", Tools.UI.MVC.MessageTypes.Error);
            }
            else
            {
                if (model.Color.ID > 0)
                {
                    TblColorDA.UpdateColor(model.Color, model.PostedCategories);
                    ShowMessage("ویرایش انجام شد", Tools.UI.MVC.MessageTypes.Success);
                }
                else
                {
                    TblColorDA.AddColor(model.Color, model.PostedCategories);
                    ShowMessage("رنگ اضافه شد", Tools.UI.MVC.MessageTypes.Success);
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int Id)
        {
            try
            {
                TblColorDA.DeleteColor(Id);
                ShowMessage("رنگ حذف شد", Tools.UI.MVC.MessageTypes.Success);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ShowMessage("این رنگ در هنگام ثبت محصول استفاده شده است و امکان حذف ندارد", Tools.UI.MVC.MessageTypes.Error);
                return RedirectToAction("Index");
            }
        }
    }
}