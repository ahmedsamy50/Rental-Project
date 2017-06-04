using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RentalDataAccess;
using RentalProject.Classes;
using System.Text;

namespace RentalProject
{
    public partial class LeftMenu : System.Web.UI.UserControl
    {
        private UserClass UserCls = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            // search on menu
            if (SessionWrapper.MenuControl == null)
            {

                List<Modules> parentLst = new List<Modules>();
                List<Modules> pageLst = null;

                using (dbRentalsEntities ctx = new dbRentalsEntities())
                {
                    pageLst = ctx.Modules.Where(o => o.Active == true && o.URL != null).OrderBy(o => o.Priority).ToList();
                }
                UserCls = Authentication.GetUserFromSessionOrFromTicket();
                dbRentalsEntities db = new dbRentalsEntities();
                List<int> userModule = (List<int>)db.Modules.Where(x => x.Users.Any(u => u.UserId == UserCls._UserId)).Select(x => x.ModuleId).ToList();
                var userLinks = (from ul in pageLst
                                 where userModule.Contains(Convert.ToInt32(ul.ModuleId)) && ul.Active == true
                                 select ul).ToList();
                StringBuilder sp = new StringBuilder();
                if (!IsPostBack)
                {
                    parentLst = userLinks.FindAll(obj => obj.ParentId == 0);
                    /// For all the parent pages , The menu will add an item with the page.
                    for (int i = 0; i < parentLst.Count; i++)
                    {
                        string Icon = "";
                        if (parentLst[i].ModuleId == 2)
                            Icon = "fa fa-file-text";
                        else if (parentLst[i].ModuleId == 7)
                            Icon = "fa fa-edit";
                        else
                            Icon = "fa fa-edit";
                        sp.Append(string.Format("<li  class='parent'><a href='#'><i class='" + Icon + "'></i><span>{1}</span></a>", string.IsNullOrEmpty(parentLst[i].Name) ? "iconfa-th-list" : parentLst[i].Name, parentLst[i].Name));
                        sp.Append(AddChildMenuItems(userLinks, Convert.ToInt32(parentLst[i].ModuleId)));
                        sp.Append("</li>");
                    }
                    SessionWrapper.MenuControl = sp.ToString();
                    litNav.Text = sp.ToString();
                }
            }
            else
            {
                litNav.Text = SessionWrapper.MenuControl;
            }

        }


        private string AddChildMenuItems(List<Modules> MenuLst, int parentID)
        {
            StringBuilder spx = new StringBuilder();
            try
            {
                List<Modules> ChildLst = new List<Modules>();
                ChildLst = MenuLst.FindAll(obj => obj.ParentId == parentID && obj.Active == true);
                if (ChildLst.Count > 0)
                {
                    /// For all the child pages under the parentID , The menu will add an item with the page.
                    spx.Append("<ul class='children'>");
                    for (int i = 0; i < ChildLst.Count; i++)
                    {
                        string url = ChildLst[i].URL == "#" ? "#" : ResolveUrl(ChildLst[i].URL);
                        spx.Append("<li> <a href='" + url + "'>" + ChildLst[i].Name + "  </a>");
                        spx.Append(AddChildMenuItems(MenuLst, Convert.ToInt32(ChildLst[i].ModuleId)));
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

    }
}