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
    public class TblTransportTypeDA : AlbDataAccessBase
    {
        public static IEnumerable<TblTransportType> GetTransportTypes()
        {
            return GetConnection().Query<TblTransportType>("select * from TblTransportType");
        }

        public static TblTransportType GetTransportType(int Id)
        {
            return GetConnection().Query<TblTransportType>("select * from TblTransportType where ID=" + Id, null, commandType: CommandType.Text).FirstOrDefault();
        }


        public static int InsertTblTransportType(TblTransportType transportType)
        {
            transportType.ID = GetConnection().Query<int>("usp_Insert_TblTransportType", transportType, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return transportType.ID;
        }
        public static void UpdateTransportType(TblTransportType transportType)
        {
            GetConnection().Execute("usp_Update_TblTransportType", transportType, commandType: CommandType.StoredProcedure);
        }
        public static void DeleteTransportType(TblTransportType transportType)
        {
            GetConnection().Execute("usp_Delete_TblTransportType", new { ID = transportType.ID }, commandType: CommandType.StoredProcedure);
        }
    }
}
