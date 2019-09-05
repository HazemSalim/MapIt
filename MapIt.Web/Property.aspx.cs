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
    public partial class Property : MapIt.Lib.BasePage
    {
        #region Variables

        PropertiesRepository propertiesRepository;

        #endregion

        #region Properties

        public long PropertyId
        {
            get
            {
                long id = 0;
                if (ViewState["PropertyId"] != null && long.TryParse(ViewState["PropertyId"].ToString().Trim(), out id))
                    return id;

                return 0;
            }
            set
            {
                ViewState["PropertyId"] = value;
            }
        }

        public int TypeId
        {
            get
            {
                int id = 0;
                if (ViewState["TypeId"] != null && int.TryParse(ViewState["TypeId"].ToString().Trim(), out id))
                    return id;

                return 0;
            }
            set
            {
                ViewState["TypeId"] = value;
            }
        }

        #endregion

        #region Methods

        void LoadData()
        {
            try
            {
                propertiesRepository = new PropertiesRepository();

                var propertyObj = propertiesRepository.GetByKey(PropertyId);
                if (propertyObj != null)
                {
                    if (!propertyObj.IsActive)
                    {
                        Response.Redirect(".", false);
                        return;
                    }

                    //Increase Viewers Count
                    propertyObj.ViewersCount++;
                    propertiesRepository.Update(propertyObj);

                    Title = Culture.ToLower() == "ar-kw" ? GeneralSetting.TitleAR + " | " + propertyObj.TitleAR : GeneralSetting.TitleEN + " | " + propertyObj.TitleEN;
                    litTitle.Text = Culture.ToLower() == "ar-kw" ? propertyObj.TitleAR : propertyObj.TitleEN;
                    litDetails.Text = propertyObj.Details;

                    TypeId = propertyObj.TypeId;
                    litViewers.Text = propertyObj.ViewersCount.ToString();
                    litFav.Text = propertyObj.PropertyFavorites.Count.ToString();
                    litReports.Text = propertyObj.PropertyReports.Count.ToString();
                    litOthers.Text = (propertyObj.User.Properties.Count).ToString();
                    aOther.HRef = "/Proprties?op=1&user=" + propertyObj.UserId;
                    litDuration.Text = Culture.ToLower() == "ar-kw" ? PresentHelper.GetDurationAr(propertyObj.AddedOn) : PresentHelper.GetDurationEn(propertyObj.AddedOn);

                    tr_SellingPrice.Visible = propertyObj.SellingPrice.HasValue;
                    litSellingPrice.Text = propertyObj.SellingPrice.HasValue ? propertyObj.SellingPrice.Value.ToString() : string.Empty;

                    tr_RentPrice.Visible = propertyObj.RentPrice.HasValue;
                    litRentPrice.Text = propertyObj.RentPrice.HasValue ? propertyObj.RentPrice.Value.ToString() : string.Empty;

                    tr_Area.Visible = propertyObj.Area.HasValue;
                    litArea.Text = propertyObj.Area.HasValue ? propertyObj.Area.Value.ToString() : string.Empty;

                    tr_BuildingAge.Visible = propertyObj.BuildingAge.HasValue;
                    litBuildingAge.Text = propertyObj.BuildingAge.HasValue ? propertyObj.BuildingAge.Value.ToString() : string.Empty;

                    tr_MonthlyIncome.Visible = propertyObj.MonthlyIncome.HasValue;
                    litMonthlyIncome.Text = propertyObj.MonthlyIncome.HasValue ? propertyObj.MonthlyIncome.Value.ToString() : string.Empty;

                    tr_OtherPhones.Visible = !string.IsNullOrEmpty(propertyObj.OtherPhones);
                    litOtherPhones.Text = propertyObj.OtherPhones;

                    litAddress.Text = Culture.ToLower() == "ar-kw" ? propertyObj.AddressAR : propertyObj.AddressEN;

                    hdLocation.Value = propertyObj.Longitude + "," + propertyObj.Latitude;

                    if (propertyObj.PropertyComponents.Count() > 0)
                    {
                        rComponents.DataSource = propertyObj.PropertyComponents.OrderBy(c => c.Component.Ordering);
                        rComponents.DataBind();
                    }

                    if (propertyObj.PropertyFeatures.Count() > 0)
                    {
                        rFeatures.DataSource = propertyObj.PropertyFeatures;
                        rFeatures.DataBind();
                    }

                    if (propertyObj.PropertyPhotos.Count > 0)
                    {
                        rPhotos.DataSource = propertyObj.PropertyPhotos;
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

        void BindRelatedProperties()
        {
            try
            {
                propertiesRepository = new PropertiesRepository();
                var list = propertiesRepository.GetAvProperties().Where(s => (TypeId > 0 ? (s.TypeId == TypeId) : true) && s.Id != PropertyId)
                    .OrderByDescending(p => p.AddedOn).ThenByDescending(p => p.Id).Take(3);

                if (list != null && list.Count() > 0)
                {
                    rRelatedPros.DataSource = list;
                    rRelatedPros.DataBind();

                    // Load Property Components
                    foreach (RepeaterItem item in rRelatedPros.Items)
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
                    rRelatedPros.DataSource = new List<object>();
                    rRelatedPros.DataBind();
                    div_related.Visible = false;
                }
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
                        PropertyId = id.Value;
                        LoadData();
                        BindRelatedProperties();
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
                    propertiesRepository = new PropertiesRepository();
                    propertiesRepository.SetFavorite(PropertyId, UserId);
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

        protected void lnkBtnReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserId > 0)
                {
                    propertiesRepository = new PropertiesRepository();
                    propertiesRepository.SetReport(PropertyId, UserId, 1,string.Empty);
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

        protected void rRelatedPros_ItemCommand(object source, RepeaterCommandEventArgs e)
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

        #endregion
    }
}