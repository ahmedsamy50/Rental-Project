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

namespace RentalProject.WebSite
{
    public partial class EditAdvertising : System.Web.UI.Page
    {
        UserClass UserCls = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["AdvertisingId"]))
                {
                    LoadData(Convert.ToInt32(Request.QueryString["AdvertisingId"]));
                    RequiredFieldValidator2.Enabled = false;
                }
                else
                {
                    txtstartdate.Text = System.DateTime.Now.ToShortDateString();
                    txtenddate.Text = System.DateTime.Now.ToShortDateString();
                }
            }

        }

        private void LoadData(Int32 ID)
        {
            using (var db = new dbRentalsEntities())
            {
                try
                {
                    var _Advertising = db.Advertising.Where(x => x.AdvertisingId == ID).ToList().SingleOrDefault();
                    txtenddate.Text = _Advertising.EndDate.ToShortDateString();
                    txtstartdate.Text = _Advertising.StartDate.ToShortDateString();
                    txtPriority.Text = _Advertising.Priority.ToString();
                    ChkActive.Checked = _Advertising.Active;
                    Imgprw2.ImageUrl = "~/WebSite/Advertising/Photos/" + _Advertising.Image;

                }
                catch { }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            using (dbRentalsEntities db = new dbRentalsEntities())
            {
                var connection = ((IObjectContextAdapter)db).ObjectContext.Connection;
                connection.Open();
                //Opening transaction
                using (System.Data.Common.DbTransaction transaction = connection.BeginTransaction())
                {
                    if (!String.IsNullOrEmpty(Request.QueryString["AdvertisingId"])) // Update
                    {
                        try
                        {
                            Int32 ID = Convert.ToInt32(Request.QueryString["AdvertisingId"]);
                            var _Advertising = db.Advertising.Where(x => x.AdvertisingId == ID).ToList().SingleOrDefault();
                            UserCls = Authentication.GetUserFromSessionOrFromTicket();
                            _Advertising.Active = ChkActive.Checked;
                            _Advertising.StartDate = Convert.ToDateTime(txtstartdate.Text);
                            _Advertising.EndDate = Convert.ToDateTime(txtenddate.Text);
                            _Advertising.Priority = Int32.Parse(txtPriority.Text);

                            if (ImageUpload.HasFile)
                            {
                                string path = Server.MapPath("~/WebSite/Advertising/Photos/"+_Advertising.Image);
                                if (System.IO.File.Exists(path))
                                    System.IO.File.Delete(path);
                                string fileName = Guid.NewGuid().ToString();
                                string ext = System.IO.Path.GetExtension(this.ImageUpload.PostedFile.FileName);
                                ImageUpload.SaveAs(Server.MapPath("~/WebSite/Advertising/Photos/") + fileName + ext);
                                _Advertising.Image = fileName + ext;
                            }
                            db.SaveChanges();
                            transaction.Commit();
                            Response.Redirect("~/WebSite/ShowAdvertising.aspx", false);

                        }
                        catch (Exception)
                        {
                            transaction.Dispose();
                        }
                    }

                    else
                    {  // Add New
                        try
                        {
                            UserCls = Authentication.GetUserFromSessionOrFromTicket();
                            Advertising _Advertising = new Advertising();
                            _Advertising.Active = ChkActive.Checked;
                            _Advertising.StartDate = Convert.ToDateTime(txtstartdate.Text);
                            _Advertising.EndDate = Convert.ToDateTime(txtenddate.Text);
                            _Advertising.Priority = Int32.Parse(txtPriority.Text);
                            _Advertising.Dated = System.DateTime.Now;
                            _Advertising.UserId = UserCls._UserId;
                            string fileName = Guid.NewGuid().ToString();
                            string ext = System.IO.Path.GetExtension(this.ImageUpload.PostedFile.FileName);
                            ImageUpload.SaveAs(Server.MapPath("/WebSite/Advertising/Photos/") + fileName + ext);
                            _Advertising.Image = fileName + ext;
                            _Advertising.OrganizationId = UserCls._OrganizationId;
                            db.Advertising.Add(_Advertising);
                            db.SaveChanges();
                            transaction.Commit();
                            Response.Redirect("~/WebSite/ShowAdvertising.aspx", false);

                        }
                        catch (DbEntityValidationException ex)
                        {
                            transaction.Dispose();
                        }
                    }


                }
            }
        }
    }
}