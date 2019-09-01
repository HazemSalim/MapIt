using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web.Admin
{
    public partial class CommAds : System.Web.UI.Page
    {
        #region Variables

        CommercialAdsRepository cAdsRepository;
        CountriesRepository countriesRepository;
        CommAdPlacesRepository commAdPlacesRepository;

        #endregion Variables

        #region Properties

        public int? RecordId
        {
            get
            {
                int id = 0;

                if (ViewState["RecordId"] != null && int.TryParse(ViewState["RecordId"].ToString(), out id))
                    return id;

                return null;
            }
            set
            {
                ViewState["RecordId"] = value;
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

        public string OldPhoto
        {
            get
            {
                if (ViewState["OldPhoto"] != null)
                    return ViewState["OldPhoto"].ToString();

                return null;
            }
            set
            {
                ViewState["OldPhoto"] = value;
            }
        }

        #endregion

        #region Methods

        void LoadData()
        {
            try
            {
                cAdsRepository = new CommercialAdsRepository();
                var list = cAdsRepository.GetAll().OrderByDescending(c => c.AddedOn).AsQueryable();
                if (list != null && list.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        list = SortHelper.SortList<CommercialAd>(list, SortExpression, SortDirection);
                    }

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    pds.DataSource = list.ToList();
                    gvAds.DataSource = pds;
                    gvAds.DataBind();
                    AspNetPager1.RecordCount = list.Count();
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvAds.DataSource = new List<CommercialAd>();
                    gvAds.DataBind();
                    AspNetPager1.Visible = false;
                }

            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void BindPlaces()
        {
            try
            {
                ddlPlace.DataValueField = "Id";
                ddlPlace.DataTextField = "Title";

                commAdPlacesRepository = new CommAdPlacesRepository();
                var data = commAdPlacesRepository.GetAll();
                if (data != null && data.Count() > 0)
                {
                    ddlPlace.DataSource = data.ToList();
                    ddlPlace.DataBind();
                }
                commAdPlacesRepository = null;
                data = null;
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
                ddlCountry.DataTextField = "TitleEN";

                countriesRepository = new CountriesRepository();
                var data = countriesRepository.Find(c => c.IsActive).OrderBy(c => c.TitleEN).ToList();
                if (data != null)
                {
                    ddlCountry.DataSource = data;
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

        public void ClearControls()
        {
            txtTitle.Text = txtFromDate.Text = txtToDate.Text = txtLink.Text =
                aOld.HRef = imgOld.Src = string.Empty;
            ddlPlace.SelectedIndex = ddlCountry.SelectedIndex = 0;
            chkActive.Checked = true;
            div_old.Visible = false;
            rfvPhoto.Enabled = true;
            gvAds.SelectedIndex = -1;
            RecordId = null;
            OldPhoto = null;
        }

        string SaveImage()
        {
            try
            {
                string imageExt = Path.GetExtension(fuPhoto.FileName);
                string imageName = Guid.NewGuid().ToString();
                string imagePath = AppSettings.CommAdPhotos + imageName + imageExt;

                FileStream fs = new FileStream(Server.MapPath(imagePath), FileMode.Create, FileAccess.ReadWrite);
                fs.Write(fuPhoto.FileBytes, 0, fuPhoto.FileBytes.Length);
                fs.Close();

                return imageName + imageExt;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return null;
            }
        }

        void DeleteImage(string imageName)
        {
            try
            {
                try
                {
                    File.Delete(Server.MapPath(AppSettings.CommAdPhotos + imageName));
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

        void Add()
        {
            string photo = string.Empty;
            try
            {
                int? place = ParseHelper.GetInt(ddlPlace.SelectedValue);
                if (!place.HasValue)
                {
                    PresentHelper.ShowScriptMessage("Select Place");
                    return;
                }

                if (fuPhoto.HasFile)
                {
                    photo = SaveImage();
                    if (string.IsNullOrEmpty(photo))
                    {
                        PresentHelper.ShowScriptMessage("Error in saving photo");
                        return;
                    }
                }

                var cAdObj = new CommercialAd();
                cAdObj.Title = txtTitle.Text;
                cAdObj.CommAdPlaceId = place.Value;
                cAdObj.CountryId = ParseHelper.GetInt(ddlCountry.SelectedValue);
                cAdObj.FromDate = ParseHelper.GetDate(txtFromDate.Text, "yyyy-MM-dd", null).Value;
                cAdObj.ToDate = ParseHelper.GetDate(txtToDate.Text, "yyyy-MM-dd", null).Value;
                cAdObj.Link = txtLink.Text;
                cAdObj.Photo = photo;
                cAdObj.IsActive = chkActive.Checked;
                cAdObj.AddedOn = DateTime.Now;
                cAdObj.AdminUserId = ParseHelper.GetInt(Session["AdminUserId"].ToString());

                cAdsRepository = new CommercialAdsRepository();
                cAdsRepository.Add(cAdObj);
                ClearControls();
                LoadData();
                PresentHelper.ShowScriptMessage("Add successfully");
                pnlAllRecords.Visible = true;
                pnlRecordDetails.Visible = false;
            }
            catch (Exception ex)
            {
                PresentHelper.ShowScriptMessage("Error in adding");
                LogHelper.LogException(ex);
                if (!string.IsNullOrEmpty(photo))
                    DeleteImage(photo);
            }
        }

        void Update(int id)
        {
            string photo = OldPhoto;
            try
            {
                int? place = ParseHelper.GetInt(ddlPlace.SelectedValue);
                if (!place.HasValue)
                {
                    PresentHelper.ShowScriptMessage("Select Place");
                    return;
                }

                if (fuPhoto.HasFile)
                {
                    photo = SaveImage();
                    if (string.IsNullOrEmpty(photo))
                    {
                        PresentHelper.ShowScriptMessage("Error in saving photo");
                        return;
                    }
                }

                cAdsRepository = new CommercialAdsRepository();
                var cAdObj = cAdsRepository.GetByKey(id);
                cAdObj.Title = txtTitle.Text;
                cAdObj.CommAdPlaceId = place.Value;
                cAdObj.CountryId = ParseHelper.GetInt(ddlCountry.SelectedValue);
                cAdObj.FromDate = ParseHelper.GetDate(txtFromDate.Text, "yyyy-MM-dd", null).Value;
                cAdObj.ToDate = ParseHelper.GetDate(txtToDate.Text, "yyyy-MM-dd", null).Value;
                cAdObj.Link = txtLink.Text;
                cAdObj.Photo = photo;
                cAdObj.IsActive = chkActive.Checked;
                cAdObj.ModifiedOn = DateTime.Now;
                cAdObj.AdminUserId = ParseHelper.GetInt(Session["AdminUserId"].ToString());

                cAdsRepository = new CommercialAdsRepository();
                cAdsRepository.Update(cAdObj);

                if (fuPhoto.HasFile && !string.IsNullOrEmpty(OldPhoto))
                    DeleteImage(OldPhoto);

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
                if (photo != OldPhoto)
                    DeleteImage(photo);
            }
        }

        void Delete(int id)
        {
            try
            {
                cAdsRepository = new CommercialAdsRepository();
                var AdObj = cAdsRepository.GetByKey(id);
                if (AdObj != null)
                {
                    string photo = AdObj.Photo;

                    cAdsRepository.Delete(AdObj);

                    if (!string.IsNullOrEmpty(photo))
                    {
                        DeleteImage(photo);
                    }

                    ClearControls();
                    LoadData();
                    PresentHelper.ShowScriptMessage("Delete successfully");
                }
            }
            catch (Exception ex)
            {
                PresentHelper.ShowScriptMessage("Error in deleting, as there are many objects related to this record.");
                LogHelper.LogException(ex);
            }
        }

        bool SetData(int id)
        {
            try
            {
                cAdsRepository = new CommercialAdsRepository();
                var adObj = cAdsRepository.GetByKey(id);
                if (adObj != null)
                {
                    txtTitle.Text = adObj.Title;
                    ddlPlace.SelectedValue = adObj.CommAdPlaceId.ToString();
                    ddlCountry.SelectedValue = (adObj.CountryId.HasValue) ? adObj.CountryId.Value.ToString() : string.Empty;
                    txtFromDate.Text = adObj.FromDate.ToString("yyyy-MM-dd");
                    txtToDate.Text = adObj.ToDate.ToString("yyyy-MM-dd");
                    txtLink.Text = adObj.Link;
                    chkActive.Checked = adObj.IsActive;

                    if (!string.IsNullOrEmpty(adObj.Photo))
                    {
                        rfvPhoto.Enabled = false;
                        aOld.HRef = imgOld.Src = AppSettings.CommAdPhotos + adObj.Photo;
                        div_old.Visible = true;
                        OldPhoto = adObj.Photo;
                    }

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

        #endregion Methods

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AdminUserId"] != null && (int)ParseHelper.GetInt(Session["AdminUserId"].ToString()) > 1 &&
                    !(new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminUserId"].ToString()), (int)AppEnums.AdminPages.CommAds)))
                {
                    Response.Redirect(".");
                }

                LoadData();
                BindPlaces();
                BindCountries();
                pnlAllRecords.Visible = true;
                pnlRecordDetails.Visible = false;
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            ClearControls();
            pnlAllRecords.Visible = false;
            pnlRecordDetails.Visible = true;
        }

        protected void gvAds_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditItem")
            {
                if (SetData(int.Parse(e.CommandArgument.ToString())))
                {
                    RecordId = int.Parse(e.CommandArgument.ToString());
                    pnlAllRecords.Visible = false;
                    pnlRecordDetails.Visible = true;
                }
            }
            else if (e.CommandName == "DeleteItem")
            {
                Delete(int.Parse(e.CommandArgument.ToString()));
            }
        }

        protected void gvAds_Sorting(object sender, GridViewSortEventArgs e)
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

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
            pnlAllRecords.Visible = true;
            pnlRecordDetails.Visible = false;
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Page.Validate(btnSave.ValidationGroup);

            base.OnPreRenderComplete(e);
        }

        #endregion Events
    }
}