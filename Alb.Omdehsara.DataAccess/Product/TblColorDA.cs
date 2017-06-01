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
    public class TblColorDA : AlbDataAccessBase
    {
        public static IEnumerable<ColorCategories> _Colors = null;
        public static object _lock = new object();
        public static IEnumerable<ColorCategories> GetColor(long? categoryId = null, bool fromCache = false)
        {
            lock (_lock)
            {
                if (_Colors == null || !fromCache)
                {
                    _Colors = GetConnection().Query<ColorCategories>("usp_GetColor", new { }, commandType: CommandType.StoredProcedure);
                    foreach (ColorCategories color in _Colors)
                    {
                        color.Categoryies = GetColorCategoryies(color.ID);
                    }
                }
                if (categoryId == null)
                {
                    return _Colors;
                }
                else
                {
                    return _Colors.Where(b => b.Categoryies.Any(c=>categoryId.Value.ToString().StartsWith(c.ID.ToString())));
                }
            }
        }

        public static void UpdateColor(TblColor tblColor, IEnumerable<long> categories)
        {
            using (TransactionScope trans = CreateTransactionScope())
            {
                var con = GetConnection();
                con.Execute("usp_update_TblColor", tblColor, commandType: CommandType.StoredProcedure);
                if (categories.Count() > 0)
                {
                    con.Execute("delete from TblColorCategory where ColorID = @ColorID", new { ColorID = tblColor.ID }, commandType: CommandType.Text);
                }
                foreach (long cat in categories)
                {
                    con.Execute("usp_Insert_TblColorCategory", new { ColorID = tblColor.ID, CategoryID = cat }, commandType: CommandType.StoredProcedure);
                }
                trans.Complete();
            }
        }
        public static void AddColor(TblColor tblColor, IEnumerable<long> categories)
        {
            using (TransactionScope trans = CreateTransactionScope())
            {
                var con = GetConnection();
                tblColor.ID = con.Query<int>("usp_insert_TblColor", tblColor, commandType: CommandType.StoredProcedure).First();
                foreach (long cat in categories)
                {
                    con.Execute("usp_Insert_TblColorCategory", new { ColorID = tblColor.ID, CategoryID = cat }, commandType: CommandType.StoredProcedure);
                }
                trans.Complete();
            }
        }

        public static void DeleteColor(int Id)
        {
            GetConnection().Execute("Delete from tblcolor where Id = @Id", new { Id = Id });
            _Colors = null;
        }

        static IEnumerable<TblCategory> GetColorCategoryies(int colorId)
        {
            return GetConnection().Query<TblCategory>("usp_GetColorCategoryies", new { colorId = colorId }, commandType: CommandType.StoredProcedure);
        }
    }
}
