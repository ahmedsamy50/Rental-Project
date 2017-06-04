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
    public partial class EditSubApartment : System.Web.UI.Page
    {
        List<Units> TempList;
        UserClass UserCls = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TempTransactionDetail.Clear();
                LoadOwners();
                LoadAppartmentType();
            }


        }

        private void LoadAppartmentType()
        {
            using (var db = new dbRentalsEntities())
            {
                var _Nationals = db.UnitTypes.Select(x => new { x.UnitTypeId, Name = x.EnglishName + "_" + x.ArabicName }).ToList();
                DDLAppartmentType.DataTextField = "Name";
                DDLAppartmentType.DataValueField = "UnitTypeId";
                DDLAppartmentType.DataSource = _Nationals;
                DDLAppartmentType.DataBind();
                DDLAppartmentType.Items.Insert(0, new ListItem("Select Apartment type", "0"));
            }
        }

        private void LoadOwners()
        {
            using (var db = new dbRentalsEntities())
            {
                var _Nationals = db.Owners.Select(x => new { x.OwnerId, Name = x.FullName }).ToList();
                DDLOwner.DataTextField = "Name";
                DDLOwner.DataValueField = "OwnerId";
                DDLOwner.DataSource = _Nationals;
                DDLOwner.DataBind();
                DDLOwner.Items.Insert(0, new ListItem("Select Owner", "0"));
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Units detail = new Units();
            detail.UnitNumber = Convert.ToInt32(txtunitnumber.Text);
            detail.UnitName = txtunitName.Text;
            detail.Description = txtDescription.Text;
            detail.UnitTypeId = Convert.ToInt32(DDLAppartmentType.SelectedValue);
            TempList = this.TempTransactionDetail;
            TempList.Add(detail);
            this.TempTransactionDetail = TempList;
            LoadDetailGrid();
        }

        private void LoadDetailGrid()
        {
            using (var db = new dbRentalsEntities())
            {
                var Linq = from iTempTransactionDetail in this.TempTransactionDetail
                           join f in db.UnitTypes on iTempTransactionDetail.UnitTypeId equals f.UnitTypeId
                           select new
                           {
                               iTempTransactionDetail.UnitNumber,
                               iTempTransactionDetail.UnitName,
                               EnglishName = f.EnglishName,
                               ArabicName = f.ArabicName,
                               iTempTransactionDetail.Description
                           };
                RptUnit.DataSource = Linq.ToArray();
                RptUnit.DataBind();
            }
        }

        protected void LinkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton button = (sender as LinkButton);
                string commandArgument = button.CommandArgument;
                //Get the Repeater Item reference
                RepeaterItem item = button.NamingContainer as RepeaterItem;
                //Get the repeater item index
                int index = item.ItemIndex;
                TempList = this.TempTransactionDetail;
                TempList.RemoveAt(index);
                this.TempTransactionDetail = TempList;
                LoadDetailGrid();
            }
            catch (Exception)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "alert", "showNotification({ message: \"Error in Deleted Item\", type: \"error\" });", true);

            }
        }


        protected void DDLOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var db = new dbRentalsEntities())
            {
                if (DDLOwner.SelectedIndex != 0)
                {
                    Int32 OwnerId = int.Parse(DDLOwner.SelectedValue);
                    var _Unit = db.Units.Where(x => x.Deleted == false && x.OwnerId == OwnerId && x.ParentID == null).Select(x => new { x.UnitId, Name = SqlFunctions.StringConvert((double)x.UnitNumber).Trim() + "_" + x.UnitName, }).ToList();
                    DDLAppartment.DataTextField = "Name";
                    DDLAppartment.DataValueField = "UnitId";
                    DDLAppartment.DataSource = _Unit;
                    DDLAppartment.DataBind();
                    DDLAppartment.Items.Insert(0, new ListItem("Select Apartment", "0"));
                }
            }
        }

        protected List<Units> TempTransactionDetail
        {
            get
            {
                if (Session["TEMPLIST"] == null)
                {
                    Session["TEMPLIST"] = new List<Units>();
                }

                return (List<Units>)Session["TEMPLIST"];
            }
            set
            {
                Session["TEMPLIST"] = value;
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
                   
                    try
                    {
                        UserCls = Authentication.GetUserFromSessionOrFromTicket();
                        Int32 UnitId = Convert.ToInt32(DDLAppartment.SelectedValue);
                        var StreetId = db.Units.Where(x => x.UnitId == UnitId).Select(x => x.StreetId).FirstOrDefault();
                        foreach (var item in this.TempTransactionDetail)
                        {
                             Units _Users = new Units();
                            _Users.StreetId = StreetId;
                            _Users.OwnerId = int.Parse(DDLOwner.SelectedValue);
                            _Users.UnitNumber = item.UnitNumber;
                            _Users.UnitName = item.UnitName;
                            _Users.UnitTypeId = int.Parse(DDLAppartmentType.SelectedValue);
                            _Users.Description = item.Description;
                            _Users.Dated = System.DateTime.Now;
                            _Users.Deleted = false;
                            _Users.ParentID = UnitId;
                            _Users.UserId = UserCls._UserId;
                            db.Units.Add(_Users);
                            db.SaveChanges();
                            // Save in Transaction 
                            TransactionLogs _TransactionLogs = new TransactionLogs();
                            _TransactionLogs.Dated = System.DateTime.Now;
                            _TransactionLogs.OrganizationId = UserCls._OrganizationId;
                            _TransactionLogs.TransactionLogTypeId = 17;
                            _TransactionLogs.TransactionUserId = UserCls._UserId;
                            _TransactionLogs.UnitId = _Users.UnitId;
                            db.TransactionLogs.Add(_TransactionLogs);
                            db.SaveChanges();

                        }

                        // Save in Transaction 
                        transaction.Commit();
                        Response.Redirect("~/DataEntry/ShowSubAppartment.aspx?UpdatedId=" + UnitId + "", false);

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