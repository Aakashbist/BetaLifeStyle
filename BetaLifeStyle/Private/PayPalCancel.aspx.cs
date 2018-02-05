using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BetaLifeStyle.Private
{
    public partial class PayPalCancel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["email"] == null)
            {
                Response.Redirect("/Home?Login=true");
                Session.Clear();
            }
        }
    }
}