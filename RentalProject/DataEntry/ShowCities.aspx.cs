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
    public partial class ShowCities : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCities();
            }
        }

        private void LoadCities()
        {
            using (var db = new dbRentalsEntities())
            {
                var _LoadCities = db.Cities.Select(x => new
                {
                    CitiyId = x.CitiyId,
                    EnglishName = x.EnglishName,
                    ArabicName = x.ArabicName,
                    CountryArabicName = x.Countries.ArabicName,
                    CountryEnglishName = x.Countries.EnglishName
                }).ToList().OrderBy(x => x.CitiyId);
                RptUsers.DataSource = _LoadCities;
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
                        Int32 HF = Convert.ToInt32(HFDeleteId.Value);
                        var _Deleted = db.Cities.Where(x => x.CitiyId == HF).ToList().SingleOrDefault();
                        db.Cities.Attach(_Deleted);
                        db.Cities.Remove(_Deleted);
                        db.SaveChanges();
                        transaction.Commit();
                        LoadCities();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "success('Saved',' Success','growl-success');", true);
                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "javascript:parent.danger('City has one or more distric(s) ','Delete distric first','growl-danger');", true);
                        transaction.Dispose();
                    }

                }
            }


        }
    }
}