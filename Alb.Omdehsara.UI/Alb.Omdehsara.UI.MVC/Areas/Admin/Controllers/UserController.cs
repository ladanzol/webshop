using Alb.Common.Common;
using Alb.Common.DataAccess;
using Alb.Omdehsara.Common;
using Alb.Omdehsara.DataAccess;
using Alb.Omdehsara.UI.MVC.Areas.Admin.Models;
using Alb.Omdehsara.UI.MVC.Controllers;
using Alb.Tools.UI.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alb.Omdehsara.UI.MVC.Areas.Admin.Controllers
{
   [AuthorizeEnum(OmdehsaraRoles.Admin)]
    public class UserController : OmdehsaraControllerBase
    {
        public ActionResult ManageUsers()
        {
            return View();
        }
        
        public ActionResult List(int pageIndex = 1, string userName = null)
        {
            ViewBag.UserName = userName;
            UserListViewModel model = new UserListViewModel();
            model.PageIndex = pageIndex;
            model.PageSize = 15;
            int totalRecords;
            model.Users = UserDA.GetUserList(userName, model.PageIndex, model.PageSize, out totalRecords);
            return View(model);
        }
        public JsonResult Index(int pageIndex = 1, int pageSize = 20, string userName = null)
        {
            ViewBag.UserName = userName;
            UserListViewModel model = new UserListViewModel();
            model.PageIndex = pageIndex;
            model.PageSize = pageSize;
            int totalRecords;
            model.Users = UserDA.GetUserList(userName, model.PageIndex, model.PageSize, out totalRecords);
            return Json(new { Data = model.Users, TotalCount = totalRecords }, JsonRequestBehavior.AllowGet);
        }
        [HttpPut]
        [ValidateInput(false)]
        public JsonResult Edit(TblUser user)
        {
            var orgUser = UserDA.GetUser(user.ID);
            orgUser.FirstName = user.FirstName;
            orgUser.LastName = user.LastName;
            orgUser.Pass = user.Pass;
            orgUser.Discount = user.Discount;
            orgUser.IsApproved = user.IsApproved;
            UserDA.UpdateUser(orgUser);
            return Json(new { Data = user });
        }
        public ActionResult DisableUser(int userID, int pageIndex = 1)
        {
            UserDA.DisableUser(userID);
            ShowMessage("کاربر غیر فعال شد", Tools.UI.MVC.MessageTypes.Success);
            return RedirectToAction("List", new { pageIndex = pageIndex });
        }
    }
}