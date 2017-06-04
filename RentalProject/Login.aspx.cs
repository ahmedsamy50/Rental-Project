using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RentalProject.Classes;
using System.Data.Entity;
using RentalDataAccess;

namespace RentalProject
{
    public partial class Login : System.Web.UI.Page
    {
        List<int> UserMoudules = new List<int>();
        string userName = "";
        string password = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionWrapper.MenuCategiries = null;
                SessionWrapper.MenuControl = null;
                if (ValidateUserFromCookie())
                {
                    Response.Redirect("Login.aspx");
                }
            }

        }

        private bool ValidateUserFromCookie()
        {
            string UserName = "";
            string Password = "";
            Authentication.GetUserCredentialFromCookie(out UserName, out Password);
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                // return ValidteUser(UserName, Password, true);
                TxtUserName.Text = UserName;
                TxtPassword.Attributes.Add("value", Password);
                chkRemember.Checked = true;
            }
            return false;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            try
            {
                divMsgError.Visible = false;
                divMsgError.InnerHtml = "";
                using (dbRentalsEntities db = new dbRentalsEntities())
                {

                    if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["pg"]))
                    {
                        // The "pg" is a query string to indicate that the user was in a page and the session closed.
                        Response.Redirect(Request.QueryString["pg"].ToString());
                    }
                    else
                    {
                        userName = TxtUserName.Text;
                        password = TxtPassword.Text;
                        var _UserName = db.Login_Users(0).Where(u => u.UserName == userName).ToList();
                        if (_UserName != null && _UserName.Count > 0)
                        {
                            var _Password = db.Login_Users(0).Where(u => u.Password == password && u.UserName == userName).ToList();
                            if (_Password != null && _Password.Count > 0)
                            {
                                var _Active = db.Login_Users(0).Where(u => u.Password == password && u.UserName == userName && u.Exist == true).ToList().SingleOrDefault();
                                if (_Active != null)
                                {
                                    // Login Succeed
                                    // ask for Categories
                                    var _UserID = db.Login_Users(0).Where(u => u.Exist == true && u.UserName == userName && u.Password == password).ToList().SingleOrDefault();
                                    if (!_UserID.Deleted)
                                    {
                                        if (_UserID != null)
                                        {
                                            if (HasModules(_UserID.UserId))
                                            {
                                                FillUserMoudules(_UserID.UserId);
                                                FillUserClass(_UserID.UserId);
                                                LogUserAction logUserActionCls = new LogUserAction();
                                                logUserActionCls.LogDate = DateTime.Now;
                                                logUserActionCls.PageUrl = Request.RawUrl;
                                                logUserActionCls.PageName = UtilityClass.GetCurrentPageName().ToLower();
                                                logUserActionCls.UserId = _UserID.UserId;
                                                logUserActionCls.LogAction = "Loged in";
                                                logUserActionCls.IPAddress = HttpContext.Current.Request.UserHostAddress;
                                                db.LogUserAction.Add(logUserActionCls);
                                                db.SaveChanges();
                                                Response.Redirect("~/Default.aspx", false);
                                            }

                                            else
                                            {
                                                divMsgError.Visible = true;
                                                divMsgError.InnerHtml = "You Don't have modules yet , Contact system administrator";
                                                return;
                                            }
                                        }
                                    }

                                    else
                                    {
                                        divMsgError.Visible = true;
                                        divMsgError.InnerHtml = "User deleted , Contact system administrator";
                                        return;
                                    }
                                }

                                else
                                {
                                    divMsgError.Visible = true;
                                    divMsgError.InnerHtml = "User  " + _Active.FullName + "  Not Activeted in Rental System";
                                    TxtPassword.Focus();
                                    return;

                                }
                            }

                            else
                            {
                                divMsgError.Visible = true;
                                divMsgError.InnerHtml = "Password Error";
                                TxtPassword.Focus();
                                return;
                            }


                        }

                        else
                        {
                            divMsgError.Visible = true;
                            divMsgError.InnerHtml = "User Name  Error";
                            TxtPassword.Focus();
                            return;

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                //Response.Write (ex.Message);
                divMsgError.Visible = true;
                divMsgError.InnerHtml = "Network Error Please Check The Connection";

            }


        }

        private bool HasModules(Int32 UserId)
        {
            using (var db = new dbRentalsEntities())
            {
                var _Mod = db.Modules.Where(x => x.Users.Any(a => a.UserId == UserId)).FirstOrDefault();
                if (_Mod != null)
                    return true;
                else return false;


            }
        }



        private void FillUserMoudules(long _UserID)
        {
            using (var db = new dbRentalsEntities())
            {
                var UserModules = db.Modules.Where(u => u.Users.Any(m => m.UserId == _UserID)).ToList();
                for (int i = 0; i < UserModules.Count; i++)
                {
                    /// Checking the pages which the user has access.
                    UserMoudules.Add(Convert.ToInt32(UserModules[i].ModuleId));
                }
            }
        }



        private void FillUserClass(long _UserId)
        {
            using (var db = new dbRentalsEntities())
            {
                UserClass UserCls = null;
                var _userCls = db.Login_Users(0).Where(u => u.UserId == _UserId).ToList().SingleOrDefault();

                UserCls = new UserClass(_userCls.UserId, _userCls.FullName, _userCls.UserName, _userCls.Password, _userCls.OrganizationId, _userCls.Email, chkRemember.Checked, UserMoudules, _userCls.Photo);

                Authentication.CreateTicketForUser(_userCls,
                                                   GenericJSONHelper.Serialize<UserClass>(UserCls),
                                                   chkRemember.Checked);
                if (chkRemember.Checked)
                {
                    // If the user select the "Remember me" checkbox, we will call the "SaveUserCredentialToCookie" function in the "Authentication" class to save the username , and password in the cookies 
                    Authentication.SaveUserCredentialToCookie(userName, password);
                }
                else
                {
                    HttpCookie CycleCookie = new HttpCookie(StaticNames.CookieLoginName);
                    CycleCookie.Expires = DateTime.Now.AddDays(-35);
                    Response.Cookies.Add(CycleCookie);
                }
            }
        }
    }
}