﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Beta.Master" AutoEventWireup="true" CodeBehind="Activationpage.aspx.cs" Inherits="BetaLifeStyle.Public.Activationpage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="container-fluid">

        <asp:Panel runat="server" CssClass="panel panel-default" ID="AccountActivation" Visible="false">
            <div class="panel-heading">
                Message
            </div>
            <div class="panel-body">
                <asp:Label ID="ltMessage" CssClass="h4" runat="server" Text="Label"></asp:Label>
                <br />
                <asp:Label ID="lblMessagedesc" CssClass="h3" runat="server" Text="Label"></asp:Label>
            </div>
        </asp:Panel>

      
    </div>
</asp:Content>
