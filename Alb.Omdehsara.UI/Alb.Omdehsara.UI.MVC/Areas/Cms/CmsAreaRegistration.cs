using System.Web.Mvc;

namespace Alb.Omdehsara.UI.MVC.Areas.Cms
{
    public class CmsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Cms";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
    "Cms_Page",
    "Cms/{id}_{title}",
    new { action = "Index", controller = "Home" },
    namespaces: new[] { "Alb.Omdehsara.UI.MVC.Areas.Cms.Controllers" }
);
            context.MapRoute(
                "Cms_default",
                "Cms/{controller}/{action}/{id}",
                new { action = "Index",controller="Home", id = UrlParameter.Optional },
                namespaces: new[] { "Alb.Omdehsara.UI.MVC.Areas.Cms.Controllers" }
            );

        }
    }
}