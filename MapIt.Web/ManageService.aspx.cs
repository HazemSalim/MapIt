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
    public partial class ManageService : MapIt.Lib.BasePage
    {
        #region Variables

        ServicesCategoriesRepository servicesCategoriesRepository;
        CountriesRepository countriesRepository;
        CitiesRepository citiesRepository;
        ServicesRepository servicesRepository;
        ServicePhotosRepository servicePhotosRepository;

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

        #endregion

        #region Methods

        void BindCategories()
        {
            try
            {
                ddlCategory.DataValueField = "Id";
                ddlCategory.DataTextField = Resources.Resource.db_title_col;

                servicesCategoriesRepository = new ServicesCategoriesRepository();
                var data = servicesCategoriesRepository.Find(c => c.SubServicesCategories.Count == 0).Select(c => new
                {
                    Id = c.Id,
                    TitleEN = c.ParentId.HasValue ? (c.MainServicesCategory.TitleEN + " -> " + c.TitleEN) : c.TitleEN,
                    TitleAR = c.ParentId.HasValue ? (c.MainServicesCategory.TitleAR + " -> " + c.TitleAR) : c.TitleAR

                });
                data = Culture.ToLower() == "ar-kw" ? data.OrderBy(c => c.TitleAR) : data.OrderBy(c => c.TitleEN);

                if (data != null)
                {
                    ddlCategory.DataSource = data;
                    ddlCategory.DataBind();
                }

                data = null;
                servicesCategoriesRepository = null;
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
                var data = countriesRepository.Find(c => c.IsActive).ToList();
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

                int? countryId = ParseHelper.GetInt(ddlCountry.SelectedValue);
                if (countryId.HasValue)
                {
                    citiesRepository = new CitiesRepository();
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
                rAreas.DataSource = null;
                rAreas.DataBind();

                servicesRepository = new ServicesRepository();
                var areas = from s in servicesRepository.Entities.Areas.Where(a => a.City.CountryId == this.CountryId).ToList()
                            select new
                            {
                                Area = Culture.ToLower() == "ar-kw" ? s.TitleAR : s.TitleEN,
                                AreaId = s.Id,
                                IsCovered = false
                            };

                rAreas.DataSource = areas.OrderBy(a => a.Area).ToList();
                rAreas.DataBind();

                div_area.Visible = true;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }
        void ClearLists()
        {
            try
            {
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

        void SetData()
        {
            try
            {
                if (ServiceId > 0)
                {
                    servicesRepository = new ServicesRepository();
                    var serviceObj = servicesRepository.GetByKey(ServiceId);
                    if (serviceObj != null)
                    {
                        if (serviceObj.UserId != UserId)
                        {
                            Response.Redirect("~/MyServices");
                            return;
                        }

                        ddlType.SelectedValue = serviceObj.IsCompany ? "1" : "2";
                        txtTitle.Text = serviceObj.Title;
                        txtDescription.Text = serviceObj.Description;
                        ddlCountry.SelectedValue = serviceObj.City.CountryId.ToString();
                        BindCities();
                        ddlCity.SelectedValue = serviceObj.CityId.ToString();
                        ddlCategory.SelectedValue = serviceObj.CategoryId.ToString();
                        txtExYears.Text = serviceObj.ExYears.ToString();

                        // Other Phones
                        if (!string.IsNullOrEmpty(serviceObj.OtherPhones))
                        {
                            if (serviceObj.OtherPhones.EndsWith(","))
                            {
                                serviceObj.OtherPhones = serviceObj.OtherPhones.TrimEnd(',');
                            }

                            var otherPhones = serviceObj.OtherPhones.Split(',').Select(p => new
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

                        // load areas
                        var areas = from s in servicesRepository.Entities.Areas.Where(a => a.City.CountryId == serviceObj.City.CountryId)
                                    join p in servicesRepository.Entities.ServiceAreas.Where(ap => ap.ServiceId == ServiceId) on s equals p.Area into op
                                    from p in op.DefaultIfEmpty()
                                    select new { Area = s.TitleEN, AreaId = s.Id, IsCovered = (!p.Equals(null) ? true : false) };
                        div_area.Visible = true;

                        rAreas.DataSource = areas.OrderBy(a => a.Area).ToList();
                        rAreas.DataBind();

                        if (!string.IsNullOrEmpty(serviceObj.Latitude) && !string.IsNullOrEmpty(serviceObj.Longitude))
                        {
                            txtLocation.Text = serviceObj.Latitude.Trim() + ", " + serviceObj.Longitude.Trim();
                            hfLatitude.Value = serviceObj.Latitude.Trim();
                            hfLongitude.Value = serviceObj.Longitude.Trim();
                        }

                        LoadPhotos(serviceObj.ServicePhotos.ToList());
                        Title = Resources.Resource.web_title + " | " + Resources.Resource.edit_service;
                        litTitle.Text = Resources.Resource.edit_service;
                    }
                    else
                    {
                        Response.Redirect("~/MyServices");
                    }
                }
                else
                {
                    LoadPhotos(null);
                    Title = Resources.Resource.web_title + " | " + Resources.Resource.add_new_service;
                    litTitle.Text = Resources.Resource.add_new_service;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                Response.Redirect("~/MyServices");
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

        void LoadPhotos(List<ServicePhoto> photosList)
        {
            try
            {
                if (photosList == null)
                {
                    photosList = new List<ServicePhoto>();
                }

                int currentLength = photosList.Count;
                int remainingLength = 12 - currentLength;
                for (int i = 0; i < remainingLength; i++)
                {
                    photosList.Add(new ServicePhoto { Id = 0, Photo = string.Empty });
                }
                var list = photosList.Select(p => new
                {
                    Photo = String.IsNullOrEmpty(p.Photo) ? string.Empty : p.Photo,
                    FullPhoto = String.IsNullOrEmpty(p.Photo) ? string.Empty : AppSettings.ServiceWMPhotos + p.Photo
                });
                rPhotos.DataSource = list;
                rPhotos.DataBind();
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }
        List<ServiceArea> GetAreasList()
        {
            try
            {
                var areasList = new List<ServiceArea>();
                for (int i = 0; i < rAreas.Items.Count; i++)
                {
                    HiddenField hfAreaId = rAreas.Items[i].FindControl("hfAreaId") as HiddenField;
                    CheckBox chkCovered = rAreas.Items[i].FindControl("chkCovered") as CheckBox;
                    if (hfAreaId != null && chkCovered != null)
                    {
                        int? aId = ParseHelper.GetInt(hfAreaId.Value);
                        bool? covered = ParseHelper.GetBool(chkCovered.Checked);

                        if (aId.HasValue && covered.HasValue && covered.Value)
                        {
                            areasList.Add(new ServiceArea { AreaId = aId.Value });
                        }
                    }
                }

                return areasList;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return new List<ServiceArea>();
            }
        }
        string SavePhoto(FileUpload fuPhoto)
        {
            try
            {
                string imageExt = Path.GetExtension(fuPhoto.FileName);
                string imageName = Guid.NewGuid().ToString();
                string imagePath = AppSettings.ServicePhotos + imageName + imageExt;

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
                image.Save(Server.MapPath(AppSettings.ServiceWMPhotos + imageName));
                PresentHelper.ResizeImage(AppSettings.ServiceWMPhotos + imageName, AppSettings.ServiceWMPhotos + imageName, 300);
                PresentHelper.ResizeImage(AppSettings.ServiceWMPhotos + imageName, AppSettings.ServiceWMPhotos + imageName, 540);
                PresentHelper.ResizeImage(AppSettings.ServiceWMPhotos + imageName, AppSettings.ServiceWMPhotos + imageName, 1080);

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

        MapIt.Data.Service GetData()
        {
            try
            {
                int? categoryId = ParseHelper.GetInt(ddlCategory.SelectedValue);
                if (!categoryId.HasValue || categoryId.Value <= 0)
                {
                    PresentHelper.ShowScriptMessage(Resources.Resource.select_category);
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

                MapIt.Data.Service serviceObj;

                if (ServiceId > 0)
                {
                    servicesRepository = new ServicesRepository();
                    serviceObj = servicesRepository.GetByKey(ServiceId);
                    serviceObj.ModifiedOn = DateTime.Now;
                }
                else
                {
                    serviceObj = new MapIt.Data.Service();
                    serviceObj.ViewersCount = 0;
                    serviceObj.AddedOn = DateTime.Now;
                }

                serviceObj.Title = txtTitle.Text;
                serviceObj.Description = txtDescription.Text;
                serviceObj.UserId = this.UserId;
                serviceObj.CityId = cityId.Value;
                serviceObj.CategoryId = categoryId.Value;
                serviceObj.ExYears = ParseHelper.GetInt(txtExYears.Text).Value;

                //Other Phones
                var phoneList = GetPhoneList();
                serviceObj.OtherPhones = string.Join(",", phoneList);

                serviceObj.Latitude = hfLatitude.Value;
                serviceObj.Longitude = hfLongitude.Value;
                serviceObj.IsCompany = ddlType.SelectedValue == "1" ? true : false;
                serviceObj.IsActive = GeneralSetting.AutoActiveAd;
                serviceObj.AdminAdded = false;

                return serviceObj;
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
                servicesRepository = new ServicesRepository();
                servicePhotosRepository = new ServicePhotosRepository();

                string photos = GetPhotos();
                var photosList = new List<ServicePhoto>();
                if (!string.IsNullOrEmpty(photos))
                {
                    string[] imagesArr = photos.Split(',');
                    foreach (string img in imagesArr)
                    {
                        if (!string.IsNullOrEmpty(img) && img.Trim() != string.Empty)
                            photosList.Add(new ServicePhoto { Photo = img });
                    }
                }

                var serviceObj = GetData();
                var areasList = GetAreasList();
                if (serviceObj != null)
                {
                    if (serviceObj.Id > 0)
                    {
                        servicesRepository.DeleteServiceAreas(serviceObj);
                        areasList.ForEach(serviceObj.ServiceAreas.Add);
                        servicePhotosRepository.Delete(pp => pp.ServiceId == serviceObj.Id);
                        photosList.ForEach(serviceObj.ServicePhotos.Add);
                        servicesRepository.Update(serviceObj);
                    }
                    else
                    {
                        areasList.ForEach(serviceObj.ServiceAreas.Add);
                        photosList.ForEach(serviceObj.ServicePhotos.Add);
                        servicesRepository.Add(serviceObj);
                    }

                    // Saving message ...
                    if (serviceObj.IsActive)
                    {
                        PresentHelper.ShowScriptMessage(Resources.Resource.save_success, "MyServices");
                    }
                    else
                    {
                        PresentHelper.ShowScriptMessage(Resources.Resource.save_success_with_confirm, "MyServices");
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
                BindCategories();
                BindCountries();
                BindAreas();
                ClearLists();

                long id = 0;
                if (Request.QueryString["id"] != null && long.TryParse(Request.QueryString["id"].Trim(), out id))
                {
                    ServiceId = id;
                }

                SetData();
            }
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCities();
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
        #endregion
    }
}