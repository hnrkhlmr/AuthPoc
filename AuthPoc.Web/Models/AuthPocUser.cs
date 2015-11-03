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

        public static int UserId
        {
            get
            {
                var exists = false;
                foreach (var item in System.Web.HttpContext.Current.Session.Keys)
	            {
		            item.ToString();
                    if(item.ToString().Equals("userid"))
                        exists = true;
	            }
                if (exists)
                    return Convert.ToInt32(System.Web.HttpContext.Current.Session["userid"].ToString());
                else
                    return 0;
            }
        }
        
        public static string UserName
        {
            get 
            {
                var exists = false;
                foreach (var item in System.Web.HttpContext.Current.Session.Keys)
                {
                    item.ToString();
                    if (item.ToString().Equals("username"))
                        exists = true;
                }
                if (exists)
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
                var exists = false;
                foreach (var item in System.Web.HttpContext.Current.Session.Keys)
                {
                    item.ToString();
                    if (item.ToString().Equals("ApiAccessToken"))
                        exists = true;
                }
                if (exists)
                {
                    return System.Web.HttpContext.Current.Session["ApiAccessToken"].ToString();
                }
                else
                    return null;

            }
            set
            {
                System.Web.HttpContext.Current.Session["ApiAccessToken"] = value;
            }
        }
        
    }	
}