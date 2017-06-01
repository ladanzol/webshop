using Alb.Common.Common;
using Alb.Common.DataAccess;
using Alb.Tools.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alb.Tools.UI.MVC.Security;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Security;
using Alb.Omdehsara.Common;
using Alb.Omdehsara.Common.Polling;
using Alb.Omdehsara.DataAccess.Polling;
using Alb.Omdehsara.UI.MVC.Models;
namespace Alb.Omdehsara.UI.MVC.Controllers
{
    public class PollingController : OmdehsaraControllerBase
    {
        public ActionResult Show(int Id)
        {
            TblQuestion question = QuestionDA.GetQuestion(Id);
            IEnumerable<TblOption> options = OptionDA.GetOptions(Id);
            PollingViewModel model = new PollingViewModel() { Question = question, Options = options };
            return View(model);
        }
    }
}
