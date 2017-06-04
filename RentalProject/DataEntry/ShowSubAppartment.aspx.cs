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
    public partial class ShowSubApartment : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["UpdatedId"]))
                    LoadUpdated();
                LoadOwners();
            }

        }

        private void LoadUpdated()
        {
             using (var db = new dbRentalsEntities())
            {
                 Int32 UnitId = Convert.ToInt32(Request.QueryString["UpdatedId"].ToString());
                 LoadOwners();
                 var _Unit = db.Units.Where(x => x.UnitId == UnitId).FirstOrDefault();
                 DDLOWner.SelectedValue = _Unit.OwnerId.ToString();
                 DDLCountry_SelectedIndexChanged(null,null);
                 DDLApartment.SelectedValue = _Unit.UnitId.ToString();
                 LoadSubUnits();
             }


        }

        private void LoadOwners()
        {
            using (var db = new dbRentalsEntities())
            {
                var _Nationals = db.Owners.Where(x => x.Deleted == false).Select(x => new { x.OwnerId, Name = x.FullName }).ToList();
                DDLOWner.DataTextField = "Name";
                DDLOWner.DataValueField = "OwnerId";
                DDLOWner.DataSource = _Nationals;
                DDLOWner.DataBind();
                DDLOWner.Items.Insert(0, new ListItem("Select Owner", "0"));
            }
        }

        protected void DDLCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLOWner.SelectedIndex != 0)
            {
                using (var db = new dbRentalsEntities())
                {
                    Int32 OwnerId = int.Parse(DDLOWner.SelectedValue);
                    var _Unit = db.Units.Where(x => x.Deleted == false && x.OwnerId == OwnerId && x.ParentID == null).Select(x => new { x.UnitId, Name = SqlFunctions.StringConvert((double)x.UnitNumber).Trim() + "_" + x.UnitName, }).ToList();
                    DDLApartment.DataTextField = "Name";
                    DDLApartment.DataValueField = "UnitId";
                    DDLApartment.DataSource = _Unit;
                    DDLApartment.DataBind();
                    DDLApartment.Items.Insert(0, new ListItem("Select Apartment", "0"));
                }
            }

        }

        private void LoadSubUnits()
        {
            using (var db = new dbRentalsEntities())
            {
                Int32 OwnerId = int.Parse(DDLOWner.SelectedValue);
                Int32 AppartMentID = int.Parse(DDLApartment.SelectedValue);
                var _LoadUnits = (from u in db.Units
                                  where u.Deleted == false && (u.ParentID != null) && u.OwnerId == OwnerId && u.ParentID == AppartMentID
                                  select new
                                  {
                                      UnitId = u.UnitId,
                                      UnitNumber = u.UnitNumber,
                                      UnitName = u.UnitName,
                                      Street = u.Streets.EnglishName + "_" + u.Streets.ArabicName,
                                      UnitType = u.UnitTypes.EnglishName + "_" + u.UnitTypes.ArabicName,
                                      Description = u.Description,
                                  }).ToList().OrderBy(x => x.UnitNumber);
                RptUsers.DataSource = _LoadUnits;
                RptUsers.DataBind();
            }
        }

        protected void DDLApartment_Select(object sender, EventArgs e)
        {
            LoadSubUnits();
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
                        UserClass UserCls = Authentication.GetUserFromSessionOrFromTicket();
                        Int32 HF = Convert.ToInt32(HFDeleteId.Value);
                        var _Delete = db.Units.Where(x => x.UnitId == HF).ToList().SingleOrDefault();
                        if (_Delete != null)
                        {
                            _Delete.Deleted = true;
                            db.SaveChanges();

                            TransactionLogs _TransactionLogs = new TransactionLogs();
                            _TransactionLogs.Dated = System.DateTime.Now;
                            _TransactionLogs.OrganizationId = UserCls._OrganizationId;
                            _TransactionLogs.TransactionLogTypeId = 14;
                            _TransactionLogs.TransactionUserId = UserCls._UserId;
                            _TransactionLogs.UnitId = _Delete.UnitId;
                            db.TransactionLogs.Add(_TransactionLogs);
                            db.SaveChanges();
                            transaction.Commit();
                            LoadSubUnits();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "success('Saved',' Success','growl-success');", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "javascript:parent.danger('Apartment has one or more Contract(s) ','Delete Contract first','growl-danger');", true);
                        transaction.Dispose();
                    }

                }
            }


        }
    }
}