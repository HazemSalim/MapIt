using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Data;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web.Admin
{
    public partial class TechMessages : System.Web.UI.Page
    {
        #region Variables

        TechMessagesRepository techMessagesRepository;

        #endregion Variables

        #region Properties

        public bool Search
        {
            get
            {
                bool s = false;
                if (ViewState["Search"] != null && bool.TryParse(ViewState["Search"].ToString(), out s))
                    return s;
                return false;
            }
            set
            {
                ViewState["Search"] = value;
            }
        }

        public DateTime? SearchDateFrom
        {
            get
            {
                if (ViewState["SearchDateFrom"] != null)
                    return DateTime.Parse(ViewState["SearchDateFrom"].ToString());
                return null;
            }
            set
            {
                ViewState["SearchDateFrom"] = value;
            }
        }

        public DateTime? SearchDateTo
        {
            get
            {
                if (ViewState["SearchDateTo"] != null)
                    return DateTime.Parse(ViewState["SearchDateTo"].ToString());
                return null;
            }
            set
            {
                ViewState["SearchDateTo"] = value;
            }
        }

        public long? SearchUser
        {
            get
            {
                long t = 0;
                if (ViewState["SearchUser"] != null && long.TryParse(ViewState["SearchUser"].ToString(), out t))
                    return t;
                return null;
            }
            set
            {
                ViewState["SearchUser"] = value;
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
                techMessagesRepository = new TechMessagesRepository();
                IQueryable<MapIt.Data.User> list;

                if (Search)
                {
                    list = techMessagesRepository.Search(SearchUser, SearchDateFrom, SearchDateTo).Select(m => m.User).Distinct();
                }
                else
                {
                    list = techMessagesRepository.GetAll().Select(m => m.User).Distinct();
                }

                if (list != null && list.Count() > 0)
                {
                    list = list.OrderByDescending(u => u.TechMessages.Max(t => t.AddedOn));

                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        list = SortHelper.SortList<MapIt.Data.User>(list, SortExpression, SortDirection);
                    }

                    int count = list.Count();
                    list = list.Skip((AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize).Take(AspNetPager1.PageSize);

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    //pds.DataSource = list.ToList();
                    gvUsers.DataSource = list.ToList();
                    gvUsers.DataBind();
                    AspNetPager1.RecordCount = count;
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvUsers.DataSource = new List<UserBalanceLog>();
                    gvUsers.DataBind();
                    AspNetPager1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        public void ClearSearch()
        {
            txtSearchUser.Text = hfSUserId.Value = txtSearchDateFrom.Text = txtSearchDateTo.Text = string.Empty;
            SearchUser = null;
            SearchDateFrom = SearchDateTo = null;
            Search = false;
            AspNetPager1.CurrentPageIndex = 0;
        }

        #endregion Methods

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AdminUserId"] != null && (int)ParseHelper.GetInt(Session["AdminUserId"].ToString()) > 1 &&
                    !(new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminUserId"].ToString()), (int)AppEnums.AdminPages.Users)))
                {
                    Response.Redirect(".");
                }

                LoadData();
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void hfUserId_ValueChanged(object sender, EventArgs e)
        {
            long? userId = ParseHelper.GetInt64(hfSUserId.Value);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search = true;

            SearchUser = ParseHelper.GetInt64(hfSUserId.Value);
            SearchDateFrom = ParseHelper.GetDate(txtSearchDateFrom.Text, "yyyy-MM-dd", null);
            SearchDateTo = ParseHelper.GetDate(txtSearchDateTo.Text, "yyyy-MM-dd", null);

            AspNetPager1.CurrentPageIndex = 0;
            LoadData();
        }

        protected void gvUsers_Sorting(object sender, GridViewSortEventArgs e)
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

        #endregion Events
    }
}