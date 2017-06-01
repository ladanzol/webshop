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
using Alb.Omdehsara.Common.Polling;
namespace Alb.Omdehsara.DataAccess.Polling
{
    public class UserAnswerDA : AlbDataAccessBase
    {
        public static IEnumerable<PollingResult> GetPollingResult()
        {
            return GetConnection().Query<PollingResult>("usp_GetPollingResult", new { }, commandType: CommandType.StoredProcedure);
        }
        public static void AddUserAnswer(TblUserAnswer tblUserAnswer)
        {
            tblUserAnswer.ID = GetConnection().Query<int>("usp_insert_TblUserAnswer", tblUserAnswer, commandType: CommandType.StoredProcedure).First();
        }
    }
}
