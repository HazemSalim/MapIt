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
    public partial class ContentPages : System.Web.UI.Page
    {
        #region Variables

        ContentPagesRepository pagesRepository;

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

        public string OldPhoto
        {
            get
            {
                if (ViewState["OldPhoto"] != null)
                    return ViewState["OldPhoto"].ToString();

                return null;
            }
            set
            {
                ViewState["OldPhoto"] = value;
            }
        }

        #endregion

        #region Methods

        void LoadData()
        {
            try
            {
                pagesRepository = new ContentPagesRepository();
                var pagesList = pagesRepository.GetAll();
                if (pagesList != null && pagesList.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        pagesList = SortHelper.SortList<ContentPage>(pagesList, SortExpression, SortDirection);
                    }

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    pds.DataSource = pagesList.ToList();
                    gvPages.DataSource = pds;
                    gvPages.DataBind();
                    AspNetPager1.RecordCount = pagesList.Count();
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvPages.DataSource = new List<ContentPage>();
                    gvPages.DataBind();
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
            txtTitleEN.Text = txtTitleAR.Text = txtLink.Text = txtContentEN.Text = txtContentAR.Text = txtOrdering.Text = string.Empty;
            chkHasLink.Checked = chkActive.Checked = true;
            gvPages.SelectedIndex = -1;
            RecordId = null;

            // load shows
            pagesRepository = new ContentPagesRepository();
            var shows = from s in pagesRepository.Entities.PagePlaces.ToList()
                        select new { PagePlace = s.Title, PagePlaceId = s.Id, Show = false };

            rShows.DataSource = shows;
            rShows.DataBind();
        }

        List<PageShow> GetShowList()
        {
            try
            {
                var showList = new List<PageShow>();
                for (int i = 0; i < rShows.Items.Count; i++)
                {
                    HiddenField hfPagePlaceId = rShows.Items[i].FindControl("hfPagePlaceId") as HiddenField;
                    CheckBox chkShow = rShows.Items[i].FindControl("chkShow") as CheckBox;
                    if (hfPagePlaceId != null && chkShow != null)
                    {
                        int? pId = ParseHelper.GetInt(hfPagePlaceId.Value);
                        bool? show = ParseHelper.GetBool(chkShow.Checked);

                        if (pId.HasValue && show.HasValue)
                        {
                            showList.Add(new PageShow { PagePlaceId = pId.Value, Show = show.Value });
                        }
                    }
                }

                return showList;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return new List<PageShow>();
            }
        }

        void Add()
        {
            try
            {
                pagesRepository = new ContentPagesRepository();
                var pageObj = new ContentPage();
                pageObj.TitleEN = txtTitleEN.Text;
                pageObj.TitleAR = txtTitleAR.Text;

                pageObj.HasLink = chkHasLink.Checked;
                pageObj.Link = txtLink.Text;

                pageObj.ContentEN = txtContentEN.Text;
                pageObj.ContentAR = txtContentAR.Text;

                pageObj.Ordering = ParseHelper.GetInt(txtOrdering.Text).Value;
                pageObj.IsActive = chkActive.Checked;
                pageObj.AddedOn = DateTime.Now;
                pageObj.AdminUserId = ParseHelper.GetInt(Session["AdminUserId"].ToString());

                //------------- save shows ------------------//
                var showsList = GetShowList();
                showsList.ForEach(pageObj.PageShows.Add);
                //------------------------------------------------//

                pagesRepository.Add(pageObj);

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
                pagesRepository = new ContentPagesRepository();
                var pageObj = pagesRepository.GetByKey(id);

                pageObj.TitleEN = txtTitleEN.Text;
                pageObj.TitleAR = txtTitleAR.Text;

                pageObj.HasLink = chkHasLink.Checked;
                pageObj.Link = txtLink.Text;

                pageObj.ContentEN = txtContentEN.Text;
                pageObj.ContentAR = txtContentAR.Text;

                pageObj.Ordering = ParseHelper.GetInt(txtOrdering.Text).Value;
                pageObj.IsActive = chkActive.Checked;
                pageObj.ModifiedOn = DateTime.Now;
                pageObj.AdminUserId = ParseHelper.GetInt(Session["AdminUserId"].ToString());

                //------------- delete shows ------------------//
                pagesRepository.DeletePageShows(pageObj);
                //------------------------------------------------//

                //------------- save shows ------------------//
                var showsList = GetShowList();
                showsList.ForEach(pageObj.PageShows.Add);
                //------------------------------------------------//

                pagesRepository.Update(pageObj);

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
                pagesRepository = new ContentPagesRepository();
                pagesRepository.Delete(id);
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
                pagesRepository = new ContentPagesRepository();
                var pageObj = pagesRepository.GetByKey(id);
                if (pageObj != null)
                {
                    txtTitleEN.Text = pageObj.TitleEN;
                    txtTitleAR.Text = pageObj.TitleAR;

                    chkHasLink.Checked = pageObj.HasLink;
                    txtLink.Text = pageObj.Link;

                    txtContentEN.Text = pageObj.ContentEN;
                    txtContentAR.Text = pageObj.ContentAR;

                    txtOrdering.Text = pageObj.Ordering.ToString();
                    chkActive.Checked = pageObj.IsActive;

                    // load shows
                    var shows = from s in pagesRepository.Entities.PagePlaces
                                join p in pagesRepository.Entities.PageShows.Where(ap => ap.PageId == id) on s equals p.PagePlace into op
                                from p in op.DefaultIfEmpty()
                                select new { PagePlace = s.Title, PagePlaceId = s.Id, Show = (!p.Equals(null) ? p.Show : false) };

                    rShows.DataSource = shows.ToList();
                    rShows.DataBind();

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

        protected void gvPages_RowCommand(object sender, GridViewCommandEventArgs e)
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

        protected void gvPages_Sorting(object sender, GridViewSortEventArgs e)
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