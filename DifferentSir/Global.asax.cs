using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DifferentSir
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


        // Using Application_Error event of HttpApplication
        /**
         * 
         * The ideal way to log exception occurred in any part of your MVC application is to handle it in 
         * the Application_Error event in the global.asax file. 
         * 
         * 
         * The Application_Error event is fired on any type of exception and error codes. So, handle it carefully. 
         */

        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            //log an exception
        }
    }
}
