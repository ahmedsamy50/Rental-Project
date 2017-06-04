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
    public partial class EditCurrency : System.Web.UI.Page
    {
        UserClass UserCls = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["CurrencyId"]))
                    LoadData(Convert.ToInt32(Request.QueryString["CurrencyId"]));
            }
        }

        private void LoadData(Int32 ID)
        {
            using (var db = new dbRentalsEntities())
            {
                try
                {
                    var _Country = db.Currency.Where(x => x.CurrencyId == ID).ToList().SingleOrDefault();
                    txtArabicName.Text = _Country.ArabicName;
                    txtEnglishName.Text = _Country.EnglishName;
                    txtcode.Text = _Country.Code;
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
                    if (!String.IsNullOrEmpty(Request.QueryString["CurrencyId"])) // Update
                    {
                        try
                        {
                            Int32 ID = Convert.ToInt32(Request.QueryString["CurrencyId"]);
                            var _Users = db.Currency.Where(x => x.CurrencyId == ID).ToList().SingleOrDefault();
                            UserCls = Authentication.GetUserFromSessionOrFromTicket();
                            _Users.ArabicName = txtArabicName.Text.Trim();
                            _Users.EnglishName = txtEnglishName.Text.Trim();
                            _Users.Code = txtcode.Text.Trim();
                            db.SaveChanges();
                            transaction.Commit();
                            Response.Redirect("~/DataEntry/ShowCurrency.aspx", false);

                        }
                        catch (DbEntityValidationException ex)
                        {
                            transaction.Dispose();
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "javascript:parent.danger('','','growl-danger');", true);
                            //foreach (var eve in ex.EntityValidationErrors)
                            //{
                            //    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            //        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            //    foreach (var ve in eve.ValidationErrors)
                            //    {
                            //        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            //            ve.PropertyName, ve.ErrorMessage);
                            //    }
                            //}
                            //throw;
                        }
                    }

                    else
                    {  // Add New
                        try
                        {
                            UserCls = Authentication.GetUserFromSessionOrFromTicket();
                            Currency _Users = new Currency();
                            _Users.ArabicName = txtArabicName.Text.Trim();
                            _Users.EnglishName = txtEnglishName.Text.Trim();
                            _Users.Code = txtcode.Text.Trim();
                            db.Currency.Add(_Users);
                            db.SaveChanges();
                            // Save in Transaction 
                            transaction.Commit();
                            Response.Redirect("~/DataEntry/ShowCurrency.aspx", false);

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