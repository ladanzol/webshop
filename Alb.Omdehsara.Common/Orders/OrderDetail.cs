using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alb.Omdehsara.Common.Orders
{
    public class OrderDetail : TblOrderDetail
    {
        public Nullable<int> ColorID { get; set; }
        public Nullable<int> SizeID { get; set; }
        public long ProductTypeID { get; set; }
        public long CategoryID { get; set; }
        public string ProductTitle { get; set; }
        public string SizeName { get; set; }
        public string ColorName { get; set; }
        public string BrandName { get; set; }
        public int ProductQuantity { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public int MinOrderQuantity { get; set; }
        public int UnitQuantity { get; set; }
        public string UnitName { get; set; }
    }
}
