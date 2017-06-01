using Alb.Omdehsara.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alb.Omdehsara.UI.MVC.Areas.Product.Models
{
    public class UploadProductImageViewModel
    {
        public UploadProductImageViewModel()
        {
        }
        public long ProductTypeID { get; set; }
        public long CategoryID { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<TblProductImage> Images { get; set; }
        public Int16 ImageSize { get; set; }
    }
}