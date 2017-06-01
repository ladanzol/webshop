using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alb.Omdehsara.Common.Orders
{
    public enum OrderStatus
    {
        Created = 1,//سفارش توسط کاربر ایجاد شده
        Confirmed = 2,//سفارش بوسیله ادمین برای ارسال تایید شده
        Shipped = 3,//سفارش ارسال شده
        Recieved = 4,//دریفات سفارش بوسیله مشتری تایید شده
        Canceled = 5//سفارش لغو شده
    }
    public enum PaymentTypes
    {
        OnlinePayment = 1,
        CartToCart = 2,
        ToAccount = 3,
        Credit = 4,
        InPlace = 5
    }
}
