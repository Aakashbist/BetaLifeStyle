using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BetaLifeStyle.Private
{
    public partial class Product : System.Web.UI.Page
    {
        BetaDB beta = new BetaDB();
        static int productId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["UserRole"] == "SuperAdmin" || (string)Session["UserRole"] == "Admin")
            {
                
            }
            else
            {
                Response.Redirect("/Home?Login=true");
            }

            if (!IsPostBack)
            {
                DrpData();
                showData();
            }
        }

        private void DrpData()
        {

            drpSubCat.DataSource = drpCat.DataSource = null;
            try
            {
                drpCat.DataSource = beta.ProductCategories.ToList();
                drpCat.DataTextField = "ProductCatName";
                drpCat.DataValueField = "ProductCatID";
                drpCat.DataBind();
            }
            catch (Exception drp) { throw drp; }

        }

        protected void drpCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilldropDown();
        }

        private void FilldropDown()
        {
            int __id = Convert.ToInt32(drpCat.SelectedValue);

            drpSubCat.DataSource = null;
            drpSubCat.Items.Clear();
            drpSubCat.Items.Insert(0, "Select Sub Categories");

            if (__id != 0)
            {
                drpSubCat.DataSource = (from sc in beta.ProductSubCategories where sc.ProductCatID.Equals(__id) select sc).ToList();
                drpSubCat.DataTextField = "ProductSubCatName";
                drpSubCat.DataValueField = "ProductSubCatID";
            }
            drpSubCat.DataBind();
        }

        private void showData()
        {
            var qry = (from pr in beta.Products
                       join pc in beta.ProductCategories on pr.ProductCatID equals pc.ProductCatID
                       join ps in beta.ProductSubCategories on pr.ProductSubCatID equals ps.ProductSubCatID

                       orderby pc.ProductCatID
                       select new
                       {
                           ID = pr.ProductID,
                           Categories = pc.ProductCatName,
                           SubCat = ps.ProductSubCatName,
                           ProductName = pr.ProductName,
                           BrandName = pr.ProductBrandName,
                           Price = pr.ProductPrice,
                           Color = pr.ProductColor,
                           Description = pr.ProductDesc,
                       }).ToList();
            GrdProduct.DataSource = qry;
            GrdProduct.DataBind();
        }

        private void InsertStock(int proId)
        {
            int drpSubcatvalue = Convert.ToInt32(drpSubCat.SelectedValue);
            var sizelistforstock = beta.ProductSizes.Where(s => s.ProductSubCatID.Equals(drpSubcatvalue)).ToList();
            foreach (var i in sizelistforstock)
            {
                Stock insertstock = new Stock()
                {
                    ProductID = proId,
                    StockCount = 0,
                    ProductSizeName = i.ProductSizeName
                };
                beta.Stocks.Add(insertstock);
                beta.SaveChanges();
            }
        }

        private void InsertProductImage(int proId)
        {
            if (ProductImageUpload.PostedFiles != null)
            {
                foreach (HttpPostedFile upldImage in this.ProductImageUpload.PostedFiles)
                {
                    string Imagename = "ProImg_" + proId + "_" + System.DateTime.Now.Ticks.ToString() + Path.GetExtension(ProductImageUpload.FileName);
                    upldImage.SaveAs(Path.Combine(Server.MapPath("~/ProductImage/") + Imagename));

                    ProductImage InsImage = new ProductImage()
                    {
                        ProductID = proId,
                        ProductImageName = Imagename,
                        ProductImagePath = Imagename
                    };
                    beta.ProductImages.Add(InsImage);
                    beta.SaveChanges();

                }
            }
        }

        protected void GrdProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ///HiddenField hdnUserID = e.Row.FindControl("PRoID") as HiddenField;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int id = Convert.ToInt32(e.Row.Cells[0].Text);
                if (id != 0)
                {
                    //int uid = Convert.ToInt32(hdnUserID.Value);
                    Repeater imageRep = e.Row.FindControl("RepEx") as Repeater;
                    imageRep.DataSource = (from im in beta.ProductImages
                                           where im.ProductID.Equals(id)
                                           select new
                                           {
                                               IDM = im.ProductImageID,
                                               ProImgName = im.ProductImageName,
                                               Source = im.ProductImagePath
                                           }).ToList();
                    imageRep.DataBind();

                    GridView StockList = e.Row.FindControl("StockList") as GridView;
                    StockList.DataSource = beta.Stocks.Where(x => x.ProductID.Equals(id)).ToList();
                    StockList.DataBind();

                }
                else
                {
                    return;
                }
            }
        }

        protected void btnProduct_Click1(object sender, EventArgs e)
        {
            try
            {
                if (productId == 0)
                {
                    BetaLifeStyle.Product InsProduct = new BetaLifeStyle.Product()
                    {
                        ProductBrandName = txtProBrand.Text,
                        ProductName = txtProName.Text,
                        ProductColor = drpColor.SelectedValue.ToString(),
                        ProductDesc = txtProDec.Text,

                        ProductPrice = Convert.ToDecimal(txtProPrice.Text),
                        ProductAddDate = System.DateTime.Now.Date,
                        ProductSubCatID = Convert.ToInt32(drpSubCat.SelectedValue),

                        ProductCatID = Convert.ToInt32(drpCat.SelectedValue)
                    };
                    beta.Products.Add(InsProduct);
                    beta.SaveChanges();
                    int ProId = InsProduct.ProductID;
                    InsertProductImage(ProId);
                    InsertStock(ProId);

                }
                else
                {
                    BetaLifeStyle.Product editpro = beta.Products.FirstOrDefault(ed => ed.ProductID.Equals(productId));
                    editpro.ProductBrandName = txtProBrand.Text;
                    editpro.ProductName = txtProBrand.Text;
                    editpro.ProductColor = drpColor.SelectedValue.ToString();
                    editpro.ProductDesc = txtProDec.Text;

                    editpro.ProductPrice = Convert.ToDecimal(txtProPrice.Text);
                    //editpro.ProductAddDate = System.DateTime.Now.Date;
                    editpro.ProductSubCatID = Convert.ToInt32(drpSubCat.SelectedValue);

                    editpro.ProductCatID = Convert.ToInt32(drpCat.SelectedValue);
                    beta.SaveChanges();

                    if (ProductImageUpload.PostedFiles != null)
                    {
                        InsertProductImage(productId);

                    }

                }
                showData();
                clearData();


            }
            catch (Exception pr)
            {
                throw pr;
            };


        }


        private void clearData()
        {
            txtProBrand.Text = txtProDec.Text = txtProName.Text = txtProPrice.Text = null;
            DrpData();
            productId = 0;
            Response.Redirect(Request.RawUrl);
        }

        protected void GrdProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            if (e.CommandName.ToString().ToUpper() == "SELECT")
            {
                productId = Convert.ToInt32(e.CommandArgument);

                var selproduct = beta.Products.FirstOrDefault(p => p.ProductID == productId);
                txtProBrand.Text = selproduct.ProductBrandName;
                txtProDec.Text = selproduct.ProductDesc;
                txtProName.Text = selproduct.ProductName;
                txtProPrice.Text = selproduct.ProductPrice.ToString();
                drpCat.SelectedValue = selproduct.ProductCatID.ToString();
                FilldropDown();
                drpSubCat.SelectedValue = selproduct.ProductSubCatID.ToString(); ;
                drpColor.SelectedValue = selproduct.ProductColor.ToString();

                repEditImage();
            }
            if (e.CommandName.ToString().ToUpper() == "SAVE")
            {
                productId = Convert.ToInt32(e.CommandArgument);

                Control ctrl = e.CommandSource as Control;

                if (ctrl != null)

                {
                    string c = "10+20";
                    //int a = Convert.ToInt32(c);
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

                    GridView stocklist = row.FindControl("StockList") as GridView;
                    foreach (GridViewRow gr in stocklist.Rows)
                    {
                        HiddenField HdnStockID = gr.FindControl("StockID") as HiddenField;
                        if (HdnStockID != null)
                        {

                            int StockID = Convert.ToInt32(HdnStockID.Value);
                            TextBox stockchage = gr.FindControl("txtStock") as TextBox;

                            Label lblstock = gr.FindControl("lblStock") as Label;

                            int Stock = Convert.ToInt32(lblstock.Text);
                            DropDownList DropdownOperator = gr.FindControl("DrpOperator") as DropDownList;
                            if (DropdownOperator.SelectedValue == "P")
                            {
                                Stock += Convert.ToInt32(stockchage.Text);
                            }
                            else
                            {

                                Stock -= Convert.ToInt32(stockchage.Text);
                                if (Stock < 0)
                                {
                                    Stock = 0;
                                }
                            }
                            Stock EditStock = beta.Stocks.FirstOrDefault(x => x.StockID.Equals(StockID));
                            EditStock.StockCount = Stock;
                            beta.SaveChanges();

                        }
                    }
                    stocklist.DataSource = beta.Stocks.Where(x => x.ProductID.Equals(productId)).ToList();
                    stocklist.DataBind();

                }

            }
        }


        private void repEditImage()
        {
            repImage.DataSource = (from im in beta.ProductImages
                                   where im.ProductID.Equals(productId)
                                   select new
                                   {
                                      IDM = im.ProductImageID,
                                       ProImgName = im.ProductImageName,
                                       Source = im.ProductImagePath
                                   }).ToList();
            repImage.DataBind();
            repImage.Visible = true;

        }

        protected void repImage_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int delid = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.ToString().ToUpper() == "DELETE")
            {
                ProductImage delimage = beta.ProductImages.FirstOrDefault(d => d.ProductImageID.Equals(delid));
                beta.ProductImages.Remove(delimage);
                beta.SaveChanges();
                repEditImage();


            }

        }

        protected void GrdProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int a = Convert.ToInt32(GrdProduct.DataKeys[0].Value);
            var delimage = beta.ProductImages.Where(d => d.ProductID.Equals(a)).ToList();
            beta.ProductImages.RemoveRange(delimage);
            BetaLifeStyle.Product delproduct = beta.Products.SingleOrDefault(d => d.ProductID == a);
            beta.Products.Remove(delproduct);
            beta.SaveChanges();
            showData();
            clearData();
        }

        protected void GrdProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            showData();
            GrdProduct.PageIndex = e.NewPageIndex;
            GrdProduct.DataBind();

        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {

        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            var SearchList = (from pr in beta.Products
                              join pc in beta.ProductCategories on pr.ProductCatID equals pc.ProductCatID
                              join ps in beta.ProductSubCategories on pr.ProductSubCatID equals ps.ProductSubCatID
                              where pr.ProductName.Contains(txtSearch.Text.ToString().Trim() == null ? pr.ProductName : txtSearch.Text.ToString().Trim())
                              orderby pc.ProductCatID
                              select new
                              {
                                  ID = pr.ProductID,
                                  Categories = pc.ProductCatName,
                                  SubCat = ps.ProductSubCatName,
                                  ProductName = pr.ProductName,
                                  BrandName = pr.ProductBrandName,
                                  Price = pr.ProductPrice,
                                  Color = pr.ProductColor,
                                  Description = pr.ProductDesc,
                              }).ToList();

            GrdProduct.DataSource = SearchList;
            GrdProduct.DataBind();
        }
    }
}