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

namespace Alb.Omdehsara.DataAccess
{
    public class ConnectionDA : AlbDataAccessBase
    {

        public static void AddConnection(TblConnection conn)
        {
            GetConnection().Execute("usp_insert_TblConnection", conn, commandType: CommandType.StoredProcedure);
        }
        public static void DeleteConnection(string connectionID)
        {
            GetConnection().Execute("usp_delete_TblConnection", new { connectionID = connectionID }, commandType: CommandType.StoredProcedure);
        }

        public static IEnumerable<TblConnection> GetUserConnection(int userID)
        {
            return GetConnection().Query<TblConnection>("usp_TblConnection_GetUserConnection", new { userID = userID }, commandType: CommandType.StoredProcedure);
        }
        public static IEnumerable<TblConnection> GetUserAdsSearchConnection(int adsID)
        {
            return GetConnection().Query<TblConnection>("usp_GetUserAdsSearchConnection", new { adsID = adsID }, commandType: CommandType.StoredProcedure);
        }
    }
}
