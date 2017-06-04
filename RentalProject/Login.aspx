<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RentalProject.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Login Page</title>
    <link href="_Design/css/style.default.css" rel="stylesheet" />
    <script src="_Design/css/bootstrap.css"></script>
    <link href="_Design/css/style.default.css" rel="stylesheet" />
    <link href="_Design/css/jquery.fancybox.css" rel="stylesheet" />
    <script src="_Design/js/jquery-1.11.1.min.js"></script>
    <script src="_Design/js/jquery-migrate-1.2.1.min.js"></script>
    <script src="_Design/js/bootstrap.min.js"></script>
    <script src="_Design/js/modernizr.min.js"></script>
    <script src="_Design/js/pace.min.js"></script>
    <script src="_Design/js/retina.min.js"></script>
    <script src="_Design/js/jquery.cookies.js"></script>
    <script src="_Design/js/jquery.fancybox.pack.js"></script>
    <script src="_Design/js/custom.js"></script>
</head>
<body class="signin">
    <section>
        <div class="panel panel-signin">
            <div class="panel-body">
                <div class="logo text-center">
                    <img src="Logo/smsma.jpg" alt="Chain Logo" style="width: 100px; height: 100px" class="img-circle" />
                </div>
                <br />
                <h4 class="text-center mb5">Already a Member?</h4>
                <p class="text-center">Sign in to your account</p>

                <div class="mb30"></div>

                <form runat="server" id="form1">
                    <div runat="server" id="divMsgError" visible="false" class="alert alert-danger"></div>
                    <asp:RequiredFieldValidator ControlToValidate="TxtUserName" ID="RequiredFieldValidator1" runat="server" ValidationGroup="Login" ErrorMessage="User Name is required" ForeColor="Red"></asp:RequiredFieldValidator>
                    <div class="input-group mb15">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:TextBox ID="TxtUserName" class="form-control" runat="server" placeholder="User Name"></asp:TextBox>

                    </div>

                    <!-- input-group -->
                    <asp:RequiredFieldValidator ControlToValidate="TxtPassword" ID="RequiredFieldValidator2" runat="server" ValidationGroup="Login" ErrorMessage="Password is required" ForeColor="Red"></asp:RequiredFieldValidator>
                    <div class="input-group mb15">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                        <asp:TextBox ID="TxtPassword" runat="server" class="form-control" TextMode="Password" placeholder="Password"></asp:TextBox>
                    </div>

                    <!-- input-group -->
                    <div class="clearfix">


                        <div class="pull-left">
                            <div class="ckbox ckbox-primary mt10">
                                <input type="checkbox" id="chkRemember" value="1" runat="server" />
                                <label for="chkRemember">Remember Me</label>
                            </div>
                        </div>
                        <div class="pull-right">
                            <asp:Button ID="btnLogin" CssClass="btn btn-success" OnClick="btnLogin_Click" runat="server" Text="Login" ValidationGroup="Login" />
                        </div>
                    </div>
                </form>

            </div>


            <!-- panel-body -->
            <div class="panel-footer">
                <a href="#" target="_blank">Forgot Password ?</a>
                <%--  <a href="#" target="_blank">Arabic Language ?</a>--%>
            </div>

            <!-- panel-footer -->
        </div>
        <!-- panel -->
    </section>

    <script src="_Design/js/jquery-1.11.1.min.js"></script>
    <script src="_Design/js/jquery-migrate-1.2.1.min.js"></script>
    <script src="_Design/js/bootstrap.min.js"></script>
    <script src="_Design/js/modernizr.min.js"></script>
    <script src="_Design/js/pace.min.js"></script>
    <script src="_Design/js/retina.min.js"></script>
    <script src="_Design/js/jquery.cookies.js"></script>
    <script src="_Design/js/custom.js"></script>
</body>

</html>
