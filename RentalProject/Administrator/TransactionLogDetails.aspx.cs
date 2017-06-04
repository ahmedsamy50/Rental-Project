using System;
using System.Collections.Generic;
using System.Linq;
using RentalDataAccess;

namespace RentalProject.Administrator
{
    public partial class TransactionLogDetails : System.Web.UI.Page
    {
        public string Sign = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["TransactionLogId"]))
                    LoadData(Convert.ToInt32(Request.QueryString["TransactionLogId"]));
            }
        }

        private void LoadData(Int32 TransactionLogId)
        {
            using (var db = new dbRentalsEntities())
            {
                var _TransactionLogId = db.TransactionLogs.Where(x => x.TransactionLogId == TransactionLogId).SingleOrDefault();
                List<Owners> _Owners = new List<Owners>();
                List<Users> _Users = new List<Users>();
                List<Rentals> _Rentals = new List<Rentals>();
                List<Units> _Units = new List<Units>();
                List<Contracts> _Contracts = new List<Contracts>();
                if (_TransactionLogId != null)
                {
                    string y = null;
                    var _Data = y;
                    if (_TransactionLogId.UserId != null) // User
                    {
                        _Users = db.Users.Where(x => x.UserId == _TransactionLogId.UserId).ToList();
                        Sign = "User";
                    }

                    if (_TransactionLogId.RentalId != null) // Rental
                    {
                        _Rentals = db.Rentals.Where(x => x.RentalId == _TransactionLogId.RentalId).ToList();
                        Sign = "Rental";
                    }

                    if (_TransactionLogId.OwnerId != null) // Owner
                    {
                        _Owners = db.Owners.Where(x => x.OwnerId == _TransactionLogId.OwnerId).ToList();
                        Sign = "Owner";
                    }

                    if (_TransactionLogId.UnitId != null) // Unit
                    {
                        var _LoadUnits = (from u in db.Units
                                          where u.UnitId == _TransactionLogId.UnitId
                                          select new
                                          {
                                              UnitId = u.UnitId,
                                              UnitNumber = u.UnitNumber,
                                              UnitName = u.UnitName,
                                              Street = u.Streets.EnglishName + "_" + u.Streets.ArabicName,
                                              UnitType = u.UnitTypes.EnglishName + "_" + u.UnitTypes.ArabicName,
                                              Owner = u.Owners.FullName,
                                              Description = u.Description,
                                          }).ToList().OrderBy(x => x.Owner).ThenBy(x => x.UnitNumber);
                        RptUnits.DataSource = _LoadUnits;
                        RptUnits.DataBind();
                        RptUnits.Visible = true;
                        Sign = "Unit";
                        return;

                    }

                    if (_TransactionLogId.ContractId != null) // ContractId
                    {
                        var _LoadContracts = (from u in db.Contracts
                                          where u.ContractId == _TransactionLogId.ContractId
                                          select new
                                          {
                                              ContractId = u.ContractId,
                                              StartDate = u.StartDate,
                                              EndDate = u.EndDate,
                                              Price = u.Price,
                                              Description = u.Descriptions,
                                          }).ToList().OrderBy(x => x.StartDate);
                        rptContract.DataSource = _LoadContracts;
                        rptContract.DataBind();
                        rptContract.Visible = true;
                        Sign = "Contracts";
                        return;

                    }



                    RptDetails.Visible = true;
                    if (_Users.Count() > 0)
                    {
                        RptDetails.DataSource = _Users;
                        RptDetails.DataBind();
                    }
                    if (_Rentals.Count() > 0)
                    {
                        RptDetails.DataSource = _Rentals;
                        RptDetails.DataBind();
                    }
                    if (_Owners.Count() > 0)
                    {
                        RptDetails.DataSource = _Owners;
                        RptDetails.DataBind();
                    }

                  

                }
            }
        }
    }
}