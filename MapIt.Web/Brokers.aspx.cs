using System;
using System.Collections.Generic;
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
    public partial class Brokers : MapIt.Lib.BasePage
    {
        #region Variables

        CitiesRepository citiesRepository;
        BrokersRepository brokersRepository;

        #endregion

        #region Properties

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

        #endregion

        #region Methods

        void BindCities()
        {
            citiesRepository = new CitiesRepository();
            var data = citiesRepository.Find(c => c.CountryId == this.CountryId);
            if (data != null)
            {
                rCities.DataSource = data;
                rCities.DataBind();
            }
            else
            {
                rCities.DataSource = new List<City>();
                rCities.DataBind();
            }
        }

        void LoadData()
        {
            try
            {
                brokersRepository = new BrokersRepository();
                var list = brokersRepository.Find(b => b.IsActive && (SearchCity.HasValue && SearchCity.Value > 0 ? b.CityId == SearchCity.Value : true)).OrderByDescending(s => s.AddedOn).Take(TopVal);

                if (list != null && list.Count() > 0)
                {
                    rBrokers.DataSource = list;
                    rBrokers.DataBind();
                }
                else
                {
                    rBrokers.DataSource = new List<object>();
                    rBrokers.DataBind();
                }

                btnLoadMore.Visible = rBrokers.Items.Count < TopVal ? false : true;
                list = null;
                brokersRepository = null;
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
                BindCities();
            }

            TopVal += GeneralSetting.PageSize;
        }

        protected void rCities_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "City")
            {
                int? cityId = ParseHelper.GetInt(e.CommandArgument);
                if (cityId.HasValue)
                {
                    citiesRepository = new CitiesRepository();
                    var cityObj = citiesRepository.GetByKey(cityId);
                    if (cityObj != null)
                    {
                        SearchCity = cityObj.Id;
                        litCity.Text = Culture.ToLower() == "ar-kw" ? cityObj.TitleAR : cityObj.TitleEN;
                        LoadData();
                    }
                }
                else
                {
                    SearchCity = null;
                    litCity.Text = Resources.Resource.all_cities;
                    LoadData();
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            LoadData();
            base.OnPreRender(e);
        }

        protected void btnLoadMore_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        #endregion
    }
}