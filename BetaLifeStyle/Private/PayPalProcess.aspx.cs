using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayPal.Api;

namespace BetaLifeStyle.Private
{
    public partial class PayPalProcess : System.Web.UI.Page
    {
        public Dictionary<string, string> config;
        OAuthTokenCredential auth;

        // Payment Status == 0 then payment started
        //                == 1 them successful payment (but waitng for approval )
        //                == 2 payment recieved and order processing 
        //                == 3 order processed
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["email"] == null)
            {
                Response.Redirect("/Home?Login=true");
                Session.Clear();
            }

            try
            {
                var paymentId = Request.Params["paymentId"].ToString();
                var token = Request.Params["token"].ToString();
                var payerid = Request.Params["PayerID"].ToString();

                // Using the information from the redirect, setup the payment to execute.
                var paymentExecution = new PaymentExecution() { payer_id = payerid };
                var payment = new PayPal.Api.Payment() { id = paymentId };

                config = PayPal.Api.ConfigManager.Instance.GetProperties();
                auth = new OAuthTokenCredential(config);
                var apiContext = new APIContext(auth.GetAccessToken());

                BetaDB db = new BetaDB();

                string email = Session["email"].ToString();
                var login = db.Logins.Where(x => x.Email == email).FirstOrDefault();


                var executedPayment = payment.Execute(apiContext, paymentExecution);
                if (executedPayment.failed_transactions == null)
                {
                    List<Transaction> tl = executedPayment.transactions;
                    foreach(Transaction transaction in tl)
                    {
                        String invoice = transaction.invoice_number;
                        Payment pay = db.Payments.Where(x => x.InvoiceNo == invoice).SingleOrDefault();
                        pay.Status = 1;
                        ItemList itemlist = transaction.item_list;
                        foreach (Item i in transaction.item_list.items)
                        {
                            try
                            {
                                int id = Convert.ToInt16(i.sku);
                                int q = Convert.ToInt16(i.quantity);
                                string size = i.description;
                                Cart cartitem = db.Carts.Where(x => x.ProductID == id && x.UserId == login.ID && x.Quantity==q && x.Size==size).SingleOrDefault();
                                String _size = cartitem.Size;
                                Stock stock = db.Stocks.Where(pro => pro.ProductID == id && pro.ProductSizeName == size).SingleOrDefault();
                                stock.StockCount = stock.StockCount - Convert.ToInt16(i.quantity);
                                db.Carts.Remove(cartitem);
                                
                            }
                            catch(Exception er)
                            {

                            }
                            
                        }
                        // Send Email to user
                        Services.MethodHandler.Sendemail(Session["email"].ToString(), "OrderSuccess", transaction.invoice_number);
                        //
                        paymentlbl.Text = "We recieved you payment and your order is being processed";
                    }
                }
                db.SaveChanges();
            }
            catch(Exception error)
            {
                
            }
        }
    }
}