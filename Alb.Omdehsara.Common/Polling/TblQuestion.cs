using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alb.Omdehsara.Common.Polling
{
    public class TblQuestion
    {
        public int ID { get; set; }
        public int? UserID { get; set; }
        public string Question { get; set; }
        public bool Enabled { get; set; }
    }
}
