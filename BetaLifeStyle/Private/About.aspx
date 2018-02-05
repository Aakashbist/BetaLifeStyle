<%@ Page Title="About" Language="C#" MasterPageFile="~/Beta.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="BetaLifeStyle.About" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#Lgbtn").click(function () {
                $.ajax({
                    type: "POST",
                    url: "About.aspx/Page_Load",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(msg) {
                        // Replace the div's content with the page method's return.                       
                    }})
            })
                    })
        
       
    </script>
 <div class="container">

  
  <button type='button' style="margin-top:20px;" class="btn btn-success" onclick="btnlgn" data-toggle="modal" data-target="#popUpWindow">LogIn</button>
  
  <div class="modal fade" id="popUpWindow">
    <div class="modal-dialog">
      <div class="modal-content">
        <!-- header -->
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title">Login Form</h4>
        </div>
        <!-- body -->
        <div class="modal-header">
         <%-- <form role="form">--%>
            <div class="form-group">
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
              <%--<input type="email" id="txtmail" class="form-control" placeholder="Email"/>
              <input type="password" id="txtpass" class="form-control" placeholder="Password" />--%>
            </div>
         <%-- </form>--%>
        </div>
        <!-- footer -->
        <div class="modal-footer">
          <button  id="Lgbtn"  class="btn btn-primary btn-block">Log In</button>
        </div>
        
      </div>
    </div>
  </div>
  
</div>
    
   

</asp:Content>
