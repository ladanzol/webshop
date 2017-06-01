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
namespace Alb.Omdehsara.DataAccess
{
    public class TblUnitDA : AlbDataAccessBase
    {
        public static IEnumerable<TblUnit> _Units = null;
        public static object _lock = new object();
        public static IEnumerable<TblUnit> GetUnit(long? categoryId = null, bool fromCache = false)
        {
            lock (_lock)
            {
                if (_Units == null || !fromCache)
                {
                    _Units = GetConnection().Query<TblUnit>("select * from TblUnit", new { }, commandType: CommandType.Text);
                }
                return _Units;
            }
        }

        public static void UpdateUnit(TblUnit tblUnit)
        {
            GetConnection().Execute("usp_update_TblUnit", tblUnit, commandType: CommandType.StoredProcedure);
        }
        public static void AddUnit(TblUnit tblUnit)
        {
            tblUnit.ID = GetConnection().Query<int>("usp_insert_TblUnit", tblUnit, commandType: CommandType.StoredProcedure).First();
        }

        public static void DeleteUnit(int Id)
        {
            GetConnection().Execute("Delete from tblunit where Id = @Id", new { Id = Id });
            _Units = null;
        }
    }
}
