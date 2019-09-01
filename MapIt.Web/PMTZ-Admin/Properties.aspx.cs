using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Drawing;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;
using System.Threading;

namespace MapIt.Web.Admin
{
    public partial class Properties : System.Web.UI.Page
    {
        #region Variables

        PropertiesRepository propertiesRepository;
        PropertyPhotosRepository propertyPhotosRepository;
        UsersRepository usersRepository;
        PurposesRepository purposesRepository;
        PropertyTypesRepository propertyTypesRepository;
        CountriesRepository countriesRepository;
        CitiesRepository citiesRepository;
        AreasRepository areasRepository;
        BlocksRepository blocksRepository;
        PropertySettingsRepository propertySettingsRepository;
        FeaturesRepository featuresRepository;
        ComponentsRepository componentsRepository;

        long pushPropertyId;
        string pushMessageEN;
        string pushMessageAR;

        #endregion Variables

        #region Properties

        public Int64? RecordId
        {
            get
            {
                Int64 id = 0;

                if (ViewState["RecordId"] != null && Int64.TryParse(ViewState["RecordId"].ToString(), out id))
                    return id;

                return null;
            }
            set
            {
                ViewState["RecordId"] = value;
            }
        }

        public bool Search
        {
            get
            {
                bool s = false;
                if (ViewState["Search"] != null && bool.TryParse(ViewState["Search"].ToString(), out s))
                    return s;
                return false;
            }
            set
            {
                ViewState["Search"] = value;
            }
        }

        public long? SearchUser
        {
            get
            {
                long t = 0;
                if (ViewState["SearchUser"] != null && long.TryParse(ViewState["SearchUser"].ToString(), out t))
                    return t;
                return null;
            }
            set
            {
                ViewState["SearchUser"] = value;
            }
        }

        public int? SearchPurpose
        {
            get
            {
                int t = 0;
                if (ViewState["SearchPurpose"] != null && int.TryParse(ViewState["SearchPurpose"].ToString(), out t))
                    return t;
                return null;
            }
            set
            {
                ViewState["SearchPurpose"] = value;
            }
        }

        public int? SearchType
        {
            get
            {
                int t = 0;
                if (ViewState["SearchType"] != null && int.TryParse(ViewState["SearchType"].ToString(), out t))
                    return t;
                return null;
            }
            set
            {
                ViewState["SearchType"] = value;
            }
        }

        public int? SearchCountry
        {
            get
            {
                int t = 0;
                if (ViewState["SearchCountry"] != null && int.TryParse(ViewState["SearchCountry"].ToString(), out t))
                    return t;
                return null;
            }
            set
            {
                ViewState["SearchCountry"] = value;
            }
        }

        public string SortDirection
        {
            get
            {
                if (ViewState["SortDirection"] != null)
                    return ViewState["SortDirection"].ToString();

                return string.Empty;
            }
            set
            {
                ViewState["SortDirection"] = value;
            }
        }

        public string SortExpression
        {
            get
            {
                if (ViewState["SortExpression"] != null)
                    return ViewState["SortExpression"].ToString();

                return string.Empty;
            }
            set
            {
                ViewState["SortExpression"] = value;
            }
        }

        #endregion

        #region Methods

