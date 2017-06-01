using Alb.Omdehsara.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alb.Omdehsara.UI.MVC.Api.Models
{
    public class CategoryViewModel
    {
        public IEnumerable<CategoryPropertyViewModel> CategoryProperties { get; set; }
        public IEnumerable<TblCategory> Categories { get; set; }
    }
}