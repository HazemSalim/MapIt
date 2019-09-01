using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web.Admin
{
    public partial class FAQs : System.Web.UI.Page
    {
        #region Variables

        FAQsRepository faqsRepository;

        #endregion Variables

        #region Properties

        public int? RecordId
        {
            get
            {
                int id = 0;

                if (ViewState["RecordId"] != null && int.TryParse(ViewState["RecordId"].ToString(), out id))
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
                faqsRepository = new FAQsRepository();
                var faqsList = faqsRepository.GetAll();
                if (faqsList != null && faqsList.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        faqsList = SortHelper.SortList<FAQ>(faqsList, SortExpression, SortDirection);
                    }

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    pds.DataSource = faqsList.ToList();
                    gvFAQs.DataSource = pds;
                    gvFAQs.DataBind();
                    AspNetPager1.RecordCount = faqsList.Count();
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvFAQs.DataSource = new List<FAQ>();
                    gvFAQs.DataBind();
                    AspNetPager1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        public void ClearControls()
        {
            txtTitleEN.Text = txtTitleAR.Text = txtContentEN.Text = txtContentAR.Text = txtOrdering.Text = string.Empty;
            chkActive.Checked = true;
            gvFAQs.SelectedIndex = -1;
            RecordId = null;
        }

        void Add()
        {
            try
            {
                faqsRepository = new FAQsRepository();
                var faqObj = new FAQ();
                faqObj.TitleEN = txtTitleEN.Text;
                faqObj.TitleAR = txtTitleAR.Text;

                faqObj.ContentEN = txtContentEN.Text;
                faqObj.ContentAR = txtContentAR.Text;

                faqObj.Ordering = ParseHelper.GetInt(txtOrdering.Text).Value;
                faqObj.IsActive = chkActive.Checked;
                faqObj.AddedOn = DateTime.Now;
                faqObj.AdminUserId = ParseHelper.GetInt(Session["AdminUserId"].ToString());

                faqsRepository.Add(faqObj);

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

        void Update(int id)
        {
            try
            {
                faqsRepository = new FAQsRepository();
                var faqObj = faqsRepository.GetByKey(id);

                faqObj.TitleEN = txtTitleEN.Text;
                faqObj.TitleAR = txtTitleAR.Text;

                faqObj.ContentEN = txtContentEN.Text;
                faqObj.ContentAR = txtContentAR.Text;

                faqObj.Ordering = ParseHelper.GetInt(txtOrdering.Text).Value;
                faqObj.IsActive = chkActive.Checked;
                faqObj.ModifiedOn = DateTime.Now;
                faqObj.AdminUserId = ParseHelper.GetInt(Session["AdminUserId"].ToString());

                faqsRepository.Update(faqObj);

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

        void Delete(int id)
        {
            try
            {
                faqsRepository = new FAQsRepository();
                faqsRepository.Delete(id);
                ClearControls();
                LoadData();
                PresentHelper.ShowScriptMessage("Delete successfully");
            }
            catch (Exception ex)
            {
                PresentHelper.ShowScriptMessage("Error in deleting, as there are many objects related to this record.");
                LogHelper.LogException(ex);
            }
        }

        bool SetData(int id)
        {
            try
            {
                faqsRepository = new FAQsRepository();
                var faqObj = faqsRepository.GetByKey(id);
                if (faqObj != null)
                {
                    txtTitleEN.Text = faqObj.TitleEN;
                    txtTitleAR.Text = faqObj.TitleAR;

                    txtContentEN.Text = faqObj.ContentEN;
                    txtContentAR.Text = faqObj.ContentAR;

                    txtOrdering.Text = faqObj.Ordering.ToString();
                    chkActive.Checked = faqObj.IsActive;

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

        #endregion Methods

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AdminUserId"] != null && (int)ParseHelper.GetInt(Session["AdminUserId"].ToString()) > 1 &&
                    !(new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminUserId"].ToString()), (int)AppEnums.AdminPages.ContentPages)))
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
            ClearControls();
            pnlAllRecords.Visible = false;
            pnlRecordDetails.Visible = true;
        }

        protected void gvFAQs_RowCommand(object sender, GridViewCommandEventArgs e)
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

        protected void gvFAQs_Sorting(object sender, GridViewSortEventArgs e)
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
            LoadData();
            pnlAllRecords.Visible = true;
            pnlRecordDetails.Visible = false;
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Page.Validate(btnSave.ValidationGroup);

            base.OnPreRenderComplete(e);
        }

        #endregion Events
    }
}