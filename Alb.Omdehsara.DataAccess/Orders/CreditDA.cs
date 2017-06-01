using Alb.Tools.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Alb.Omdehsara.Common;
using System.Data;
using Alb.Omdehsara.Common.Orders;
using AutoMapper.Mappers;
using AutoMapper;
using Alb.Tools.Common;
using Alb.Omdehsara.Common.Product;
using Alb.Omdehsara.DataAccess.Product;
using Alb.Tools.Utility;
using Alb.Common.DataAccess;
namespace Alb.Omdehsara.DataAccess.Orders
{
    public class CreditDA : AlbDataAccessBase
    {
        public static int GetUserCreditAcount(int UserID)
        {
            return GetConnection().Query<int>("usp_GetUserCreditAcount", new { UserID = UserID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }
        public static IEnumerable<CreditView> GetCredits(int userId, int pageIndex, int pageSize, out int totalRecords)
        {
            return GetMultiple<CreditView>("usp_GetCredits", new { UserID = userId, pageIndex = pageIndex, pageSize = pageSize }, out totalRecords);
        }
        public static IEnumerable<CreditView> GetCredits(int pageIndex, string userName, int pageSize, out int totalRecords)
        {
            return GetMultiple<CreditView>("usp_GetCredits", new { pageIndex = pageIndex, pageSize = pageSize, userName = userName }, out totalRecords);
        }

        public static void ChangeStatus(int Id)
        {
            TblCredit credit = GetCredit(Id);
            credit.Enabled = !credit.Enabled;
            UpdateCredit(credit);
        }

        public static TblCredit GetCredit(int Id)
        {
            return GetConnection().Query<TblCredit>("select * from tblCredit where ID=" + Id).FirstOrDefault();
        }

        public static void UpdateCredit(TblCredit credit)
        {
            GetConnection().Execute("usp_Update_TblCredit", credit, commandType: CommandType.StoredProcedure);
        }
        public static int AddCredit(TblCredit credit)
        {
            credit.CurrentAmount = credit.CurrentAmount * 10;
            var result = GetConnection().Query<dynamic>("usp_Insert_TblCredit", credit, commandType: CommandType.StoredProcedure);
            return Convert.ToInt32( result.FirstOrDefault().ID);
        }

        public static void Delete(int Id)
        {
            GetConnection().Execute("usp_Delete_TblCredit", new {Id=Id }, commandType: CommandType.StoredProcedure);
        }
    }
}
