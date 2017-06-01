using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Alb.Tools.Utility;
using Alb.Omdehsara.UI.MVC;

namespace Alb.Omdehsara.UI.Web
{
    public class Payment
    {
        public static string RefId = "";

        string PayDate = DateTools.Now().Year.ToString() + DateTools.Now().Month.ToString().PadLeft(2, '0') + DateTools.Now().Day.ToString().PadLeft(2, '0');
        string PayTime = DateTools.Now().Hour.ToString().PadLeft(2, '0') + DateTools.Now().Minute.ToString().PadLeft(2, '0') + DateTools.Now().Second.ToString().PadLeft(2, '0');

        public string PayRequest(long amount, Int64 payOrderID)
        {
            string result;
            BypassCertificateError();
            Alb.Omdehsara.UI.MVC.BPService.PaymentGatewayImplService bpService = new Alb.Omdehsara.UI.MVC.BPService.PaymentGatewayImplService();
            result = bpService.bpPayRequest(Int64.Parse(Helper.Payment.TerminalId),
                Helper.Payment.UserName,
                Helper.Payment.UserPassword,
                payOrderID,
                amount,
                PayDate,
                PayTime,
                "",
                Helper.Payment.CallBackUrl,
                0);
            return result;
        }

        public string VerifyRequest(Int64 verifyOrderId, Int64 verifySaleOrderId, Int64 verifySaleReferenceId)
        {
            string result;
            BypassCertificateError();
            Alb.Omdehsara.UI.MVC.BPService.PaymentGatewayImplService bpService = new Alb.Omdehsara.UI.MVC.BPService.PaymentGatewayImplService();
            result = bpService.bpVerifyRequest(long.Parse(Helper.Payment.TerminalId),
                Helper.Payment.UserName,
                Helper.Payment.UserPassword,
               verifyOrderId,
                verifySaleOrderId,
                verifySaleReferenceId);
            return result;
        }

        public string InquiryRequest(Int64 InquiryOrderId, Int64 InquirySaleOrderId, Int64 InquirySaleReferenceId)
        {
            string result;
            BypassCertificateError();
            Alb.Omdehsara.UI.MVC.BPService.PaymentGatewayImplService bpService = new Alb.Omdehsara.UI.MVC.BPService.PaymentGatewayImplService();
            result = bpService.bpInquiryRequest(long.Parse(Helper.Payment.TerminalId),
                Helper.Payment.UserName,
                Helper.Payment.UserPassword,
                InquiryOrderId,
                InquirySaleOrderId,
                InquirySaleReferenceId);
            return result;
        }

        public string ReversalRequest(Int64 ReversalOrderId, Int64 ReversalSaleOrderId, Int64 ReversalSaleReferenceId)
        {
            string result;
            BypassCertificateError();
            Alb.Omdehsara.UI.MVC.BPService.PaymentGatewayImplService bpService = new Alb.Omdehsara.UI.MVC.BPService.PaymentGatewayImplService();
            result = bpService.bpReversalRequest(Int64.Parse(Helper.Payment.TerminalId),
                Helper.Payment.UserName,
                Helper.Payment.UserPassword,
               ReversalOrderId,
                ReversalSaleOrderId,
               ReversalSaleReferenceId);
            return result;
        }

        public string SettleRequest(Int64 SettleOrderId, Int64 SettleSaleOrderId, Int64 SettleSaleReferenceId)
        {
            string result;
            BypassCertificateError();
            Alb.Omdehsara.UI.MVC.BPService.PaymentGatewayImplService bpService = new Alb.Omdehsara.UI.MVC.BPService.PaymentGatewayImplService();
            result = bpService.bpSettleRequest(Int64.Parse(Helper.Payment.TerminalId),
                Helper.Payment.UserName,
                Helper.Payment.UserPassword,
                SettleOrderId,
                SettleSaleOrderId,
                SettleSaleReferenceId);
            return result;
        }

        void BypassCertificateError()
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                delegate(
                    Object sender1,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
        }

        public static string GetMessage(string id)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(HttpContext.Current.Server.MapPath("~/BankMessage.config"));
            XmlNode xNode = xDoc.DocumentElement.SelectSingleNode("/Messages/msg[@no=" + id+"]");
            if (xNode != null)
                return xNode.InnerText;
            else
                return null;
        }
    }
}