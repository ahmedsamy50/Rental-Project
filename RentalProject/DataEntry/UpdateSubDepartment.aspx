<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/RentalMaster.Master" AutoEventWireup="true" CodeBehind="UpdateSubDepartment.aspx.cs" Inherits="RentalProject.DataEntry.UpdateSubDepartment" %>

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
                    <li>ِManage Update Sub Apartments</li>
                </ul>
                <h4>Manage Update Sub Apartments</h4>
            </div>
        </div>
        <!-- media -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="panel panel-primary">
        <div class="panel-body">

            <div class="form-group">
                <label class="control-label">Apartment Type</label>
                <br />
                <asp:DropDownList ID="DDLUnittype" CssClass="width300 select" data-placeholder="Choose Apartment Type" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0"
                    ControlToValidate="DDLUnittype" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Apartment Type Required"></asp:RequiredFieldValidator>

            </div>
            <div class="form-group">
                <label class="control-label">Apartment Number</label>
                <asp:TextBox ID="txtunitnumber" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="txtunitnumber" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Apartment Number Required"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label class="control-label">Apartment Name</label>
                <asp:TextBox ID="txtunitName" runat="server" class="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                    ControlToValidate="txtunitName" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Apartment Name Required"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label class="control-label">Description</label>
                <asp:TextBox ID="txtDescription" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
            </div>

            <div class="panel-footer">
                <asp:Button runat="server" class="btn btn-primary mr5" OnClick="btnSubmit_Click" ID="btnSubmit" Text="Submit" ValidationGroup="Users" />


            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            jQuery(".select").select2({
            });
        });

    </script>
</asp:Content>
