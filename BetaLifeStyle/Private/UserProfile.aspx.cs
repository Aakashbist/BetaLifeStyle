using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BetaLifeStyle
{
    public partial class UserProfile : System.Web.UI.Page
    {
        BetaDB db = new BetaDB();
        int id;
        Login login;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["email"] != null)
                    {
                        var email = Session["email"].ToString();
                        login = db.Logins.Where(x => x.Email == email).FirstOrDefault();
                        txt_Email.Text = email;
                        loaddata(id);
                    }
                    else
                    {
                        Response.Redirect("/");
                    }
                }
                catch
                {
                    Response.Redirect("/");
                }
            }
        }
        private void loaddata(int id)
        {
            try
            {
                var email = Session["email"].ToString();
                login = db.Logins.Where(x => x.Email == email).FirstOrDefault();
                var profile = db.Profiles.SingleOrDefault(x => x.UserId == login.ID);
                Label3.Text = profile.FirstName.ToString();
                txt_Firstname.Text = profile.FirstName.ToString();
                txt_Lastname.Text = profile.LastName.ToString();
                txt_Address.Text = profile.Address.ToString();
                txt_City.Text = profile.City.ToString();
                txt_Country.Text = profile.Country.ToString();
                txt_Pincode.Text = profile.PinCode.ToString();
                txt_State.Text = profile.State.ToString();
                txt_Phone.Text = profile.Phone.ToString();
               // txt_Email.Text = email;
            }catch(Exception e)
            {

            }

        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/");

        }

        protected void abc_Click(object sender, EventArgs e)
        {
            Response.Redirect("/");
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                bool newprofile = false;
                var email = Session["email"].ToString();
                login = db.Logins.Where(x => x.Email == email).FirstOrDefault();
                Profile profile;
                try
                {
                    profile = db.Profiles.Where(x => x.UserId == login.ID).SingleOrDefault();
                }catch(Exception ee)
                {
                    profile = new Profile();
                    newprofile = true;
                }
                if(profile==null)
                {
                    profile = new Profile();
                    newprofile = true;
                }
                profile.FirstName = txt_Firstname.Text;
                profile.LastName = txt_Lastname.Text;
                profile.Address = txt_Address.Text;
                profile.City = txt_City.Text;
                profile.Country = txt_Country.Text;
                profile.PinCode = Convert.ToDecimal(txt_Pincode.Text).ToString();
                profile.State = txt_State.Text;
                profile.Phone = Convert.ToDecimal(txt_Phone.Text).ToString();
                profile.Login = login;
                if (newprofile)
                {
                    db.Profiles.Add(profile);
                }
                db.SaveChanges();
                loaddata(id);
            }
            catch (Exception ee) { }
        }
    }
 }