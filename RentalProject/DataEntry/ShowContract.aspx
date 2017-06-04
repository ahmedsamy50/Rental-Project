<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/RentalMaster.Master" AutoEventWireup="true" CodeBehind="ShowContract.aspx.cs" Inherits="RentalProject.DataEntry.ShowContract" %>

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
                    <li><a href="#">Contracts</a></li>
                    <li>ِManage Contracts</li>
                </ul>
                <h4>Manage Contracts</h4>
            </div>
        </div>
        <!-- media -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="panel panel-default form-bordered">
        <div class="panel-body">
            <div class="table-responsive ">
                <asp:Repeater ID="RptContracts" runat="server">
                    <HeaderTemplate>
                        <table id="basicTable" class="table table-striped table-bordered responsive" style="table-layout: fixed;">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Owner
                                    </th>
                                    <th>Rental
                                    </th>
                                    <th>Unit
                                    </th>
                                    <th>Start Month
                                    </th>

                                    <th>End Month
                                    </th>

                                    <th>Email Service
                                    </th>

                                    <th>SMS Service 
                                    </th>
                                    <th>Price
                                    </th>
                                    <th>Active
                                    </th>
                                  <%--  <th runat="server" id="trUpdate">Update
                                    </th>--%>
                                    <th runat="server" id="trDelete">Delete
                                    </th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("ContractId")%></td>
                            <td><%#Eval("Owner")%></td>
                            <td><%#Eval("Rental")%></td>
                            <td><%#Eval("Unit")%></td>
                            <td><%# Eval("StartDate", "{0:M/yyyy}") %></td>
                            <td><%# Eval("EndDate", "{0:M/yyyy}") %></td>
                            <td><%#Eval("EmailService")%></td>
                            <td><%#Eval("SMSService")%></td>
                            <td><%#Eval("Price")%></td>
                            <td><%#Eval("Active")%></td>
                            <% if (IsAdmin() == true)
                                { %>
                            <%--<td><a href='EditContract.aspx?ContractId=<%#Eval("ContractId")%>' class="btn btn-primary">Update</a></td>--%>
                            <td><a href='#myModal' data-toggle="modal" class="btn btn-primary" onclick="SetId(<%#Eval("ContractId")%>)">Delete</a></td>

                            <% } %>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:HiddenField ID="HFDeleteId" runat="server" />
            </div>
        </div>

        <div class="panel-footer">
            <a href='EditContract.aspx' class="btn btn-primary"><i class="fa fa-user"></i>Add New</a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="myModalLabel">Confirmation Message</h4>
                </div>
                <div class="modal-body">
                    Are you sure ?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <asp:Button ID="BtnUpdateOpen" runat="server" Text="Save Change(s)" OnClick="BtnUpdateOpen_Click"
                        class="btn btn-primary" formnovalidate="formnovalidate" />
                </div>
            </div>
            <!-- modal-content -->
        </div>


        <!-- modal-dialog -->
    </div>

    <script type="text/javascript">
        function SetId(Id) {
            var myHidden = document.getElementById('<%= HFDeleteId.ClientID %>');
            if (myHidden)//checking whether it is found on DOM, but not necessary
            {
                myHidden.value = Id;
            }
        }
    </script>
</asp:Content>
