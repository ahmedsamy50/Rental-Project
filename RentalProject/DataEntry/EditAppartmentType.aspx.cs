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
    public partial class EditApartmentType : System.Web.UI.Page
    {
        UserClass UserCls = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["UnitTypeId"]))
                    LoadData(Convert.ToInt32(Request.QueryString["UnitTypeId"]));
            }
        }

        private void LoadData(Int32 ID)
        {
            using (var db = new dbRentalsEntities())
            {
                try
                {
                    var _Country = db.UnitTypes.Where(x => x.UnitTypeId == ID).ToList().SingleOrDefault();
                    txtarabicname.Text = _Country.ArabicName;
                    txtEnglishName.Text = _Country.EnglishName;
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
                    if (!String.IsNullOrEmpty(Request.QueryString["UnitTypeId"])) // Update
                    {
                        try
                        {
                            Int32 ID = Convert.ToInt32(Request.QueryString["UnitTypeId"]);
                            var _Users = db.UnitTypes.Where(x => x.UnitTypeId == ID).ToList().SingleOrDefault();
                            UserCls = Authentication.GetUserFromSessionOrFromTicket();
                            _Users.ArabicName = txtarabicname.Text.Trim();
                            _Users.EnglishName = txtEnglishName.Text.Trim();
                            db.SaveChanges();
                            transaction.Commit();
                            Response.Redirect("~/DataEntry/ShowAppartmentType.aspx", false);

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
                            UnitTypes _Users = new UnitTypes();
                            _Users.ArabicName = txtarabicname.Text.Trim();
                            _Users.EnglishName = txtEnglishName.Text.Trim();
                            db.UnitTypes.Add(_Users);
                            db.SaveChanges();
                            // Save in Transaction 
                            transaction.Commit();
                            Response.Redirect("~/DataEntry/ShowAppartmentType.aspx", false);

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