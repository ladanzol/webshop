using Alb.Omdehsara.Common;
using Alb.Omdehsara.DataAccess;
using Alb.Omdehsara.UI.MVC.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Alb.Omdehsara.UI.MVC.Api;
using Alb.Tools.UI.MVC.Security;
using Alb.Omdehsara.UI.MVC.Controllers;
using Alb.Tools.Common;
using Alb.Tools.Utility;
using Alb.Omdehsara.Common.Product;
using Alb.Omdehsara.UI.MVC.Models;
using Alb.Omdehsara.DataAccess.Product;
using Alb.Tools.UI.MVC;
using Alb.Omdehsara.Common.Polling;
using Alb.Omdehsara.DataAccess.Polling;

namespace Alb.Omdehsara.UI.MVC.Api
{
    public class PollingController : OmdehsaraApiControllerBase
    {
        public IHttpActionResult GetPolling(int Id)
        {
            TblQuestion question = QuestionDA.GetQuestion(Id);
            IEnumerable<TblOption> options = OptionDA.GetOptions(Id);
            PollingViewModel model = new PollingViewModel() { Question = question, Options = options };
            return Ok(model);
        }
        public IHttpActionResult setPolling(TblUserAnswer answer)
        {
            if (User.Identity.IsAuthenticated)
            {
                answer.UserID = UserID;
            }
            answer.IP = GetClientIp();
            UserAnswerDA.AddUserAnswer(answer);
            return Ok();
        }
    }
}
