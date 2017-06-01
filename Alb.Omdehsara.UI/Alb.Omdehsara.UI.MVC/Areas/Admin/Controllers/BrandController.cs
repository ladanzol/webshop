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
    public class BrandController : OmdehsaraControllerBase
    {
        [HttpGet]
        public ActionResult Index(long? categoryId = null,int? Id = null)
        {
            BrandViewModel brandModel = FillModel(Id);
            return View(brandModel);
        }

        private static BrandViewModel FillModel(int? Id)
        {
            BrandViewModel brandModel = new BrandViewModel();
            if (Id.HasValue && Id !=0)
            {
                var brand = TblBrandDA.GetBrand(fromCache: false).Where(c => c.ID == Id.Value).FirstOrDefault();
                brandModel.Brand = brand;
                brandModel.SelectedCategories = brand.Categoryies;
            }
            else
            {
                brandModel.SelectedCategories = new List<TblCategory>();
            }
            brandModel.Brands = TblBrandDA.GetBrand();
            brandModel.Cateogries = TblCategoryDA.GetCategoryList().Where(c => c.ID < 100);
            return brandModel;
        }
        [HttpPost]
        public ActionResult AddBrand(BrandViewModel model)
        {
            if (model.PostedCategories == null || model.PostedCategories.Count() == 0)
            {
                ShowMessage("لطفا دست کم یک گروه را انتخاب کنید", Tools.UI.MVC.MessageTypes.Error);
            }
            else
            {
                if (model.Brand.ID > 0)
                {
                    TblBrandDA.UpdateBrand(model.Brand, model.PostedCategories);
                    ShowMessage("ویرایش انجام شد", Tools.UI.MVC.MessageTypes.Success);
                }
                else
                {
                    TblBrandDA.AddBrand(model.Brand, model.PostedCategories);
                    ShowMessage("رنگ اضافه شد", Tools.UI.MVC.MessageTypes.Success);
                }
                TblBrandDA.Reload();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int Id)
        {
            try
            {
                TblBrandDA.DeleteBrand(Id);
                ShowMessage("رنگ حذف شد", Tools.UI.MVC.MessageTypes.Success);
                return RedirectToAction("Index");
            }
            catch
            {
                ShowMessage("این برند هنگام ثبت محصولات استفاده شده و امکان حذف وجود ندارد", Tools.UI.MVC.MessageTypes.Error);
                return RedirectToAction("Index");
            }
        }
    }
}