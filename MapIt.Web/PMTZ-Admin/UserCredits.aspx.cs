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
    public partial class UserCredits : System.Web.UI.Page
    {
        #region Variables

        UserCreditsRepository userCreditsRepository;
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

        public int? SearchPaymentStatus
        {
            get
            {
                int t = 0;
                if (ViewState["SearchPaymentStatus"] != null && int.TryParse(ViewState["SearchPaymentStatus"].ToString(), out t))
                    return t;
                return null;
            }
            set
            {
                ViewState["SearchPaymentStatus"] = value;
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

        public int? SearchPaymentMethod
        {
            get
            {
                int t = 0;
                if (ViewState["SearchPaymentMethod"] != null && int.TryParse(ViewState["SearchPaymentMethod"].ToString(), out t))
                    return t;
                return null;
            }
            set
            {
                ViewState["SearchPaymentMethod"] = value;
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

        void BindPaymentMethod()
        {
            try
            {
                ddlSearchPaymentMethod.DataValueField = "Id";
                ddlSearchPaymentMethod.DataTextField = "TitleEN";

                PaymentMethodsRepository paymentMethodsRepository = new PaymentMethodsRepository();
                var data = paymentMethodsRepository.Find(p => p.IsActive);
                if (data != null)
                {
                    ddlSearchPaymentMethod.DataSource = data;
                    ddlSearchPaymentMethod.DataBind();
                }
                data = null;
                paymentMethodsRepository = null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void BindPaymentStatus()
        {
            try
            {
                var status = from AppEnums.PaymentStatus s in AppEnums.PaymentStatus.GetValues(typeof(AppEnums.PaymentStatus))
                             select new
                             {
                                 ID = (int)s,
                                 Name = s.ToString()
                             };
                ddlSearchPaymentStatus.DataTextField = "Name";
                ddlSearchPaymentStatus.DataValueField = "ID";
                ddlSearchPaymentStatus.DataSource = status;
                ddlSearchPaymentStatus.DataBind();
                status = null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void LoadData()
        {
            try
            {
                userCreditsRepository = new UserCreditsRepository();
                IQueryable<UserCredit> list;

                if (Search)
                {
                    list = userCreditsRepository.Search(SearchUser, SearchPaymentMethod, SearchDateFrom, SearchDateTo, SearchPaymentStatus).OrderByDescending(o => o.TransOn);
                }
                else
                {
                    list = userCreditsRepository.GetAll().OrderByDescending(o => o.TransOn);
                }

                if (list != null && list.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        list = SortHelper.SortList<UserCredit>(list, SortExpression, SortDirection);
                    }

                    //if (Search && (SearchUser != null || SearchPaymentMethod != null || SearchDateFrom != null || SearchDateTo != null || SearchPaymentStatus != null))
                    //{
                    gvCreditsExcel.DataSource = list;
                    gvCreditsExcel.DataBind();
                    //}

                    int count = list.Count();
                    list = list.Skip((AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize).Take(AspNetPager1.PageSize);

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    //pds.DataSource = list.ToList();
                    gvCredits.DataSource = list.ToList();
                    gvCredits.DataBind();
                    AspNetPager1.RecordCount = count;
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvCreditsExcel.DataSource = new List<UserCredit>();
                    gvCreditsExcel.DataBind();

                    gvCredits.DataSource = new List<UserCredit>();
                    gvCredits.DataBind();
                    AspNetPager1.Visible = false;
                }

                // Search criteria
                lblSearchUser.Text = !string.IsNullOrEmpty(txtSearchUser.Text) ? txtSearchUser.Text : "All Users";
                lblSearchPaymentMethod.Text = ddlSearchPaymentMethod.SelectedItem.Text;
                lblSearchPaymentStatus.Text = ddlSearchPaymentStatus.SelectedItem.Text;
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
            ddlSearchPaymentStatus.SelectedIndex = 0;
            txtSearchUser.Text = hfSUserId.Value = txtSearchDateFrom.Text = txtSearchDateTo.Text = string.Empty;
            SearchUser = SearchPaymentMethod = SearchPaymentStatus = null;
            SearchDateFrom = SearchDateTo = null;
            Search = false;
            AspNetPager1.CurrentPageIndex = 0;
        }

        void Confirm(long id)
        {
            try
            {
                userCreditsRepository = new UserCreditsRepository();
                var creditObj = userCreditsRepository.GetByKey(id);
                if (creditObj != null)
                {
                    if (creditObj.PaymentStatus == (int)AppEnums.PaymentStatus.NotPaid)
                    {
                        creditObj.PaymentStatus = (int)AppEnums.PaymentStatus.Paid;

                        userCreditsRepository.Update(creditObj);

                        LoadData();
                        PresentHelper.ShowScriptMessage("Update successfully");
                    }
                    else
                    {
                        creditObj.PaymentStatus = (int)AppEnums.PaymentStatus.NotPaid;

                        userCreditsRepository.Update(creditObj);

                        LoadData();
                        PresentHelper.ShowScriptMessage("Update successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                PresentHelper.ShowScriptMessage("Error in updating");
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
                    !(new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminUserId"].ToString()), (int)AppEnums.AdminPages.Users)))
                {
                    Response.Redirect(".");
                }

                BindPaymentMethod();
                BindPaymentStatus();
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
            SearchPaymentMethod = ParseHelper.GetInt(ddlSearchPaymentMethod.SelectedValue);
            SearchPaymentStatus = ParseHelper.GetInt(ddlSearchPaymentStatus.SelectedValue);
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

        protected void gvCredits_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ConfirmItem")
            {
                Confirm(Int64.Parse(e.CommandArgument.ToString()));
            }
        }

        protected void gvCredits_Sorting(object sender, GridViewSortEventArgs e)
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
                if (gvCreditsExcel.Rows.Count == 0)
                {
                    PresentHelper.ShowScriptMessage("Please search for the criteria you want to export.");
                    return;
                }

                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "credits" + DateTime.Now + ".xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                //Change the Header Row back to white color
                gvCreditsExcel.HeaderRow.Style.Add("background-color", "#FFFFFF");

                for (int i = 0; i < gvCreditsExcel.Rows.Count; i++)
                {
                    GridViewRow row = gvCreditsExcel.Rows[i];

                    //Change Color back to white
                    row.BackColor = System.Drawing.Color.White;

                    //Apply text style to each Row
                    row.Attributes.Add("class", "textmode");
                }
                gvCreditsExcel.RenderControl(htmltextwrtter);

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