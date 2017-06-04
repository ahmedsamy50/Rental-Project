<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/RentalMaster.Master" AutoEventWireup="true" CodeBehind="EditSubApartment.aspx.cs" Inherits="RentalProject.DataEntry.EditSubApartment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        legend.scheduler-border {
            width: inherit; /* Or auto */
            padding: 0 10px; /* To give a bit of padding on the left and right */
            border-bottom: none;
        }

        fieldset.scheduler-border {
            border: 1px groove #ddd !important;
            padding: 0 1.4em 1.4em 1.4em !important;
            margin: 0 0 1.5em 0 !important;
            -webkit-box-shadow: 0px 0px 0px 0px #000;
            box-shadow: 0px 0px 0px 0px #000;
        }
    </style>
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
                    <li>ِManage Sub Apartments</li>
                </ul>
                <h4>Manage Sub Apartments</h4>
            </div>
        </div>
        <!-- media -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainPlaceHolder" runat="server">

    <div class="panel panel-primary">
        <div class="panel-body">


            <fieldset class="scheduler-border">
                <legend class="scheduler-border">Appartment
                </legend>


                <div class="form-group">
                    <label class="control-label">Owner</label>
                    <br />
                    <div class="col-sm-10">
                        <asp:DropDownList ID="DDLOwner" OnSelectedIndexChanged="DDLOwner_SelectedIndexChanged" CssClass="width300 select" runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0"
                            ControlToValidate="DDLOwner" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Owner Required"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label">Appartment</label>
                    <br />
                    <div class="col-sm-10">
                        <asp:DropDownList ID="DDLAppartment" CssClass="width300 select" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue="0"
                            ControlToValidate="DDLAppartment" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Appartment Required"></asp:RequiredFieldValidator>
                    </div>
                </div>


            </fieldset>



            <fieldset class="scheduler-border">
                <legend class="scheduler-border">Sub Appartment
                </legend>


                <div class="form-group">
                    <label class="control-label">Appartment Type</label>
                    <br />
                   
                        <asp:DropDownList ID="DDLAppartmentType" CssClass="width300 select" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0"
                            ControlToValidate="DDLAppartmentType" ForeColor="Red" ValidationGroup="Add" SetFocusOnError="true" ErrorMessage="Appartment Type Required"></asp:RequiredFieldValidator>
                   
                </div>

                <div class="form-group">
                    <label class="control-label">Apartment Number</label>
                    <asp:TextBox ID="txtunitnumber" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ControlToValidate="txtunitnumber" ForeColor="Red" ValidationGroup="Add" SetFocusOnError="true" ErrorMessage="Apartment Number Required"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label class="control-label">Apartment Name</label>
                    <asp:TextBox ID="txtunitName" runat="server" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                        ControlToValidate="txtunitName" ForeColor="Red" ValidationGroup="Add" SetFocusOnError="true" ErrorMessage="Apartment Name Required"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label class="control-label">Description</label>
                    <asp:TextBox ID="txtDescription" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                </div>

                <asp:UpdatePanel runat="server" ID="Updatepanelunit">
                    <ContentTemplate>

                   

                <div class="form-group">
                    <asp:Button runat="server" OnClick="btnAdd_Click" class="btn btn-primary mr5" ValidationGroup="Add" ID="btnAdd" Text="Add" />
                </div>



                <asp:Repeater ID="RptUnit" runat="server">
                    <HeaderTemplate>
                        <table  class="table table-striped table-bordered responsive" style="table-layout: fixed;">
                            <thead>
                                <tr>
                                    <th>Apartment No
                                    </th>
                                    <th>Apartment Name
                                    </th>
                                    <th>Apartment Type
                                    </th>
                                    <th>Description
                                    </th>
                                    <th>Delete
                                    </th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("UnitNumber")%></td>
                            <td><%#Eval("UnitName")%></td>
                            <td><%#Eval("EnglishName")%> _ <%#Eval("ArabicName")%></td>
                            <td><%#Eval("Description")%></td>
                            <td>
                                <asp:LinkButton ID="LinkDelete" runat="server" ToolTip='<%# Eval("UnitNumber") %>' OnClick="LinkDelete_Click" Text="Delete" CommandArgument='<%# Eval("UnitNumber") %>'></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>


                 </ContentTemplate>
                </asp:UpdatePanel>

            </fieldset>













        </div>
        <div class="panel-footer">
            <asp:Button runat="server" class="btn btn-primary mr5" ID="btnSubmit" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="Users" />
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
