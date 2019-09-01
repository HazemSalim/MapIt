using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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
    public partial class SrvFavorites : MapIt.Lib.BasePage
    {
        #region Variables

        ServicesRepository servicesRepository;

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
                servicesRepository = new ServicesRepository();
                var list = servicesRepository.GetAvServices();

                List<long> favIds = servicesRepository.Entities.ServiceFavorites.Where(uf => uf.UserId == UserId).Select(uf => uf.ServiceId).ToList();
                list = list.Where(l => favIds.Contains(l.Id)).OrderByDescending(f => f.AddedOn).Take(TopVal);

                if (list != null && list.Count() > 0)
                {
                    list = list.OrderByDescending(p => p.Id);

                    rFavSrvs.DataSource = list;
                    rFavSrvs.DataBind();
                }
                else
                {
                    rFavSrvs.DataSource = new List<object>();
                    rFavSrvs.DataBind();
                }

                btnLoadMore.Visible = rFavSrvs.Items.Count < TopVal ? false : true;
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
            if (UserId <= 0)
            {
                Response.Redirect("~/Login?ReturnUrl=" + Request.RawUrl, true);
            }

            TopVal += GeneralSetting.PageSize;
        }

        protected void rFavSrvs_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteFav")
            {
                try
                {
                    if (UserId > 0)
                    {
                        servicesRepository = new ServicesRepository();
                        servicesRepository.SetFavorite(long.Parse(e.CommandArgument.ToString()), UserId);
                        Response.Redirect("~/SrvFavorites", true);
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