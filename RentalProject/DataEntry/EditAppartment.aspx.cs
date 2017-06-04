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
    public partial class EditApartment : System.Web.UI.Page
    {
      
        UserClass UserCls = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                LoadCountries();
                LoadUnitType();
                LoadOwner();

                if (!String.IsNullOrEmpty(Request.QueryString["UnitId"]))
                {
                    LoadData(Convert.ToInt32(Request.QueryString["UnitId"]));
                }
            }
        }

        private void LoadCountries()
        {
            using (var db = new dbRentalsEntities())
            {
                var _Nationals = db.Countries.Select(x => new { x.CountryId, Name = (x.EnglishName == null ? "" : x.EnglishName) + "_" + (x.ArabicName == null ? "" : x.ArabicName) }).ToList();
                DDLCountry.DataTextField = "Name";
                DDLCountry.DataValueField = "CountryId";
                DDLCountry.DataSource = _Nationals;
                DDLCountry.DataBind();
                DDLCountry.Items.Insert(0, new ListItem("Select Country", "0"));
            }
        }


        private void LoadUnitType()
        {
            using (var db = new dbRentalsEntities())
            {
                var _Nationals = db.UnitTypes.Select(x => new { x.UnitTypeId, Name = x.EnglishName + "_" + x.ArabicName }).ToList();
                DDLUnittype.DataTextField = "Name";
                DDLUnittype.DataValueField = "UnitTypeId";
                DDLUnittype.DataSource = _Nationals;
                DDLUnittype.DataBind();
                DDLUnittype.Items.Insert(0, new ListItem("Select Unit type", "0"));
            }
        }

        private void LoadOwner()
        {
            using (var db = new dbRentalsEntities())
            {
                var _Nationals = db.Owners.Select(x => new { x.OwnerId, Name = x.FullName }).ToList();
                DDLOWner.DataTextField = "Name";
                DDLOWner.DataValueField = "OwnerId";
                DDLOWner.DataSource = _Nationals;
                DDLOWner.DataBind();
                DDLOWner.Items.Insert(0, new ListItem("Select Owner", "0"));
            }
        }

        private void LoadData(Int32 ID)
        {
            using (var db = new dbRentalsEntities())
            {
                try
                {
                    var _Country = db.Units.Where(x => x.UnitId == ID).ToList().SingleOrDefault();
                    txtunitnumber.Text = _Country.UnitNumber.ToString();
                    txtunitName.Text = _Country.UnitName;
                    DDLOWner.SelectedValue = _Country.OwnerId.ToString();
                    DDLStreet.SelectedValue = _Country.StreetId.ToString();
                    DDLUnittype.SelectedValue = _Country.UnitTypeId.ToString();
                    txtDescription.Text = _Country.Description;

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
                    if (!String.IsNullOrEmpty(Request.QueryString["UnitId"])) // Update
                    {
                        try
                        {
                            Int32 ID = Convert.ToInt32(Request.QueryString["UnitId"]);
                            var _Users = db.Units.Where(x => x.UnitId == ID).ToList().SingleOrDefault();
                            UserCls = Authentication.GetUserFromSessionOrFromTicket();
                            _Users.UnitNumber = int.Parse(txtunitnumber.Text);
                            _Users.UnitName = txtunitName.Text.Trim();
                            _Users.StreetId = int.Parse(DDLStreet.SelectedValue);
                            _Users.UnitTypeId = int.Parse(DDLUnittype.SelectedValue);
                            _Users.OwnerId = int.Parse(DDLOWner.SelectedValue);
                            _Users.Description = txtDescription.Text;

                            db.SaveChanges();
                            // Save in Transaction 
                            TransactionLogs _TransactionLogs = new TransactionLogs();
                            _TransactionLogs.Dated = System.DateTime.Now;
                            _TransactionLogs.OrganizationId = UserCls._OrganizationId;
                            _TransactionLogs.TransactionLogTypeId = 13;
                            _TransactionLogs.TransactionUserId = UserCls._UserId;
                            _TransactionLogs.UnitId = _Users.UnitId;
                            db.TransactionLogs.Add(_TransactionLogs);
                            db.SaveChanges();
                            transaction.Commit();
                            Response.Redirect("~/DataEntry/ShowAppartment.aspx", false);

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
                            UserCls = Authentication.GetUserFromSessionOrFromTicket();
                            Units _Users = new Units();
                            _Users.UnitNumber = int.Parse(txtunitnumber.Text);
                            _Users.UnitName = txtunitName.Text.Trim();
                            _Users.StreetId = int.Parse(DDLStreet.SelectedValue);
                            _Users.UnitTypeId = int.Parse(DDLUnittype.SelectedValue);
                            _Users.OwnerId = int.Parse(DDLOWner.SelectedValue);
                            _Users.Description = txtDescription.Text;
                            _Users.Dated = System.DateTime.Now;
                            _Users.Deleted = false;
                            _Users.UserId = UserCls._UserId;
                            db.Units.Add(_Users);
                            db.SaveChanges();
                            // Save in Transaction 
                            TransactionLogs _TransactionLogs = new TransactionLogs();
                            _TransactionLogs.Dated = System.DateTime.Now;
                            _TransactionLogs.OrganizationId = UserCls._OrganizationId;
                            _TransactionLogs.TransactionLogTypeId = 3;
                            _TransactionLogs.TransactionUserId = UserCls._UserId;
                            _TransactionLogs.UnitId = _Users.UnitId;
                            db.TransactionLogs.Add(_TransactionLogs);
                            db.SaveChanges();
                            // Save in Transaction 
                            transaction.Commit();
                            Response.Redirect("~/DataEntry/ShowAppartment.aspx", false);

                        }
                        catch (DbEntityValidationException ex)
                        {
                            transaction.Dispose();
                        }
                    }


                }
            }
        }

        protected void DDLCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLCountry.SelectedIndex != 0)
            {
                using (var db = new dbRentalsEntities())
                {
                    Int32 ID = int.Parse(DDLCountry.SelectedValue);
                    var _Nationals = db.Cities.Where(x => x.CountyId == ID).Select(x => new { x.CitiyId, Name = (x.EnglishName == null ? "" : x.EnglishName) + "_" + (x.ArabicName == null ? "" : x.ArabicName) }).ToList();
                    DDLCity.DataTextField = "Name";
                    DDLCity.DataValueField = "CitiyId";
                    DDLCity.DataSource = _Nationals;
                    DDLCity.DataBind();
                    DDLCity.Items.Insert(0, new ListItem("Select City", "0"));
                }

            }
        }

        protected void DDLCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLCity.SelectedIndex != 0)
            {
                using (var db = new dbRentalsEntities())
                {
                    Int32 ID = int.Parse(DDLCity.SelectedValue);
                    var _Nationals = db.Districts.Where(x => x.CitiyId == ID).Select(x => new { x.DistricId, Name = (x.EnglishName == null ? "" : x.EnglishName) + "_" + (x.ArabicName == null ? "" : x.ArabicName) }).ToList();
                    DDLDistric.DataTextField = "Name";
                    DDLDistric.DataValueField = "DistricId";
                    DDLDistric.DataSource = _Nationals;
                    DDLDistric.DataBind();
                    DDLDistric.Items.Insert(0, new ListItem("Select Distric", "0"));
                }

            }

        }

        protected void DDLDistric_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLDistric.SelectedIndex != 0)
            {
                using (var db = new dbRentalsEntities())
                {
                    Int32 ID = int.Parse(DDLDistric.SelectedValue);
                    var _Nationals = db.Streets.Where(x => x.DistricId == ID).Select(x => new { x.StreetId, Name = (x.EnglishName == null ? "" : x.EnglishName) + "_" + (x.ArabicName == null ? "" : x.ArabicName) }).ToList();
                    DDLStreet.DataTextField = "Name";
                    DDLStreet.DataValueField = "StreetId";
                    DDLStreet.DataSource = _Nationals;
                    DDLStreet.DataBind();
                    DDLStreet.Items.Insert(0, new ListItem("Select Street", "0"));
                }

            }
        }

    }

}