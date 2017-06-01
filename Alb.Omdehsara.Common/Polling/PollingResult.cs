using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alb.Omdehsara.Common.Polling
{
    public class PollingResult
    {
        public int ID    { get; set; }
        public string Question { get; set; }
        public int AnswerID { get; set; }
        public string OptionText { get; set; }
        public int Count { get; set; }
    }
}
