<%@ Page Title="" Language="C#" MasterPageFile="~/Beta.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="BetaLifeStyle.Private.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .box {
            background-color: white;
            padding-top: 10px;
            padding-bottom: 10px;
            padding-right: 20px;
            padding-left: 20px;
            border: 1px solid #f3f3f3;
        }
    </style>
   
    <div class="row">
         <asp:Panel ID="checkoutok" runat="server"  CssClass="col-sm-12 col-md-9">
            <div >
                <div class="panel panel-default">
                    <div class="panel-heading">Order Details</div>
                    <div class="panel-body">
                        <div runat="server" id="Products">
                        </div>
                        <br/>
                        <asp:Label runat="server" ID="outofstocklbl" CssClass="h6" style="color:#939393"></asp:Label>
                    </div>
                    <div class="panel-footer" style="height: 56px !important">
                        <asp:Button ID="Checkoutt" runat="server" OnClick="Checkoutt_Click" Cssclass="btn btn-primary pull-right" Text="Checkout"></asp:Button>
                    </div>
                </div>
            </div>
         </asp:Panel>
        <asp:Panel ID="filladdress" runat="server" CssClass="col-sm-12 col-md-9" >
            <div >
                <div class="panel panel-default">
                    <div class="panel-heading">Invalid Address</div>
                    <div class="panel-body">
                        An Invalid Address is found Please Recheck your address and try checkout again, Thank you.
                    </div>
                    <div class="panel-footer" style="height: 56px !important">
                        <asp:Button ID="GotoProfile" runat="server" OnClick="GotoProfile_Click" Cssclass="btn btn-primary pull-right" Text="Go to Profile"></asp:Button>
                    </div>
                </div>
            </div>
        </asp:Panel>



        <div class="col-sm-12 col-md-3">
            <div class="panel panel-default">
                 <div class="panel-heading">100% Secure Payment</div>
                <img src="../icons/100secure.png" class="img-responsive" style="padding:20px;"/>
                
            </div>
        </div>
    </div>
        
</asp:Content>
