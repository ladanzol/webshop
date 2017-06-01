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
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
namespace Alb.Omdehsara.DataAccess
{
    public class TblCategoryDA : AlbDataAccessBase
    {
        private static object _lock = new object();
        public static IEnumerable<TblCategory> GetCategoryList(string parentCategory = null)
        {
            lock (_lock)
            {
                if (CategoryList == null)
                {
                    CategoryList = GetConnection().Query<TblCategory>("select * from TblCategory");
                }
            }
            if (parentCategory != null)
            {
                return CategoryList.Where(c => c.ID > 100 && c.ID.ToString().StartsWith(parentCategory));
            }
            else
            {
                return CategoryList;
            }
        }
        static IEnumerable<TblCategory> CategoryList { get; set; }

        public static TblCategory GetCategory(int cat)
        {
            return GetConnection().Query<TblCategory>("select * from Category where categoryID=" + cat,null, commandType: CommandType.Text).FirstOrDefault();
        }
        public static IEnumerable<TblCategory> GetCategories(Int64? ParentCategoryId)
        {
            return GetConnection().Query<TblCategory>("usp_GetCategory", new { ParentCategoryId = ParentCategoryId }, commandType: CommandType.StoredProcedure);
        }
        public static IEnumerable<TblCategory> GetAllCategories()
        {
            return GetConnection().Query<TblCategory>("select * from TblCategory");
        }
        public static IEnumerable<TblCategory> GetCategories(int pageIndex , int pageSize ,Int64? ParentCategoryId, out int TotalRecords)
        {
            return GetMultiple<TblCategory>("usp_GetCategory", new { pageIndex = pageIndex, pageSize = pageSize, ParentCategoryId = ParentCategoryId }, out TotalRecords);
        }
        public static void InsertCategory(TblCategory cat)
        {
            GetConnection().Execute("usp_Insert_Category", cat, commandType: CommandType.StoredProcedure);
            Reload();
        }

        private static void Reload()
        {
            lock (_lock)
            {
                CategoryList = null;
            }
        }
        public static void UpdateCategory(TblCategory cat)
        {
            GetConnection().Execute("usp_Update_Category", cat, commandType: CommandType.StoredProcedure);
            Reload();
        }
        public static void DeleteCategory(TblCategory cat)
        {
            GetConnection().Execute("usp_Delete_Category", new { ID = cat.ID }, commandType: CommandType.StoredProcedure);
            Reload();
        }
    }
}
