using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BetaLifeStyle.Public
{
    public partial class Activationpage : System.Web.UI.Page
    {

        // In page load check for valid activation code and deleting it on successful activation
        public String IsGuidValid(string guid)
        {
            var db = new BetaDB();

            var uniqueId = Guid.Parse(Page.RouteData.Values["guid"].ToString());
            var IsValid = db.GUIDs.Where(x => x.UniqueId.Equals(uniqueId)).FirstOrDefault();
            if (IsValid == null)
            {

                return null;
            }
            else
            {
                var result = IsValid.Purpose;
                return result;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    var db = new BetaDB();
                    string guid = Page.RouteData.Values["guid"].ToString();
                    var uniqueId = Guid.Parse(guid);
                    var IsValid = db.GUIDs.Where(x => x.UniqueId.Equals(uniqueId)).FirstOrDefault();

                    if (IsValid != null && IsValid.Purpose == "Activation")
                    {
                        Login lg = db.Logins.Find(IsValid.UserId);
                        lg.IsActive = true;
                        db.Entry(lg).State = System.Data.Entity.EntityState.Modified;
                        var deleteGUID = db.GUIDs.SingleOrDefault(x => x.UniqueId.Equals(IsValid.UniqueId));
                        db.Entry(deleteGUID).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                        AccountActivation.Visible = true;
                        ltMessage.Text = "Account Activated Successfully!";
                    }
                    else
                    {
                        AccountActivation.Visible = true;
                        ltMessage.Text = "Broken Link !";
                        lblMessagedesc.Text = "Varify link has Experied or Invalid. Please click here to activate account";
                    }
                }
                catch
                {
                    return;
                }
            }
        }
    }
}