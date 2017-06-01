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
using Alb.Common.DataAccess;

namespace Alb.Omdehsara.UI.MVC.Api
{
    public class LocationController : OmdehsaraApiControllerBase
    {
        [HttpGet]
        public IHttpActionResult GetProvinces()
        {
            try
            {
                return Ok(LocationDA.GetProvinces());
            }
            catch (Exception ex)
            {
                return HttpException(ex);
            }
        }
        public IHttpActionResult GetCities(int Id)
        {
            try
            {
                return Ok(LocationDA.GetCities(Id));
            }
            catch (Exception ex)
            {
                return HttpException(ex);
            }
        }
    }
}
