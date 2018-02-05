using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Data;

namespace BetaLifeStyle.Private
{
    public partial class ViewProducts : System.Web.UI.Page
    {
        BetaDB db = new BetaDB();

        string category, subcategory, searchterm;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                try
                {
                    // Search/{SearchTerm}
                    searchterm = Page.RouteData.Values["SearchTerm"].ToString();
                    Console.Write("Search term=" + searchterm);
                    string[] keywords = searchterm.Split(' ');
                    productrepeater.DataSource = db.Products.Where(q => q.ProductDesc.Contains(searchterm) || q.ProductName.Contains(searchterm)).ToList();
                    productrepeater.DataBind();
                    return;
                }
                catch { }

                try
                {
                    // View/{Category}/{SubCategory}
                    subcategory = Page.RouteData.Values["SubCategory"].ToString();

                    category = Page.RouteData.Values["Category"].ToString();
                    Console.Write("Category =" + category + "    Subcategory=" + subcategory);

                    int productsubcategoryid = db.ProductSubCategories.SingleOrDefault(x => x.ProductSubCatName == subcategory).ProductSubCatID;
                    productrepeater.DataSource = db.Products.Where(x => x.ProductSubCatID == productsubcategoryid).ToList(); ;
                    productrepeater.DataBind();
                    return;
                }
                catch { }

                try
                {
                    // View/{Category}
                    category = Page.RouteData.Values["Category"].ToString();
                    Console.Write("Category=" + category);
                    int productcategoryid = db.ProductCategories.SingleOrDefault(x => x.ProductCatName == category).ProductCatID;
                    productrepeater.DataSource = db.Products.Where(x => x.ProductCatID == productcategoryid).ToList();
                    productrepeater.DataBind();
                    return;
                }
                catch { }


                // for -> Home?Login=true -> open login modal
                try
                {
                    string toopenlogin = Request.QueryString["Login"].ToString();

                    if (toopenlogin.ToLower().Equals("true"))
                    {
                        // Go to Login 
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "OpenLoginModel",
                            "<script type='text/javascript'>function OpenLoginModel(){$('#loginModal').modal('show');}; OpenLoginModel()</script>", false);
                    }
                }
                catch (Exception ee){ }

                productrepeater.DataSource = db.Products.ToList();
                productrepeater.DataBind();
            }
            
        }



        static int limit = 20;
        //DOnt change here
        [WebMethod]
        public static string GetProducts(int page)
        {
            BetaDB db = new BetaDB();
            db.Configuration.ProxyCreationEnabled = false;
            try
            {
                var listt = db.Products.Select(x => new { Id = x.ProductID, Desciption = x.ProductDesc, Name = x.ProductName, Price = x.ProductPrice, BrandName = x.ProductBrandName, ImageUrl = x.ProductImages.FirstOrDefault() }).ToList().Skip(limit * (page - 1)).Take(limit);
                return new JavaScriptSerializer().Serialize(listt);
            }
            catch (Exception e)
            {
                return new JavaScriptSerializer().Serialize(new List<Product>());
            }
        }

        [WebMethod]
        public static string Search(string searchterm, int page)
        {
            BetaDB db = new BetaDB();
            db.Configuration.ProxyCreationEnabled = false;
            try
            {
                var listt = db.Products.Select(x => new { Id = x.ProductID, Desciption = x.ProductDesc, Name = x.ProductName, Price = x.ProductPrice, BrandName = x.ProductBrandName, ImageUrl = x.ProductImages.FirstOrDefault() }).Where(q => q.Desciption.Contains(searchterm) || q.Name.Contains(searchterm)).ToList().Skip(limit * (page - 1)).Take(limit);
                return new JavaScriptSerializer().Serialize(listt);
            }
            catch (Exception e)
            {
                return new JavaScriptSerializer().Serialize(new List<Product>());
            }
        }

        [WebMethod]
        public static string GetProductsBySubCategory(string category,string subcategory, int page)
        {
            BetaDB db = new BetaDB();
            db.Configuration.ProxyCreationEnabled = false;
            try
            {
                int productcategoryid = db.ProductCategories.SingleOrDefault(x => x.ProductCatName == category).ProductCatID;
                int productsubcategoryid = db.ProductSubCategories.SingleOrDefault(x => x.ProductSubCatName == subcategory && x.ProductCatID==productcategoryid).ProductSubCatID;
                var list = db.Products.Select(x => new { Id = x.ProductID, Desciption = x.ProductDesc, Name = x.ProductName, Price = x.ProductPrice, BrandName = x.ProductBrandName, ImageUrl = x.ProductImages.FirstOrDefault(), SubcategoryId = x.ProductSubCatID, CategoryId = x.ProductCatID }).Where(x => x.SubcategoryId == productsubcategoryid).ToList().Skip(limit * (page - 1)).Take(limit);
                return new JavaScriptSerializer().Serialize(list);
            }
            catch (Exception e)
            {
                return new JavaScriptSerializer().Serialize(new List<Product>());
            }
        }

        [WebMethod]
        public static string GetProductsByCategory(string category, int page)
        {
            BetaDB db = new BetaDB();
            db.Configuration.ProxyCreationEnabled = false;

            try
            {
                int productcategoryid = db.ProductCategories.SingleOrDefault(x => x.ProductCatName == category).ProductCatID;
                var list = db.Products.Where(x => x.ProductCatID == productcategoryid).ToList().Skip(limit * (page - 1)).Take(limit);
                return new JavaScriptSerializer().Serialize(list);
            }
            catch (Exception e)
            {
                return new JavaScriptSerializer().Serialize(new List<Product>());
            }

        }


        protected void productrepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            BetaDB db = new BetaDB();
            HiddenField hdnID = e.Item.FindControl("ProHdn") as HiddenField;
            int Id = Convert.ToInt32(hdnID.Value);
            var images = db.ProductImages.Where(im => im.ProductID.Equals(Id)).FirstOrDefault();

            Image img = e.Item.FindControl("proImg") as Image;
            string imgpath = "~/ProductImage/" + images.ProductImagePath.ToString();
            img.ImageUrl = imgpath;


        }


        public string GetProductCategories()
        {
            string categories = "";
            BetaDB db = new BetaDB();
            foreach (ProductCategory pc in db.ProductCategories.ToList())
            {
                string subcat = "";
                foreach (ProductSubCategory psc in pc.ProductSubCategories)
                {
                    subcat = subcat + psc.ProductSubCatName + ",";
                }
                if (subcat.Length > 1)
                {
                    subcat = subcat.Substring(0, subcat.Length - 1);
                    categories = categories + pc.ProductCatName + "|" + subcat + "#";
                }
                else
                {
                    categories = categories + pc.ProductCatName + "|#";
                }

            }
            if (categories.Length > 1)
            {
                categories = categories.Substring(0, categories.Length - 1);
            }
            return categories;
        }


    }
}