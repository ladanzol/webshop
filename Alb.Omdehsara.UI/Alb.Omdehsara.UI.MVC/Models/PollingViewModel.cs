using Alb.Omdehsara.Common.Polling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alb.Omdehsara.UI.MVC.Models
{
    public class PollingViewModel
    {
        public IEnumerable<TblOption> Options { get; set; }
        public TblQuestion Question { get; set; }
    }
}
