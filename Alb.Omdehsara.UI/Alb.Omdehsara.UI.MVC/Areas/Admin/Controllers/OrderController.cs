using Alb.Common.Common;
using Alb.Omdehsara.Common.Orders;
using Alb.Omdehsara.DataAccess.Orders;
using Alb.Omdehsara.UI.MVC.Areas.Admin.Models;
using Alb.Omdehsara.UI.MVC.Controllers;
using Alb.Omdehsara.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alb.Omdehsara.UI.MVC.Areas.Admin.Controllers
{
    public class OrderController : OmdehsaraControllerBase
    {
        // GET: Orders
        public ActionResult List(int? userId = null,byte? status = null, int pageIndex = 1)
        {
            OrderListViewModel model = new OrderListViewModel();
            model.Status =(OrderStatus?) status;
            model.PageSize = 15;
            int totalRecords;
            model.Orders = OrderDA.GetOrders(userId, status, pageIndex, model.PageSize, out totalRecords).Select(o => AutoMapper.Mapper.Map<OrderViewModel>(o)).ToList();
            foreach (var o in model.Orders)
            {
                o.OrderDetails = OrderDA.GetOrderDetails(o.ID);
            }
            return View(model);
        }
        public ActionResult UserList(int pageIndex = 1,string userName = null)
        {
            ViewBag.UserName = userName;
            UserListViewModel model = new UserListViewModel();
            model.PageIndex = pageIndex;
            model.PageSize = 15;
            int totalRecords;
            model.Users = OrderDA.GetUserList(userName, model.PageIndex, model.PageSize, out totalRecords);
            return View(model);
        }
        public ActionResult ChangeStatus()
        {
            long orderID = Convert.ToInt64(Request["OrderID"]);
            short status = Convert.ToInt16(Request["Status"]);
            OrderDA.ChangeStatus(orderID, (OrderStatus)status);
            ShowMessage("تغییر وضعیت انجام شد", Tools.UI.MVC.MessageTypes.Success);
            return RedirectToAction("List");
        }
    }
}