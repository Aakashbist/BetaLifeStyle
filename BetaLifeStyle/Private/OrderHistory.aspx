<%@ Page Title="" Language="C#" MasterPageFile="~/Beta.Master" AutoEventWireup="true" CodeBehind="OrderHistory.aspx.cs" Inherits="BetaLifeStyle.Private.OrderHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/CartStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div class="row">
        <div class="col-sm-12 col-md-9">
            <div class="panel panel-default">
                <div class="panel-heading">Order History</div>
                <div class="panel-body">
 <div>
        <asp:Panel ID="orderempty" runat="server" CssClass="white" Visible="false">
            <asp:Label ID="orderemptymsg" CssClass="h4" Style="padding-top: 10px !important; padding-bottom: 10px !important; padding-right: 10px !important; padding-left: 10px !important" runat="server"></asp:Label>
            <a href="/Home">GO BACK TO HOME </a>
        </asp:Panel>
    </div>
                    <div class="cartcontainerfororder clearfix">
                        <div class="products">
                            <asp:Repeater ID="orderViewPanelRepeater" runat="server" OnItemDataBound="orderViewPanelRepeater_ItemDataBound">
                                <ItemTemplate>
                                    <div class="panel">
                                        <div class="panel-heading info">
                                            <asp:HiddenField ID="hfInvoiceNo" runat="server" Value='<%# Eval("InvoiceNo")%>' />                                            
                                            <a data-toggle="collapse" data-target='#<%# Eval("InvoiceNo")%>'><%# Eval("InvoiceNo")%></a>
                                        </div>
                                        <div class="panel-body" style="padding: 0">
                                            <div id='<%# Eval("InvoiceNo")%>' class="collapse in">
                                                <asp:Repeater ID="orderViewRepeater" runat="server" OnItemDataBound=" orderrepeater_ItemDataBound">
                                                    <ItemTemplate>
                                                        <div class="cartproduct">
                                                            <div class="cartproductimage white">
                                                                <asp:HiddenField ID="ProHdn" runat="server" Value='<%# Eval("ProductId")%>' />
                                                                <asp:Image ID="proImg" runat="server" CssClass="productimagecart center-block" />
                                                            </div>
                                                            <div class="cartproductdetails">
                                                                <div class="h4 clearfix">
                                                                    <p class="pull-left" style="display: inline-block">
                                                                        <strong>
                                                                            <asp:Label runat="server" Text='<%# Eval("ProductName")%>'></asp:Label><br />
                                                                        </strong>
                                                                    </p>
                                                                    <p class="pull-right" style="display: inline-block; margin-bottom: 0px">
                                                                        <span class="productcontenttitle" style="margin-top: 0px">PRICE</span>
                                                                        $<asp:Label runat="server" Text='<%# Eval("Price")%>'></asp:Label>
                                                                    </p>
                                                                </div>
                                                                <div class="form-inline h5 clearm">
                                                                    <p class="form-group">
                                                                        <span class="productcontenttitle" style="margin-top: -4px;">Invoice</span>
                                                                        <asp:Label runat="server" Text='<%# Eval("InvoiceNo")%>'></asp:Label>
                                                                    </p>
                                                                    <p class="form-group marginfordiv">
                                                                        <span class="productcontenttitle">QUANTITY</span>
                                                                        <asp:Label runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                                    </p>
                                                                </div>
                                                                <hr />
                                                                <div class="clearmp">
                                                                    <p class="pull-left" style="display: inline-block">
                                                                        <asp:Label runat="server" Text='<%# Eval("AddedOn")%>'></asp:Label>
                                                                    </p>
                                                                    <p class="pull-right" style="display: inline-block; margin-bottom: 0px">
                                                                        <span class="productcontenttitle">Status</span>
                                                                        <asp:Label runat="server" Text='<%# Eval("Status")%>'></asp:Label>
                                                                    </p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>                                                
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
