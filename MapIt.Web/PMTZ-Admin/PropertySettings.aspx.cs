using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web.Admin
{
    public partial class PropertySettings : System.Web.UI.Page
    {
        #region Variables

        PropertyTypesRepository propertyTypesRepository;
        PropertySettingsRepository propertySettingsRepository;

        #endregion

        #region Methods

        void BindPropertyTypes()
        {
            try
            {
                ddlPropertyType.DataValueField = "Id";
                ddlPropertyType.DataTextField = "TitleEN";

                propertyTypesRepository = new PropertyTypesRepository();
                var list = propertyTypesRepository.GetAll();
                if (list != null && list.Count() > 0)
                {
                    ddlPropertyType.DataSource = list.ToList();
                    ddlPropertyType.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void BindSettings(int proTypeId)
        {
            try
            {
                propertySettingsRepository = new PropertySettingsRepository();
                var data = propertySettingsRepository.GetSettings(proTypeId);
                if (data != null && data.Count > 0)
                {
                    rprSettings.DataSource = data;
                    rprSettings.DataBind();

                    lblMSG.Visible = false;
                    dvFields.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        protected bool IsMainCategory(object categoryName)
        {
            try
            {
                if (categoryName != null && categoryName.ToString().Trim().ToLower() == "main")
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return false;
            }
        }

        void Save()
        {
            try
            {
                int? proTypeId = ParseHelper.GetInt(ddlPropertyType.SelectedValue);
                if (proTypeId.HasValue && proTypeId.Value > 0)
                {
                    string data = string.Empty;
                    string category = "", property = "", available = "", mondatory = "";
                    foreach (RepeaterItem item in rprSettings.Items)
                    {
                        property = (item.FindControl("hfPropertyName") as HiddenField).Value;
                        category = (item.FindControl("hfCategory") as HiddenField).Value;
                        available = (item.FindControl("chkAvailable") as CheckBox).Checked ? "1" : "0";
                        if ((!string.IsNullOrEmpty(property) && IsMainCategory(category)) || (string.IsNullOrEmpty(property) && !IsMainCategory(category)))
                        {
                            mondatory = (item.FindControl("chkMondatory") as CheckBox).Checked ? "1" : "0";
                        }

                        if (!string.IsNullOrEmpty(property))
                        {
                            int? pId = ParseHelper.GetInt(property);
                            data += category + ",";
                            data += property + ",";
                            data += (pId.HasValue ? pId.Value.ToString() : "0") + ",";
                            data += available + ",";
                            data += mondatory + "#";
                        }
                    }

                    propertySettingsRepository = new PropertySettingsRepository();
                    bool result = propertySettingsRepository.Save(proTypeId.Value, data);
                    if (result == true)
                    {
                        PresentHelper.ShowScriptMessage("Saving successfully");
                    }
                    else
                    {
                        PresentHelper.ShowScriptMessage("Error in saving");
                    }
                }
                else
                {
                    lblMSG.Visible = true;
                    dvFields.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                PresentHelper.ShowScriptMessage("Error in saving");
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AdminUserId"] != null && (int)ParseHelper.GetInt(Session["AdminUserId"].ToString()) > 1 &&
                    !(new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminUserId"].ToString()), (int)AppEnums.AdminPages.PropertiesSettings)))
                {
                    Response.Redirect(".");
                }

                BindPropertyTypes();
            }
        }

        protected void ddlPropertyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPropertyType.SelectedIndex == 0)
            {
                lblMSG.Visible = true;
                dvFields.Visible = false;
            }
            else
            {
                int? proTypeId = ParseHelper.GetInt(ddlPropertyType.SelectedValue);
                if (proTypeId.HasValue && proTypeId.Value > 0)
                {
                    BindSettings(proTypeId.Value);
                }
                else
                {
                    lblMSG.Visible = true;
                    dvFields.Visible = false;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        #endregion
    }
}