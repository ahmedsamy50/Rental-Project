<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebsiteMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RentalProject.WebSite.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
   
    <nav class="navbar">
        <div class="container">
            <!-- Brand and toggle get grouped for better mobile display -->
            <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="#">
                    <img src="../_Design/Website/img/logo.png" data-active-url="img/logo-active.png" alt=""></a>
            </div>
            <!-- Collect the nav links, forms, and other content for toggling -->
            <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                <ul class="nav navbar-nav navbar-right main-nav">
                    <%--   <li><a href="#intro">Intro</a></li>--%>
                    <li><a href="#Owners">Our Owners
                    </a></li>
                    <%--   <li><a href="#team">Team</a></li>--%>
                    <li><a href="#ContactUs">Contact Us</a></li>
                    <li><a href="#" data-toggle="modal" data-target="#modal1" class="btn btn-blue">Sign In</a></li>
                </ul>
            </div>
            <!-- /.navbar-collapse -->
        </div>
        <!-- /.container-fluid -->
    </nav>
    <header id="intro">
        <div class="container">
            <div class="table">
                <div class="header-text">
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <h3 class="light white">Take care of your Real Estats.</h3>
                            <h1 class="white typed">It's the  best way to pay and collect real estats online.</h1>
                            <span class="typed-cursor">|</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </header>

    <section id="Owners" class="section section-padded">
        <div class="container">
            <div class="row text-center title">
                <h2>Our  Owners</h2>
            </div>
            <div class="row services">

                <asp:Repeater ID="RptAdvertising" runat="server">
                    <HeaderTemplate>

                        <thead>
                        </thead>
                    </HeaderTemplate>
                    <ItemTemplate>

                        <div class="col-md-2">
                            <div class="service">
                                <asp:Image ID="Imgprw2" runat="server" ImageUrl='<%#Eval("Image")%>' Height="300px" Width="100px" alt="Image before upload" ClientIDMode="Static" />
                            </div>
                        </div>

                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>

        <div class="cut cut-bottom"></div>
    </section>
    <section id="team" class="section gray-bg">
    </section>
    <footer id="ContactUs">
        <div class="container">
            <div class="row">
                <h3 class="white">Contact Us</h3>
                <h6 class="white">if you have any questions,Please contact me </h6>
                <h6 class="white">i will replay you as soon as possible .</h6>
                <div class="col-md-6 wow fadeInUp" data-wow-delay="0.6s">
                    <div class="contact-form">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="txtName" class="form-control" placeholder="Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorName" ControlToValidate="txtName" ValidationGroup="ContactUs" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Name is required"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                        runat="server" Display="dynamic"
                                        ControlToValidate="txtName"
                                        ForeColor="Red"
                                        ValidationGroup="ContactUs"
                                        SetFocusOnError="true"
                                        ValidationExpression="^([\S\s]{0,50})$"
                                        ErrorMessage="Please enter maxium 50 characters for name">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="txtEmail" class="form-control" placeholder="Email"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtEmail" ValidationGroup="ContactUs" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Email is required"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                        runat="server" Display="dynamic"
                                        ControlToValidate="txtEmail"
                                        ForeColor="Red"
                                        ValidationGroup="ContactUs"
                                        SetFocusOnError="true"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ErrorMessage="Please enter availd email address">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="txtSubject" class="form-control" placeholder="Subject"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtSubject" ValidationGroup="ContactUs" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Subject is required"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                        runat="server" Display="dynamic"
                                        ControlToValidate="txtSubject"
                                        ForeColor="Red"
                                        ValidationGroup="ContactUs"
                                        SetFocusOnError="true"
                                        ValidationExpression="^([\S\s]{0,100})$"
                                        ErrorMessage="Please enter maxium 50 characters for subject">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="txtmessage" TextMode="MultiLine" class="form-control" placeholder="Message"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtmessage" ValidationGroup="ContactUs" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Message is required"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4"
                                        runat="server" Display="dynamic"
                                        ControlToValidate="txtmessage"
                                        ForeColor="Red"
                                        ValidationGroup="ContactUs"
                                        SetFocusOnError="true"
                                        ValidationExpression="^([\S\s]{0,200})$"
                                        ErrorMessage="Please enter maxium 50 characters for message">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <br />
                                <div runat="server" id="divSucess" visible="false" class="white"></div>
                                <div runat="server" id="diverror" visible="false" class="white"></div>
                                <div class="col-md-12">
                                    <asp:Button runat="server" ID="btnSend" OnClick="btnSend_Click" Text="Send" ValidationGroup="ContactUs" type="submit" class="form-control text-uppercase" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-md-6">
                    Image Address<h6 class="white">Address : Address</h6>
                    Image Phone<h6 class="white">Phone : Phone</h6>
                    Image Email<h6 class="white">Email : Email</h6>
                    <iframe class="actAsDiv" width="600" height="500" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="https://maps.google.com/maps?f=q&amp;source=s_q&amp;hl=en&amp;q=Adobe%20Systems%20Inc%2C%20Park%20Avenue%2C%20San%20Jose%2C%20CA&amp;aq=0&amp;ie=UTF8&amp;t=m&amp;z=12&amp;iwloc=A&amp;output=embed"></iframe>
                </div>
            </div>
            <div class="row bottom-footer text-center-mobile">
                <div class="col-sm-8">
                    <p>&copy; 2015 All Rights Reserved. Powered by <a href="http://www.phir.co/">PHIr</a> exclusively for <a href="http://tympanus.net/codrops/">Codrops</a></p>
                </div>
                <div class="col-sm-4 text-right text-center-mobile">
                    <ul class="social-footer">
                        <li><a href="http://www.facebook.com/pages/Codrops/159107397912"><i class="fa fa-facebook"></i></a></li>
                        <li><a href="http://www.twitter.com/codrops"><i class="fa fa-twitter"></i></a></li>
                        <li><a href="https://plus.google.com/101095823814290637419"><i class="fa fa-google-plus"></i></a></li>
                    </ul>
                </div>
            </div>
        </div>
    </footer>


    <div class="modal fade" id="modal1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content modal-popup">
                <a href="#" class="close-link"><i class="icon_close_alt2"></i></a>
                <h3 class="white">Sign In</h3>
                <div runat="server" id="divMsgError" visible="false" class="alert alert-danger"></div>
                <asp:TextBox runat="server" ID="txtuersname" ValidationGroup="SignIn" class="form-control form-white" placeholder="User Name"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtuersname" ValidationGroup="SignIn" ForeColor="Red" SetFocusOnError="true" ErrorMessage="User Name is required"></asp:RequiredFieldValidator>
                <asp:TextBox runat="server" TextMode="Password" ID="txtpassword" ValidationGroup="SignIn" class="form-control form-white" placeholder="Password"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtpassword" ValidationGroup="SignIn" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Password is required"></asp:RequiredFieldValidator>
              <%--  <div class="checkbox-holder text-left">
                    <div class="checkbox">
                        <asp:CheckBox runat="server" onclick="disableOwner();" value="1" ID="CheckboxRentak" Checked="true" />
                        <label for="MainContentPlaceHolder_CheckboxRentak"><span><strong>Rental</strong></span></label>
                    </div>
                </div>
                <div class="checkbox-holder text-left">
                    <div class="checkbox">
                        <asp:CheckBox runat="server" onclick="disableRental();" value="2" ID="CheckboxOwner" />
                        <label for="MainContentPlaceHolder_CheckboxOwner"><span><strong>Owner</strong></span></label>
                    </div>
                </div>--%>

                <asp:Button runat="server"  ID="btnSignIn"  OnClick="btnSignIn_Click" type="submit" ValidationGroup="SignIn" Text="Sign In" class="btn btn-submit"></asp:Button>

            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScriptPlaceHolder" runat="server">
    <script type="text/javascript">
        function showPopup() {
            $('#modal1').modal('show');
        }
        //Near checkboxes
       // function disableOwner() {
        //    $('#MainContentPlaceHolder_CheckboxOwner').prop('checked', false);
        //}
        //function disableRental() {
        //    $('#MainContentPlaceHolder_CheckboxRentak').prop('checked', false);
        //}
    </script>
</asp:Content>


