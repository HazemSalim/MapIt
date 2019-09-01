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
    public partial class Cities : System.Web.UI.Page
    {
        #region Variables

        CitiesRepository citiesRepository;

        #endregion Variables

        #region Properties

        int CountryId
        {
            get
            {
                int id = 0;
                if (ViewState["CountryId"] != null && int.TryParse(ViewState["CountryId"].ToString(), out id))
                    return id;
                return 0;
            }
            set
            {
                ViewState["CountryId"] = value;
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
                citiesRepository = new CitiesRepository();
                var list = citiesRepository.Find(c => c.CountryId == CountryId);
                if (list != null && list.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        list = SortHelper.SortList<City>(list, SortExpression, SortDirection);
                    }

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    pds.DataSource = list.ToList();
                    gvCities.DataSource = pds;
                    gvCities.DataBind();
                    AspNetPager1.RecordCount = list.Count();
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvCities.DataSource = new List<City>();
                    gvCities.DataBind();
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
            btnSave.Text = "Add new City";
            gvCities.SelectedIndex = -1;
            RecordId = null;
        }

        void Add()
        {
            try
            {
                var cityObj = new City();
                cityObj.TitleEN = txtTitleEN.Text;
                cityObj.TitleAR = txtTitleAR.Text;
                cityObj.CountryId = CountryId;
                cityObj.Coordinates = hdnCoordinates.Value;

                citiesRepository = new CitiesRepository();
                citiesRepository.Add(cityObj);
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
                citiesRepository = new CitiesRepository();
                var cityObj = citiesRepository.GetByKey(id);
                cityObj.TitleEN = txtTitleEN.Text;
                cityObj.TitleAR = txtTitleAR.Text;
                cityObj.CountryId = CountryId;
                cityObj.Coordinates = hdnCoordinates.Value;

                citiesRepository = new CitiesRepository();
                citiesRepository.Update(cityObj);
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
                citiesRepository = new CitiesRepository();
                citiesRepository.Delete(id);
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
                citiesRepository = new CitiesRepository();
                City cityObj = citiesRepository.GetByKey(id);
                if (cityObj != null)
                {
                    txtTitleEN.Text = cityObj.TitleEN;
                    txtTitleAR.Text = cityObj.TitleAR;

                    hdnTempCoordinates.Value = string.Empty;

                    hdnCoordinates.Value = cityObj.Coordinates;
                    hdnTempCoordinates.Value = cityObj.Coordinates;

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
                    CountryId = id;

                    var repository = new CountriesRepository();
                    var ctryObj = repository.GetByKey(CountryId);
                    litTitle.Text = ctryObj.TitleEN;

                    LoadData();
                }
            }
        }

        protected void gvCities_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        gvCities.SelectedIndex = index;
                        RecordId = id;
                        btnSave.Text = "Update City";
                        btnCancel.Visible = true;
                    }
                }
            }
            else if (e.CommandName == "DeleteItem")
            {
                Delete(int.Parse(e.CommandArgument.ToString()));
            }
        }

        protected void gvCities_Sorting(object sender, GridViewSortEventArgs e)
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