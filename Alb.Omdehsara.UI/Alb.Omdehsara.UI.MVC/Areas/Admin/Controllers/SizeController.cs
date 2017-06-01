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
    public class SizeController : OmdehsaraControllerBase
    {
        [HttpGet]
        public ActionResult Index(long? categoryId = null,int? Id = null)
        {
            SizeViewModel sizeModel = FillModel(Id);
            return View(sizeModel);
        }

        private static SizeViewModel FillModel(int? Id)
        {
            SizeViewModel sizeModel = new SizeViewModel();
            if (Id.HasValue && Id !=0)
            {
                var size = TblSizeDA.GetSize(fromCache: false).Where(c => c.ID == Id.Value).FirstOrDefault();
                sizeModel.Size = size;
                sizeModel.SelectedCategories = size.Categoryies;
            }
            else
            {
                sizeModel.SelectedCategories = new List<TblCategory>();
            }
            sizeModel.Sizes = TblSizeDA.GetSize();
            sizeModel.Cateogries = TblCategoryDA.GetCategoryList().Where(c => c.ID < 100);
            return sizeModel;
        }
        [HttpPost]
        public ActionResult AddSize(SizeViewModel model)
        {
            if (model.PostedCategories == null || model.PostedCategories.Count() == 0)
            {
                ShowMessage("لطفا دست کم یک گروه را انتخاب کنید", Tools.UI.MVC.MessageTypes.Error);
            }
            else
            {
                if (model.Size.ID > 0)
                {
                    TblSizeDA.UpdateSize(model.Size, model.PostedCategories);
                    ShowMessage("ویرایش انجام شد", Tools.UI.MVC.MessageTypes.Success);
                }
                else
                {
                    TblSizeDA.AddSize(model.Size, model.PostedCategories);
                    ShowMessage("رنگ اضافه شد", Tools.UI.MVC.MessageTypes.Success);
                    TblSizeDA.ReLoad();
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int Id)
        {
            try
            {
                TblSizeDA.DeleteSize(Id);
                ShowMessage("رنگ حذف شد", Tools.UI.MVC.MessageTypes.Success);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ShowMessage("این اندازه هنگام ثبت محصول استفاده شده است و امکان حذف ندارد", Tools.UI.MVC.MessageTypes.Error);
                return RedirectToAction("Index");
            }
        }
        
    }
}