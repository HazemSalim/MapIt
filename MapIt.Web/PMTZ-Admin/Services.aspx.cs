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
    public partial class Services : System.Web.UI.Page
    {
        #region Variables

        CountriesRepository countriesRepository;
        CitiesRepository citiesRepository;
        ServicesRepository servicesRepository;
        ServicePhotosRepository servicePhotosRepository;
        ServicesCategoriesRepository categoriesRepository;
        UsersRepository usersRepository;

        long pushServiceId;
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

        public int? SearchCategory
        {
            get
            {
                int t = 0;
                if (ViewState["SearchCategory"] != null && int.TryParse(ViewState["SearchCategory"].ToString(), out t))
                    return t;
                return null;
            }
            set
            {
                ViewState["SearchCategory"] = value;
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

        public string SearchKeyWord
        {
            get
            {
                if (ViewState["SearchKeyWord"] != null)
                    return ViewState["SearchKeyWord"].ToString();
                return null;
            }
            set
            {
                ViewState["SearchKeyWord"] = value;
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

        void BindCountries()
        {
            try
            {
                ddlCountry.DataValueField = "Id";
                ddlCountry.DataTextField = "TitleEN";

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
                rAreas.DataSource = null;
                rAreas.DataBind();

                int? countryId = ParseHelper.GetInt(ddlCountry.SelectedValue);
                if (countryId.HasValue)
                {
                    servicesRepository = new ServicesRepository();
                    var areas = from s in servicesRepository.Entities.Areas.Where(a => a.City.CountryId == countryId.Value).ToList()
                                select new { Area = s.TitleEN, AreaId = s.Id, IsCovered = false };

                    rAreas.DataSource = areas.OrderBy(a => a.Area).ToList();
                    rAreas.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void BindCategories()
        {
            try
            {
                ddlSearchCategory.DataValueField = ddlCategory.DataValueField = "Id";
                ddlSearchCategory.DataTextField = ddlCategory.DataTextField = "Title";

                categoriesRepository = new ServicesCategoriesRepository();
                var data = categoriesRepository.Find(c => c.SubServicesCategories.Count == 0).Select(c => new
                {
                    Id = c.Id,
                    Title = c.ParentId.HasValue ? (c.MainServicesCategory.TitleEN + " -> " + c.TitleEN) : c.TitleEN

                }).OrderBy(c => c.Title).ToList();
                if (data != null)
                {
                    ddlSearchCategory.DataSource = data;
                    ddlSearchCategory.DataBind();

                    ddlCategory.DataSource = data;
                    ddlCategory.DataBind();
                }

                data = null;
                categoriesRepository = null;
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
                servicesRepository = new ServicesRepository();
                IQueryable<MapIt.Data.Service> list;

                if (Search)
                {
                    list = servicesRepository.Search(SearchUser, null, null, SearchCategory, null, null, null, SearchKeyWord);
                }
                else
                {
                    list = servicesRepository.GetAll().AsQueryable();
                }

                if (list != null && list.Count() > 0)
                {
                    list = list.OrderBy(c => c.Ordering.HasValue ? 0 : 1).ThenBy(c => c.Ordering).ThenByDescending(c => c.Id);

                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        list = SortHelper.SortList<MapIt.Data.Service>(list, SortExpression, SortDirection);
                    }

                    //if (Search && (SearchUser != null || SearchCategory != null || !string.IsNullOrEmpty(SearchKeyWord)))
                    //{
                    gvServicesExcel.DataSource = list;
                    gvServicesExcel.DataBind();
                    //}

                    int count = list.Count();
                    list = list.Skip((AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize).Take(AspNetPager1.PageSize);

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    //pds.DataSource = list.ToList();
                    gvServices.DataSource = list.ToList();
                    gvServices.DataBind();
                    AspNetPager1.RecordCount = count;
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvServicesExcel.DataSource = new List<Service>();
                    gvServicesExcel.DataBind();

                    gvServices.DataSource = new List<Service>();
                    gvServices.DataBind();
                    AspNetPager1.Visible = false;
                }

                // Srearch criteria
                lblSearchUser.Text = !string.IsNullOrEmpty(txtSearchUser.Text) ? txtSearchUser.Text : "All Users";
                lblSearchCategory.Text = ddlSearchCategory.SelectedItem.Text;
                lblSearchKeyword.Text = txtSearchKeyWord.Text;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void LoadImages(List<ServicePhoto> imagesList)
        {
            try
            {
                if (imagesList == null)
                {
                    imagesList = new List<ServicePhoto>();
                }

                int currentLength = imagesList.Count;
                int remainingLength = 12 - currentLength;

                for (int i = 0; i < remainingLength; i++)
                {
                    imagesList.Add(new ServicePhoto { Id = 0, Photo = AppSettings.ServiceDefaultImage.Trim() });
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
            ddlSearchCategory.SelectedIndex = 0;
            SearchCategory = null;
            SearchUser = null;
            txtSearchUser.Text = hfSUserId.Value = txtSearchKeyWord.Text = SearchKeyWord = string.Empty;
            Search = false;
            AspNetPager1.CurrentPageIndex = 0;
        }

        public void ClearControls()
        {
            ddlCountry.SelectedIndex = ddlCity.SelectedIndex = ddlCategory.SelectedIndex = 0;
            txtUser.Text = hfUserId.Value = txtTitle.Text = txtDescription.Text = txtExYears.Text = txtViewersCount.Text = txtOrdering.Text = string.Empty;
            hfLatitude.Value = hfLongitude.Value = null;
            txtViewersCount.Text = "0";
            chkActive.Checked = true;
            chkSendPush.Checked = false;
            gvServices.SelectedIndex = -1;
            LoadImages(null);
            RecordId = null;
            div_areas.Visible = true;

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

        List<ServicePhoto> GetPhotoList()
        {
            try
            {
                var photoList = new List<ServicePhoto>();
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
                            hfPhoto.Value.Trim() != AppSettings.ServiceDefaultImage.Trim())
                        {
                            photo = hfPhoto.Value.Trim();
                        }

                        if (!string.IsNullOrEmpty(photo))
                        {
                            photoList.Add(new ServicePhoto { Photo = photo });
                        }
                    }
                }

                return photoList;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return new List<ServicePhoto>();
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

        void DeletePhoto(string photo)
        {
            try
            {
                try
                {
                    File.Delete(Server.MapPath(AppSettings.ServicePhotos + photo));
                    File.Delete(Server.MapPath(AppSettings.ServiceWMPhotos + photo));
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
            AppPushs.Push((int)AppEnums.NotifTypes.Service, null, null, null, pushServiceId, null, pushMessageEN, pushMessageAR);
        }

        void Add()
        {
            try
            {
                int? countryId = ParseHelper.GetInt(ddlCountry.SelectedValue);
                if (!countryId.HasValue)
                {
                    PresentHelper.ShowScriptMessage("Select Country");
                    return;
                }

                int? cityId = ParseHelper.GetInt(ddlCity.SelectedValue);
                if (!cityId.HasValue)
                {
                    PresentHelper.ShowScriptMessage("Select City");
                    return;
                }

                int? categoryId = ParseHelper.GetInt(ddlCategory.SelectedValue);
                if (!categoryId.HasValue)
                {
                    PresentHelper.ShowScriptMessage("Select Category");
                    return;
                }

                var phoneList = GetPhoneList();
                var photoList = GetPhotoList();

                servicesRepository = new ServicesRepository();
                var serviceObj = new MapIt.Data.Service();
                serviceObj.Title = txtTitle.Text;
                serviceObj.Description = txtDescription.Text;
                serviceObj.UserId = ParseHelper.GetInt64(hfUserId.Value).Value;
                serviceObj.CityId = cityId.Value;
                serviceObj.CategoryId = categoryId.Value;
                serviceObj.ExYears = ParseHelper.GetInt(txtExYears.Text).Value;
                serviceObj.OtherPhones = string.Join(",", phoneList);
                serviceObj.Longitude = hfLongitude.Value;
                serviceObj.Latitude = hfLatitude.Value;
                serviceObj.ViewersCount = ParseHelper.GetInt(txtViewersCount.Text).Value;
                serviceObj.IsCompany = chkCompany.Checked;
                serviceObj.IsActive = chkActive.Checked;
                serviceObj.Ordering = ParseHelper.GetInt(txtOrdering.Text);
                serviceObj.AdminAdded = true;
                serviceObj.AddedOn = DateTime.Now;

                //------------- save areas ------------------//
                var areasList = GetAreasList();
                areasList.ForEach(serviceObj.ServiceAreas.Add);
                //------------------------------------------//

                //------------- save photos ------------------//
                photoList.ForEach(serviceObj.ServicePhotos.Add);
                //------------------------------------------------//

                servicesRepository.Add(serviceObj);

                if (chkSendPush.Checked)
                {
                    pushServiceId = serviceObj.Id;
                    pushMessageEN = !string.IsNullOrEmpty(txtNotMsg.Text) ? txtNotMsg.Text : serviceObj.Title;
                    pushMessageAR = !string.IsNullOrEmpty(txtNotMsg.Text) ? txtNotMsg.Text : serviceObj.Title;

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
                int? countryId = ParseHelper.GetInt(ddlCountry.SelectedValue);
                if (!countryId.HasValue)
                {
                    PresentHelper.ShowScriptMessage("Select Country");
                    return;
                }

                int? cityId = ParseHelper.GetInt(ddlCity.SelectedValue);
                if (!cityId.HasValue)
                {
                    PresentHelper.ShowScriptMessage("Select City");
                    return;
                }

                int? categoryId = ParseHelper.GetInt(ddlCategory.SelectedValue);
                if (!categoryId.HasValue)
                {
                    PresentHelper.ShowScriptMessage("Select Category");
                    return;
                }

                var phoneList = GetPhoneList();
                var photoList = GetPhotoList();

                servicesRepository = new ServicesRepository();
                var serviceObj = servicesRepository.GetByKey(id);
                serviceObj.Title = txtTitle.Text;
                serviceObj.Description = txtDescription.Text;
                serviceObj.UserId = ParseHelper.GetInt64(hfUserId.Value).Value;
                serviceObj.CityId = cityId.Value;
                serviceObj.CategoryId = categoryId.Value;
                serviceObj.ExYears = ParseHelper.GetInt(txtExYears.Text).Value;
                serviceObj.OtherPhones = string.Join(",", phoneList);
                serviceObj.Longitude = hfLongitude.Value;
                serviceObj.Latitude = hfLatitude.Value;
                serviceObj.ViewersCount = ParseHelper.GetInt(txtViewersCount.Text).Value;
                serviceObj.IsCompany = chkCompany.Checked;
                serviceObj.IsActive = chkActive.Checked;
                serviceObj.Ordering = ParseHelper.GetInt(txtOrdering.Text);
                serviceObj.ModifiedOn = DateTime.Now;

                //------------- delete areas ------------------//
                servicesRepository.DeleteServiceAreas(serviceObj);
                //---------------------------------------------//

                //------------- save areas ------------------//
                var areasList = GetAreasList();
                areasList.ForEach(serviceObj.ServiceAreas.Add);
                //------------------------------------------//

                //------------- save photos ------------------//
                photoList.ForEach(serviceObj.ServicePhotos.Add);
                //------------------------------------------------//

                servicesRepository.Update(serviceObj);

                if (chkSendPush.Checked)
                {
                    pushServiceId = serviceObj.Id;
                    pushMessageEN = !string.IsNullOrEmpty(txtNotMsg.Text) ? txtNotMsg.Text : serviceObj.Title;
                    pushMessageAR = !string.IsNullOrEmpty(txtNotMsg.Text) ? txtNotMsg.Text : serviceObj.Title;

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
                servicesRepository = new ServicesRepository();
                int result = servicesRepository.DeleteAnyWay(id);
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

                for (int i = 0; i < gvServices.Rows.Count; i++)
                {
                    long? id = ParseHelper.GetInt64(gvServices.DataKeys[i]["Id"].ToString());
                    if (!id.HasValue)
                        break;
                    CheckBox chkSelect = gvServices.Rows[i].FindControl("chkSelect") as CheckBox;
                    if (chkSelect != null && chkSelect.Checked)
                    {
                        lstIds.Add(id.Value);
                    }
                }

                servicesRepository = new ServicesRepository();
                foreach (var id in lstIds)
                {
                    servicesRepository.DeleteAnyWay(id);
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
                servicePhotosRepository = new ServicePhotosRepository();
                var servicePhotoObj = servicePhotosRepository.GetByKey(id);
                if (servicePhotoObj != null)
                {
                    string photo = servicePhotoObj.Photo;

                    servicePhotosRepository.Delete(servicePhotoObj);

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
                servicesRepository = new ServicesRepository();
                var serviceObj = servicesRepository.GetByKey(id);
                if (serviceObj != null)
                {
                    txtTitle.Text = serviceObj.Title;
                    txtDescription.Text = serviceObj.Description;
                    txtUser.Text = serviceObj.User.Phone;
                    hfUserId.Value = serviceObj.UserId.ToString();
                    ddlCountry.SelectedValue = serviceObj.City.CountryId.ToString();
                    BindCities();
                    ddlCity.SelectedValue = serviceObj.CityId.ToString();
                    ddlCategory.SelectedValue = serviceObj.CategoryId.ToString();
                    txtExYears.Text = serviceObj.ExYears.ToString();
                    hfLongitude.Value = serviceObj.Longitude;
                    hfLatitude.Value = serviceObj.Latitude;
                    txtViewersCount.Text = serviceObj.ViewersCount.ToString();
                    chkCompany.Checked = serviceObj.IsCompany;
                    chkActive.Checked = serviceObj.IsActive;
                    txtOrdering.Text = serviceObj.Ordering.HasValue ? serviceObj.Ordering.ToString() : string.Empty;

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
                                join p in servicesRepository.Entities.ServiceAreas.Where(ap => ap.ServiceId == id) on s equals p.Area into op
                                from p in op.DefaultIfEmpty()
                                select new { Area = s.TitleEN, AreaId = s.Id, IsCovered = (!p.Equals(null) ? true : false) };

                    rAreas.DataSource = areas.OrderBy(a => a.Area).ToList();
                    rAreas.DataBind();

                    // load photos
                    LoadImages(serviceObj.ServicePhotos.ToList());

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

        #endregion Methods

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AdminUserId"] != null && (int)ParseHelper.GetInt(Session["AdminUserId"].ToString()) > 1 &&
                    !(new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminUserId"].ToString()), (int)AppEnums.AdminPages.Services)))
                {
                    Response.Redirect(".");
                }

                BindCountries();
                BindCategories();
                LoadData();
                pnlAllRecords.Visible = true;
                pnlRecordDetails.Visible = false;
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCities();
            BindAreas();
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

        protected void gvServices_RowCommand(object sender, GridViewCommandEventArgs e)
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
            SearchCategory = ParseHelper.GetInt(ddlSearchCategory.SelectedValue);
            SearchKeyWord = txtSearchKeyWord.Text;
            AspNetPager1.CurrentPageIndex = 0;
            LoadData();
        }

        protected void gvServices_Sorting(object sender, GridViewSortEventArgs e)
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
                if (gvServicesExcel.Rows.Count == 0)
                {
                    PresentHelper.ShowScriptMessage("Please search for the criteria you want to export.");
                    return;
                }

                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "services" + DateTime.Now + ".xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                //Change the Header Row back to white color
                gvServicesExcel.HeaderRow.Style.Add("background-color", "#FFFFFF");

                for (int i = 0; i < gvServicesExcel.Rows.Count; i++)
                {
                    GridViewRow row = gvServicesExcel.Rows[i];

                    //Change Color back to white
                    row.BackColor = System.Drawing.Color.White;

                    //Apply text style to each Row
                    row.Attributes.Add("class", "textmode");
                }
                gvServicesExcel.RenderControl(htmltextwrtter);

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

        #endregion Events
    }
}