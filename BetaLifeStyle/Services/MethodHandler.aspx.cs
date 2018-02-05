using BetaLifeStyle.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BetaLifeStyle.Services
{
    public partial class MethodHandler : System.Web.UI.Page
    {
        const string Domain = "http://betastyle-001-site1.gtempurl.com"; // "http://localhost:44394";// 
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        [WebMethod]
        public static string register(string email, string password)
        {
            try
            {
                var db = new BetaDB();

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrWhiteSpace(email))
                {
                    var emailexist = db.Logins.FirstOrDefault(x => x.Email == email);


                    if (emailexist == null)
                    {
                        // checking email format
                        bool valid = email.ValidateEmail();
                        if (valid == true)
                        {
                            Login l = new Login();

                            // for password requirement
                            string validpassword = password.ValidatePassword();

                            if (validpassword == "true")

                            {
                                l.Email = email;

                                l.Password = HelpUs.Encrypt(password);
                                l.UserRole = "User";
                                l.CreatedOn = DateTime.Now;
                                l.IsActive = false;
                                l.Islocked = false;

                                db.Logins.Add(l);
                                db.SaveChanges();

                                //to send activation email and save data in GUID data
                                string purpose = "Activation";
                                GUID UniqueCode = NewGuid(l, purpose);

                                string url = HttpContext.Current.Server.HtmlEncode(Domain + "/Activation/" + UniqueCode.UniqueId);
                                Sendemail(email, purpose, url);

                                Dictionary<string, string> dic = new Dictionary<string, string>();
                                dic.Add("Error", "0");
                                dic.Add("Message", "User Registered Successfully");
                                return JsonConvert.SerializeObject(dic);
                            }
                            else
                            {
                                Dictionary<string, string> dic = new Dictionary<string, string>();
                                dic.Add("Error", "1");
                                dic.Add("Message", "Password Must Contain Uppercase,Lowercase,Character and 8 Digits");
                                return JsonConvert.SerializeObject(dic);
                            }
                        }
                        else
                        {
                            Dictionary<string, string> dic = new Dictionary<string, string>();
                            dic.Add("Error", "1");
                            dic.Add("Message", "Email is Invalid");
                            return JsonConvert.SerializeObject(dic);
                        }
                    }
                    else
                    {
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("Error", "1");
                        dic.Add("Message", "User Already Exists");
                        return JsonConvert.SerializeObject(dic);
                    }
                }

                else
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("Error", "1");
                    dic.Add("Message", "Email and Password Required");
                    return JsonConvert.SerializeObject(dic);
                }
            }

            catch(Exception e)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("Error", "1");
                dic.Add("Message", "Server Error");
                return JsonConvert.SerializeObject(dic);
            }
        }
        
        [WebMethod]
        public static string login(string email, string password)
        {
            using (var db = new BetaDB())
            {
                if (!string.IsNullOrEmpty(email) && !string.IsNullOrWhiteSpace(email))
                {

                    var login = db.Logins.SingleOrDefault(x => x.Email == email);
                    string Password = HelpUs.Encrypt(password);
                    if (login != null && login.Password == Password)
                    {
                        //check if isemail varified
                        if (login.IsActive == true)
                        {
                            HttpContext.Current.Session["email"] = login.Email;
                            HttpContext.Current.Session["UserRole"] = login.UserRole;

                            CartHandler carthandler = new CartHandler();
                            carthandler.SyncCookieDatainDb();

                            Dictionary<string, string> dic = new Dictionary<string, string>();
                            dic.Add("Error", "0");
                            dic.Add("Message", "Login Successfull ");
                            dic.Add("RedirectUrl", "/Home");
                            return JsonConvert.SerializeObject(dic);

                        }
                        else
                        {
                            // To send a activation email if user forget to activate when register

                            var guidId = db.GUIDs.FirstOrDefault(f => f.UserId == login.ID && f.Purpose == "Activation");

                            if (login.IsActive == false)
                            {
                                string purpose = "Activation";
                                if (guidId == null)
                                {
                                    guidId = NewGuid(login, purpose);
                                }
                                string url = HttpContext.Current.Server.HtmlEncode(Domain + "/Activation/" + guidId.UniqueId);
                                Sendemail(email, guidId.Purpose, url);
                            }
                            else if (guidId != null && guidId.UserId != null && guidId.Purpose == "Activation")
                            {

                                string url = HttpContext.Current.Server.HtmlEncode(Domain + "/Activation/" + guidId.UniqueId);
                                Sendemail(email, guidId.Purpose, url);
                            }
                            // guid is null

                            Dictionary<string, string> dic = new Dictionary<string, string>();
                            dic.Add("Error", "1");
                            dic.Add("Message", "Verify your Email first ");
                            return JsonConvert.SerializeObject(dic);
                        }
                    }
                    else
                    {
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("Error", "1");
                        dic.Add("Message", "Username or Password Do not Match");
                        dic.Add("RedirectUrl", "/Home");
                        return JsonConvert.SerializeObject(dic);
                    }
                }
                else
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("Error", "1");
                    dic.Add("Message", "Email and Password Required");
                    return JsonConvert.SerializeObject(dic);

                }
            }
        }

        //To send email
        // url is click url or my be some other details or message
        public static bool Sendemail(string email, string purpose, string url)
        {
            try
            {

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("bistaakash123@gmail.com", "Beta LifeStyle");
                mailMessage.To.Add(email);
                mailMessage.Subject = GetEmailSubject(purpose);

                mailMessage.Body = GetEmailBody(email, purpose, url);
                mailMessage.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("betagymstyle@gmail.com", "B3t4gymstyl3");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mailMessage);
                return true;


            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool SendCustomEmail(string email, string body, string subject)
        {
            try
            {

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("bistaakash123@gmail.com", "Beta LifeStyle");
                mailMessage.To.Add(email);
                mailMessage.Subject = subject;

                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("betagymstyle@gmail.com", "B3t4gymstyl3");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mailMessage);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [WebMethod]
        public static string SendForgetPasswordLink(string email)
        {
            BetaDB db = new BetaDB();
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrWhiteSpace(email))
            {
                if (IsUserExist(email))
                {
                    string purpose = "Reset";
                    var user = GetUserByEmail(email);
                    var guidId = db.GUIDs.FirstOrDefault(f => f.UserId == user.ID && f.Purpose == purpose);
                    if (guidId == null)
                    {
                        guidId = NewGuid(user, purpose);
                        string url = HttpContext.Current.Server.HtmlEncode(Domain + "/Reset/" + guidId.UniqueId);
                        Sendemail(email, guidId.Purpose, url);
                    }
                    else if (guidId != null && guidId.UserId != null && guidId.Purpose == "Reset")
                    {
                        string url = HttpContext.Current.Server.HtmlEncode(Domain + "/Reset/" + guidId.UniqueId);
                        Sendemail(email, guidId.Purpose, url);
                    }

                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("Error", "0");
                    dic.Add("Message", "Check your email to reset your password. ");
                    return JsonConvert.SerializeObject(dic);

                }
                else
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("Error", "1");
                    dic.Add("Message", "User does not exist");
                    return JsonConvert.SerializeObject(dic);
                }
            }
            else
            {

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("Error", "1");
                dic.Add("Message", "Email and Password Required");
                return JsonConvert.SerializeObject(dic);
            }
        }

        public static string GetEmailBody(string email, string purpose, string url)
        {
            string body = "";
            switch (purpose)
            {
                case "Activation":
                    body = "Hello " + email + ",";
                    body += "<br /><br />Please click the following link to activate your account";
                    body += "<a href='" + url + "'>Click Here to verify your acount</a>";

                    body += "<br /><br />Thanks";
                    body += "<br /><br />Beta LifeStyle";
                    return body;

                case "Reset":
                    body = "Hello " + email + ",";
                    body += "<br /><br />Please click the following link to To Reset your Password";
                    body += "<a href='" + url + "'>Click Here to Rest your Password</a>";

                    body += "<br /><br />Thanks";
                    body += "<br /><br />Beta LifeStyle";
                    return body;
                case "OrderSuccess":

                    body = "Hello " + email + ",";
                    body += "<br /><br />We Got your Order and it will process soon!";
                    body += "<br/>You can always log back in and check your order history.";
                    body += "<br/><br/>Your Invoice is "+url;

                    body += "<br /><br /><br />Thanks";
                    body += "<br />Beta LifeStyle";
                    return body;
                default:
                    return "";
            }

        }

        public static string GetEmailSubject(string purpose)
        {
            switch (purpose)
            {
                case "Activation":
                    return "Beta LifeStyle Account Activation";

                case "Reset":
                    return "Beta LifeStyle Password Reset";
                case "OrderSuccess":
                    return "Thank you for Ordering on Beta Lifestyle";
                default:
                    return "";
            }

        }

        public static bool IsUserExist(string email)
        {
            var db = new BetaDB();
            var user = db.Logins.Where(d => d.Email.Equals(email)).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static Login GetUserByEmail(string email)
        {
            var db = new BetaDB();
            var userId = db.Logins.Where(d => d.Email.Equals(email)).FirstOrDefault();
            return userId;
        }

        [WebMethod]
        public static string forgetpassword(string password, string uniqueId)
        {
            var db = new BetaDB();
            string validpassword = password.ValidatePassword();
            if (validpassword == "true")
            {

                var uuniqueId = Guid.Parse(uniqueId);
                var IsValid = db.GUIDs.Where(x => x.UniqueId.Equals(uuniqueId)).FirstOrDefault();

                Login lg = db.Logins.Find(IsValid.UserId);
                lg.Password = HelpUs.Encrypt(password);
                db.Entry(lg).State = System.Data.Entity.EntityState.Modified;
                var deleteGUID = db.GUIDs.SingleOrDefault(x => x.UniqueId.Equals(IsValid.UniqueId));
                db.Entry(deleteGUID).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("Error", "0");
                dic.Add("Message", "Password reset successfully");
                return JsonConvert.SerializeObject(dic);

            }
            else
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("Error", "1");
                dic.Add("Message", "server error");
                return JsonConvert.SerializeObject(dic);
            }
        }

        [WebMethod]
        public static string ChangePassword(string password, string newpassword)
        {
            string email = HttpContext.Current.Session["email"].ToString();
            var db = new BetaDB();
            var oldpassword = HelpUs.Encrypt(password);
            var newPassword = HelpUs.Encrypt(newpassword);
            var User = db.Logins.Where(x => x.Email == email).FirstOrDefault();
            if (User.Password != oldpassword)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("Error", "1");
                dic.Add("Message", "Old Password did not match");
                return JsonConvert.SerializeObject(dic);
            }
            else if (User.Password == newPassword)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("Error", "1");
                dic.Add("Message", "New Password Cannot be same as before");
                return JsonConvert.SerializeObject(dic);
            }


            else
            {
                string validpassword = newpassword.ValidatePassword();
                if (validpassword == "true")
                {
                    User.Password = HelpUs.Encrypt(newpassword);
                    //db.Entry(lg).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("Error", "0");
                    dic.Add("Message", "Password Changed successfully");
                    return JsonConvert.SerializeObject(dic);
                }
                else
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("Error", "1");
                    dic.Add("Message", "Password Must Contain Uppercase,Lowercase,Character and 8 Digits");
                    return JsonConvert.SerializeObject(dic);
                }
            }



        }

        public static GUID NewGuid(Login login, string _purpose)
        {
            BetaDB db = new BetaDB();
            Guid UniqueCode = Guid.NewGuid();
            GUID guid = new GUID()
            {
                UniqueId = UniqueCode,
                UserId = login.ID,
                CreatedOn = DateTime.Now,
                Purpose = _purpose
            };
            db.GUIDs.Add(guid);
            db.SaveChanges();
            return guid;
        }

    }
}

