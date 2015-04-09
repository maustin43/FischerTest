using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace FischerWeb
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			// Redirect to the SSL Site
			if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("http:"))
			{
				Response.Redirect(HttpContext.Current.Request.Url.ToString().ToLower().Replace("http:", "https:"));
			}
		}
    }
}
