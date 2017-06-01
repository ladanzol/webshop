using Alb.Omdehsara.Common;
using Alb.Omdehsara.Common.Polling;
using Alb.Omdehsara.Common.Product;
using Alb.Omdehsara.DataAccess;
using Alb.Omdehsara.DataAccess.Polling;
using Alb.Omdehsara.UI.MVC.Areas.Admin.Models;
using Alb.Omdehsara.UI.MVC.Areas.Polling.Models;
using Alb.Omdehsara.UI.MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alb.Omdehsara.UI.MVC.Areas.Polling.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OptionController : OmdehsaraControllerBase
    {
        [HttpGet]
        public ActionResult Index(int questionId, int? Id = null)
        {
            OptionViewModel optionModel = FillModel(questionId, Id);
            return View(optionModel);
        }

        private static OptionViewModel FillModel(int questionId, int? Id = null)
        {
            OptionViewModel optionModel = new OptionViewModel();
            if (Id.HasValue && Id !=0 )
            {
                var option = OptionDA.GetOption(Id);
                optionModel.Option = option;
            }
            else
            {
                optionModel.Option = new Common.Polling.TblOption();
                optionModel.Option.QuestionID = questionId;
            }
            optionModel.Options = OptionDA.GetOptions(questionId);
            return optionModel;
        }

        [HttpPost]
        public ActionResult AddOption(OptionViewModel model)
        {
            if (model.Option.ID > 0)
            {
                OptionDA.UpdateOption(model.Option);
                ShowMessage("ویرایش انجام شد", Tools.UI.MVC.MessageTypes.Success);
            }
            else
            {
                OptionDA.AddOption(model.Option);
                ShowMessage("واحد اضافه شد", Tools.UI.MVC.MessageTypes.Success);
            }
            return RedirectToAction("Index", new {questionId= model.Option.QuestionID });
        }

        public ActionResult Delete(int Id)
        {
            try
            {
                TblOption option = OptionDA.GetOption(Id);
                OptionDA.DeleteOption(Id);
                ShowMessage("واحد حذف شد", Tools.UI.MVC.MessageTypes.Success);
                return RedirectToAction("Index", new { questionId = option.QuestionID });
            }
            catch (Exception ex)
            {
                ShowMessage("امکان حذف وجود ندارد", Tools.UI.MVC.MessageTypes.Error);
                return RedirectToAction("Index");
            }
        }
    }
}