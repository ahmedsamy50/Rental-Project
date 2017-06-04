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
    public partial class ShowDistric : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDistrict();
            }
        }

        private void LoadDistrict()
        {
            using (var db = new dbRentalsEntities())
            {
                var _LoadCities = db.Districts.Select(x => new
                {
                    DistricId = x.DistricId,
                    EnglishName = x.EnglishName,
                    ArabicName = x.ArabicName,
                    CityArabicName = x.Cities.ArabicName,
                    CityEnglishName = x.Cities.EnglishName
                }).ToList().OrderBy(x => x.DistricId);
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
                        var _Deleted = db.Districts.Where(x => x.DistricId == HF).ToList().SingleOrDefault();
                        db.Districts.Attach(_Deleted);
                        db.Districts.Remove(_Deleted);
                        db.SaveChanges();
                        transaction.Commit();
                        LoadDistrict();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "success('Saved',' Success','growl-success');", true);
                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "javascript:parent.danger('District has one or more city(s) ','Delete city first','growl-danger');", true);
                        transaction.Dispose();
                    }

                }
            }


        }
    }
}