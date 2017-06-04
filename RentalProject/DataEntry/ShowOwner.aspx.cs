using System;
using System.Linq;
using RentalDataAccess;
using RentalProject.Classes;
using System.Data.Entity.Infrastructure;

namespace RentalProject.DataEntry
{
    public partial class ShowOwner : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadOwners();
            }
        }

        private void LoadOwners()
        {
            using (var db = new dbRentalsEntities())
            {
                var _Owners = db.Owners.Where(x => x.Deleted == false).Select(x => new
                {
                    x.Nationalities.EnglishName,
                    x.OwnerId,
                    x.Dated,
                    x.Email,
                    x.Exist,
                    x.FullName,
                    x.Phone,
                    x.SSN,
                    x.UserName,
                    x.Password
                }).ToList().OrderBy(x => x.FullName);
                RptUsers.DataSource = _Owners;
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
                        var _Delete = db.Owners.Where(x => x.OwnerId == HF).ToList().SingleOrDefault();
                        if (_Delete != null)
                        {
                            _Delete.Deleted = true;
                            db.SaveChanges();

                            TransactionLogs _TransactionLogs = new TransactionLogs();
                            _TransactionLogs.Dated = System.DateTime.Now;
                            _TransactionLogs.OrganizationId = UserCls._OrganizationId;
                            _TransactionLogs.TransactionLogTypeId = 12;
                            _TransactionLogs.TransactionUserId = UserCls._UserId;
                            _TransactionLogs.OwnerId = _Delete.OwnerId;
                            db.TransactionLogs.Add(_TransactionLogs);
                            db.SaveChanges();
                            transaction.Commit();
                            LoadOwners();
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