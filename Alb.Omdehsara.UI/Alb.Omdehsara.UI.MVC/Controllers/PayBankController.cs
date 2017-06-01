using Alb.Common.UI.MVC;
using Alb.Omdehsara.Common.Orders;
using Alb.Omdehsara.DataAccess;
using Alb.Omdehsara.DataAccess.Orders;
using Alb.Omdehsara.UI.MVC.Models;
using Alb.Omdehsara.UI.Web;
using Alb.Tools.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Alb.Omdehsara.UI.MVC.Controllers
{
    [Authorize]
    public class PayBankController : OmdehsaraControllerBase
    {
        public ActionResult Index(int amount)
        {
            try
            {
                Payment payment = new Payment();
                TblCredit credit = new TblCredit();
                credit.PayDate = DateTools.Now();
                credit.Price = amount;
                credit.UserID = CurrentUser.UserId;
                credit.Enabled = false;
                int creditID = Convert.ToInt32(CreditDA.AddCredit(credit));
                string result = payment.PayRequest(amount, creditID);
                ViewBag.ErrorMessage = result;
                if (result != null)
                {
                    String[] resultArray = result.Split(',');
                    if (resultArray[0] == "0")
                        ViewBag.ValueToPost = resultArray[1];
                    else
                    {
                        throw new Exception(result);
                    }
                }
                else
                {
                    throw new Exception("Return value = null");
                }
            }
            catch (Exception e)
            {
                string message = e.Message + "||";
                ErrorLogger.LogError(e, "PayBank", false);
                if (e is WebException)
                {
                    WebResponse errResp = ((WebException)e).Response;
                    if (errResp != null)
                    {
                        using (Stream respStream = errResp.GetResponseStream())
                        {
                            byte[] error = new byte[respStream.Length];
                            respStream.Read(error, 0, (int)respStream.Length);
                            message += System.Text.Encoding.Default.GetString(error);
                        }
                    }
                }
                ErrorLogger.LogError(new Exception(message), "PayBank", false);
            }
            return View();
        }
        public ActionResult In()
        {
            return View("Index");
        }
        public ActionResult Callback()
        {
            string refID = Request.Params["RefId"];
            string ResCode = Request.Params["ResCode"];
            string saleOrderID = Request.Params["SaleOrderId"];
            string saleReferenceId = Request.Params["SaleReferenceId"];

            try
            {
                if (ResCode == "0")
                {
                    string resultVerify = new Payment().VerifyRequest(int.Parse(saleOrderID), int.Parse(saleOrderID), long.Parse(saleReferenceId));
                    if (resultVerify == "0")
                    {
                        string resultSettle = new Payment().SettleRequest(int.Parse(saleOrderID), int.Parse(saleOrderID), long.Parse(saleReferenceId));
                        if (resultSettle == "0")
                        {
                            SetCredit(int.Parse(saleOrderID), saleReferenceId);
                        }
                    }
                    else
                    {
                        string resultInquiry = new Payment().InquiryRequest(int.Parse(saleOrderID), int.Parse(saleOrderID), long.Parse(saleReferenceId));
                        if (resultInquiry == "0")
                        {
                            string resultSettle = new Payment().SettleRequest(int.Parse(saleOrderID), int.Parse(saleOrderID), long.Parse(saleReferenceId));
                            if (resultSettle == "0")
                            {
                                SetCredit(int.Parse(saleOrderID), saleReferenceId);
                            }
                        }
                        else
                        {
                            ResCode = resultInquiry;
                            string resultReverse = new Payment().ReversalRequest(int.Parse(saleOrderID), int.Parse(saleOrderID), long.Parse(saleReferenceId));
                            ViewBag.ErrorMessage = "در انجام عملیات خطا رخ داده است. پرداختی انجام نشد";
                        }
                    }
                }else
                {
                    ViewBag.ErrorMessage = Payment.GetMessage(ResCode);
                }
            }
            catch
            {
                ViewBag.ErrorMessage = "در انجام عملیات خطا رخ داده است. پرداختی انجام نشد";
            }
            return View("Index");
        }

        private void SetCredit(int saleOrderID, string saleReferenceId)
        {
            TblCredit credit = CreditDA.GetCredit(saleOrderID);
            credit.PaymentRef = saleReferenceId;
            credit.Enabled = true;
            CreditDA.UpdateCredit(credit);
            ViewBag.RefId = saleReferenceId;
            //نمایش شماره پیگیری
        }
    }
}