using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web.Admin
{
    public partial class Blocks : System.Web.UI.Page
    {
        #region Variables

        BlocksRepository blocksRepository;

        #endregion Variables

        #region Properties

        protected int AreaId
        {
            get
            {
                int id = 0;
                if (ViewState["AreaId"] != null && int.TryParse(ViewState["AreaId"].ToString(), out id))
                    return id;
                return 0;
            }
            set
            {
                ViewState["AreaId"] = value;
            }
        }

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
                blocksRepository = new BlocksRepository();
                var list = blocksRepository.Find(a => a.AreaId == AreaId);
                if (list != null && list.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        list = SortHelper.SortList<Block>(list, SortExpression, SortDirection);
                    }

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    pds.DataSource = list.ToList();
                    gvBlocks.DataSource = pds;
                    gvBlocks.DataBind();
                    AspNetPager1.RecordCount = list.Count();
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvBlocks.DataSource = new List<Block>();
                    gvBlocks.DataBind();
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
            txtTitleEN.Text = txtTitleAR.Text = hdnCoordinates.Value = hdnTempCoordinates.Value = string.Empty;
            btnCancel.Visible = false;
            btnSave.Text = "Add new Block";
            gvBlocks.SelectedIndex = -1;
            RecordId = null;
        }

        void Add()
        {
            try
            {
                var blockObj = new Block();
                blockObj.TitleEN = txtTitleEN.Text;
                blockObj.TitleAR = txtTitleAR.Text;
                blockObj.AreaId = AreaId;
                blockObj.Coordinates = hdnCoordinates.Value;

                blocksRepository = new BlocksRepository();
                blocksRepository.Add(blockObj);
                ClearControls();
                LoadData();
                PresentHelper.ShowScriptMessage("Add successfully");
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
                blocksRepository = new BlocksRepository();
                var blockObj = blocksRepository.GetByKey(id);
                blockObj.TitleEN = txtTitleEN.Text;
                blockObj.TitleAR = txtTitleAR.Text;
                blockObj.AreaId = AreaId;
                blockObj.Coordinates = hdnCoordinates.Value;

                blocksRepository = new BlocksRepository();
                blocksRepository.Update(blockObj);
                ClearControls();
                LoadData();
                PresentHelper.ShowScriptMessage("Update successfully");
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
                blocksRepository = new BlocksRepository();
                blocksRepository.Delete(id);
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
                blocksRepository = new BlocksRepository();
                Block blockObj = blocksRepository.GetByKey(id);
                if (blockObj != null)
                {
                    txtTitleEN.Text = blockObj.TitleEN;
                    txtTitleAR.Text = blockObj.TitleAR;

                    hdnTempCoordinates.Value = string.Empty;

                    hdnCoordinates.Value = blockObj.Coordinates;
                    hdnTempCoordinates.Value = blockObj.Coordinates;

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
                    !(new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminUserId"].ToString()), (int)AppEnums.AdminPages.Countries)))
                {
                    Response.Redirect(".");
                }

                int id = 0;
                if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out id))
                {
                    AreaId = id;

                    var repository = new AreasRepository();
                    var areaObj = repository.GetByKey(AreaId);
                    litTitle.Text = areaObj.TitleEN;

                    LoadData();
                }
            }
        }

        protected void gvBlocks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditItem")
            {
                int id = 0;
                int index = 0;
                string[] args = e.CommandArgument.ToString().Split(',');
                if (args.Length == 2 && int.TryParse(args[0], out id) && int.TryParse(args[1], out index))
                {
                    if (SetData(id))
                    {
                        gvBlocks.SelectedIndex = index;
                        RecordId = id;
                        btnSave.Text = "Update Block";
                        btnCancel.Visible = true;
                    }
                }
            }
            else if (e.CommandName == "DeleteItem")
            {
                Delete(int.Parse(e.CommandArgument.ToString()));
            }
        }

        protected void gvBlocks_Sorting(object sender, GridViewSortEventArgs e)
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
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Page.Validate(btnSave.ValidationGroup);

            base.OnPreRenderComplete(e);
        }

        #endregion Events
    }
}