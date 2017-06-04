<%@ Page AutoEventWireup="true" CodeBehind="TransactionLogDetails.aspx.cs" Inherits="RentalProject.Administrator.TransactionLogDetails" Language="C#" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../_Design/css/style.default.css" rel="stylesheet" />
    <link href="../_Design/css/jquery.fancybox.css" rel="stylesheet" />
    <link href="../_Design/css/morris.css" rel="stylesheet" />
    <link href="../_Design/css/select2.css" rel="stylesheet" />
    <link href="../_Design/css/jquery.gritter.css" rel="stylesheet" />
    <link href="../_Design/dynatree/skin-vista/ui.dynatree.css" rel="stylesheet" type="text/css" />
    <link href="../_Design/css/style.datatables.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="comments-container">
            <div class="comment-avatar">
                <asp:Image ID="Imgprw" runat="server" ImageUrl="~/Photos/Users/profile.png" alt="Image before upload" ClientIDMode="Static" ToolTip='<%#Eval("FullName")%>' />
            </div>
            <div class="btn-group-sm small-icon">
                <div class="btn tops  badge badge-info"><%=Sign%></div>
            </div>
            <asp:Repeater ID="RptDetails" runat="server" Visible="false">
                <HeaderTemplate>
                    <div class="col-md-6">
                        <div class="table-responsive">
                            <table class="table table-primary mb30">
                                <thead>
                                    <tr>
                                        <th>Full Name</th>
                                        <th>Register Date</th>
                                        <th>Phone</th>
                                        <th>SSN</th>
                                        <th>Email</th>
                                        <th>UserName</th>
                                        <th>Password</th>
                                    </tr>
                                </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tbody>
                        <tr>

                            <td><%#Eval("FullName")%></td>
                            <td><%#Eval("Dated")%></td>
                            <td><%#Eval("Phone")%></td>
                            <td><%#Eval("SSN")%></td>
                            <td><%#Eval("Email")%></td>
                            <td><%#Eval("UserName")%></td>
                            <td><%#Eval("Password")%></td>
                        </tr>
                    </tbody>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                        </div>
                    </div>
                </FooterTemplate>
            </asp:Repeater>


            <asp:Repeater ID="RptUnits" runat="server" Visible="false">
                <HeaderTemplate>
                    <table id="basicTable" class="table table-striped table-bordered responsive" style="table-layout: fixed;">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Unit No
                                </th>
                                <th>Unit Name
                                </th>
                                <th>Street
                                </th>

                                <th>Unit Type
                                </th>

                                <th>Price
                                </th>

                                <th>Owner
                                </th>

                                <th>Description
                                </th>

                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("UnitId")%></td>
                        <td><%#Eval("UnitNumber")%></td>
                        <td><%#Eval("UnitName")%></td>
                        <td><%#Eval("Street")%></td>
                        <td><%#Eval("UnitType")%></td>
                        <td><%#Eval("UnitId")%></td>
                        <td><%#Eval("Owner")%></td>
                        <td><%#Eval("Description")%></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>


            <asp:Repeater ID="rptContract" runat="server" Visible="false">
                <HeaderTemplate>
                    <table id="basicTable" class="table table-striped table-bordered responsive" style="table-layout: fixed;">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>StartDate
                                </th>
                                <th>EndDate
                                </th>
                                <th>Price
                                </th>

                                <th>Description
                                </th>

                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("ContractId")%></td>
                        <td><%#Eval("StartDate")%></td>
                        <td><%#Eval("EndDate")%></td>
                        <td><%#Eval("Price")%></td>
                        <td><%#Eval("Description")%></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>


        </div>
    </form>
</body>
</html>
