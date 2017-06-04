using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RentalDataAccess;
using RentalProject.Classes;

namespace RentalProject.WebSite
{
    public partial class Default : System.Web.UI.Page
    {
        string userName = "";
        string password = "";
        List<int> UserMoudules = new List<int>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionWrapper.MenuCategiries = null;

                LoadAdvertising();
            }

        }

        private void LoadAdvertising()
        {
            using (var db = new dbRentalsEntities())
            {
                var _Advertising = db.Advertising.Where(x => x.Active == true && x.EndDate > System.DateTime.Now).OrderBy(x => x.Priority).Select(x => new { Image = "~/WebSite/Advertising/Photos/" + x.Image }).ToList();
                RptAdvertising.DataSource = _Advertising;
                RptAdvertising.DataBind();

            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            using (var db = new dbRentalsEntities())
            {
                ContactUs _ContactUs = new ContactUs();
                _ContactUs.Dated = System.DateTime.Now;
                _ContactUs.Email = txtEmail.Text;
                _ContactUs.Message = txtmessage.Text;
                _ContactUs.Name = txtName.Text;
                _ContactUs.Subject = txtSubject.Text;
                db.ContactUs.Add(_ContactUs);
                if (db.SaveChanges() >= 1)
                {

                    txtEmail.Text = "";
                    txtmessage.Text = "";
                    txtName.Text = "";
                    txtSubject.Text = "";
                    divSucess.Visible = true;
                    divSucess.InnerHtml = "Message Send Succeed";
                }
                else
                {
                    diverror.Visible = true;
                    diverror.InnerHtml = "Error in sending message";
                }

            }

        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {

            using (var db = new dbRentalsEntities())
            {
                int LoginType = 3;
                userName = txtuersname.Text;
                password = txtpassword.Text;
                Login_Users_Result _UserName = null; 
                _UserName = db.Login_Users(1).FirstOrDefault(u => u.UserName == userName); // Rental
                if (_UserName != null)
                    LoginType = 1;
                else 
                {
                    _UserName = db.Login_Users(2).FirstOrDefault(u => u.UserName == userName); // Owner
                    if (_UserName != null)
                        LoginType = 2;
                }

                if (_UserName != null)
                {
                    var _Password = db.Login_Users(LoginType).Where(u => u.Password == password && u.UserName == userName).ToList();
                    if (_Password != null && _Password.Count > 0)
                    {
                        var _Active = db.Login_Users(LoginType).Where(u => u.Password == password && u.UserName == userName && u.Exist == true).ToList().SingleOrDefault();
                        if (_Active != null)
                        {
                            // Login Succeed
                            // ask for Categories
                            var _UserID = db.Login_Users(LoginType).Where(u => u.Exist == true && u.UserName == userName && u.Password == password).ToList().SingleOrDefault();
                            if (!_UserID.Deleted)
                            {
                                if (_UserID != null)
                                {
                                    FillUserClass(_UserID.UserId, LoginType);
                                    LogUserAction logUserActionCls = new LogUserAction();
                                    logUserActionCls.LogDate = DateTime.Now;
                                    logUserActionCls.PageUrl = Request.RawUrl;
                                    logUserActionCls.PageName = UtilityClass.GetCurrentPageName().ToLower();
                                    logUserActionCls.UserId = _UserID.UserId;
                                    logUserActionCls.LogAction = "Web Site Loged in";
                                    logUserActionCls.IPAddress = HttpContext.Current.Request.UserHostAddress;
                                    db.LogUserAction.Add(logUserActionCls);
                                    db.SaveChanges();
                                    if (LoginType == 1)
                                        Response.Redirect("~/WebSite/RentalWebSite.aspx", false);
                                    else if (LoginType == 2)
                                        Response.Redirect("~/WebSite/OwnersWebsite.aspx", false);
                                    else Response.Redirect("~/WebSite/Default.aspx", false);
                                }
                            }

                            else
                            {
                                divMsgError.Visible = true;
                                divMsgError.InnerHtml = "User deleted , Contact system administrator";
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showPopup()", true);
                                return;
                            }
                        }

                        else
                        {
                            divMsgError.Visible = true;
                            divMsgError.InnerHtml = "User  " + _UserName.FullName.ToString() + "  Not Activeted in Rental System";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showPopup()", true);
                            return;

                        }
                    }

                    else
                    {
                        divMsgError.Visible = true;
                        divMsgError.InnerHtml = "Password Error";
                        txtpassword.Focus();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showPopup()", true);

                    }


                }

                else
                {
                    divMsgError.Visible = true;
                    divMsgError.InnerHtml = "User Name  Error";
                    txtuersname.Focus();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showPopup()", true);
                    return;

                }
            }

        }

        private void FillUserClass(long _UserId, int LoginType)
        {
            using (var db = new dbRentalsEntities())
            {
                UserClass UserCls = null;
                var _userCls = db.Login_Users(LoginType).Where(u => u.UserId == _UserId).ToList().SingleOrDefault();
                UserCls = new UserClass(_userCls.UserId, _userCls.FullName, _userCls.UserName, _userCls.Password, _userCls.OrganizationId, _userCls.Email, false, UserMoudules, _userCls.Photo);

                Authentication.CreateTicketForUser(_userCls,
                                                   GenericJSONHelper.Serialize<UserClass>(UserCls),
                                                   false);
                if (false)
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