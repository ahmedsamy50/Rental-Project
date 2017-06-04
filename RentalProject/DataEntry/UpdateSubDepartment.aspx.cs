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
    public partial class UpdateSubDepartment : System.Web.UI.Page
    {
        UserClass UserCls = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            UserClass UserCls = null;
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["UnitId"]))
                {
                    loadUnit();
                    LoadUnitType();
                }
                else
                    Response.Redirect("~/DataEntry/ShowSubAppartment.aspx");
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


        private void loadUnit()
        {

            using (var db = new dbRentalsEntities())
            {
                Int32 ID = Convert.ToInt32(Request.QueryString["UnitId"]);
                var _Users = db.Units.Where(x => x.UnitId == ID).ToList().SingleOrDefault();
                txtDescription.Text = _Users.Description;
                txtunitName.Text = _Users.UnitName;
                txtunitnumber.Text = _Users.UnitNumber.ToString();
                DDLUnittype.SelectedValue = _Users.UnitTypeId.ToString();
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
                            _Users.UnitTypeId = int.Parse(DDLUnittype.SelectedValue);
                            _Users.Description = txtDescription.Text;
                            db.SaveChanges();
                            // Save in Transaction 
                            TransactionLogs _TransactionLogs = new TransactionLogs();
                            _TransactionLogs.Dated = System.DateTime.Now;
                            _TransactionLogs.OrganizationId = UserCls._OrganizationId;
                            _TransactionLogs.TransactionLogTypeId = 15;
                            _TransactionLogs.TransactionUserId = UserCls._UserId;
                            _TransactionLogs.UnitId = _Users.UnitId;
                            db.TransactionLogs.Add(_TransactionLogs);
                            db.SaveChanges();
                            transaction.Commit();
                            Response.Redirect("~/DataEntry/ShowSubAppartment.aspx?UpdatedId=" + _Users.ParentID+ "", false);

                        }
                        catch (Exception)
                        {
                            transaction.Dispose();
                        }
                    }
                }
            }
        }
    }
}