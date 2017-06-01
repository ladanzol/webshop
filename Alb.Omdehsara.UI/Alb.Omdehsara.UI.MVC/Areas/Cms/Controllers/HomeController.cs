using Alb.Omdehsara.Common;
using Alb.Omdehsara.DataAccess.Cms;
using Alb.Omdehsara.UI.MVC.Areas.Cms.Models;
using Alb.Tools.UI.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alb.Tools.Utility;
using Alb.Omdehsara.DataAccess;
using Alb.Omdehsara.UI.MVC.Controllers;
namespace Alb.Omdehsara.UI.MVC.Areas.Cms.Controllers
{
    public class HomeController : OmdehsaraControllerBase
    {
        // GET: Cms/Home
        public ActionResult Index(int Id,string title)
        {
            TblPage page = PageDA.GetPage(Id);
            if (title.Replace("-", " ") != page.Title.Purify().ToLower())
            {
                string url = Helper.GetCmsPageUrl(Url, Id, page.Title);
                return RedirectPermanent(url);
            }
            else
            {
                return View(page);
            }
        }

        [AuthorizeEnum(ProjectRoles.Admin, ProjectRoles.Admin)]
        public ActionResult PageList(PageListViewModel model)
        {
            GetPages(model);
            return View(model);
        }
        public ActionResult Archive(PageListViewModel model)
        {
            GetPages(model);
            return View(model);
        }

        private static PageListViewModel GetPages(PageListViewModel model)
        {
            int totalRecords = 0;
            model.PageSize = OmdehsaraConstant.CmsPageSize;
            if (model.PageIndex == 0)
            {
                model.PageIndex = 1;
            }
            model.PageList = PageDA.GetPages(model.PageTypeID, model.PageIndex, model.PageSize, model.IsApproved, model.CategoryId, out totalRecords);
            model.TotalRecords = totalRecords;
            return model;
        }
        public PartialViewResult PageLinks(int count, int? pageTypeID = null, int? categoryID = null)
        {
            PageLinksViewModel model = new PageLinksViewModel(){
                Pages = PageDA.GetPageList(count, pageTypeID, categoryID),
                CategoryId = categoryID
            };
            return PartialView("_PageLinks", model);
        }
        public PartialViewResult NewsArticleList(int count, int? categoryID = null)
        {
            PageLinksViewModel model = new PageLinksViewModel()
            {
                Pages = PageDA.GetNewsArticleList(count, categoryID),
                CategoryId = categoryID
            };
            return PartialView("_PageLinks", model);
        }
        [AuthorizeEnum(ProjectRoles.Admin, ProjectRoles.Admin)]
        public ActionResult Create(int? Id = null)
        {
            PageViewModel model = new PageViewModel();
            if (Id != null)
            {
                model.Page = PageDA.GetPage(Id.Value);
                model.Page.Tags = model.Page.Tags.Replace(",", Environment.NewLine);
            }
            else
            {
                model.Page = new TblPage();
            }
            return View(model);
        }

        [AuthorizeEnum(ProjectRoles.Admin, ProjectRoles.Admin)]
        public ActionResult Delete(int Id)
        {
            PageDA.DeletePage(Id);
            ShowMessage("صفحه حذف شد.", MessageTypes.Success);
            return RedirectToAction("PageList");
        }
        [AuthorizeEnum(ProjectRoles.Admin, ProjectRoles.Admin)]
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(PageViewModel model)
        {
            try
            {
                model.Page.Introduction = model.Page.Introduction.Replace("‌", " ").Trim().FixPersian();
                model.Page.Tags = model.Page.Tags.Replace(Environment.NewLine, ",");
                model.Page.Title = model.Page.Title.Replace("‌", " ").Trim().Trim();
                if (model.Page.ID > 0)
                {
                    TblPage oldPage = PageDA.GetPage(model.Page.ID);
                    if (model.MainImage!= null && !string.IsNullOrEmpty(model.MainImage.FileName))
                    {
                        if (!string.IsNullOrEmpty(oldPage.MainImageUrl))
                        {
                            System.IO.File.Delete(Server.MapPath(oldPage.MainImageUrl));
                        }
                        model.Page.MainImageUrl = Helper.GetCmsImageUrl(model.MainImage);
                        ImageTools.SaveImage(model.MainImage.InputStream, Server.MapPath(model.Page.MainImageUrl), ImageTools.Side.Height, 100);
                    }
                    else
                    {
                        model.Page.MainImageUrl = oldPage.MainImageUrl;
                    }
                    model.Page.InsDate = oldPage.InsDate;
                    model.Page.IsApproved = false;
                    PageDA.UpdatePage(model.Page);
                }
                else
                {
                    if (model.MainImage!=null && !string.IsNullOrEmpty(model.MainImage.FileName))
                    {
                        model.Page.MainImageUrl = Helper.GetCmsImageUrl(model.MainImage);
                        ImageTools.SaveImage(model.MainImage.InputStream, Server.MapPath(model.Page.MainImageUrl), ImageTools.Side.Height, 100);
                    }
                    model.Page.InsDate = DateTime.Now;
                    model.Page.UserID = CurrentUser.UserId;
                    PageDA.AddPage(model.Page);
                }
                ShowMessage("ذخیره انجام شد", MessageTypes.Success);
                return RedirectToAction("Create", new { Id = model.Page.ID });
            }
            catch(Exception ex)
            {
                ShowMessage("خطا در ذخیره اطلاعات", MessageTypes.Error);
                if (model.Page.ID > 0)
                {
                    return RedirectToAction("Create", new { Id = model.Page.ID });
                }
                else
                {
                    return RedirectToAction("Create");
                }
            }
        }
        [HttpPost]
        [AuthorizeEnum(ProjectRoles.Admin, ProjectRoles.Admin)]
        public ActionResult ApprovePage(int Id)
        {
            PageDA.ApprovePage(Id);
            return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AuthorizeEnum(ProjectRoles.Admin, ProjectRoles.Admin)]
        public ActionResult RejectPage(int Id)
        {
            PageDA.RejectPage(Id);
            return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }
    }
}
