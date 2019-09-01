using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using MapIt.Web;
using MapIt.Helpers;
using MapIt.Repository;

namespace MapIt.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Add Routes
            RegisterCustomRoutes(RouteTable.Routes);
        }

        void RegisterCustomRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("Default", "", "~/Default.aspx");
            routes.MapPageRoute("CPage", "Page/{PageName}", "~/CPage.aspx");
            routes.MapPageRoute("Offer", "Ofr/{PageName}", "~/Offer.aspx");
            routes.MapPageRoute("Types", "Typs/{PageName}", "~/Types.aspx");
            routes.MapPageRoute("Property", "Pro/{PageName}", "~/Property.aspx");
            routes.MapPageRoute("ServCat", "SrvCat/{PageName}", "~/ServCat.aspx");
            routes.MapPageRoute("Services", "Srvs/{PageName}", "~/Services.aspx");
            routes.MapPageRoute("Service", "Srv/{PageName}", "~/Service.aspx");
            routes.MapPageRoute("Broker", "Bro/{PageName}", "~/Broker.aspx");
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }
        void Session_Start(object sender, EventArgs e)
        {
            TokenObj obj = PresentHelper.GenerateToken();
            if (obj != null)
            {
                Session["Tocken"] = obj.Token;
            }
        }
        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }
    }
}
