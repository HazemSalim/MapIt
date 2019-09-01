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
    public partial class Types : MapIt.Lib.BasePage
    {
        #region Variables

        PropertyTypesRepository propertyTypesRepository;
        PropertiesRepository propertiesRepository;

        #endregion

        #region Properties

        public int TypeId
        {
            get
            {
                int id = 0;
                if (ViewState["TypeId"] != null && int.TryParse(ViewState["TypeId"].ToString(), out id))
                    return id;

                return 0;
            }
            set
            {
                ViewState["TypeId"] = value;
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

        void LoadData()
        {
            try
            {
                propertiesRepository = new PropertiesRepository();
                var list = propertiesRepository.GetAvProperties().Where(i =>(TypeId > 0 ? (i.TypeId == TypeId) : true)).OrderByDescending(o => o.AddedOn).Take(TopVal);

                if (list != null && list.Count() > 0)
                {
                    list = list.OrderByDescending(p => p.Id);

                    rPros.DataSource = list;
                    rPros.DataBind();

                    // Load Property Components
                    foreach (RepeaterItem item in rPros.Items)
                    {
                        if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
                        {
                            HiddenField hfProId = (HiddenField)item.FindControl("hfProId");
                            long? proId = ParseHelper.GetInt64(hfProId.Value);
                            Repeater rComponents = (Repeater)item.FindControl("rComponents");

                            if (proId.HasValue && proId.Value > 0)
                            {
                                propertiesRepository = new PropertiesRepository();
                                var proObj = propertiesRepository.GetByKey(proId);
                                if (proObj != null)
                                {
                                    var compList = proObj.PropertyComponents;

                                    if (compList != null && compList.Count() > 0)
                                    {
                                        rComponents.DataSource = compList.OrderBy(c => c.Component.Ordering);
                                        rComponents.DataBind();
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    rPros.DataSource = new List<object>();
                    rPros.DataBind();
                }

                btnLoadMore.Visible = rPros.Items.Count < TopVal ? false : true;
                list = null;
                propertiesRepository = null;
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
                        TypeId = id.Value;

                        propertyTypesRepository = new PropertyTypesRepository();
                        var typeObj = propertyTypesRepository.GetByKey(TypeId);
                        if (typeObj != null)
                        {
                            Title = Culture.ToLower() == "ar-kw" ? GeneralSetting.TitleAR + " | " + typeObj.TitleAR : GeneralSetting.TitleEN + " | " + typeObj.TitleEN;
                            litTitle.Text = Culture.ToLower() == "ar-kw" ? typeObj.TitleAR : typeObj.TitleEN;
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

        protected void rFavPros_ItemCommand(object source, RepeaterCommandEventArgs e)
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
