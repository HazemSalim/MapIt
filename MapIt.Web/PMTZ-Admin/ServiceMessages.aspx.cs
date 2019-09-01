using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web.Admin
{
    public partial class ServiceMessages : System.Web.UI.Page
    {
        #region Variables

        ServiceCommentsRepository serviceCommentsRepository;

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
                serviceCommentsRepository = new ServiceCommentsRepository();
                var list = serviceCommentsRepository.Find(m => m.ServiceId == ServiceId).OrderByDescending(m => m.AddedOn).AsQueryable();
                if (list != null && list.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        list = SortHelper.SortList<ServiceComment>(list, SortExpression, SortDirection);
                    }

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    pds.DataSource = list.ToList();
                    gvMessages.DataSource = pds;
                    gvMessages.DataBind();
                    AspNetPager1.RecordCount = list.Count();
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvMessages.DataSource = new List<TechMessage>();
                    gvMessages.DataBind();
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
                if (Session["AdminUserId"] != null && (int)ParseHelper.GetInt(Session["AdminUserId"].ToString()) > 1 &&
                    !(new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminUserId"].ToString()), (int)AppEnums.AdminPages.Services)))
                {
                    Response.Redirect(".");
                }

                long id = 0;
                if (Request.QueryString["id"] != null && long.TryParse(Request.QueryString["id"], out id))
                {
                    ServiceId = id;

                    var repository = new ServicesRepository();
                    var pObj = repository.GetByKey(ServiceId);
                    litTitle.Text = pObj.Title;

                    LoadData();
                }
            }
        }

        protected void gvMessages_Sorting(object sender, GridViewSortEventArgs e)
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