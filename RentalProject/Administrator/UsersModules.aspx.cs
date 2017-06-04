using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RentalDataAccess;
using RentalProject.Classes;
using System.Text;

namespace RentalProject.Administrator
{

    public partial class UsersModules : BasePage
    {
        private UserClass UserCls = null;
        public string clientPages = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserCls = Authentication.GetUserFromSessionOrFromTicket();
                if (UserCls != null)
                {
                    litPageTree.Text = GetPagesAsTreeHierarchy();
                    LoadUsers();
                }
                else

                    Response.Redirect("~/login.aspx");
            }
        }

        private string GetPagesAsTreeHierarchy()
        {
            StringBuilder sp = new StringBuilder();
            using (dbRentalsEntities ctx = new dbRentalsEntities())
            {
                try
                {
                    List<Modules> pageList = ctx.Modules.OrderBy(o => o.Priority).ToList();

                    if (pageList != null && pageList.Count > 0)
                    {
                        List<Modules> parentLst = new List<Modules>();

                        parentLst = pageList.FindAll(obj => obj.ParentId == 0);

                        sp.Append("<li id='0' class='active focused'>Root</li>");
                        /// For all the parent pages , The menu will add an item with the page.
                        for (int i = 0; i < parentLst.Count; i++)
                        {
                            if (pageList.FirstOrDefault(obj => obj.ParentId == parentLst[i].ModuleId) != null)
                            {
                                sp.Append("<li id='" + parentLst[i].ModuleId + "' class='folder'>" + parentLst[i].Name);
                            }
                            else
                            {
                                sp.Append("<li id='" + parentLst[i].ModuleId + "'>" + parentLst[i].Name);
                            }

                            sp.Append(AddChildTreeItems(pageList, Convert.ToInt32(parentLst[i].ModuleId)));
                            sp.Append("</li>");
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                return sp.ToString();
            }
        }

        private static string AddChildTreeItems(List<Modules> treeLst, int parentId)
        {
            StringBuilder spx = new StringBuilder();
            try
            {
                List<Modules> ChildLst = new List<Modules>();
                ChildLst = treeLst.FindAll(obj => obj.ParentId == parentId);
                if (ChildLst.Count > 0)
                {
                    /// For all the child pages under the parentID , The menu will add an item with the page.
                    spx.Append("<ul>");
                    for (int i = 0; i < ChildLst.Count; i++)
                    {
                        if (treeLst.FirstOrDefault(obj => obj.ParentId == ChildLst[i].ModuleId) != null)
                        {
                            spx.Append("<li id='" + ChildLst[i].ModuleId + "' class='folder'>" + ChildLst[i].Name);
                        }
                        else
                        {
                            spx.Append("<li id='" + ChildLst[i].ModuleId + "'> " + ChildLst[i].Name);
                        }
                        spx.Append(AddChildTreeItems(treeLst, Convert.ToInt32(ChildLst[i].ModuleId)));
                    }
                    spx.Append("</ul>");
                    spx.Append("</li>");
                }
                else
                {
                    spx.Append("</li>");
                }
            }
            catch
            {

            }

            return spx.ToString();
        }

        private void LoadUsers()
        {
            using (dbRentalsEntities ctx = new dbRentalsEntities())
            {
                var users = ctx.Users.Where(x=> x.UserId != 30 && x.Deleted == false).Select(o => new { o.UserId, UserName = o.FullName }).Distinct().OrderBy(o => o.UserName).ToList();
                ddlUser.DataSource = users;
                ddlUser.DataTextField = "UserName";
                ddlUser.DataValueField = "UserId";
                ddlUser.DataSource = users;
                ddlUser.DataBind();
                ddlUser.Items.Insert(0, new ListItem("Select User", "0"));
            }
        }

        protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUser.SelectedIndex != -1 && ddlUser.SelectedValue != "0")
            {

                using (dbRentalsEntities ctx = new dbRentalsEntities())
                {
                    try
                    {
                        Int32 _UserID = Convert.ToInt32(ddlUser.SelectedValue);
                        List<Int32> pagesId = ctx.Modules.Where(o => o.Users.Any(i => i.UserId == _UserID) && o.ParentId != 0).Select(o => o.ModuleId).ToList();
                        ScriptManager.RegisterStartupScript(btnSubmit, typeof(string), "a",
                                                            "checkUserPrivilege('" + String.Join(",", pagesId) +
                                                            "');", true);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (ddlUser.SelectedIndex != -1 && ddlUser.SelectedValue != "0")
                {
                    using (dbRentalsEntities ctx = new dbRentalsEntities())
                    {
                        try
                        {
                            StringBuilder sb = new StringBuilder();
                            string[] pageIds = txtTreeSelectedNodes.Text.Split(new char[] { ',' },
                                                                               StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < pageIds.Length; i++)
                            {
                                if (pageIds[i] != "0")
                                {
                                    sb.Append(
                                        string.Format(" INSERT INTO UsersModules(ModuleId,UserId) VALUES({0},{1}); ",
                                                      pageIds[i], ddlUser.SelectedValue));
                                }
                            }

                            ctx.Database.ExecuteSqlCommand("DELETE FROM UsersModules WHERE UserId=" +
                                                           ddlUser.SelectedValue + "; " + sb.ToString());

                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "success('User Modules Saved',' Success','growl-success');", true);
                            ddlUser_SelectedIndexChanged(null, null);
                        }
                        catch (Exception ex)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "danger('User Users Modules Faile to Save','Error','growl-danger');", true);
                        }
                    }
                }
                else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "danger('Select The User','Error','growl-danger');", true);
            }
        }
    }
}