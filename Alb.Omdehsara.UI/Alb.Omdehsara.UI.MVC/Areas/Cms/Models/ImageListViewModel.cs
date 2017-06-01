using Alb.Omdehsara.Common;
using Alb.Tools.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alb.Omdehsara.UI.MVC.Areas.Cms.Models
{
    public class ImageListViewModel : Paged
    {
        public IEnumerable<TblImage> ImageList { get; set; }
    }
}