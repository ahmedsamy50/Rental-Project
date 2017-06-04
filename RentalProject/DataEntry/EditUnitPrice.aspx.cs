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
using System.Data.Objects.SqlClient;

namespace RentalProject.DataEntry
{
    public partial class EditUnitPrice : System.Web.UI.Page
    {
        UserClass UserCls = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCurrencey();
                LoadOwners();
                if (!String.IsNullOrEmpty(Request.QueryString["UnitPriceId"]))
                    LoadData(Convert.ToInt32(Request.QueryString["UnitPriceId"]));
            }
        }

        private void LoadOwners()
        {
            using (var db = new dbRentalsEntities())
            {
                var _LoadOwners = db.Owners
                    .Where(x=> x.Exist == true && x.Deleted == false)
                    .Select(x => new { OwnerId = x.OwnerId, Name = x.FullName}).ToList().OrderBy(x => x.Name);
                DDLOwner.DataSource = _LoadOwners;
                DDLOwner.DataTextField = "Name";
                DDLOwner.DataValueField = "OwnerId";
                DDLOwner.DataBind();
                DDLOwner.Items.Insert(0, new ListItem("Select Owner", "0"));
                DDLUnit.Items.Insert(0, new ListItem("Select Unit", "0"));
            }
        }

        private void LoadCurrencey()
        {
            using (var db = new dbRentalsEntities())
            {
                var _LoadCurrencey = db.Currency.Select(x => new { CurrencyId = x.CurrencyId, Name = x.EnglishName + " " + (x.ArabicName == null ? "" : x.ArabicName) }).ToList().OrderBy(x => x.Name);
                DDLCurency.DataSource = _LoadCurrencey;
                DDLCurency.DataTextField = "Name";
                DDLCurency.DataValueField = "CurrencyId";
                DDLCurency.DataBind();
                DDLCurency.Items.Insert(0, new ListItem("Select Currency", "0"));
            }

        }

       

        private void LoadData(Int32 ID)
        {
            using (var db = new dbRentalsEntities())
            {
                try
                {
                    var _Country = db.UnitPrices.Where(x => x.UnitPriceId == ID).ToList().SingleOrDefault();
                    txtprice.Text = _Country.Price.ToString();
                    DDLCurency.SelectedValue = _Country.CurrenceId.ToString();
                    var UnitOwner = db.Units.Where(x => x.UnitId == _Country.UnitId).Select(x => x.OwnerId).SingleOrDefault();
                    DDLOwner.SelectedValue = UnitOwner.ToString();
                    DDLOwner_SelectedIndexChanged(null, null);

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
                    if (!String.IsNullOrEmpty(Request.QueryString["UnitPriceId"])) // Update
                    {
                        try
                        {
                            //Int32 ID = Convert.ToInt32(Request.QueryString["UnitPriceId"]);
                            //var _Users = db.UnitPrices.Where(x => x.UnitPriceId == ID).ToList().SingleOrDefault();
                            //UserCls = Authentication.GetUserFromSessionOrFromTicket();
                            //txtprice.Text = _Users.Price.ToString();
                            //DDLCurency.SelectedValue = _Users.CurrenceId.ToString();
                            //DDLUnit.SelectedValue = _Users.UnitId.ToString();

                            //// Save in Transaction 
                            //TransactionLogs _TransactionLogs = new TransactionLogs();
                            //_TransactionLogs.Dated = System.DateTime.Now;
                            //_TransactionLogs.OrganizationId = UserCls._OrganizationId;
                            //_TransactionLogs.TransactionLogTypeId = 16;
                            //_TransactionLogs.TransactionUserId = UserCls._UserId;
                            //_TransactionLogs.UnitPriceId = _Users.UnitPriceId;
                            //db.TransactionLogs.Add(_TransactionLogs);
                            //db.SaveChanges();
                            //transaction.Commit();
                            //Response.Redirect("~/DataEntry/ShowUnitPrice.aspx", false);

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
                            //UserCls = Authentication.GetUserFromSessionOrFromTicket();
                            //UnitPrices _Users = new UnitPrices();
                            //_Users.Price = Convert.ToDecimal(txtprice.Text.Trim());
                            //_Users.UnitId = Convert.ToInt32(DDLUnit.SelectedValue);
                            //_Users.CurrenceId = Convert.ToInt32(DDLCurency.SelectedValue);
                            //_Users.Dated = System.DateTime.Now;
                            //_Users.UserId = UserCls._UserId;
                            //db.UnitPrices.Add(_Users);
                            //db.SaveChanges();
                            //// Save in Transaction 
                            //TransactionLogs _TransactionLogs = new TransactionLogs();
                            //_TransactionLogs.Dated = System.DateTime.Now;
                            //_TransactionLogs.OrganizationId = UserCls._OrganizationId;
                            //_TransactionLogs.TransactionLogTypeId = 15;
                            //_TransactionLogs.TransactionUserId = UserCls._UserId;
                            //_TransactionLogs.UnitPriceId = _Users.UnitPriceId;
                            //db.TransactionLogs.Add(_TransactionLogs);
                            //db.SaveChanges();
                            //transaction.Commit();
                            //Response.Redirect("~/DataEntry/ShowUnitPrice.aspx", false);

                        }
                        catch (DbEntityValidationException ex)
                        {
                            transaction.Dispose();
                        }
                    }


                }
            }
        }

        protected void DDLOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var db = new dbRentalsEntities())
            {
                if (DDLOwner.SelectedValue != "0")
                {
                    Int32 _OwnerId = Int32.Parse(DDLOwner.SelectedValue);
                    var _LoadCountries = db.Units
                        .Where(x=> x.OwnerId == _OwnerId).Select(x=> new  {x.UnitId, Name = SqlFunctions.StringConvert((double)x.UnitNumber) + "_" + x.UnitName })
                        .OrderBy(x => x.UnitId).ToList();
                    if(_LoadCountries.Count > 0)
                    {
                        DDLUnit.DataSource = _LoadCountries;
                        DDLUnit.DataTextField = "Name";
                        DDLUnit.DataValueField = "UnitId";
                        DDLUnit.DataBind();
                        DDLUnit.Items.Insert(0, new ListItem("Select Unit", "0"));
                        if (!String.IsNullOrEmpty(Request.QueryString["UnitPriceId"]))
                        {
                            Int32 UnitPriceId = Convert.ToInt32(Request.QueryString["UnitPriceId"]);
                            DDLUnit.SelectedValue = db.UnitPrices.Where(x => x.UnitPriceId == UnitPriceId).Select(x => x.UnitId).SingleOrDefault().ToString();
                        }
                            
                    }

                    else
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "danger('Owner "+DDLOwner.SelectedItem.Text+ " has no units yet ',' Error','growl-danger');", true);

                }
            }
        }
    }

   
}