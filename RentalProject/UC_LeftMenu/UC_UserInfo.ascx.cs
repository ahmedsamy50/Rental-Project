using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RentalDataAccess;
using RentalProject.Classes;
using System.Text;


namespace RentalProject.UC_LeftMenu
{
    public partial class UC_UserInfo : System.Web.UI.UserControl
    {
        private UserClass UserCls = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionWrapper.MenuCategiries == null)
            {
                using (dbRentalsEntities ctx = new dbRentalsEntities())
                {
                    StringBuilder sp = new StringBuilder();
                    sp.Append("<div class='media profile-left nomargin'>");
                    sp.Append("<a class='pull-left profile-thumb' href='#'>");
                    UserCls = Authentication.GetUserFromSessionOrFromTicket();
                    if (UserCls != null)
                    {
                        string URL = @"..\Photos\Users\" + UserCls._Photo;
                        sp.Append("<img id='Imgprw88' class='img-circle' alt='' src=" + URL + " /></a>");
                        sp.Append("<div class='media-body'>");
                        sp.Append("<h4 class='media-heading colorw'>" + UserCls._Name + "</h4>");
                        List<string> userModule = (List<string>)ctx.Modules.Where(x => x.URL == "#" && x.ParentId == 0 && x.Users.Any(u => u.UserId == UserCls._UserId)).Select(x => x.Name).ToList();
                        string br = userModule.Count() > 1 ? "</br>" : "";
                        string groupName = "";
                        foreach (var item in userModule)
                        {
                            groupName += item + br;
                        }
                        sp.Append("<small class='text-muted'>" + groupName + "</small>");
                        sp.Append("<br />");
                        sp.Append("<small class='text-muted'>" + UserCls._Email + "</small>");
                        sp.Append("</div>");
                    }
                    sp.Append("</div>");
                    SessionWrapper.MenuCategiries = sp.ToString();
                    litNav.Text = sp.ToString();
                }

               
            }


            else
            {
                litNav.Text = SessionWrapper.MenuCategiries;
            }

        }
    }
}