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
    public partial class Services : MapIt.Lib.BasePage
    {
        #region Variables

        ServicesCategoriesRepository servicesCategoriesRepository;
        ServicesRepository servicesRepository;

        #endregion

        #region Properties

        public int CategoryId
        {
            get
            {
                int id = 0;
                if (ViewState["CategoryId"] != null && int.TryParse(ViewState["CategoryId"].ToString().Trim(), out id))
                    return id;

                return 0;
            }
            set
            {
                ViewState["CategoryId"] = value;
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

        void BindServices()
        {
            try
            {
                servicesRepository = new ServicesRepository();
                var list = servicesRepository.Find(s => s.IsActive && s.CategoryId == CategoryId).OrderBy(c => c.Ordering.HasValue ? 0 : 1).ThenBy(c => c.Ordering).ThenByDescending(c => c.AddedOn).Take(TopVal);

                if (list != null && list.Count() > 0)
                {
                    rServices.DataSource = list;
                    rServices.DataBind();
                }
                else
                {
                    rServices.DataSource = new List<object>();
                    rServices.DataBind();
                }

                btnLoadMore.Visible = rServices.Items.Count < TopVal ? false : true;
                list = null;
                servicesRepository = null;
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
                    }
                    else
                    {
                        Response.Redirect(".");
                    }
                }
                else
                {
                    Response.Redirect(".");
                }
            }

            TopVal += GeneralSetting.PageSize;
        }

        protected void rFavSrvs_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Fav")
            {
                try
                {
                    if (UserId > 0)
                    {
                        servicesRepository = new ServicesRepository();
                        servicesRepository.SetFavorite(ParseHelper.GetInt64(e.CommandArgument.ToString()).Value, UserId);
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

        protected override void OnPreRender(EventArgs e)
        {
            BindServices();
            base.OnPreRender(e);
        }

        protected void btnLoadMore_Click(object sender, EventArgs e)
        {
            BindServices();
        }

        #endregion
    }
}