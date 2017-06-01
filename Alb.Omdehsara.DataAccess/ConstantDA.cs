using Alb.Tools.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Alb.Omdehsara.Common;
using System.Data;
namespace Alb.Omdehsara.DataAccess
{
    public class ConstantDA : AlbDataAccessBase
    {
        public static IEnumerable<TblConstant> GetConstant(int pageIndex, int pageSize, string subject, out int totalRecords)
        {
            return GetMultiple<TblConstant>("usp_GetConstant", new { pageIndex = pageIndex, pageSize = pageSize, subject = subject }, out totalRecords);
        }
        public static void InsertConstant(TblConstant constant)
        {
            GetConnection().Execute("usp_Insert_TblConstant", constant, commandType: CommandType.StoredProcedure);
            Reload();
        }
        public static void UpdateConstant(TblConstant constant)
        {
            GetConnection().Execute("usp_Update_TblConstant", constant, commandType: CommandType.StoredProcedure);
            Reload();
        }
        public static void DeleteConstant(TblConstant constant)
        {
            GetConnection().Execute("usp_Delete_TblConstant", new { ID = constant.ID }, commandType: CommandType.StoredProcedure);
            Reload();
        }

        static IEnumerable<TblConstant> _constantList { get; set; }
        static readonly object _lock = new object();
        public static IEnumerable<TblConstant> ConstantList
        {
            get
            {
                lock (_lock)
                {
                    if (_constantList == null)
                    {
                        int totalRecords;
                        _constantList = GetConstant(1, int.MaxValue, null, out totalRecords);
                    }
                    return _constantList;
                }
            }
        }

        public static void Reload()
        {
            lock (_lock)
            {
                _constantList = null;
            }
        }
    }
}
