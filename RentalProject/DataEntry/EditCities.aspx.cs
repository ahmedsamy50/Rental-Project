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
    public partial class EditCities : System.Web.UI.Page
    {
        UserClass UserCls = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCountries();
                if (!String.IsNullOrEmpty(Request.QueryString["CitiyId"]))
                {
                    LoadData(Convert.ToInt32(Request.QueryString["CitiyId"]));
                }
            }
        }

        private void LoadCountries()
        {
            using (var db = new dbRentalsEntities())
            {
                var _LoadCountries = db.Countries.Select(x => new { CountryId = x.CountryId, Name = x.EnglishName + " " + (x.ArabicName == null ? "" : x.ArabicName) }).ToList().OrderBy(x => x.CountryId);
                DDLCountry.DataSource = _LoadCountries;
                DDLCountry.DataTextField = "Name";
                DDLCountry.DataValueField = "CountryId";
                DDLCountry.DataBind();
                DDLCountry.Items.Insert(0, new ListItem("Select Country", "0"));
            }
        }

        private void LoadData(Int32 ID)
        {
            using (var db = new dbRentalsEntities())
            {
                try
                {
                    var _Country = db.Cities.Where(x => x.CitiyId == ID).ToList().SingleOrDefault();
                    txtarabicname.Text = _Country.ArabicName;
                    txtEnglishName.Text = _Country.EnglishName;
                    DDLCountry.SelectedValue = _Country.CountyId.ToString();
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
                    if (!String.IsNullOrEmpty(Request.QueryString["CitiyId"])) // Update
                    {
                        try
                        {
                            Int32 ID = Convert.ToInt32(Request.QueryString["CitiyId"]);
                            var _Users = db.Cities.Where(x => x.CitiyId == ID).ToList().SingleOrDefault();
                            _Users.ArabicName = txtarabicname.Text.Trim();
                            _Users.EnglishName = txtEnglishName.Text.Trim();
                            _Users.CountyId = Convert.ToInt32(DDLCountry.SelectedValue);
                            db.SaveChanges();
                            transaction.Commit();
                            Response.Redirect("~/DataEntry/ShowCities.aspx", false);

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
                            Cities _Users = new Cities();
                            _Users.ArabicName = txtarabicname.Text.Trim();
                            _Users.EnglishName = txtEnglishName.Text.Trim();
                            _Users.CountyId = Convert.ToInt32(DDLCountry.SelectedValue);
                            db.Cities.Add(_Users);
                            db.SaveChanges();
                            // Save in Transaction 
                            transaction.Commit();
                            Response.Redirect("~/DataEntry/ShowCities.aspx", false);

                        }
                        catch (DbEntityValidationException ex)
                        {
                            transaction.Dispose();
                        }
                    }


                }
            }
        }

    }
}