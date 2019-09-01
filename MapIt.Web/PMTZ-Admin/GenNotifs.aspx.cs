using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Drawing;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;
using System.Threading;

namespace MapIt.Web.Admin
{
    public partial class GenNotifs : System.Web.UI.Page
    {
        #region Variables

        GenNotifsRepository genNotifsRepository;

        int pushGNotifId;
        string pushMessageEN;
        string pushMessageAR;

        #endregion Variables

        #region Properties

        public Int32? RecordId
        {
            get
            {
                Int32 id = 0;

                if (ViewState["RecordId"] != null && Int32.TryParse(ViewState["RecordId"].ToString(), out id))
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
                genNotifsRepository = new GenNotifsRepository();
                IQueryable<MapIt.Data.GenNotif> list;
                list = genNotifsRepository.GetAll().AsQueryable();

                if (list != null && list.Count() > 0)
                {
                    list = list.OrderByDescending(p => p.Id);

                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        list = SortHelper.SortList<MapIt.Data.GenNotif>(list, SortExpression, SortDirection);
                    }

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    pds.DataSource = list.ToList();
                    gvGenNotifs.DataSource = pds;
                    gvGenNotifs.DataBind();
                    AspNetPager1.RecordCount = list.Count();
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvGenNotifs.DataSource = new List<Offer>();
                    gvGenNotifs.DataBind();
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
            txtTitleEN.Text = txtTitleAR.Text = string.Empty;
            gvGenNotifs.SelectedIndex = -1;
            RecordId = null;
        }

        void DoWork()
        {
            AppPushs.Push((int)AppEnums.NotifTypes.General, null, pushGNotifId, null, null, null, pushMessageEN, pushMessageAR);
        }

        void Add()
        {
            try
            {
                genNotifsRepository = new GenNotifsRepository();
                var genNotifObj = new MapIt.Data.GenNotif();

                genNotifObj.TitleEN = txtTitleEN.Text;
                genNotifObj.TitleAR = txtTitleAR.Text;
                genNotifObj.AddedOn = DateTime.Now;

                genNotifsRepository.Add(genNotifObj);

                pushGNotifId = genNotifObj.Id;
                pushMessageEN = genNotifObj.TitleEN;
                pushMessageAR = genNotifObj.TitleAR;

                Thread th = new Thread(DoWork);
                th.Start();

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

        void Delete(Int32 id)
        {
            try
            {
                genNotifsRepository = new GenNotifsRepository();
                int result = genNotifsRepository.DeleteAnyWay(id);
                switch (result)
                {
                    case -3:
                        PresentHelper.ShowScriptMessage("Error in deleting, as there are many objects related to this record.");
                        break;
                    case 1:
                        ClearControls();
                        LoadData();
                        PresentHelper.ShowScriptMessage("Delete successfully");
                        break;
                }
            }
            catch (Exception ex)
            {
                PresentHelper.ShowScriptMessage("Error in deleting, as there are many objects related to this record.");
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
                    !(new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminUserId"].ToString()), (int)AppEnums.AdminPages.GenNotifs)))
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Add();
        }

        protected void gvGenNotifs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                Delete(Int32.Parse(e.CommandArgument.ToString()));
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
            pnlAllRecords.Visible = true;
            pnlRecordDetails.Visible = false;
        }

        protected void gvGenNotifs_Sorting(object sender, GridViewSortEventArgs e)
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

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Page.Validate(btnSave.ValidationGroup);

            base.OnPreRenderComplete(e);
        }

        #endregion Events
    }
}