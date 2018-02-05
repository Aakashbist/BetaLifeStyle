<%@ Page Title="" Language="C#" MasterPageFile="~/Beta.Master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="BetaLifeStyle.UserManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container col-xs-6" style="background-color:#f8f8f8; margin-top:100px; margin-left:400px ">
        <div class="row">
        
        <div class="col-xs-12 ">
            <div class="form-horizontal">
              <div class="form-group bg-primary">                 
                  <div class="col-xs-12">
                  <h3>Create Admin</h3>
                  </div>                 
              </div> 
                 
              <div class="form-group">               
                <label>Name</label>
                <div>
                    <asp:TextBox CssClass="form-control" ID="TxtName" runat="server"></asp:TextBox>
                </div>             
              </div>

            <div class="form-group">              
                <label>Email</label>
                <div >
                    <asp:TextBox CssClass="form-control" textmode="Email" ID="TxtEmail" runat="server"></asp:TextBox>
                </div>              
            </div>
  
          <div class="form-group">             
                <label>Password</label>
                <div>
                    <asp:TextBox CssClass="form-control" TextMode="Password" ID="TxtPassword" runat="server"></asp:TextBox>
                </div>              
            </div>
       
              <div class="form-group">              
                <div>
                  <asp:Button  Text="Create" ID="AddAdmin" runat="server" class="btn btn-success" OnClick="AddAdmin_Click" />
                </div>               
              </div>
                
            <div class="form-group">
                <div class="col-sm-8 text-center">
                  <asp:Label ID="lblmessage"  runat="server" Text=""></asp:Label>
                </div>
                <div class="col-xs-2"></div>
            </div>

          </div>
        </div>
</div></div>
</asp:Content>
