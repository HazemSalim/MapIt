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
    public partial class MyProperties : MapIt.Lib.BasePage
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
                AspNetPager1.PageSize = GeneralSetting.PageSize;
                propertiesRepository = new PropertiesRepository();
                var list = propertiesRepository.Find(i => i.UserId == UserId && i.IsActive);//.OrderByDescending(p => p.Id).Take(TopVal);
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

        void Delete(long id)
        {
            try
            {
                propertiesRepository = new PropertiesRepository();
                int result = propertiesRepository.DeleteAnyWay(id);
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

        protected void rPros_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                Delete(long.Parse(e.CommandArgument.ToString()));
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
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        #endregion
    }
}
