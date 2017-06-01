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
    public class OptionDA : AlbDataAccessBase
    {
        public static IEnumerable<TblOption> GetOptions(int? questionId = null)
        {
            if (questionId.HasValue)
            {
                return GetConnection().Query<TblOption>("Select * from pol.TblOption where questionId=@questionId", new { questionId = questionId }, commandType: CommandType.Text);
            }
            else
            {
                return GetConnection().Query<TblOption>("Select * from pol.TblOption", new { }, commandType: CommandType.Text);
            }
        }

        public static void UpdateOption(TblOption tblOption)
        {
            GetConnection().Execute("usp_update_TblOption", tblOption, commandType: CommandType.StoredProcedure);
        }

        public static void AddOption(TblOption tblOption)
        {
            tblOption.ID = GetConnection().Query<int>("usp_insert_TblOption", tblOption, commandType: CommandType.StoredProcedure).First();
        }

        public static void DeleteOption(int Id)
        {
            GetConnection().Execute("Delete from pol.TblOption where Id = @Id", new { Id = Id });
        }

        public static TblOption GetOption(int? Id)
        {
            return GetConnection().Query<TblOption>("Select * from pol.TblOption where Id = @Id", new { Id = Id }).FirstOrDefault();
        }
    }
}
