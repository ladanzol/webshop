using Alb.Omdehsara.Common;
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
    public class MainPageController : OmdehsaraControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = new MainPageBrandsAdminViewModel();
            model.Img1_1_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img1_1_Url").FirstOrDefault().Text);
            model.Url1_1 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url1_1").FirstOrDefault().Text);
            model.Img1_2_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img1_2_Url").FirstOrDefault().Text);
            model.Url1_2 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url1_2").FirstOrDefault().Text);

            model.Img2_1_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img2_1_Url").FirstOrDefault().Text);
            model.Url2_1 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url2_1").FirstOrDefault().Text);
            model.Img2_2_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img2_2_Url").FirstOrDefault().Text);
            model.Url2_2 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url2_2").FirstOrDefault().Text);

            model.Img3_1_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img3_1_Url").FirstOrDefault().Text);
            model.Url3_1 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url3_1").FirstOrDefault().Text);
            model.Img3_2_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img3_2_Url").FirstOrDefault().Text);
            model.Url3_2 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url3_2").FirstOrDefault().Text);

            model.Img4_1_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img4_1_Url").FirstOrDefault().Text);
            model.Url4_1 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url4_1").FirstOrDefault().Text);
            model.Img4_2_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img4_2_Url").FirstOrDefault().Text);
            model.Url4_2 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url4_2").FirstOrDefault().Text);

            model.Img5_1_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img5_1_Url").FirstOrDefault().Text);
            model.Url5_1 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url5_1").FirstOrDefault().Text);
            model.Img5_2_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img5_2_Url").FirstOrDefault().Text);
            model.Url5_2 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url5_2").FirstOrDefault().Text);

            model.Img6_1_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img6_1_Url").FirstOrDefault().Text);
            model.Url5_1 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url5_1").FirstOrDefault().Text);
            model.Img6_2_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img6_2_Url").FirstOrDefault().Text);
            model.Url6_2 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url6_2").FirstOrDefault().Text);

            model.Img7_1_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img7_1_Url").FirstOrDefault().Text);
            model.Url7_1 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url7_1").FirstOrDefault().Text);
            model.Img7_2_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img7_2_Url").FirstOrDefault().Text);
            model.Url7_2 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url7_2").FirstOrDefault().Text);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(MainPageBrandsAdminViewModel model)
        {
            if (model.Img1_1 != null)
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Img1_1_Url").FirstOrDefault();
                constant.Text = Helper.GetCmsImageUrl(model.Img1_1);
                model.Img1_1.SaveAs(Server.MapPath(constant.Text));
                model.Img1_1_Url = Url.Content(constant.Text);
                ConstantDA.UpdateConstant(constant);
            }
            if (!string.IsNullOrEmpty(model.Url1_1))
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Url1_1").FirstOrDefault();
                constant.Text = model.Url1_1;
                ConstantDA.UpdateConstant(constant);
            }
            if (model.Img1_2 != null)
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Img1_2_Url").FirstOrDefault();
                constant.Text = Helper.GetCmsImageUrl(model.Img1_2);
                model.Img1_2.SaveAs(Server.MapPath(constant.Text));
                model.Img1_2_Url = Url.Content(constant.Text);
                ConstantDA.UpdateConstant(constant);
            }
            if (!string.IsNullOrEmpty(model.Url1_2))
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Url1_2").FirstOrDefault();
                constant.Text = model.Url1_2;
                ConstantDA.UpdateConstant(constant);
            }
            if (model.Img2_1 != null)
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Img2_1_Url").FirstOrDefault();
                constant.Text = Helper.GetCmsImageUrl(model.Img2_1);
                model.Img2_1.SaveAs(Server.MapPath(constant.Text));
                model.Img2_1_Url = Url.Content(constant.Text);
                ConstantDA.UpdateConstant(constant);
            }
            if (!string.IsNullOrEmpty(model.Url2_1))
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Url2_1").FirstOrDefault();
                constant.Text = model.Url2_1;
                ConstantDA.UpdateConstant(constant);
            }
            if (model.Img2_2 != null)
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Img2_2_Url").FirstOrDefault();
                constant.Text = Helper.GetCmsImageUrl(model.Img2_2);
                model.Img2_2.SaveAs(Server.MapPath(constant.Text));
                model.Img2_2_Url = Url.Content(constant.Text);
                ConstantDA.UpdateConstant(constant);
            }
            if (!string.IsNullOrEmpty(model.Url2_2))
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Url2_2").FirstOrDefault();
                constant.Text = model.Url2_2;
                ConstantDA.UpdateConstant(constant);
            }
            if (model.Img3_1 != null)
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Img3_1_Url").FirstOrDefault();
                constant.Text = Helper.GetCmsImageUrl(model.Img3_1);
                model.Img3_1.SaveAs(Server.MapPath(constant.Text));
                model.Img3_1_Url = Url.Content(constant.Text);
                ConstantDA.UpdateConstant(constant);
            }
            if (!string.IsNullOrEmpty(model.Url3_1))
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Url3_1").FirstOrDefault();
                constant.Text = model.Url3_1;
                ConstantDA.UpdateConstant(constant);
            }
            if (model.Img3_2 != null)
                {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Img3_2_Url").FirstOrDefault();
                constant.Text = Helper.GetCmsImageUrl(model.Img3_2);
                model.Img3_2.SaveAs(Server.MapPath(constant.Text));
                model.Img3_2_Url = Url.Content(constant.Text);
                ConstantDA.UpdateConstant(constant);
            }
            if (!string.IsNullOrEmpty(model.Url3_2))
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Url3_2").FirstOrDefault();
                constant.Text = model.Url3_2;
                ConstantDA.UpdateConstant(constant);
            }
            if (model.Img4_1 != null)
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Img4_1_Url").FirstOrDefault();
                constant.Text = Helper.GetCmsImageUrl(model.Img4_1);
                model.Img4_1.SaveAs(Server.MapPath(constant.Text));
                model.Img4_1_Url = Url.Content(constant.Text);
                ConstantDA.UpdateConstant(constant);
            }
            if (!string.IsNullOrEmpty(model.Url4_1))
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Url4_1").FirstOrDefault();
                constant.Text = model.Url4_1;
                ConstantDA.UpdateConstant(constant);
            }
            if (model.Img4_2 != null)
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Img4_2_Url").FirstOrDefault();
                constant.Text = Helper.GetCmsImageUrl(model.Img4_2);
                model.Img4_2.SaveAs(Server.MapPath(constant.Text));
                model.Img4_2_Url = Url.Content(constant.Text);
                ConstantDA.UpdateConstant(constant);
            }
            if (!string.IsNullOrEmpty(model.Url4_2))
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Url4_2").FirstOrDefault();
                constant.Text = model.Url4_2;
                ConstantDA.UpdateConstant(constant);
            }
            if (model.Img5_1 != null)
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Img5_1_Url").FirstOrDefault();
                constant.Text = Helper.GetCmsImageUrl(model.Img5_1);
                model.Img5_1.SaveAs(Server.MapPath(constant.Text));
                model.Img5_1_Url = Url.Content(constant.Text);
                ConstantDA.UpdateConstant(constant);
            }
            if (!string.IsNullOrEmpty(model.Url5_1))
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Url5_1").FirstOrDefault();
                constant.Text = model.Url5_1;
                ConstantDA.UpdateConstant(constant);
            }
            if (model.Img5_2 != null)
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Img5_2_Url").FirstOrDefault();
                constant.Text = Helper.GetCmsImageUrl(model.Img5_2);
                model.Img5_2.SaveAs(Server.MapPath(constant.Text));
                model.Img5_2_Url = Url.Content(constant.Text);
                ConstantDA.UpdateConstant(constant);
            }
            if (!string.IsNullOrEmpty(model.Url5_2))
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Url5_2").FirstOrDefault();
                constant.Text = model.Url5_2;
                ConstantDA.UpdateConstant(constant);
            }
            if (model.Img6_1 != null)
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Img6_1_Url").FirstOrDefault();
                constant.Text = Helper.GetCmsImageUrl(model.Img6_1);
                model.Img6_1.SaveAs(Server.MapPath(constant.Text));
                model.Img6_1_Url = Url.Content(constant.Text);
                ConstantDA.UpdateConstant(constant);
            }
            if (!string.IsNullOrEmpty(model.Url6_1))
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Url6_1").FirstOrDefault();
                constant.Text = model.Url6_1;
                ConstantDA.UpdateConstant(constant);
            }
            if (model.Img6_2 != null)
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Img6_2_Url").FirstOrDefault();
                constant.Text = Helper.GetCmsImageUrl(model.Img6_2);
                model.Img6_2.SaveAs(Server.MapPath(constant.Text));
                model.Img6_2_Url = Url.Content(constant.Text);
                ConstantDA.UpdateConstant(constant);
            }
            if (!string.IsNullOrEmpty(model.Url6_2))
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Url6_2").FirstOrDefault();
                constant.Text = model.Url6_2;
                ConstantDA.UpdateConstant(constant);
            }
            if (model.Img7_1 != null)
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Img7_1_Url").FirstOrDefault();
                constant.Text = Helper.GetCmsImageUrl(model.Img7_1);
                model.Img7_1.SaveAs(Server.MapPath(constant.Text));
                model.Img7_1_Url = Url.Content( constant.Text);
                ConstantDA.UpdateConstant(constant);
            }
            if (!string.IsNullOrEmpty(model.Url7_1))
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Url7_1").FirstOrDefault();
                constant.Text = model.Url7_1;
                ConstantDA.UpdateConstant(constant);
            }
            if (model.Img7_2 != null)
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Img7_2_Url").FirstOrDefault();
                constant.Text = Helper.GetCmsImageUrl(model.Img7_2);
                model.Img7_2.SaveAs(Server.MapPath(constant.Text));
                model.Img7_2_Url = Url.Content(constant.Text);
                ConstantDA.UpdateConstant(constant);
            }
            if (!string.IsNullOrEmpty(model.Url7_2))
            {
                var constant = ConstantDA.ConstantList.Where(c => c.Subject == "Url7_2").FirstOrDefault();
                constant.Text = model.Url7_2;
                ConstantDA.UpdateConstant(constant);
            }
            ShowMessage("ثبت انجام شد", Tools.UI.MVC.MessageTypes.Success);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult SetFooter()
        {
            TblConstant constant = ConstantDA.ConstantList.Where(c => c.Subject == "Footer").First();
            return View(constant);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SetFooter(TblConstant tblConstant)
        {
            ConstantDA.UpdateConstant(tblConstant);
            ShowMessage("ثبت انجام شد", Tools.UI.MVC.MessageTypes.Success);
            return RedirectToAction("SetFooter");
        }

        [HttpGet]
        public ActionResult SetTopMenu()
        {
            TblConstant constant = ConstantDA.ConstantList.Where(c => c.Subject == "TopMenu").First();
            return View(constant);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SetTopMenu(TblConstant tblConstant)
        {
            ConstantDA.UpdateConstant(tblConstant);
            ShowMessage("ثبت انجام شد", Tools.UI.MVC.MessageTypes.Success);
            return RedirectToAction("SetTopMenu");
        }
    }
}