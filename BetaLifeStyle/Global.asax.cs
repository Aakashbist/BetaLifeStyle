using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Optimization;
using Beta;
using System.Web.Routing;

namespace BetaLifeStyle
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteTable.Routes.MapPageRoute("Home",                  "Home",                         "~/Public/ViewProducts.aspx");
            RouteTable.Routes.MapPageRoute("Default",               "",                             "~/Public/ViewProducts.aspx");
            RouteTable.Routes.MapPageRoute("About",                 "About",                        "~/Private/About.aspx");
            RouteTable.Routes.MapPageRoute("PaypalProcess",         "Process/{q}",                  "~/Private/PayPalProcess.aspx", true, new RouteValueDictionary { { "q", "defaultvalue" } });
            RouteTable.Routes.MapPageRoute("PaypalCancel",          "Cancel/{q}",                   "~/Private/PayPalCancel.aspx", true, new RouteValueDictionary { { "q", "defaultvalue" } });
            RouteTable.Routes.MapPageRoute("Activation",            "Activation/{guid}",            "~/Public/Activationpage.aspx");
            RouteTable.Routes.MapPageRoute("Productview",           "Product/{id}",                 "~/Public/ProductDetails.aspx");
            RouteTable.Routes.MapPageRoute("ViewCategory",          "View/{Category}",              "~/Public/ViewProducts.aspx");
            RouteTable.Routes.MapPageRoute("ViewSubCategory",       "View/{Category}/{SubCategory}","~/Public/ViewProducts.aspx");
            RouteTable.Routes.MapPageRoute("SearchProduct",         "Search/{SearchTerm}",          "~/Public/ViewProducts.aspx");
            RouteTable.Routes.MapPageRoute("ChangePassword",        "ChangePassword/{guid}",        "~/Private/ChangePassword.aspx");
            RouteTable.Routes.MapPageRoute("ChangePassword2",       "ChangePassword",               "~/Private/ChangePassword.aspx");
            RouteTable.Routes.MapPageRoute("Checkout",              "Checkout",                     "~/Private/Checkout.aspx");


        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}