using Alb.Common.UI.MVC;
using Alb.Omdehsara.Common;
using Alb.Tools.Common;
using Alb.Tools.UI.MVC;
using Alb.Tools.UI.MVC.Security;
using Alb.UI.MVC.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Alb.Omdehsara.UI.MVC.Api
{
    public class OmdehsaraApiControllerBase : ApiControllerBase
    {
        public IHttpActionResult HttpException(Exception ex)
        {
            if (ex is RuleException)
            {
                return BadRequest(ex.Message);
            }
            else
            {
                if (HttpContext.Current != null)
                {
                    ErrorLogger.LogError(ex, HttpContext.Current.Request.Url.ToString(), false);
                }
                else
                {
                    ErrorLogger.LogError(ex, string.Empty, false);
                }
                if (IsAuthenticated && User.IsInRole(OmdehsaraRoles.Admin.ToString()))
                {
                    return InternalServerError(ex);
                }
                {
                    return InternalServerError(new Exception("خطا هنگام انجام عملیات"));
                }
            }
        }
    }
}