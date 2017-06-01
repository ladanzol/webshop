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
namespace Alb.Omdehsara.DataAccess
{
    public class TblShelfDA : AlbDataAccessBase
    {
        private static object _lock = new object();
        public static IEnumerable<TblShelf> GetShelfList(string parentShelf = null)
        {
            return GetConnection().Query<TblShelf>("select * from TblShelf");
        }

        public static TblShelf GetShelf(int Id)
        {
            return GetConnection().Query<TblShelf>("select * from TblShelf where ID=" + Id, null, commandType: CommandType.Text).FirstOrDefault();
        }

        public static IEnumerable<TblShelf> GetAllShelfs()
        {
            return GetConnection().Query<TblShelf>("select * from TblShelf");
        }
        public static IEnumerable<TblShelf> GetShelfs(int? storeID, int pageIndex, int pageSize, out int TotalRecords)
        {
            return GetMultiple<TblShelf>("usp_GetShelf", new {storeID =storeID, pageIndex = pageIndex, pageSize = pageSize}, out TotalRecords);
        }
        public static int InsertShelf(TblShelf shelf)
        {
            shelf.ID = GetConnection().Query<int>("usp_Insert_TblShelf", shelf, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return shelf.ID;
        }
        public static void UpdateShelf(TblShelf cat)
        {
            GetConnection().Execute("usp_Update_TblShelf", cat, commandType: CommandType.StoredProcedure);
        }
        public static void DeleteShelf(TblShelf cat)
        {
            GetConnection().Execute("usp_Delete_TblShelf", new { ID = cat.ID }, commandType: CommandType.StoredProcedure);
        }

    }
}
