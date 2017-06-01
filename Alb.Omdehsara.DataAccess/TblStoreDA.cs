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
    public class TblStoreDA : AlbDataAccessBase
    {
        private static object _lock = new object();
        public static IEnumerable<TblStore> GetStoreList(string parentStore = null)
        {
            lock (_lock)
            {
                if (StoreList == null)
                {
                    StoreList = GetConnection().Query<TblStore>("select * from TblStore");
                }
            }
            if (parentStore != null)
            {
                return StoreList.Where(c => c.ID > 100 && c.ID.ToString().StartsWith(parentStore));
            }
            else
            {
                return StoreList;
            }
        }
        static IEnumerable<TblStore> StoreList { get; set; }

        public static TblStore GetStore(int Id)
        {
            return GetConnection().Query<TblStore>("select * from Store where ID=" + Id, null, commandType: CommandType.Text).FirstOrDefault();
        }

        public static IEnumerable<TblStore> GetAllStores()
        {
            return GetConnection().Query<TblStore>("select * from TblStore");
        }
        public static IEnumerable<TblStore> GetStores(int pageIndex, int pageSize, out int TotalRecords)
        {
            return GetMultiple<TblStore>("usp_GetStore", new { pageIndex = pageIndex, pageSize = pageSize}, out TotalRecords);
        }
        public static int InsertStore(TblStore store)
        {
            store.ID = GetConnection().Query<int>("usp_Insert_Store", store, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return store.ID;
        }
        public static void UpdateStore(TblStore cat)
        {
            GetConnection().Execute("usp_Update_Store", cat, commandType: CommandType.StoredProcedure);
        }
        public static void DeleteStore(TblStore cat)
        {
            GetConnection().Execute("usp_Delete_Store", new { ID = cat.ID }, commandType: CommandType.StoredProcedure);
        }

    }
}
