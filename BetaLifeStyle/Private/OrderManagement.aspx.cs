using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BetaLifeStyle.Private
{
    public partial class OrderManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        //    if ((string)Session["UserRole"] == "SuperAdmin" || (string)Session["UserRole"] == "Admin")
        //    {
        //        return;

        //    }
        //    else
        //    {
        //        Response.Redirect("/Home");

        //    }
        }

        [WebMethod]
        public static string OrderDetail()
        {
            BetaDB db = new BetaDB();
            db.Configuration.ProxyCreationEnabled = false;
            var list = db.OrderDetails.Select(x => new {x.ProductId,x.Product.ProductName,x.Price,x.Login.Email,x.InvoiceNo,x.Quantity,Status=db.Payments.FirstOrDefault().Status}).ToList();
            return new JavaScriptSerializer().Serialize(list);
        }
    }
}