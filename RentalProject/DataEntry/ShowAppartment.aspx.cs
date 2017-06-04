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
    public partial class ShowApartment : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUnits();
            }
        }

        private void LoadUnits()
        {
            using (var db = new dbRentalsEntities())
            {
                //var _LoadUnits = (from u in db.Units
                //                  where u.Deleted == false && u.ParentID == null
                //                  select new
                //                  {
                //                      UnitId = u.UnitId,
                //                      UnitNumber = u.UnitNumber,
                //                      UnitName = u.UnitName,
                //                      Street = u.Streets.EnglishName + "_" + u.Streets.ArabicName,
                //                      UnitType = u.UnitTypes.EnglishName + "_" + u.UnitTypes.ArabicName,
                //                      Owner = u.Owners.FullName,
                //                      Description = u.Description,
                //                  }).ToList().OrderBy(x => x.Owner).ThenBy(x => x.UnitNumber);
                //RptUsers.DataSource = _LoadUnits;
                //RptUsers.DataBind();
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
                        var _Delete = db.Units.Where(x => x.UnitId == HF).ToList().SingleOrDefault();
                        if (_Delete != null)
                        {
                            _Delete.Deleted = true;
                            db.SaveChanges();

                            TransactionLogs _TransactionLogs = new TransactionLogs();
                            _TransactionLogs.Dated = System.DateTime.Now;
                            _TransactionLogs.OrganizationId = UserCls._OrganizationId;
                            _TransactionLogs.TransactionLogTypeId = 19;
                            _TransactionLogs.TransactionUserId = UserCls._UserId;
                            _TransactionLogs.UnitId = _Delete.UnitId;
                            db.TransactionLogs.Add(_TransactionLogs);
                            db.SaveChanges();
                            transaction.Commit();
                            LoadUnits();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "success('Saved',' Success','growl-success');", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "javascript:parent.danger('Apartment has one or more Contract(s) ','Delete Contract first','growl-danger');", true);
                        transaction.Dispose();
                    }

                }
            }


        }
    }
}