<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/RentalMaster.Master" AutoEventWireup="true" CodeBehind="ShowAdvertising.aspx.cs" Inherits="RentalProject.WebSite.ShowAdvertising" %>

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
                    <li><a href="#">Web Site</a></li>
                    <li>ِAdvertising</li>
                </ul>
                <h4>Manage Advertising</h4>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="panel panel-default form-bordered">
        <div class="panel-body">
            <div class="table-responsive ">
                <asp:Repeater ID="RptAdvertising" runat="server">
                    <HeaderTemplate>
                        <table id="basicTable" class="table table-striped table-bordered responsive" style="table-layout: fixed;">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>StartDate
                                    </th>
                                    <th>EndDate
                                    </th>
                                    <th>Active
                                    </th>
                                    <th>Priority
                                    </th>
                                    <th>Image
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

                            <td><%#Eval("AdvertisingId")%></td>
                            <td><%#Eval("StartDate", "{0:d/M/yyyy}")%></td>
                            <td><%#Eval("EndDate", "{0:d/M/yyyy}")%></td>
                            <td><%#Eval("Active")%></td>
                            <td><%#Eval("Priority")%></td>
                            <td>
                                <asp:Image ID="Imgprw2" runat="server" ImageUrl='<%#Eval("Image")%>' Height="80px" Width="80px" alt="Image before upload" ClientIDMode="Static" /></td>
                            </td>
                            <td><a href='EditAdvertising.aspx?AdvertisingId=<%#Eval("AdvertisingId")%>' class="btn btn-primary">Update</a></td>
                            <td><a href='#myModal' data-toggle="modal" class="btn btn-primary" onclick="SetId(<%#Eval("AdvertisingId")%>)">Delete</a></td>
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
            <a href='EditAdvertising.aspx' class="btn btn-primary"><i class="fa fa-user"></i>Add New</a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
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
