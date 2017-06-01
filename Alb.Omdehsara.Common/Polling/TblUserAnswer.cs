using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alb.Omdehsara.Common.Polling
{
    public class TblUserAnswer
    {
        public int ID { get; set; }
        public string ClientID { get; set; }
        public string IP { get; set; }
        public int? UserID { get; set; }
        public int AnswerID { get; set; }
    }
}
