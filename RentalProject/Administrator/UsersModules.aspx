<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/RentalMaster.Master" AutoEventWireup="true" CodeBehind="UsersModules.aspx.cs" Inherits="RentalProject.Administrator.UsersModules" %>


<asp:Content ID="Content2" ContentPlaceHolderID="PageTitlePlaceHolder" runat="server">
    <div class="pageheader">
        <div class="media">
            <div class="pageicon pull-left">
                <i class="fa fa-hand-o-up"></i>
            </div>
            <div class="media-body">
                <ul class="breadcrumb">
                    <li><a href="/default.aspx"><i class="glyphicon glyphicon-home"></i></a></li>
                    <li><a href="#">Admin Setting</a></li>
                    <li>User Modules</li>
                </ul>
                <h4>User Modules</h4>
            </div>
        </div>
        <!-- media -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainPlaceHolder" runat="server">

    <div class="panel panel-default form-bordered">
        <div class="panel-heading">
            <div class="panel-btns">
            </div>
            <!-- panel-btns -->
            <h4 class="panel-title">Note</h4>
            <p>Provide the Authoritation (Permission) for Which allowed Users can  Access The Pages .</p>
        </div>
        <!-- panel-heading -->
        <div class="panel-body nopadding">

            <div class="form-group">
                <label class="col-sm-2 control-label" for="ddlUser">User</label>
                <div class="col-sm-10">
                    <asp:DropDownList ID="ddlUser" CssClass="width300 select-basic" data-placeholder="Choose User" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged" required="required"
                        x-moz-errormessage="Select User"
                        placeholder="Select User">
                    </asp:DropDownList>

                </div>
            </div>

            <div class="form-group">
                <label class="col-sm-2 control-label">Pages</label>
                <div class="col-sm-10" id="tree">
                    <ul id="treeData">
                        <asp:Literal runat="server" ID="litPageTree" />
                    </ul>
                </div>
            </div>



        </div>

        <div class="panel-footer">
            <asp:Button runat="server" class="btn btn-primary mr5" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="Group" />

            <asp:TextBox runat="server" ID="txtSelectedNode" Style="display: none" Text="0"></asp:TextBox>
            <asp:TextBox runat="server" ID="txtTreeSelectedNodes" Style="display: none" Text=""></asp:TextBox>

        </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
   
    <script type="text/javascript">

        function pageSetup() {
            setTree();
        }

        function setTree() {

            jQuery(".select-basic").select2();
            jQuery("#tree").dynatree({
                checkbox: true,
                selectMode: 3,
                onSelect: function (select, node) {
                    var node = jQuery("#tree").dynatree("getSelectedNodes");
                    // ... and convert to a key array:
                    var selRootKeys = jQuery.map(node, function (nodeValue) {
                        return nodeValue.data.key;
                    });

                    var partsel = [];
                    jQuery(".dynatree-partsel:not(.dynatree-selected)").each(function () {
                        var nodeSelected = jQuery.ui.dynatree.getNode(this);
                        partsel.push(nodeSelected.data.key);
                    });

                    selRootKeys = selRootKeys.concat(partsel);
                    jQuery('#<%= txtTreeSelectedNodes.ClientID%>').val(selRootKeys.join(","));
                }

            });
        }
        function pageLoad() { pageSetup(); }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (sender, args) {
            pageSetup();
        });

        jQuery(document).ready(function () {
            pageSetup();
        });

        function checkUserPrivilege(userData) {
            if (userData) {

                setTree();

                jQuery("#tree").dynatree("getRoot").visit(function (dtnode) {
                    dtnode.select(false);
                });

                var pageIds = jQuery.parseJSON('[' + userData + ']');


                jQuery.each(pageIds, function (i, item) {

                    jQuery("#tree").dynatree("getTree").selectKey(item.toString());
                });
            }
        }


    </script>
</asp:Content>

