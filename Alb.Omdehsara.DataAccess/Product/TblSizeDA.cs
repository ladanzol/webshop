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
    public class TblSizeDA : AlbDataAccessBase
    {
        public static IEnumerable<SizeCategories> _Sizes = null;
        public static object _lock = new object();
        public static IEnumerable<SizeCategories> GetSize(long? categoryId = null, bool fromCache = true)
        {
            lock (_lock)
            {
                if (_Sizes == null || !fromCache)
                {
                    _Sizes = GetConnection().Query<SizeCategories>("usp_GetSize", new { }, commandType: CommandType.Text);
                    foreach (SizeCategories size in _Sizes)
                    {
                        size.Categoryies = GetSizeCategoryies(size.ID);
                    }
                }
                if (categoryId == null)
                {
                    return _Sizes;
                }
                else
                {
                    return _Sizes.Where(b => b.Categoryies.Any(c => categoryId.Value.ToString().StartsWith(c.ID.ToString())));
                   // return _Sizes.Where(b => categoryId.Value.ToString().StartsWith(b.CategoryID.ToString()));
                }
            }
        }
        public static void UpdateSize(TblSize tblSize, IEnumerable<long> categories)
        {
            using (TransactionScope trans = CreateTransactionScope())
            {
                var con = GetConnection();
                con.Execute("usp_update_TblSize", tblSize, commandType: CommandType.StoredProcedure);
                if (categories.Count() > 0)
                {
                    con.Execute("delete from TblSizeCategory where SizeID = @SizeID", new { SizeID = tblSize.ID }, commandType: CommandType.Text);
                }
                foreach (long cat in categories)
                {
                    con.Execute("usp_Insert_TblSizeCategory", new { SizeID = tblSize.ID, CategoryID = cat }, commandType: CommandType.StoredProcedure);
                }
                trans.Complete();
            }
        }
        public static void AddSize(TblSize tblSize, IEnumerable<long> categories)
        {
            using (TransactionScope trans = CreateTransactionScope())
            {
                var con = GetConnection();
                tblSize.ID = con.Query<int>("usp_insert_TblSize", tblSize, commandType: CommandType.StoredProcedure).First();
                foreach (long cat in categories)
                {
                    con.Execute("usp_Insert_TblSizeCategory", new { SizeID = tblSize.ID, CategoryID = cat }, commandType: CommandType.StoredProcedure);
                }
                trans.Complete();
            }
        }

        public static void DeleteSize(int Id)
        {
            GetConnection().Execute("Delete from tblsize where Id = @Id", new { Id = Id });
            _Sizes = null;
        }

        static IEnumerable<TblCategory> GetSizeCategoryies(int sizeId)
        {
            return GetConnection().Query<TblCategory>("usp_GetSizeCategoryies", new { sizeId = sizeId }, commandType: CommandType.StoredProcedure);
        }

        public static void ReLoad()
        {
            lock (_lock)
            {
                _Sizes = null;
            }
        }
    }
}
