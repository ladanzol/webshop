using Alb.Tools.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Alb.Omdehsara.Common;
using System.Data;

namespace Alb.Omdehsara.DataAccess
{
    public class CategoryPropertyDA : AlbDataAccessBase
    {
        public static IEnumerable<TblCategoryProperty> GetCategoryProperty(int pageIndex, int pageSize,int? CategoryID,int? ID, out int totalRecords)
        {
            return GetMultiple<TblCategoryProperty>("usp_GetCategoryProperty", new { pageIndex = pageIndex, pageSize = pageSize, ID, CategoryID }, out totalRecords);
        }
        public static int InsertCategoryProperty(TblCategoryProperty catProp)
        {
            return GetConnection().Query<int>("usp_Insert_TblCategoryProperty", catProp, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }
        public static void UpdateCategoryProperty(TblCategoryProperty catProp)
        {
            GetConnection().Execute("usp_Update_TblCategoryProperty", catProp, commandType: CommandType.StoredProcedure);
        }
        public static void DeleteCategoryProperty(TblCategoryProperty catProp)
        {
            GetConnection().Execute("usp_Delete_TblCategoryProperty", new { ID = catProp.ID }, commandType: CommandType.StoredProcedure);
        }

        public static IEnumerable<TblCategoryProperty> GetCategoryProperties(Int64? cat)
        {
            //return GetConnection().Query<TblCategoryProperty>("select * from TblCategoryProperty where charindex(convert(varchar(30), categoryID), '" + cat + "')>0 order by sortNumber");
            return GetConnection().Query<TblCategoryProperty>("select * from TblCategoryProperty where categoryID = @categoryId order by sortNumber", new {categoryId = cat });
        }
    }
}
