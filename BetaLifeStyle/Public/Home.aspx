<%@ Page Title="Home" Language="C#" MasterPageFile="~/Beta.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="BetaLifeStyle.Home" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin:20px 2px 3px">
          <a href="ChangePassword.aspx" class="btn-link" style="align-content: flex-end">Change Password</a>
    </div>
       
    <div class="container"> 
        <div class="row">
            <div class="jumbotron">
                <h2>Beta Lifestyle</h2>
                <hr />
                <b>Slides here</b>
            </div>
         
        </div>
    </div>
    <div>
        <asp:Button  runat="server" ID="btnchangepassword" CssClass="btn btn-primary" OnClick="btnchangepassword_Click"/>
    </div>

    <div class="container jumbotron" >
        <div class="thumbnail col-md-3"  style="height:200px">
             <p >dynamic product</p>
  
</div>
           <div class="thumbnail col-md-3" style="height:200px;">
        <p >dynamic product</p>
</div>
         <div class="thumbnail col-md-3"  style="height:200px;">
            
          <p >dynamic product</p>
</div>
           <div class="thumbnail col-md-3"  style="height:200px;">
            
        <p >dynamic product</p>
</div>


    </div>
</asp:Content>
