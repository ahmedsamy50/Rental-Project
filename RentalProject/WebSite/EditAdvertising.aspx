<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/RentalMaster.Master" AutoEventWireup="true" CodeBehind="EditAdvertising.aspx.cs" Inherits="RentalProject.WebSite.EditAdvertising" %>

<asp:Content ID="Content3" ContentPlaceHolderID="PageTitlePlaceHolder" runat="server">
    
    <%--<script type="text/javascript" src="../_Design/Datetime/jquery.js"></script>--%>
   
    <div class="pageheader">
        <div class="media">
            <div class="pageicon pull-left">
                <i class="fa fa-hand-o-up"></i>
            </div>
            <div class="media-body">
                <ul class="breadcrumb">
                    <li><a href="/default.aspx"><i class="glyphicon glyphicon-home"></i></a></li>
                    <li><a href="#">Administrator</a></li>
                    <li>ِManage Advertising(s)</li>
                </ul>
                <h4>Manage Advertising(s)</h4>
            </div>
        </div>
        <!-- media -->
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="panel panel-primary">


        <div class="panel-body">
            <div class="form-group">
                <label class="control-label">Start Date</label>
                <asp:TextBox ID="txtstartdate" runat="server" class="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="txtstartdate" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Start Date Required"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group">

                <label class="control-label">End Date: </label>

                <asp:TextBox ID="txtenddate" runat="server" class="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                    ControlToValidate="txtenddate" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="End Date Required"></asp:RequiredFieldValidator>

            </div>

            <div class="form-group">
                <label class="control-label">Priority</label>
                <asp:TextBox ID="txtPriority" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                    ControlToValidate="txtPriority" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Priority number Required"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group">
                <label class="control-label">Image</label>
                <asp:FileUpload ID="ImageUpload" runat="server" class="form-control" type="file" name="file1" onchange="ValidateSingleInput(this);imagepreview2(this),validateFileSize(this)" />
                <asp:Image ID="Imgprw2" runat="server" Height="80px" Width="80px" alt="Image before upload" ClientIDMode="Static" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                    ControlToValidate="ImageUpload" ForeColor="Red" ValidationGroup="Users" SetFocusOnError="true" ErrorMessage="Image Required"></asp:RequiredFieldValidator>
            </div>


            <div class="form-group">
                <div class="ckbox ckbox-primary mt10">
                    <input type="checkbox" id="ChkActive" value="1" checked="" runat="server" />
                    <label for="ChkActive">Active</label>
                </div>
            </div>


        </div>
        <div class="panel-footer">
            <asp:Button runat="server" class="btn btn-primary mr5" ID="btnSubmit" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="Users" />
        </div>
    </div>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            jQuery(".select").select2({
            });

            $('#<%= txtstartdate.ClientID %>').datetimepicker({
                Dateformat: 'dd MM yyyy',
                timepicker: false
               
            });

            $('#<%= txtenddate.ClientID %>').datetimepicker({
                Dateformat: 'dd MM yyyy',
                timepicker: false

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
