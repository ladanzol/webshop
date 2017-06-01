using Alb.Omdehsara.Common.Orders;
using Alb.Omdehsara.DataAccess;
using Alb.Omdehsara.DataAccess.Orders;
using Alb.Omdehsara.UI.MVC.Models;
using Alb.Tools.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alb.Omdehsara.UI.MVC.Controllers
{
    public class PaymentController : OmdehsaraControllerBase
    {
        // GET: Payment
        public ActionResult Pay()
        {
            PaymentViewModel model = new PaymentViewModel();
            model.Banks = new SelectList(ConstantDA.ConstantList.Where(c => c.Subject == "Bank"), "ID", "Text");
            model.Payment = new Common.Orders.TblCredit();
            return View(model);
        }
        [HttpGet]
        public ActionResult GetOnline(int amount=0)
        {
            if (amount == 0)
            {
                ShowMessage("لطفا مبلغ را وارد نمایید.", Tools.UI.MVC.MessageTypes.Error);
                return RedirectToAction("Pay");
            }
            else
            {
                ShowMessage("پرداخت الکترونیک فعال نمی باشد. لطفا از روشهای دیگر پرداخت استفاده نمایید..", Tools.UI.MVC.MessageTypes.Error);
                return RedirectToAction("Pay");
            }
        }
        [HttpPost]
        public ActionResult AddPayment(PaymentViewModel model)
        {
            model.Payment.UserID = CurrentUser.UserId;
            model.Payment.PayDate = DateTools.Now();
            CreditDA.AddCredit(model.Payment);
            ShowMessage("عملیات انجام شد.", Tools.UI.MVC.MessageTypes.Success);
            return RedirectToAction("Index", "Profile");
        }
        [HttpGet]
        public ActionResult PaymentReport(int pageIndex = 1)
        {
            CreditReportViewModel model = new CreditReportViewModel();
            int totalRecords = 0;
            model.PageSize = 15;
            model.Credits = CreditDA.GetCredits(CurrentUser.UserId, pageIndex, model.PageSize, out totalRecords);
            model.PageIndex = pageIndex;
            model.TotalRecords = totalRecords;
            return View(model);
        }
    }
}