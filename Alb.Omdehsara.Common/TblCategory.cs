using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alb.Omdehsara.Common
{
    public partial class TblCategory
    {
        public TblCategory()
        {
        }
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryName_En { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public bool Enabled { get; set; }
        public Nullable<int> ImageID { get; set; }
        public Nullable<short> Sort { get; set; }
        public string IconUrl { get; set; }
        public string ImageUrl1 { get; set; }
        public string ImageUrl2 { get; set; }
    }
}
