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
    public partial class ProFavorites : MapIt.Lib.BasePage
    {
        #region Variables

        PropertiesRepository propertiesRepository;

        #endregion

        #region Properties

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
                var list = propertiesRepository.GetAvProperties();

                List<long> favIds = propertiesRepository.Entities.PropertyFavorites.Where(uf => uf.UserId == UserId).Select(uf => uf.PropertyId).ToList();
                list = list.Where(l => favIds.Contains(l.Id)).OrderByDescending(f => f.AddedOn).Take(TopVal);

                if (list != null && list.Count() > 0)
                {
                    list = list.OrderByDescending(p => p.Id);

                    rFavPros.DataSource = list;
                    rFavPros.DataBind();

                    // Load Property Components
                    foreach (RepeaterItem item in rFavPros.Items)
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
                    rFavPros.DataSource = new List<object>();
                    rFavPros.DataBind();
                }

                btnLoadMore.Visible = rFavPros.Items.Count < TopVal ? false : true;
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
            if (UserId <= 0)
            {
                Response.Redirect("~/Login?ReturnUrl=" + Request.RawUrl, true);
            }

            TopVal += GeneralSetting.PageSize;
        }

        protected void rFavPros_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteFav")
            {
                try
                {
                    if (UserId > 0)
                    {
                        propertiesRepository = new PropertiesRepository();
                        propertiesRepository.SetFavorite(long.Parse(e.CommandArgument.ToString()), UserId);
                        Response.Redirect("~/ProFavorites", true);
                    }
                    else
                    {
                        PresentHelper.ShowScriptMessage(Resources.Resource.error);
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
            if (!IsPostBack)
            {
                LoadData();
            }

            base.OnPreRender(e);
        }

        protected void btnLoadMore_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        #endregion
    }
}