        void BindPurposes()
        {
            try
            {
                ddlSearchPurpose.DataValueField = ddlPurpose.DataValueField = "Id";
                ddlSearchPurpose.DataTextField = ddlPurpose.DataTextField = "TitleEN";

                purposesRepository = new PurposesRepository();
                var data = purposesRepository.GetAll().ToList();
                if (data != null)
                {
                    ddlSearchPurpose.DataSource = data;
                    ddlSearchPurpose.DataBind();

                    ddlPurpose.DataSource = data;
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
                ddlSearchType.DataValueField = ddlType.DataValueField = "Id";
                ddlSearchType.DataTextField = ddlType.DataTextField = "TitleEN";

                propertyTypesRepository = new PropertyTypesRepository();
                var data = propertyTypesRepository.GetAll().OrderBy(c => c.Ordering).ToList();
                if (data != null)
                {
                    ddlSearchType.DataSource = data;
                    ddlSearchType.DataBind();

                    ddlType.DataSource = data;
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
                ddlCountry.DataValueField = ddlSearchCountry.DataValueField = "Id";
                ddlCountry.DataTextField = ddlSearchCountry.DataTextField = "TitleEN";

                countriesRepository = new CountriesRepository();
                var data = countriesRepository.Find(c => c.IsActive).ToList();
                if (data != null)
                {
                    ddlSearchCountry.DataSource = data.OrderBy(c => c.TitleEN).ToList();
                    ddlSearchCountry.DataBind();

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
                ddlCity.DataTextField = "TitleEN";

                for (int i = ddlCity.Items.Count - 1; i > 0; i--)
                {
                    ddlCity.Items.RemoveAt(i);
                }

                int? countryId = ParseHelper.GetInt(ddlCountry.SelectedValue);
                if (countryId.HasValue)
                {
                    citiesRepository = new CitiesRepository();
                    var data = citiesRepository.Find(c => c.CountryId == countryId.Value).ToList();
                    if (data != null && data.Count > 0)
                    {
                        ddlCity.DataSource = data;
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
                ddlArea.DataTextField = "TitleEN";

                for (int i = ddlArea.Items.Count - 1; i > 0; i--)
                {
                    ddlArea.Items.RemoveAt(i);
                }

                int? cityId = ParseHelper.GetInt(ddlCity.SelectedValue);
                if (cityId.HasValue)
                {
                    areasRepository = new AreasRepository();
                    var data = areasRepository.Find(a => a.CityId == cityId.Value).ToList();
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
                ddlBlock.DataTextField = "TitleEN";

                for (int i = ddlBlock.Items.Count - 1; i > 0; i--)
                {
                    ddlBlock.Items.RemoveAt(i);
                }

                int? areaId = ParseHelper.GetInt(ddlArea.SelectedValue);
                if (areaId.HasValue)
                {
                    blocksRepository = new BlocksRepository();
                    var data = blocksRepository.Find(b => b.AreaId == areaId.Value).ToList();
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

        void LoadData()
        {
            try
            {
                propertiesRepository = new PropertiesRepository();
                IQueryable<MapIt.Data.Property> list;

                if (Search)
                {
                    list = propertiesRepository.Search(null, SearchUser, SearchPurpose, SearchType, SearchCountry, null, null, null, null, null, null, null,
                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
                }
                else
                {
                    list = propertiesRepository.GetAll().AsQueryable();
                }

                if (list != null && list.Count() > 0)
                {
                    list = list.OrderByDescending(p => p.Id);

                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        list = SortHelper.SortList<MapIt.Data.Property>(list, SortExpression, SortDirection);
                    }

                    //if (Search && (SearchUser != null || SearchPurpose != null || SearchType != null || SearchCountry != null))
                    //{
                    gvPropertiesExcel.DataSource = list;
                    gvPropertiesExcel.DataBind();
                    //}

                    int count = list.Count();
                    list = list.Skip((AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize).Take(AspNetPager1.PageSize);

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    //pds.DataSource = list.ToList();
                    gvProperties.DataSource = list.ToList();
                    gvProperties.DataBind();
                    AspNetPager1.RecordCount = count;
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvPropertiesExcel.DataSource = new List<Property>();
                    gvPropertiesExcel.DataBind();

                    gvProperties.DataSource = new List<Property>();
                    gvProperties.DataBind();
                    AspNetPager1.Visible = false;
                }

                // Srearch criteria
                lblSearchUser.Text = !string.IsNullOrEmpty(txtSearchUser.Text) ? txtSearchUser.Text : "All Users";
                lblSearchPurpose.Text = ddlSearchPurpose.SelectedItem.Text;
                lblSearchType.Text = ddlSearchType.SelectedItem.Text;
                lblSearchCountry.Text = ddlSearchCountry.SelectedItem.Text;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }
        void LoadImages(List<PropertyPhoto> imagesList)
        {
            try
            {
                if (imagesList == null)
                {
                    imagesList = new List<PropertyPhoto>();
                }

                int currentLength = imagesList.Count;
                int remainingLength = 12 - currentLength;

                for (int i = 0; i < remainingLength; i++)
                {
                    imagesList.Add(new PropertyPhoto { Id = 0, Photo = AppSettings.PropertyDefaultImage.Trim() });
                }

                rImages.DataSource = imagesList;
                rImages.DataBind();
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        public void ClearSearch()
        {
            ddlSearchPurpose.SelectedIndex = ddlSearchType.SelectedIndex = ddlSearchCountry.SelectedIndex = 0;
            SearchUser = SearchPurpose = SearchType = SearchCountry = null;
            txtSearchUser.Text = hfSUserId.Value = string.Empty;
            Search = false;
            AspNetPager1.CurrentPageIndex = 0;
        }

        public void ClearControls()
        {
            ddlPurpose.SelectedIndex = ddlType.SelectedIndex = ddlCountry.SelectedIndex = 0;
            txtUser.Text = hfUserId.Value = txtDetails.Text = txtViewersCount.Text = string.Empty;
            txtViewersCount.Text = "0";
            chkActive.Checked = true;
            chkSendPush.Checked = false;
            gvProperties.SelectedIndex = -1;
            LoadImages(null);
            RecordId = null;

            // load other phones repeater
            var list = new List<string>().Select(p => new
            {
                Code = string.Empty,
                Phone = string.Empty
            });

            rOtherPhones.DataSource = list;
            rOtherPhones.DataBind();
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

        List<PropertyPhoto> GetPhotoList()
        {
            try
            {
                var photoList = new List<PropertyPhoto>();
                for (int i = 0; i < rImages.Items.Count; i++)
                {
                    string photo = string.Empty;
                    FileUpload fuPhoto = rImages.Items[i].FindControl("fuPhoto") as FileUpload;
                    HiddenField hfPhoto = rImages.Items[i].FindControl("hfPhoto") as HiddenField;

                    if (fuPhoto != null && hfPhoto != null)
                    {
                        if (fuPhoto.HasFile)
                        {
                            photo = SavePhoto(fuPhoto);
                        }

                        if (!string.IsNullOrEmpty(photo) && hfPhoto != null && !string.IsNullOrEmpty(hfPhoto.Value.Trim()) &&
                            hfPhoto.Value.Trim() != AppSettings.PropertyDefaultImage.Trim())
                        {
                            photo = hfPhoto.Value.Trim();
                        }

                        if (!string.IsNullOrEmpty(photo))
                        {
                            photoList.Add(new PropertyPhoto { Photo = photo });
                        }
                    }
                }

                return photoList;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return new List<PropertyPhoto>();
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

        void DeletePhoto(string photo)
        {
            try
            {
                try
                {
                    File.Delete(Server.MapPath(AppSettings.PropertyPhotos + photo));
                    File.Delete(Server.MapPath(AppSettings.PropertyWMPhotos + photo));
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(ex);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
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

        void DoWork()
        {
            AppPushs.Push((int)AppEnums.NotifTypes.Property, null, null, pushPropertyId, null, null, pushMessageEN, pushMessageAR);
        }

        void Add()
        {
            try
            {
                int? purposeId = ParseHelper.GetInt(ddlPurpose.SelectedValue);
                if (!purposeId.HasValue)
                {
                    PresentHelper.ShowScriptMessage("Select Purpose");
                    return;
                }

                int? typeId = ParseHelper.GetInt(ddlType.SelectedValue);
                if (!typeId.HasValue)
                {
                    PresentHelper.ShowScriptMessage("Select Type");
                    return;
                }

                int? countryId = ParseHelper.GetInt(ddlCountry.SelectedValue);
                if (!countryId.HasValue)
                {
                    PresentHelper.ShowScriptMessage("Select Country");
                    return;
                }

                int? cityId = ParseHelper.GetInt(ddlCity.SelectedValue);

                int? areaId = ParseHelper.GetInt(ddlArea.SelectedValue);

                int? blockId = ParseHelper.GetInt(ddlBlock.SelectedValue);

                var phoneList = GetPhoneList();
                var photoList = GetPhotoList();
                var componentList = GetComponentList();
                var featureList = GetFeatureList();

                var propertyObj = new MapIt.Data.Property();
                propertyObj.UserId = ParseHelper.GetInt64(hfUserId.Value).Value;
                propertyObj.PurposeId = purposeId.Value;
                propertyObj.TypeId = typeId.Value;
                propertyObj.CountryId = countryId.Value;
                propertyObj.BlockId = blockId;

                if (div_Aream2.Visible)
                    propertyObj.Area = ParseHelper.GetDouble(txtArea.Text);
                else
                    propertyObj.Area = null;
                if (div_BuildingAge.Visible)
                    propertyObj.BuildingAge = ParseHelper.GetInt(txtBuildingAge.Text);
                else
                    propertyObj.BuildingAge = null;
                if (div_MonthlyIncome.Visible)
                    propertyObj.MonthlyIncome = ParseHelper.GetDouble(txtMonthlyIncome.Text);
                else
                    propertyObj.MonthlyIncome = null;
                if (div_SellingPrice.Visible)
                    propertyObj.SellingPrice = ParseHelper.GetDouble(txtSellingPrice.Text);
                else
                    propertyObj.SellingPrice = null;
                if (div_RentPrice.Visible)
                    propertyObj.RentPrice = ParseHelper.GetDouble(txtRentPrice.Text);
                else
                    propertyObj.RentPrice = null;
                if (div_Details.Visible && !string.IsNullOrEmpty(txtDetails.Text))
                    propertyObj.Details = txtDetails.Text;
                else
                    propertyObj.Details = null;

                propertyObj.OtherPhones = string.Join(",", phoneList);
                propertyObj.Longitude = hfLongitude.Value;
                propertyObj.Latitude = hfLatitude.Value;
                propertyObj.ViewersCount = ParseHelper.GetInt(txtViewersCount.Text).Value;
                propertyObj.IsActive = chkActive.Checked;
                propertyObj.AdminAdded = true;
                propertyObj.AddedOn = DateTime.Now;

                //------------- save photos ------------------//
                photoList.ForEach(propertyObj.PropertyPhotos.Add);
                //------------------------------------------------//

                //------------- save components ------------------//
                componentList.ForEach(propertyObj.PropertyComponents.Add);
                //------------------------------------------------//

                //------------- save features ------------------//
                featureList.ForEach(propertyObj.PropertyFeatures.Add);
                //------------------------------------------------//

                propertiesRepository = new PropertiesRepository();
                propertiesRepository.Add(propertyObj);

                if (chkSendPush.Checked)
                {
                    pushPropertyId = propertyObj.Id;
                    pushMessageEN = !string.IsNullOrEmpty(txtNotMsg.Text) ? txtNotMsg.Text : propertyObj.TitleEN;
                    pushMessageAR = !string.IsNullOrEmpty(txtNotMsg.Text) ? txtNotMsg.Text : propertyObj.TitleAR;

                    Thread th = new Thread(DoWork);
                    th.Start();
                }

                ClearControls();
                ClearSearch();
                LoadData();
                PresentHelper.ShowScriptMessage("Add successfully");
                pnlAllRecords.Visible = true;
                pnlRecordDetails.Visible = false;
            }
            catch (Exception ex)
            {
                PresentHelper.ShowScriptMessage("Error in adding");
                LogHelper.LogException(ex);
            }
        }

        void Update(Int64 id)
        {
            try
            {
                int? purposeId = ParseHelper.GetInt(ddlPurpose.SelectedValue);
                if (!purposeId.HasValue)
                {
                    PresentHelper.ShowScriptMessage("Select Purpose");
                    return;
                }

                int? typeId = ParseHelper.GetInt(ddlType.SelectedValue);
                if (!typeId.HasValue)
                {
                    PresentHelper.ShowScriptMessage("Select Type");
                    return;
                }

                int? countryId = ParseHelper.GetInt(ddlCountry.SelectedValue);
                if (!countryId.HasValue)
                {
                    PresentHelper.ShowScriptMessage("Select Country");
                    return;
                }

                int? cityId = ParseHelper.GetInt(ddlCity.SelectedValue);

                int? areaId = ParseHelper.GetInt(ddlArea.SelectedValue);

                int? blockId = ParseHelper.GetInt(ddlBlock.SelectedValue);

                var phoneList = GetPhoneList();
                var photoList = GetPhotoList();
                var componentList = GetComponentList();
                var featureList = GetFeatureList();

                propertiesRepository = new PropertiesRepository();
                var propertyObj = propertiesRepository.GetByKey(id);
                propertyObj.UserId = ParseHelper.GetInt64(hfUserId.Value).Value;
                propertyObj.PurposeId = purposeId.Value;
                propertyObj.TypeId = typeId.Value;
                propertyObj.CountryId = countryId.Value;
                propertyObj.BlockId = blockId;

                if (div_Aream2.Visible)
                    propertyObj.Area = ParseHelper.GetDouble(txtArea.Text);
                else
                    propertyObj.Area = null;
                if (div_BuildingAge.Visible)
                    propertyObj.BuildingAge = ParseHelper.GetInt(txtBuildingAge.Text);
                else
                    propertyObj.BuildingAge = null;
                if (div_MonthlyIncome.Visible)
                    propertyObj.MonthlyIncome = ParseHelper.GetDouble(txtMonthlyIncome.Text);
                else
                    propertyObj.MonthlyIncome = null;
                if (div_SellingPrice.Visible)
                    propertyObj.SellingPrice = ParseHelper.GetDouble(txtSellingPrice.Text);
                else
                    propertyObj.SellingPrice = null;
                if (div_RentPrice.Visible)
                    propertyObj.RentPrice = ParseHelper.GetDouble(txtRentPrice.Text);
                else
                    propertyObj.RentPrice = null;
                if (div_Details.Visible && !string.IsNullOrEmpty(txtDetails.Text))
                    propertyObj.Details = txtDetails.Text;
                else
                    propertyObj.Details = null;

                propertyObj.OtherPhones = string.Join(",", phoneList);
                propertyObj.Longitude = hfLongitude.Value;
                propertyObj.Latitude = hfLatitude.Value;
                propertyObj.ViewersCount = ParseHelper.GetInt(txtViewersCount.Text).Value;
                propertyObj.IsActive = chkActive.Checked;
                propertyObj.ModifiedOn = DateTime.Now;

                //------------- save photos ------------------//
                photoList.ForEach(propertyObj.PropertyPhotos.Add);
                //------------------------------------------------//

                //------------- save components ------------------//
                propertiesRepository.DeletePropertyComponents(propertyObj);
                componentList.ForEach(propertyObj.PropertyComponents.Add);
                //------------------------------------------------//

                //------------- save features ------------------//
                propertiesRepository.DeletePropertyFeatures(propertyObj);
                featureList.ForEach(propertyObj.PropertyFeatures.Add);
                //------------------------------------------------//

                propertiesRepository.Update(propertyObj);

                if (chkSendPush.Checked)
                {
                    pushPropertyId = propertyObj.Id;
                    pushMessageEN = !string.IsNullOrEmpty(txtNotMsg.Text) ? txtNotMsg.Text : propertyObj.TitleEN;
                    pushMessageAR = !string.IsNullOrEmpty(txtNotMsg.Text) ? txtNotMsg.Text : propertyObj.TitleAR;

                    Thread th = new Thread(DoWork);
                    th.Start();
                }

                ClearControls();
                LoadData();
                PresentHelper.ShowScriptMessage("Update successfully");
                pnlAllRecords.Visible = true;
                pnlRecordDetails.Visible = false;
            }
            catch (Exception ex)
            {
                PresentHelper.ShowScriptMessage("Error in updating");
                LogHelper.LogException(ex);
            }
        }

        void Delete(Int64 id)
        {
            try
            {
                propertiesRepository = new PropertiesRepository();
                int result = propertiesRepository.DeleteAnyWay(id);
                switch (result)
                {
                    case -3:
                        PresentHelper.ShowScriptMessage("Error in deleting, as there are many objects related to this record.");
                        break;
                    case 1:
                        ClearControls();
                        LoadData();
                        PresentHelper.ShowScriptMessage("Delete successfully");
                        break;
                }
            }
            catch (Exception ex)
            {
                PresentHelper.ShowScriptMessage("Error in deleting, as there are many objects related to this record.");
                LogHelper.LogException(ex);
            }
        }

        void MultiDelete()
        {
            try
            {
                List<long> lstIds = new List<long>();

                for (int i = 0; i < gvProperties.Rows.Count; i++)
                {
                    long? id = ParseHelper.GetInt64(gvProperties.DataKeys[i]["Id"].ToString());
                    if (!id.HasValue)
                        break;
                    CheckBox chkSelect = gvProperties.Rows[i].FindControl("chkSelect") as CheckBox;
                    if (chkSelect != null && chkSelect.Checked)
                    {
                        lstIds.Add(id.Value);
                    }
                }

                propertiesRepository = new PropertiesRepository();
                foreach (var id in lstIds)
                {
                    propertiesRepository.DeleteAnyWay(id);
                }

                LoadData();
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void DeleteAttachment(long id)
        {
            try
            {
                propertyPhotosRepository = new PropertyPhotosRepository();
                var propertyPhotoObj = propertyPhotosRepository.GetByKey(id);
                if (propertyPhotoObj != null)
                {
                    string photo = propertyPhotoObj.Photo;

                    propertyPhotosRepository.Delete(propertyPhotoObj);

                    if (!string.IsNullOrEmpty(photo))
                    {
                        DeletePhoto(photo);
                    }

                    SetData(RecordId.Value);
                }
            }
            catch (Exception ex)
            {
                PresentHelper.ShowScriptMessage(Resources.Resource.error);
                LogHelper.LogException(ex);
            }
        }

        bool SetData(Int64 id)
        {
            try
            {
                propertiesRepository = new PropertiesRepository();
                var propertyObj = propertiesRepository.GetByKey(id);
                if (propertyObj != null)
                {
                    txtUser.Text = propertyObj.User.Phone;
                    hfUserId.Value = propertyObj.UserId.ToString();
                    ddlPurpose.SelectedValue = propertyObj.PurposeId.ToString();
                    ddlType.SelectedValue = propertyObj.TypeId.ToString();

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
                    else
                    {
                        ddlCountry.SelectedValue = propertyObj.CountryId.ToString();
                    }

                    //ddlCountry.SelectedValue = propertyObj.CountryId.ToString();
                    //ddlCity.SelectedValue = propertyObj.BlockId.HasValue ? propertyObj.Block.Area.CityId.ToString() : string.Empty;
                    //ddlArea.SelectedValue = propertyObj.BlockId.HasValue ? propertyObj.Block.AreaId.ToString() : string.Empty;
                    //ddlBlock.SelectedValue = propertyObj.BlockId.HasValue ? propertyObj.BlockId.ToString() : string.Empty;

                    txtDetails.Text = propertyObj.Details;
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
                        txtArea.Text = propertyObj.Area.HasValue ? propertyObj.Area.Value.ToString() : string.Empty;
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

                    txtViewersCount.Text = propertyObj.ViewersCount.ToString();
                    chkActive.Checked = propertyObj.IsActive;

                    // load photos
                    LoadImages(propertyObj.PropertyPhotos.ToList());

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return false;
            }
        }

        void ExportToExcel()
        {
            try
            {

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

        #endregion Methods

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AdminUserId"] != null && (int)ParseHelper.GetInt(Session["AdminUserId"].ToString()) > 1 &&
                    !(new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminUserId"].ToString()), (int)AppEnums.AdminPages.Properties)))
                {
                    Response.Redirect(".");
                }

                BindPurposes();
                BindTypes();
                BindCountries();
                LoadData();
                pnlAllRecords.Visible = true;
                pnlRecordDetails.Visible = false;
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

        protected void hfUserId_ValueChanged(object sender, EventArgs e)
        {
            usersRepository = new UsersRepository();
            long? userId = ParseHelper.GetInt64(hfUserId.Value);
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            ClearControls();
            pnlAllRecords.Visible = false;
            pnlRecordDetails.Visible = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (RecordId.HasValue)
            {
                Update(RecordId.Value);
            }
            else
            {
                Add();
            }
        }

        protected void gvProperties_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditItem")
            {
                if (SetData(Int64.Parse(e.CommandArgument.ToString())))
                {
                    RecordId = Int64.Parse(e.CommandArgument.ToString());
                    pnlAllRecords.Visible = false;
                    pnlRecordDetails.Visible = true;
                }
            }
            else if (e.CommandName == "DeleteItem")
            {
                Delete(Int64.Parse(e.CommandArgument.ToString()));
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            MultiDelete();
        }

        protected void rImages_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                long? id = ParseHelper.GetInt64(e.CommandArgument.ToString());
                if (id.HasValue)
                {
                    DeleteAttachment(id.Value);
                }
            }
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

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
            pnlAllRecords.Visible = true;
            pnlRecordDetails.Visible = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search = true;
            SearchUser = ParseHelper.GetInt64(hfSUserId.Value);
            SearchPurpose = ParseHelper.GetInt(ddlSearchPurpose.SelectedValue);
            SearchType = ParseHelper.GetInt(ddlSearchType.SelectedValue);
            SearchCountry = ParseHelper.GetInt(ddlSearchCountry.SelectedValue);
            AspNetPager1.CurrentPageIndex = 0;
            LoadData();
        }

        protected void gvProperties_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortDirection = "ASC";
            if (!string.IsNullOrEmpty(SortExpression) && SortExpression == e.SortExpression
                && !string.IsNullOrEmpty(SortDirection) && SortDirection.Trim().ToLower() == "asc")
            {
                sortDirection = "DESC";
            }

            SortDirection = sortDirection;
            SortExpression = e.SortExpression;

            LoadData();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvPropertiesExcel.Rows.Count == 0)
                {
                    PresentHelper.ShowScriptMessage("Please search for the criteria you want to export.");
                    return;
                }

                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "properties" + DateTime.Now + ".xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                //Change the Header Row back to white color
                gvPropertiesExcel.HeaderRow.Style.Add("background-color", "#FFFFFF");

                for (int i = 0; i < gvPropertiesExcel.Rows.Count; i++)
                {
                    GridViewRow row = gvPropertiesExcel.Rows[i];

                    //Change Color back to white
                    row.BackColor = System.Drawing.Color.White;

                    //Apply text style to each Row
                    row.Attributes.Add("class", "textmode");
                }
                gvPropertiesExcel.RenderControl(htmltextwrtter);

                //style to format numbers to string
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(strwritter.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
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

                BasePage obj = new BasePage();
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
        #endregion Events
    }
}