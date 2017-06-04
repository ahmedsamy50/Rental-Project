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
    public partial class ShowUnitPrice : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUnitPrice();
            }
        }

        private void LoadUnitPrice()
        {
            using (var db = new dbRentalsEntities())
            {
                var _LoadUnitPrice = (from p in db.UnitPrices
                                     select new
                                     {
                                         p.Currency.ArabicName,
                                         p.Currency.EnglishName,
                                         p.UnitPriceId,
                                         p.Price,
                                         p.Units.UnitNumber,
                                         p.Units.UnitName,
                                         p.Units.Owners.FullName

                                     }).ToList().OrderBy(x=> x.UnitNumber).ThenBy(x=> x.UnitName).ThenBy(x=> x.Price);
                RptUsers.DataSource = _LoadUnitPrice;
                RptUsers.DataBind();
            }
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
                        var _Deleted = db.UnitPrices.Where(x => x.UnitPriceId == HF).ToList().SingleOrDefault();
                        db.UnitPrices.Attach(_Deleted);
                        db.UnitPrices.Remove(_Deleted);
                        db.SaveChanges();
                        transaction.Commit();
                        LoadUnitPrice();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "success('Saved',' Success','growl-success');", true);
                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "javascript:parent.danger('Error ','Delete','growl-danger');", true);
                        transaction.Dispose();
                    }

                }
            }


        }
    }
}