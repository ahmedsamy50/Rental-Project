using RentalProject.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RentalDataAccess;
using System.Data.Entity.Infrastructure;

namespace RentalProject.WebSite
{
    public partial class ShowAdvertising : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadAdvetising();
        }

        private void LoadAdvetising()
        {
            using (var db = new dbRentalsEntities())
            {
                UserClass UserCls = Authentication.GetUserFromSessionOrFromTicket();
                var Advertising = db.Advertising.Where(x => x.OrganizationId == UserCls._OrganizationId).Select(c => new { c.Active, c.AdvertisingId, c.Dated, c.EndDate, c.OrganizationId, c.Priority, c.StartDate, Image = "~/WebSite/Advertising/Photos/" + c.Image }).ToList().OrderByDescending(x => x.Dated);
                RptAdvertising.DataSource = Advertising;
                RptAdvertising.DataBind();

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
                        var _Delete = db.Advertising.Where(x => x.AdvertisingId == HF).ToList().SingleOrDefault();
                        if (_Delete != null)
                        {
                            db.Advertising.Remove(_Delete);
                            db.SaveChanges();
                            transaction.Commit();
                            LoadAdvetising();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "success('Saved',' Success','growl-success');", true);

                        }
                    }
                    catch (Exception)
                    {
                        transaction.Dispose();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "danger('Error','danger','growl-danger');", true);
                    }

                }

            }

        }
    }
}