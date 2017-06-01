using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alb.Omdehsara.Common
{
    public partial class TblImage
    {
        public int ID { get; set; }
        public string ImageTitle { get; set; }
        public string ImageDescription { get; set; }
        public string ImageAddress { get; set; }
        public DateTime InsDate { get; set; }
    }
}
