using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web
{
    public partial class Proprties : MapIt.Lib.BasePage
    {
        #region Variables

        PropertyTypesRepository propertyTypesRepository;
        PropertiesRepository propertiesRepository;
        PurposesRepository purposeRepository;

        #endregion

        #region Properties

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

        public int TopVal
        {
            get
            {
                int val = 0;
                if (ViewState["TopVal"] != null && int.TryParse(ViewState["TopVal"].ToString(), out val))
                    return val;

                return 0;
            }
            set
            {
                ViewState["TopVal"] = value;
            }
        }
        public double? SPriceFrom
        {
            get
            {
                double t = 0;
                if (ViewState["SPriceFrom"] != null && double.TryParse(ViewState["SPriceFrom"].ToString(), out t))
                    return t;
                return null;
            }
            set
            {
                ViewState["SPriceFrom"] = value;
            }
        }
        public double? SPriceTo
        {
            get
            {
                double t = 0;
                if (ViewState["SPriceTo"] != null && double.TryParse(ViewState["SPriceTo"].ToString(), out t))
                    return t;
                return null;
            }
            set
            {
                ViewState["SPriceTo"] = value;
            }
        }
        public double? RPriceFrom
        {
            get
            {
                double t = 0;
                if (ViewState["RPriceFrom"] != null && double.TryParse(ViewState["RPriceFrom"].ToString(), out t))
                    return t;
                return null;
            }
            set
            {
                ViewState["RPriceFrom"] = value;
            }
        }
        public double? RPriceTo
        {
            get
            {
                double t = 0;
                if (ViewState["RPriceTo"] != null && double.TryParse(ViewState["RPriceTo"].ToString(), out t))
                    return t;
                return null;
            }
            set
            {
                ViewState["RPriceTo"] = value;
            }
        }

        public int? LoadCount
        {
            get
            {
                int t = 0;
                if (ViewState["LoadCount"] != null && int.TryParse(ViewState["LoadCount"].ToString(), out t))
                    return t;
                return null;
            }
            set
            {
                ViewState["LoadCount"] = value;
            }
        }
        #endregion

        #region Methods

        void BindPurpose()
        {
            purposeRepository = new PurposesRepository();
            var data = purposeRepository.GetAll();
            if (data != null)
            {
                ddlPurpose.DataValueField = "Id";
                ddlPurpose.DataTextField = Culture.ToLower() == "ar-kw" ? "TitleAR" : "TitleEN";
                ddlPurpose.DataSource = data;
                ddlPurpose.DataBind();

                rPurpose.DataSource = data;
                rPurpose.DataBind();
            }
            else
            {
                ddlPurpose.DataSource = new List<Purpos>();
                ddlPurpose.DataBind();

                rPurpose.DataSource = new List<Purpos>();
                rPurpose.DataBind();
            }
        }

        void BindPropertyType()
        {
            propertyTypesRepository = new PropertyTypesRepository();
            var data = propertyTypesRepository.GetAll();
            if (data != null)
            {
                rTypes.DataSource = data;
                rTypes.DataBind();
            }
            else
            {
                rTypes.DataSource = new List<Purpos>();
                rTypes.DataBind();
            }
        }

        void SetPrice()
        {
            double? min = ParseHelper.GetDouble(txtMin.Text);
            int? purposeId = ParseHelper.GetInt(ddlPurpose.SelectedValue);

            if (min.HasValue)
            {
                SPriceFrom = purposeId.HasValue && purposeId.Value == 1 ? min.Value : (double?)null;
                RPriceFrom = purposeId.HasValue && purposeId.Value == 2 ? min.Value : (double?)null;
            }
            double? max = ParseHelper.GetDouble(txtMax.Text);
            if (max.HasValue)
            {
                SPriceTo = purposeId.HasValue && purposeId.Value == 1 ? max.Value : (double?)null;
                RPriceTo = purposeId.HasValue && purposeId.Value == 2 ? max.Value : (double?)null;
            }
        }

        void LoadData()
        {
            try
            {
                LoadCount = LoadCount.HasValue && LoadCount.Value > 0 ? LoadCount.Value + 1 : 1;
                AspNetPager1.PageSize = 8;// GeneralSetting.PageSize;
                hdLocation.Value = string.Empty;
                string cordinates = string.Empty;
                propertiesRepository = new PropertiesRepository();

                IQueryable<MapIt.Data.Property> list;

                if (Search)
                {
                    list = propertiesRepository.Search(null, SearchUser, SearchPurpose, SearchType, CountryId, SearchCity, SearchArea, SearchBlock, null, null, null, null,
                        null, null, null, null, null, SPriceFrom, SPriceTo, RPriceFrom, RPriceTo, null, null, null, 1, 1, 0, null, 1, SearchKeyWord)
                        .OrderByDescending(o => o.AddedOn).Take(TopVal);
                }
                else
                {
                    list = propertiesRepository.GetAvProperties().OrderByDescending(o => o.AddedOn).Take(TopVal);
                }

                if (list != null && list.Count() > 0)
                {
                    list = list.OrderByDescending(p => p.Id);
                    int count = list.Count();
                    list = list.Skip((AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize).Take(AspNetPager1.PageSize);
                    rPros.DataSource = list;
                    rPros.DataBind();
                    AspNetPager1.RecordCount = count;
                    AspNetPager1.Visible = true;
                    // Load Property Components
                    foreach (RepeaterItem item in rPros.Items)
                    {
                        if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
                        {
                            HiddenField hfProId = (HiddenField)item.FindControl("hfProId");
                            long? proId = ParseHelper.GetInt64(hfProId.Value);
                            Repeater rComponents = (Repeater)item.FindControl("rComponents");
                            HiddenField hdCoordinates = (HiddenField)item.FindControl("hdCoordinates");

                            if (proId.HasValue && proId.Value > 0)
                            {
                                cordinates = cordinates + "|" + hdCoordinates.Value;
                                propertiesRepository = new PropertiesRepository();
                                var proObj = propertiesRepository.GetByKey(proId);
                                if (proObj != null)
                                {
                                    var compList = proObj.PropertyComponents.Take(3);

                                    if (compList != null && compList.Count() > 0)
                                    {
                                        rComponents.DataSource = compList.OrderBy(c => c.Component.Ordering);
                                        rComponents.DataBind();
                                    }
                                }
                            }
                        }
                    }
                    hdLocation.Value = cordinates;
                    if (LoadCount > 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showMap();", true);
                    }
                }
                else
                {
                    rPros.DataSource = new List<object>();
                    rPros.DataBind();
                    AspNetPager1.Visible = false;
                }

                //btnLoadMore.Visible = rPros.Items.Count < TopVal ? false : true;
                list = null;
                propertiesRepository = null;

            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void ClearSearch()
        {
            txtSearchKeyword.Text = txtMax.Text = txtMin.Text = SearchKeyWord = string.Empty;
            SearchPurpose = SearchType = null;
            SPriceFrom = RPriceFrom = SPriceTo = RPriceTo = null;
            AspNetPager1.CurrentPageIndex = 0;
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPurpose();
                BindPropertyType();
                if (Request.QueryString["op"] != null && !string.IsNullOrEmpty(Request.QueryString["op"].Trim()))
                {
                    Search = ParseHelper.GetInt(Request.QueryString["op"].Trim()).Value == 1 ? true : false;
                }

                if (Request.QueryString["user"] != null && !string.IsNullOrEmpty(Request.QueryString["user"].Trim()))
                {
                    SearchUser = ParseHelper.GetInt64(Request.QueryString["user"].Trim());
                }

                if (Request.QueryString["purpose"] != null && !string.IsNullOrEmpty(Request.QueryString["purpose"].Trim()))
                {
                    SearchPurpose = ParseHelper.GetInt(Request.QueryString["purpose"].Trim());
                }

                if (Request.QueryString["type"] != null && !string.IsNullOrEmpty(Request.QueryString["type"].Trim()))
                {
                    SearchType = ParseHelper.GetInt(Request.QueryString["type"].Trim());
                }

                if (Request.QueryString["city"] != null && !string.IsNullOrEmpty(Request.QueryString["city"].Trim()))
                {
                    SearchCity = ParseHelper.GetInt(Request.QueryString["city"].Trim());
                }

                if (Request.QueryString["area"] != null && !string.IsNullOrEmpty(Request.QueryString["area"].Trim()))
                {
                    SearchArea = ParseHelper.GetInt(Request.QueryString["area"].Trim());
                }

                if (Request.QueryString["block"] != null && !string.IsNullOrEmpty(Request.QueryString["block"].Trim()))
                {
                    SearchBlock = ParseHelper.GetInt(Request.QueryString["block"].Trim());
                }

                if (Request.QueryString["keyword"] != null && !string.IsNullOrEmpty(Request.QueryString["keyword"].Trim()))
                {
                    SearchKeyWord = Request.QueryString["keyword"].Trim();
                }

                if (Search)
                {
                    Title = (Culture.ToLower() == "ar-kw" ? GeneralSetting.TitleAR : GeneralSetting.TitleEN) + " | " + Resources.Resource.search_results;
                    litTitle.Text = Resources.Resource.search_results;
                }
                else
                {
                    Title = (Culture.ToLower() == "ar-kw" ? GeneralSetting.TitleAR : GeneralSetting.TitleEN) + " | " + Resources.Resource.latest_added;
                    litTitle.Text = Resources.Resource.latest_added;
                }
            }

            TopVal += GeneralSetting.PageSize;
        }

        protected void rPros_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Fav")
            {
                try
                {
                    if (UserId > 0)
                    {
                        propertiesRepository = new PropertiesRepository();
                        propertiesRepository.SetFavorite(ParseHelper.GetInt64(e.CommandArgument.ToString()).Value, UserId);
                    }
                    else
                    {
                        PresentHelper.ShowScriptMessage(Resources.Resource.login_first, "/Login");
                    }
                }
                catch (Exception ex)
                {
                    PresentHelper.ShowScriptMessage(Resources.Resource.error);
                    LogHelper.LogException(ex);
                }
            }
        }

        protected void rPurpose_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Purpose")
            {
                ClearSearch();
                int? purposeId = ParseHelper.GetInt(e.CommandArgument);
                if (purposeId.HasValue)
                {
                    purposeRepository = new PurposesRepository();
                    var purObj = purposeRepository.GetByKey(purposeId);
                    if (purObj != null)
                    {
                        SearchPurpose = purObj.Id;
                        litPurpose.Text = Culture.ToLower() == "ar-kw" ? purObj.TitleAR : purObj.TitleEN;
                        LoadData();
                    }
                }
                else
                {
                    SearchCity = null;
                    litPurpose.Text = Resources.Resource.all_purposes;
                    LoadData();
                }
            }
        }

        protected void rTypes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Types")
            {
                ClearSearch();
                int? typeId = ParseHelper.GetInt(e.CommandArgument);
                if (typeId.HasValue)
                {
                    propertyTypesRepository = new PropertyTypesRepository();
                    var typObj = propertyTypesRepository.GetByKey(typeId);
                    if (typObj != null)
                    {
                        SearchType = typObj.Id;
                        litType.Text = Culture.ToLower() == "ar-kw" ? typObj.TitleAR : typObj.TitleEN;
                        LoadData();
                    }
                }
                else
                {
                    SearchType = null;
                    litType.Text = Resources.Resource.all_types;
                    LoadData();
                }
            }
        }

        protected void btnSearchByPrice_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 0;
            txtSearchKeyword.Text = SearchKeyWord = string.Empty;
            SearchPurpose = SearchType = null;
            SPriceFrom = RPriceFrom = SPriceTo = RPriceTo = null;
            SetPrice();
            LoadData();
        }

        protected void btnSearchKeyword_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 0;
            txtMax.Text = txtMin.Text = string.Empty;
            SearchPurpose = SearchType = null;
            SPriceFrom = RPriceFrom = SPriceTo = RPriceTo = null;
            SearchKeyWord = txtSearchKeyword.Text;
            LoadData();
        }

        protected override void OnPreRender(EventArgs e)
        {
            LoadData();
            //ClearSearch();
            base.OnPreRender(e);
        }

        protected void btnLoadMore_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        #endregion
    }
}
