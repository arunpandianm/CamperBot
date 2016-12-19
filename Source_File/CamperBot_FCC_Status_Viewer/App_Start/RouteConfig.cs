using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CamperBot_FCC_Status_Viewer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "General",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "General", action = "Dashboard", id = UrlParameter.Optional }
            );
        }
    }
}
