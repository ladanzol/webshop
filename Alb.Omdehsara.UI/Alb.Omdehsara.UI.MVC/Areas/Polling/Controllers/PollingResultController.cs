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
using Alb.Tools.Utility;
namespace Alb.Omdehsara.UI.MVC.Areas.Polling.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PollingResultController : OmdehsaraControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<PollingResult> res = UserAnswerDA.GetPollingResult();
            var model = res.GroupBy(p => p.Question, p => new { p.OptionText, p.AnswerID, p.Count }.ToDynamic(), (key, answers) => new
            {
                Question = key,
                Answers = answers
            }.ToDynamic());

            return View(model);
        }
    }
}