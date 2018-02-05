<%@ Page Title="" Language="C#" MasterPageFile="~/Beta.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="BetaLifeStyle.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-lg-3"></div>
            <div class="col-lg-6" style="border: 5px solid gray; padding: 10px; background-color: white;">
                <div class="text-center" style="font-family: Verdana;">
                    <h4>
                        <asp:Label ID="Label1" runat="server" Text="Personal Details"></asp:Label></h4>
                </div>
                <div class="form-inline" style="font-family: Verdana">
                    <h5>
                        <asp:Label ID="Label2" runat="server" Text="Welcome "></asp:Label></h5>
                    <asp:Label ID="Label3" runat="server"></asp:Label>

                </div>
                <br />

                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                    <asp:TextBox ID="txt_Firstname" Class="form-control" runat="server" Placeholder="First Name"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="FirstName" runat="server" ErrorMessage="First Name Is Required" ControlToValidate="txt_Firstname" ForeColor="Red" ValidationGroup="ValGroup1"></asp:RequiredFieldValidator>


                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                    <asp:TextBox ID="txt_Lastname" runat="server" Class="form-control" Placeholder="Last Name"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="LastName" runat="server" ErrorMessage="Last Name Is Required" ControlToValidate="txt_Lastname" ForeColor="Red" ValidationGroup="ValGroup1"></asp:RequiredFieldValidator>
                <br />


                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-envelope"></i></span>
                    <asp:TextBox ID="txt_Email" runat="server" Class="form-control" Placeholder="Email" ReadOnly="True" ValidationGroup="ValGroup1"></asp:TextBox>
                </div>
                <br />

                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-phone"></i></span>
                    <asp:TextBox ID="txt_Phone" runat="server" Class="form-control" Placeholder="Phone"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="Phone" runat="server" ErrorMessage="Phone Number Is Required" ControlToValidate="txt_Phone" ValidationGroup="ValGroup1"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="PhoneNumber" runat="server" ControlToValidate="txt_Phone" ValidationExpression="^(((\+?64\s*[-\.]?[3-9]|\(?0[3-9]\)?)\s*[-\.]?\d{3}\s*[-\.]?\d{4})|((\+?64\s*[-\.\(]?2\d{1}[-\.\)]?|\(?02\d{1}\)?)\s*[-\.]?\d{3}\s*[-\.]?\d{3,5})|((\+?64\s*[-\.]?[-\.\(]?800[-\.\)]?|[-\.\(]?0800[-\.\)]?)\s*[-\.]?\d{3}\s*[-\.]?(\d{2}|\d{5})))$" ForeColor="Red"></asp:RegularExpressionValidator>
                <br />

                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-home"></i></span>
                    <asp:TextBox ID="txt_Address" runat="server" Class="form-control" Placeholder="Address"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="Address" runat="server" ErrorMessage="Address is Required" ControlToValidate="txt_Address" ForeColor="Red" ValidationGroup="ValGroup1"></asp:RequiredFieldValidator>


                <div class="input-group">
                    <span class="input-group-addon"><b>City</b></span>
                    <asp:TextBox ID="txt_City" runat="server" Class="form-control" Placeholder="City"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="City1" runat="server" ErrorMessage="Enter the City" ControlToValidate="txt_City" ValidationGroup="ValGroup1" ForeColor="Red"></asp:RequiredFieldValidator>

                <div class="input-group">
                    <span class="input-group-addon"><b>Suburb</b></span>
                    <asp:TextBox ID="txt_State" runat="server" Class="form-control" Placeholder="Suburb"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="Suburb1" runat="server" ErrorMessage="Enter the Suburb" ControlToValidate="txt_State" ForeColor="Red" ValidationGroup="ValGroup1"></asp:RequiredFieldValidator>

                <div class="input-group">
                    <span class="input-group-addon"><b>Pincode</b></span>
                    <asp:TextBox ID="txt_Pincode" runat="server" Class="form-control" Placeholder="Pincode"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="Pincode1" runat="server" ErrorMessage="Enter the Pincode" ControlToValidate="txt_Pincode" ForeColor="Red" ValidationGroup="ValGroup1"></asp:RequiredFieldValidator>

                <div class="input-group">
                    <span class="input-group-addon"><b>Country</b></span>
                    <asp:TextBox ID="txt_Country" runat="server" Class="form-control" Placeholder="Country"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="Country1" runat="server" ErrorMessage="Enterthe Country Name" ControlToValidate="txt_Country" ForeColor="Red" ValidationGroup="ValGroup1"></asp:RequiredFieldValidator>

                <div class="form-inline text-center">
                    <asp:Button ID="Submit" runat="server" Width="30%" Text="Submit" CssClass="btn btn-primary"  OnClick="Submit_Click" ValidationGroup="ValGroup1" />
                    <a id="BtnCancel" runat="server" width="50%" href="UserProfile.aspx" class="btn btn-primary">Cancel</a>
                </div>
            </div>
            <div class="col-lg-3"></div>
        </div>
    </div>
</asp:Content>
    