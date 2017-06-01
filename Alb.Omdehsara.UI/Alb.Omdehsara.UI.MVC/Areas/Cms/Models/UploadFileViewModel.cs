using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alb.Omdehsara.UI.MVC.Areas.Cms.Models
{
    public class UploadFileViewModel
    {
        public HttpPostedFile File { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}