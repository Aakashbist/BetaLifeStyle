<%@ Page Title="" Language="C#" MasterPageFile="~/Beta.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="BetaLifeStyle.ChangePassword1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container-fluid" style="margin: 100px 0 0 50px">
           <asp:Panel runat="server" CssClass="panel panel-default" ID="ErrorMessage" Visible="false">
            <div class="panel-heading">
                Message
            </div>
            <div class="panel-body">
                <asp:Label ID="ltMessage" CssClass="h4" runat="server" Text="Label"></asp:Label>
                <br />
                <asp:Label ID="lblMessagedesc" CssClass="h3" runat="server" Text="Label"></asp:Label>
            </div>
        </asp:Panel>
           <asp:Panel ID="ResetPassword" CssClass="panel panel-default" runat="server" Visible="false">

            <div class="panel-heading bg-primary">
                Reset Password
            </div>
            <div class="panel-body">
                <div class="row"></div>
                <div class="col-sm-12 col-md-4">
                    <div class="form-group">
                        <div class="input-group">
                            <span class="input-group-addon">
                                <i class="glyphicon glyphicon-lock"></i></span>
                            <input class="form-control" id="TxtResetPassword" maxlength="8" type="password" placeholder="New Password" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="input-group">
                            <span class="input-group-addon">
                                <i class="glyphicon glyphicon-lock"></i></span>
                            <input class="form-control" id="TxtRestConfirmaPassword" maxlength="8" type="password" placeholder="Conform Password" />
                        </div>
                    </div>
                    <div>
                        <asp:Label runat="server" ID="lblmessageResetpassword"></asp:Label>
                    </div>
                    <div class="form-group">
                        <input type="button" id="BtnResetPassword" value="Change Password" class="btn btn-success" />
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="ChangePassword" CssClass="panel panel-default" runat="server" Visible="false">
            <div class="panel-heading">
                Change Password
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-xs-6">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon">
                                    <i class="glyphicon glyphicon-lock"></i></span>
                                <input class="form-control" id="TxtCurrentPassword" maxlength="8" type="password" placeholder="Current Password" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon">
                                    <i class="glyphicon glyphicon-lock"></i></span>
                                <input class="form-control" id="TxtNewPassword" maxlength="8" type="password" placeholder="NewPassword" />
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon">
                                    <i class="glyphicon glyphicon-lock"></i></span>
                                <input class="form-control" id="TxtConformNewPassword" maxlength="8" type="password" placeholder="Conform Password" />
                            </div>
                        </div>
                        <div>
                        <asp:Label runat="server" ID="LblMessageChangePassword"></asp:Label>
                    </div>
                        <div class="form-group">
                            <input type="button" id="BtnChangePassword" value="Change Password" class="btn btn-success" />
                        </div>
                    </div>
                </div>
            </div>

        </asp:Panel>
     </div>
</asp:Content>
