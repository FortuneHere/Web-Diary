using System.Web.Mvc;
using System.Web.Routing;

namespace WebDiary
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "LoginRoute",
                "{controller}/{action}/{id}",
                new {controller = "News", action = "Index", id = UrlParameter.Optional}
            );
            //
            // routes.MapRoute(
            //     "Default",
            //     "{controller}/{action}/{id}",
            //     new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            // );
        }
    }
}