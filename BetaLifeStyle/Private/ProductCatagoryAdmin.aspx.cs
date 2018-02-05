using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BetaLifeStyle.Private
{
    public partial class ProductCat : System.Web.UI.Page
    {
        static BetaDB beta = new BetaDB();
        private static int catId = 0;
        private static int SubcatId = 0;
        private static int SizeID = 0;
        private static int count = 0;

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

            if (!IsPostBack)
            {
                GetDrpCat();
            }
        }
        public void GetDrpCat()
        {
            try
            {

                drpShowSubSizeCat.Items.Clear(); drpSubCatsize.Items.Clear(); drpCat.Items.Clear();
                drpSubCatsize.Items.Insert(0, new ListItem("Select SubCategories", "0"));
                drpShowSubSizeCat.Items.Insert(0, new ListItem("Select SubCategories", "0"));
                drpCat.Items.Insert(0, new ListItem("Select Categories", "0"));

                drpCat.DataSource = beta.ProductCategories.ToList();
               drpCat.DataTextField = "ProductCatName";
                drpCat.DataValueField = "ProductCatID";

                drpShowSubSizeCat.DataSource = drpSubCatsize.DataSource = beta.ProductSubCategories.ToList();
                drpShowSubSizeCat.DataTextField = drpSubCatsize.DataTextField = "ProductSubCatName";
                 drpShowSubSizeCat.DataValueField = drpSubCatsize.DataValueField = "ProductSubCatID";


                drpCat.DataBind();
                drpShowSubSizeCat.DataBind();
                drpSubCatsize.DataBind();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        protected void btnCatSubmit_Click(object sender, EventArgs e)
        {
            lblError.Visible = true;
            if (catId == 0)
            {
                checkCat(txtCatName.Text.ToString().ToUpper().Trim());
                if (count == 0)
                {
                    ProductCategory cat = new ProductCategory()
                    {
                        ProductCatName = txtCatName.Text.ToString().ToUpper().Trim()
                    };
                    beta.ProductCategories.Add(cat);

                    beta.SaveChanges();

                    lblError.Text = "Category Added";
                    GetDrpCat();
                    ShowCat();
                    ClearData();
                }
                else
                {
                    lblError.Text = "Category Already Exist";
                }
            }
            else
            {
                ProductCategory updatecat = beta.ProductCategories.First(sc => sc.ProductCatID.Equals(catId));
                updatecat.ProductCatName = txtCatName.Text.ToString().ToUpper().Trim();
                beta.SaveChanges();
                lblError.Text = "Data Edited";
                catId = 0;
                GetDrpCat();
                ShowCat();
                ClearData();
            }

        }

        private void checkCat(string catName)
        {
            if ((beta.ProductCategories.Any(nm => nm.ProductCatName == catName)))
            {
                count++;
            }
            else
            {
                count = 0;
            }
        }

        //CatShow
        protected void ShowData_Click(object sender, EventArgs e)
        {
            GetDrpCat();
            ShowCat();
            GrdCat.Visible = true;
        }
        private void ShowCat()
        {
            try
            {
                GrdCat.DataSource = beta.ProductCategories.ToList();
                GrdCat.DataBind();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //Cat Select
        protected void GrdCat_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            catId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName.ToString().ToUpper() == "SELECT")
            {
                ProductCategory selectcat = beta.ProductCategories.First(sc => sc.ProductCatID.Equals(catId));
                txtCatName.Text = selectcat.ProductCatName.ToString();
            }
        }

        //Cat Delete
        protected void GrdCat_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            int delID = Convert.ToInt32(GrdCat.DataKeys[e.RowIndex].Value.ToString());
            ProductCategory selectcat = beta.ProductCategories.First(ProductCategory => ProductCategory.ProductCatID == delID);
            beta.ProductCategories.Remove(selectcat);
            beta.SaveChanges();
            ShowCat();
            GetDrpCat();

        }
        
        //Paging
        protected void GrdCat_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ShowCat();
            GrdCat.PageIndex = e.NewPageIndex;
            GrdCat.DataBind();

        }


        //Sub Categories
        // Sub Categories Insert Update
        protected void btnSubCat_Click(object sender, EventArgs e)
        {
            CheckSubCat(Convert.ToInt32(drpCat.SelectedValue), txtSubCat.Text.ToUpper());
            try
            {

                if (SubcatId == 0)
                {
                    if (count == 0)
                    {
                        ProductSubCategory addsubcat = new ProductSubCategory()
                        {
                            ProductSubCatName = txtSubCat.Text.ToUpper(),
                            ProductCatID = Convert.ToInt32(drpCat.SelectedValue)
                        };
                        beta.ProductSubCategories.Add(addsubcat);
                        beta.SaveChanges();
                        lblSubError.Text = "SubCategories Added";
                    }
                    else
                    {
                        lblSubError.Text = "Already Exist";
                    }
                }
                else
                {
                    ProductSubCategory updatesubcat = beta.ProductSubCategories.First((sc => sc.ProductSubCatID.Equals(SubcatId)));
                    updatesubcat.ProductSubCatName = txtSubCat.Text.ToUpper();
                    updatesubcat.ProductCatID = Convert.ToInt32(drpCat.SelectedValue);
                    beta.SaveChanges();

                    lblSubError.Text = "SubCategories Updated";
                }
                GetDrpCat();
                ShowSubCat();
                ClearData();
                

            }
            catch (Exception s)
            {
                throw s;
            }
        }
        // Sub Categories Check
        private void CheckSubCat(int v1, string v2)
        {
            if (beta.ProductSubCategories.Any(sc => sc.ProductCatID == v1 && sc.ProductSubCatName == v2))
            {
                count++;
            }
            else
            {
                count = 0;
            }
        }
        // Sub Categories Show
        private void ShowSubCat()
        {
            grdSubCat.DataSource = (from sc in beta.ProductSubCategories
                                    join c in beta.ProductCategories on sc.ProductCatID equals c.ProductCatID
                                    select new
                                    {
                                        SubCatID = sc.ProductSubCatID,
                                        Cat_Name = c.ProductCatName,
                                        SubCat_Name = sc.ProductSubCatName

                                    }
                                  ).ToList();

            grdSubCat.DataBind();
        }
        protected void btnShowSubCat_Click(object sender, EventArgs e)
        {
            ShowSubCat();
            grdSubCat.Visible = true;
        }
        // Sub Categories Select
        protected void grdSubCat_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SubcatId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName.ToString().ToUpper() == "SELECT")
            {
                ProductSubCategory selSubCat = beta.ProductSubCategories.First(sc => sc.ProductSubCatID.Equals(SubcatId));
                drpCat.SelectedValue = selSubCat.ProductCatID.ToString();
                txtSubCat.Text = selSubCat.ProductSubCatName.ToString();
            }
        }
        // Sub Categories Delete
        protected void grdSubCat_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ProductSubCategory delsubcat = beta.ProductSubCategories.First((sc => sc.ProductSubCatID.Equals(SubcatId)));
            beta.ProductSubCategories.Remove(delsubcat);
            beta.SaveChanges();
            ShowSubCat();
            ClearData();
            GetDrpCat();
            lblSubError.Text = "SubCategories Deleted";
        }
        //Clear Control
        private void ClearData()
        {

            txtSubCat.Text = txtCatName.Text = txtSize.Text = null;
            catId = SubcatId = SizeID = 0;
            GrdCat.Visible = grdSize.Visible = GrdCat.Visible = false;
        }
        //Size Insert Update
        protected void BtnSize_Click(object sender, EventArgs e)
        {
            CheckSize(Convert.ToInt32(drpSubCatsize.SelectedValue), txtSize.Text.ToUpper());
            //try
            //{


            if (SizeID == 0)
            {
                if (count == 0)
                {
                    ProductSize addSize = new ProductSize()
                    {
                        ProductSizeName = txtSize.Text.ToUpper().ToString(),
                        ProductSubCatID = Convert.ToInt32(drpSubCatsize.SelectedValue)
                    };
                    beta.ProductSizes.Add(addSize);
                    try
                    {
                        beta.SaveChanges();
                    }
                    catch (DbEntityValidationException dbEx)
                    {
                        
                        foreach (var eve in dbEx.EntityValidationErrors)
                        {
                            string a = eve.Entry.Entity.GetType().Name + "__" + eve.Entry.State;
                            Response.Write(eve.Entry.Entity.GetType().Name+"__"+ eve.Entry.State);

                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                var c = ve.PropertyName.ToString() + ve.ErrorMessage.ToString();
                              Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }
                    

                    ClearData();
                    ShowSizeList();

                    lblSizeError.Text = "Data Added Sucessfully";
                }

                else
                {
                    lblSizeError.Text = "Already Exist";
                }
            }
            else
            {
                ProductSize updatesize = beta.ProductSizes.First(ps => ps.ProductSizeID.Equals(SizeID));
                updatesize.ProductSizeName = txtSize.Text.ToUpper();
                updatesize.ProductSubCatID = Convert.ToInt32(drpSubCatsize.SelectedValue);
                beta.SaveChanges();
                ClearData();
                lblSizeError.Text = "Data Updated Sucessfully";
                ShowSizeList();

            }

            //            }
            //catch (Exception s)
            //{
            //    throw s;
            //}
        }
        //Check Size
        private void CheckSize(int v1, string v2)
        {
            if (beta.ProductSizes.Any(sc => sc.ProductSubCatID == v1 && sc.ProductSizeName == v2))
            {
                count++;
            }
            else
            {
                count = 0;
            }
        }
        //Paging
        protected void grdSubCat_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ShowSubCat();
            grdSubCat.PageIndex = e.NewPageIndex;
            grdSubCat.DataBind();

        }



        //Show Size Button
        protected void btnShoeSize_Click(object sender, EventArgs e)
        {
            grdSize.Visible = true;
            drpShowSubSizeCat.Visible = true;
            //grdSize.Visible = drpShoeSizeCat.Visible = true;
            // ShowSizeList();
        }
        //Show Size
        private void ShowSizeList()
        {
            int selSubCat = Convert.ToInt32(drpShowSubSizeCat.SelectedValue);

            
            if (selSubCat != 0)
            {
                grdSize.DataSource = (from s in beta.ProductSizes
                                      join c in beta.ProductSubCategories on s.ProductSubCatID equals c.ProductSubCatID
                                      where s.ProductSubCatID == selSubCat
                                      select new
                                      {
                                          ProductSizeID = s.ProductSizeID,
                                          Cat_Name = c.ProductSubCatName,
                                          ProductSizeName = s.ProductSizeName

                                      }
                                     ).ToList();

            }
            else
            {
                grdSize.DataSource = (from s in beta.ProductSizes
                                      join c in beta.ProductSubCategories on s.ProductSubCatID equals c.ProductSubCatID

                                      select new
                                      {
                                          ProductSizeID = s.ProductSizeID,
                                          Cat_Name = c.ProductSubCatName,
                                          ProductSizeName = s.ProductSizeName
                                      }
                                     ).ToList();

            }
            grdSize.DataBind();

        }
        //Select Size
        protected void grdSize_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SizeID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.ToUpper() == "SELECT")
            {
                ProductSize selSize = beta.ProductSizes.First(ps => ps.ProductSizeID.Equals(SizeID));
                drpSubCatsize.SelectedValue = selSize.ProductSubCatID.ToString();
                txtSize.Text = selSize.ProductSizeName;
            }
        }
        //Delete Size
        protected void grdSize_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ProductSize DelSize = beta.ProductSizes.First(ps => ps.ProductSizeID.Equals(SizeID));
            beta.ProductSizes.Remove(DelSize);
            beta.SaveChanges();
            ShowSizeList();
            ClearData();
        }
        //SizeList by Cat
        protected void drpShoeSizeCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowSizeList();
        }
        //Paging
        protected void grdSize_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ShowSizeList();
            grdSize.PageIndex = e.NewPageIndex;
            grdSize.DataBind();
        }

     
    }
}