using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alb.Omdehsara.Common.Orders;
using Alb.Omdehsara.Common;
using Alb.Omdehsara.UI.MVC.Models;
using Alb.Omdehsara.DataAccess.Orders;
namespace Alb.Omdehsara.UI.MVC.Controllers
{
    public class OrderController : OmdehsaraControllerBase
    {
        // GET: Orders
        public ActionResult List(int pageIndex = 1)
        {
            OrderListViewModel model = new OrderListViewModel();
            model.PageSize = 15;
            int totalRecords;
            model.Orders = OrderDA.GetOrders(CurrentUser.UserId, null, pageIndex,model.PageSize,out totalRecords ).Select(o => AutoMapper.Mapper.Map<OrderViewModel>(o)).ToList();
            foreach (var o in model.Orders)
            {
                o.OrderDetails = OrderDA.GetOrderDetails(o.ID);
            }
            return View(model);
        }
    }
}