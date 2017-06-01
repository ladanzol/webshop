using Alb.Omdehsara.Common;
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
    public class QuestionController : OmdehsaraControllerBase
    {
        [HttpGet]
        public ActionResult Index(long? categoryId = null,int? Id = null)
        {
            QuestionViewModel questionModel = FillModel(Id);
            return View(questionModel);
        }

        private static QuestionViewModel FillModel(int? Id)
        {
            QuestionViewModel questionModel = new QuestionViewModel();
            if (Id.HasValue && Id !=0)
            {
                var question = QuestionDA.GetQuestion(Id);
                questionModel.Question = question;
            }
            questionModel.Questions = QuestionDA.GetQuestions();
            return questionModel;
        }
        [HttpPost]
        public ActionResult AddQuestion(QuestionViewModel model)
        {
            if (model.Question.ID > 0)
            {
                model.Question.UserID = CurrentUser.UserId;
                QuestionDA.UpdateQuestion(model.Question);
                ShowMessage("ویرایش انجام شد", Tools.UI.MVC.MessageTypes.Success);
            }
            else
            {
                model.Question.UserID = CurrentUser.UserId;
                QuestionDA.AddQuestion(model.Question);
                ShowMessage("واحد اضافه شد", Tools.UI.MVC.MessageTypes.Success);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int Id)
        {
            try
            {
                QuestionDA.DeleteQuestion(Id);
                ShowMessage("واحد حذف شد", Tools.UI.MVC.MessageTypes.Success);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ShowMessage("امکان حذف وجود ندارد", Tools.UI.MVC.MessageTypes.Error);
                return RedirectToAction("Index");
            }
        }
    }
}