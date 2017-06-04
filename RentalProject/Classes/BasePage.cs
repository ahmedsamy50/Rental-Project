using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RentalDataAccess;

namespace RentalProject.Classes
{
    public class BasePage : System.Web.UI.Page
    {
        #region Fields
        private int _PageSize = 10;
        private string _PageName = string.Empty;

        //  private Enums.WhichModule _WhichModule;
        #endregion

        #region Properties


        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                _PageSize = value;
            }
        }
        public string PageName
        {
            get
            {
                return _PageName;
            }
            set
            {
                _PageName = value;
            }
        }

        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                {
                    ViewState["sortDirection"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }


        public string GridViewSortExpression
        {
            get
            {
                if (ViewState["sortExpression"] == null)
                    ViewState["sortExpression"] = "Id";

                return ViewState["sortExpression"].ToString();
            }
            set { ViewState["sortExpression"] = value; }
        }

        #endregion

        #region Constructors
        public BasePage()
        {
        }
        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (!IsPostBack)
            {

                UserClass UserCls = Authentication.GetUserFromSessionOrFromTicket();

                if (UserCls != null)
                {
                    Modules ModuleCls = null;
                    if (string.IsNullOrEmpty(_PageName))
                    {
                        using (dbRentalsEntities ctx = new dbRentalsEntities())
                        {
                            string pName = UtilityClass.GetCurrentPageName().ToLower();
                            ModuleCls = ctx.Modules.FirstOrDefault(o => o.URL.ToLower().Contains(pName));
                        }
                    }
                    else
                    {
                        using (dbRentalsEntities ctx = new dbRentalsEntities())
                        {
                            ModuleCls = ctx.Modules.FirstOrDefault(o => o.URL.ToLower().Contains(_PageName.ToLower()));
                        }
                    }

                    if (ModuleCls != null)
                    {
                       
                    }
                    else
                    {
                        Response.Redirect("~/_ErrorPages/PermissionPage.html");
                    }
                }
                else
                {
                    UtilityClass.GoToLoginPage();
                }
            }

        }

        #endregion

    }
}