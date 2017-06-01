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
using Alb.Omdehsara.DataAccess;
using Alb.Omdehsara.UI.MVC.Areas.Admin.Models;
using Alb.Omdehsara.UI.MVC.Models;
using Alb.Omdehsara.UI.MVC.Api.Models;
using Alb.Omdehsara.DataAccess.Product;
using Alb.Omdehsara.DataAccess.Cms;
namespace Alb.Omdehsara.UI.MVC.Controllers
{
    public class HomeController : OmdehsaraControllerBase
    {
        // GET: /Home/
        public ActionResult Index()
        {
            ViewBag.SliderScript = ConstantDA.ConstantList.Where(c => c.Subject == "MainPageSliderScript").First().Text;
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult MainPageBrand()
        {
            var model = new MainPageBrandsViewModel();
            model.Img1_1_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img1_1_Url").FirstOrDefault().Text);
            model.Url1_1 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url1_1").FirstOrDefault().Text);
            model.Img1_2_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img1_2_Url").FirstOrDefault().Text);
            model.Url1_2 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url1_2").FirstOrDefault().Text);

            model.Img2_1_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img2_1_Url").FirstOrDefault().Text);
            model.Url2_1 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url2_1").FirstOrDefault().Text);
            model.Img2_2_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img2_2_Url").FirstOrDefault().Text);
            model.Url2_2 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url2_2").FirstOrDefault().Text);

            model.Img3_1_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img3_1_Url").FirstOrDefault().Text);
            model.Url3_1 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url3_1").FirstOrDefault().Text);
            model.Img3_2_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img3_2_Url").FirstOrDefault().Text);
            model.Url3_2 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url3_2").FirstOrDefault().Text);

            model.Img4_1_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img4_1_Url").FirstOrDefault().Text);
            model.Url4_1 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url4_1").FirstOrDefault().Text);
            model.Img4_2_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img4_2_Url").FirstOrDefault().Text);
            model.Url4_2 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url4_2").FirstOrDefault().Text);

            model.Img5_1_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img5_1_Url").FirstOrDefault().Text);
            model.Url5_1 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url5_1").FirstOrDefault().Text);
            model.Img5_2_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img5_2_Url").FirstOrDefault().Text);
            model.Url5_2 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url5_2").FirstOrDefault().Text);

            model.Img6_1_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img6_1_Url").FirstOrDefault().Text);
            model.Url5_1 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url5_1").FirstOrDefault().Text);
            model.Img6_2_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img6_2_Url").FirstOrDefault().Text);
            model.Url6_2 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url6_2").FirstOrDefault().Text);

            model.Img7_1_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img7_1_Url").FirstOrDefault().Text);
            model.Url7_1 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url7_1").FirstOrDefault().Text);
            model.Img7_2_Url = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Img7_2_Url").FirstOrDefault().Text);
            model.Url7_2 = Url.Content(ConstantDA.ConstantList.Where(c => c.Subject == "Url7_2").FirstOrDefault().Text);
            return PartialView("_MainPageBrand", model);
        }
        public ActionResult MainPageNews()
        {
            IEnumerable<TblPage> pages = PageDA.GetPageList(4, (int)PageType.News);
            return PartialView("_MainPageNews", pages);
        }
        public ActionResult MainPageSlider()
        {
            ViewBag.SliderHtml = ConstantDA.ConstantList.Where(c => c.Subject == "MainPageSliderHtml").First().Text;
            return PartialView("_MainPageSlider");
        }
        public ActionResult HotProducts()
        {
            List<ProductTypeViewModel> productList = ProductDA.GetHotProducts().Select(a => new ProductTypeViewModel() { Product = a }).ToList<ProductTypeViewModel>();
            foreach (ProductTypeViewModel product in productList)
            {
                if (!string.IsNullOrEmpty(product.Product.MainImageUrl))
                {
                    product.Product.MainImageUrl = Url.Content(product.Product.MainImageUrl);
                }
                product.Product.ProductUrl = Helper.ProductPageUrl(product.Product.ID, product.Product.ProductTitle);
                product.Product.Price = product.Product.Price;
                product.Product.PreviousPrice = product.Product.PreviousPrice;
            }
            return PartialView("_HotProducts", productList);
        }
    }
}
