<%@ Page Title="" Language="C#" MasterPageFile="~/Beta.Master" AutoEventWireup="true" CodeBehind="ProductAdmin.aspx.cs" Inherits="BetaLifeStyle.Private.Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /*.pagination {
            display: inline-block;
            border:none;
            
        }

            .pagination a {
                color: black;
                float: left;
                width: 20px;
                padding: 8px 16px;
                text-decoration: none;
            }

                .pagination a.active {
                    background-color: #4CAF50;
                    color: white;
                    border-radius: 5px;
                }

                .pagination a:hover:not(.active) {
                    background-color: #ddd;
                    border-radius: 5px;
                }*/
        .wrepup {
            word-wrap: break-word;
            word-break: break-word;
        }

        .brd {
            border-top-style: none;
            border: none
        }

            .brd td {
                border-top-style: none;
                border: none
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <div class="panel panel-default">
        <div class="panel-heading">Add Product</div>
        <div class="panel-body">

            <div class="container-fluid">
                <div class="row">

                    <div class="col-md-6 col-sm-12">
                        <div class="container-fluid">

                            <asp:UpdatePanel ID="PnlPRoduct" runat="server">
                                <ContentTemplate>

                                    <div class="form-horizontal">

                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Product Categories</label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="drpCat" CssClass="form-control" OnSelectedIndexChanged="drpCat_SelectedIndexChanged" AppendDataBoundItems="true" runat="server" AutoPostBack="true">
                                                    <asp:ListItem Value="0" Text="Select Categories" Selected="True">
                                                    </asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:CompareValidator runat="server" ValueToCompare="0" Operator="NotEqual" Type="Integer" ID="CmpCat" CssClass="label label-danger" SetFocusOnError="true" ErrorMessage="Please Select Data" ControlToValidate="drpCat" Display="Dynamic" ValidationGroup="Products"></asp:CompareValidator>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">SubCategories Categories</label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="drpSubCat" CssClass="form-control" AppendDataBoundItems="true" runat="server">
                                                    <asp:ListItem Value="0" Text="Select Sub Categories" Selected="True">
                                                    </asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:CompareValidator runat="server" ValueToCompare="0" Operator="NotEqual" Type="Integer" ID="CmpSubCat" CssClass="label label-danger" SetFocusOnError="true" ErrorMessage="Please Select Data" ControlToValidate="drpSubCat" Display="Dynamic" ValidationGroup="Products"></asp:CompareValidator>
                                            </div>

                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Name</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtProName" CssClass="form-control" runat="server" ValidationGroup="Products"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="reqName" CssClass="label label-danger" SetFocusOnError="true" ErrorMessage="Input Name" ControlToValidate="txtProName" Display="Dynamic" ValidationGroup="Products"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Price</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtProPrice" CssClass="form-control" runat="server" ValidationGroup="Products"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="reqPrice" CssClass="label label-danger" SetFocusOnError="true" ErrorMessage="Input Price" ControlToValidate="txtProPrice" Display="Dynamic" ValidationGroup="Products"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator runat="server" ID="regPrice" CssClass="label label-danger" SetFocusOnError="true" ErrorMessage="Price is not valid" ValidationExpression="^[1-9]\d*(\.\d+)?$" ControlToValidate="txtProPrice" Display="Dynamic" ValidationGroup="Products"></asp:RegularExpressionValidator>

                                            </div>

                                        </div>


                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Description</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtProDec" CssClass="form-control" runat="server" TextMode="MultiLine" ValidationGroup="Products"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="reqDec" CssClass="label label-danger" SetFocusOnError="true" ErrorMessage="Add Detail" ControlToValidate="txtProDec" Display="Dynamic" ValidationGroup="Products"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-3 control-label ">Color</label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="drpColor" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="Color" Text="Select Color" Selected="True">
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="Red" Text="Red">
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="Black" Text="Black">
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="Purple" Text="Purple">
                                                    </asp:ListItem>
                                                     <asp:ListItem Value="White" Text="White">
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="Grey" Text="Grey">
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="Pink" Text="Pink">
                                                    </asp:ListItem>
                                                     <asp:ListItem Value="Mint" Text="Mint">
                                                    </asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:CompareValidator runat="server" ValueToCompare="Color" Operator="NotEqual" Type="String" ID="CmpColor" CssClass="label label-danger" SetFocusOnError="true" ErrorMessage="Please Select Color" ControlToValidate="drpColor" Display="Dynamic" ValidationGroup="Products"></asp:CompareValidator>
                                            </div>
                                        </div>
                                        <%-- --%>
                                        <%--                                        <div class="form-group clearfix">
                                            <label class="col-sm-3 control-label">Size</label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="drpSize" runat="server" AppendDataBoundItems="true" ValidationGroup="Products" CausesValidation="true"
                                                    CssClass="radio form-control" ItemType="BetaLifeStyle.ProductSizeTbl">
                                                    <asp:ListItem Value="0" Text="Size" Selected="True">
                                                    </asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-6 clearfix">
                                                <asp:CompareValidator runat="server" ValueToCompare="0"
                                                    Operator="NotEqual" Type="Integer" ID="CmpSize" CssClass="label label-danger"
                                                    SetFocusOnError="true" ErrorMessage="Select Size" ControlToValidate="drpSize" Display="Dynamic" ValidationGroup="Products"></asp:CompareValidator>
                                            </div>
                                        </div>--%>

                                        <div class=" form-group">
                                            <label class="col-sm-3 control-label">Brand Name</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtProBrand" CssClass="form-control" runat="server" TextMode="SingleLine" ValidationGroup="Products"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="reqBrandName" CssClass="label label-danger" SetFocusOnError="true" ErrorMessage="Input Brand" ControlToValidate="txtProBrand" Display="Dynamic" ValidationGroup="Products"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div class="form-horizontal">

                                <div class="form-group">
                                    <label class="col-sm-3 control-label">Images</label>
                                    <div class="col-sm-9">
                                        <asp:FileUpload ID="ProductImageUpload" AllowMultiple="true" runat="server" />
                                    </div>

                                    <div class="list-group" id="list"></div>
                                    <div class="has-error" id="errlist"></div>

                                </div>

                                <div class="form-group">
                                    <div class="text-center center-block ">
                                        <asp:Button ID="btnProduct" CssClass="btn btn-success" Text="Add New Product" runat="server" ValidationGroup="Products" OnClick="btnProduct_Click1" />
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                    <div class="col-md-6 col-sm-12">


                        <asp:Repeater ID="repImage" runat="server" Visible="false" OnItemCommand="repImage_ItemCommand">
                            <ItemTemplate>
                                <div class="col-sm-4">
                                    <a href='<%#String.Format("/ProductImage/{0}",Eval("Source")) %>' id="btn" target="_blank">
                                        <img id='<%# Eval("ProImgName") %>' src='<%#String.Format("/ProductImage/{0}",Eval("Source")) %>' height="100" class="img-rounded" width="100" style="margin: 5px;" /></a>
                                    <asp:LinkButton ID="lnkDlt" CommandName="Delete" CommandArgument='<%# Eval("IDM") %>' runat="server"><span class="glyphicon glyphicon-remove-circle"></span></asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <div class="panel panel-default">
        <div class="panel-heading">Products List</div>
        <div class="panel-body">


            <div class="container-fluid">
                <div class="row">
                    <div class="form-inline vcenter pull-right" role="search">
                        <div class="input-group input-group-sm">
                            <asp:TextBox ID="txtSearch" CssClass="form-control" placeholder="Search" runat="server"></asp:TextBox>

                            <div class="input-group-btn">
                                <asp:LinkButton ID="lnksearch" runat="server" CssClass="btn btn-default" OnClick="lnkSearch_Click"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                            </div>
                        </div>


                        <%--  <asp:UpdatePanel ID="SearchUpdate" runat="server">
                            <ContentTemplate>
                              <%--  <asp:DropDownList ID="drpSearchCat" CssClass="form-control" OnSelectedIndexChanged="drpCat_SelectedIndexChanged" AppendDataBoundItems="true" runat="server" AutoPostBack="true">
                                    <asp:ListItem Value="0" Text="Select Categories" Selected="True">
                                    </asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="DropDownList1" CssClass="form-control" OnSelectedIndexChanged="drpCat_SelectedIndexChanged" AppendDataBoundItems="true" runat="server">
                                                    <asp:ListItem Value="0" Text="Select Categories" Selected="True">
                                                    </asp:ListItem>
                                                </asp:DropDownList>--%>

                        <%--    <asp:LinkButton ID="lnkSearch" runat="server" CssClass="btn input-group-addon" OnClick="lnkSearch_Click"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <asp:GridView ID="GrdProduct" RowStyle-VerticalAlign="Middle" FooterStyle-BorderStyle="None" AllowPaging="true" PageSize="10" runat="server" OnPageIndexChanging="GrdProduct_PageIndexChanging" AutoGenerateColumns="false" OnRowDeleting="GrdProduct_RowDeleting" DataKeyNames="ID" OnRowDataBound="GrdProduct_RowDataBound" OnRowCommand="GrdProduct_RowCommand" CssClass="table table-hover table-responsive wrepup" HeaderStyle-Wrap="true" RowStyle-Wrap="true" BorderStyle="None">
                            <Columns>
                                <asp:BoundField DataField="ID" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderText="ID" HeaderStyle-BorderStyle="None" HeaderStyle-CssClass="wrepup" ItemStyle-CssClass="wrepup" ControlStyle-CssClass="wrepup" ItemStyle-BorderStyle="None" />
                                <asp:BoundField DataField="Categories" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderText="Categories" ItemStyle-CssClass="wrepup" HeaderStyle-CssClass="wrepup" ControlStyle-CssClass="wrepup" HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None" />
                                <asp:BoundField DataField="SubCat" HeaderText="SubCat" HeaderStyle-BorderStyle="None" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-BorderStyle="None" HeaderStyle-CssClass="wrepup" ItemStyle-CssClass="wrepup" ControlStyle-CssClass="wrepup" />
                                <asp:BoundField DataField="ProductName" HeaderText="ProductName" HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None" HeaderStyle-CssClass="wrepup" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="wrepup" ControlStyle-CssClass="wrepup" />
                                <asp:BoundField DataField="BrandName" HeaderText="BrandName" HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None" ItemStyle-CssClass="wrepup" HeaderStyle-CssClass="wrepup" ControlStyle-CssClass="wrepup" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                                <asp:BoundField DataField="Price" HeaderText="Price" HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None" ItemStyle-CssClass="wrepup" ControlStyle-CssClass="wrepup" HeaderStyle-CssClass="wrepup" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                                <asp:BoundField DataField="Color" HeaderText="Color" HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None" ItemStyle-CssClass="wrepup" ControlStyle-CssClass="wrepup" HeaderStyle-CssClass="wrepup" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                                <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None" ItemStyle-CssClass="wrepup" ControlStyle-CssClass="wrepup" HeaderStyle-CssClass="wrepup" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                                <asp:TemplateField HeaderText="Stock & Images" HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None" ItemStyle-CssClass="wrepup" ControlStyle-CssClass="wrepup" HeaderStyle-CssClass="wrepup">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="PRoID" Value='<%# Eval("ID") %>' runat="server" />

                                        <div class="form-group">
                                            <%-- Stock Manage --%>

                                            <button type="button" class="btn btn-info" data-toggle="modal" data-target='<%#String.Format("#PR{0}",Eval("ID")) %>'>
                                                Stock</button>

                                            <!-- Modal -->
                                            <div class="modal fade ms" id='<%#String.Format("PR{0}",Eval("ID")) %>' role="dialog">
                                                <div class="modal-dialog">
                                                    <!-- Modal content-->
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                            <h4 class="modal-title">Stock List</h4>
                                                        </div>
                                                        <div class="modal-body">

                                                            <div class="center-block text-center" style="width: 300px;">
                                                                <asp:GridView ID="StockList" AutoGenerateColumns="false" Style="border: none; width: 400px; position: center" runat="server" RowStyle-Height="50" RowStyle-Width="100" RowStyle-BorderStyle="None" CssClass="table">
                                                                    <Columns>
                                                                        <asp:BoundField HeaderText="Size" DataField="ProductSizeName" HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None" />
                                                                        <asp:BoundField HeaderText="Previous" DataField="StockCount" HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None" />

                                                                        <asp:TemplateField ShowHeader="false" ItemStyle-BorderStyle="None" ItemStyle-Font-Size="Medium" ItemStyle-Height="20" HeaderStyle-BorderStyle="None" HeaderText="Update Stock">
                                                                            <ItemTemplate>
                                                                                <div style="margin-bottom: 20px; padding: 0px; float: none;">
                                                                                    <%--            <div class="pull-left" style="padding: 0px">
                                                                                                <label id="lblSize" runat="server"><%#String.Format("{0}",Eval("ProductSizeName")) %> </label>
                                                                                            </div>
                                                                                    --%>
                                                                                    <asp:HiddenField ID="StockID" Value='<%# Eval("StockID") %>' runat="server" />

                                                                                    <asp:Label ID="lblStock" CssClass="hidden" runat="server" Text='<%#String.Format("{0}",Eval("StockCount")) %>'></asp:Label>
                                                                                    <asp:DropDownList ID="DrpOperator" runat="server" Width="60" Height="30">
                                                                                        <asp:ListItem Selected="True" Text="+" Value="P"></asp:ListItem>
                                                                                        <asp:ListItem Text="-" Value="M"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <asp:TextBox ID="txtStock" Width="60" TextMode="Number" Height="30" runat="server" Text="0"></asp:TextBox>

                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                                <div class="clearfix">
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:LinkButton ID="btnSave" CssClass="btn btn-info" CommandName="SAVE" runat="server" CommandArgument='<%# Eval("ID") %>'>Save</asp:LinkButton>
                                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <!-- Modal Ends-->


                                            <%-- Image List --%>


                                            <button type="button" class="btn btn-info" data-toggle="modal" data-target='<%#String.Format("#PRI{0}",Eval("ID")) %>'>
                                                Images</button>


                                            <!-- Modal -->
                                            <div class="modal fade ms" id='<%#String.Format("PRI{0}",Eval("ID")) %>' role="dialog">
                                                <div class="modal-dialog">

                                                    <!-- Modal content-->
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                            <h4 class="modal-title">Images</h4>
                                                        </div>
                                                        <div class="modal-body">

                                                            <div class="row" style="word-break: break-word;">
                                                                <asp:Repeater ID="RepEx" runat="server">
                                                                    <ItemTemplate>
                                                                        <a href='<%#String.Format("/ProductImage/{0}",Eval("Source")) %>' id="btn" target="_blank">
                                                                            <img id='<%# Eval("ProImgName") %>' src='<%#String.Format("/ProductImage/{0}",Eval("Source")) %>' height="100" class="img-rounded col-sm-2" style="margin: 5px;" /></a>

                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </div>

                                                        </div>
                                                        <div class="modal-footer">
                                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <!-- Modal Ends-->

                                        </div>


                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action" HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" CssClass="btn btn-info" CommandName="Select" runat="server" CommandArgument='<%# Eval("ID") %>'><span class="glyphicon glyphicon-edit"></span></asp:LinkButton>
                                        <asp:LinkButton ID="lnkDlt" CssClass="btn btn-info" CommandName="Delete" runat="server" CommandArgument='<%# Eval("ID") %>'><span class="glyphicon glyphicon-remove-circle"></span></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <PagerSettings PageButtonCount="2" Mode="NumericFirstLast" Position="Bottom" />


                            <PagerStyle CssClass="brd"></PagerStyle>
                        </asp:GridView>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
