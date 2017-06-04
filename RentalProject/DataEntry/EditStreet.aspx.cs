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
    public partial class EditStreet : System.Web.UI.Page
    {
        UserClass UserCls = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDistricts();
                if (!String.IsNullOrEmpty(Request.QueryString["StreetId"]))
                {
                    LoadData(Convert.ToInt32(Request.QueryString["StreetId"]));
                }
            }
        }

        private void LoadDistricts()
        {
            using (var db = new dbRentalsEntities())
            {
                var _LoadCountries = db.Districts.Select(x => new { DistricId = x.DistricId, Name = x.EnglishName + " " + (x.ArabicName == null ? "" : x.ArabicName) }).ToList().OrderBy(x => x.DistricId);
                DDLDistric.DataSource = _LoadCountries;
                DDLDistric.DataTextField = "Name";
                DDLDistric.DataValueField = "DistricId";
                DDLDistric.DataBind();
                DDLDistric.Items.Insert(0, new ListItem("Select District", "0"));
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
                    DDLDistric.SelectedValue = _Country.DistricId.ToString();
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
                    if (!String.IsNullOrEmpty(Request.QueryString["StreetId"])) // Update
                    {
                        try
                        {
                            Int32 ID = Convert.ToInt32(Request.QueryString["StreetId"]);
                            var _Users = db.Streets.Where(x => x.StreetId == ID).ToList().SingleOrDefault();
                            _Users.ArabicName = txtarabicname.Text.Trim();
                            _Users.EnglishName = txtEnglishName.Text.Trim();
                            _Users.DistricId = Convert.ToInt32(DDLDistric.SelectedValue);
                            db.SaveChanges();
                            transaction.Commit();
                            Response.Redirect("~/DataEntry/ShowStreets.aspx", false);

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
                            Streets _Users = new Streets();
                            _Users.ArabicName = txtarabicname.Text.Trim();
                            _Users.EnglishName = txtEnglishName.Text.Trim();
                            _Users.DistricId = Convert.ToInt32(DDLDistric.SelectedValue);
                            db.Streets.Add(_Users);
                            db.SaveChanges();
                            // Save in Transaction 
                            transaction.Commit();
                            Response.Redirect("~/DataEntry/ShowStreets.aspx", false);

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