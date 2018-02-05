using BetaLifeStyle.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static BetaLifeStyle.Services.CartHandler;

namespace BetaLifeStyle.Public
{
    public partial class CartView : System.Web.UI.Page
    {
        CartHandler carthandler;

        int totalPrice = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            BetaDB db = new BetaDB();

            carthandler = new CartHandler();

            if (!IsPostBack)
            {
                CartDetails();
            }

            if (carthandler.GetCount() == 0)
            {
                cartCalculation.Visible = false;
            }
            //else
            //{
            //    cartCalculation.Visible = true;
            //}
            if (!Page.IsPostBack)
            {
                lblCartTotal.Text = totalPrice.ToString() + "$";
                double tax = (((double)totalPrice * 15) / 100);
                lbltax.Text = tax.ToString();
                lbldelivery.Text = NicePayment.PaymentHandler._fixedShipping.ToString()+"$";
                double grandtotal = totalPrice + Convert.ToDouble(lbltax.Text) + NicePayment.PaymentHandler._fixedShipping;
                lbltax.Text += "$";
                lblGrandCartTotal.Text = grandtotal.ToString() + "$";
            }
        }

        public void CartDetails()
        {
            BetaDB db = new BetaDB();
            if (Session["email"] != null)
            {
                var email = Session["email"].ToString();
                var Id = db.Logins.Where(x => x.Email.Equals(email)).Select(x => x.ID).FirstOrDefault();
                var producttt = (from c in db.Carts
                                 join p in db.Products
                                on c.ProductID equals p.ProductID
                                 where c.UserId == Id
                                 orderby c.ID
                                 select new { cartId = c.ID, ProductId = p.ProductID, size = c.Size, quantity = c.Quantity, productName = p.ProductName, price = p.ProductPrice * c.Quantity }).ToList();
                cartViewRepeater.DataSource = producttt;
                cartViewRepeater.DataBind();
                if (producttt.Count == 0)
                {
                    cartempty.Visible = true;
                    cartemptymsg.Text = "YOUR CART IS EMPTY";
                }
            }
            
            else if (carthandler.GetCount()!=0)
            {
                var productt = carthandler.cookiehandler.cartitems.Select(x => new { ProductId = x.Id, size = x.Size, quantity = x.Quantity }).ToList();
                cartViewRepeater.DataSource = productt;
                cartViewRepeater.DataBind();
            }
            else
            {
                cartempty.Visible = true;
                cartemptymsg.Text = "YOUR CART IS EMPTY";
            }
        }

        protected void cartViewRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            BetaDB db = new BetaDB();
            HiddenField hdnID = e.Item.FindControl("ProHdn") as HiddenField;
            int Id = Convert.ToInt32(hdnID.Value);
            var images = db.ProductImages.Where(im => im.ProductID.Equals(Id)).FirstOrDefault();

            HiddenField qtyhdnID = e.Item.FindControl("hdnqty") as HiddenField;
            int qty = Convert.ToInt32(qtyhdnID.Value);

            var productts = db.Products.Where(x => x.ProductID == Id).Select(x => new { productName = x.ProductName, price = qty * x.ProductPrice }).ToList();

            Label productPrice = e.Item.FindControl("lblProductPrice") as Label;
            productPrice.Text = productts.Select(x => x.price).FirstOrDefault().ToString();

            Label productName = e.Item.FindControl("lblProductName") as Label;
            productName.Text = productts.Select(x => x.productName).FirstOrDefault().ToString();

            Label productSize = e.Item.FindControl("lblProductSize") as Label;
            var size = productSize.Text;

            DropDownList productQuantity = e.Item.FindControl("Quantity") as DropDownList;
            var stock = db.Stocks.Where(x => x.ProductID == Id && x.ProductSizeName == size).FirstOrDefault();
            if (stock != null)
            {
                Label stocklabel = e.Item.FindControl("lblstock") as Label;
                if (stock.StockCount > 0)
                {
                    stocklabel.ForeColor = System.Drawing.Color.Green;
                    stocklabel.Text = "Available in stock";
                }
                else
                {
                    stocklabel.ForeColor = System.Drawing.Color.Red;
                    stocklabel.Text = "Out of stock";
                }

                List<int> list = new List<int>();
                if (stock.StockCount >= qty)
                {
                    for (int i = 0; i <= stock.StockCount; i++)
                    {
                        list.Add(i);
                    }
                }
                else
                {
                    for (int i = 0; i <= qty; i++)
                    {
                        list.Add(i);
                    }
                }

                productQuantity.DataSource = list;
                // to update quantity in cart
                productQuantity.Text = qty.ToString();
                productQuantity.DataBind();
            }
            else
            {
                List<int> list = new List<int>();
                list.Add(0);
                list.Add(1);
                productQuantity.DataSource = list;
                productQuantity.Text = qty.ToString();
                productQuantity.DataBind();

                Label stocklabel = e.Item.FindControl("lblstock") as Label;
                stocklabel.ForeColor = System.Drawing.Color.Red;
                stocklabel.Text = "Out of stock";
            }
            Image img = e.Item.FindControl("proImg") as Image;
            string imgpath = "~/ProductImage/" + images.ProductImagePath.ToString();
            img.ImageUrl = imgpath;

