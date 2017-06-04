<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/RentalMaster.Master" AutoEventWireup="true" CodeBehind="ShowUsers.aspx.cs" Inherits="RentalProject.Administrator.ShowUsers" %>




<asp:Content ID="Content3" ContentPlaceHolderID="PageTitlePlaceHolder" runat="server">
    <div class="pageheader">
        <div class="media">
            <div class="pageicon pull-left">
                <i class="fa fa-hand-o-up"></i>
            </div>
            <div class="media-body">
                <ul class="breadcrumb">
                    <li><a href="/default.aspx"><i class="glyphicon glyphicon-home"></i></a></li>
                    <li><a href="#">Admin Setting</a></li>
                    <li>ِManage User(s)</li>
                </ul>
                <h4>Manage User(s)</h4>
            </div>
        </div>
        <!-- media -->
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="panel panel-primary">


        <div class="panel-body">




            <div class="form-group">
                <label class="col-sm-2 control-label">Name</label>
                <asp:TextBox ID="txtfirstname" runat="server" class="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="txtfirstname" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Name Required"></asp:RequiredFieldValidator>
            </div>





            <div class="form-group">

                <label class="col-sm-2 control-label">Email: </label>

                <asp:TextBox ID="txtemail" runat="server" class="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                    ControlToValidate="txtemail" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Email Required"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ForeColor="Red" ValidationGroup="Users" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtemail" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>

            </div>





            <div class="form-group">
                <label class="col-sm-2 control-label">Phone Number</label>
                <asp:TextBox ID="txtphone" runat="server" class="form-control"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                    ControlToValidate="txtphone" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Phone Required"></asp:RequiredFieldValidator>
            </div>




            <div class="form-group">
                <label class="col-sm-2 control-label">SSN</label>
                <asp:TextBox ID="txtssn" runat="server" class="form-control"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                    ControlToValidate="txtssn" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="SSN Required"></asp:RequiredFieldValidator>
            </div>


            <div class="form-group">
                <label class="col-sm-10 control-label" for="DDLNationality">Nationality</label>
                <asp:DropDownList ID="DDLNationality" CssClass="width300 select" data-placeholder="Choose Nationality" runat="server" required="required"
                    x-moz-errormessage="Select Nationality"
                    placeholder="Select Transaction">
                </asp:DropDownList>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0"
                    ControlToValidate="DDLNationality" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Nationality Required"></asp:RequiredFieldValidator>
            </div>





            <div class="form-group">
                <label class="col-sm-2 control-label">Login Name</label>
                <asp:TextBox ID="txtusername" runat="server" class="form-control"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                    ControlToValidate="txtusername" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Login Name Required"></asp:RequiredFieldValidator>
            </div>




            <div class="form-group">
                <label class="col-sm-2 control-label">Password</label>
                <asp:TextBox ID="txtpassword" runat="server" class="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                    ControlToValidate="txtpassword" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Password Required"></asp:RequiredFieldValidator>
            </div>


            
            <div class="form-group">
                <label class="control-label">Image</label>
                <asp:FileUpload ID="ImageUpload" runat="server" class="form-control" type="file" name="file1" onchange="ValidateSingleInput(this);imagepreview2(this),validateFileSize(this)" />
                <asp:Image ID="Imgprw2"  runat="server" Height="80px" Width="80px" alt="Image before upload"  ClientIDMode="Static" />
            </div>
           


            <div class="form-group">
                <div class="ckbox ckbox-primary mt10">
                    <input type="checkbox" id="chkRemember" value="1" checked="checked" runat="server" />
                    <label for="chkRemember">Active</label>
                </div>
            </div>




        </div>
        <div class="panel-footer">
            <asp:Button runat="server" class="btn btn-primary mr5" OnClick="btnSubmit_Click" ID="btnSubmit" Text="Submit" ValidationGroup="Users" />
        </div>
    </div>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            jQuery(".select").select2({
            });
        });

    </script>

     <script type="text/javascript">
        function imagepreview2(input) {
            if (input.files && input.files[0]) {
                var fildr = new FileReader();
                fildr.onload = function (e) {
                    $('#Imgprw2').attr('src', e.target.result);
                }
                fildr.readAsDataURL(input.files[0]);
            }
        }


    </script>

    <script type="text/javascript">

        var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
        function ValidateSingleInput(oInput) {
            if (oInput.type == "file") {
                var sFileName = oInput.value;
                if (sFileName.length > 0) {
                    var blnValid = false;
                    for (var j = 0; j < _validFileExtensions.length; j++) {
                        var sCurExtension = _validFileExtensions[j];
                        if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                            blnValid = true;
                            break;
                        }
                    }

                    if (!blnValid) {
                        alert("Extension Allowed " + _validFileExtensions.join(" , "));
                        oInput.value = "";
                        return false;
                    }
                }
            }
            return true;
        }

        function validateFileSize(oInput) {
            var uploadControl = document.getElementById('<%= ImageUpload.ClientID %>');
            if (uploadControl.files[0].size > 1048576) // 5 MB = 5242880)
            {
                alert("File Size is greater than 1 MB");
                {
                    oInput.value = "";
                    return false;
                }

            }
        }
    </script>
</asp:Content>
