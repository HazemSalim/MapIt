using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web.Admin
{
    public partial class UserBalanceLogs : System.Web.UI.Page
    {
        #region Variables

        UserBalanceLogsRepository userBalanceLogsRepository;
        UsersRepository usersRepository;

        #endregion Variables

        #region Properties

        public Int64? RecordId
        {
            get
            {
                Int64 id = 0;

                if (ViewState["RecordId"] != null && Int64.TryParse(ViewState["RecordId"].ToString(), out id))
                    return id;

                return null;
            }
            set
            {
                ViewState["RecordId"] = value;
            }
        }

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
                userBalanceLogsRepository = new UserBalanceLogsRepository();
                IQueryable<UserBalanceLog> list;

                if (Search)
                {
                    list = userBalanceLogsRepository.Search(SearchUser, SearchDateFrom, SearchDateTo).OrderByDescending(o => o.TransOn);
                }
                else
                {
                    list = userBalanceLogsRepository.GetAll().OrderByDescending(o => o.TransOn);
                }

                if (list != null && list.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        list = SortHelper.SortList<UserBalanceLog>(list, SortExpression, SortDirection);
                    }

                    //if (Search && (SearchUser != null || SearchDateFrom != null || SearchDateTo != null))
                    //{
                    gvLogsExcel.DataSource = list;
                    gvLogsExcel.DataBind();
                    //}

                    int count = list.Count();
                    list = list.Skip((AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize).Take(AspNetPager1.PageSize);

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    //pds.DataSource = list.ToList();
                    gvLogs.DataSource = list.ToList();
                    gvLogs.DataBind();
                    AspNetPager1.RecordCount = count;
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvLogsExcel.DataSource = new List<UserBalanceLog>();
                    gvLogsExcel.DataBind();

                    gvLogs.DataSource = new List<UserBalanceLog>();
                    gvLogs.DataBind();
                    AspNetPager1.Visible = false;
                }

                // Search criteria
                lblSearchUser.Text = !string.IsNullOrEmpty(txtSearchUser.Text) ? txtSearchUser.Text : "All Users";
                lblSearchDateFrom.Text = !string.IsNullOrEmpty(txtSearchDateFrom.Text) ? txtSearchDateFrom.Text : "All Days";
                lblSearchDateTo.Text = !string.IsNullOrEmpty(txtSearchDateTo.Text) ? txtSearchDateTo.Text : "All Days";
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        public string GetPaymentStatusName(object status)
        {
            try
            {
                if (status != null && !string.IsNullOrEmpty(status.ToString()))
                {
                    return AppEnums.PaymentStatus.GetName(typeof(AppEnums.PaymentStatus), status);
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return string.Empty;
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
            usersRepository = new UsersRepository();
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

        protected void btnExport_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearSearch();
        }

        protected void gvLogs_Sorting(object sender, GridViewSortEventArgs e)
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvLogsExcel.Rows.Count == 0)
                {
                    PresentHelper.ShowScriptMessage("Please search for the criteria you want to export.");
                    return;
                }

                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "logs" + DateTime.Now + ".xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                //Change the Header Row back to white color
                gvLogsExcel.HeaderRow.Style.Add("background-color", "#FFFFFF");

                for (int i = 0; i < gvLogsExcel.Rows.Count; i++)
                {
                    GridViewRow row = gvLogsExcel.Rows[i];

                    //Change Color back to white
                    row.BackColor = System.Drawing.Color.White;

                    //Apply text style to each Row
                    row.Attributes.Add("class", "textmode");
                }
                gvLogsExcel.RenderControl(htmltextwrtter);

                //style to format numbers to string
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(strwritter.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        #endregion Events
    }
}