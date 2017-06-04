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
    public partial class ShowCountry : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCountries();
            }
        }

        private void LoadCountries()
        {
            using (var db = new dbRentalsEntities())
            {
                var _LoadCountries = db.Countries.ToList().OrderBy(x => x.CountryId);
                RptUsers.DataSource = _LoadCountries;
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
                        var _Deleted = db.Countries.Where(x => x.CountryId == HF).ToList().SingleOrDefault();
                        db.Countries.Attach(_Deleted);
                        db.Countries.Remove(_Deleted);
                        db.SaveChanges();
                        transaction.Commit();
                        LoadCountries();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "success('Saved',' Success','growl-success');", true);
                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "javascript:parent.danger('Country has one or more citie(s) ','Delete cities first','growl-danger');", true);
                        transaction.Dispose();
                    }

                }
            }
            

        }
    }
}