using AutoMapper;
using Alb.Tools.UI.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using System.Web.Security;
using Newtonsoft.Json;
using Alb.Tools.UI.MVC.Security;
using System.Configuration;

using Alb.Tools.Utility;
using System.Threading.Tasks;
using System.IO;
using Alb.Omdehsara.UI.MVC.Api.Models;
using Alb.Omdehsara.Common.Product;
using Alb.Omdehsara.Common;
using Alb.Omdehsara.UI.MVC.Models;
using Alb.Omdehsara.Common.Orders;
namespace Alb.Omdehsara.UI.MVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class AlbMvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.DefaultBinder = new AlbModelBinder();
            RegisterMapping();
        }

        private void RegisterMapping()
        {
            Mapper.CreateMap<TblCategoryProperty, CategoryPropertyViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            Mapper.CreateMap<ProductPropertyList, TblProductProperty>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            Mapper.CreateMap<ProductViewModel, TblProduct>().IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();
            Mapper.CreateMap<ProductTypeView, TblProductType>().IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();
            
            Mapper.CreateMap<ProductView, TblProduct>().IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();
            Mapper.CreateMap<OrderDetail, TblOrderDetail>().IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();
            Mapper.CreateMap<OrderViewModel, OrderView>().IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();
            Mapper.CreateMap<StoreProduct, TblProduct>().IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();
            Mapper.CreateMap<StoreProduct, TblProductLocation>().IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();
            
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
            {

                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                CustomPrincipalSerializeModel serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);
                CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
                newUser.UserId = serializeModel.UserId;
                newUser.FirstName = serializeModel.FirstName;
                newUser.LastName = serializeModel.LastName;
                newUser.roles = serializeModel.roles;
                HttpContext.Current.User = newUser;
            }
        }
    }
}