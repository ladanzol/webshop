using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alb.Omdehsara.Common
{
    public class TblTransportType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Tehran { get; set; }
        public bool OtherCity { get; set; }
        public int TehranPrice { get; set; }
        public int OtherPrice { get; set; }
    }
}
