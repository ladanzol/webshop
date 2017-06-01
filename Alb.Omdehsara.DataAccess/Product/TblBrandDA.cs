using Alb.Omdehsara.Common;
using Alb.Tools.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Alb.Omdehsara.Common.Product;
using System.Transactions;
namespace Alb.Omdehsara.DataAccess
{
    public class TblBrandDA : AlbDataAccessBase
    {
        private static IEnumerable<BrandCategories> _Brands = null;
        public static object _lock = new object();
        public static IEnumerable<BrandCategories> GetBrand(long? categoryId = null, bool fromCache = true)
        {
            lock (_lock)
            {
                if (_Brands == null || !fromCache)
                {
                    _Brands = GetConnection().Query<BrandCategories>("usp_GetBrand", new { }, commandType: CommandType.Text);
                    foreach (BrandCategories brand in _Brands)
                    {
                        brand.Categoryies = GetBrandCategoryies(brand.ID);
                    }
                }
                if (categoryId == null)
                {
                    return _Brands;
                }
                else
                {
                    return _Brands.Where(b => b.Categoryies.Any(c => categoryId.Value.ToString().StartsWith(c.ID.ToString())));
                   // return _brands.Where(b => categoryId.Value.ToString().StartsWith(b.CategoryID.ToString()));
                }
            }
        }

        public static void UpdateBrand(TblBrand tblBrand, IEnumerable<long> categories)
        {
            using (TransactionScope trans = CreateTransactionScope())
            {
                var con = GetConnection();
                con.Execute("usp_update_TblBrand", tblBrand, commandType: CommandType.StoredProcedure);
                if (categories.Count() > 0)
                {
                    con.Execute("delete from TblBrandCategory where BrandID = @BrandID", new { BrandID = tblBrand.ID }, commandType: CommandType.Text);
                }
                foreach (long cat in categories)
                {
                    con.Execute("usp_Insert_TblBrandCategory", new { BrandID = tblBrand.ID, CategoryID = cat }, commandType: CommandType.StoredProcedure);
                }
                trans.Complete();
            }
        }
        public static void AddBrand(TblBrand tblBrand, IEnumerable<long> categories)
        {
            using (TransactionScope trans = CreateTransactionScope())
            {
                var con = GetConnection();
                tblBrand.ID = con.Query<int>("usp_insert_TblBrand", tblBrand, commandType: CommandType.StoredProcedure).First();
                foreach (long cat in categories)
                {
                    con.Execute("usp_Insert_TblBrandCategory", new { BrandID = tblBrand.ID, CategoryID = cat }, commandType: CommandType.StoredProcedure);
                }
                trans.Complete();
            }
        }

        public static void DeleteBrand(int Id)
        {
            GetConnection().Execute("Delete from tblbrand where Id = @Id", new { Id = Id });
            _Brands = null;
        }

        static IEnumerable<TblCategory> GetBrandCategoryies(int brandId)
        {
            return GetConnection().Query<TblCategory>("usp_GetBrandCategoryies", new { brandId = brandId }, commandType: CommandType.StoredProcedure);
        }

        public static void Reload()
        {
            lock (_lock)
            {
                _Brands = null;
            }
        }
    }
}
