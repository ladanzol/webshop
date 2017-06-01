using Alb.Omdehsara.Common;
using Alb.Omdehsara.DataAccess;
using Alb.Tools.Utility;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Alb.Tools.UI.MVC;
using System.Text.RegularExpressions;
using Alb.Omdehsara.UI.MVC.Models;
using Alb.Omdehsara.Common.Product;
using Alb.Omdehsara.DataAccess.Product;
using Alb.Omdehsara.UI.MVC.Api.Models;
using Alb.Omdehsara.Common.Orders;
namespace Alb.Omdehsara.UI.MVC
{
    public static class Helper
    {
      
   
        public static string ImageFolder
        {
            get
            {
                return "~/Images/";
            }
        }
        public static string BarCodeFolder
        {
            get
            {
                return "~/BarCode/";
            }
        }
        public static string GetAppRootUrl(bool endSlash = true)
        {
            string host = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            string appRootUrl = HttpContext.Current.Request.ApplicationPath;
            if (!appRootUrl.EndsWith("/")) //a virtual
            {
                appRootUrl += "/";
            }
            if (!endSlash)
            {
                appRootUrl = appRootUrl.Substring(0, appRootUrl.Length - 1);
            }
            return host + appRootUrl;
        }
        public static string Host
        {
            get
            {
                if (IsLocal())
                {
                    return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath;
                }
                else
                {
                    return ConfigurationManager.AppSettings["Host"];
                }
            }
        }
        public static string GetPageUrl(UrlHelper helper, int index)
        {
            string url = helper.RequestContext.HttpContext.Request.Url.PathAndQuery;
            if (url.Contains("pageIndex"))
            {
                Regex reg = new Regex("pageIndex=\\d+");
                url = reg.Replace(url, "pageIndex=" + index);
            }
            else
            {
                if (!url.Contains("?"))
                {
                    url += "?pageIndex=" + index;
                }
                else
                {
                    url += "&pageIndex=" + index;
                }
            }
            return url;
        }
        //internal static string SaveImage(HttpPostedFile file, int adsID, string title, out int imageId)
        //{
          
            
        //    string basePath = HttpContext.Current.Server.MapPath("~" + ImageFolder );
        //    if (!Directory.Exists(basePath))
        //    {
        //        Directory.CreateDirectory(basePath);
        //    }
        //    basePath = Path.Combine(basePath, DateF.Month.ToString());
        //    if (!Directory.Exists(basePath))
        //    {
        //        Directory.CreateDirectory(basePath);
        //    }
        //    basePath = Path.Combine(basePath, DateF.Day.ToString());
        //    if (!Directory.Exists(basePath))
        //    {
        //        Directory.CreateDirectory(basePath);
        //    }
        //    var date = DateTime.Now;
        //    string img = adsID.ToString() + "-" + date.Hour + "-" + date.Minute + "-" + date.Second + "-" + date.Millisecond + Path.GetExtension(file.FileName);
        //    string imagePath = basePath + "\\" + img;

        //    ImageTools.SaveImage(file.InputStream, imagePath, ImageTools.Side.Width, Helper.TransLogoPath, OmdehsaraConstant.MainImageWidth);

        //    string s1ImagePath = basePath + "\\" + "s1-" + img;
        //    string s2ImagePath = basePath + "\\" + "s2-" + img;
        //    string s3ImagePath = basePath + "\\" + "s3-" + img;
        //    ImageTools.SaveImage(imagePath, s1ImagePath, ImageTools.Side.Width, OmdehsaraConstant.S1ImageWidth);

        //    ImageTools.SaveImage(imagePath, s2ImagePath, ImageTools.Side.Width, OmdehsaraConstant.S2ImageWidth);

        //    ImageTools.SaveImage(imagePath, s3ImagePath, ImageTools.Side.Width, OmdehsaraConstant.S3ImageWidth);

        //    imageId = TblProductImageDA.AddImage(adsID, img, title, DateF);

        //    return s3ImagePath;
        //}

    
        private static bool IsLocal()
        {
            return !HttpContext.Current.Request.ApplicationPath.EndsWith("/");
        }

