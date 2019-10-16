using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using MapIt.Helpers;
using MapIt.Repository;

namespace MapIt.Web
{
    public partial class Service : MapIt.Lib.BasePage
    {
        #region Variables

        ServicesRepository servicesRepository;

        #endregion

        #region Properties

        public long ServiceId
        {
            get
            {
                long id = 0;
                if (ViewState["ServiceId"] != null && long.TryParse(ViewState["ServiceId"].ToString().Trim(), out id))
                    return id;

                return 0;
            }
            set
            {
                ViewState["ServiceId"] = value;
            }
        }

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

        #endregion

        #region Methods

        void LoadData()
        {
            try
            {
                servicesRepository = new ServicesRepository();

                var serviceObj = servicesRepository.GetByKey(ServiceId);
                if (serviceObj != null)
                {
                    if (!serviceObj.IsActive)
                    {
                        Response.Redirect(".", false);
                        return;
                    }

                    Title = Culture.ToLower() == "ar-kw" ? GeneralSetting.TitleAR + " | " + serviceObj.Title : GeneralSetting.TitleEN + " | " + serviceObj.Title;
                    litTitle.Text = serviceObj.Title;
                    litDesc.Text = serviceObj.Description;

                    CategoryId = serviceObj.CategoryId;
                    aPhone.InnerText = serviceObj.User.Phone;
                    //aPhone.HRef = "tel:" + serviceObj.User.Phone;

                    aReportAbuse.HRef = "/ReportAbuse?op=2&id=" + serviceObj.Id;

                    litViewers.Text = serviceObj.ViewersCount.ToString();
                    litFav.Text = serviceObj.ServiceFavorites.Count.ToString();
                    litYears.Text = serviceObj.ExYears.ToString();
                    litReports.Text = serviceObj.ServiceReports.Count.ToString();
                    litOthers.Text = (serviceObj.User.Services.Count - 1).ToString();

                    //iframeMap.Attributes.Add("src", "http://maps.google.com/maps?output=embed&z=14&t=m&q=loc:" + serviceObj.Latitude + "+" + serviceObj.Longitude);
                    hdLocation.Value = serviceObj.Longitude + "," + serviceObj.Latitude;

                    if (serviceObj.ServicePhotos.Count > 0)
                    {
                        rPhotos.DataSource = serviceObj.ServicePhotos;
                        rPhotos.DataBind();
                    }
                }
                else
                {
                    Response.Redirect(".");
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                Response.Redirect(".");
            }
        }

        void BindRelatedServices()
        {
            try
            {
                servicesRepository = new ServicesRepository();
                var list = servicesRepository.GetAvServices().Where(s => (CategoryId > 0 ? (s.CategoryId == CategoryId) : true) && s.Id != ServiceId)
                    .OrderByDescending(p => p.AddedOn).ThenByDescending(p => p.Id).Take(3);

                if (list != null && list.Count() > 0)
                {
                    rRelatedSrvs.DataSource = list;
                    rRelatedSrvs.DataBind();
                }
                else
                {
                    rRelatedSrvs.DataSource = new List<object>();
                    rRelatedSrvs.DataBind();
                    div_related.Visible = false;
                }
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
                        ServiceId = id.Value;
                        LoadData();
                        BindRelatedServices();
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
        }

        protected void lnkAddToFavorite_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserId > 0)
                {
                    servicesRepository = new ServicesRepository();
                    servicesRepository.SetFavorite(ServiceId, UserId);
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

        protected void rRelatedSrvs_ItemCommand(object source, RepeaterCommandEventArgs e)
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

        #endregion
    }
}