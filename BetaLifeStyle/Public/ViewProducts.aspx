<%@ Page Title="" Language="C#" MasterPageFile="~/Beta.Master" AutoEventWireup="true" CodeBehind="ViewProducts.aspx.cs" Inherits="BetaLifeStyle.Private.ViewProducts" %>

<%@ MasterType VirtualPath="~/Beta.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/ProductUI.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">

        <%--Left Side Panel with category DS--%>
        <div class="col-sm-2 white" style="top: 0px; left: 0px; padding-bottom: 1px; margin-bottom: 10px;">
            <strong>
                <h5 class="text-uppercase" style="padding-top: 10px; margin-top: 0px;"><strong>Filters</strong></h5>
            </strong>
            <hr class="clearm xhr" />
            <div class="categories" style="margin-bottom: 10px">
                

                <%try
                    {
                        string[] categories = GetProductCategories().Split('#');
                        %>
                <p class="h5">Categories<span class="badge pull-right"><%  Response.Write(categories.Length); %></span></p>
                <%
                        for (int i = 0; i < categories.Length; i++)
                        {
                            string[] arr = categories[i].Split('|');
                            if (arr[0] == "")
                            {
                                continue;
                            }
                            string[] subcategories = arr[1].Split(',');
                            if (subcategories.Length >= 1)
                            {
                %>
                <div>
                    <a href="#" style="text-transform: capitalize;" class="anchorcollapse" data-toggle="collapse" data-target="#<% Response.Write(arr[0].Replace(' ', '_')); %>SubCategory">
                        <% Response.Write(arr[0]);%>
                    </a>
                    <div id="<% Response.Write(arr[0].Replace(' ', '_')); %>SubCategory" class="collapse ctogg">
                        <%
                            for (int j = 0; j < subcategories.Length; j++)
                            {
                        %>
                        <span class="sidebarsubcategory"><% Response.Write(subcategories[j]); %></span>
                        <%  }%>
                    </div>
                </div>
                <%}
                    else
                    {%>

                <div>
                    <a href="#" class="CategoryNoSubCategory" style="text-transform: capitalize;">
                        <% Response.Write(arr[0]);%>
                    </a>
                </div>


                <%}
                    }
                %>
            </div>
        </div>

        <%--Middle Section - Products -DS--%>
        <div class="col-sm-8" style="top: 0px; left: 0px !important;">
            <%--First Row - Header with search-DS--%>
            <div class="container-fluid white" style="padding: 10px !important;">
                <label id="ProductListTitle" class="vcenter h5 text-uppercase">Our Products</label>
                <div class="form-inline vcenter pull-right" role="search">
                    <div class="input-group input-group-sm">
                        <input type="text" id="searchtxtbox" class="form-control" placeholder="Search" />
                        <div class="input-group-btn">
                            <button class="btn btn-default" id="searchbtn" type="submit" onclick="Search(1,true); return false;">
                                <span class="glyphicon glyphicon-search" />
                            </button>

                        </div>
                    </div>
                </div>
            </div>



            <%--Product List - DS--%>
            <div class="productgrid" style="margin-top: 10px;">
                <asp:Repeater ID="productrepeater" runat="server" OnItemDataBound="productrepeater_ItemDataBound">
                    <ItemTemplate>
                        <div class="product">
                            <asp:HiddenField ID="ProHdn" runat="server" Value='<%# Eval("ProductID")%>' />
                            <a href="/Product/<%# Eval("ProductID")%>"><asp:Image ID="proImg" runat="server" CssClass="productimage" /></a>
                            <div class="productdescription">
                                <p class="h6 text-center"></p>
                                 <div style="padding-left: 10px; padding-right: 10px">
                                    <span><strong><%# Eval("ProductName")%></strong></span>
                                    <span class="pull-right"><b>$<%# Eval("ProductPrice")%></b></span>
                                </div>
                                <div style="padding-left: 10px; padding-right: 10px">
                                    <p><small><%# Eval("ProductBrandName")%></small></p>
                                </div>
                                <br/>
                                <div class="text-center">
                                    <a href="/Product/<%# Eval("ProductID")%>" class="buybtn">More Details</a>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            </div>

        </div>


        <%--Right Side - Ads-DSS--%>
        <div class="col-sm-2 hidden-xs white" style="top: 0px; left: 0px !important;">
            <img class="img-responsive" src="/ProductImage/adsdemo.PNG" />
            Example Ad
        </div>

        <% }
            catch (Exception e)
            {
                Response.Redirect("/Error");
            }%>
    </div>

</asp:Content>
