<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/RentalMaster.Master" AutoEventWireup="true" CodeBehind="ChangePicture.aspx.cs" Inherits="RentalProject.DataEntry.ChangePicture" %>

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
                    <li>ِManage Picture</li>
                </ul>
                <h4>Manage Picture</h4>
            </div>
        </div>
        <!-- media -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="panel panel-default">

        <div class="panel-body ">
            <div class="alert alert-success">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                <strong>Note</strong> Change the Picture of your profile or set as default picture , effect changes after next sign In 
                               
            </div>

            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label class="control-label">Image</label>
                        <asp:FileUpload ID="ImageUpload" runat="server" class="form-control" type="file" name="file1" onchange="ValidateSingleInput(this);imagepreview2(this),validateFileSize(this)" />
                        <asp:Image ID="Imgprw2" runat="server" Height="80px" Width="80px" alt="Image before upload" ClientIDMode="Static" />
                    </div>
                    <!-- table-responsive -->
                    <asp:Button ID="BtnUpload" runat="server" class="btn btn-primary pull-right mr5 mb10" Text="Save Changes" OnClick="BtnUpload_Click" />
                    <asp:Button ID="BtnRemove" runat="server" class="btn btn-dark pull-right mr5 mb10" Text="Remove Picture" OnClick="BtnRemove_Click" />
                </div>

            </div>

        </div>

    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">

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
