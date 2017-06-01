using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alb.Omdehsara.Common.Orders
{
    public class TblOrderDetail 
    {
        public long OrderID { get; set; }
        public long ProductID { get; set; }
        public int UnitPrice { get; set; }
        public short Quantity { get; set; }
    }
}
