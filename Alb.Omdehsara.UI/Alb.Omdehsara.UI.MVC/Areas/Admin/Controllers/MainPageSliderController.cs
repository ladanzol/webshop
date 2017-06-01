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
    public class MainPageSliderController : OmdehsaraControllerBase
    {
        public ActionResult Index()
        {
            var model = new MainPageSliderViewModel();
            model.SliderHtml = ConstantDA.ConstantList.Where(c => c.Subject == "MainPageSliderHtml").First().Text;
            model.SliderScript = ConstantDA.ConstantList.Where(c => c.Subject == "MainPageSliderScript").First().Text;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(MainPageSliderViewModel model)
        {
            TblConstant mainpageSliderHtml = ConstantDA.ConstantList.Where(c => c.Subject == "MainPageSliderHtml").First();
            mainpageSliderHtml.Text = model.SliderHtml;
            ConstantDA.UpdateConstant(mainpageSliderHtml);

            TblConstant mainpageSliderScript = ConstantDA.ConstantList.Where(c => c.Subject == "MainPageSliderScript").First();
            mainpageSliderScript.Text = model.SliderScript;
            ConstantDA.UpdateConstant(mainpageSliderScript);
            ShowMessage("ذخیره سازی انجام شد", Tools.UI.MVC.MessageTypes.Success);
            return View("Index", model);
        }
    }
}