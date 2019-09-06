using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetaLifeStyle.Services
{
    public class CartHandler
    {
        public CookieHandler cookiehandler;
        BetaDB db;
        public CartHandler()
        {
            db = new BetaDB();
            cookiehandler = new CookieHandler();
        }

        public void SyncCookieDatainDb()
        {
            if (CheckLoginSession())
            {
                foreach (CartItem cartitem in cookiehandler.cartitems)
                {
                    AddtoCart(cartitem.Id, cartitem.Size, cartitem.Quantity);
                }

                cookiehandler.DeleteCookie(CookieHandler.CartCookieName);
            }
        }

        public bool CheckLoginSession()
        {
            if(HttpContext.Current.Session["email"]!=null)
            {
                return true;
            }
            return false;
        }

        public void AddtoCart(int id, string size,int quantity)
        {
            CartItem _new = new CartItem{ Id = id, Quantity = quantity, Size=size };
            if (CheckLoginSession())
            {
                // write to db if required
                var email = HttpContext.Current.Session["email"].ToString();
                var user = MethodHandler.GetUserByEmail(email);
                var cartitem = db.Carts.Any(x => x.ProductID == id && x.Size == size && x.UserId == user.ID);
                if (!cartitem)
                {
                    Cart cart = new Cart()
                    {
                        ProductID = id,
                        UserId = user.ID,
                        Size = size,
                        Quantity = 1
                    };
                    db.Carts.Add(cart);
                    db.SaveChanges();
                }
            }
            else
            {
                if (!cookiehandler.cartitems.Any(x => x.Id== id && x.Size == size ))
                {
                    cookiehandler.cartitems.Add(_new);
                    //Update Cookie
                    cookiehandler.SyncListItems();
                }
            }
        }

        

        public int GetCount()
        {
            if (CheckLoginSession())
            {
                string email = HttpContext.Current.Session["email"].ToString();
                var user = MethodHandler.GetUserByEmail(email);
                int count  = db.Carts.Count(x => x.UserId == user.ID);
                return count;
            }
            else
            {
                try
                {
                    return cookiehandler.cartitems.Count;
                }catch(Exception e)
                {
                    return 0;
                }
            }
        }

        public void RemoveFromCart(CartItem product)
        {
            if (CheckLoginSession())
            {
                string email = HttpContext.Current.Session["email"].ToString();
                var user = MethodHandler.GetUserByEmail(email);
                Cart cartId = db.Carts.Where(x => x.ProductID == product.Id && x.Size == product.Size && x.UserId == user.ID).FirstOrDefault();
                db.Entry(cartId).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            else
            {
                if (cookiehandler.cartitems.Exists(x=>x.Id==product.Id && x.Size ==product.Size))
                {
                    cookiehandler.cartitems.Remove(cookiehandler.cartitems.Single(x => x.Id == product.Id && x.Size == product.Size));
                    cookiehandler.SyncListItems();
                }
            }
        }


        public void RemoveFromCart(int id, string Size)
        {
            if (CheckLoginSession())
            {
                string email = HttpContext.Current.Session["email"].ToString();
                var user = MethodHandler.GetUserByEmail(email);
                Cart cartId = db.Carts.Where(x => x.ProductID == id && x.Size == Size && x.UserId == user.ID).FirstOrDefault();
                db.Entry(cartId).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            else
            {
                CartItem toberemoved = new CartItem();
                foreach (CartItem ci in cookiehandler.cartitems)
                {
                    if (ci.Id == id && ci.Size == Size)
                    {
                        toberemoved.Id = ci.Id;
                        toberemoved.Quantity = ci.Quantity;
                        toberemoved.Size = ci.Size;
                    }
                }
                cookiehandler.cartitems.Remove(toberemoved);
                cookiehandler.SyncListItems();

            }
        }

        //public void SetProductList(List<ProductModel> pms)
        //{

        //}

        //public List<ProductModel> GetProductList()
        //{

        //}

        public class CartItem
        {
            public int Id { get; set; } 
            public string Size { get; set; }
            public int Quantity { get; set; }

        }
    }
}
    