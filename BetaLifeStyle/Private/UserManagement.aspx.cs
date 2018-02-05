using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace BetaLifeStyle.Private
{
    public partial class UserManagement : System.Web.UI.Page
    {
        static BetaDB db = new BetaDB();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["UserRole"] == "SuperAdmin" || (string)Session["UserRole"] == "Admin")
            {
                return;
            }
            else
            {
                Response.Redirect("/Home?Login=true");
            }
        }

        [WebMethod]
        public static string Search(string searchterm)
        {
            BetaDB db = new BetaDB();
            db.Configuration.ProxyCreationEnabled = false;
            var listt = db.Logins.Where(x =>x.Email.Contains(searchterm) && x.UserRole == "User").Select(x=>new {x.ID, x.Email, x.UserRole,x.IsActive}).ToList();
            return new JavaScriptSerializer().Serialize(listt);
        }

        [WebMethod]
        public static string UserDetail()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var list = db.Logins.Where(x => x.UserRole == "User").Select(x => new { x.ID, x.Email, x.UserRole, x.IsActive }).ToList();
            return new JavaScriptSerializer().Serialize(list);
        }

        [WebMethod]
        public static bool IsActivate(int Id)
        {
            var user = db.Logins.Where(x => x.ID == Id).FirstOrDefault();
            if (user.IsActive == false) {
                user.IsActive = true;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            else
            {
                user.IsActive = false;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return false;
            }
          
           
        }
    }
}