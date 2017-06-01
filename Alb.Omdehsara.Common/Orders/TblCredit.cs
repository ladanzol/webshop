using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alb.Omdehsara.Common.Orders
{
    public partial class TblCredit
    {
        public int ID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string PaymentRef { get; set; }
        public Nullable<int> Price { get; set; }
        public string Description { get; set; }
        public DateTime PayDate { get; set; }
        public bool Enabled { get; set; }
        public Nullable<int> CurrentAmount { get; set; }
        public Nullable<int> PaymentTypeID { get; set; }
        public Nullable<int> BankID { get; set; }

    }
}
