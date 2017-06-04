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
    public partial class EditDistric : System.Web.UI.Page
    {
        UserClass UserCls = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCities();
                if (!String.IsNullOrEmpty(Request.QueryString["DistricId"]))
                {
                    LoadData(Convert.ToInt32(Request.QueryString["DistricId"]));
                }
            }
        }

        private void LoadCities()
        {
            using (var db = new dbRentalsEntities())
            {
                var _LoadCountries = db.Cities.Select(x => new { DistricId = x.CitiyId, Name = x.EnglishName + " " + (x.ArabicName == null ? "" : x.ArabicName) }).ToList().OrderBy(x => x.DistricId);
                DDLCity.DataSource = _LoadCountries;
                DDLCity.DataTextField = "Name";
                DDLCity.DataValueField = "DistricId";
                DDLCity.DataBind();
                DDLCity.Items.Insert(0, new ListItem("Select City", "0"));
            }
        }

        private void LoadData(Int32 ID)
        {
            using (var db = new dbRentalsEntities())
            {
                try
                {
                    var _Country = db.Districts.Where(x => x.DistricId == ID).ToList().SingleOrDefault();
                    txtarabicname.Text = _Country.ArabicName;
                    txtEnglishName.Text = _Country.EnglishName;
                    DDLCity.SelectedValue = _Country.DistricId.ToString();
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
                    if (!String.IsNullOrEmpty(Request.QueryString["DistricId"])) // Update
                    {
                        try
                        {
                            Int32 ID = Convert.ToInt32(Request.QueryString["DistricId"]);
                            var _Users = db.Districts.Where(x => x.DistricId == ID).ToList().SingleOrDefault();
                            _Users.ArabicName = txtarabicname.Text.Trim();
                            _Users.EnglishName = txtEnglishName.Text.Trim();
                            _Users.CitiyId = Convert.ToInt32(DDLCity.SelectedValue);
                            db.SaveChanges();
                            transaction.Commit();
                            Response.Redirect("~/DataEntry/ShowDistric.aspx", false);

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
                            Districts _Users = new Districts();
                            _Users.ArabicName = txtarabicname.Text.Trim();
                            _Users.EnglishName = txtEnglishName.Text.Trim();
                            _Users.CitiyId = Convert.ToInt32(DDLCity.SelectedValue);
                            db.Districts.Add(_Users);
                            db.SaveChanges();
                            // Save in Transaction 
                            transaction.Commit();
                            Response.Redirect("~/DataEntry/ShowDistric.aspx", false);

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