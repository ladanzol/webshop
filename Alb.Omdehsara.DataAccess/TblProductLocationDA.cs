using Alb.Omdehsara.Common;
using Alb.Tools.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Dapper.Contrib.Extensions;
using Alb.Omdehsara.Common.Product;
using Alb.Tools.Utility;
namespace Alb.Omdehsara.DataAccess
{
    public class TblProductLocationDA : AlbDataAccessBase
    {
        public static IEnumerable<TblProductLocation> GetProductLocatoins(int shelfID)
        {
            return GetConnection().Query<TblProductLocation>("select * from TblProductLocation where shelfID=@shelfID", new { shelfID = shelfID });
        }
        public static void InsertProductLocation(TblProductLocation productLocation)
        {
            GetConnection().Query<int>("usp_Insert_TblProductLocation", productLocation, commandType: CommandType.StoredProcedure);
        }
        public static void UpdateProductLocation(TblProductLocation productLocation)
        {
            GetConnection().Execute("usp_Update_TblProductLocation", productLocation, commandType: CommandType.StoredProcedure);
        }
        public static void DeleteProductLocation(TblProductLocation productLocation)
        {
            GetConnection().Execute("usp_Delete_TblProductLocation", productLocation, commandType: CommandType.StoredProcedure);
        }

        public static IEnumerable<Common.Product.ProductLocation> SearchProductLocation(Common.Product.ProductLocationSearchOption searchOption, out int totalRecords)
        {
            if (!searchOption.PageIndex.HasValue)
            {
                searchOption.PageIndex = 1;
            }
            if (!searchOption.PageSize.HasValue)
            {
                searchOption.PageSize = 15;
            }
            var result = GetMultiple<ProductLocation>("usp_SearchProductLocation", searchOption, out totalRecords);
            foreach (var p in result)
            {
                if (p.InsDate.HasValue)
                {
                    p.InsDatePersian = DateTools.GetPersianDateString(p.InsDate.Value);
                }
            }
            return result;
        }
    }
}
