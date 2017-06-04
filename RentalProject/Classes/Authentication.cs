using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using RentalDataAccess;



namespace RentalProject.Classes
{
    public class Authentication
    {
        /// <summary>
        /// Creating a ticket of the user data on the applicaion.
        /// </summary>
        /// <param name="Users_CheckLoginCls"></param>
        /// <param name="UserData"></param>
        /// <param name="RememberMe"></param>
        public static void CreateTicketForUser(Login_Users_Result Users_CheckLoginCls, string UserData, bool RememberMe)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                 1, // Ticket version
                 Users_CheckLoginCls.UserName.ToString(),// Username to be associated with this ticket
                 DateTime.Now, // Date/time ticket was issued
                 DateTime.Now.AddDays(7), // Date and time the cookie will expire
                 RememberMe, // if user has checked remember me then create persistent cookie
                 UserData, // store the user data, in this case roles of the user
                 FormsAuthentication.FormsCookiePath); // Cookie path specified in the web.config file in <Forms> tag if any.

            // To give more security it is suggested to hash it
            string hashCookies = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashCookies); // Hashed ticket

            // Add the cookie to the response, user browser
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        /// <summary>
        /// Get the data from the ticket.
        /// </summary>
        /// <returns></returns>
        public static UserClass GetUserFromTheTicket()
        {
            UserClass UserCls = null;
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                UserCls = GenericJSONHelper.Deserialize<UserClass>(authTicket.UserData);
            }
            return UserCls;
        }
        /// <summary>
        /// Get the user data from ticket or session(if it saved in session) 
        /// </summary>
        /// <returns></returns>
        public static UserClass GetUserFromSessionOrFromTicket()
        {
            UserClass UserCls = null;
            //if (SessionWrapper.UserClassFromTicket == null)
            //{
            UserCls = Authentication.GetUserFromTheTicket();
            //if (UserCls != null)
            //{
            //    SessionWrapper.UserClassFromTicket = UserCls;
            //}
            //}
            //else
            //{
            //    UserCls = SessionWrapper.UserClassFromTicket;
            //}

            return UserCls;
        }
        /// <summary>
        /// Saving the cookies. 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        public static void SaveUserCredentialToCookie(string UserName, string Password)
        {
            HttpCookie CycleCookie = new HttpCookie(StaticNames.CookieLoginName);
            CycleCookie.Expires = DateTime.Now.AddDays(7);
            CycleCookie[StaticNames.CookieUserName] = UtilityClass.EnDecryptPassword(UserName, UtilityClass.SecurityAction.Encrypt);
            CycleCookie[StaticNames.CookiePassword] = UtilityClass.EnDecryptPassword(Password, UtilityClass.SecurityAction.Encrypt);

            // Encoding the cookie then tamering it before decoding.
            //HttpCookie encodedCookie = CookieSecurity.EncodeCookie(CycleCookie);
            HttpContext.Current.Response.Cookies.Add(CycleCookie);
        }
        /// <summary>
        /// Getting the data from the cookies.
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        public static void GetUserCredentialFromCookie(out string UserName, out string Password)
        {
            UserName = "";
            Password = "";
            HttpCookie CycleCookie = HttpContext.Current.Request.Cookies.Get(StaticNames.CookieLoginName);
            if (CycleCookie != null)
            {
                try
                {
                    //  CycleCookie = CookieSecurity.DecodeCookie(CycleCookie);
                    UserName = UtilityClass.EnDecryptPassword(CycleCookie[StaticNames.CookieUserName].ToString(), UtilityClass.SecurityAction.Decrypt);
                    Password = UtilityClass.EnDecryptPassword(CycleCookie[StaticNames.CookiePassword].ToString(), UtilityClass.SecurityAction.Decrypt);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}