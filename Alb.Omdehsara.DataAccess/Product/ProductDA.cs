using Alb.Omdehsara.Common.Product;
using Alb.Tools.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Alb.Omdehsara.Common;
using System.Transactions;
using System.Data;
using Alb.Tools.Utility;
using System.IO;
namespace Alb.Omdehsara.DataAccess.Product
{
    public class ProductDA : AlbDataAccessBase
    {

        public static ProductTypeView GetProductType(long productID)
        {
            return GetConnection().Query<ProductTypeView>("usp_GetProductType", new { ID = productID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }
        public static ProductTypeView GetProductBrief(int productID)
        {
            return GetConnection().Query<ProductTypeView>("usp_GetProductBrief", new { productID = productID }, commandType: CommandType.StoredProcedure).FirstOrDefault();

        }



        public static Int64 InsertProductType(TblProductType productType, IEnumerable<TblProductProperty> productProperties)
        {
            using (TransactionScope trans = CreateTransactionScope())
            {
                productType.Price = productType.Price;
                productType.PreviousPrice = productType.PreviousPrice ;

                productType.InsDate = DateTools.Now();
                productType.ID = GetConnection().Query<Int64>("usp_Insert_TblProductType", productType, commandType: CommandType.StoredProcedure).FirstOrDefault();
                foreach (TblProductProperty adProperty in productProperties.Where(a => a.PropertyValue != null || a.PropertyConstantID != null))
                {
                    GetConnection().Execute("usp_insert_TblProductProperty", new TblProductProperty() { ProductID = productType.ID, PropertyConstantID = adProperty.PropertyConstantID, PropertyID = adProperty.PropertyID, PropertyValue = adProperty.PropertyValue }
                        , commandType: CommandType.StoredProcedure);
                }
                trans.Complete();
                return productType.ID;
            }
        }


        public static void UpdateProductType(TblProductType productType, IEnumerable<TblProductProperty> productProperties = null)
        {
            using (TransactionScope trans = CreateTransactionScope())
            {
                productType.Price = productType.Price ;
                productType.PreviousPrice = productType.PreviousPrice;

                GetConnection().Query<int>("usp_Update_ProductType", productType, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (productProperties != null)
                {

                    GetConnection().Execute("usp_delete_TblProductProperty", new { ProductID = productType.ID }
                       , commandType: CommandType.StoredProcedure);
                    foreach (TblProductProperty adProperty in productProperties.Where(a => a.PropertyValue != null || a.PropertyConstantID != null))
                    {
                        GetConnection().Execute("usp_insert_TblProductProperty", new TblProductProperty() { ProductID = productType.ID, PropertyConstantID = adProperty.PropertyConstantID, PropertyID = adProperty.PropertyID, PropertyValue = adProperty.PropertyValue }
                            , commandType: CommandType.StoredProcedure);
                    }
                }
                trans.Complete();
            }
        }


        public static IEnumerable<ProductTypeView> GetRecentProductType(int pageIndex, int pageSize, Int64? categoryID, int? provinceId = null)
        {
            return GetConnection().Query<ProductTypeView>("usp_GetRecentProduct", new { pageIndex = pageIndex, pageSize = pageSize, categoryID = categoryID, provinceId = provinceId }, commandType: CommandType.StoredProcedure);
        }



        public static IEnumerable<ProductTypeView> SearchProduct(string q, int PageIndex , int PageSize, out int totalRecords)
        {
            string query = QueryTools.GetQuery(q);
            return GetMultiple<ProductTypeView>("usp_searchProduct", new { query = query, PageIndex = PageIndex, PageSize = PageSize }, out totalRecords);
        }

        public static IEnumerable<ProductTypeView> GetProductList(string product)
        {
            IEnumerable<int> productId = product.Split(',').Select(a => int.Parse(a));
            return GetConnection().Query<ProductTypeView>("usp_getProductList", new { product = product }, commandType: CommandType.StoredProcedure);
        }
        public static IEnumerable<ProductTypeView> SearchProductProperty(ProductSearchProperty productProp, out int totalRecords)
        {
            string productTypeColumns = "TblProductType.ID, CategoryID, Description, PreviousPrice PreviousPrice, Price Price, UserID, VisitCount, Freight, Enabled, ProductTitle, MainImageID, HasGallery, ProductKeywords, BrandID, [Count], CountryID, PriceDate, Country, ShowInFirstPage,MinOrderQuantity,UnitID, TblUnit.UnitName,TblUnit.UnitQuantity ";
            string columnList = @" ROW_NUMBER() over(order by TblProductType.ID desc) RowNumber, " + productTypeColumns + ", TblProductImage.ImageUrl MainImageUrl,TblBrand.BrandName";
            string sql = @" with Result as(  select " + columnList;
            string from = @" from TblProductType ";

            if (productProp.CategoryID.HasValue)
            {
                int i = 0;

                foreach (PropertySearch prop in productProp.ProductProperties)
                {
                    i++;
                    if (prop.SelectedConstants.Count() > 0 || !string.IsNullOrEmpty(prop.PropertyValue))
                    {
                        if (prop.SelectedConstants.Count() == 0 && !string.IsNullOrEmpty(prop.PropertyValue))
                        {
                            if (prop.PropertyType == PropertyTypes.TextBox.ToString())
                            {
                                from += " inner join TblProductProperty productProp" + i.ToString() + " on productProp" + i.ToString() + ".PropertyID = " + prop.PropertyID;
                                from += " and productProp" + i.ToString() + ".ProductId= TblProductType.Id and productProp" + i.ToString() + ".PropertyValue like '%" + prop.PropertyValue.ToSafeString() + "%'";
                            }
                            else
                                if (prop.PropertyType == PropertyTypes.CheckBox.ToString() && prop.PropertyValue != "0")
                                {
                                    from += " inner join TblProductProperty productProp" + i.ToString() + " on productProp" + i.ToString() + ".PropertyID = " + prop.PropertyID;
                                    from += " and productProp" + i.ToString() + ".ProductId= TblProductType.Id and productProp" + i.ToString() + ".PropertyValue = '" + prop.PropertyValue.ToSafeString() + "'";
                                }
                        }
                        if (prop.SelectedConstants.Count() == 1)
                        {
                            from += " inner join TblProductProperty productProp" + i.ToString() + " on productProp" + i.ToString() + ".PropertyID = " + prop.PropertyID;
                            from += " and productProp" + i.ToString() + ".ProductId= TblProductType.Id and productProp" + i.ToString() + ".PropertyConstantID = " + prop.SelectedConstants.First().ID;
                        }
                        else if (prop.SelectedConstants.Count() > 1)
                        {
                            from += " inner join TblProductProperty productProp" + i.ToString() + " on productProp" + i.ToString() + ".PropertyID = " + prop.PropertyID;
                            string propId = string.Empty;
                            propId = string.Join(",", prop.SelectedConstants.Select(c => c.ID));
                            from += " and  productProp" + i.ToString() + ".ProductId= TblProductType.Id and productProp" + i.ToString() + ".PropertyConstantID  in (" + propId + ")";
                        }
                    }
                }
            }
            from += " inner join TblBrand on TblBrand.ID = TblProductType.BrandID";
            from += " inner join TblUnit on TblUnit.ID = TblProductType.UnitID";
            from += " left join TblProductImage on TblProductImage.ProductID = TblProductType.ID and TblProductType.MainImageID = TblProductImage.ImageID";

            string where = " where  TblProductType.Enabled=1 ";

            if (productProp.ProvinceId.HasValue)
            {
                where += " and State=" + productProp.ProvinceId;
            }
            if (productProp.CategoryID.HasValue)
            {
                where += " and CHARINDEX('" + productProp.CategoryID + "', convert(varchar(30), TblProductType.CategoryID))=1";
            }
            if (productProp.ProvinceId.HasValue)
            {
                where += " and TblProductType.City=" + productProp.City;
            }
            if (productProp.FromPrice > 0)
            {
                where += " and TblProductType.Price >=" + productProp.FromPrice.Value;
            }
            if (productProp.ToPrice.HasValue)
            {
                where += " and TblProductType.Price <=" + productProp.ToPrice.Value;
            }
            if (productProp.SelectedBrands!= null && productProp.SelectedBrands.Length > 0)
            {
                where += " and TblProductType.BrandID in (" + string.Join(",", productProp.SelectedBrands) + ")";
            }
            if (productProp.SelectedColors != null && productProp.SelectedColors.Length > 0)
            {
                where += " and Exists(select 1 from TblProduct where TblProductType.ID = TblProduct.ProductTypeID and TblProduct.ColorID in (" + string.Join(",", productProp.SelectedColors) + "))";
            }
            if (productProp.SelectedSizes != null && productProp.SelectedSizes.Length > 0)
            {
                where += " and Exists(select 1 from TblProduct where TblProductType.ID = TblProduct.ProductTypeID and  TblProduct.SizeID in (" + string.Join(",", productProp.SelectedSizes) + "))";
            }
            sql += from + where + ")";
            sql += " Select * from Result where RowNumber between " + ((productProp.PageIndex - 1) * productProp.PageSize + 1) + " and " + productProp.PageIndex * productProp.PageSize;
            sql += " select COUNT(*) " + from;
            IEnumerable<ProductTypeView> result = null;
            using (var multi = GetConnection().QueryMultiple(sql))
            {
                result = multi.Read<ProductTypeView>().ToList();
                totalRecords = multi.Read<int>().Single();
            }
            return result;
        }

        public static IEnumerable<ProductPropertyList> GetProductProperty(int Id)
        {
            return GetConnection().Query<ProductPropertyList>("usp_GetProductProperty", new { ProductID = Id }, commandType: CommandType.StoredProcedure);
        }

        public static IEnumerable<ProductPropertyList> GetProperties(IEnumerable<int> product)
        {
            if (product.Count() > 0)
            {
                string str = string.Join(",", product);
                return GetConnection().Query<ProductPropertyList>(@"select ap.ProductID, ap.PropertyConstantID,PropertyID,isnull(ap.PropertyValue, c.Text)PropertyValue
,cp.PropertyName
  from TblProductProperty 
ap left join TblConstant c on ap.PropertyConstantID = c.ID
inner join TblCategoryProperty cp on cp.ID = ap.PropertyID where ProductID in (" + str + ")");
            }
            else return null;
        }

        public static void DeleteProductType(int Id)
        {
        }


        public static IEnumerable<TblProductImage> GetProductImages(long productId, ImageSize? imageSize = null)
        {
            return GetConnection().Query<TblProductImage>("usp_GetProductImage", new { ProductID = productId ,imageSize=(short?)imageSize}, commandType: CommandType.StoredProcedure);
        }

        public static void DeleteImage(long ID)
        {
            GetConnection().Execute("usp_delete_TblProductImage", new { ImageID = ID }, commandType: CommandType.StoredProcedure);
        }

        public static Int64 AddImage(TblProductImage image)
        {
            return GetConnection().Query<Int64>("usp_insert_TblProductImage", image, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public static TblProductImage GetImage(int ID)
        {
            return GetConnection().Query<TblProductImage>("select * from tblProductImage where ImageID = @ID", new { ID = ID }, commandType: CommandType.Text).FirstOrDefault();
        }

        public static IEnumerable<ProductView> GetProducts(long productTypeID, int pageIndex, int pageSize, out int totalRecords)
        {
            return GetMultiple<ProductView>("usp_GetProducts", new { productTypeID = productTypeID, pageIndex = pageIndex, pageSize = pageSize },out totalRecords);
        }

        public static IEnumerable<ProductTypeView> GetProductTypes(int pageIndex, int pageSize, out int totalRecords)
        {
            return GetMultiple<ProductTypeView>("usp_GetProductTypes", new { pageIndex = pageIndex, pageSize = pageSize }, out totalRecords);
        }

        public static IEnumerable<ProductTypeView> GetProductTypes(ProductLocationSearchOption searchOption, out int totalRecords)
        {
            return GetMultiple<ProductTypeView>("usp_GetProductTypes", searchOption, out totalRecords);
        }

        public static Int64 InsertProduct(TblProduct product)
        {

            product.InsDate = DateTools.Now();
            product.ID = GetConnection().Query<Int64>("usp_Insert_TblProduct", product, commandType: CommandType.StoredProcedure).FirstOrDefault();

            return product.ID;

        }


        public static void UpdateProduct(TblProduct productType)
        {
            using (TransactionScope trans = CreateTransactionScope())
            {
                GetConnection().Execute("usp_Update_TblProduct", productType, commandType: CommandType.StoredProcedure);
                trans.Complete();
            }
        }

        public static ProductView GetProduct(long ID)
        {
            return GetConnection().Query<ProductView>("usp_GetProduct", new { ID = ID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public static void DeleteProduct(int Id)
        {
            GetConnection().Execute("usp_TblProduct_Delete", new { ID = Id }, commandType: CommandType.StoredProcedure);
        }

        public static IEnumerable<TblColor> GetProductTypeAvailableColors(long Id)
        {
            return GetConnection().Query<TblColor>("usp_GetProductTypeAvailableColors", new { Id = Id }, commandType: CommandType.StoredProcedure);
        }

        public static IEnumerable<SizeProduct> GetProductTypeAvailableSizes(long Id)
        {
            return GetConnection().Query<SizeProduct>("usp_GetProductAvailableSizes", new { Id = Id }, commandType: CommandType.StoredProcedure);
        }

        public static IEnumerable<TblColor> GetProductAvailableColors(long Id, int sizeId)
        {
            return GetConnection().Query<TblColor>("usp_GetProductAvailableColors", new { Id = Id, sizeId = sizeId }, commandType: CommandType.StoredProcedure);
        }
        public static ProductView GetProductByColorAndSize(long ProductTypeID, int? sizeId, int? colorId)
        {
            return GetConnection().Query<ProductView>("usp_GetProductByColorSize", new { ColorID = colorId, SizeID = sizeId, ProductTypeID = ProductTypeID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public static void SetMainImage(long productTypeID, int imageID)
        {
            string imageUrl = GetImage(imageID).ImageUrl;
            GetConnection().Execute("update tblProductType set MainImageId = @MainImageId where ID = @ID", new { MainImageId = imageID, ID = productTypeID }, commandType: CommandType.Text);
            //string mainImageUrl = imageUrl.Substring(0, imageUrl.LastIndexOf("-")) + "-" + (int)ImageSize.Small + Path.GetExtension(imageUrl);
            //GetConnection().Execute("usp_UpdateMainImage", new {productTypeID =productTypeID, ImageUrl = mainImageUrl }, commandType: CommandType.StoredProcedure);
        }

        public static TblBrand GetBrand(long productTypeID)
        {
            return GetConnection().Query<TblBrand>("select * from TblBrand where ID = (Select BrandID from TblProductType where ID=@ID)", new { ID = productTypeID }).FirstOrDefault();
        }

        public static void SetPriceForProduct(long productTypeID)
        {
            GetConnection().Execute("usp_setPriceForProduct", new { ID = productTypeID }, commandType: CommandType.StoredProcedure);
        }

        public static IEnumerable<ProductTypeView> GetHotProducts()
        {
            return GetConnection().Query<ProductTypeView>("usp_getHotProudcts", new { }, commandType:CommandType.StoredProcedure);
        }

        public static void DisableProductType(int productTypeID)
        {
            GetConnection().Execute("Update TblProductType set Enabled = 0 where ID=@ID", new { ID = productTypeID });
        }

        public static void EnableProductType(int productTypeID)
        {
            GetConnection().Execute("Update TblProductType set Enabled = 1 where ID=@ID", new { ID = productTypeID });
        }

        internal static void UpdateProductQuantity(long productID, int quantity)
        {
            GetConnection().Execute("Update TblProduct set Quantity = @Quantity where ID=@ID", new { ID = productID, Quantity = quantity });
        }

        public static long StoreProuct(StoreProduct model)
        {
            using (TransactionScope trans = CreateTransactionScope())
            {
                if (model.InOut == 2)
                {
                    //product go outs of store
                     model.Quantity = -1 * model.Quantity;
                }

                model.InsDate = DateTools.Now();
                TblProduct postedProduct = AutoMapper.Mapper.Map<TblProduct>(model);
                TblProduct p = GetProduct(model.ProductTypeID, model.ColorID, model.SizeID);
                if (p == null)
                {
                    postedProduct.Enabled = 1;
                    model.ProductID = InsertProduct(postedProduct);
                    TblProductLocationDA.InsertProductLocation(AutoMapper.Mapper.Map<TblProductLocation>(model));
                }
                else
                {
                    model.ProductID = p.ID;
                    p.Quantity += model.Quantity;
                    UpdateProduct(p);
                    TblProductLocation productLocation = AutoMapper.Mapper.Map<TblProductLocation>(model);
                    productLocation.ProductID = p.ID;
                    TblProductLocationDA.InsertProductLocation(productLocation);
                }
                trans.Complete();
                return model.ProductID;
            }
        }

        private static TblProduct GetProduct(long? productTypeID, int? colorId, int? sizeId)
        {
            return GetConnection().Query<TblProduct>("usp_getProductByColorAndSize", new { ProductTypeID = productTypeID, ColorID = colorId, SizeID = sizeId }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }


    }
}
