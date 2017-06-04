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
    public partial class ShowStreets : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStreets();
            }
        }

        private void LoadStreets()
        {
            using (var db = new dbRentalsEntities())
            {
                var _LoadCities = db.Streets.Select(x => new
                {
                    StreetId = x.StreetId,
                    EnglishName = x.EnglishName,
                    ArabicName = x.ArabicName,
                    DistricArabicName = x.Districts.ArabicName,
                    DistricEnglishName = x.Districts.EnglishName
                }).ToList().OrderBy(x => x.StreetId);
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
                        var _Deleted = db.Streets.Where(x => x.StreetId == HF).ToList().SingleOrDefault();
                        db.Streets.Attach(_Deleted);
                        db.Streets.Remove(_Deleted);
                        db.SaveChanges();
                        transaction.Commit();
                        LoadStreets();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "success('Saved',' Success','growl-success');", true);
                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "javascript:parent.danger('Street has one or more distric(s) ','Delete distric first','growl-danger');", true);
                        transaction.Dispose();
                    }

                }
            }


        }
    }
}