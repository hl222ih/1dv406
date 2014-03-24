using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Project;
using System.Web.Configuration;
using System.Data.SqlClient;


namespace Project
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //för att lyssna på ändringar gjorda i databasen, 
            //placeras här för att endast köras en enda gång.
            //Men...inte möjligt p.g.a. att jag saknar rättigheter att 
            //ställa in change_tracking/enable_broker på databasen. :/
            //SqlDependency.Start(WebConfigurationManager.ConnectionStrings["BlissKomConnectionString"].ConnectionString);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
            // frigör resurser...
            //SqlDependency.Stop(WebConfigurationManager.ConnectionStrings["BlissKomConnectionString"].ConnectionString);
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }
    }
}
