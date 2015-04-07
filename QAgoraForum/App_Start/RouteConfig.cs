using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QAgoraForum
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "CreateTopic",
                url: "{controller}/{action}/{topic}/{sectionId}",
                defaults: new { controller = "Topics", action = "Create", sectionId = UrlParameter.Optional}
            );

            routes.MapRoute(
                name: "Sender",
                url: "Mail/SendMessage/{message}/{user}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );

        }
    }
}
