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
    public partial class Photographers : System.Web.UI.Page
    {
        #region Variables

        PhotographersRepository photographersRepository;
        //CountriesRepository countriesRepository;

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

        void LoadData()
        {
            try
            {
                photographersRepository = new PhotographersRepository();
                IQueryable<MapIt.Data.Photographer> photographersList;

                if (Search)
                {
                    photographersList = photographersRepository.Search(null, SearchActiveStatus, SearchCDateFrom, SearchCDateTo, SearchKeyWord);
                }
                else
                {
                    photographersList = photographersRepository.GetAll();
                }

                if (photographersList != null && photographersList.Count() > 0)
                {
                    photographersList = photographersList.OrderByDescending(u => u.Id);

                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        photographersList = SortHelper.SortList<MapIt.Data.Photographer>(photographersList, SortExpression, SortDirection);
                    }

                    //if (Search && (SearchActiveStatus != null || SearchCDateFrom != null || SearchCDateTo != null || !string.IsNullOrEmpty(SearchKeyWord)))
                    //{
                    gvPhotographersExcel.DataSource = photographersList;
                    gvPhotographersExcel.DataBind();
                    //}

                    int count = photographersList.Count();
                    photographersList = photographersList.Skip((AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize).Take(AspNetPager1.PageSize);

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    gvPhotographers.DataSource = photographersList.ToList();
                    gvPhotographers.DataBind();
                    AspNetPager1.RecordCount = count;
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvPhotographersExcel.DataSource = new List<MapIt.Data.Photographer>();
                    gvPhotographersExcel.DataBind();

                    gvPhotographers.DataSource = new List<MapIt.Data.Photographer>();
                    gvPhotographers.DataBind();
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
            txtFullName.Text = txtCountry.Text = txtCity.Text = txtCode.Text = txtPhone.Text = txtEmail.Text =
                txtDetails.Text = string.Empty;
            chkActive.Checked = true;
            gvPhotographers.SelectedIndex = -1;
            RecordId = null;
        }

        void Add()
        {
            try
            {
                photographersRepository = new PhotographersRepository();
                var uObj = photographersRepository.GetByPhone(txtPhone.Text);
                if (uObj != null)
                {
                    PresentHelper.ShowScriptMessage("This phone already exist");
                    return;
                }

                uObj = photographersRepository.GetByEmail(txtEmail.Text);
                if (uObj != null)
                {
                    PresentHelper.ShowScriptMessage("This email already exist");
                    return;
                }

                var photographerObj = new MapIt.Data.Photographer();
                photographerObj.FullName = txtFullName.Text;
                photographerObj.Country = txtCountry.Text;
                photographerObj.City = txtCity.Text;
                photographerObj.Phone = txtCode.Text + " " + txtPhone.Text;
                photographerObj.Email = txtEmail.Text;
                photographerObj.Details = txtDetails.Text;
                photographerObj.IsActive = chkActive.Checked;
                photographerObj.AddedOn = DateTime.Now;

                photographersRepository.Add(photographerObj);

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
                photographersRepository = new PhotographersRepository();
                var uObj = photographersRepository.GetByPhone(txtPhone.Text, id);
                if (uObj != null)
                {
                    PresentHelper.ShowScriptMessage("This phone already exist");
                    return;
                }

                uObj = photographersRepository.GetByEmail(txtEmail.Text, id);
                if (uObj != null)
                {
                    PresentHelper.ShowScriptMessage("This email already exist");
                    return;
                }

                var photographerObj = photographersRepository.GetByKey(id);

                if (photographerObj == null)
                {
                    PresentHelper.ShowScriptMessage("Error in updating");
                    return;
                }

                photographerObj.FullName = txtFullName.Text;
                photographerObj.Country = txtCountry.Text;
                photographerObj.City = txtCity.Text;
                photographerObj.Phone = txtCode.Text + " " + txtPhone.Text;
                photographerObj.Email = txtEmail.Text;
                photographerObj.Details = txtDetails.Text;
                photographerObj.IsActive = chkActive.Checked;
                photographerObj.ModifiedOn = DateTime.Now;
                photographersRepository.Update(photographerObj);

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
                photographersRepository = new PhotographersRepository();
                var photographerObj = photographersRepository.GetByKey(id);
                if (photographerObj != null)
                {
                    photographersRepository.Delete(photographerObj);

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
                photographersRepository = new PhotographersRepository();
                var photographerObj = photographersRepository.GetByKey(id);
                if (photographerObj != null)
                {
                    txtFullName.Text = photographerObj.FullName;
                    txtCountry.Text = photographerObj.Country;
                    txtCity.Text = photographerObj.City;

                    var phone = photographerObj.Phone.Split(' ');
                    txtCode.Text = phone[0];
                    txtPhone.Text = phone[1];

                    txtEmail.Text = photographerObj.Email;
                    txtDetails.Text = photographerObj.Details;

                    chkActive.Checked = photographerObj.IsActive;

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
                if (Session["AdminPhotographerId"] != null && (int)ParseHelper.GetInt(Session["AdminPhotographerId"].ToString()) > 1 &&
                    !(new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminPhotographerId"].ToString()), (int)AppEnums.AdminPages.Requests)))
                {
                    Response.Redirect(".");
                }

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

        protected void gvPhotographers_RowCommand(object sender, GridViewCommandEventArgs e)
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

        protected void gvPhotographers_Sorting(object sender, GridViewSortEventArgs e)
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
                if (gvPhotographersExcel.Rows.Count == 0)
                {
                    PresentHelper.ShowScriptMessage("Please search for the criteria you want to export.");
                    return;
                }

                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "photographers" + DateTime.Now + ".xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                //Change the Header Row back to white color
                gvPhotographersExcel.HeaderRow.Style.Add("background-color", "#FFFFFF");

                for (int i = 0; i < gvPhotographersExcel.Rows.Count; i++)
                {
                    GridViewRow row = gvPhotographersExcel.Rows[i];

                    //Change Color back to white
                    row.BackColor = System.Drawing.Color.White;

                    //Apply text style to each Row
                    row.Attributes.Add("class", "textmode");
                }
                gvPhotographersExcel.RenderControl(htmltextwrtter);

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