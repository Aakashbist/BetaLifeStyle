using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PayPal.Api;
using System.Configuration;

namespace BetaLifeStyle.Services
{
    public class PaymentHandler
    {
        // Authenticate with PayPal
        public Dictionary<string, string> config = ConfigManager.Instance.GetProperties();

        public string clientId { get; private set; }
        public string clientSecret { get; private set; }

        public PaymentHandler()
        {
            Dictionary<string, string> config = PayPal.Api.ConfigManager.Instance.GetProperties();
            OAuthTokenCredential auth = new OAuthTokenCredential(config);
            string accessToken = auth.GetAccessToken();
            var apiContext = new APIContext(accessToken);

            var payer = new Payer() { payment_method = "paypal" };

            var guid = Convert.ToString((new Random()).Next(100000));
            var redirUrls = new RedirectUrls()
            {
                cancel_url = "http://localhost:3000/cancel",
                return_url = "http://localhost:3000/process"
            };

            var itemList = new ItemList()
            {
                items = new List<Item>()
                  {
                    new Item()
                    {
                      name = "Item Name",
                      currency = "USD",
                      price = "15",
                      quantity = "5",
                      sku = "sku"
                    }
                  }
            };

            var details = new Details()
            {
                tax = "15",
                shipping = "10",
                subtotal = "75"
            };

            var amount = new Amount()
            {
                currency = "USD",
                total = "100.00", // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Transaction description.",
                invoice_number = "123",
                amount = amount,
                item_list = itemList
            });

            var payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                redirect_urls = redirUrls,
                transactions = transactionList
            };

            var createdPayment = payment.Create(apiContext);

            var links = createdPayment.links.GetEnumerator();
            while (links.MoveNext())
            {
                var link = links.Current;
                if (link.rel.ToLower().Trim().Equals("approval_url"))
                {
                    // REDIRECT USER TO link.href
                    HttpContext.Current.Response.Redirect(link.href);
                    //
                }
            }

           
        }

    }

}


   
        