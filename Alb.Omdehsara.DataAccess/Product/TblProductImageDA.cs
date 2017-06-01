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
namespace Alb.Omdehsara.DataAccess
{
    public class TblProductImageDA : AlbDataAccessBase
    {
        public static IEnumerable<TblProductImage> GetProductImage(int productID)
        {
            return GetConnection().Query<TblProductImage>("select * from tblproductImage where ProductID = @ProductID", new { ProductID = productID }, commandType: CommandType.Text);
        }
        public static IEnumerable<TblProductImage> GetProductImage(int productID, byte imageSize)
        {
            return GetConnection().Query<TblProductImage>("select * from tblproductImage where ProductID = @ProductID and ImageSize = @imageSize", new { ProductID = productID, ImageSize = imageSize }, commandType: CommandType.Text);
        }
        public static IEnumerable<TblProductImage> GetProductImage(IEnumerable<int> specialProductId)
        {
            string ids = string.Join(",", specialProductId);
            return GetConnection().Query<TblProductImage>("select * from tblproductImage where ProductID in (" + ids + ")");
        }
        public static int AddImage(int productID,string  img, string title,DateTime DateF)
        {
            var row = GetConnection().Query("usp_TblProductImage_Insert", new { ProductID = productID, ImageUrl = img, Title = title, CreateDate = DateF }, commandType: CommandType.StoredProcedure).Single();
           return Convert.ToInt32(row.ImageID);
        }



        public static TblProductImage GetImage(int imageId, int userID)
        {
            return GetConnection().Query<TblProductImage>("usp_TblProductImage_Get", new { imageId = imageId, userID = userID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }
        public static TblProductImage GetImage(string imageUrl)
        {
            return GetConnection().Query<TblProductImage>("usp_TblProductImage_GetByUrl", new { imageUrl = imageUrl }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }
        public static void DeleteProductImage(int imageId)
        {
            GetConnection().Execute("usp_TblProductImage_Delete", new { imageId = imageId }, commandType: CommandType.StoredProcedure);
        }

        public static int GetProductImageCount(int productID)
        {
            var row = GetConnection().Query("select count(*) ImageCount from TblProductImage where ProductID = @ProductID", new { ProductID = productID }).Single();
            return Convert.ToInt32(row.ImageCount);
        }


        public static void DeleteAllImage(int productID)
        {
            GetConnection().Execute("usp_TblProductImage_DeleteAllImage", new { productID = productID }, commandType: CommandType.StoredProcedure);
        }


    }
}
