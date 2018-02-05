using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BetaLifeStyle.NicePayment;

namespace BetaLifeStyle.Private
{
    public partial class Checkout : System.Web.UI.Page
    {
        BetaDB db;
        IEnumerable<Cart> cartitems;
        int userId;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Already Logged in
            if ((string)Session["email"] == null)
            {
                Response.Redirect("/Home?Login=true");
                Session.Clear();
            }
            if (!IsPostBack)
            {
                db = new BetaDB();
                var email = Session["email"].ToString();
                userId = db.Logins.Where(x => x.Email == email).Select(x => x.ID).FirstOrDefault();
                var login = db.Logins.Where(x => x.Email == email).SingleOrDefault();
                int count = login.Profiles.Count();
                Profile profile;
                if (count<=0)
                {
                    filladdress.Visible = true;
                    checkoutok.Visible = false;
                    return;
                }
                else
                {
                    profile = login.Profiles.ElementAt(0);
                    if(profile.Address.Trim().Equals("")|| profile.State.Trim().Equals("") || profile.City.Trim().Equals("") || profile.Country.Trim().Equals(""))
                    {
                        filladdress.Visible = true;
                        checkoutok.Visible = false;
                        return;
                    }
                    else
                    {
                        filladdress.Visible = false;
                        checkoutok.Visible = true;
                    }
                }
                cartitems = db.Carts.Where(x => x.UserId == userId).ToList<Cart>();
                double subtotal = 0;
                foreach (Cart ci in cartitems)
                {
                    var product = db.Products.Where(x => x.ProductID == ci.ProductID).SingleOrDefault();
                    var stock = db.Stocks.Where(x => x.ProductID == ci.ProductID && x.ProductSizeName == ci.Size).FirstOrDefault();
                    if (stock.StockCount < ci.Quantity)
                    {
                        outofstocklbl.Text = "Some Products are Out of Stock";
                    }
                    else
                    {
                        subtotal = subtotal + (Convert.ToDouble(product.ProductPrice * ci.Quantity));
                        var p = new HtmlGenericControl("p");
                        p.InnerHtml = "<b>" + product.ProductName + "</b>";
                        p.Attributes["class"] = "h5";

                        var p2 = new HtmlGenericControl("p");
                        p2.InnerHtml = product.ProductColor + "<br>";
                        p2.Attributes["class"] = "h6";

                        var span = new HtmlGenericControl("span");
                        span.InnerHtml = "Quantity: " + ci.Quantity + " × $" + (product.ProductPrice).ToString() +
                            " = <b>" + (ci.Quantity * product.ProductPrice).ToString() + "</b>";
                        span.Attributes["class"] = "h5 pull-right";

                        var div = new HtmlGenericControl("div");
                        div.Attributes["class"] = "h5 right";

                        div.Controls.Add(span);
                        div.Controls.Add(p);
                        div.Controls.Add(p2);
                        div.Attributes.Add("style", "padding:10px;background-color:#f3f3f3");
                        Products.Controls.Add(div);
                    }
                }


                var maindiv = new HtmlGenericControl("div");
                maindiv.Attributes.Add("style", "padding:10px;background-color:#f3f3f3");

                // Shipping
                double shipping = NicePayment.PaymentHandler._fixedShipping;
                var pp = new HtmlGenericControl("span");
                pp.InnerHtml = "<b>Shipping</b>";
                pp.Attributes["class"] = "h5";

                var sspan = new HtmlGenericControl("span");
                sspan.InnerHtml = "<b>$" + shipping + "</b><br/>";
                sspan.Attributes["class"] = " pull-right";

                var ddiv = new HtmlGenericControl("div");
                ddiv.Attributes["class"] = "h5";

                ddiv.Controls.Add(sspan);
                ddiv.Controls.Add(pp);
                maindiv.Controls.Add(ddiv);

                // Shipping
                var address = new HtmlGenericControl("span");
                address.InnerHtml = "<b>Shipping Address</b>";
                address.Attributes["class"] = "h5";

                var address2 = new HtmlGenericControl("span");
                address2.InnerHtml = profile.Address+", "+profile.City+", "+profile.State+", Pin: "+profile.PinCode+", "+profile.Country;
                address2.Attributes["class"] = " pull-right";

                var outer = new HtmlGenericControl("div");
                outer.Attributes["class"] = "h5";

                outer.Controls.Add(address);
                outer.Controls.Add(address2);
                maindiv.Controls.Add(outer);

                double tax = ((double)15 / 100) * subtotal;
                // Tax
                pp = new HtmlGenericControl("span");
                pp.InnerHtml = "<b>Tax</b>";
                pp.Attributes["class"] = "h5";

                sspan = new HtmlGenericControl("span");
                sspan.InnerHtml = "<b>$ " + tax + "</b><br/>";
                sspan.Attributes["class"] = "pull-right";

                ddiv = new HtmlGenericControl("div");
                ddiv.Attributes["class"] = "h5";

                ddiv.Controls.Add(sspan);
                ddiv.Controls.Add(pp);
                maindiv.Controls.Add(ddiv);

                double total = subtotal + tax + shipping;

                // Total
                pp = new HtmlGenericControl("span");
                pp.InnerHtml = "<b>Total</b>";
                pp.Attributes["class"] = "h5";

                sspan = new HtmlGenericControl("span");
                sspan.InnerHtml = "<b>$ " + total.ToString() + "</b>";
                sspan.Attributes["class"] = "pull-right";

                ddiv = new HtmlGenericControl("div");
                ddiv.Attributes["class"] = "h5";

                ddiv.Controls.Add(sspan);
                ddiv.Controls.Add(pp);
                maindiv.Controls.Add(ddiv);

                Products.Controls.Add(maindiv);
            }
        }

        protected void Checkoutt_Click(object sender, EventArgs e)
        {
            if (cartitems == null)
            {
                db = new BetaDB();
                var email = Session["email"].ToString();
                userId = db.Logins.Where(x => x.Email == email).Select(x => x.ID).FirstOrDefault();
                cartitems = db.Carts.Where(x => x.UserId == userId).ToList<Cart>();
            }
            List<PaypalItem> approvedItems = new List<PaypalItem>();
            foreach (Cart ci in cartitems)
            {
                var product = db.Products.Where(x => x.ProductID == ci.ProductID).SingleOrDefault();
                var stock = db.Stocks.Where(x => x.ProductID == ci.ProductID && x.ProductSizeName == ci.Size).FirstOrDefault();
                if (stock.StockCount < ci.Quantity)
                {
                    outofstocklbl.Text = "Some Products are Out of Stock";
                }
                else
                {
                    PaypalItem pi = new PaypalItem
                    {
                        ProductId = product.ProductID,
                        name = product.ProductName,
                        currency = "NZD",
                        price = Convert.ToDouble(product.ProductPrice),
                        quantity = (int)ci.Quantity,
                        size = ci.Size,
                        sku = product.ProductID.ToString()
                    };
                    approvedItems.Add(pi);
                }
            }

            PaymentHandler ph = new PaymentHandler(approvedItems,userId);
        }

        protected void GotoProfile_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("/Private/UserProfile.aspx");
        }
    }
}