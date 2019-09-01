using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web
{
    public partial class ServCat : MapIt.Lib.BasePage
    {
        #region Variables

        ServicesCategoriesRepository servicesCategoriesRepository;

        #endregion

        #region Properties

        public int? CategoryId
        {
            get
            {
                int id = 0;
                if (ViewState["CategoryId"] != null && int.TryParse(ViewState["CategoryId"].ToString(), out id))
                    return id;

                return null;
            }
            set
            {
                ViewState["CategoryId"] = value;
            }
        }

        #endregion

        #region Methods

        void LoadData()
        {
            try
            {
                servicesCategoriesRepository = new ServicesCategoriesRepository();
                var list = servicesCategoriesRepository.Find(c => c.IsActive && c.ParentId == CategoryId).OrderBy(p => p.Ordering);

                if (list != null && list.Count() > 0)
                {
                    rServCats.DataSource = list;
                    rServCats.DataBind();
                }
                else
                {
                    rServCats.DataSource = new List<object>();
                    rServCats.DataBind();
                }

                list = null;
                servicesCategoriesRepository = null;
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
                if (this.Page.RouteData.Values["PageName"] != null)
                {
                    string pageName = this.Page.RouteData.Values["PageName"].ToString();
                    int? id = ParseHelper.GetInt(Regex.Match(pageName, @"(?<=(\D|^))\d+(?=\D*$)"));

                    if (id.HasValue)
                    {
                        CategoryId = id.Value;

                        servicesCategoriesRepository = new ServicesCategoriesRepository();
                        var catObj = servicesCategoriesRepository.GetByKey(CategoryId);
                        if (catObj != null)
                        {
                            Title = Culture.ToLower() == "ar-kw" ? GeneralSetting.TitleAR + " | " + catObj.TitleAR : GeneralSetting.TitleEN + " | " + catObj.TitleEN;
                            litTitle.Text = Culture.ToLower() == "ar-kw" ? catObj.TitleAR : catObj.TitleEN;
                        }
                        else
                        {
                            Title = (Culture.ToLower() == "ar-kw" ? GeneralSetting.TitleAR : GeneralSetting.TitleEN) + " | " + catObj.TitleEN;
                            litTitle.Text = Resources.Resource.cons_services;
                        }
                    }
                }
                else
                {
                    Title = (Culture.ToLower() == "ar-kw" ? GeneralSetting.TitleAR : GeneralSetting.TitleEN) + " | " + Resources.Resource.cons_services;
                    litTitle.Text = Resources.Resource.cons_services;
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            LoadData();
            base.OnPreRender(e);
        }

        #endregion
    }
}