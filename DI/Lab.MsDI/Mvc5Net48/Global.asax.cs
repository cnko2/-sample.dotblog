using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Mvc5Net48;

[assembly: PreApplicationStartMethod(typeof(MvcApplication), "InitModule")]
namespace Mvc5Net48
{
    public class MvcApplication : HttpApplication
    {
        public static void InitModule()
        {
            RegisterModule(typeof(ServiceScopeHttpModule));
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DependencyInjectionConfig.Register();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}