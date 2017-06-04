using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentalDataAccess;


namespace RentalProject.Classes
{

    public class SessionWrapper
    {
        public SessionWrapper()
        {

        }

        #region Get & Set functions
        /// <summary>
        /// Unify the type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>

        private static T GetFromSession<T>(string key)
        {
            object obj = HttpContext.Current.Session[key];
            if (obj == null)
            {
                return default(T);
            }
            return (T)obj;
        }

        private static void SetInSession<T>(string key, T value)
        {
            HttpContext.Current.Session[key] = value;
        }

        private static T GetFromApplication<T>(string key)
        {
            return (T)HttpContext.Current.Application[key];
        }

        private static void SetInApplication<T>(string key, T value)
        {
            if (value == null)
            {
                HttpContext.Current.Application.Remove(key);
            }
            else
            {
                HttpContext.Current.Application[key] = value;
            }
        }


        #endregion


        #region Session Names

        #region Users

       

        public static UserClass UserClassFromTicket
        {
            get { return GetFromSession<UserClass>("UserClassFromTicket"); }
            set
            {
                if (value == null)
                {
                    HttpContext.Current.Session.Remove("UserClassFromTicket");
                }
                else
                {
                    SetInSession<UserClass>("UserClassFromTicket", value);
                }
            }
        }

        #endregion

        public static string MenuControl
        {
            get { return GetFromSession<string>("MenuControl"); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    HttpContext.Current.Session.Remove("MenuControl");
                }
                else
                {
                    SetInSession<string>("MenuControl", value);
                }
            }
        }


        public static string MenuCategiries
        {
            get { return GetFromSession<string>("MenuCategiries"); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    HttpContext.Current.Session.Remove("MenuCategiries");
                }
                else
                {
                    SetInSession<string>("MenuCategiries", value);
                }
            }
        }

        public static string MenuSearchControl
        {
            get { return GetFromSession<string>("MenuSearchControl"); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    HttpContext.Current.Session.Remove("MenuSearchControl");
                }
                else
                {
                    SetInSession<string>("MenuSearchControl", value);
                }
            }
        }

        #endregion

    }

}