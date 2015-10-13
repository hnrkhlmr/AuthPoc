using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthPoc.Web.Models
{
    public class AuthPocUser
    {
        public static bool IsAuthenticated
        {
            get
            {
                if (System.Web.HttpContext.Current.Session.Count > 0 && !string.IsNullOrEmpty(System.Web.HttpContext.Current.Session["ApiAccessToken"].ToString()))
                {
                    return true;
                }
                else
                    return false;

            }
        }

        
        public static string UserName
        {
            get 
            {
                if (System.Web.HttpContext.Current.Session.Count > 0 && !string.IsNullOrEmpty(System.Web.HttpContext.Current.Session["username"].ToString()))
                    return System.Web.HttpContext.Current.Session["username"].ToString();
                else
                    return "Unknown";
            }
            set { System.Web.HttpContext.Current.Session["username"] = value; }
        }
        
        public static string AccessToken
        {
            get
            {
                if (System.Web.HttpContext.Current.Session.Count > 0 && !string.IsNullOrEmpty(System.Web.HttpContext.Current.Session["ApiAccessToken"].ToString()))
                {
                    return System.Web.HttpContext.Current.Session["ApiAccessToken"].ToString();
                }
                else
                    return "";

            }
            set
            {
                System.Web.HttpContext.Current.Session["ApiAccessToken"] = value;
            }
        }
        
    }	
}