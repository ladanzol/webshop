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
using Dapper.Contrib.Extensions;
namespace Alb.Omdehsara.DataAccess.Cms
{
    public class FileDA : AlbDataAccessBase
    {
        public static int SaveImage(TblImage image)
        {
           return  GetConnection().Query<int>("cms.usp_insert_image", image, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public static IEnumerable<TblImage> GetImages(int pageIndex, int pageSize, string search, out int totalRecords)
        {
            return GetMultiple<TblImage>("cms.usp_GetImages", new { pageIndex = pageIndex, pageSize = pageSize, search = search }, out  totalRecords);
        }

        public static TblImage GetImage(int Id)
        {
            return GetConnection().Query<TblImage>("select * from cms.tblImage where id= @id", new { Id = Id }, commandType: CommandType.Text).FirstOrDefault();
        }

        public static void DeleteImage(int Id)
        {
            GetConnection().Execute("delete from cms.tblImage where id= @id", new { Id = Id }, commandType: CommandType.Text);
        }
    }
}
