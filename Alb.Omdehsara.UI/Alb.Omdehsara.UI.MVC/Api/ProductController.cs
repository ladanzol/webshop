using Alb.Omdehsara.Common;
using Alb.Omdehsara.DataAccess;
using Alb.Omdehsara.UI.MVC.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Alb.Omdehsara.UI.MVC.Api;
using Alb.Tools.UI.MVC.Security;
using Alb.Omdehsara.UI.MVC.Controllers;
using Alb.Tools.Common;
using Alb.Tools.Utility;
using Alb.Omdehsara.Common.Product;
using Alb.Omdehsara.UI.MVC.Models;
using Alb.Omdehsara.DataAccess.Product;
using System.Web;

namespace Alb.Omdehsara.UI.MVC.Api
{
    public class ProductController : OmdehsaraApiControllerBase
    {
        [HttpPost]
        public IEnumerable<ProductTypeViewModel> Search(ProductSearchProperty model, bool tabular = false)
        {
            int totalRecords;
            model.PageSize = OmdehsaraConstant.ProductPageSize;
            if (model.ToPrice >= 1000000)
            {
                model.ToPrice = null;
            }
            if (model.FromPrice == 0)
            {
                model.FromPrice = null;
            }
            List<ProductTypeViewModel> productList = ProductDA.SearchProductProperty(model, out totalRecords).Select(a => new ProductTypeViewModel() { Product = a }).ToList<ProductTypeViewModel>();
            foreach (ProductTypeViewModel product in productList)
            {
                if (!string.IsNullOrEmpty(product.Product.MainImageUrl))
                {
                    product.Product.MainImageUrl = Url.Content(product.Product.MainImageUrl);
                }
                product.Product.ProductUrl = Helper.ProductPageUrl(product.Product.ID, product.Product.ProductTitle);
            }
            model.TotalRecords = totalRecords;
            return productList;
        }

        // GET api/province
        [HttpGet]
        public CategoryViewModel GetCategory(Int64? parentId = null)
        {
            CategoryViewModel cvm = new CategoryViewModel();
            cvm.Categories = TblCategoryDA.GetCategories(parentId);
            cvm.CategoryProperties = GetCategoryProperty(parentId);
            return cvm;
        }

        [HttpPost]
        public IHttpActionResult SetPriceForProduct(object ID)
        {
            ProductDA.SetPriceForProduct(Convert.ToInt64(ID));
            return Ok();
        }
        [HttpGet]
        public IEnumerable<TblCategory> Category(Int64? parentId = null)
        {
            return TblCategoryDA.GetCategories(parentId);
        }
        [HttpGet]
        public IEnumerable<TblCategory> GetAllCategories()
        {
            return TblCategoryDA.GetAllCategories();
        }
        IEnumerable<CategoryPropertyViewModel> GetCategoryProperty(Int64? Id)
        {
            IEnumerable<CategoryPropertyViewModel> cpvm = CategoryPropertyDA.GetCategoryProperties(Id).Select(cp => AutoMapper.Mapper.Map<TblCategoryProperty, CategoryPropertyViewModel>(cp)).ToList();
            foreach (CategoryPropertyViewModel cp in cpvm)
            {
                cp.ConstantList = ConstantDA.ConstantList.Where(c => c.Subject == cp.ListSubject).ToList();
            }
            return cpvm;
        }
        public ProductTypeViewModel GetProductType(int Id)
        {
            ProductTypeViewModel model = new ProductTypeViewModel();
            model.Product = ProductDA.GetProductType(Id);
            model.Product.PriceHour = (model.Product.PriceDate.HasValue) ? model.Product.PriceDate.Value.Hour : 0;
            model.Product.PriceMinute = (model.Product.PriceDate.HasValue) ? model.Product.PriceDate.Value.Minute : 0;
            // model.Product = AutoMapper.Mapper.Map<TblProductType>(product);
            model.ProductProperties = ProductDA.GetProductProperty(Id).Select(ap => AutoMapper.Mapper.Map<TblProductProperty>(ap));
            IEnumerable<TblProductImage> productImage = TblProductImageDA.GetProductImage(Id);
            model.ProductImages = productImage.Select(i => i.ImageUrl);

            return model;
        }
        [HttpGet]
        public ProductTypeView GetSimpleProductType(int Id)
        {
            return ProductDA.GetProductType(Id);
        }
        [HttpPost]
        public IHttpActionResult Add(ProductTypeViewModel model)
        {
            model.Product.UserID = UserID;
            //change price to toman

            var id = ProductDA.InsertProductType(AutoMapper.Mapper.Map<TblProductType>(model.Product), model.ProductProperties);
            return Ok(id);
        }
        [HttpPost]
        public dynamic Update(ProductTypeViewModel model)
        {
            if (model.Product.PriceDate.HasValue)
            {
                model.Product.PriceDate = model.Product.PriceDate.Value.Date + new TimeSpan(model.Product.PriceHour, model.Product.PriceMinute, 0);
            }
            ProductDA.UpdateProductType(AutoMapper.Mapper.Map<TblProductType>(model.Product), model.ProductProperties);
            return Ok(model.Product.ID);
        }
        [HttpGet]
        public dynamic GetProductList(string product)
        {
            return GetProductOutput(ProductDA.GetProductList(product));
        }


        [HttpGet]
        public dynamic GetProduct(int pageIndex = 1, int pageSize = 15, Int64? categoryID = null, int? provinceId = null)
        {
            return GetProductOutput(ProductDA.GetRecentProductType(pageIndex, pageSize, categoryID, provinceId));
        }

        private dynamic GetProductOutput(IEnumerable<ProductTypeView> list)
        {
            return list;
        }

