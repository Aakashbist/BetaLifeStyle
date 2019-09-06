<%@ Page Title="" Language="C#" MasterPageFile="~/Beta.Master" AutoEventWireup="true" CodeBehind="CartView.aspx.cs" Inherits="BetaLifeStyle.Public.CartView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/CartStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Panel ID="cartempty" runat="server" CssClass="white" Visible="false" style="padding:12px;">
            <asp:Label ID="cartemptymsg" CssClass="h4" runat="server"></asp:Label>
            <a href="/Home" class="pull-right">GO BACK TO HOME </a>
        </asp:Panel>
    </div>

    <div class="cartcontainer clearfix">
        <div class="products">
            <asp:Repeater ID="cartViewRepeater" runat="server" OnItemDataBound="cartViewRepeater_ItemDataBound" OnItemCommand="cartViewRepeater_ItemCommand">
                <ItemTemplate>
                    <div class="cartproduct">
                        <div class="cartproductimage white">
                            <asp:HiddenField ID="ProHdn" runat="server" Value='<%# Eval("ProductId")%>' />
                            <asp:Image ID="proImg" runat="server" CssClass="productimagecart center-block"/>
                        </div>
                        <div class="cartproductdetails">
                            <div class="h4 clearfix">
                                <p class="pull-left" style="display: inline-block">
                                    <strong>
                                        <asp:Label ID="lblProductName" runat="server"></asp:Label><br />
                                    </strong>
                                </p>
                                <p class="pull-right" style="display: inline-block; margin-bottom: 0px">
                                    <span class="productcontenttitle" style="margin-top: 0px">PRICE</span>
                                    $<asp:Label ID="lblProductPrice" runat="server"></asp:Label>
                                </p>
                            </div>
                            <div class="form-inline h5">
                                <p class="form-group">
                                    <span class="productcontenttitle" style="margin-top: -4px;">SIZE</span>
                                    <asp:Label ID="lblProductSize" runat="server" Text='<%# Eval("size")%>'></asp:Label>
                                </p>
                                <p class="form-group marginfordiv">
                                    <asp:HiddenField runat="server" ID="hdnqty" Value='<%# Eval("quantity")%>'></asp:HiddenField>
                                    <span class="productcontenttitle">QUANTITY</span>
                                    <asp:DropDownList ID="Quantity" CssClass="dropdownlist clearm" runat="server" OnSelectedIndexChanged="Quantity_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList><br />
                                </p>
                                <p class="form-group h5 marginfordiv" style="margin-top:0px;">
                                    <span class="productcontenttitle" style="margin-top:-4px;">AVAILABILITY</span>
                                    <asp:Label ID="lblstock" runat="server"></asp:Label>
                                </p>
                            </div>
                            <hr />
                            <div class="pull-right clearmp">
                                <asp:LinkButton ID="lblDelete" runat="server" CommandArgument='<%#Eval("ProductId") %>' CommandName="remove">REMOVE</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="cartcalculation">
            <asp:Panel ID="cartCalculation" runat="server">
                <div class="white" style="text-align: justify; padding: 16px 16px 16px 16px">
                    <p class="productcontenttitle" style="margin-top: 0px">PURCHASE DETAILS</p>
                    <hr class="xhr" style="margin-top:0px"/>
                    <p>Cart Total <strong><asp:Label CssClass="pull-right" runat="server" ID="lblCartTotal"></asp:Label></strong></p>
                    <p>Tax <strong><asp:Label runat="server" ID="lbltax" CssClass="pull-right"></asp:Label></strong></p>
                    <p class="productcontenttitle">Delivery <asp:Label runat="server" CssClass="pull-right" ID="lbldelivery" Text="2"></asp:Label></p>
                    <hr class="xhr" />
                    
                    <div>    
                        <p class="form-group clearm">
                            <asp:Button runat="server" CssClass="btn btn-success clearm" Text="PLACE ORDER" ID="placeorderbutton" onclick="placeorderbutton_Click" OnClientClick=""/>
                            <strong ><asp:Label CssClass="h4 pull-right"  runat="server" ID="lblGrandCartTotal"></asp:Label></strong>
                        </p>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

