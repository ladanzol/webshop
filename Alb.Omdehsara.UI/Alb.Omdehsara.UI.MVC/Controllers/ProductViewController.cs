using Alb.Common.Common;
using Alb.Common.DataAccess;
using Alb.Tools.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alb.Tools.UI.MVC.Security;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Security;
using Alb.Omdehsara.Common;
using Alb.Omdehsara.UI.MVC.Models;
using Alb.Omdehsara.Common.Product;
using Alb.Omdehsara.DataAccess.Product;
using Alb.Omdehsara.UI.MVC.Api.Models;
namespace Alb.Omdehsara.UI.MVC.Controllers
{
    public class ProductViewController : OmdehsaraControllerBase
    {
        // GET: /Home/
        public ActionResult Index(int Id, string title)
        {
            ProductTypeViewModel model = new ProductTypeViewModel() { Product = ProductDA.GetProductType(Id) };
            if (title.Replace("-", " ") != model.Product.ProductTitle.Purify().ToLower())
            {
                string url = Helper.ProductPageUrl(Id, model.Product.ProductTitle);
                return RedirectPermanent(url);
            }
            else
            {
                if (model.Product.MainImageID.HasValue)
                {
                    model.Product.MainImageUrl = Helper.GetProductImageUrl(model.Product.MainImageUrl, ImageSize.Medium);
                }
                model.AvailableSizes = ProductDA.GetProductTypeAvailableSizes(model.Product.ID).Where(s => s.SizeCount > 0);
                model.AvailableColors = ProductDA.GetProductTypeAvailableColors(model.Product.ID);
                model.ProductImages = ProductDA.GetProductImages(Id, ImageSize.Medium).Select(i=>i.ImageUrl);
                model.Product.Price = model.Product.Price;
                if (!string.IsNullOrEmpty(model.Product.ProductKeywords))
                {
                    ViewBag.keywords = model.Product.ProductKeywords.Replace("\n", ",");
                }
                if (!string.IsNullOrEmpty(model.Product.Description))
                {
                }
                return View(model);
            }
        }
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult List(ProductSearchProperty model, int pageIndex = 1)
        {
            IndexViewModel indexVW = new IndexViewModel();
            //indexVW.SearchTitle = CreateSearchTitle(model);
            //CreateMetatag();
            ////if (tabular)
            ////{
            ////    indexVW.Tabular = true;
            ////    model.PageSize = ItConstant.ProductTabularPageSize;
            ////}
            ////else
            ////{
            ////    model.PageSize = ItConstant.ProductPageSize;
            ////}
            //model.PageIndex = pageIndex;
            //int totalRecords;
            //List<ProductTypeViewModel> productList = ProductDA.SearchProductProperty(model, out totalRecords).Select(a => new ProductTypeViewModel() { Product = a }).ToList<ProductTypeViewModel>();
            //foreach (var p in productList)
            //{
            //    p.Product.ProductUrl = Helper.ProductPageUrl(p.Product.ID, p.Product.ProductTitle);
            //}
            //model.TotalRecords = totalRecords;
            //indexVW.ProductList = productList;
            //indexVW.ProductSearchProperty = model ?? new ProductSearchProperty();
            return View("List", indexVW);
        }

        private string CreateSearchTitle(ProductSearchProperty model)
        {
            return string.Empty;
        }

        private void CreateMetatag()
        {
            if (!Request.Url.ToString().Contains("?"))
            {
                ViewBag.description = "";
                ViewBag.keywords = "";
            }
        }
    }
}
