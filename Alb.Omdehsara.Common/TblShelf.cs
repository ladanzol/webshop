using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alb.Omdehsara.Common
{
    public partial class TblShelf
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int StoreID { get; set; }
    }
}
