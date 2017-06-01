using Alb.Omdehsara.Common;
using Alb.Omdehsara.DataAccess;
using Alb.Omdehsara.UI.MVC.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Alb.Omdehsara.UI.MVC.Api;
using Alb.Tools.UI.MVC.Security;
using Alb.Omdehsara.UI.MVC.Controllers;
using Alb.Tools.Common;
using Alb.Tools.Utility;
using Alb.Omdehsara.Common.Product;
using Alb.Omdehsara.UI.MVC.Models;
using Alb.Omdehsara.DataAccess.Product;
using Alb.Tools.UI.MVC;

namespace Alb.Omdehsara.UI.MVC.Api
{
    [AuthorizeEnum(ProjectRoles.Admin)]
    public class StoreController : OmdehsaraApiControllerBase
    {
        // GET api/province
        [HttpGet]
        public IEnumerable<TblStore> getStores()
        {
            return TblStoreDA.GetAllStores();
        }
        [HttpGet]
        public IEnumerable<TblShelf> getShelves(int? store)
        {
            int totalRecords;
            return TblShelfDA.GetShelfs(store, 1, int.MaxValue, out totalRecords);
        }

        public IHttpActionResult UpdateProductLocation(TblProductLocation model)
        {
            TblProductLocationDA.UpdateProductLocation(model);
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult SearchProductLocation(ProductLocationSearchOption searchOption)
        {
            int totalRecords;
            return Ok(new
            {
                Data = TblProductLocationDA.SearchProductLocation(searchOption, out totalRecords)
                ,TotalRecords = totalRecords
            });
        }
    }
}
