using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentalProject.Classes;
namespace RentalProject._handler
{
    /// <summary>
    /// Summary description for Logout
    /// </summary>
    public class Logout : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.Session != null)
            {
                context.Session.Abandon();
            }
            System.Web.Security.FormsAuthentication.SignOut();
            context.Response.Redirect("~/login.aspx");
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