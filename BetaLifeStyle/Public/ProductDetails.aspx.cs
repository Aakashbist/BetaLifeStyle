using BetaLifeStyle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace BetaLifeStyle.Public
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        public CartHandler carthandler = new CartHandler();
        CookieHandler cookiehandler { get; set; }
     
        int id;
        BetaDB db;
        Product product;
        bool productready;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new BetaDB();
            try
            {
                id = Convert.ToInt32(Page.RouteData.Values["id"].ToString());
                product =db.Products.SingleOrDefault(x => x.ProductID == id);
                ProductName.Text = product.ProductName;
                productready = true;
                CreatBreadCrumbs(product);
                if(product!=null)
                {
                    List<ProductImage> images = product.ProductImages.ToList();
                    int count = 0;
                    foreach(ProductImage image in images)
                    {
                        string path= "/ProductImage/"+image.ProductImagePath;
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        HtmlGenericControl img = new HtmlGenericControl("img");
                        
                        img.Attributes.Add("src", path);
                        img.Attributes.Add("Class", "productimagessmall");
                        img.Attributes.Add("id", "product_images_" + count);
                        ProductImagesContainer.Controls.Add(img);
                        count++;
                    }

                    if (!IsPostBack)
                    {
                  

                        Sizes.DataSource = ProductHandler.GetProductSizesFromProduct(product);
                        Sizes.DataTextField = "ProductSizeName";
                        Sizes.DataValueField = "ProductSizeID";
                        Sizes.DataBind();
                    }
                    string mainpath = "/ProductImage/" + images.ElementAt(0).ProductImagePath;
                    MainImage.Attributes.Add("src", mainpath);
                }
            }
            catch { product = new Product(); }
        }

        public Product GetCurrentProduct()
        {
            if(productready)
            {
                return product;
            }
            else
            {
                return null;
            }
        }

        private void CreatBreadCrumbs(Product product)
        {
            try
            {
                ProductCategory pc = ProductHandler.GetProductCategoryByProduct(product);
                HtmlGenericControl li = new HtmlGenericControl("li");
                HtmlGenericControl a = new HtmlGenericControl("a");
                a.Attributes.Add("href", "/View/" + pc.ProductCatName);
                a.InnerText = pc.ProductCatName.ToUpper();
                li.Attributes.Add("class", "breadcrumb-item");
                li.Controls.Add(a);
                ProductBreadCrumbs.Controls.Add(li);

                ProductSubCategory ps = ProductHandler.GetProductSubCategoryByProduct(product);
                HtmlGenericControl li2 = new HtmlGenericControl("li");
                HtmlGenericControl a2 = new HtmlGenericControl("a");
                a2.Attributes.Add("href", "/View/" + pc.ProductCatName + "/" +ps.ProductSubCatName);
                a2.InnerText = ps.ProductSubCatName.ToUpper();
                li2.Attributes.Add("class", "breadcrumb-item");
                li2.Controls.Add(a2);
                ProductBreadCrumbs.Controls.Add(li2);

                HtmlGenericControl li3 = new HtmlGenericControl("li");
                li3.InnerText = product.ProductName.ToUpper();
                li3.Attributes.Add("class", "breadcrumb-item-active");
                ProductBreadCrumbs.Controls.Add(li3);
            }
            catch (Exception e) {
                ProductBreadCrumbs.Controls.Clear();
                ProductBreadCrumbs.InnerText="Error";
            }
        }


        // for cart 
        protected void btnCart_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(Page.RouteData.Values["id"].ToString());
            string size = Sizes.SelectedItem.Text;
            var quantity = 1;
            carthandler.AddtoCart(id, size, quantity);
           
            Response.Redirect(Request.Url.ToString());
        }

        protected void btnBuy_Click(object sender, EventArgs e)
        {
            if (Session["email"] == null)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Login before proceeding further";
            }
        }
        
    }
}