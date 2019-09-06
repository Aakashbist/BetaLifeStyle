using BetaLifeStyle.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using static BetaLifeStyle.Services.CartHandler;

namespace BetaLifeStyle.Services
{
    public class CookieHandler
    {
        public const string CartCookieName = "Cart";
        public const string privatekey = "asd";

        public List<CartItem> cartitems;

        public CookieHandler()
        {
            if (CookieExist(CartCookieName))
            {
                string decrypted = GetCookie(CartCookieName);
                cartitems = new JavaScriptSerializer().Deserialize<List<CartItem>>(decrypted) ;
                if(cartitems==null)
                {
                    cartitems = new List<CartItem>();
                }
            }
            else
            {
                EmptyCookie(CartCookieName);
            }
        }

        public List<CartItem> GetCartItems()
        {
            return cartitems;
        }

        public void SyncListItems()
        {
            string str_cartitems = new JavaScriptSerializer().Serialize(cartitems);
            AddCookie(CartCookieName, str_cartitems);
        }


        public string EmptyCookie(string cookiename)
        {
            string empty = new JavaScriptSerializer().Serialize(new List<CartHandler.CartItem>());
            AddCookie(cookiename, empty);
            cartitems = new List<CartItem>();
            return empty;
        }

        public void AddCookie(string cookiename, string value)
        {
            HttpCookie cookiee = HttpContext.Current.Request.Cookies[cookiename];
            if (cookiee == null)
            {
                cookiee = new HttpCookie(cookiename);
            }

            cookiee.Value = Crypto.EncryptStringAES(value, privatekey);
            cookiee.Expires = DateTime.UtcNow.AddDays(1);
            HttpContext.Current.Response.Cookies.Add(cookiee);
        }

        public string GetCookie(string cookiename)
        {
            if (CookieExist(cookiename))
            {
                HttpCookie cookiee = HttpContext.Current.Request.Cookies[cookiename];
                var decryptCookie = Crypto.DecryptStringAES(cookiee.Value, privatekey);
                return decryptCookie;
            }
            else
            {
                return EmptyCookie(cookiename);
            }
        }

        public Boolean CookieExist(string cookiename)
        {
            HttpCookie cookiee = HttpContext.Current.Request.Cookies[cookiename];
            try
            {
                if (cookiee.Value == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public void DeleteCookie(string cookiename)
        {
            if(CookieExist(cookiename))
            {
                HttpCookie cookiee = HttpContext.Current.Request.Cookies[cookiename];
                cookiee.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(cookiee);
            }
        }
    }
}