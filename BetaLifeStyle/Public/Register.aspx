<%@ Page Title="" Language="C#" MasterPageFile="~/Beta.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="BetaLifeStyle.Account.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="table table-bordered" style="height:450px">
       
        <table style="margin-left: 30%; height:50%; margin-top:25px; border-collapse: separate; border-spacing: 0.25em; margin-right: 0px;">
              <tr>
                <td colspan="3" style="text-align:center">
                       REGISTER 
                </td>
            </tr>
     
            <tr>
                <td class="text-right">
                    <asp:Label ID="lbl1"  runat="server" Text="Email:" />
                </td>

                <td>
                    <asp:TextBox ID="TxtEmail"  placeholder="Email"  CssClass="form-control" runat="server"  ></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Email is required" ControlToValidate="TxtEmail" Display="Dynamic" ForeColor="Red">Email is required</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="TxtEmail" Display="Dynamic" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">use valid Email</asp:RegularExpressionValidator>
                </td>
            </tr>
             <tr>
                <td class="text-right">
                    <asp:Label ID="lbl2" runat="server" Text="Password:" />
                </td>

                <td>
                    <asp:TextBox ID="TxtPassword"  placeholder="Password"  CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Password is required" ControlToValidate="TxtPassword" Display="Dynamic" ForeColor="Red">Password is required</asp:RequiredFieldValidator>
                    
                </td>
            </tr>
             <tr>
                <td class="text-right">
                    <asp:Label ID="lbl3"  runat="server" Text="ConfirmPassword:" />
                </td>

                <td>
                    <asp:TextBox ID="TxtConfirmPassword"  placeholder="ConfirmPassword" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="ConfirmPassword is required" ControlToValidate="TxtConfirmPassword" Display="Dynamic" ForeColor="Red">ConfirmPassword is required</asp:RequiredFieldValidator>
                    <br />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Passwords must Match" ControlToCompare="TxtPassword" ControlToValidate="TxtConfirmPassword" Display="Dynamic" ForeColor="Red">Passwords must Match</asp:CompareValidator>
                </td>
            </tr>
             
              <tr>
                <td colspan="3">
                    <asp:Label ID="lblMessage" runat="server" ></asp:Label> 
                </td>
            </tr>
            <tr class="success">
                
                <td >
                    <asp:Button ID="BtnRegister"  runat="server" Text="Register" class="btn btn-group-sm btn-primary btn" OnClick="BtnRegister_Click"/>   
                </td>
                <td colspan="2">
                    <asp:LinkButton ID="ForgetPassword" runat="server">Forget Password</asp:LinkButton>
                </td>
            </tr>

        </table>

    </div>
</asp:Content>
