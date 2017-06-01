using Alb.Omdehsara.Common;
using Alb.Omdehsara.Common.Product;
using Alb.Omdehsara.DataAccess;
using Alb.Omdehsara.DataAccess.Product;
using Alb.Omdehsara.UI.MVC.Areas.Product.Models;
using Alb.Omdehsara.UI.MVC.Controllers;
using Alb.Tools.UI.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Alb.Omdehsara.UI.MVC.Areas.Product.Controllers
{
    public class ImageController : OmdehsaraControllerBase
    {
        [HttpGet]
        public ActionResult Index(long productId)
        {
            UploadProductImageViewModel model = new UploadProductImageViewModel();
            if (productId > 0)
            {
                model.Images = ProductDA.GetProductImages(productId);
                model.ProductTypeID = productId;
                model.CategoryID = ProductDA.GetProductType(productId).CategoryID;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(UploadProductImageViewModel model)
        {
            TblProductImage image = new TblProductImage();
            string sourceImageUrl = Helper.GetProductImageUrl(model.Image, (ImageSize)model.ImageSize);
            image.ImageUrl = sourceImageUrl;
            image.CreateDate = DateTime.Now;
            if ((ImageSize)model.ImageSize == ImageSize.Small)
            {
                Tools.Utility.ImageTools.SaveImage(model.Image.InputStream, Server.MapPath(sourceImageUrl), Tools.Utility.ImageTools.Side.Height, OmdehsaraConstant.ProductSmallImageHeight);
            }
            else
            {
                model.Image.SaveAs(Server.MapPath(image.ImageUrl));
            }
            image.ProductID = model.ProductTypeID;
            image.Title = model.Title;
            image.ImageDesc = model.Description;
            image.ImageSize = model.ImageSize;
            image.ImageID = ProductDA.AddImage(image);


            //This line was uncommented when the small image had been created automatically
            //if (model.ImageSize == (int)ImageSize.Medium)
            //{
            //    image.ImageUrl = Helper.GetProductImageUrl(sourceImageUrl, ImageSize.Small);
            //    TblBrand brand = ProductDA.GetBrand(model.ProductTypeID);
            //    Tools.Utility.ImageTools.SaveImage(model.Image.InputStream, Server.MapPath(image.ImageUrl), Tools.Utility.ImageTools.Side.Width, Server.MapPath(brand.ImageUrl), 95);
            //    image.ImageSize = (short)ImageSize.Small;
            //    ProductDA.AddImage(image);
            //}

            ShowMessage("عکس با موفقیت ثبت شد", MessageTypes.Success );
            model.Image = null;
            return RedirectToAction("Index", new { productId = model.ProductTypeID });
        }
        [HttpGet]
        public ActionResult Delete(int ID, long productID)
        {
            try
            {
                TblProductImage img = ProductDA.GetImage(ID);
                if (img.ImageSize == (short)ImageSize.Medium)
                {
                    TblProductImage image = TblProductImageDA.GetImage(Helper.GetProductImageUrl(img.ImageUrl, ImageSize.Small));
                    if (image != null)
                    {
                        ProductDA.DeleteImage(image.ImageID);
                        Helper.DeleteImage(image.ImageUrl);
                    }
                    image = TblProductImageDA.GetImage(Helper.GetProductImageUrl(img.ImageUrl, ImageSize.XSmall));
                    if (image != null)
                    {
                        ProductDA.DeleteImage(image.ImageID);
                        Helper.DeleteImage(image.ImageUrl);
                    }
                }
                ProductDA.DeleteImage(ID);
                Helper.DeleteImage(img.ImageUrl);
                ShowMessage("عکس حذف شد", MessageTypes.Success);
                return RedirectToAction("Index", new { productId = productID });
            }
            catch (Exception ex)
            {
                ShowMessage("لطفا یک عکس را به عنوان عکس اصلی انتخاب نمایید", MessageTypes.Error);
                return RedirectToAction("Index", new { productId = productID });
            }
        }
        [HttpGet]
        public ActionResult MailImage(int ID, long productID)
        {
            ProductDA.SetMainImage(productID, ID);
            ShowMessage("عکس اصلی ثبت شد", MessageTypes.Success);
            return RedirectToAction("Index", new { productId = productID });
        }
    }
}