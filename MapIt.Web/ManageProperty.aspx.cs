using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web
{
    public partial class ManageProperty : MapIt.Lib.BasePage
    {
        #region Variables

        PurposesRepository purposesRepository;
        PropertyTypesRepository propertyTypesRepository;
        CountriesRepository countriesRepository;
        CitiesRepository citiesRepository;
        AreasRepository areasRepository;
        BlocksRepository blocksRepository;
        PropertySettingsRepository propertySettingsRepository;
        FeaturesRepository featuresRepository;
        ComponentsRepository componentsRepository;
        PropertiesRepository propertiesRepository;
        PropertyPhotosRepository propertyPhotosRepository;
        //PropertyFeaturesRepository propertyFeaturesRepository;
        //PropertyComponentsRepository propertyComponentsRepository;

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

        #endregion

        #region Methods

        void SelectValue(CheckBoxList cbl, object value)
        {
            try
            {
                if (cbl != null && value != null)
                {
                    cbl.SelectedValue = value.ToString();
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void BindPurposes()
        {
            try
            {
                ddlPurpose.DataValueField = "Id";
                ddlPurpose.DataTextField = Resources.Resource.db_title_col;

                purposesRepository = new PurposesRepository();
                var data = purposesRepository.GetAll().ToList();
                if (data != null)
                {
                    ddlPurpose.DataSource = data.OrderBy(c => c.Id).ToList();
                    ddlPurpose.DataBind();
                }

                data = null;
                purposesRepository = null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void BindTypes()
        {
            try
            {
                ddlType.DataValueField = "Id";
                ddlType.DataTextField = Resources.Resource.db_title_col;

                propertyTypesRepository = new PropertyTypesRepository();
                var data = propertyTypesRepository.GetAll().ToList();
                if (data != null)
                {
                    ddlType.DataSource = data.OrderBy(c => c.Ordering).ToList();
                    ddlType.DataBind();
                }

                data = null;
                propertyTypesRepository = null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }
        void BindCountries()
        {
            try
            {
                ddlCountry.DataValueField = "Id";
                ddlCountry.DataTextField = Resources.Resource.db_title_col;

                countriesRepository = new CountriesRepository();
                var data = countriesRepository.Find(a => a.IsActive).ToList();
                if (data != null)
                {
                    ddlCountry.DataSource = data.OrderBy(c => c.TitleEN).ToList();
                    ddlCountry.DataBind();
                }

                data = null;
                countriesRepository = null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void BindCities()
        {
            try
            {
                ddlCity.DataValueField = "Id";
                ddlCity.DataTextField = Resources.Resource.db_title_col;

                for (int i = ddlCity.Items.Count - 1; i > 0; i--)
                {
                    ddlCity.Items.RemoveAt(i);
                }

                citiesRepository = new CitiesRepository();
                int? countryId = ParseHelper.GetInt(ddlCountry.SelectedValue);
                if (countryId.HasValue)
                {
                    var data = citiesRepository.Find(c => c.CountryId == countryId.Value).ToList();
                    if (data != null)
                    {
                        ddlCity.DataSource = data.OrderBy(c => c.TitleEN).ToList();
                        ddlCity.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void BindAreas()
        {
            try
            {
                ddlArea.DataValueField = "Id";
                ddlArea.DataTextField = Resources.Resource.db_title_col;

                for (int i = ddlArea.Items.Count - 1; i > 0; i--)
                {
                    ddlArea.Items.RemoveAt(i);
                }

                int? cityId = ParseHelper.GetInt(ddlCity.SelectedValue);
                if (cityId.HasValue)
                {
                    areasRepository = new AreasRepository();
                    var data = areasRepository.Find(c => c.CityId == cityId.Value).ToList();
                    if (data != null && data.Count > 0)
                    {
                        ddlArea.DataSource = data;
                        ddlArea.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void BindBlocks()
        {
            try
            {
                ddlBlock.DataValueField = "Id";
                ddlBlock.DataTextField = Resources.Resource.db_title_col;

                for (int i = ddlBlock.Items.Count - 1; i > 0; i--)
                {
                    ddlBlock.Items.RemoveAt(i);
                }

                int? areaId = ParseHelper.GetInt(ddlArea.SelectedValue);
                if (areaId.HasValue)
                {
                    blocksRepository = new BlocksRepository();
                    var data = blocksRepository.Find(c => c.AreaId == areaId.Value).ToList();
                    if (data != null && data.Count > 0)
                    {
                        ddlBlock.DataSource = data;
                        ddlBlock.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void HideAllFields()
        {
            div_Aream2.Visible = div_BuildingAge.Visible = div_MonthlyIncome.Visible = div_SellingPrice.Visible = div_RentPrice.Visible = div_Details.Visible =
                div_Features.Visible = div_Components.Visible = false;
        }

        void ClearLists()
        {
            try
            {
                //for (int i = cblFeatures.Items.Count - 1; i > 0; i--)
                //{
                //    cblFeatures.Items.RemoveAt(i);
                //}

                //for (int i = cblComponents.Items.Count - 1; i > 0; i--)
                //{
                //    cblComponents.Items.RemoveAt(i);
                //}

                rFeatures.DataSource = null;
                rFeatures.DataBind();

                rComponents.DataSource = null;
                rComponents.DataBind();

                // load other phones repeater
                var list = new List<string>().Select(p => new
                {
                    Code = string.Empty,
                    Phone = string.Empty
                });

                rOtherPhones.DataSource = list;
                rOtherPhones.DataBind();
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void BindBasedOnType()
        {
            try
            {
                int? propertyTypeId = ParseHelper.GetInt(ddlType.SelectedValue);

                if (propertyTypeId.HasValue && propertyTypeId.Value > 0)
                {
                    propertySettingsRepository = new PropertySettingsRepository();
                    var settingsList = propertySettingsRepository.Find(s => s.TypeId == propertyTypeId.Value && s.IsAvailable == true).ToList();

                    if (settingsList == null)
                        settingsList = new List<MapIt.Data.PropertySetting>();

                    HideAllFields();

                    //Main
                    var s_Aream2 = settingsList.FirstOrDefault(s => s.Category == "Main" && s.PropertyName == "Area");
                    if (s_Aream2 != null)
                    {
                        div_Aream2.Visible = true;
                        rfvAream2.Enabled = s_Aream2.IsMondatory;
                    }

                    var s_BuildingAge = settingsList.FirstOrDefault(s => s.Category == "Main" && s.PropertyName == "BuildingAge");
                    if (s_BuildingAge != null)
                    {
                        div_BuildingAge.Visible = true;
                        rfvBuildingAge.Enabled = s_BuildingAge.IsMondatory;
                    }

                    var s_MonthlyIncome = settingsList.FirstOrDefault(s => s.Category == "Main" && s.PropertyName == "MonthlyIncome");
                    if (s_MonthlyIncome != null)
                    {
                        div_MonthlyIncome.Visible = true;
                        rfvMonthlyIncome.Enabled = s_MonthlyIncome.IsMondatory;
                    }

                    var s_SellingPrice = settingsList.FirstOrDefault(s => s.Category == "Main" && s.PropertyName == "SellingPrice");
                    if (s_SellingPrice != null)
                    {
                        div_SellingPrice.Visible = true;
                        rfvSellingPrice.Enabled = s_SellingPrice.IsMondatory;
                    }

                    var s_RentPrice = settingsList.FirstOrDefault(s => s.Category == "Main" && s.PropertyName == "RentPrice");
                    if (s_RentPrice != null)
                    {
                        div_RentPrice.Visible = true;
                        rfvRentPrice.Enabled = s_RentPrice.IsMondatory;
                    }

                    var s_Details = settingsList.FirstOrDefault(s => s.Category == "Main" && s.PropertyName == "Details");
                    if (s_Details != null)
                    {
                        div_Details.Visible = true;
                        rfvDetails.Enabled = s_Details.IsMondatory;
                    }


                    // Fill CheckBoxLists
                    ClearLists();

                    //Feature
                    var s_Feature = settingsList.FirstOrDefault(s => s.Category == "Feature");
                    if (s_Feature != null)
                    {
                        featuresRepository = new FeaturesRepository();
                        var data = from s in featuresRepository.GetBasic(propertyTypeId.Value).ToList()
                                   select new { TitleEN = s.TitleEN, FeatureId = s.Id, IsChecked = false };

                        rFeatures.DataSource = data;
                        rFeatures.DataBind();

                        div_Features.Visible = true;
                    }

                    //Component
                    var s_Component = settingsList.FirstOrDefault(s => s.Category == "Component");
                    if (s_Component != null)
                    {
                        componentsRepository = new ComponentsRepository();
                        var data = from s in componentsRepository.GetBasic(propertyTypeId.Value).ToList()
                                   select new { TitleEN = s.TitleEN, ComponentId = s.Id, Count = 0 };

                        rComponents.DataSource = data;
                        rComponents.DataBind();

                        div_Components.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void SetData()
        {
            try
            {
                if (PropertyId > 0)
                {
                    propertiesRepository = new PropertiesRepository();
                    var propertyObj = propertiesRepository.GetByKey(PropertyId);
                    if (propertyObj != null)
                    {
                        if (propertyObj.UserId != UserId)
                        {
                            Response.Redirect("~/MyProperties");
                            return;
                        }

                        ddlPurpose.SelectedValue = propertyObj.PurposeId.ToString();
                        ddlType.SelectedValue = propertyObj.TypeId.ToString();
                        ddlType.Enabled = false;
                        ddlType.BackColor = System.Drawing.Color.Gray;

                        txtPACI.Text = Convert.ToString(propertyObj.PACI);
                        txtStreet.Text = Convert.ToString(propertyObj.Street);
                        if (propertyObj.BlockId.HasValue)
                        {
                            ddlCountry.SelectedValue = propertyObj.CountryId.ToString();
                            BindCities();
                            ddlCity.SelectedValue = propertyObj.Block.Area.CityId.ToString();
                            BindAreas();
                            ddlArea.SelectedValue = propertyObj.Block.AreaId.ToString();
                            BindBlocks();
                            ddlBlock.SelectedValue = propertyObj.BlockId.ToString();
                        }

                        if (!string.IsNullOrEmpty(propertyObj.Latitude) && !string.IsNullOrEmpty(propertyObj.Longitude))
                        {
                            txtLocation.Text = propertyObj.Latitude.Trim() + ", " + propertyObj.Longitude.Trim();
                            hfLatitude.Value = propertyObj.Latitude.Trim();
                            hfLongitude.Value = propertyObj.Longitude.Trim();
                        }

                        propertySettingsRepository = new PropertySettingsRepository();
                        var settingsList = propertySettingsRepository.Find(s => s.TypeId == propertyObj.TypeId && s.IsAvailable == true).ToList();

                        if (settingsList == null)
                            settingsList = new List<MapIt.Data.PropertySetting>();

                        HideAllFields();

                        //Main
                        var s_Aream2 = settingsList.FirstOrDefault(s => s.Category == "Main" && s.PropertyName == "Area");
                        if (s_Aream2 != null)
                        {
                            div_Aream2.Visible = true;
                            txtAream2.Text = propertyObj.Area.HasValue ? propertyObj.Area.Value.ToString() : string.Empty;
                            rfvAream2.Enabled = s_Aream2.IsMondatory;
                        }

                        var s_BuildingAge = settingsList.FirstOrDefault(s => s.Category == "Main" && s.PropertyName == "BuildingAge");
                        if (s_BuildingAge != null)
                        {
                            div_BuildingAge.Visible = true;
                            txtBuildingAge.Text = propertyObj.BuildingAge.HasValue ? propertyObj.BuildingAge.Value.ToString() : string.Empty;
                            rfvBuildingAge.Enabled = s_BuildingAge.IsMondatory;
                        }

                        var s_MonthlyIncome = settingsList.FirstOrDefault(s => s.Category == "Main" && s.PropertyName == "MonthlyIncome");
                        if (s_MonthlyIncome != null)
                        {
                            div_MonthlyIncome.Visible = true;
                            txtMonthlyIncome.Text = propertyObj.MonthlyIncome.HasValue ? propertyObj.MonthlyIncome.Value.ToString() : string.Empty;
                            rfvMonthlyIncome.Enabled = s_MonthlyIncome.IsMondatory;
                        }

                        var s_SellingPrice = settingsList.FirstOrDefault(s => s.Category == "Main" && s.PropertyName == "SellingPrice");
                        if (s_SellingPrice != null)
                        {
                            div_SellingPrice.Visible = true;
                            txtSellingPrice.Text = propertyObj.SellingPrice.HasValue ? propertyObj.SellingPrice.Value.ToString() : string.Empty;
                            rfvSellingPrice.Enabled = s_SellingPrice.IsMondatory;
                        }

                        var s_RentPrice = settingsList.FirstOrDefault(s => s.Category == "Main" && s.PropertyName == "RentPrice");
                        if (s_RentPrice != null)
                        {
                            div_RentPrice.Visible = true;
                            txtRentPrice.Text = propertyObj.RentPrice.HasValue ? propertyObj.RentPrice.Value.ToString() : string.Empty;
                            rfvRentPrice.Enabled = s_RentPrice.IsMondatory;
                        }

                        var s_Details = settingsList.FirstOrDefault(s => s.Category == "Main" && s.PropertyName == "Details");
                        if (s_Details != null)
                        {
                            div_Details.Visible = true;
                            txtDetails.Text = propertyObj.Details;
                            rfvDetails.Enabled = s_Details.IsMondatory;
                        }

                        ClearLists();

                        //Feature
                        var s_Feature = settingsList.FirstOrDefault(s => s.Category == "Feature");
                        if (s_Feature != null)
                        {
                            featuresRepository = new FeaturesRepository();
                            var data = from s in featuresRepository.GetBasic(propertyObj.TypeId)
                                       join p in propertyObj.PropertyFeatures on s.Id equals p.FeatureId into op
                                       from p in op.DefaultIfEmpty()
                                       select new
                                       {
                                           TitleEN = s.TitleEN,
                                           FeatureId = s.Id,
                                           IsChecked = p == null ? false : true
                                       };

                            rFeatures.DataSource = data.ToList();
                            rFeatures.DataBind();

                            div_Features.Visible = true;
                        }

                        //Component
                        var s_Component = settingsList.FirstOrDefault(s => s.Category == "Component");
                        if (s_Component != null)
                        {
                            componentsRepository = new ComponentsRepository();
                            var data = from s in componentsRepository.GetBasic(propertyObj.TypeId)
                                       join p in propertyObj.PropertyComponents on s.Id equals p.ComponentId into op
                                       from p in op.DefaultIfEmpty()
                                       select new
                                       {
                                           TitleEN = s.TitleEN,
                                           ComponentId = s.Id,
                                           Count = p == null ? 0 : p.Count
                                       };

                            rComponents.DataSource = data.ToList();
                            rComponents.DataBind();

                            div_Components.Visible = true;
                        }

                        // Other Phones
                        if (!string.IsNullOrEmpty(propertyObj.OtherPhones))
                        {
                            if (propertyObj.OtherPhones.EndsWith(","))
                            {
                                propertyObj.OtherPhones = propertyObj.OtherPhones.TrimEnd(',');
                            }

                            var otherPhones = propertyObj.OtherPhones.Split(',').Select(p => new
                            {
                                Code = p.Split(' ')[0],
                                Phone = p.Split(' ')[1]
                            });
                            if (otherPhones != null && otherPhones.Count() > 0)
                            {
                                rOtherPhones.DataSource = otherPhones;
                                rOtherPhones.DataBind();
                            }
                        }

                        foreach (RepeaterItem item in rOtherPhones.Items)
                        {
                            // Phones List
                            HiddenField hfCode1 = item.FindControl("hfCode1") as HiddenField;
                            DropDownList ddlCode1 = item.FindControl("ddlCode1") as DropDownList;
                            if (ddlCode1 != null)
                            {
                                ddlCode1.DataValueField = "CCode";
                                ddlCode1.DataTextField = "FullCode";

                                countriesRepository = new CountriesRepository();
                                var list = countriesRepository.Find(c => c.IsActive).ToList();
                                if (list != null)
                                {
                                    ddlCode1.DataSource = list.OrderBy(c => c.ISOCode).ToList();
                                    ddlCode1.DataBind();
                                }

                                if (list != null && list.Count > 0)
                                {
                                    ddlCode1.DataSource = list;
                                    ddlCode1.DataBind();
                                    ddlCode1.SelectedValue = hfCode1.Value;
                                }

                                list = null;
                                countriesRepository = null;
                            }
                        }

                        LoadPhotos(propertyObj.PropertyPhotos.ToList());
                        Title = Resources.Resource.web_title + " | " + Resources.Resource.edit_property;
                        litTitle.Text = Resources.Resource.edit_property;
                    }
                    else
                    {
                        Response.Redirect("~/MyProperties");
                    }
                }
                else
                {
                    LoadPhotos(null);
                    Title = Resources.Resource.web_title + " | " + Resources.Resource.add_new_property;
                    litTitle.Text = Resources.Resource.add_new_property;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                Response.Redirect("~/MyProperties");
            }
        }

        List<string> GetPhoneList()
        {
            try
            {
                var phoneList = new List<string>();
                for (int i = 0; i < rOtherPhones.Items.Count; i++)
                {
                    if (rOtherPhones.Items[i].Visible)
                    {
                        DropDownList ddlCode1 = rOtherPhones.Items[i].FindControl("ddlCode1") as DropDownList;
                        TextBox txtPhone1 = rOtherPhones.Items[i].FindControl("txtPhone1") as TextBox;

                        if (ddlCode1 != null && txtPhone1 != null)
                        {
                            string code = ddlCode1.SelectedValue;
                            string phone = txtPhone1.Text;

                            if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(phone))
                            {
                                phoneList.Add(code + " " + phone);
                            }
                        }
                    }
                }

                return phoneList;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return new List<string>();
            }
        }

        void LoadPhotos(List<PropertyPhoto> photosList)
        {
            try
            {
                if (photosList == null)
                {
                    photosList = new List<PropertyPhoto>();
                }

                int currentLength = photosList.Count;
                int remainingLength = 12 - currentLength;
                for (int i = 0; i < remainingLength; i++)
                {
                    photosList.Add(new PropertyPhoto { Id = 0, Photo = string.Empty });
                }
                var list = photosList.Select(p => new
                {
                    Photo = String.IsNullOrEmpty(p.Photo) ? string.Empty : p.Photo,
                    FullPhoto = String.IsNullOrEmpty(p.Photo) ? string.Empty : AppSettings.PropertyWMPhotos + p.Photo
                });
                rPhotos.DataSource = list;
                rPhotos.DataBind();
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        string SavePhoto(FileUpload fuPhoto)
        {
            try
            {
                string imageExt = Path.GetExtension(fuPhoto.FileName);
                string imageName = Guid.NewGuid().ToString();
                string imagePath = AppSettings.PropertyPhotos + imageName + imageExt;

                FileStream fs = new FileStream(Server.MapPath(imagePath), FileMode.Create, FileAccess.ReadWrite);
                fs.Write(fuPhoto.FileBytes, 0, fuPhoto.FileBytes.Length);
                fs.Close();

                SaveAsWaterMark(fuPhoto.PostedFile, imageName + imageExt);
                return imageName + imageExt;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return null;
            }
        }
        List<PropertyComponent> GetComponentList()
        {
            try
            {
                var componentList = new List<PropertyComponent>();
                for (int i = 0; i < rComponents.Items.Count; i++)
                {
                    HiddenField hfComponentId = rComponents.Items[i].FindControl("hdnComponentId") as HiddenField;
                    TextBox txtCount = rComponents.Items[i].FindControl("txtCount") as TextBox;

                    if (hfComponentId != null && txtCount != null)
                    {
                        int? id = ParseHelper.GetInt(hfComponentId.Value);
                        int? count = ParseHelper.GetInt(txtCount.Text);
                        if (count > 0)
                        {
                            componentList.Add(new PropertyComponent { ComponentId = id.Value, Count = count.Value });
                        }
                    }
                }

                return componentList;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return new List<PropertyComponent>();
            }
        }
        List<PropertyFeature> GetFeatureList()
        {
            try
            {
                var featureList = new List<PropertyFeature>();
                for (int i = 0; i < rFeatures.Items.Count; i++)
                {
                    HiddenField hfFeatureId = rFeatures.Items[i].FindControl("hdnFeatureId") as HiddenField;
                    CheckBox isChecked = rFeatures.Items[i].FindControl("chkFeature") as CheckBox;

                    if (hfFeatureId != null && isChecked != null)
                    {
                        int? id = ParseHelper.GetInt(hfFeatureId.Value);
                        bool? _isChecked = ParseHelper.GetBool(isChecked.Checked);
                        if (_isChecked.HasValue && _isChecked.Value)
                        {
                            featureList.Add(new PropertyFeature { FeatureId = id.Value });
                        }
                    }
                }

                return featureList;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return new List<PropertyFeature>();
            }
        }
        void SaveAsWaterMark(HttpPostedFile flfile, string imageName)
        {
            try
            {
                byte[] imageByte = null;
                using (var binaryReader = new BinaryReader(flfile.InputStream))
                {
                    imageByte = binaryReader.ReadBytes(flfile.ContentLength);
                }

                MemoryStream memStream = new MemoryStream(imageByte);
                System.Drawing.Bitmap image = (System.Drawing.Bitmap)System.Drawing.Image.FromStream(memStream);
                System.Drawing.Bitmap waterMarkImage = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(Server.MapPath(AppSettings.WaterMark));

                Graphics graphics = System.Drawing.Graphics.FromImage(image);
                waterMarkImage.SetResolution(graphics.DpiX, graphics.DpiY);

                int width = image.Width / 6;
                int height = (width * waterMarkImage.Height) / waterMarkImage.Width;

                int x = 10;
                int y = image.Height - ((image.Height * 30) / 100);

                graphics.DrawImage(waterMarkImage, x, y, width, height);
                image.Save(Server.MapPath(AppSettings.PropertyWMPhotos + imageName));
                PresentHelper.ResizeImage(AppSettings.PropertyWMPhotos + imageName, AppSettings.PropertyWMPhotos300 + imageName, 300);
                PresentHelper.ResizeImage(AppSettings.PropertyWMPhotos + imageName, AppSettings.PropertyWMPhotos540 + imageName, 540);
                PresentHelper.ResizeImage(AppSettings.PropertyWMPhotos + imageName, AppSettings.PropertyWMPhotos1080 + imageName, 1080);

                graphics.Dispose();
                waterMarkImage.Dispose();
                image.Dispose();
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        string GetPhotos()
        {
            try
            {
                string images = string.Empty;

                for (int i = 0; i < rPhotos.Items.Count; i++)
                {
                    string photo = string.Empty;
                    FileUpload fuPhoto = rPhotos.Items[i].FindControl("fuPhoto") as FileUpload;
                    HiddenField hfPhoto = rPhotos.Items[i].FindControl("hfPhoto") as HiddenField;
                    if (fuPhoto.HasFile)
                    {
                        photo = SavePhoto(fuPhoto);
                    }
                    if (string.IsNullOrEmpty(photo) && hfPhoto != null && !string.IsNullOrEmpty(hfPhoto.Value.Trim()) && hfPhoto.Value.Trim() != AppSettings.NoImage.Trim())
                    {
                        photo = hfPhoto.Value.Trim();
                    }
                    if (!string.IsNullOrEmpty(photo))
                    {
                        images += photo + ",";
                    }
                }

                return images;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return null;
            }
        }

        Data.Property GetData()
        {
            try
            {
                int? purposeId = ParseHelper.GetInt(ddlPurpose.SelectedValue);
                if (!purposeId.HasValue || purposeId.Value <= 0)
                {
                    PresentHelper.ShowScriptMessage(Resources.Resource.select_purpose);
                    return null;
                }

                int? typeId = ParseHelper.GetInt(ddlType.SelectedValue);
                if (!typeId.HasValue || typeId.Value <= 0)
                {
                    PresentHelper.ShowScriptMessage(Resources.Resource.select_type);
                    return null;
                }

                int? countryId = ParseHelper.GetInt(ddlCountry.SelectedValue);
                if (!countryId.HasValue || countryId.Value <= 0)
                {
                    PresentHelper.ShowScriptMessage(Resources.Resource.select_country);
                    return null;
                }

                int? cityId = ParseHelper.GetInt(ddlCity.SelectedValue);
                if (!cityId.HasValue || cityId.Value <= 0)
                {
                    PresentHelper.ShowScriptMessage(Resources.Resource.select_city);
                    return null;
                }

                int? areaId = ParseHelper.GetInt(ddlArea.SelectedValue);
                if (!areaId.HasValue || areaId.Value <= 0)
                {
                    PresentHelper.ShowScriptMessage(Resources.Resource.select_area);
                    return null;
                }

                int? blockId = ParseHelper.GetInt(ddlBlock.SelectedValue);
                if (!blockId.HasValue || blockId.Value <= 0)
                {
                    PresentHelper.ShowScriptMessage(Resources.Resource.select_block);
                    return null;
                }

                MapIt.Data.Property propertyObj;

                if (PropertyId > 0)
                {
                    propertiesRepository = new PropertiesRepository();
                    propertyObj = propertiesRepository.GetByKey(PropertyId);
                    propertyObj.ModifiedOn = DateTime.Now;
                }
                else
                {
                    propertyObj = new MapIt.Data.Property();
                    propertyObj.ViewersCount = 0;
                    propertyObj.AddedOn = DateTime.Now;
                }

                propertyObj.UserId = this.UserId;
                propertyObj.PurposeId = purposeId.Value;
                propertyObj.TypeId = typeId.Value;
                propertyObj.CountryId = countryId.Value;
                propertyObj.BlockId = blockId.Value;

                if (div_Aream2.Visible)
                {
                    propertyObj.Area = ParseHelper.GetDouble(txtAream2.Text);
                }

                if (div_BuildingAge.Visible)
                {
                    propertyObj.BuildingAge = ParseHelper.GetInt(txtBuildingAge.Text);
                }

                if (div_MonthlyIncome.Visible)
                {
                    propertyObj.MonthlyIncome = ParseHelper.GetDouble(txtMonthlyIncome.Text);
                }

                if (div_SellingPrice.Visible)
                {
                    propertyObj.SellingPrice = ParseHelper.GetDouble(txtSellingPrice.Text);
                }

                if (div_RentPrice.Visible)
                {
                    propertyObj.RentPrice = ParseHelper.GetDouble(txtRentPrice.Text);
                }

                if (div_Details.Visible && !string.IsNullOrEmpty(txtDetails.Text))
                {
                    propertyObj.Details = txtDetails.Text;
                }

                //Other Phones
                var phoneList = GetPhoneList();
                propertyObj.OtherPhones = string.Join(",", phoneList);

                propertyObj.Latitude = hfLatitude.Value;
                propertyObj.Longitude = hfLongitude.Value;

                propertyObj.Street = txtStreet.Text;
                propertyObj.PACI = txtPACI.Text;

                propertyObj.PaySpecial = propertyObj.IsSpecial = false;
                propertyObj.PayVideo = false;

                propertyObj.IsAvailable = true;
                propertyObj.IsActive = GeneralSetting.AutoActiveAd;
                propertyObj.AdminAdded = false;

                return propertyObj;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                PresentHelper.ShowScriptMessage(Resources.Resource.error);
                return null;
            }
        }

        void Save()
        {
            try
            {
                propertiesRepository = new PropertiesRepository();
                propertyPhotosRepository = new PropertyPhotosRepository();

                string photos = GetPhotos();
                var photosList = new List<PropertyPhoto>();
                if (!string.IsNullOrEmpty(photos))
                {
                    string[] imagesArr = photos.Split(',');
                    foreach (string img in imagesArr)
                    {
                        if (!string.IsNullOrEmpty(img) && img.Trim() != string.Empty)
                            photosList.Add(new PropertyPhoto { Photo = img });
                    }
                }
                else
                {
                    PresentHelper.ShowScriptMessage(Resources.Resource.select_photo);
                    return;
                }

                var componentList = GetComponentList();
                var featureList = GetFeatureList();

                var propertyObj = GetData();
                if (propertyObj != null)
                {
                    if (propertyObj.Id > 0)
                    {
                        propertyPhotosRepository.Delete(pp => pp.PropertyId == propertyObj.Id);
                        photosList.ForEach(propertyObj.PropertyPhotos.Add);

                        propertiesRepository.DeletePropertyComponents(propertyObj);
                        componentList.ForEach(propertyObj.PropertyComponents.Add);

                        propertiesRepository.DeletePropertyFeatures(propertyObj);
                        featureList.ForEach(propertyObj.PropertyFeatures.Add);

                        propertiesRepository.Update(propertyObj);
                    }
                    else
                    {
                        photosList.ForEach(propertyObj.PropertyPhotos.Add);
                        componentList.ForEach(propertyObj.PropertyComponents.Add);
                        featureList.ForEach(propertyObj.PropertyFeatures.Add);
                        propertiesRepository.Add(propertyObj);
                    }

                    // Saving message ...
                    if (propertyObj.IsActive)
                    {
                        PresentHelper.ShowScriptMessage(Resources.Resource.save_success, "MyProperties");
                    }
                    else
                    {
                        PresentHelper.ShowScriptMessage(Resources.Resource.save_success_with_confirm, "MyProperties");
                    }
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

            if (!IsPostBack)
            {
                BindPurposes();
                BindTypes();
                BindCountries();
                ClearLists();

                long id = 0;
                if (Request.QueryString["id"] != null && long.TryParse(Request.QueryString["id"].Trim(), out id))
                {
                    PropertyId = id;
                }

                SetData();
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBasedOnType();
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCities();
        }
        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAreas();
        }

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBlocks();
        }

        protected void rOtherPhones_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "AddItem")
            {
                DropDownList ddlCode1 = e.Item.FindControl("ddlCode1") as DropDownList;
                TextBox txtPhone1 = e.Item.FindControl("txtPhone1") as TextBox;

                if (ddlCode1 != null && txtPhone1 != null)
                {
                    string code = ddlCode1.SelectedValue;
                    string phone = txtPhone1.Text;

                    if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(phone))
                    {
                        var phoneList = GetPhoneList();
                        phoneList.Add(code + " " + phone);

                        var list = phoneList.Select(p => new
                        {
                            Code = p.Split(' ')[0],
                            Phone = p.Split(' ')[1]
                        });

                        rOtherPhones.DataSource = list;
                        rOtherPhones.DataBind();
                    }
                }
            }
            else if (e.CommandName == "DeleteItem")
            {
                e.Item.Visible = false;
            }
        }
        protected void rOtherPhones_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem
                    || e.Item.ItemType == ListItemType.Footer)
                {
                    DropDownList ddlCode1 = ((DropDownList)e.Item.FindControl("ddlCode1"));

                    ddlCode1.DataValueField = "CCode";
                    ddlCode1.DataTextField = "FullCode";

                    countriesRepository = new CountriesRepository();
                    var data = countriesRepository.Find(c => c.IsActive).ToList();
                    if (data != null)
                    {
                        ddlCode1.DataSource = data.OrderBy(c => c.ISOCode).ToList();
                        ddlCode1.DataBind();
                    }

                    data = null;
                    countriesRepository = null;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate(btnSave.ValidationGroup);
            if (Page.IsValid)
            {
                Save();
            }
        }
        protected override void OnPreRenderComplete(EventArgs e)
        {
            Page.Validate(btnSave.ValidationGroup);

            base.OnPreRenderComplete(e);
        }
        protected void txtPACI_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPACI.Text))
            {
                string token = Convert.ToString(Session["Tocken"]);
                if (string.IsNullOrEmpty(token))
                {
                    return;
                }

                PACIData paciDataObj = PresentHelper.GetPACIData(txtPACI.Text, token);
                if (paciDataObj != null)
                {
                    ddlCountry.SelectedValue = "1";
                    BindCities();
                    ddlCity.SelectedValue = paciDataObj.governorateid.ToString();
                    BindAreas();
                    ddlArea.SelectedValue = paciDataObj.neighborhoodid.ToString();
                    BindBlocks();
                    string block = Culture.ToLower() == "ar-kw" ? paciDataObj.blockarabic : paciDataObj.blockenglish;
                    ddlBlock.ClearSelection();
                    ddlBlock.Items.FindByText(block).Selected = true;
                    txtStreet.Text = Culture.ToLower() == "ar-kw" ? paciDataObj.streetarabic : paciDataObj.streetenglish;
                    if (!string.IsNullOrEmpty(Convert.ToString(paciDataObj.lat)) && !string.IsNullOrEmpty(Convert.ToString(paciDataObj.lon)))
                    {
                        txtLocation.Text = paciDataObj.lat + ", " + paciDataObj.lon;
                        hfLatitude.Value = Convert.ToString(paciDataObj.lat);
                        hfLongitude.Value = Convert.ToString(paciDataObj.lon);
                    }
                }
            }
        }
        #endregion
    }
}