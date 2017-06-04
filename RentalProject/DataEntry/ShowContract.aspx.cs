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
using System.Data.Objects.SqlClient;

namespace RentalProject.DataEntry
{
    public partial class ShowContract : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadContracts();
            }
        }

        private void LoadContracts()
        {
            using (var db = new dbRentalsEntities())
            {
                var _LoadUnits = (from u in db.Contracts
                                  where u.Active == true
                                  select new
                                  {
                                      ContractId = u.ContractId,
                                      Owner = u.Owners.FullName,
                                      Rental = u.Rentals.FullName,
                                      Unit = SqlFunctions.StringConvert((double)u.Units.UnitNumber).Trim() + "_" + u.Units.UnitName,
                                      StartDate = u.StartDate,
                                      EndDate = u.EndDate,
                                      u.EmailService,
                                      u.SMSService,
                                      u.Price,
                                      u.Active,
                                      u.Dated
                                  }).ToList().OrderBy(x => x.Dated);
                RptContracts.DataSource = _LoadUnits;
                RptContracts.DataBind();
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
                        var _Delete = db.Contracts.Where(x => x.ContractId == HF).ToList().SingleOrDefault();
                        if (_Delete != null)
                        {
                            _Delete.Active = false;
                            db.SaveChanges();
                            transaction.Commit();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "success('Saved',' Success','growl-success');", true);
                            LoadContracts();
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


        public bool IsAdmin()
        {
            using (var db = new dbRentalsEntities())
            {
                UserClass UserCls = Authentication.GetUserFromSessionOrFromTicket();
                var _HasAdmin = db.Modules.FirstOrDefault(X => X.Users.Any(x => x.UserId == UserCls._UserId) && X.ModuleId == 2);
                if (_HasAdmin != null)
                {
                    //foreach (RepeaterItem item in RptContracts.Items)
                    //{
                    //    var trUpdate = (System.Web.UI.HtmlControls.HtmlContainerControl)item.FindControl("trUpdate");
                    //    trUpdate.Visible = true;
                    //}

                    return true;
                }
                else
                {

                    return false;
                }
                
            }
        }
    }
}