        [Authorize]
        [HttpPost]
        public dynamic DeleteProductType(int Id, int? uid = null)
        {
            int userId;
            if (User.IsInRole(OmdehsaraRoles.Admin.ToString()) && uid.HasValue)
            {
                userId = uid.Value;
            }
            else
            {
                userId = UserID;
            }
            ProductDA.DeleteProductType(Id);
            DeleteProductImage(Id);
            return new { success = 1 };
        }

        private void DeleteProductImage(object Id)
        {
            int productId = Convert.ToInt32(Id);
            IEnumerable<TblProductImage> productImage = TblProductImageDA.GetProductImage(productId);
            TblProductImageDA.DeleteAllImage(productId);
            foreach (TblProductImage img in productImage)
            {
                Helper.DeleteImage(img.ImageUrl);
            }
        }
        [HttpPost]
        public dynamic DeleteImage(int Id)
        {

                TblProductImage img = TblProductImageDA.GetImage(Convert.ToInt32(Id), UserID);
                if (img != null)
                {
                    TblProductImageDA.DeleteProductImage(Id);
                    // Helper.DeleteProductImage(img);
                    return Ok();
                }
                return BadRequest();
        }
        [HttpGet]
        public IEnumerable<dynamic> GetProductImage(int Id)
        {
            return TblProductImageDA.GetProductImage(Convert.ToInt32(Id)).Select(i => new { i.ImageID, ImageUrl = i.ImageUrl });
        }

        [HttpGet]
        public dynamic GetBrands(int? categoryId)
        {
            return TblBrandDA.GetBrand(categoryId).Select(b => new { b.ID, b.BrandName, ImageUrl = (string.IsNullOrEmpty(b.ImageUrl)) ? null : Url.Content(b.ImageUrl) });
        }

        [HttpGet]
        public dynamic GetUnits()
        {
            return TblUnitDA.GetUnit();
        }

        [HttpPost]
        public IHttpActionResult GetProductTypes(ProductLocationSearchOption searchOption)
        {
            //This function does not return the store info. But SearchProductLocation in StoreController returns store info too.
            if (!searchOption.PageIndex.HasValue)
            {
                searchOption.PageIndex = 1;
            }
            if (!searchOption.PageSize.HasValue)
            {
                searchOption.PageSize = 20;
            }
            int totalRecords;
            return Ok(
                new
                {
                    Data = ProductDA.GetProductTypes(searchOption, out totalRecords),
                    TotalRecords = totalRecords
                });
        }
        [HttpGet]
        public IHttpActionResult GetProducts(long Id, int pageIndex = 1)
        {
            int totalRecords;
            return Ok(
                new
                {
                    Data = ProductDA.GetProducts(Id, pageIndex, 20, out totalRecords),
                    TotalRecords = totalRecords
                }
                );
        }

        [HttpGet]
        public IHttpActionResult GetProductLocations(ProductLocationSearchOption model)
        {
            int totalRecords;
            return Ok(
                new
                {
                    Data = TblProductLocationDA.SearchProductLocation(model, out totalRecords),
                    TotalRecords = totalRecords
                }
                );
        }

        [HttpGet]
        public dynamic GetColors(int? categoryId)
        {
            return TblColorDA.GetColor(categoryId);
        }

        [HttpGet]
        public IHttpActionResult GetSizes(int? categoryId)
        {
            return Ok(TblSizeDA.GetSize(categoryId));
        }

        [HttpGet]
        public IHttpActionResult GetProductTypeAvailableColors(long Id)
        {
            return Ok(ProductDA.GetProductTypeAvailableColors(Id));
        }

        [HttpGet]
        public IHttpActionResult GetProductTypeAvailableSizes(long Id)
        {
            return Ok(ProductDA.GetProductTypeAvailableSizes(Id).Where(s=>s.SizeCount>0));
        }

        [HttpGet]
        public IHttpActionResult GetProductAvailableColors(long Id, int sizeId)
        {
            return Ok(ProductDA.GetProductAvailableColors(Id, sizeId));
        }
        [HttpGet]
        public IHttpActionResult GetProductByColorAndSize(long Id, int sizeId, int colorId)
        {
            return Ok(ProductDA.GetProductByColorAndSize(Id, sizeId, colorId));
        }

        [HttpPost]
        public IHttpActionResult AddProduct(ProductViewModel model)
        {
            model.UserID = UserID;
            model.ID = ProductDA.InsertProduct(AutoMapper.Mapper.Map<TblProduct>(model));
            return Ok(ProductDA.GetProduct(model.ID));
        }

        [HttpPost]
        public IHttpActionResult StoreProduct(StoreProduct model)
        {
            model.UserID = UserID;
            Int64 Id = ProductDA.StoreProuct(model);
            return Ok(Id);
        }
        [HttpPost]
        public IHttpActionResult UpdateProduct(ProductViewModel model)
        {
            ProductDA.UpdateProduct(AutoMapper.Mapper.Map<TblProduct>(model));
            return Ok(ProductDA.GetProduct(model.ID));
        }
        [Authorize]
        [HttpPost]
        public IHttpActionResult DeleteProduct(int Id)
        {
            ProductDA.DeleteProduct(Id);
            return Ok();
        }
                [Authorize]
        [HttpPost]
        public IHttpActionResult DisableProductType(int Id)
        {
            ProductDA.DisableProductType(Id);
            return Ok();
        }
        [Authorize]
        [HttpPost]
        public IHttpActionResult EnableProductType(int Id)
        {
            ProductDA.EnableProductType(Id);
            return Ok();
        }
        
    }
}
