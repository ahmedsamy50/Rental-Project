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
    public partial class ShowApartmentType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadApprtmentTypes();
            }
        }

        private void LoadApprtmentTypes()
        {
            using (var db = new dbRentalsEntities())
            {
                var _LoadApprtmentTypes = db.UnitTypes.ToList().OrderBy(x => x.EnglishName);
                RptUsers.DataSource = _LoadApprtmentTypes;
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
                        var _Deleted = db.UnitTypes.Where(x => x.UnitTypeId == HF).ToList().SingleOrDefault();
                        db.UnitTypes.Attach(_Deleted);
                        db.UnitTypes.Remove(_Deleted);
                        db.SaveChanges();
                        transaction.Commit();
                        LoadApprtmentTypes();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "success('Saved',' Success','growl-success');", true);
                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "javascript:parent.danger('Apprtment Type has one or more unit(s) ','Delete unit first','growl-danger');", true);
                        transaction.Dispose();
                    }

                }
            }


        }
    }
}