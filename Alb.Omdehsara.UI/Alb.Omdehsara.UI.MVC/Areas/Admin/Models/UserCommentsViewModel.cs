using Alb.Omdehsara.Common;
using Alb.Tools.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alb.Omdehsara.UI.MVC.Areas.Admin.Models
{
    public class UserCommentsViewModel : Paged
    {
        public IEnumerable<TblUserComment> UserComments { get; set; }
    }
}