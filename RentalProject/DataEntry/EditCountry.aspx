<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/RentalMaster.Master" AutoEventWireup="true" CodeBehind="EditCountry.aspx.cs" Inherits="RentalProject.DataEntry.EditCountry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitlePlaceHolder" runat="server">
    <div class="pageheader">
        <div class="media">
            <div class="pageicon pull-left">
                <i class="fa fa-hand-o-up"></i>
            </div>
            <div class="media-body">
                <ul class="breadcrumb">
                    <li><a href="/default.aspx"><i class="glyphicon glyphicon-home"></i></a></li>
                    <li><a href="#">Data Entry</a></li>
                    <li>ِManage Countries</li>
                </ul>
                <h4>Manage Countries</h4>
            </div>
        </div>
        <!-- media -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="panel panel-primary">
        <div class="panel-body">

            <div class="col-sm-3">
                <div class="form-group">
                    <label class="control-label">Sort Name</label>
                    <asp:TextBox ID="txtSortName" runat="server" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtSortName" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Sort Name Required"></asp:RequiredFieldValidator>
                </div>

            </div>

            <div class="col-sm-3">
                <div class="form-group">
                    <label class="control-label">English Name</label>
                    <asp:TextBox ID="txtEnglishName" runat="server" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                        ControlToValidate="txtEnglishName" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="English Name Required"></asp:RequiredFieldValidator>
                </div>

            </div>

            <div class="col-sm-3">
                <div class="form-group">
                    <label class="control-label">Arabic Name</label>
                    <asp:TextBox ID="txtArabicName" runat="server" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ControlToValidate="txtArabicName" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Arabic Name Required"></asp:RequiredFieldValidator>
                </div>

            </div>


            <div class="col-sm-3">
                <div class="form-group">
                    <label class="control-label">Phone Code</label>
                    <asp:TextBox ID="txtPhoneCode" runat="server" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                        ControlToValidate="txtPhoneCode" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Phone Code Required"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="txtPhoneCode" SetFocusOnError="true"
                        ForeColor="Red" ErrorMessage="enter 4 digits" validationexpression="^[0-9]{4}$"  ValidationGroup="Users"></asp:RegularExpressionValidator>
                </div>
            </div>
        </div>

        <div class="panel-footer">
            <asp:Button runat="server" class="btn btn-primary mr5" OnClick="btnSubmit_Click" ID="btnSubmit" Text="Submit" ValidationGroup="Users" />


        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
