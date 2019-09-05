using System;
using System.Linq;
using System.Web.UI.WebControls;
using MapIt.Helpers;
using MapIt.Repository;

namespace MapIt.Web
{
    public partial class Default : MapIt.Lib.BasePage
    {
        #region Variables

        //GeneralSettingsRepository generalSettingsRepository;
        CitiesRepository citiesRepository;
        AreasRepository areasRepository;
        BlocksRepository blocksRepository;
        PropertyTypesRepository propertyTypesRepository;
        //PropertiesRepository propertiesRepository;
        ServicesCategoriesRepository servicesCategoriesRepository;

        #endregion Variables

        #region Properties

        public int? SearchPurpose
        {
            get
            {
                int t = 0;
                if (ViewState["SearchPurpose"] != null && int.TryParse(ViewState["SearchPurpose"].ToString(), out t))
                    return t;
                return null;
            }
            set
            {
                ViewState["SearchPurpose"] = value;
            }
        }

        public int? SearchType
        {
            get
            {
                int t = 0;
                if (ViewState["SearchType"] != null && int.TryParse(ViewState["SearchType"].ToString(), out t))
                    return t;
                return null;
            }
            set
            {
                ViewState["SearchType"] = value;
            }
        }

        public int? SearchCity
        {
            get
            {
                int t = 0;
                if (ViewState["SearchCity"] != null && int.TryParse(ViewState["SearchCity"].ToString(), out t))
                    return t;
                return null;
            }
            set
            {
                ViewState["SearchCity"] = value;
            }
        }

        public int? SearchArea
        {
            get
            {
                int t = 0;
                if (ViewState["SearchArea"] != null && int.TryParse(ViewState["SearchArea"].ToString(), out t))
                    return t;
                return null;
            }
            set
            {
                ViewState["SearchArea"] = value;
            }
        }

        public int? SearchBlock
        {
            get
            {
                int t = 0;
                if (ViewState["SearchBlock"] != null && int.TryParse(ViewState["SearchBlock"].ToString(), out t))
                    return t;
                return null;
            }
            set
            {
                ViewState["SearchBlock"] = value;
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

        #endregion

        #region Methods

        void BindSettings()
        {
            try
            {
                aAppStore.HRef = GeneralSetting.AppStore;
                aGooglePlay.HRef = GeneralSetting.GooglePlay;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void BindTypes()
        {
            try
            {
                ddlSearchType.DataValueField = "Id";
                ddlSearchType.DataTextField = Resources.Resource.db_title_col;

                propertyTypesRepository = new PropertyTypesRepository();
                var data = propertyTypesRepository.GetAll().ToList();
                if (data != null)
                {
                    ddlSearchType.DataSource = data.OrderBy(c => c.Ordering).ToList();
                    ddlSearchType.DataBind();
                }

                data = null;
                propertyTypesRepository = null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void BindCities()
        {
            try
            {
                ddlSearchCity.DataValueField = "Id";
                ddlSearchCity.DataTextField = Resources.Resource.db_title_col;

                citiesRepository = new CitiesRepository();
                var data = citiesRepository.Find(c => c.CountryId == this.CountryId).ToList();
                if (data != null)
                {
                    ddlSearchCity.DataSource = data.OrderBy(c => c.TitleEN).ToList();
                    ddlSearchCity.DataBind();
                }

                data = null;
                citiesRepository = null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void BindAreas()
        {
            try
            {
                ddlSearchArea.DataValueField = "Id";
                ddlSearchArea.DataTextField = Resources.Resource.db_title_col;

                for (int i = ddlSearchArea.Items.Count - 1; i > 0; i--)
                {
                    ddlSearchArea.Items.RemoveAt(i);
                }

                int? cityId = ParseHelper.GetInt(ddlSearchCity.SelectedValue);
                if (cityId.HasValue)
                {
                    areasRepository = new AreasRepository();
                    var data = areasRepository.Find(c => c.CityId == cityId.Value).ToList();
                    if (data != null && data.Count > 0)
                    {
                        ddlSearchArea.DataSource = data;
                        ddlSearchArea.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void BindBlocks()
        {
            try
            {
                ddlSearchBlock.DataValueField = "Id";
                ddlSearchBlock.DataTextField = Resources.Resource.db_title_col;

                for (int i = ddlSearchBlock.Items.Count - 1; i > 0; i--)
                {
                    ddlSearchBlock.Items.RemoveAt(i);
                }

                int? areaId = ParseHelper.GetInt(ddlSearchArea.SelectedValue);
                if (areaId.HasValue)
                {
                    blocksRepository = new BlocksRepository();
                    var data = blocksRepository.Find(c => c.AreaId == areaId.Value).ToList();
                    if (data != null && data.Count > 0)
                    {
                        ddlSearchBlock.DataSource = data;
                        ddlSearchBlock.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void BindPropertiesTypes()
        {
            try
            {
                propertyTypesRepository = new PropertyTypesRepository();
                var data = propertyTypesRepository.GetAll().ToList();
                if (data != null)
                {
                    rTypes.DataSource = data.OrderBy(c => c.Ordering).Take(5).ToList();
                    rTypes.DataBind();
                }

                data = null;
                propertyTypesRepository = null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void BindServicesCats()
        {
            try
            {
                servicesCategoriesRepository = new ServicesCategoriesRepository();
                var data = servicesCategoriesRepository.MainServicesCategories.ToList();
                if (data != null)
                {
                    rCats.DataSource = data.OrderBy(c => c.Ordering).Take(5).ToList();
                    rCats.DataBind();
                }

                data = null;
                servicesCategoriesRepository = null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void SearchProperties()
        {
            try
            {
                string url = "/Proprties.aspx?op=1";

                if (SearchPurpose.HasValue)
                {
                    url += "&purpose=" + SearchPurpose.Value;
                }
                if (SearchType.HasValue)
                {
                    url += "&type=" + SearchType.Value;
                }
                if (SearchCity.HasValue)
                {
                    url += "&city=" + SearchCity.Value;
                }
                if (SearchArea.HasValue)
                {
                    url += "&area=" + SearchArea.Value;
                }
                if (SearchBlock.HasValue)
                {
                    url += "&block=" + SearchBlock.Value;
                }
                if (!string.IsNullOrEmpty(SearchKeyWord))
                {
                    url += "&keyword=" + SearchKeyWord;
                }

                Response.Redirect(url, false);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSettings();
                BindTypes();
                BindCities();
                BindPropertiesTypes();
                BindServicesCats();
            }
        }

        protected void ddlSearchCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAreas();
        }

        protected void ddlSearchArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBlocks();
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            SearchPurpose = ParseHelper.GetInt(hfPurpose.Value);
            SearchType = ParseHelper.GetInt(ddlSearchType.SelectedValue);
            SearchCity = ParseHelper.GetInt(ddlSearchCity.SelectedValue);
            SearchArea = ParseHelper.GetInt(ddlSearchArea.SelectedValue);
            SearchBlock = ParseHelper.GetInt(ddlSearchBlock.SelectedValue);
            SearchKeyWord = txtSearch.Text;

            SearchProperties();
        }

        #endregion
    }
}