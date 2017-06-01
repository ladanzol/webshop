
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alb.Omdehsara.Common
{
    public partial class TblPage
    {
        public TblPage()
        {
            IsApproved = false;
            LanguageID = 1;
        }
        public int ID { get; set; }
        public int PageTypeID { get; set; }
        public int LanguageID { get; set; }
        public string Body { get; set; }
        public string Introduction { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int UserID { get; set; }
        public int VisitCount { get; set; }
        public DateTime InsDate { get; set; }
        public bool IsApproved { get; set; }
        public string Tags { get; set; }
        public string MainImageUrl { get; set; }
        public bool SentAsNewsLetter { get; set; }
    }
}
