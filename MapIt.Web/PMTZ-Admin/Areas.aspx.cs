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
    public partial class Areas : System.Web.UI.Page
    {
        #region Variables

        AreasRepository areasRepository;

        #endregion Variables

        #region Properties

        protected int CityId
        {
            get
            {
                int id = 0;
                if (ViewState["CityId"] != null && int.TryParse(ViewState["CityId"].ToString(), out id))
                    return id;
                return 0;
            }
            set
            {
                ViewState["CityId"] = value;
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
                areasRepository = new AreasRepository();
                var list = areasRepository.Find(a => a.CityId == CityId);
                if (list != null && list.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        list = SortHelper.SortList<Area>(list, SortExpression, SortDirection);
                    }

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    pds.DataSource = list.ToList();
                    gvAreas.DataSource = pds;
                    gvAreas.DataBind();
                    AspNetPager1.RecordCount = list.Count();
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvAreas.DataSource = new List<Area>();
                    gvAreas.DataBind();
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
            btnSave.Text = "Add new Area";
            gvAreas.SelectedIndex = -1;
            RecordId = null;
        }

        void Add()
        {
            try
            {
                var areaObj = new Area();
                areaObj.TitleEN = txtTitleEN.Text;
                areaObj.TitleAR = txtTitleAR.Text;
                areaObj.CityId = CityId;
                areaObj.Coordinates = hdnCoordinates.Value;

                areasRepository = new AreasRepository();
                areasRepository.Add(areaObj);
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
                areasRepository = new AreasRepository();
                var areaObj = areasRepository.GetByKey(id);
                areaObj.TitleEN = txtTitleEN.Text;
                areaObj.TitleAR = txtTitleAR.Text;
                areaObj.CityId = CityId;
                areaObj.Coordinates = hdnCoordinates.Value;

                areasRepository = new AreasRepository();
                areasRepository.Update(areaObj);
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
                areasRepository = new AreasRepository();
                areasRepository.Delete(id);
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
                areasRepository = new AreasRepository();
                Area areaObj = areasRepository.GetByKey(id);
                if (areaObj != null)
                {
                    txtTitleEN.Text = areaObj.TitleEN;
                    txtTitleAR.Text = areaObj.TitleAR;

                    hdnTempCoordinates.Value = string.Empty;

                    hdnCoordinates.Value = areaObj.Coordinates;
                    hdnTempCoordinates.Value = areaObj.Coordinates;

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
                    CityId = id;

                    var repository = new CitiesRepository();
                    var cityObj = repository.GetByKey(CityId);
                    litTitle.Text = cityObj.TitleEN;

                    LoadData();
                }
            }
        }

        protected void gvAreas_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        gvAreas.SelectedIndex = index;
                        RecordId = id;
                        btnSave.Text = "Update Area";
                        btnCancel.Visible = true;
                    }
                }
            }
            else if (e.CommandName == "DeleteItem")
            {
                Delete(int.Parse(e.CommandArgument.ToString()));
            }
        }

        protected void gvAreas_Sorting(object sender, GridViewSortEventArgs e)
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