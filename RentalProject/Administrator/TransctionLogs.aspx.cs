using System;
using System.Collections.Generic;
using System.Linq;
using RentalDataAccess;
using RentalProject.Classes;


namespace RentalProject.Administrator
{
    public partial class TransctionLogs : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtfromdate.Text = System.DateTime.Now.ToShortDateString();
                txttodate.Text = System.DateTime.Now.ToShortDateString();
            }

        }

        protected void ddltransactionLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddltransactionLog.SelectedIndex == 0)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "danger('Select Transaction','danger','growl-danger');", true);
                RptTransactionLog.DataSource = null;
                RptTransactionLog.DataBind();
                return;
            }


            using (var db = new dbRentalsEntities())
            {

                UserClass UserCls = Authentication.GetUserFromSessionOrFromTicket();
                var _Transactions = db.TransactionLogs.Where(x => x.OrganizationId == UserCls._OrganizationId).AsEnumerable();
                List<PersonClass> _PersonClass = new List<PersonClass>();
                if (ddltransactionLog.SelectedValue == "1") // Users
                {
                    _PersonClass = (from t in _Transactions
                                    join u in db.Users on t.UserId equals u.UserId
                                    join o in db.Users on t.TransactionUserId equals o.UserId
                                    select new PersonClass
                                    {
                                        TransactionLogId = t.TransactionLogId,
                                        TransactionFullName = o.FullName,
                                        TransactionDate = t.Dated,
                                        TransactionLogEnglishName = t.TransactionLogTypes.EnglishName,
                                        TransactionLogArabicName = t.TransactionLogTypes.ArabicName,
                                        TransactionLogForFullName = u.FullName
                                    }).ToList();
                }

                if (ddltransactionLog.SelectedValue == "2") // Rentals
                {
                    _PersonClass = (from t in _Transactions
                                    join u in db.Rentals on t.RentalId equals u.RentalId
                                    join o in db.Users on t.TransactionUserId equals o.UserId
                                    select new PersonClass
                                    {
                                        TransactionLogId = t.TransactionLogId,
                                        TransactionFullName = o.FullName,
                                        TransactionDate = t.Dated,
                                        TransactionLogEnglishName = t.TransactionLogTypes.EnglishName,
                                        TransactionLogArabicName = t.TransactionLogTypes.ArabicName,
                                        TransactionLogForFullName = u.FullName
                                    }).ToList();
                }

                if (ddltransactionLog.SelectedValue == "3") // Owners
                {
                    _PersonClass = (from t in _Transactions
                                    join u in db.Owners on t.OwnerId equals u.OwnerId
                                    join o in db.Users on t.TransactionUserId equals o.UserId
                                    select new PersonClass
                                    {
                                        TransactionLogId = t.TransactionLogId,
                                        TransactionFullName = o.FullName,
                                        TransactionDate = t.Dated,
                                        TransactionLogEnglishName = t.TransactionLogTypes.EnglishName,
                                        TransactionLogArabicName = t.TransactionLogTypes.ArabicName,
                                        TransactionLogForFullName = u.FullName
                                    }).ToList();
                }

                if (ddltransactionLog.SelectedValue == "4") // Apartment
                {
                    _PersonClass = (from t in _Transactions
                                    join u in db.Units on t.UnitId equals u.UnitId
                                    join o in db.Users on t.TransactionUserId equals o.UserId
                                    select new PersonClass
                                    {
                                        TransactionLogId = t.TransactionLogId,
                                        TransactionFullName = o.FullName,
                                        TransactionDate = t.Dated,
                                        TransactionLogEnglishName = t.TransactionLogTypes.EnglishName,
                                        TransactionLogArabicName = t.TransactionLogTypes.ArabicName,
                                        TransactionLogForFullName = u.UnitName
                                    }).ToList();
                }

                if (ddltransactionLog.SelectedValue == "5") // Contracts
                {
                    _PersonClass = (from t in _Transactions
                                    join u in db.Contracts on t.ContractId equals u.ContractId
                                    join o in db.Users on t.TransactionUserId equals o.UserId
                                    select new PersonClass
                                    {
                                        TransactionLogId = t.TransactionLogId,
                                        TransactionFullName = o.FullName,
                                        TransactionDate = t.Dated,
                                        TransactionLogEnglishName = t.TransactionLogTypes.EnglishName,
                                        TransactionLogArabicName = t.TransactionLogTypes.ArabicName,
                                        TransactionLogForFullName = u.ContractId.ToString(),
                                    }).ToList();
                }


                DateTime _FromDate = Convert.ToDateTime(String.Format("{0:d/M/yyyy 00:00:00}", txtfromdate.Text));
                DateTime _ToDate = Convert.ToDateTime(String.Format("{0:d/M/yyyy 23:59:59}", txttodate.Text));


                _PersonClass = _PersonClass.Where(x => x.TransactionDate >= _FromDate && x.TransactionDate <= _ToDate).ToList();

                RptTransactionLog.DataSource = _PersonClass;
                RptTransactionLog.DataBind();
            }

        }





        public class PersonClass
        {
            public long TransactionLogId { get; set; }
            public string TransactionFullName { get; set; }
            public DateTime TransactionDate { get; set; }
            public string TransactionLogEnglishName { get; set; }
            public string TransactionLogArabicName { get; set; }
            public string TransactionLogForFullName { get; set; }
        }

    }
}