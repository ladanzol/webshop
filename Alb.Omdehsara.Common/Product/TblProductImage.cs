using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alb.Omdehsara.Common
{
    public class TblProductImage
    {
        public Int64 ProductID { get; set; }
        public Int64 ImageID { get; set; }
        public Int16 ImageSize { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string ImageDesc { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
