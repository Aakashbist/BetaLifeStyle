using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BetaLifeStyle.Private
{
    public partial class OrderHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            OrderDetails();
        }
        public void OrderDetails()
        {
            BetaDB db = new BetaDB();
            if (Session["email"] != null)
            {
                var email = Session["email"].ToString();
                var Id = db.Logins.Where(x => x.Email.Equals(email)).Select(x => x.ID).FirstOrDefault();
                var invoiceNo = db.OrderDetails.GroupBy(x => x.InvoiceNo).Select(y=>new { InvoiceNo=y.Key}).ToList();
                orderViewPanelRepeater.DataSource = invoiceNo;
                orderViewPanelRepeater.DataBind();
                if (invoiceNo.Count <= 0)
                {
                    orderempty.Visible = true;
                    orderemptymsg.Text = "There is no Orders to show";
                }
            }
            else
            {
                orderempty.Visible = true;
                orderemptymsg.Text = "There is no Orders to show";            
            }
        }

        protected void orderrepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            BetaDB db = new BetaDB();
            HiddenField hdnID = e.Item.FindControl("ProHdn") as HiddenField;
            int Id = Convert.ToInt32(hdnID.Value);
            var images = db.ProductImages.Where(im => im.ProductID.Equals(Id)).FirstOrDefault();

            Image img = e.Item.FindControl("proImg") as Image;
            string imgpath = "~/ProductImage/" + images.ProductImagePath.ToString();
            img.ImageUrl = imgpath;


        }

        protected void orderViewPanelRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                BetaDB db = new BetaDB();
                var invoiceNo = (e.Item.FindControl("hfInvoiceNo") as HiddenField).Value;
                Repeater rptOrders = e.Item.FindControl("orderViewRepeater") as Repeater;
                var list = db.OrderDetails.Where(x=>x.InvoiceNo==invoiceNo).Select(x => new { x.ProductId, ImageUrl = x.Product.ProductImages.FirstOrDefault(), x.Product.ProductName, x.Product.ProductBrandName, x.Price, x.InvoiceNo, x.Quantity, Status = db.Payments.FirstOrDefault().Status, AddedOn = db.Payments.FirstOrDefault().AddedOn }).ToList();
                rptOrders.DataSource = list;
                rptOrders.DataBind();

            }
        }
    }
}