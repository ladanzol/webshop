using Alb.Omdehsara.Common;
using Alb.Tools.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alb.Omdehsara.UI.MVC.Areas.Cms.Models
{
    public class PageListViewModel:IPaged
    {
        public IEnumerable<TblPage> PageList { get; set; }
        public bool? IsApproved { get; set; }
        public int? PageTypeID { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int? CategoryId { get; set; }
    }
}