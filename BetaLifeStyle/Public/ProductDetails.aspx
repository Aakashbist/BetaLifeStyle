<%@ Page Title="" Language="C#" MasterPageFile="~/Beta.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="BetaLifeStyle.Public.ProductDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/ProductUI.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="productview">
        <div class="productimages" >
            <asp:Label Id="ProductName" class="h5" style="display:block;" runat="server">Product Name</asp:Label>
            <asp:Image CssClass="MainPic center-block" style="margin:auto" ID="MainImage" runat="server"/>
            <span class="productcontenttitle" style="margin-left:10px">MORE IMAGES</span>
            <div id="ProductImagesContainer" runat="server" style="margin-top:10px;">

            </div>
        </div>
        <div class="productdetails">
            <ol id="ProductBreadCrumbs" class="breadcrumb" runat="server">
            </ol>
            
            <label class="h4" style="margin-top:0px"><% Response.Write(GetCurrentProduct().ProductName); %> </label>
            <span class="productcontenttitle">BRAND</span>
            <p><% Response.Write(GetCurrentProduct().ProductBrandName); %></p>
            <span class="productcontenttitle">DESCRIPTION</span>
            <p><% Response.Write(GetCurrentProduct().ProductDesc); %></p>
            <span class="productcontenttitle">PRICE</span>
            <label class="h2" style="margin-top:5px;">$<% Response.Write(GetCurrentProduct().ProductPrice); %></label>
            <span class="productcontenttitle">COLOUR</span>
            <p><% Response.Write(GetCurrentProduct().ProductColor != null ?GetCurrentProduct().ProductColor : "NONE"); %></p>
            <span class="productcontenttitle">SELECT SIZE</span>
            <asp:DropDownList ID="Sizes" Cssclass="dropdownlist" runat="server" >
            </asp:DropDownList>
                 <%-- label to show msg if session is null before making order or using cart --%>
            <div  style="display:block;margin-top:30px;">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </div>
            <div  style="display:block;margin-top:30px;">
                <asp:Button ID="btnCart" CssClass="buybtnproductpage" runat="server" Text="Add to Cart" OnClick="btnCart_Click" />
            </div>    
        </div>
    </div>

</asp:Content>
