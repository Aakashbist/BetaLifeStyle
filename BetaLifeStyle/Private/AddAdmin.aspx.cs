﻿using BetaLifeStyle.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BetaLifeStyle
{
    public partial class AddAdmin : System.Web.UI.Page
    {
        BetaDB db = new BetaDB();
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["UserRole"] == "SuperAdmin")
            {
                
            }
            else
            {
                Response.Redirect("/Home");

            }
        }

     
        protected void Add_Click(object sender, EventArgs e)
        {

            string password = TxtRPassword.Text.ToString();
            string validpassword = password.ValidatePassword();
            if (validpassword == "true")
            {
                Login login = new Login()
                {
                    Email = TxtREmail.Text,
                    Password = Helper.HelpUs.Encrypt(password),
                    IsActive = true,
                    UserRole = "Admin"
                };
                db.Logins.Add(login);
                db.SaveChanges();
                lblmessage.ForeColor = System.Drawing.Color.Green;
                lblmessage.Text = "New Admin Created";
            }
            else
            {
                lblmessage.ForeColor = System.Drawing.Color.Red;
                lblmessage.Text = "Password Must Contain Uppercase,Lowercase,Character and 8 Digits";
            }

        }

    }
}
