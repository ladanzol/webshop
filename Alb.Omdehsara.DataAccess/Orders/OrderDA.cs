using Alb.Tools.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Alb.Omdehsara.Common;
using System.Data;
using Alb.Omdehsara.Common.Orders;
using AutoMapper.Mappers;
using AutoMapper;
using Alb.Tools.Common;
using Alb.Omdehsara.Common.Product;
using Alb.Omdehsara.DataAccess.Product;
using Alb.Tools.Utility;
using Alb.Common.DataAccess;
using Alb.Common.Common;
using System.Transactions;
namespace Alb.Omdehsara.DataAccess.Orders
{
    public class OrderDA : AlbDataAccessBase
    {
        public static long AddToCart(OrderDetail orderDetail, string clientID, int? customerID = null)
        {
            if (orderDetail.OrderID == 0)
            {
                TblOrder order = new TblOrder();
                order.Status = (int)OrderStatus.Created;
                order.ClientID = clientID;
                order.CustomerID = customerID;
                orderDetail.OrderID = GetConnection().Query<long>("usp_Insert_TblOrder", order, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            AddOrderDetail(orderDetail);
            return orderDetail.OrderID;
        }
        private static void AddOrderDetail(OrderDetail orderDetail)
        {
            var product = ProductDA.GetProductByColorAndSize(orderDetail.ProductTypeID, orderDetail.SizeID, orderDetail.ColorID);
            var con = GetConnection();
            if (product != null && product.Quantity >= orderDetail.Quantity)
            {
                orderDetail.ProductID = product.ID;
                orderDetail.UnitPrice = product.Price;
                con.Execute("usp_Insert_TblOrderDetail", AutoMapper.Mapper.Map<TblOrderDetail>(orderDetail), commandType: CommandType.StoredProcedure);
                ProductDA.UpdateProductQuantity(orderDetail.ProductID, product.Quantity.Value - orderDetail.Quantity);
            }
            else
            {
                throw new RuleException("از رنگ سایز مورد نظر شما به تعداد کافی موجود نمی باشد.");
            }
        }

        public static IEnumerable<OrderDetail> GetClientOrderDetails(string ClientID)
        {
            return GetConnection().Query<OrderDetail>("usp_GetClientOrderDetails", new { ClientID = ClientID }, commandType: CommandType.StoredProcedure);
        }
        public static IEnumerable<OrderDetail> GetOrderDetails(long orderID)
        {
            return GetConnection().Query<OrderDetail>("usp_GetOrderDetails", new { orderID = orderID }, commandType: CommandType.StoredProcedure);
        }
        public static TblOrderDetail GetOrderDetail(long orderID, long productID)
        {
            return GetConnection().Query<TblOrderDetail>("usp_GetOrderDetail", new { orderID = orderID, productID = productID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }
        public static void DeleteOrderDetail(TblOrderDetail orderDetail)
        {
            using (TransactionScope trans=  CreateTransactionScope())
            {
                GetConnection().Execute("usp_Delete_TblOrderDetail", new { ProductID = orderDetail.ProductID, OrderID = orderDetail.OrderID }, commandType: CommandType.StoredProcedure);
                ProductView product = ProductDA.GetProduct(orderDetail.ProductID);
                ProductDA.UpdateProductQuantity(orderDetail.ProductID, product.Quantity.Value + orderDetail.Quantity);
                trans.Complete();
            }
        }

        public static void ChangeOrderDetailQuantity(OrderDetail orderDetail, string clientId, int? customerID = null)
        {
            var con = GetConnection();
            var product = ProductDA.GetProductByColorAndSize(orderDetail.ProductTypeID, orderDetail.SizeID, orderDetail.ColorID);
            TblOrderDetail oldDetail = GetOrderDetail(orderDetail.OrderID, orderDetail.ProductID);
            if (product != null && oldDetail == null)
            {
                AddToCart(orderDetail, clientId, customerID);
            }
            else
            {
                if (product != null && product.Quantity >= orderDetail.Quantity - oldDetail.Quantity)
                {
                    if (oldDetail.Quantity != orderDetail.Quantity)
                    {
                        ProductDA.UpdateProductQuantity(oldDetail.ProductID, product.Quantity.Value + oldDetail.Quantity - orderDetail.Quantity);
                        con.Execute("usp_TblOrderDetail_ChangeOrderDetailQuantity", new { Quantity = orderDetail.Quantity, ProductID = product.ID, OrderID = orderDetail.OrderID }, commandType: CommandType.StoredProcedure);
                    }
                }
                else
                {
                    throw new RuleException("از رنگ و سایز مورد نظر شما به تعداد کافی موجود نمی باشد.");
                }
            }
        }

        public static void SetOrderAddress(TblOrder order)
        {
            TblOrder ord = GetLastUserOrder(order.CustomerID);
            //TblOrder order = GetOrder(order.ID);
            //if (ord == null || ord.ID != order.ID)
            //{
            //    throw new RuleException("امکان ثبت آدرس برای  این سفارش وجود ندارد.لطفا با پشتبانی سایت تماس بگیرید");
            //}
            order.ID = ord.ID;
            ord.OrderDate = DateTools.Now();
            order.Status = (byte)OrderStatus.Created;
            ord.Address = order.Address;
            ord.PostalCode = order.PostalCode;
            ord.CityID = order.CityID;
            ord.CustomerID = order.CustomerID;
            ord.CustomerName = order.CustomerName;
            ord.Phone = order.Phone;
            CalcuateOrderPrice(ord);
            UpdateOrder(ord);
        }
        private static void CalcuateOrderPrice(TblOrder order)
        {
            IEnumerable<TblOrderDetail> details = GetDetails(order.ID);
            int orderQuantity = 0;
            order.OrderPrice = 0;
            foreach (var detail in details)
            {
                orderQuantity += detail.Quantity;
                order.OrderPrice += detail.Quantity * detail.UnitPrice;
            }
            int countDiscount = Convert.ToInt32(ConstantDA.ConstantList.First(c => c.Subject == "CountDiscount").Text);
            int countDiscountPercent = Convert.ToInt32(ConstantDA.ConstantList.First(c => c.Subject == "CountDiscountPercent").Text);
            int userDiscount = UserDA.GetUserDiscount(order.CustomerID.Value);
            int level1Price = Convert.ToInt32(ConstantDA.ConstantList.First(c => c.Subject == "Level1Price").Text);
            int levelDiscount = 0;
            if (order.OrderPrice > level1Price)
            {
                int level2Price = Convert.ToInt32(ConstantDA.ConstantList.First(c => c.Subject == "Level2Price").Text);
                if (order.OrderPrice > level2Price)
                {
                    int level3Price = Convert.ToInt32(ConstantDA.ConstantList.First(c => c.Subject == "Level3Price").Text);
                    if (order.OrderPrice > level3Price)
                    {
                        levelDiscount = Convert.ToInt32(ConstantDA.ConstantList.First(c => c.Subject == "Level3Discount").Text);
                    }
                    else
                    {
                        levelDiscount = Convert.ToInt32(ConstantDA.ConstantList.First(c => c.Subject == "Level2Discount").Text);
                    }
                }
                else
                {
                    levelDiscount = Convert.ToInt32(ConstantDA.ConstantList.First(c => c.Subject == "Level1Discount").Text);
                }
            }
            if (orderQuantity >= countDiscount)
            {
                order.OrderDiscount = Convert.ToInt32(order.OrderPrice.Value * (Convert.ToDouble((countDiscountPercent + userDiscount + levelDiscount)) / 100));
            }
            else
            {
                order.OrderDiscount = Convert.ToInt32(order.OrderPrice.Value * (Convert.ToDouble(userDiscount + levelDiscount) / 100));
            }
            order.OrderFinalPrice = order.OrderPrice - order.OrderDiscount + (order.Freight ?? 0);
        }

        private static IEnumerable<TblOrderDetail> GetDetails(long orderID)
        {
            return GetConnection().Query<TblOrderDetail>("select * from TblOrderDetail where OrderID=@OrderID", new { OrderID = orderID });
        }

        private static void UpdateOrder(TblOrder order)
        {
            GetConnection().Execute("usp_Update_TblOrder", order, commandType: CommandType.StoredProcedure);
        }

        public static TblOrder GetLastUserOrder(int? customerID)
        {
            return GetConnection().Query<TblOrder>("usp_GetLastUserOrder", new { customerID = customerID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public static void SetOrderCustomer(long orderId, int UserID)
        {
            TblOrder order = GetOrder(orderId);
            order.CustomerID = UserID;
            GetConnection().Execute("usp_Update_TblOrder", order, commandType: CommandType.StoredProcedure);
        }

        public static TblOrder GetOrder(long orderId)
        {
            return GetConnection().Query<TblOrder>("select * from tblOrder where Id=@orderId", new { orderId = orderId }, commandType: CommandType.Text).FirstOrDefault();
        }

        public static IEnumerable<OrderView> GetOrders(int? userID, byte? status, int pageIndex, int pageSize,out int totalRecords)
        {
            return GetMultiple<OrderView>("usp_GetOrders", new { userID = userID,status =status, pageSize = pageSize, pageIndex = pageIndex },out totalRecords);
        }

        public static void ChangeStatus(long orderID, OrderStatus status)
        {
            using (var trans = CreateTransactionScope())
            {
                GetConnection().Execute("usp_TblOrder_ChangeStatus", new { orderID = orderID, status = status }, commandType: CommandType.StoredProcedure);
                if (status == OrderStatus.Canceled)
                {
                    OrderCanceled(orderID);
                }
                trans.Complete();
            }
        }

        private static void OrderCanceled(long orderID)
        {
            GetConnection().Execute("usp_OrderCanceled", new { orderID = orderID }, commandType: CommandType.StoredProcedure);
        }

        public static IEnumerable<TblUser> GetUserList(string userName, int pageIndex, int pageSize, out int totalRecords)
        {
            return GetMultiple<TblUser>("usp_GetCustomerList", new { userName = userName, pageSize = pageSize, pageIndex = pageIndex }, out totalRecords);
        }

        public static IEnumerable<dynamic> GetOrderTransportTypes(int cityId)
        {
            if (cityId == 924)//Tehran
            {
                return TblTransportTypeDA.GetTransportTypes().Where(t => t.Tehran).Select(t => new { t.ID, t.Name, Price = t.TehranPrice });
            }
            else
            {
                return TblTransportTypeDA.GetTransportTypes().Where(t => t.OtherCity).Select(t => new { t.ID, t.Name, Price = t.OtherPrice });
            }
        }
        public static void SetOrderTransportType(long orderId, int TransportTypeId)
        {
            GetConnection().Execute("usp_SetOrderTransportType", new { orderID = orderId, TransportTypeId = TransportTypeId }, commandType: CommandType.StoredProcedure);
        }

        public static dynamic GetCustomerAddress(int UserID)
        {
            return GetConnection().Query("usp_GetCustomerAddress", new { CustomerID = UserID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }
    }
}
