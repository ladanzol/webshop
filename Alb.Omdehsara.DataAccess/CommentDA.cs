
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
using Alb.Tools.DataAccess;
namespace Alb.Omdehsara.DataAccess
{
    public class UserCommentDA : AlbDataAccessBase
    {
        public static int AddComment(TblUserComment comment)
        {
            return GetConnection().Execute("usp_Insert_TblUserComment", comment, commandType: CommandType.StoredProcedure);
        }
        public static IEnumerable<TblUserComment> GetComments(int pageIndex, int pageSize, out int totalRecords)
        {
            return GetMultiple<TblUserComment>("usp_GetComments", new { pageIndex = pageIndex, pageSize = pageSize }, out totalRecords);
        }
    }
}
