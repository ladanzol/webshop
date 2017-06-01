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
using System.Transactions;
using Alb.Tools.DataAccess;
namespace Alb.Omdehsara.DataAccess.Cms
{
    public class PageDA : AlbDataAccessBase
    {
        public static int AddPage(TblPage tblPage)
        {
            using (TransactionScope trans = CreateTransactionScope())
            {
                tblPage.ID = GetConnection().Query<int>("cms.usp_insert_TblPage", tblPage, commandType: CommandType.StoredProcedure).FirstOrDefault();

                trans.Complete();
                return tblPage.ID;
            }
        }

        public static TblPage GetPage(int Id)
        {
            return GetConnection().Query<TblPage>("select * from cms.tblpage where id = @Id", new { Id = Id }, commandType: CommandType.Text).FirstOrDefault();
        }
        public static void UpdatePage(TblPage tblPage)
        {
            using (TransactionScope trans = CreateTransactionScope())
            {
                GetConnection().Execute("cms.usp_Update_TblPage", tblPage, commandType: CommandType.StoredProcedure);
                GetConnection().Execute("cms.usp_delete_TblPageCategory", new { PageId = tblPage.ID }, commandType: CommandType.StoredProcedure);

                trans.Complete();
            }
        }

        public static IEnumerable<TblPage> GetPages(int? PageTypeID, int pageIndex, int pageSize, bool? isApproved, int? categoryID, out int totalRecords)
        {
            return GetMultiple<TblPage>("cms.usp_getPages", new { PageTypeID = PageTypeID, pageIndex = pageIndex, pageSize = pageSize, isApproved = isApproved, categoryID = categoryID }, out totalRecords);
        }
        public static void DeletePage(int Id)
        {
            GetConnection().Execute("delete from cms.tblpage where id = @id", new { Id = Id }, commandType: CommandType.Text);
        }

        public static IEnumerable<TblPage> GetPageList(int count, int? pageType = null, int? category = null)
        {
            return GetConnection().Query<TblPage>("cms.usp_GetPageList", new { count = count, pageTypeID = pageType, categoryID = category }, commandType: CommandType.StoredProcedure);
        }
        public static IEnumerable<TblPage> GetNewsArticleList(int count, int? categoryID)
        {
            return GetConnection().Query<TblPage>("cms.usp_GetNewsArticleList", new { count = count, categoryID = categoryID }, commandType: CommandType.StoredProcedure);
        }
        public static void ApprovePage(int pageID)
        {
            GetConnection().Execute("update cms.tblpage set IsApproved = 1 where id = @id", new { Id = pageID }, commandType: CommandType.Text);
        }

        public static void RejectPage(int pageID)
        {
            GetConnection().Execute("update cms.tblpage set IsApproved = 0 where id = @id", new { Id = pageID }, commandType: CommandType.Text);
        }

        public static IEnumerable<TblPage> GetCityArticles(string cityIDs)
        {
            return GetConnection().Query<TblPage>("select ID,CityID, Title from TblPage where IsApproved = 1 and CityID in (" + cityIDs + ")");
        }



        public static IEnumerable<TblPage> GetNotSentNews()
        {
            return GetConnection().Query<TblPage>("cms.usp_GetNotSentNews", null, commandType: CommandType.StoredProcedure);
        }
        public static void SetSent(int pageID)
        {
            GetConnection().Execute("cms.usp_SetSent", new { pageID = pageID }, commandType: CommandType.StoredProcedure);
        }

        public static void SetSent()
        {
            GetConnection().Execute("cms.usp_SetSent",new {}, commandType: CommandType.StoredProcedure);
        }
    }
}
