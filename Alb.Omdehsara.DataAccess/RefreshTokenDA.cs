
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Alb.Omdehsara.Common;
using Alb.Tools.DataAccess;

namespace Alb.Omdehsara.DataAccess
{
    public class RefreshTokenDA : AlbDataAccessBase
    {

        public static int AddRefreshToken(TblRefreshToken token)
        {

            return GetConnection().Execute("usp_Insert_TblRefreshToken", token, commandType: CommandType.StoredProcedure);
        }
        public static TblRefreshToken FindRefreshToken(string hashedTokenId)
        {
            return GetConnection().Query<TblRefreshToken>("usp_GetRefreshToken", new { Id = hashedTokenId }, commandType:CommandType.StoredProcedure).FirstOrDefault();
        }
        public static void RemoveRefreshToken(string Id)
        {
            GetConnection().Execute("usp_Delete_TblRefreshToken", new { Id = Id }, commandType: CommandType.StoredProcedure);
        }
    }
}
