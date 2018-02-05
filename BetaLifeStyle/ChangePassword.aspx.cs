using BetaLifeStyle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BetaLifeStyle
{
    public partial class ChangePassword1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var db = new BetaDB();

            //if (Request.QueryString["Id"] == null && Session["email"] == null)
            //{
            //    ErrorMessage.Visible = true;
            //    ltMessage.Text = "Broken Link !";
            //    lblMessagedesc.Text = "Varify link has Experied or Invalid. Please click here to activate account";
            //}
            if (!IsPostBack)
                try
                {
                    {
                        if (Request.QueryString["Id"] != null)
                        {
                            var uniqueId = Guid.Parse(Request.QueryString["Id"]);
                            var Isvalid = db.GUIDs.Where(x => x.UniqueId.Equals(uniqueId)).SingleOrDefault();

                            if (Isvalid != null && Isvalid.Purpose == "Reset")
                            {
                                ResetPassword.Visible = true;
                                ChangePassword.Visible = false;
                            }
                            else
                            {
                                ErrorMessage.Visible = true;
                                ltMessage.Text = "Broken Link !";
                                lblMessagedesc.Text = "Varify link has Experied or Invalid. Please click here to activate account";
                            }
                        }
                        else if (Session["email"] != null)
                        {

                            ResetPassword.Visible = false;
                            ChangePassword.Visible = true;
                        }
                        else
                        {
                            ErrorMessage.Visible = true;
                            ltMessage.Text = "Broken Link !";
                            lblMessagedesc.Text = "Varify link has Experied or Invalid";
                        }
                    }
                }
                catch
                {
                    return;
                }
        }
    }
}





