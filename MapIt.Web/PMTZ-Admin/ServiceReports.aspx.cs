using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web.PMTZ_Admin
{
    public partial class ServiceReports : System.Web.UI.Page
    {
        #region Variables

        ServiceReportsRepository ServiceReportsRepository;

        #endregion Variables

        #region Properties

        long ServiceId
        {
            get
            {
                long id = 0;
                if (ViewState["ServiceId"] != null && long.TryParse(ViewState["ServiceId"].ToString(), out id))
                    return id;
                return 0;
            }
            set
            {
                ViewState["ServiceId"] = value;
            }
        }

        public long? RecordId
        {
            get
            {
                long id = 0;

                if (ViewState["RecordId"] != null && long.TryParse(ViewState["RecordId"].ToString(), out id))
                    return id;

                return null;
            }
            set
            {
                ViewState["RecordId"] = value;
            }
        }

        public string SortDirection
        {
            get
            {
                if (ViewState["SortDirection"] != null)
                    return ViewState["SortDirection"].ToString();

                return string.Empty;
            }
            set
            {
                ViewState["SortDirection"] = value;
            }
        }

        public string SortExpression
        {
            get
            {
                if (ViewState["SortExpression"] != null)
                    return ViewState["SortExpression"].ToString();

                return string.Empty;
            }
            set
            {
                ViewState["SortExpression"] = value;
            }
        }

        #endregion

        #region Methods

        void LoadData()
        {
            try
            {
                ServiceReportsRepository = new ServiceReportsRepository();
                var list = ServiceReportsRepository.Find(m => m.ServiceId == ServiceId).OrderByDescending(m => m.CreatedOn).AsQueryable();
                if (list != null && list.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        list = SortHelper.SortList(list, SortExpression, SortDirection);
                    }

                    PagedDataSource pds = new PagedDataSource
                    {
                        AllowPaging = true,
                        PageSize = AspNetPager1.PageSize,
                        CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1,

                        DataSource = list.ToList()
                    };
                    gvReports.DataSource = pds;
                    gvReports.DataBind();
                    AspNetPager1.RecordCount = list.Count();
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvReports.DataSource = new List<ServiceReport>();
                    gvReports.DataBind();
                    AspNetPager1.Visible = false;
                }

            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        #endregion Methods

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AdminUserId"] != null && ParseHelper.GetInt(Session["AdminUserId"].ToString()) > 1 &&
                    !new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminUserId"].ToString()), (int)AppEnums.AdminPages.Properties))
                {
                    Response.Redirect(".");
                }

                long id = 0;
                if (Request.QueryString["id"] != null && long.TryParse(Request.QueryString["id"], out id))
                {
                    ServiceId = id;

                    var repository = new PropertiesRepository();
                    var pObj = repository.GetByKey(ServiceId);
                    litTitle.Text = pObj.TitleEN + " - " + pObj.AddressEN;

                    LoadData();
                }
            }
        }

        protected void gvReports_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortDirection = "ASC";
            if (!string.IsNullOrEmpty(SortExpression) && SortExpression == e.SortExpression
                && !string.IsNullOrEmpty(SortDirection) && SortDirection.Trim().ToLower() == "asc")
            {
                sortDirection = "DESC";
            }

            SortDirection = sortDirection;
            SortExpression = e.SortExpression;

            LoadData();
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        #endregion Events
    }
}