            //for cart total
            totalPrice = totalPrice + Convert.ToInt32(productPrice.Text);
        }

        protected void cartViewRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            BetaDB db = new BetaDB();

            HiddenField hdnID = e.Item.FindControl("ProHdn") as HiddenField;
            int id = Convert.ToInt32(hdnID.Value);
            Label productSize = e.Item.FindControl("lblProductSize") as Label;
            var size = productSize.Text;
            DropDownList dl = e.Item.FindControl("Quantity") as DropDownList;
            int qty = Convert.ToInt32(dl.SelectedItem.Text);
            
            if (e.CommandName == "remove")
            {
                if (carthandler.GetCount() != 0)
                {
                    CartItem ci = new CartItem { Id = id, Quantity = qty, Size = size };
                    carthandler.RemoveFromCart(ci);

                    //cookiehandler.DeleteCookie("Cart", Id, size);
                    //cookiehandler.ch.products = cookiehandler.GetCookie("Cart");
                    //cookiehandler.AddCookie("Cart", cookiehandler.ch.products);
                }
            }
            CartDetails();
            Response.Redirect(Request.Url.ToString());
        }
        
        protected void Quantity_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            RepeaterItem item = (RepeaterItem)ddl.NamingContainer;
            BetaDB db = new BetaDB();

            HiddenField hdnID = item.FindControl("ProHdn") as HiddenField;
            int Id = Convert.ToInt32(hdnID.Value);
            Label productSize = item.FindControl("lblProductSize") as Label;
            var size = productSize.Text;
            DropDownList dl = item.FindControl("Quantity") as DropDownList;
            int qty = Convert.ToInt32(dl.SelectedItem.Text);

            if (Session["email"] != null)
            {
                if (Convert.ToInt32(dl.SelectedItem.Text) != 0)
                {
                    var email = Session["email"].ToString();
                    var userId = db.Logins.Where(x => x.Email == email).Select(x => x.ID).FirstOrDefault();
                    int cartId = db.Carts.Where(x => x.ProductID == Id && x.Size == size).Select(x => x.ID).FirstOrDefault();

                    int quantity = Convert.ToInt32(((DropDownList)item.FindControl("Quantity")).SelectedValue);
                    Cart cart = new Cart()
                    {
                        ID = cartId,
                        UserId = userId,
                        ProductID = Id,
                        Size = size,
                        Quantity = quantity
                    };
                    db.Entry(cart).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                }
                else
                {
                    CartItem ci = new CartItem { Id = Id, Quantity = qty, Size = size };
                    carthandler.RemoveFromCart(ci);
                }

            }
            else if (carthandler.GetCount()!=0)
            {
                if (Convert.ToInt32(dl.SelectedItem.Text) == 0)
                {
                    CartItem ci = new CartItem { Id = Id, Quantity = qty, Size = size };
                    carthandler.RemoveFromCart(ci);
                }
                else
                {

                    //carthandler.RemoveFromCart(Id,size);
                    //carthandler.AddtoCart(Id, size, qty);
                    //cookiehandler.ch.AddtoCart(Id, size, qty);
                    //cookiehandler.AddCookie("Cart", cookiehandler.ch.products);

                    foreach(CartItem ci in carthandler.cookiehandler.cartitems)
                    {
                        if(ci.Size==size && ci.Id==Id)
                        {
                            ci.Quantity = qty;
                        }
                    }
                    carthandler.cookiehandler.SyncListItems();
                }
            }
            CartDetails();
            Response.Redirect(Request.Url.ToString());
        }

        protected void placeorderbutton_Click(object sender, EventArgs e)
        {

            if (Session["email"] != null)
            {
                Response.Redirect("/Checkout");
            }
            else
            {
                // Go to Login 
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "OpenLoginModel",
                    "<script type='text/javascript'>function OpenLoginModel(){$('#loginModal').modal('show');}; OpenLoginModel()</script>", false);
            }
        }
    }
}
