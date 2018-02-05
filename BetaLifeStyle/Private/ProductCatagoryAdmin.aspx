<%@ Page Title="" Language="C#" MasterPageFile="~/Beta.Master" AutoEventWireup="true" CodeBehind="ProductCatagoryAdmin.aspx.cs" Inherits="BetaLifeStyle.Private.ProductCat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/JQMethod.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <%-- Categories --%>
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">Product Category</div>
            <div class="panel-body">
                <asp:UpdatePanel ID="pnlCat" runat="server">
                    <ContentTemplate>

                        <div class="col-md-5 col-sm-12">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-sm-6">
                                        <label class="control-label" style="margin-top: 10px;" for="txtCatName">Category Name</label>

                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox runat="server" Style="margin-top: 5px;" CssClass="form-control" ID="txtCatName" ValidationGroup="cat"></asp:TextBox>

                                    </div>

                                    <asp:RequiredFieldValidator runat="server" ID="reqCat" CssClass="label label-danger col-sm-3" ErrorMessage="Please Input Data" ControlToValidate="txtCatName" Display="Dynamic" ValidationGroup="cat"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <div class="col-sm-6">
                                        <asp:Button ID="btnCatSubmit" OnClick="btnCatSubmit_Click" runat="server" CssClass="btn btn-info" Text="Submit" UseSubmitBehavior="true" ValidationGroup="cat"></asp:Button>
                                        <asp:Label ID="lblError" runat="server" CssClass="label label-danger" Visible="false"></asp:Label>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-5 col-sm-12">
                            <div class="form-group">
                                <div class="col-sm-12 center-block">
                                    <asp:Button ID="ShowData" runat="server" CssClass="btn btn-link " Text="Show Categories" OnClick="ShowData_Click" />

                                    <asp:GridView ID="GrdCat" runat="server" AutoGenerateColumns="false" DataKeyNames="ProductCatID" OnRowDeleting="GrdCat_RowDeleting" PageSize="10" OnPageIndexChanging="GrdCat_PageIndexChanging" Visible="false" OnRowCommand="GrdCat_RowCommand" CssClass="well table table-bordered table-hover table-responsive">
                                        <Columns>
                                            <asp:BoundField DataField="ProductCatID" HeaderText="ID" ItemStyle-Width="70px" />
                                            <asp:BoundField DataField="ProductCatName" HeaderText="Name" ItemStyle-Width="100px" />
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDlt" CommandName="Delete" CssClass="btn btn-info" runat="server" CommandArgument='<%# Eval("ProductCatID") %>'><span class="glyphicon glyphicon-remove-circle"></span></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkEdit" CommandName="Select" CssClass="btn btn-info" runat="server" CommandArgument='<%# Eval("ProductCatID") %>'><span class="glyphicon glyphicon-edit"></span></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerSettings PageButtonCount="2" Mode="NumericFirstLast" Position="Bottom" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <br />

    <%--  Sub Categories--%>
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">Sub Categories</div>
            <div class="panel-body">
                <asp:UpdatePanel ID="PanSubCat" runat="server" EnableViewState="true">
                    <ContentTemplate>

                        <div class="col-md-5 col-sm-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-sm-6">
                                        <label class="control-label" for="txtCatName">Sub Categories Name</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="drpCat" CssClass="dropdown form-control"
                                            runat="server" ValidationGroup="Subcat" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="---Select Categories---" Selected="True">
                                            </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:CompareValidator runat="server" ValueToCompare="0" Operator="NotEqual" Type="Integer" ID="CmpCat" CssClass="label label-danger" SetFocusOnError="true" ErrorMessage="Please Select Data" ControlToValidate="drpCat" Display="Dynamic" ValidationGroup="Subcat"></asp:CompareValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-6">
                                        <label class="control-label" for="txtSubCat">Sub-Categorie Name</label>
                                    </div>

                                    <div class="col-sm-6">
                                        <asp:TextBox runat="server" Style="margin-top: 5px;" CssClass="form-control" ID="txtSubCat" ValidationGroup="Subcat"></asp:TextBox>
                                    </div>

                                    <div class="col-sm-4">
                                        <asp:RequiredFieldValidator ID="reqSubCat" runat="server" SetFocusOnError="true" ErrorMessage="Please Input Text" CssClass="label label-danger" ValidationGroup="Subcat" ControlToValidate="txtSubCat"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12 center-block">
                                        <asp:Button ID="btnSubCat" CssClass="btn btn-info" Text="Submit" OnClick="btnSubCat_Click"
                                            runat="server" ValidationGroup="Subcat" />
                                        <asp:Label ID="lblSubError" Text="" CssClass="label label-danger" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-5 col-sm-12">
                            <div class="form-group">
                                <div class="col-sm-12 center-block">
                                    <asp:Button ID="btnShowSubCat" CssClass="btn btn-link" Text="Show Sub Categories" OnClick="btnShowSubCat_Click" runat="server" />
                                    <asp:GridView ID="grdSubCat" Visible="false" runat="server" DataKeyNames="SubCatID" PageSize="10" OnPageIndexChanging="grdSubCat_PageIndexChanging" OnRowCommand="grdSubCat_RowCommand" OnRowDeleting="grdSubCat_RowDeleting" AutoGenerateColumns="false" CssClass="table table-hover table-bordered table-responsive well" AllowSorting="true" PagerSettings-Mode="Numeric" AllowPaging="true">
                                        <Columns>
                                            <asp:BoundField DataField="SubCatID" HeaderText="SubCatID" />
                                            <asp:BoundField DataField="Cat_Name" HeaderText="Cat_Name" />
                                            <asp:BoundField DataField="SubCat_Name" HeaderText="SubCat_Name" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LnkDlt" CommandName="Delete" runat="server" CssClass="btn btn-info" CommandArgument='<%# Eval("SubCatID") %>'><span class="glyphicon glyphicon-remove-circle"></span></asp:LinkButton>
                                                    <asp:LinkButton ID="LnkEdit" CommandName="Select" runat="server" CssClass="btn btn-info" CommandArgument='<%# Eval("SubCatID") %>'><span class="glyphicon glyphicon-edit"></span></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerSettings PageButtonCount="2" Mode="Numeric" Position="Bottom" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <br />

    <%--  Sub Sizes--%>
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">Product Size</div>
            <div class="panel-body">
                <asp:UpdatePanel ID="PnlSize" runat="server" EnableViewState="true">
                    <ContentTemplate>

                        <div class="col-md-5 col-sm-12">

                            <div class="form-horizontal">
                                <div class="form-group">
                                  
                                        <div class="col-sm-6">
                                            <label class="control-label" style="margin-top: 10px;" for="drpCatsize">Product Size Name</label>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="drpSubCatsize" CssClass="dropdown form-control"
                                                runat="server" ValidationGroup="SizeL" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0" Text="Select Categories" Selected="True">
                                                </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:CompareValidator runat="server" ValueToCompare="0" Operator="NotEqual" Type="Integer"
                                                ID="cmpDrpS" CssClass="label label-danger" SetFocusOnError="true" ErrorMessage="Please Select Data"
                                                ControlToValidate="drpSubCatsize" Display="Dynamic" ValidationGroup="SizeL"></asp:CompareValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-6">
                                            <label class="control-label" style="margin-top: 10px;" for="txtSize">Size Name</label>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" Style="margin-top: 5px;"
                                                CssClass="form-control" ID="txtSize" ValidationGroup="SizeL"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:RequiredFieldValidator ID="reqSize" runat="server" SetFocusOnError="true"
                                                ErrorMessage="Please Input Text" CssClass="label label-danger"
                                                ValidationGroup="SizeL" ControlToValidate="txtSize"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12 center-block">
                                            <asp:Button ID="BtnSize" CssClass="btn btn-info" Text="Submit" OnClick="BtnSize_Click"
                                                runat="server" ValidationGroup="SizeL" />
                                            <asp:Label ID="lblSizeError" Text="" CssClass="label label-danger" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-5 col-sm-12">
                            <div class="form-inline">
                                <div class="form-group">
                                      <div class="col-sm-12 center-block">
                                    <asp:Button ID="btnShoeSize" CssClass="btn btn-link" Text="Show Size List" OnClick="btnShoeSize_Click"
                                        runat="server" />
                                    <asp:DropDownList ID="drpShowSubSizeCat" OnSelectedIndexChanged="drpShoeSizeCat_SelectedIndexChanged"
                                        AutoPostBack="true" CssClass="dropdown form-control "
                                        runat="server" Visible="false" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0" Text="Select Categories" Selected="True">
                                        </asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                                
                            </div>
                            <div class="clearfix"></div>
                          
                              <div class="form-inline" style="margin-top:20px;">
                                <asp:GridView ID="grdSize" runat="server" CssClass="table table-bordered table-responsive table-hover well"
                                    AutoGenerateColumns="false" OnRowCommand="grdSize_RowCommand"
                                    OnRowDeleting="grdSize_RowDeleting" Visible="false" OnPageIndexChanging="grdSize_PageIndexChanging"
                                    DataKeyNames="ProductSizeID" AllowSorting="true" AllowPaging="true" PageSize="7">
                                    <Columns>
                                        <asp:BoundField DataField="ProductSizeID" HeaderText="ID" />
                                        <asp:BoundField DataField="Cat_Name" HeaderText="SubCat_Name" />

                                        <asp:BoundField DataField="ProductSizeName" HeaderText="SizeName" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDlt" CommandName="Delete" CssClass="btn btn-info" runat="server" CommandArgument='<%# Eval("ProductSizeID") %>'><span class="glyphicon glyphicon-remove-circle"></span></asp:LinkButton>
                                                <asp:LinkButton ID="lnkedit" CommandName="Select" CssClass="btn btn-info" runat="server" CommandArgument='<%# Eval("ProductSizeID") %>'><span class="glyphicon glyphicon-edit"></span></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerSettings PageButtonCount="5" Mode="Numeric" Position="Bottom" />
                                </asp:GridView>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>
