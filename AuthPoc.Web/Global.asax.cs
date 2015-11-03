using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AuthPoc.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start()
        {
            var cookie = Request.Cookies["ApiAccessToken"];

            if (cookie != null && cookie.Value != null)
                Session["ApiAccessToken"] = cookie.Value;
        }

        protected void Session_End()
        {
            // Ta bort access token från session
            Session.Remove("ApiAccessToken");
        }
    }
}
