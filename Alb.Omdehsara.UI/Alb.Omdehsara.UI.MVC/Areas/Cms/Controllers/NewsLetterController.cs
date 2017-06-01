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
using Alborz.Tools.Utility;
using System.Configuration;
using Alb.Common.Common;
using Alb.Common.DataAccess;
using Alb.Common.UI.MVC;
namespace Alb.Omdehsara.UI.MVC.Areas.Cms.Controllers
{
    [AuthorizeEnum(ProjectRoles.Admin, ProjectRoles.BlogApprover)]
    public class NewsLetterController : OmdehsaraControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendNewsLetter()
        {
            try
            {
                IEnumerable<TblPage> newsList = PageDA.GetNotSentNews();
                int tatalRecords;
                IEnumerable<TblUser> users = UserDA.GetUserList(null, 1, int.MaxValue, out tatalRecords);

                foreach (TblUser user in users)
                {
                    string body = "<div style=\"direction:rtl;\">";
                    foreach (TblPage news in newsList)
                    {
                        body += "<div><a href=\"http://omdehsara.com" + Helper.GetCmsPageUrl(Url, news.ID, news.Title) + "\"  > " + news.Title + " </a></div>";
                    }
                    body += "</div>";
                    Mails.Send(
                                ConfigurationManager.AppSettings["newsSenderMail"],
                                user.Email,
                                ConfigurationManager.AppSettings["newsSenderPass"],
                                body,
                                "آخرین اخبار عمده سرا",
                                "http://omdehsara.com",
                                true);

                    PageDA.SetSent();

                }
                ShowMessage("خبر نامه ارسال شد", MessageTypes.Success);
                return View("Index");
            }
            catch (Exception ex)
            {
                ShowMessage("خطا در ارسال خبرنامه", MessageTypes.Error);
                ErrorLogger.LogError(ex, Request.Url.ToString(), false);
                return View("Index");
            }
        }
    }
}
