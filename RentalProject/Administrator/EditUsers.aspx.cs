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
    public partial class EditUsers : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUsers();
            }
        }

        private void LoadUsers()
        {
            using (var db = new dbRentalsEntities())
            {
                var _Users = db.Users.Where(x => x.Deleted == false && x.UserId != 30).Select(x => new
                {
                    x.Nationalities.EnglishName,
                    x.UserId,
                    x.Dated,
                    x.Email,
                    x.Exist,
                    x.FullName,
                    x.Phone,
                    x.SSN,
                    x.UserName,
                    x.Password
                }).ToList().OrderBy(x => x.FullName);
                RptUsers.DataSource = _Users;
                RptUsers.DataBind();
            }
        }



        protected void BtnUpdateOpen_Click(object sender, EventArgs e)
        {
            using (dbRentalsEntities db = new dbRentalsEntities())
            {
                var connection = ((IObjectContextAdapter)db).ObjectContext.Connection;
                connection.Open();
                //Opening transaction
                using (System.Data.Common.DbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        UserClass UserCls = Authentication.GetUserFromSessionOrFromTicket();
                        Int32 HF = Convert.ToInt32(HFDeleteId.Value);
                        var _Delete = db.Users.Where(x => x.UserId == HF).ToList().SingleOrDefault();
                        if (_Delete != null)
                        {
                            var _Modules = _Delete.Modules.ToList();
                            foreach (var item in _Modules)
                            {
                                _Delete.Modules.Remove(item);
                            }
                            db.SaveChanges();

                            _Delete.Deleted = true;
                            db.SaveChanges();

                            TransactionLogs _TransactionLogs = new TransactionLogs();
                            _TransactionLogs.Dated = System.DateTime.Now;
                            _TransactionLogs.OrganizationId = UserCls._OrganizationId;
                            _TransactionLogs.TransactionLogTypeId = 8;
                            _TransactionLogs.TransactionUserId = UserCls._UserId;
                            _TransactionLogs.UserId = _Delete.UserId;
                            db.TransactionLogs.Add(_TransactionLogs);
                            db.SaveChanges();
                            transaction.Commit();
                            LoadUsers();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "success('Saved',' Success','growl-success');", true);

                        }
                    }
                    catch (Exception)
                    {
                        transaction.Dispose();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "danger('Error','danger','growl-danger');", true);
                    }

                }

            }

        }
    }
}