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
    public partial class MyServices : MapIt.Lib.BasePage
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
                var list = servicesRepository.Find(i => i.UserId == UserId && i.IsActive).OrderByDescending(p => p.Id).Take(TopVal);
                if (list != null && list.Count() > 0)
                {
                    rSrvs.DataSource = list;
                    rSrvs.DataBind();
                }
                else
                {
                    rSrvs.DataSource = new List<object>();
                    rSrvs.DataBind();
                }

                btnLoadMore.Visible = rSrvs.Items.Count < TopVal ? false : true;
                list = null;
                servicesRepository = null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void Delete(long id)
        {
            try
            {
                servicesRepository = new ServicesRepository();
                int result = servicesRepository.DeleteAnyWay(id);
                switch (result)
                {
                    case -3:
                        PresentHelper.ShowScriptMessage(Resources.Resource.delete_error);
                        break;
                    case 1:
                        LoadData();
                        PresentHelper.ShowScriptMessage(Resources.Resource.delete_success);
                        break;
                }
            }
            catch (Exception ex)
            {
                PresentHelper.ShowScriptMessage(Resources.Resource.error);
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

        protected override void OnPreRender(EventArgs e)
        {
            LoadData();
            base.OnPreRender(e);
        }

        protected void btnLoadMore_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void rSrvs_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                Delete(long.Parse(e.CommandArgument.ToString()));
            }
        }

        #endregion
    }
}