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
    public partial class EditCountry : System.Web.UI.Page
    {
        UserClass UserCls = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["CountryId"]))
                    LoadData(Convert.ToInt32(Request.QueryString["CountryId"]));
            }
        }

        private void LoadData(Int32 ID)
        {
            using (var db = new dbRentalsEntities())
            {
                try
                {
                    var _Country = db.Countries.Where(x => x.CountryId == ID).ToList().SingleOrDefault();
                    txtArabicName.Text = _Country.ArabicName;
                    txtEnglishName.Text = _Country.EnglishName;
                    txtSortName.Text = _Country.SortName;
                    txtPhoneCode.Text = _Country.PhoneCode.ToString();
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
                    if (!String.IsNullOrEmpty(Request.QueryString["CountryId"])) // Update
                    {
                        try
                        {
                            Int32 ID = Convert.ToInt32(Request.QueryString["CountryId"]);
                            var _Users = db.Countries.Where(x => x.CountryId == ID).ToList().SingleOrDefault();
                            UserCls = Authentication.GetUserFromSessionOrFromTicket();
                            _Users.ArabicName = txtArabicName.Text.Trim();
                            _Users.EnglishName = txtEnglishName.Text.Trim();
                            _Users.SortName = txtSortName.Text.Trim();
                            _Users.PhoneCode = Convert.ToInt32(txtPhoneCode.Text.Trim());
                            db.SaveChanges();
                            transaction.Commit();
                            Response.Redirect("~/DataEntry/ShowCountry.aspx", false);

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
                            Countries _Users = new Countries();
                            _Users.ArabicName = txtArabicName.Text.Trim();
                            _Users.EnglishName = txtEnglishName.Text.Trim();
                            _Users.SortName = txtSortName.Text.Trim();
                            _Users.PhoneCode = Convert.ToInt32(txtPhoneCode.Text.Trim());
                            db.Countries.Add(_Users);
                            db.SaveChanges();
                            // Save in Transaction 
                            transaction.Commit();
                            Response.Redirect("~/DataEntry/ShowCountry.aspx", false);

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