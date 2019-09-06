<%@ Page Title="" Language="C#" MasterPageFile="~/Beta.Master" AutoEventWireup="true" CodeBehind="AddAdmin.aspx.cs" Inherits="BetaLifeStyle.AddAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

            <div  class="col-sm-6 white" style="margin:auto;float:none;padding: 20px;padding-bottom:20px">
                <h4>Create Admin</h4>

                <div class="form-group">
                    <label>Name</label>
                    <div>
                        <asp:TextBox CssClass="form-control" ID="TxtName" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label>Email</label>
                    <div>
                        <asp:TextBox CssClass="form-control" TextMode="Email" ID="TxtREmail" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label>Password</label>
                    <div>
                        <asp:TextBox CssClass="form-control" TextMode="Password" ID="TxtRPassword" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="clearfix"/>

                <div class="form-group">
                    <div class="col-sm-8 text-center">
                        <asp:Label ID="lblmessage" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="col-xs-4">
                        <asp:Button Text="Create" ID="Add" runat="server" Cssclass="btn btn-success pull-right" OnClick="Add_Click" />
                    </div>
                </div>

                
            </div>
    

</asp:Content>
