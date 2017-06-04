<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/RentalMaster.Master" AutoEventWireup="true" CodeBehind="ShowCurrency.aspx.cs" Inherits="RentalProject.DataEntry.ShowCurrency" %>

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
                    <li>ِManage Currency</li>
                </ul>
                <h4>Manage Currency</h4>
            </div>
        </div>
        <!-- media -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="panel panel-default form-bordered">
        <div class="panel-body">
            <div class="table-responsive ">
                <asp:repeater id="RptUsers" runat="server">
                <HeaderTemplate>
                    <table id="basicTable" class="table table-striped table-bordered responsive" style="table-layout: fixed;">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Code</th>
                                <th>English Name
                                </th>
                                <th>Arabic Name
                                </th>
                                <th>Update
                                </th>
                                <th>Delete
                                </th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("CurrencyId")%></td>
                        <td><%#Eval("Code")%></td>
                        <td><%#Eval("EnglishName")%></td>
                        <td><%#Eval("ArabicName")%></td>
                        <td><a href='EditCurrency.aspx?CurrencyId=<%#Eval("CurrencyId")%>' class="btn btn-primary">Update</a></td>
                        <td><a href='#myModal' data-toggle="modal" class="btn btn-primary" onclick="SetId(<%#Eval("CurrencyId")%>)">Delete</a></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:repeater>
                <asp:hiddenfield id="HFDeleteId" runat="server" />
            </div>
        </div>

        <div class="panel-footer">
            <a href='EditCurrency.aspx' class="btn btn-primary"><i class="fa fa-user"></i>Add New</a>
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
                    <asp:button id="BtnUpdateOpen" runat="server" text="Save Change(s)" onclick="BtnUpdateOpen_Click"
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

