using Alb.Tools.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alb.Omdehsara.Common.Orders
{
    public class OrderView:TblOrder
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string PersianOrderDate
        {
            get
            {
                if (OrderDate.HasValue)
                {
                    return DateTools.GetPersianDateString(OrderDate.Value);
                }
                else
                {
                    return null;
                }
            }
        }
        public string CityName { get; set; }
        public string ProvinceName { get; set; }

       
    }
}
