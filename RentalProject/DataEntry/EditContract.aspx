<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/RentalMaster.Master" AutoEventWireup="true" CodeBehind="EditContract.aspx.cs" Inherits="RentalProject.DataEntry.EditContract" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../_Design/Zebra_DatePicker/css/bootstrap.css" rel="stylesheet" />
    <link href="../_Design/Zebra_DatePicker/css/default.css" rel="stylesheet" />
    <link href="../_Design/Zebra_DatePicker/css/metallic.css" rel="stylesheet" />
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
                    <li>ِManage Contract</li>
                </ul>
                <h4>Manage Contract</h4>
            </div>
        </div>
        <!-- media -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="panel panel-primary">


        <div class="panel-body">



            <div class="form-group">
                <label class="col-sm-10 control-label" for="DDLOwner">Owner</label>
                <asp:DropDownList ID="DDLOwner" CssClass="width300 select" data-placeholder="Choose Owner" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLOwner_SelectedIndexChanged"
                    x-moz-errormessage="Select Owner"
                    placeholder="Select Owner">
                </asp:DropDownList>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0"
                    ControlToValidate="DDLOwner" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Owner Required"></asp:RequiredFieldValidator>
            </div>
            <asp:UpdatePanel runat="server" ID="UpdatePanelAppartment">
                <ContentTemplate>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">Apartment Type(s)</label>
                        <div class="col-sm-8">
                            <div class="rdio rdio-primary">
                                <asp:RadioButton runat="server" ID="radioapartment" AutoPostBack="true" OnCheckedChanged="radioapartment_CheckedChanged" Checked="true" />
                                <label for="MainPlaceHolder_radioapartment">Apartment</label>
                            </div>
                            <div class="rdio rdio-primary">
                                <asp:RadioButton runat="server" Checked="false" ID="radiosubapptment" AutoPostBack="true" OnCheckedChanged="radiosubapptment_CheckedChanged" />
                                <label for="MainPlaceHolder_radiosubapptment">Sub Apartment</label>
                            </div>
                        </div>


                    </div>


                    <div class="form-group">
                        <label class="col-sm-10 control-label" for="DDLUnits">Apartment</label>
                        <asp:DropDownList ID="DDLUnits" CssClass="width300 select" data-placeholder="Choose Units" runat="server" SelectionMode="Multiple"
                            x-moz-errormessage="Select Apartment"
                            placeholder="Select Apartment">
                        </asp:DropDownList>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0"
                            ControlToValidate="DDLUnits" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Unit Required"></asp:RequiredFieldValidator>
                    </div>


                    <div class="form-group">
                        <asp:Button runat="server" OnClick="btnAdd_Click" class="btn btn-primary mr5" ValidationGroup="Add" ID="btnAdd" Text="Add" />
                    </div>



                    <asp:Repeater ID="RptUnit" runat="server">
                        <HeaderTemplate>
                            <table class="table table-striped table-bordered responsive" style="table-layout: fixed;">
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
                                    <asp:LinkButton ID="LinkDelete" runat="server" ToolTip='<%# Eval("UnitNumber") %>' OnClick="LinkDelete_Click" Text="Delete" CommandArgument='<%# Eval("UnitId") %>'></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>



            <div class="form-group">
                <label class="col-sm-10 control-label" for="DDLRental">Rental</label>
                <asp:DropDownList ID="DDLRental" CssClass="width300 select" data-placeholder="Choose Rental" runat="server"
                    x-moz-errormessage="Select Rental"
                    placeholder="Select Rental">
                </asp:DropDownList>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0"
                    ControlToValidate="DDLRental" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Rental Required"></asp:RequiredFieldValidator>
            </div>

            <div class="alert alert-success" runat="server" id="Message">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                <strong>Note</strong> Start Date Must be greter than the end date ,  and the diffrence between must also equal at least 1 month
           
            </div>

            <div class="form-group">
                <label class="control-label">Start Date</label>
                <asp:TextBox ID="txtstartdate" runat="server" class="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                    ControlToValidate="txtstartdate" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Start Date Required"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group">
                <label class="control-label">End Date: </label>
                <asp:TextBox ID="txtenddate" runat="server" class="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                    ControlToValidate="txtenddate" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="End Date Required"></asp:RequiredFieldValidator>

            </div>

            <div class="form-group">
                <label class="control-label">Price/Month</label>
                <asp:TextBox ID="txtPrice" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                    ControlToValidate="txtPrice" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Price Required"></asp:RequiredFieldValidator>
            </div>




            <div class="form-group">
                <label class="col-sm-10 control-label" for="DDLCurnency">Curency</label>
                <asp:DropDownList ID="DDLCurnency" CssClass="width300 select" data-placeholder="Choose Curency" runat="server"
                    x-moz-errormessage="Select Curency"
                    placeholder="Select Curency">
                </asp:DropDownList>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" InitialValue="0"
                    ControlToValidate="DDLCurnency" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Curency Required"></asp:RequiredFieldValidator>
            </div>


            <div class="form-group">
                <div class="ckbox ckbox-primary mt10">
                    <input type="checkbox" id="ChkEmailService" value="1" checked="" runat="server" />
                    <label for="ChkEmailService">Email Service</label>
                </div>
            </div>

            <div class="form-group">
                <div class="ckbox ckbox-primary mt10">
                    <input type="checkbox" id="ChkSMSService" value="1" checked="" runat="server" />
                    <label for="ChkSMSService">SMS Service</label>
                </div>
            </div>

            <div class="form-group">
                <div class="ckbox ckbox-primary mt10">
                    <input type="checkbox" id="ChkActive" value="1" checked="" runat="server" />
                    <label for="ChkActive">Active</label>
                </div>
            </div>


            <div class="form-group">
                <label class="control-label">Description</label>
                <asp:TextBox ID="txtdescription" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
            </div>

        </div>


        <div class="panel-footer">
            <asp:Button runat="server" class="btn btn-primary mr5" OnClick="btnSubmit_Click" ID="btnSubmit" Text="Submit" ValidationGroup="Users" />




        </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script src="../_Design/Zebra_DatePicker/javascript/zebra_datepicker.js"></script>
    <script src="../_Design/Zebra_DatePicker/javascript/zebra_datepicker.src.js"></script>


    <script type="text/javascript">
        $(document).ready(function () {
            jQuery(".select").select2({
            });

            $('#<%=txtstartdate.ClientID %>').Zebra_DatePicker({
                format: 'm Y'   //  note that because there's no day in the format
                //  users will not be able to select a day!
            });

            $('#<%=txtenddate.ClientID %>').Zebra_DatePicker({
                format: 'm Y'   //  note that because there's no day in the format
                //  users will not be able to select a day!
            });

        });




    </script>

</asp:Content>
