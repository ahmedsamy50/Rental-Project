using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RentalDataAccess;
using RentalProject.Classes;
using System.Text;
using System.Data.Entity.Validation;
using System.Transactions;
using System.Data.Entity.Infrastructure;


namespace RentalProject.Administrator
{
    public partial class ShowUsers : System.Web.UI.Page
    {
        UserClass UserCls = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {    
                LoadNationalities();
                if (!String.IsNullOrEmpty(Request.QueryString["UserId"]))
                {
                    LoadData(Convert.ToInt32(Request.QueryString["UserId"]));
                    
                }
                   
            }
        }

        private void LoadNationalities()
        {
            using (var db = new dbRentalsEntities())
            {
                var _Nationals = db.Nationalities.ToList();
                DDLNationality.DataSource = _Nationals;
                DDLNationality.DataTextField = "EnglishName";
                DDLNationality.DataValueField = "NationalityId";
                DDLNationality.DataBind();
               DDLNationality.Items.Insert(0,new ListItem("Select Nationality","0"));
            }
        }

        private void LoadData(Int32 ID)
        {
            using (var db = new dbRentalsEntities())
            {
                try
                {
                    UserCls = Authentication.GetUserFromSessionOrFromTicket();
                    var _Users = db.Users.Where(x => x.UserId == ID).ToList().SingleOrDefault();
                    txtemail.Text = _Users.Email;
                    txtfirstname.Text = _Users.FullName;
                    txtpassword.Text = _Users.Password;
                    txtphone.Text = _Users.Phone;
                    txtssn.Text = _Users.SSN;
                    txtusername.Text = _Users.UserName;
                    chkRemember.Checked = _Users.Exist;
                    Imgprw2.ImageUrl = "~/Photos/Users/" + _Users.Photo;
                    DDLNationality.SelectedValue = _Users.NationalityId.ToString();
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
                    if (!String.IsNullOrEmpty(Request.QueryString["UserId"])) // Update
                    {
                        try
                        {
                            Int32 ID = Convert.ToInt32(Request.QueryString["UserId"]);
                            var _Users = db.Users.Where(x => x.UserId == ID).ToList().SingleOrDefault();
                            UserCls = Authentication.GetUserFromSessionOrFromTicket();
                            _Users.Email = txtemail.Text;
                            _Users.Exist = chkRemember.Checked;
                            _Users.FullName = txtfirstname.Text.Trim();
                            _Users.OrganizationId = UserCls._OrganizationId;
                            _Users.UserName = txtusername.Text;
                            _Users.Password = txtpassword.Text;
                            _Users.Phone = txtphone.Text;
                            _Users.SSN = txtssn.Text;
                            _Users.NationalityId = Convert.ToInt32(DDLNationality.SelectedValue);
                            if (ImageUpload.HasFile)
                            {
                                string path = Server.MapPath("~/Photos/Users/" + _Users.Photo);
                                if (System.IO.File.Exists(path))
                                    System.IO.File.Delete(path);
                                string fileName = Guid.NewGuid().ToString();
                                string ext = System.IO.Path.GetExtension(this.ImageUpload.PostedFile.FileName);
                                ImageUpload.SaveAs(Server.MapPath("~/Photos/Users/") + fileName + ext);
                                _Users.Photo = fileName + ext;
                            }
                            db.SaveChanges();
                            // Save in Transaction 
                            TransactionLogs _TransactionLogs = new TransactionLogs();
                            _TransactionLogs.Dated = System.DateTime.Now;
                            _TransactionLogs.OrganizationId = UserCls._OrganizationId;
                            _TransactionLogs.TransactionLogTypeId = 7;
                            _TransactionLogs.TransactionUserId = UserCls._UserId;
                            _TransactionLogs.UserId = _Users.UserId;
                            db.TransactionLogs.Add(_TransactionLogs);
                            db.SaveChanges();
                            transaction.Commit();
                            Response.Redirect("~/Administrator/EditUsers.aspx", false);

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
                            Users _Users = new Users();
                            _Users.Dated = System.DateTime.Now;
                            _Users.Email = txtemail.Text;
                            _Users.Exist = chkRemember.Checked;
                            _Users.FullName = txtfirstname.Text.Trim();
                            _Users.OrganizationId = UserCls._OrganizationId;
                            _Users.UserName = txtusername.Text;
                            _Users.Password = txtpassword.Text;
                            _Users.Phone = txtphone.Text;
                            _Users.SSN = txtssn.Text;
                            _Users.UserRegId = UserCls._UserId;
                            _Users.NationalityId = Convert.ToInt32(DDLNationality.SelectedValue);
                            if (ImageUpload.HasFile)
                            {
                                string fileName = Guid.NewGuid().ToString();
                                string ext = System.IO.Path.GetExtension(this.ImageUpload.PostedFile.FileName);
                                ImageUpload.SaveAs(Server.MapPath("/Photos/Users/") + fileName + ext);
                                _Users.Photo = fileName + ext;
                            }
                            else
                            {
                                _Users.Photo = "Default.jpeg";
                            }
                            db.Users.Add(_Users);
                            db.SaveChanges();

                            // Save in Transaction 
                            TransactionLogs _TransactionLogs = new TransactionLogs();
                            _TransactionLogs.Dated = System.DateTime.Now;
                            _TransactionLogs.OrganizationId = UserCls._OrganizationId;
                            _TransactionLogs.TransactionLogTypeId = 6;
                            _TransactionLogs.TransactionUserId = UserCls._UserId;
                            _TransactionLogs.UserId = _Users.UserId;
                            db.TransactionLogs.Add(_TransactionLogs);
                            db.SaveChanges();
                            transaction.Commit();
                            Response.Redirect("~/Administrator/EditUsers.aspx", false);

                        }
                        catch (DbEntityValidationException ex)
                        {
                            transaction.Dispose();
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "javascript:parent.danger('','','growl-danger');", true);
                            //foreach (var eve in ex.EntityValidationErrors)
                            //{
                            //    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            //        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            //    foreach (var ve in eve.ValidationErrors)
                            //    {
                            //        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            //            ve.PropertyName, ve.ErrorMessage);
                            //    }
                            //}
                            //throw;
                        }

                    }
                }
            }
        }
    }
}