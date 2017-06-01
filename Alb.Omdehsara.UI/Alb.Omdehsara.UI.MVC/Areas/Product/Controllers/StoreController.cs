using Alb.Omdehsara.Common.Product;
using Alb.Omdehsara.DataAccess.Product;
using Alb.Omdehsara.UI.MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alb.Omdehsara.UI.MVC.Areas.Product.Controllers
{
    public class StoreController : OmdehsaraControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Barcode(long Id)
        {
            ProductView product = ProductDA.GetProduct(Id);
            if (string.IsNullOrEmpty(product.BarCodeUrl))
            {
                product.BarCodeUrl = Helper.GetBarCodeUrl();
                product.BarCodeContent = Id.ToString() + "-" + product.InsDate.ToShortDateString() + "-" + product.SizeID + "-" + product.ColorID;
                Tools.Utility.BarCode.GenerateBarCode(Server.MapPath(product.BarCodeUrl), product.ProductTitle, product.BarCodeContent);
                ProductDA.UpdateProduct(AutoMapper.Mapper.Map<TblProduct>(product));
            }
            return View(product);
        }
    }
}