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
    public partial class PhotographersRequests : System.Web.UI.Page
    {
        #region Variables

        MerchantsRepository merchantsRepository;
        CountriesRepository countriesRepository;

        #endregion Variables

        #region Properties

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

        public int? SearchActiveStatus
        {
            get
            {
                int t = 0;
                if (ViewState["SearchActiveStatus"] != null && int.TryParse(ViewState["SearchActiveStatus"].ToString(), out t))
                    return t;
                return null;
            }
            set
            {
                ViewState["SearchActiveStatus"] = value;
            }
        }

        public DateTime? SearchCDateFrom
        {
            get
            {
                if (ViewState["SearchCDateFrom"] != null)
                    return DateTime.Parse(ViewState["SearchCDateFrom"].ToString());
                return null;
            }
            set
            {
                ViewState["SearchCDateFrom"] = value;
            }
        }

        public DateTime? SearchCDateTo
        {
            get
            {
                if (ViewState["SearchCDateTo"] != null)
                    return DateTime.Parse(ViewState["SearchCDateTo"].ToString());
                return null;
            }
            set
            {
                ViewState["SearchCDateTo"] = value;
            }
        }

        public string SearchKeyWord
        {
            get
            {
                if (ViewState["SearchKeyWord"] != null)
                    return ViewState["SearchKeyWord"].ToString();
                return null;
            }
            set
            {
                ViewState["SearchKeyWord"] = value;
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

        void BindCountries()
        {
            try
            {
                ddlCode.DataValueField = "CCode";
                ddlCode.DataTextField = "FullCode";

                countriesRepository = new CountriesRepository();
                var data = countriesRepository.Find(c => c.IsActive).ToList();
                if (data != null)
                {
                    ddlCode.DataSource = data.OrderBy(c => c.ISOCode).ToList();
                    ddlCode.DataBind();
                }

                data = null;
                countriesRepository = null;
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
                merchantsRepository = new MerchantsRepository();
                IQueryable<MapIt.Data.Merchant> merchantsList;

                if (Search)
                {
                    merchantsList = merchantsRepository.Search(null, SearchActiveStatus, SearchCDateFrom, SearchCDateTo, SearchKeyWord);
                }
                else
                {
                    merchantsList = merchantsRepository.GetAll();
                }

                if (merchantsList != null && merchantsList.Count() > 0)
                {
                    merchantsList = merchantsList.OrderByDescending(u => u.Id);

                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        merchantsList = SortHelper.SortList<MapIt.Data.Merchant>(merchantsList, SortExpression, SortDirection);
                    }

                    if (Search && (SearchActiveStatus != null || SearchCDateFrom != null || SearchCDateTo != null || !string.IsNullOrEmpty(SearchKeyWord)))
                    {
                        gvMerchantsExcel.DataSource = merchantsList;
                        gvMerchantsExcel.DataBind();
                    }

                    int count = merchantsList.Count();
                    merchantsList = merchantsList.Skip((AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize).Take(AspNetPager1.PageSize);

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    gvMerchants.DataSource = merchantsList.ToList();
                    gvMerchants.DataBind();
                    AspNetPager1.RecordCount = count;
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvMerchantsExcel.DataSource = new List<MapIt.Data.Merchant>();
                    gvMerchantsExcel.DataBind();

                    gvMerchants.DataSource = new List<MapIt.Data.Merchant>();
                    gvMerchants.DataBind();
                    AspNetPager1.Visible = false;
                }

                // Srearch criteria
                lblSearchActiveStatus.Text = ddlSearchActiveStatus.SelectedItem.Text;
                lblSearchCDateFrom.Text = !string.IsNullOrEmpty(txtSearchCDateFrom.Text) ? txtSearchCDateFrom.Text : "All Days";
                lblSearchCDateTo.Text = !string.IsNullOrEmpty(txtSearchCDateTo.Text) ? txtSearchCDateTo.Text : "All Days";
                lblSearchKeyword.Text = txtSearchKeyWord.Text;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        public void ClearSearch()
        {
            ddlSearchActiveStatus.SelectedIndex = 0;
            txtSearchCDateFrom.Text = txtSearchCDateTo.Text = txtSearchKeyWord.Text = SearchKeyWord = string.Empty;
            SearchActiveStatus = null;
            SearchCDateFrom = SearchCDateTo = null;
            Search = false;
            AspNetPager1.CurrentPageIndex = 0;
        }

        public void ClearControls()
        {
            txtFullName.Text = txtCountry.Text = txtCity.Text = txtPhone.Text = txtEmail.Text = txtCompanyName.Text = txtDetails.Text = string.Empty;
            ddlCode.SelectedIndex = 0;
            chkActive.Checked = true;
            gvMerchants.SelectedIndex = -1;
            RecordId = null;
        }

        void Add()
        {
            try
            {
                merchantsRepository = new MerchantsRepository();
                var uObj = merchantsRepository.GetByPhone(txtPhone.Text);
                if (uObj != null)
                {
                    PresentHelper.ShowScriptMessage("This phone already exist");
                    return;
                }

                uObj = merchantsRepository.GetByEmail(txtEmail.Text);
                if (uObj != null)
                {
                    PresentHelper.ShowScriptMessage("This email already exist");
                    return;
                }

                uObj = merchantsRepository.GetByCompanyName(txtCompanyName.Text);
                if (uObj != null)
                {
                    PresentHelper.ShowScriptMessage("This Company Name already exist");
                    return;
                }

                var merchantObj = new MapIt.Data.Merchant();
                merchantObj.FullName = txtFullName.Text;
                merchantObj.Country = txtCountry.Text;
                merchantObj.City = txtCity.Text;
                merchantObj.Phone = ddlCode.SelectedValue + " " + txtPhone.Text;
                merchantObj.Email = txtEmail.Text;
                merchantObj.CompanyName = txtCompanyName.Text;
                merchantObj.Details = txtDetails.Text;
                merchantObj.IsActive = chkActive.Checked;
                merchantObj.AddedOn = DateTime.Now;

                merchantsRepository.Add(merchantObj);

                ClearSearch();
                ClearControls();
                LoadData();
                PresentHelper.ShowScriptMessage("Add successfully");
                pnlAllRecords.Visible = true;
                pnlRecordDetails.Visible = false;
            }
            catch (Exception ex)
            {
                PresentHelper.ShowScriptMessage("Error in adding");
                LogHelper.LogException(ex);
            }
        }

        void Update(long id)
        {
            try
            {
                merchantsRepository = new MerchantsRepository();
                var uObj = merchantsRepository.GetByPhone(txtPhone.Text, id);
                if (uObj != null)
                {
                    PresentHelper.ShowScriptMessage("This phone already exist");
                    return;
                }

                uObj = merchantsRepository.GetByEmail(txtEmail.Text, id);
                if (uObj != null)
                {
                    PresentHelper.ShowScriptMessage("This email already exist");
                    return;
                }

                uObj = merchantsRepository.GetByCompanyName(txtCompanyName.Text, id);
                if (uObj != null)
                {
                    PresentHelper.ShowScriptMessage("This Company Name already exist");
                    return;
                }

                var merchantObj = merchantsRepository.GetByKey(id);

                if (merchantObj == null)
                {
                    PresentHelper.ShowScriptMessage("Error in updating");
                    return;
                }

                merchantObj.FullName = txtFullName.Text;
                merchantObj.Country = txtCountry.Text;
                merchantObj.City = txtCity.Text;
                merchantObj.Phone = ddlCode.SelectedValue + " " + txtPhone.Text;
                merchantObj.Email = txtEmail.Text;
                merchantObj.CompanyName = txtCompanyName.Text;
                merchantObj.Details = txtDetails.Text;
                merchantObj.IsActive = chkActive.Checked;
                merchantObj.ModifiedOn = DateTime.Now;
                merchantsRepository.Update(merchantObj);

                ClearSearch();
                ClearControls();
                LoadData();
                PresentHelper.ShowScriptMessage("Update successfully");
                pnlAllRecords.Visible = true;
                pnlRecordDetails.Visible = false;
            }
            catch (Exception ex)
            {
                PresentHelper.ShowScriptMessage("Error in updating");
                LogHelper.LogException(ex);
            }
        }

        void Delete(long id)
        {
            try
            {
                merchantsRepository = new MerchantsRepository();
                var merchantObj = merchantsRepository.GetByKey(id);
                if (merchantObj != null)
                {
                    merchantsRepository.Delete(merchantObj);

                    ClearSearch();
                    ClearControls();
                    LoadData();
                    PresentHelper.ShowScriptMessage("Delete successfully");
                }
            }
            catch (Exception ex)
            {
                PresentHelper.ShowScriptMessage("Error in deleting, as there are many objects related to this record.");
                LogHelper.LogException(ex);
            }
        }

        bool SetData(long id)
        {
            try
            {
                merchantsRepository = new MerchantsRepository();
                var merchantObj = merchantsRepository.GetByKey(id);
                if (merchantObj != null)
                {
                    txtFullName.Text = merchantObj.FullName;
                    txtCountry.Text = merchantObj.Country;
                    txtCity.Text = merchantObj.City;

                    var phone = merchantObj.Phone.Split(' ');
                    ddlCode.SelectedValue = phone[0];
                    txtPhone.Text = phone[1];

                    txtEmail.Text = merchantObj.Email;
                    txtCompanyName.Text = merchantObj.CompanyName;

                    chkActive.Checked = merchantObj.IsActive;

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return false;
            }
        }

        void ExportToExcel()
        {
            try
            {

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
                if (Session["AdminMerchantId"] != null && (int)ParseHelper.GetInt(Session["AdminMerchantId"].ToString()) > 1 &&
                    !(new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminMerchantId"].ToString()), (int)AppEnums.AdminPages.Requests)))
                {
                    Response.Redirect(".");
                }

                BindCountries();
                LoadData();
                pnlAllRecords.Visible = true;
                pnlRecordDetails.Visible = false;
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            ClearSearch();
            ClearControls();
            pnlAllRecords.Visible = false;
            pnlRecordDetails.Visible = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (RecordId.HasValue)
            {
                Update(RecordId.Value);
            }
            else
            {
                Add();
            }
        }

        protected void gvMerchants_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditItem")
            {
                if (SetData(int.Parse(e.CommandArgument.ToString())))
                {
                    RecordId = int.Parse(e.CommandArgument.ToString());
                    pnlAllRecords.Visible = false;
                    pnlRecordDetails.Visible = true;
                }
            }
            else if (e.CommandName == "DeleteItem")
            {
                Delete(int.Parse(e.CommandArgument.ToString()));
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearSearch();
            ClearControls();
            pnlAllRecords.Visible = true;
            pnlRecordDetails.Visible = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search = true;

            SearchActiveStatus = ParseHelper.GetInt(ddlSearchActiveStatus.SelectedValue);
            SearchCDateFrom = ParseHelper.GetDate(txtSearchCDateFrom.Text, "yyyy-MM-dd", null);
            SearchCDateTo = ParseHelper.GetDate(txtSearchCDateTo.Text, "yyyy-MM-dd", null);
            SearchKeyWord = txtSearchKeyWord.Text;

            AspNetPager1.CurrentPageIndex = 0;
            LoadData();
        }

        protected void gvMerchants_Sorting(object sender, GridViewSortEventArgs e)
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
                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "merchants" + DateTime.Now + ".xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);

                //Change the Header Row back to white color
                gvMerchantsExcel.HeaderRow.Style.Add("background-color", "#FFFFFF");

                for (int i = 0; i < gvMerchantsExcel.Rows.Count; i++)
                {
                    GridViewRow row = gvMerchantsExcel.Rows[i];

                    //Change Color back to white
                    row.BackColor = System.Drawing.Color.White;

                    //Apply text style to each Row
                    row.Attributes.Add("class", "textmode");
                }
                gvMerchantsExcel.RenderControl(htmltextwrtter);

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

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Page.Validate(btnSave.ValidationGroup);

            base.OnPreRenderComplete(e);
        }

        #endregion Events
    }
}