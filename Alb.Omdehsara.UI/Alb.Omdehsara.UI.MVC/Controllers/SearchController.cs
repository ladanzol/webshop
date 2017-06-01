using Alb.Omdehsara.DataAccess.Product;
using Alb.Omdehsara.UI.MVC.Api.Models;
using Alb.Omdehsara.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alb.Tools.Utility;
namespace Alb.Omdehsara.UI.MVC.Controllers
{
    public class SearchController : OmdehsaraControllerBase
    {
        public ActionResult Index(int pageIndex = 1, string q = "")
        {
            SearchViewModel model = new SearchViewModel();
            model.Title = q.Purify();
            int totalRecords;
            model.ProductList = ProductDA.SearchProduct(model.Title, pageIndex, 15, out totalRecords).Select(a => new ProductTypeViewModel() { Product = a }).ToList<ProductTypeViewModel>();
            foreach (ProductTypeViewModel product in model.ProductList)
            {
                if (!string.IsNullOrEmpty(product.Product.MainImageUrl))
                {
                    product.Product.MainImageUrl = Url.Content(product.Product.MainImageUrl);
                }
                product.Product.ProductUrl = Helper.ProductPageUrl(product.Product.ID, product.Product.ProductTitle);
            }
            model.TotalRecords = totalRecords;
            return View(model);
        }
    }
}