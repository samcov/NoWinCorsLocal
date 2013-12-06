using System;
using System.Security.Principal;
using System.Web;
using System.Web.Routing;
using System.Web.Security;

namespace Microsoft.AspNet.SignalR.Samples
{
    public partial class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.MapHubs(new HubConfiguration { EnableCrossDomain = true }); // for SignalR
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

        //    if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
        //    {
        //        //These headers are handling the "pre-flight" OPTIONS call sent by the browser
        //        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
        //        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "*");
        //        HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
        //        HttpContext.Current.Response.End();
        //    }
        //}

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                var principal = Context.Cache[authTicket.Name] as IPrincipal;
                if (!authTicket.Expired && principal != null)
                {
                    Context.User = principal;
                }
            }
        }   
    }
}