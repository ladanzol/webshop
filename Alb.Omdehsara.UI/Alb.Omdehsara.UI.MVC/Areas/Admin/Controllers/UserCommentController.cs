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
    public class UserCommentController : OmdehsaraControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserComments(int pageIndex =1)
        {
            UserCommentsViewModel model = new UserCommentsViewModel();
            model.PageSize = 20;
            model.PageIndex = pageIndex;
            int totalRecords;
            model.UserComments = UserCommentDA.GetComments(model.PageIndex, model.PageSize, out totalRecords);
            model.TotalRecords = totalRecords;
            return View(model);
        }
    }
}