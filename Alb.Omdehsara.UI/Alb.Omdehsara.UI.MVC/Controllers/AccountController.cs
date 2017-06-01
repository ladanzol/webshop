using Alb.Common.Common;
using Alb.Common.DataAccess;
using Alb.Omdehsara.UI.MVC.Models;
using Alb.Tools.UI.MVC;
using Alb.Tools.UI.MVC.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Alb.Omdehsara.UI.MVC.Controllers
{

    public class AccountController : OmdehsaraControllerBase
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                TblUser user = UserDA.GetUserByUserName(model.Username);

                if (user != null && user.Pass == model.Password)
                {
                    if (!user.IsApproved)
                    {
                        ShowMessage("نام کابری شما فعال نمی باشد", MessageTypes.Error);
                    }
                    else
                    {
                        var roles = UserDA.GetUserRoleByUserName(user.ID).Select(role => role.RoleName).ToArray();

                        CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                        serializeModel.UserId = user.ID;
                        serializeModel.FirstName = user.FirstName;
                        serializeModel.LastName = user.LastName;
                        serializeModel.roles = roles;

                        string userData = JsonConvert.SerializeObject(serializeModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                 1,
                                user.Email,
                                 DateTime.Now,
                                 DateTime.Now.AddMinutes(30),
                                 model.RememberMe,
                                 userData);

                        string encTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                        Response.Cookies.Add(faCookie);
                        string returnUrl = HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["returnUrl"];

                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl.Replace("@","#"));
                        }
                        else
                        {
                            if (roles.Contains("Admin"))
                            {
                                return RedirectToRoute("admin_default", new { action = "Index", controller = "Home" });
                            }
                            else
                            {
                                return RedirectToAction("index", "profile");
                            }
                        }
                    }
                }
                else
                {
                    ShowMessage("نام کاربری یا رمز عبور اشتباه است", MessageTypes.Error);
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Account", null);
        }
        [HttpGet]
        public ActionResult Register(int? Id = null)
        {
            TblUser user = null;
            if (Id.HasValue)
            {
                user = UserDA.GetUser(Id);
            }
            return View(user ?? new TblUser());
        }
        [HttpPost]
        public ActionResult Register(TblUser user)
        {
            TblUser us = UserDA.GetUserByUserName(user.Email);
            if (us != null)
            {
                ShowMessage("این ایمیل قبلا ثبت نام شده است", MessageTypes.Error);
                return View(user);
            }
            else
            {
                if (user.Pass != Request["RepeatPass"])
                {
                    ShowMessage("رمز عبور و تکرار آن برابر نیستند", MessageTypes.Error);
                    return View(user);
                }
                else
                {
                    UserDA.AddUser(user);
                    ShowMessage("ثبت نام انجام شد", MessageTypes.Success);
                    return RedirectToAction("index", "profile");
                }
            }
        }
    }
}
