using RentalDataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RentalProject.Classes;
using System.Data.Entity.Validation;
using System.IO;

namespace RentalProject.DataEntry
{
    public partial class ChangePicture : System.Web.UI.Page
    {
        UserClass UserCls = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserImage();
            }
        }

        private void LoadUserImage()
        {
            using (var db = new dbRentalsEntities())
            {
                UserCls = Authentication.GetUserFromSessionOrFromTicket();
                long? _UserID = UserCls._UserId;
                if (_UserID != 0)
                {
                    string Image = db.Users.Where(u => u.UserId == UserCls._UserId).Select(o => o.Photo).SingleOrDefault();
                    if (Image != null)
                        Imgprw2.ImageUrl = "~/Photos/Users/" + Image;
                }
            }
        }

        protected void BtnUpload_Click(object sender, EventArgs e)
       {
            using (var db = new dbRentalsEntities())
            {
                if (ImageUpload.HasFile)
                {
                    UserCls = Authentication.GetUserFromSessionOrFromTicket();
                    Int64? _UserID = UserCls._UserId;
                    string File_Name = Guid.NewGuid().ToString();
                    string File_Extension = Path.GetExtension(ImageUpload.PostedFile.FileName);
                    string _Path = @"~\Photos\Users";
                    var _SaveURL = db.Users.Where(E => E.UserId == _UserID).ToList().SingleOrDefault();
                    if (_SaveURL != null)
                        _SaveURL.Photo = File_Name+ File_Extension;
                    Int32 Ok = db.SaveChanges();
                    if (Ok > 0)
                    {
                        ImageUpload.SaveAs(Server.MapPath(_Path) + "\\" + File_Name + File_Extension);
                        LoadUserImage();
                        string URL = _SaveURL.Photo;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "danger('Photo Updated','Saved','growl-success');", true);
                    }

                }
                else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "danger('Select The Photo','Profile Update Failer','growl-danger');", true);
            }
        }

        protected void BtnRemove_Click(object sender, EventArgs e)
        {
            using (var db = new dbRentalsEntities())
            {
                UserCls = Authentication.GetUserFromSessionOrFromTicket();
                Int64? _UserID = UserCls._UserId;
                var _SaveURL = db.Users.Where(E => E.UserId == _UserID).ToList().SingleOrDefault();
                if (_SaveURL != null)
                    _SaveURL.Photo = "Default.jpeg";
                db.SaveChanges();
                LoadUserImage();
                string URL = _SaveURL.Photo;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "danger('Photo Removed ','Saved','growl-success');", true);

            }
        }
    }
}