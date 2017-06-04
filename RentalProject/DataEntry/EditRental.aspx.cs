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


namespace RentalProject.DataEntry
{
    public partial class EditRental : System.Web.UI.Page
    {
        UserClass UserCls = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadNationalities();
                if (!String.IsNullOrEmpty(Request.QueryString["UserId"]))
                    LoadData(Convert.ToInt32(Request.QueryString["UserId"]));
            }
        }

        private void LoadNationalities()
        {
            using (var db = new dbRentalsEntities())
            {
                var _Nationals = db.Nationalities.ToList();
                DDLNationality.DataTextField = "EnglishName";
                DDLNationality.DataValueField = "NationalityId";
                DDLNationality.DataSource = _Nationals;
                DDLNationality.DataBind();
                DDLNationality.Items.Insert(0, new ListItem("Select Nationality", "0"));
            }
        }

        private void LoadData(Int32 ID)
        {
            using (var db = new dbRentalsEntities())
            {
                try
                {
                    var _Users = db.Rentals.Where(x => x.RentalId == ID).ToList().SingleOrDefault();
                    txtemail.Text = _Users.Email;
                    txtfirstname.Text = _Users.FullName;
                    txtpassword.Text = _Users.Password;
                    txtphone.Text = _Users.Phone;
                    txtssn.Text = _Users.SSN;
                    txtusername.Text = _Users.UserName;
                    chkRemember.Checked = _Users.Exist;
                    Imgprw2.ImageUrl = "~/Photos/Rentals/" + _Users.Photo;
                    DDLNationality.SelectedValue = _Users.NationalityId.ToString();
                }
                catch { }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            using (dbRentalsEntities db = new dbRentalsEntities())
            {
                if (!CheckUserName(txtusername.Text.Trim()))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "javascript:parent.danger('Login Name Exist Before , Use Another One','Error','growl-danger');", true);
                    return;
                }
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
                            var _Users = db.Rentals.Where(x => x.RentalId == ID).ToList().SingleOrDefault();
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
                                string path = Server.MapPath("~/Photos/Rentals/" + _Users.Photo);
                                if (System.IO.File.Exists(path))
                                    System.IO.File.Delete(path);
                                string fileName = Guid.NewGuid().ToString();
                                string ext = System.IO.Path.GetExtension(this.ImageUpload.PostedFile.FileName);
                                ImageUpload.SaveAs(Server.MapPath("~/Photos/Rentals/") + fileName + ext);
                                _Users.Photo = fileName + ext;
                            }
                            db.SaveChanges();
                            // Save in Transaction 
                            TransactionLogs _TransactionLogs = new TransactionLogs();
                            _TransactionLogs.Dated = System.DateTime.Now;
                            _TransactionLogs.OrganizationId = UserCls._OrganizationId;
                            _TransactionLogs.TransactionLogTypeId = 9;
                            _TransactionLogs.TransactionUserId = UserCls._UserId;
                            _TransactionLogs.RentalId = _Users.RentalId;
                            db.TransactionLogs.Add(_TransactionLogs);
                            db.SaveChanges();
                            transaction.Commit();

                            Response.Redirect("~/DataEntry/ShowRental.aspx", false);

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
                            Rentals _Users = new Rentals();
                            _Users.Dated = System.DateTime.Now;
                            _Users.Email = txtemail.Text;
                            _Users.Exist = chkRemember.Checked;
                            _Users.FullName = txtfirstname.Text.Trim();
                            _Users.OrganizationId = UserCls._OrganizationId;
                            _Users.UserName = txtusername.Text;
                            _Users.Password = txtpassword.Text;
                            _Users.Phone = txtphone.Text;
                            _Users.SSN = txtssn.Text;
                            _Users.UserId = UserCls._UserId;
                            _Users.NationalityId = Convert.ToInt32(DDLNationality.SelectedValue);
                            if(ImageUpload.HasFile)
                            {
                                string fileName = Guid.NewGuid().ToString();
                                string ext = System.IO.Path.GetExtension(this.ImageUpload.PostedFile.FileName);
                                ImageUpload.SaveAs(Server.MapPath("/Photos/Rentals/") + fileName + ext);
                                _Users.Photo = fileName + ext;
                            }
                            else
                                _Users.Photo = "Default.jpeg";
                           
                            db.Rentals.Add(_Users);
                            db.SaveChanges();
                            // Save in Transaction 
                            TransactionLogs _TransactionLogs = new TransactionLogs();
                            _TransactionLogs.Dated = System.DateTime.Now;
                            _TransactionLogs.OrganizationId = UserCls._OrganizationId;
                            _TransactionLogs.TransactionLogTypeId = 1;
                            _TransactionLogs.TransactionUserId = UserCls._UserId;
                            _TransactionLogs.RentalId = _Users.RentalId;
                            db.TransactionLogs.Add(_TransactionLogs);
                            db.SaveChanges();
                            transaction.Commit();
                            Response.Redirect("~/DataEntry/ShowRental.aspx", false);

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

        public bool CheckUserName(string UserName)
        {
            using (var db = new dbRentalsEntities())
            {
                var _Owner = db.Owners.FirstOrDefault(x => x.UserName == UserName);
                if (_Owner == null)
                {
                    var _Rental = db.Rentals.FirstOrDefault(x => x.UserName == UserName);
                    if (_Rental == null)
                        return true;
                    else return false;
                }
                else return false;
            }
        }
    }
}