<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/RentalMaster.Master" AutoEventWireup="true" CodeBehind="TransctionLogs.aspx.cs" Inherits="RentalProject.Administrator.TransctionLogs" %>

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
                    <li><a href="#">Administrator</a></li>
                    <li>ِTransaction Log(s)</li>
                </ul>
                <h4>ِTransaction Log(s)</h4>
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
                    <label class="control-label">From Date</label>
                    <asp:TextBox ID="txtfromdate" runat="server" class="form-control"></asp:TextBox>
                </div>
            </div>


            <div class="col-sm-3">

                <div class="form-group">
                    <label class="control-label">To Date</label>
                    <asp:TextBox ID="txttodate" runat="server" class="form-control"></asp:TextBox>
                </div>
            </div>


            <div class="col-sm-3">
                <div class="form-group">
                    <label class="control-label" for="ddlUser">Transaction</label>
                    <br />
                    <div class="col-sm-10">
                        <asp:DropDownList ID="ddltransactionLog" CssClass="width300 select" data-placeholder="Choose Transaction" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddltransactionLog_SelectedIndexChanged" required="required"
                            x-moz-errormessage="Select Transaction"
                            placeholder="Select Transaction">
                            <asp:ListItem Selected="True" Value="0">Select Transaction</asp:ListItem>
                            <asp:ListItem Value="1">Users</asp:ListItem>
                            <asp:ListItem Value="2">Rentals</asp:ListItem>
                            <asp:ListItem Value="3">Owners</asp:ListItem>
                            <asp:ListItem Value="4">Apartment</asp:ListItem>
                            <asp:ListItem Value="5">Contract</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <br />
            <br />
            <br />
            <hr />


            <div class="panel-body">
                <div class="table-responsive ">
                    <asp:Repeater ID="RptTransactionLog" runat="server">
                        <HeaderTemplate>
                            <table id="basicTable" class="table table-striped table-bordered responsive" style="table-layout: fixed;">
                                <thead>
                                    <tr>
                                        <th>Transaction Log Id</th>
                                        <th>Transaction Date
                                        </th>
                                        <th>User Name
                                        </th>
                                        <th>English Action
                                        </th>
                                        <th>Arabic Action
                                        </th>
                                        <th><%= ddltransactionLog.SelectedItem.Text.Remove(ddltransactionLog.SelectedItem.Text.Length - 1)%> Name</th>
                                    </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("TransactionLogId")%></td>
                                <td><%#Eval("TransactionDate")%></td>
                                <td><%#Eval("TransactionFullName")%></td>
                                <td><%#Eval("TransactionLogEnglishName")%></td>
                                <td><%#Eval("TransactionLogArabicName")%></td>
                                <td><a href='TransactionLogDetails.aspx?TransactionLogId=<%#Eval("TransactionLogId")%>' class="fancyLink"><%#Eval("TransactionLogForFullName")%></a></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:HiddenField ID="HFDeleteId" runat="server" />
                </div>
            </div>

        </div>



    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            jQuery(".select").select2({
            });

            $('#<%= txtfromdate.ClientID %>').datetimepicker({
                Dateformat: 'dd MM yyyy',

                timepicker: false
            });

            $('#<%= txttodate.ClientID %>').datetimepicker({
                Dateformat: 'dd MM yyyy',

                timepicker: false

            });


        });





    </script>
</asp:Content>
