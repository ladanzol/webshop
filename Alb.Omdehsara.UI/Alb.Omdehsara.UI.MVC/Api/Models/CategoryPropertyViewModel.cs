using Alb.Omdehsara.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alb.Omdehsara.UI.MVC.Api.Models
{
    public class CategoryPropertyViewModel:TblCategoryProperty
    {
        public IEnumerable<TblConstant> ConstantList { get; set; }
    }
}