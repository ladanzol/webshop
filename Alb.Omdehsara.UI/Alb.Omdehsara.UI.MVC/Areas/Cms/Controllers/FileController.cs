using Alb.Omdehsara.UI.MVC.Areas.Cms.Models;
using Alb.Tools.UI.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Alb.Omdehsara.Common;
using Alb.Omdehsara.DataAccess;
using Alb.Omdehsara.DataAccess.Cms;
using Alb.Tools.Utility;
using Alb.Omdehsara.UI.MVC.Controllers;
namespace Alb.Omdehsara.UI.MVC.Areas.Cms.Controllers
{
    public class FileController : OmdehsaraControllerBase
    {
        [AuthorizeEnum(ProjectRoles.Admin)]
        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeEnum( ProjectRoles.Admin)]
        public ActionResult UploadFile(UploadFileViewModel model)
        {
            return View();
        }
        [AuthorizeEnum(ProjectRoles.Admin)]
        public ActionResult UploadImage()
        {
            return View(new UploadImageViewModel());
        }
        [HttpPost]
        [AuthorizeEnum(ProjectRoles.Admin)]
        public ActionResult UploadImage(UploadImageViewModel model)
        {
            TblImage image = new TblImage();
            if (!model.UploadInImageFolder)
            {
                image.ImageAddress = Helper.GetCmsImageUrl(model.Image);
            }
            else
            {
                image.ImageAddress = "~/images/" + model.Image.FileName;
            }
            image.InsDate = DateTime.Now;
            if (model.HasLogo)
            {
                ImageTools.AddTransparentLogo(model.Image.InputStream, Helper.TransLogoPath, Server.MapPath(image.ImageAddress));
            }
            else
            {
                model.Image.SaveAs(Server.MapPath(image.ImageAddress));
            }
            image.ImageTitle = model.Title;
            image.ImageDescription = model.Description;
            image.ID = FileDA.SaveImage(image);
            ShowMessage("عکس با موفقیت ثبت شد", MessageTypes.Success);
            model.Image = null;
            return View(model);
        }
        [AuthorizeEnum(ProjectRoles.Admin)]
        public ActionResult ImageList(int pageIndex = 1, string search = null)
        {
            int totalRecords = 0;
            ImageListViewModel model = new ImageListViewModel();
            model.PageIndex = pageIndex;
            model.PageSize = 15;
            model.ImageList = FileDA.GetImages(pageIndex, model.PageSize, search, out totalRecords);
            model.TotalRecords = totalRecords;
            return View(model);
        }

        [AuthorizeEnum(ProjectRoles.Admin)]
        public ActionResult DeleteImage(int Id)
        {
            try
            {
                TblImage img = FileDA.GetImage(Id);
                System.IO.File.Delete(Server.MapPath(img.ImageAddress));
                FileDA.DeleteImage(Id);
                ShowMessage("حذف انجام شد", MessageTypes.Success);
                return RedirectToAction("ImageList");
            }
            catch (Exception ex)
            {
                ShowMessage("خطا در انجام عملیات", MessageTypes.Error);
                return RedirectToAction("ImageList");
            }
        }
    }
}
