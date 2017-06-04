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
using System.Diagnostics;

namespace RentalProject.DataEntry
{
    public partial class EditContract : System.Web.UI.Page
    {
        List<Units> TempList;
        UserClass UserCls = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TempTransactionDetail.Clear();
                LoadOwner();
                LoadCurrency();
                LoadRentals();
                if (!String.IsNullOrEmpty(Request.QueryString["ContractId"]))
                    LoadData(Convert.ToInt32(Request.QueryString["ContractId"]));
            }
        }






        private void LoadCurrency()
        {
            using (var db = new dbRentalsEntities())
            {
                var _Currency = db.Currency.Select(x => new { x.CurrencyId, Name = (x.ArabicName == null ? " " : x.ArabicName) + "_" + x.EnglishName }).ToList();
                DDLCurnency.DataTextField = "Name";
                DDLCurnency.DataValueField = "CurrencyId";
                DDLCurnency.DataSource = _Currency;
                DDLCurnency.DataBind();
                DDLCurnency.Items.Insert(0, new ListItem("Select Currency", "0"));
            }
        }

        private void LoadRentals()
        {
            using (var db = new dbRentalsEntities())
            {
                var _Rentals = db.Rentals.Where(x => x.Deleted == false && x.Exist == true).Select(x => new { x.RentalId, Name = x.FullName }).ToList();
                DDLRental.DataTextField = "Name";
                DDLRental.DataValueField = "RentalId";
                DDLRental.DataSource = _Rentals;
                DDLRental.DataBind();
                DDLRental.Items.Insert(0, new ListItem("Select Rental", "0"));
            }
        }

        private void LoadOwner()
        {
            using (var db = new dbRentalsEntities())
            {
                var _Owners = db.Owners.Where(x => x.Deleted == false && x.Exist == true).Select(x => new { x.OwnerId, Name = x.FullName }).ToList();
                DDLOwner.DataTextField = "Name";
                DDLOwner.DataValueField = "OwnerId";
                DDLOwner.DataSource = _Owners;
                DDLOwner.DataBind();
                DDLOwner.Items.Insert(0, new ListItem("Select Owner", "0"));
                //DDLUnits.Items.Insert(0, new ListItem("Select Unit", "0"));
            }
        }

        private void LoadData(Int32 ID)
        {
            using (var db = new dbRentalsEntities())
            {
                try
                {
                    var _Contracts = db.Contracts.Where(x => x.ContractId == ID).ToList().SingleOrDefault();
                    DDLRental.SelectedValue = _Contracts.RentalId.ToString();
                    DDLOwner.SelectedValue = _Contracts.OwnerId.ToString();
                    DDLCurnency.SelectedValue = _Contracts.CurrencyId.ToString();
                    //DDLUnits.SelectedValue = _Contracts.UnitId.ToString();
                    txtstartdate.Text = _Contracts.StartDate.Date.ToString("MM yyyy");
                    txtenddate.Text = _Contracts.EndDate.Date.ToString("MM yyyy");
                    ChkActive.Checked = _Contracts.Active;
                    ChkSMSService.Checked = _Contracts.SMSService;
                    ChkEmailService.Checked = _Contracts.EmailService;
                    txtPrice.Text = _Contracts.Price.ToString();
                    txtdescription.Text = _Contracts.Descriptions;
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
                    DateTime _FromDate = AbsoluteStart(Convert.ToDateTime(txtstartdate.Text));
                    DateTime _ToDate = AbsoluteEnd(Convert.ToDateTime(txtenddate.Text));
                    if (!String.IsNullOrEmpty(Request.QueryString["ContractId"])) // Update
                    {
                        #region MyRegion
                        //try
                        //{

                        //    var _SQL = db.Contracts.Where(x => x.Active == true && x.EndDate >= _ToDate).OrderByDescending(x => x.EndDate).FirstOrDefault();
                        //    if (_SQL != null)
                        //    {
                        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "javascript:parent.danger('Apartment " + DDLUnits.SelectedItem.Text + " has rented until " + _SQL.EndDate.Date + " ','Error','growl-danger');", true);
                        //        return;
                        //    }

                        //    if (System.DateTime.Now.Date.Month > _FromDate.Date.Month)
                        //    {
                        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "javascript:parent.danger('Start Month " + _FromDate.Date.Month + " Shoud be grater than The Current Month " + System.DateTime.Now.Date.Month + "','Error','growl-danger');", true);
                        //        return;
                        //    }

                        //    if (_FromDate.Date > _ToDate.Date)
                        //    {
                        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "javascript:parent.danger('Start Date Must be Less Than End Date','Error','growl-danger');", true);
                        //        return;
                        //    }

                        //    Int32 ID = Convert.ToInt32(Request.QueryString["ContractId"]);
                        //    var _Contracts = db.Contracts.Where(x => x.ContractId == ID).ToList().SingleOrDefault();
                        //    UserCls = Authentication.GetUserFromSessionOrFromTicket();
                        //    _Contracts.RentalId = int.Parse(DDLRental.SelectedValue);
                        //    _Contracts.OwnerId = int.Parse(DDLOwner.SelectedValue);
                        //    _Contracts.UnitId = int.Parse(DDLUnits.SelectedValue);
                        //    _Contracts.CurrencyId = int.Parse(DDLCurnency.SelectedValue);
                        //    _Contracts.StartDate = _FromDate.Date;
                        //    _Contracts.EndDate = _ToDate.Date;
                        //    _Contracts.Price = Convert.ToDecimal(txtPrice.Text);
                        //    _Contracts.Descriptions = txtdescription.Text;
                        //    _Contracts.Active = ChkActive.Checked;
                        //    _Contracts.SMSService = ChkSMSService.Checked;
                        //    _Contracts.EmailService = ChkEmailService.Checked;
                        //    db.SaveChanges();

                        //    Dictionary<string, string> _MonthYear = new Dictionary<string, string>();
                        //    DateTime from = _FromDate;
                        //    while (from <= _ToDate)
                        //    {
                        //        _MonthYear.Add(from.Month.ToString(), from.Year.ToString());
                        //        from = from.AddMonths(1);
                        //    }

                        //    foreach (var item in _MonthYear)
                        //    {
                        //        BankTransactionDetails _BankTransactionDetails = new BankTransactionDetails();
                        //        _BankTransactionDetails.BankTransactionId = _BankTransactions.TransactionId;
                        //        _BankTransactionDetails.Month = item.Key.ToString();
                        //        _BankTransactionDetails.Year = item.Value.ToString();
                        //        _BankTransactionDetails.Amount = Convert.ToDecimal(txtPrice.Text);
                        //        _BankTransactionDetails.Paid = false;
                        //        db.BankTransactionDetails.Add(_BankTransactionDetails);
                        //        db.SaveChanges();

                        //    }


                        //    transaction.Commit();


                        //    Response.Redirect("~/DataEntry/ShowContract.aspx", false);

                        //}
                        //catch (Exception)
                        //{
                        //    transaction.Dispose();
                        //} 
                        #endregion
                    }

                    else
                    {  // Add New
                        try
                        {
                            if (this.TempTransactionDetail == null && this.TempTransactionDetail.Count() == 0)
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "javascript:parent.danger('Add at least one apartment','Error','growl-danger');", true);
                                return;
                            }

                            foreach (var item in this.TempTransactionDetail)
                            {
                                var _SQL = db.Contracts.Where(x => x.Active == true  &&  x.UnitId == item.UnitId && x.EndDate >= _ToDate.Date).OrderByDescending(x => x.EndDate).FirstOrDefault();
                                if (_SQL != null)
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "javascript:parent.danger('Apartment " + DDLUnits.SelectedItem.Text + " has rented until " + _SQL.EndDate.Date + " ','Error','growl-danger');", true);
                                    return;
                                }
                                
                            }
                          
                            // Units Added Are Valid
                            if (System.DateTime.Now > _FromDate)
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "javascript:parent.danger('Start Month " + _FromDate.Date.Month + " Shoud be grater than The Current Month " + System.DateTime.Now.Date.Month + "','Error','growl-danger');", true);
                                return;
                            }

                            if (_FromDate > _ToDate)
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "javascript:parent.danger('Start Date Must be Less Than End Date','Error','growl-danger');", true);
                                return;
                            }

                            UserCls = Authentication.GetUserFromSessionOrFromTicket();
                            foreach (var item in this.TempTransactionDetail)
                            {
                                 Contracts _Contracts = new Contracts();
                                _Contracts.RentalId = int.Parse(DDLRental.SelectedValue);
                                _Contracts.OwnerId = int.Parse(DDLOwner.SelectedValue);
                                _Contracts.UnitId = item.UnitId;
                                _Contracts.CurrencyId = int.Parse(DDLCurnency.SelectedValue);
                                _Contracts.StartDate = _FromDate.Date;
                                _Contracts.EndDate = _ToDate.Date;
                                _Contracts.Price = Convert.ToDecimal(txtPrice.Text);
                                _Contracts.Descriptions = txtdescription.Text;
                                _Contracts.Active = ChkActive.Checked;
                                _Contracts.SMSService = ChkSMSService.Checked;
                                _Contracts.EmailService = ChkEmailService.Checked;
                                _Contracts.Dated = System.DateTime.Now;
                                _Contracts.UserId = UserCls._UserId;
                                _Contracts.OrganizationId = UserCls._OrganizationId;
                                db.Contracts.Add(_Contracts);
                                db.SaveChanges();
                            }

                            //BankTransactions _BankTransactions = new BankTransactions();
                            //_BankTransactions.OwnerId = int.Parse(DDLOwner.SelectedValue);
                            //_BankTransactions.RentalId = int.Parse(DDLRental.SelectedValue);
                            //_BankTransactions.ContractId = _Contracts.ContractId;
                            //db.BankTransactions.Add(_BankTransactions);
                            //db.SaveChanges();

                            Dictionary<string, string> _MonthYear = new Dictionary<string, string>();
                            DateTime from = _FromDate;
                            while (from <= _ToDate)
                            {
                                _MonthYear.Add(from.Month.ToString(), from.Year.ToString());
                                from = from.AddMonths(1);
                            }

                            //foreach (var item in _MonthYear)
                            //{
                            //    BankTransactionDetails _BankTransactionDetails = new BankTransactionDetails();
                            //    _BankTransactionDetails.BankTransactionId = _BankTransactions.TransactionId;
                            //    _BankTransactionDetails.Month = item.Key.ToString();
                            //    _BankTransactionDetails.Year = item.Value.ToString();
                            //    _BankTransactionDetails.Amount = Convert.ToDecimal(txtPrice.Text);
                            //    _BankTransactionDetails.Paid = false;
                            //    db.BankTransactionDetails.Add(_BankTransactionDetails);
                            //    db.SaveChanges();

                            //}


                            //// Save in Transaction 
                            //TransactionLogs _TransactionLogs = new TransactionLogs();
                            //_TransactionLogs.Dated = System.DateTime.Now;
                            //_TransactionLogs.OrganizationId = UserCls._OrganizationId;
                            //_TransactionLogs.TransactionLogTypeId = 4;
                            //_TransactionLogs.TransactionUserId = UserCls._UserId;
                            //_TransactionLogs.ContractId = _Contracts.ContractId;
                            //db.TransactionLogs.Add(_TransactionLogs);
                            //db.SaveChanges();
                            //// Save in Transaction 
                            //transaction.Commit();
                            //Response.Redirect("~/DataEntry/ShowContract.aspx", false);

                        }
                        catch (DbEntityValidationException ex)
                        {
                            transaction.Dispose();
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "javascript:parent.danger('','','growl-danger');", true);
                        }
                    }


                }
            }
        }

        private bool HasRented(int UnitId, DateTime _ToDate, out string _Until)
        {
            using (var db = new dbRentalsEntities())
            {
                
                var _SQL = db.Contracts.Where(x => x.Active == true && x.UnitId == UnitId && x.EndDate.Date <= _ToDate.Date).OrderByDescending(x => x.EndDate).FirstOrDefault();
                if (_SQL != null)
                {
                    _Until = _SQL.EndDate.ToString();
                    return true;
                }
                else
                {
                    _Until = "";
                    return false;
                }
            }
        }

        public DateTime AbsoluteStart(DateTime dateTime)
        {
            return dateTime.Date;
        }

        /// <summary>
        /// Gets the 11:59:59 instance of a DateTime
        /// </summary>
        /// 
        public DateTime AbsoluteEnd(DateTime dateTime)
        {
            return AbsoluteStart(dateTime).AddDays(1).AddTicks(-1);
        }

        //public int MonthDifference(DateTime Start, DateTime End)
        //{
        //    return Math.Abs((Start.Month - End.Month) + 12 * (Start.Year - End.Year));
        //}



        protected void radioapartment_CheckedChanged(object sender, EventArgs e)
        {




            TempTransactionDetail.Clear();
            RptUnit.DataSource = null;
            radiosubapptment.Checked = false;
            using (var db = new dbRentalsEntities())
            {
                if (DDLOwner.SelectedIndex != 0)
                {
                    Int32 OwnerId = int.Parse(DDLOwner.SelectedValue);
                    var _Unit = db.Units.Where(x => x.Deleted == false && x.OwnerId == OwnerId && (x.ParentID == null || x.ParentID == 0)).Select(x => new { x.UnitId, Name = SqlFunctions.StringConvert((double)x.UnitNumber).Trim() + "_" + x.UnitName, x.Description }).ToList();
                    DDLUnits.DataTextField = "Name";
                    DDLUnits.DataValueField = "UnitId";
                    DDLUnits.DataSource = _Unit;
                    DDLUnits.DataBind();
                    DDLUnits.Items.Insert(0, new ListItem("Select Apartment", "0"));
                }
                else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "javascript:parent.danger('Select Owner','Error','growl-danger');", true);
            }
        }

        protected void radiosubapptment_CheckedChanged(object sender, EventArgs e)
        {
            radioapartment.Checked = false;
            using (var db = new dbRentalsEntities())
            {
                TempTransactionDetail.Clear();
                RptUnit.DataSource = null;
                if (DDLOwner.SelectedIndex != 0)
                {
                    Int32 OwnerId = int.Parse(DDLOwner.SelectedValue);
                    var _Unit = db.Units.Where(x => x.Deleted == false && x.OwnerId == OwnerId && (x.ParentID != null || x.ParentID != 0)).Select(x => new { x.UnitId, Name = SqlFunctions.StringConvert((double)x.UnitNumber).Trim() + "_" + x.UnitName, x.Description }).ToList();
                    DDLUnits.DataTextField = "Name";
                    DDLUnits.DataValueField = "UnitId";
                    DDLUnits.DataSource = _Unit;
                    DDLUnits.DataBind();
                    DDLUnits.Items.Insert(0, new ListItem("Select Sub Spartment", "0"));
                }
                else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "javascript:parent.danger('Select Owner','Error','growl-danger');", true);
            }

        }

        protected void DDLOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            radioapartment_CheckedChanged(null, null);
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
                var Undo = this.TempTransactionDetail.Select(x => new { x.UnitId, Name = x.UnitNumber + "_" + x.UnitName, x.Description }).AsEnumerable().FirstOrDefault();
                if (Undo != null)
                {
                    DDLUnits.Items.Add(new ListItem(Undo.Name, Undo.UnitId.ToString()));
                }
               
              
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


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (var db = new dbRentalsEntities())
            {
                if (DDLUnits.SelectedIndex != 0)
                {
                    Units detail = new Units();
                    Int32 _SelectedUnit = Convert.ToInt32(DDLUnits.SelectedValue);
                    ListItem itemToRemove = DDLUnits.Items.FindByValue(DDLUnits.SelectedValue);
                    if (itemToRemove != null)
                    {
                        DDLUnits.Items.Remove(itemToRemove);
                    }
                    var _get = db.Units.Where(c => c.UnitId == _SelectedUnit).FirstOrDefault();
                    detail.UnitNumber = _get.UnitNumber;
                    detail.UnitName = _get.UnitName;
                    detail.Description = _get.Description;
                    detail.UnitTypeId = _get.UnitTypeId;
                    detail.UnitId = _get.UnitId;

                    TempList = this.TempTransactionDetail;
                    TempList.Add(detail);
                    this.TempTransactionDetail = TempList;
                    LoadDetailGrid();
                }

                else
                    this.ClientScript.RegisterStartupScript(this.GetType(), "alert", "showNotification({ message: \"Error\", type: \"error\" });", true);
            }

        }



        private void LoadDetailGrid()
        {
            using (var db = new dbRentalsEntities())
            {
                var Linq = from iTempTransactionDetail in this.TempTransactionDetail
                           join f in db.UnitTypes on iTempTransactionDetail.UnitTypeId equals f.UnitTypeId
                           select new
                           {
                               iTempTransactionDetail.UnitId,
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



    }
}