        public static string TransLogoPath
        {
            get
            {
                return HttpContext.Current.Server.MapPath("~/content/images/trans200.png");
            }
        }
        internal static string GetCmsImageUrl(HttpPostedFileBase file)
        {
            DateTime createDate = DateTime.Now;
            string cmsImageFolder = "~/Content/Cms/images/";
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(cmsImageFolder + createDate.Year)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(cmsImageFolder + createDate.Year));
            }
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(cmsImageFolder + createDate.Year + "/" + createDate.Month)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(cmsImageFolder + createDate.Year + "/" + createDate.Month));
            }
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(cmsImageFolder + createDate.Year + "/" + createDate.Month + "/" + createDate.Day)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(cmsImageFolder + createDate.Year + "/" + createDate.Month + "/" + createDate.Day));
            }
            return cmsImageFolder + createDate.Year + "/" + createDate.Month + "/" + createDate.Day + "/" + createDate.Hour + "-" + createDate.Minute + "-" + createDate.Second + "-" + createDate.Millisecond+Path.GetExtension(file.FileName);
        }
        public static string GetCmsPageUrl(UrlHelper url, int Id, string title)
        {
            return url.Action("Index", "Home", new {area="cms", Id = Id, title = title.Purify().Replace(" ", "-") });
        }
        internal static string GetProductImageUrl(HttpPostedFileBase file, ImageSize size)
        {
            DateTime createDate = DateTime.Now;
            string cmsImageFolder = "~/Images/";
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(cmsImageFolder + createDate.Year)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(cmsImageFolder + createDate.Year));
            }
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(cmsImageFolder + createDate.Year + "/" + createDate.Month)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(cmsImageFolder + createDate.Year + "/" + createDate.Month));
            }
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(cmsImageFolder + createDate.Year + "/" + createDate.Month + "/" + createDate.Day)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(cmsImageFolder + createDate.Year + "/" + createDate.Month + "/" + createDate.Day));
            }
            return cmsImageFolder + createDate.Year + "/" + createDate.Month + "/" +
                createDate.Day + "/" + createDate.Hour + "-" + createDate.Minute + "-" + createDate.Second +
                "-" + createDate.Millisecond + "-" + (int)size + Path.GetExtension(file.FileName);
        }
        internal static void DeleteImage(string imageUrl)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                if (File.Exists(HttpContext.Current.Server.MapPath(imageUrl)))
                {
                    File.Delete(HttpContext.Current.Server.MapPath(imageUrl));
                }
            }
        }
        public static string GetProductImageUrl(string imageUrl, ImageSize size)
        {
            return imageUrl.Substring(0, imageUrl.LastIndexOf("-")) + "-" + (int)size + Path.GetExtension(imageUrl);
        }
        public static string GetImageSizeName(Int16 imageTypeID)
        {
            switch (imageTypeID)
            {
                case 1:
                    return "بزرگ";
                case 2:
                    return "متوسط";
                case 3:
                    return "کوچک";
            }
            return string.Empty;
        }

        internal static string GetBarCodeUrl()
        {
            DateTime createDate = DateTime.Now;
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(BarCodeFolder + createDate.Year)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(BarCodeFolder + createDate.Year));
            }
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(BarCodeFolder + createDate.Year + "/" + createDate.Month)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(BarCodeFolder + createDate.Year + "/" + createDate.Month));
            }
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(BarCodeFolder + createDate.Year + "/" + createDate.Month + "/" + createDate.Day)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(BarCodeFolder + createDate.Year + "/" + createDate.Month + "/" + createDate.Day));
            }
            return BarCodeFolder + createDate.Year + "/" + createDate.Month + "/" + createDate.Day + "/" + createDate.Hour + "-" + createDate.Minute + "-" + createDate.Second + "-" + createDate.Millisecond + ".jpg";
        }


        //internal static void SetImageList(List<ProductTypeViewModel> productList)
        //{
        //    if (productList != null && productList.Count() > 0)
        //    {
        //        IEnumerable<int> specialProductId = productList.Where(m => m.Product.TypeAd > 0).Select(m => m.Product.Id);
        //        if (specialProductId.Count() > 0)
        //        {
        //            IEnumerable<TblProductImage> specialProductImageList = TblProductImageDA.GetProductImage(specialProductId);
        //            foreach (ProductTypeViewModel product in productList)
        //            {
        //                product.ImageList = specialProductImageList.Where(i => i.ProductID == product.Product.Id).Select(a => new ProductImage() { TblProductImage = a, HasS3Image = (product.Product.HasS3Image.HasValue && product.Product.HasS3Image.Value) });
        //            }
        //        }
        //    }
        //}


        internal static string ProductPageUrl(long Id, string title)
        {
            return Host + "/products/" + Id + "_" + title.Purify().Replace(" ", "-");
        }
        public static string GetCategoryUrl(long cat)
        {
            return "~/productview/list/#/show/"+cat;
        }
        public static string GetOrderStatus(OrderStatus status)
        {
            var result = "تایید نشده";
            switch (status)
            {
                case OrderStatus.Canceled:
                    result =  "تایید نشده";
                    break;
                case OrderStatus.Confirmed:
                    result = "تایید شده";
                    break;
                case OrderStatus.Shipped:
                    result = "ارسال شده";
                    break;
                case OrderStatus.Recieved:
                    result = "دریافت شده";
                    break;
            }
            return result;
        }
        public static class Payment
        {
            public static string PgwSite
            {
                get
                {
                    return ConfigurationManager.AppSettings["PgwSite"].ToString();
                }
            }
            public static string TerminalId
            {
                get
                {
                    return ConfigurationManager.AppSettings["TerminalId"].ToString();
                }
            }
            public static string UserName
            {
                get
                {
                    return ConfigurationManager.AppSettings["UserName"].ToString();
                }
            }
            public static string UserPassword
            {
                get
                {
                    return ConfigurationManager.AppSettings["UserPassword"].ToString();
                }
            }
            public static string CallBackUrl
            {
                get
                {
                    return ConfigurationManager.AppSettings["CallBackUrl"].ToString();
                }
            }
        }
    }
}

