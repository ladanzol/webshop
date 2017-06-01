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
    public class QuestionDA : AlbDataAccessBase
    {
        public static IEnumerable<TblQuestion> GetQuestions()
        {
            return GetConnection().Query<TblQuestion>("Select * from pol.TblQuestion", new { }, commandType: CommandType.Text);
        }

        public static void UpdateQuestion(TblQuestion tblQuestion)
        {
            GetConnection().Execute("usp_update_TblQuestion", tblQuestion, commandType: CommandType.StoredProcedure);
        }

        public static void AddQuestion(TblQuestion tblQuestion)
        {
            tblQuestion.ID = GetConnection().Query<int>("usp_insert_TblQuestion", tblQuestion, commandType: CommandType.StoredProcedure).First();
        }

        public static void DeleteQuestion(int Id)
        {
            GetConnection().Execute("Delete from pol.TblQuestion where Id = @Id", new { Id = Id });
        }

        public static TblQuestion GetQuestion(int? Id)
        {
            return GetConnection().Query<TblQuestion>("select * from pol.TblQuestion where Id = @Id", new { Id = Id }).FirstOrDefault();
        }
    }
}
