using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RentalProject.Classes;
using System.Data.Entity;
using RentalDataAccess;


namespace RentalProject.MasterPage
{
    public partial class RentalMaster : System.Web.UI.MasterPage
    {
        public string userName;
        public string groupName;
        public string Email;
        UserClass UserCls = null;
        dbRentalsEntities ctx = new dbRentalsEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
            }

        }

    }
}