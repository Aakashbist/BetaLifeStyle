using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using BetaLifeStyle.Helper;

namespace BetaLifeStyle.Account
{
    public partial class Register : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
         
        }

        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            using (var db = new BetaDB())
            {
                

                if (db.Logins.Count((a) => a.Email ==TxtEmail.Text)>0)
                {
                    lblMessage.Text = "sorry";

                }
                else
                {
                    var encryptedpassword = HelpUs.Encrypt(TxtPassword.Text);
                    Login lg = new Login
                    {
                        Email = TxtEmail.Text,
                        Password = encryptedpassword
                    };
                    db.Logins.Add(lg);
                    db.SaveChanges();
                }

            }
            }
        
    }
}