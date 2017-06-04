using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalProject._handler
{
    /// <summary>
    /// Summary description for LogoutWebsite
    /// </summary>
    public class LogoutWebsite : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            if (context.Session != null)
            {
                context.Session.Abandon();
            }

            System.Web.Security.FormsAuthentication.SignOut();
            context.Response.Redirect("~/WebSite/Default.aspx");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}