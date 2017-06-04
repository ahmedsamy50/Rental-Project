<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/RentalMaster.Master" AutoEventWireup="true" CodeBehind="EditUnitPrice.aspx.cs" Inherits="RentalProject.DataEntry.EditUnitPrice" %>

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
                    <li>ِManage Unit Price</li>
                </ul>
                <h4>Manage Unit Price</h4>
            </div>
        </div>
        <!-- media -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="panel panel-primary">
        <div class="panel-body">

            <div class="alert alert-success">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                <strong>Note</strong> Price Per Month 
                               
            </div>

            <div class="form-group">
                <label class="control-label">Owner</label>
                <br />
                <div class="col-sm-10">
                    <asp:DropDownList ID="DDLOwner" CssClass="width300 select" data-placeholder="Choose Owner" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLOwner_SelectedIndexChanged"
                        x-moz-errormessage="Select Owner"
                        placeholder="Select Owner">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0"
                        ControlToValidate="DDLOwner" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Owner Required"></asp:RequiredFieldValidator>
                </div>
            </div>


            <div class="form-group">
                <label class="control-label">Unit</label>
                <br />
                <div class="col-sm-10">
                    <asp:DropDownList ID="DDLUnit" CssClass="width300 select" data-placeholder="Choose Unit" runat="server"
                        x-moz-errormessage="Select Unit"
                        placeholder="Select Transaction">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0"
                        ControlToValidate="DDLUnit" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Unit Required"></asp:RequiredFieldValidator>
                </div>
            </div>




            <div class="form-group">
                <label class="control-label">Curency</label>
                <br />
                <div class="col-sm-10">
                    <asp:DropDownList ID="DDLCurency" CssClass="width300 select" data-placeholder="Choose Curency" runat="server"
                        x-moz-errormessage="Select Curency"
                        placeholder="Select Transaction">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0"
                        ControlToValidate="DDLCurency" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Curency Required"></asp:RequiredFieldValidator>
                </div>
            </div>




            <div class="form-group">
                <label class="control-label">Price/Month</label>
                <asp:TextBox ID="txtprice" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                    ControlToValidate="txtprice" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Price Required"></asp:RequiredFieldValidator>
            </div>



        </div>

        <div class="panel-footer">
            <asp:Button runat="server" class="btn btn-primary mr5" OnClick="btnSubmit_Click" ID="btnSubmit" Text="Submit" ValidationGroup="Users" />


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
