﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alb.Omdehsara.UI.MVC.Areas.Cms.Models
{
    public class UploadImageViewModel
    {
        public UploadImageViewModel()
        {
           
        }
        public HttpPostedFileBase Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool HasLogo { get; set; }
        public bool UploadInImageFolder { get; set; }
    }
}