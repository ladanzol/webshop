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
using Alb.Omdehsara.Common.Orders;
using Alb.Omdehsara.DataAccess.Orders;
using Alb.Common.DataAccess;

namespace Alb.Omdehsara.UI.MVC.Api
{
    public class ShoppingController : OmdehsaraApiControllerBase
    {
        [HttpPost]
        public IHttpActionResult AddToCart(OrderDetailViewModel order)
        {
            try
            {
                if (IsAuthenticated)
                {
                    return Ok(OrderDA.AddToCart(order, order.ClientID, UserID));
                }
                else
                {
                    return Ok(OrderDA.AddToCart(order, order.ClientID));
                }
            }
            catch (Exception ex)
            {
                return HttpException(ex);
            }
        }
        [HttpPost]
        public IHttpActionResult GetClientOrderDetails(string Id)
        {
            return Ok(OrderDA.GetClientOrderDetails(Id));
        }
        [HttpPost]
        public IHttpActionResult DeleteOrderDetail(TblOrderDetail orderDetail)
        {
            OrderDA.DeleteOrderDetail(orderDetail);
            return Ok();
        }
        [HttpPost]
        public IHttpActionResult ChangeOrderDetailQuantity(OrderDetailViewModel orderDetail)
        {
            if (IsAuthenticated)
            {
                OrderDA.ChangeOrderDetailQuantity(orderDetail, orderDetail.ClientID, UserID);
            }
            else
            {
                OrderDA.ChangeOrderDetailQuantity(orderDetail, orderDetail.ClientID);
            }
            return Ok();
        }
        [HttpGet]
        public IHttpActionResult GetDiscount()
        {
            int userDiscount = 0;
            if (IsAuthenticated)
            {
                userDiscount = UserDA.GetUserDiscount(UserID);
            }
            return Ok(new
            {
                UserDiscount = userDiscount,
                CountDiscount = Convert.ToInt32(ConstantDA.ConstantList.First(c => c.Subject == "CountDiscount").Text),
                CountDiscountPercent = Convert.ToInt32(ConstantDA.ConstantList.First(c => c.Subject == "CountDiscountPercent").Text),
                Level1Price = Convert.ToInt32(ConstantDA.ConstantList.First(c => c.Subject == "Level1Price").Text),
                Level1Discount = Convert.ToInt32(ConstantDA.ConstantList.First(c => c.Subject == "Level1Discount").Text),
                Level2Price = Convert.ToInt32(ConstantDA.ConstantList.First(c => c.Subject == "Level2Price").Text),
                Level2Discount = Convert.ToInt32(ConstantDA.ConstantList.First(c => c.Subject == "Level2Discount").Text),
                Level3Price = Convert.ToInt32(ConstantDA.ConstantList.First(c => c.Subject == "Level3Price").Text),
                Level3Discount = Convert.ToInt32(ConstantDA.ConstantList.First(c => c.Subject == "Level3Discount").Text)
            });
        }
        [Authorize]
        [HttpPost]
        public IHttpActionResult SetOrderCustomer(object orderId)
        {
            OrderDA.SetOrderCustomer((long)orderId, UserID);
            return Ok();
        }
        [Authorize]
        [HttpGet]
        public IHttpActionResult GetCustomerAddress()
        {
            return Ok(OrderDA.GetCustomerAddress(UserID));
        }
        [Authorize]
        [HttpPost]
        public IHttpActionResult SetOrderAddress(TblOrder order)
        {
            order.CustomerID = UserID;
            OrderDA.SetOrderAddress(order);
            return Ok();
        }
        [Authorize]
        [HttpGet]
        public IHttpActionResult GetOrderTransportTypes()
        {
            TblOrder order = OrderDA.GetLastUserOrder(UserID);
            return Ok(OrderDA.GetOrderTransportTypes(order.CityID.Value));
        }
        [Authorize]
        [HttpPost]
        public IHttpActionResult SetOrderTransportType(dynamic data)
        {
            TblOrder order = OrderDA.GetLastUserOrder(UserID);
            OrderDA.SetOrderTransportType((long)order.ID, (int)data.TransportTypeID);
            return Ok();
        }   
        [HttpGet]
        public IHttpActionResult GetOrderPrice()
        {
            var order = OrderDA.GetLastUserOrder(UserID);
            return Ok(order.OrderFinalPrice + order.TransportPrice ?? 0);
        }

        [HttpGet]
        public IHttpActionResult GetBanks()
        {
            return Ok(ConstantDA.ConstantList.Where(c => c.Subject == "Bank"));
        }
        [HttpGet]
        public IHttpActionResult GetUserCreditAcount()
        {
            return Ok(CreditDA.GetUserCreditAcount(UserID));
        }
        [HttpPost]
        public IHttpActionResult SetCredit(TblCredit credit)
        {
            credit.UserID = UserID;
            credit.PayDate = DateTools.Now();
            CreditDA.AddCredit(credit);
            return Ok();
        }
        [HttpGet]
        public IHttpActionResult GetConstants()
        {
            return Ok(new
            {
                MinCount = ConstantDA.ConstantList.Where(c => c.Subject == "MinCount").First().Text
            });
        }
    }
}
