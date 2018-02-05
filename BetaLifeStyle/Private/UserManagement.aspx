<%@ Page Title="" Language="C#" MasterPageFile="~/Beta.Master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="BetaLifeStyle.Private.UserManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/Pagination.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row clearmp">

        <div class="col-sm-3 pull-right">
            <div class="input-group  input-group-sm" >
                <input type="text" id="searchtxtbox" class="form-control" placeholder="Search" />
                <a class="input-group-addon" id="searchbtn" onclick="SearchUser();">
                    <span class="fa fa-search"/>
                </a>
            </div>
        </div>
            
        
        <div style="margin-top:50px;">
            <table class="table table-bordered table-condensed  table-hover" style="margin-bottom:0px;background-color: #fff; word-break: break-word" id="UserManagementTable">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Email</th>
                        <th>IsActive</th>
                        <th>UserRole</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody class="tbody"></tbody>
            </table>
        </div>

            <div class="input-group input-group-sm col-sm-2">
                <a class="input-group-addon" id="btnPrevious"><span class=" fa fa-angle-left"></span></a>
                <input title="Only Digits" class="form-control" pattern="^[0-9]\d*$" id="TxtPageNumber" />
                <div class="input-group-addon">
                    <label style="margin-bottom: 0">/</label>
                    <label class="control-label" style="margin-bottom: 0" id="lblTotalPage"></label>
                </div>
                <a class="input-group-addon" id="btnNext"><span class=" fa fa-angle-right"></span></a>
            </div>
        
    </div>

</asp:Content>
