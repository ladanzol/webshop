using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alb.Omdehsara.Common
{
    public partial class TblProductLocation
    {
        public int ShelfID { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public long ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime InsDate { get; set; }
        public int UserID { get; set; }
        public Int64? OrderID { get; set; }
    }
}
