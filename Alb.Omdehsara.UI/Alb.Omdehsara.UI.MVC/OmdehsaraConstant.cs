
using Alb.Omdehsara.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alb.Omdehsara.UI.MVC
{
    public class OmdehsaraConstant
    {
       
        public static int CmsPageSize
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["CmsPageSize"]);
            }
        }
        public static int AmlakFirstPageArticleCount
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["AmlakFirstPageArticleCount"]);
            }
        }
        public static int ProductPageSize
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["ProductPageSize"]);
            }
        }
        public static int ProductTabularPageSize
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["ProductTabularPageSize"]);
            }
        }
        public static int ProductSpecialProductPageSize
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["ProductSpecialProductPageSize"]);
            }
        }
        public static int ProductSmallImageHeight
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["ProductSmallImageHeight"]);
            }
        }
        
    }
}