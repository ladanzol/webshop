using Alb.Tools.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alb.Omdehsara.Common.Orders
{
    public partial class CreditView : TblCredit
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
        public string PersianDate
        {
            get
            {
                return DateTools.GetPersianDateString(this.PayDate);
            }
        }

    }
}
