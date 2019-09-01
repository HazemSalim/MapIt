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
    public partial class Offers : System.Web.UI.Page
    {
        #region Variables

        OffersRepository offersRepository;
        CountriesRepository countriesRepository;

        int pushOfferId;
        string pushMessageEN;
        string pushMessageAR;

        #endregion Variables

        #region Properties

        public Int32? RecordId
        {
            get
            {
                Int32 id = 0;

                if (ViewState["RecordId"] != null && Int32.TryParse(ViewState["RecordId"].ToString(), out id))
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

        void BindCountries()
        {
            try
            {
                ddlCountry.DataValueField = ddlSearchCountry.DataValueField = "Id";
                ddlCountry.DataTextField = ddlSearchCountry.DataTextField = "TitleEN";

                ddlCode.DataValueField = "CCode";
                ddlCode.DataTextField = "FullCode";

                countriesRepository = new CountriesRepository();
                var data = countriesRepository.Find(c => c.IsActive).ToList();
                if (data != null)
                {
                    ddlSearchCountry.DataSource = data.OrderBy(c => c.TitleEN).ToList();
                    ddlSearchCountry.DataBind();

                    ddlCountry.DataSource = data.OrderBy(c => c.TitleEN).ToList();
                    ddlCountry.DataBind();

                    ddlCode.DataSource = data.OrderBy(c => c.ISOCode).ToList();
                    ddlCode.DataBind();
                }

                data = null;
                countriesRepository = null;
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
                offersRepository = new OffersRepository();
                IQueryable<MapIt.Data.Offer> list;

                if (Search)
                {
                    list = offersRepository.Search(null, SearchCountry, null, null, null, SearchKeyWord);
                }
                else
                {
                    list = offersRepository.GetAll().AsQueryable();
                }

                if (list != null && list.Count() > 0)
                {
                    list = list.OrderBy(c => c.Ordering.HasValue ? 0 : 1).ThenBy(c => c.Ordering).ThenByDescending(c => c.Id);

                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        list = SortHelper.SortList<MapIt.Data.Offer>(list, SortExpression, SortDirection);
                    }

                    gvOffersExcel.DataSource = list;
                    gvOffersExcel.DataBind();

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    pds.DataSource = list.ToList();
                    gvOffers.DataSource = pds;
                    gvOffers.DataBind();
                    AspNetPager1.RecordCount = list.Count();
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvOffersExcel.DataSource = new List<Offer>();
                    gvOffersExcel.DataBind();

                    gvOffers.DataSource = new List<Offer>();
                    gvOffers.DataBind();
                    AspNetPager1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        public void ClearSearch()
        {
            ddlSearchCountry.SelectedIndex = 0;
            SearchCountry = null;
            txtSearchKeyWord.Text = SearchKeyWord = string.Empty;
            Search = false;
            AspNetPager1.CurrentPageIndex = 0;
        }

        public void ClearControls()
        {
            ddlCountry.SelectedIndex = ddlCode.SelectedIndex = 0;
            txtTitleEN.Text = txtTitleAR.Text = txtDescriptionEN.Text = txtDescriptionAR.Text =
                txtPhone.Text = txtLink.Text = txtViewersCount.Text = txtOrdering.Text = string.Empty;
            txtViewersCount.Text = "0";
            chkActive.Checked = true;
            div_old.Visible = false;
            rfvPhoto.Enabled = true;
            gvOffers.SelectedIndex = -1;
            RecordId = null;
            OldPhoto = null;
        }

        string SavePhoto()
        {
            try
            {
                string imageExt = Path.GetExtension(fuPhoto.FileName);
                string imageName = Guid.NewGuid().ToString();
                string imagePath = AppSettings.OfferPhotos + imageName + imageExt;

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

        void DeletePhoto(string photo)
        {
            try
            {
                try
                {
                    File.Delete(Server.MapPath(AppSettings.OfferPhotos + photo));
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

        void DoWork()
        {
            AppPushs.Push((int)AppEnums.NotifTypes.Offer, null, null, null, null, pushOfferId, pushMessageEN, pushMessageAR);
        }

        void Add()
        {
            string photo = string.Empty;
            try
            {
                int? countryId = ParseHelper.GetInt(ddlCountry.SelectedValue);
                if (!countryId.HasValue)
                {
                    PresentHelper.ShowScriptMessage("Select Country");
                    return;
                }

                if (fuPhoto.HasFile)
                {
                    photo = SavePhoto();
                    if (string.IsNullOrEmpty(photo))
                    {
                        PresentHelper.ShowScriptMessage("Error in saving photo");
                        return;
                    }
                }

                offersRepository = new OffersRepository();
                var offerObj = new MapIt.Data.Offer();

                offerObj.TitleEN = txtTitleEN.Text;
                offerObj.TitleAR = txtTitleAR.Text;
                offerObj.DescriptionEN = txtDescriptionEN.Text;
                offerObj.DescriptionAR = txtDescriptionAR.Text;
                offerObj.CountryId = countryId.Value;
                offerObj.Phone = ddlCode.SelectedValue + " " + txtPhone.Text;
                offerObj.Link = txtLink.Text;
                offerObj.Photo = photo;
                offerObj.ViewersCount = ParseHelper.GetInt(txtViewersCount.Text).Value;
                offerObj.IsActive = chkActive.Checked;
                offerObj.Ordering = ParseHelper.GetInt(txtOrdering.Text);
                offerObj.AddedOn = DateTime.Now;

                offersRepository.Add(offerObj);

                if (chkSendPush.Checked)
                {
                    pushOfferId = offerObj.Id;
                    pushMessageEN = offerObj.TitleEN;
                    pushMessageAR = offerObj.TitleAR;

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
                if (!string.IsNullOrEmpty(photo))
                    DeletePhoto(photo);
            }
        }

        void Update(Int32 id)
        {
            string photo = OldPhoto;
            try
            {
                int? countryId = ParseHelper.GetInt(ddlCountry.SelectedValue);
                if (!countryId.HasValue)
                {
                    PresentHelper.ShowScriptMessage("Select Country");
                    return;
                }

                if (fuPhoto.HasFile)
                {
                    photo = SavePhoto();
                    if (string.IsNullOrEmpty(photo))
                    {
                        PresentHelper.ShowScriptMessage("Error in saving photo");
                        return;
                    }
                }

                offersRepository = new OffersRepository();
                var offerObj = offersRepository.GetByKey(id);

                offerObj.TitleEN = txtTitleEN.Text;
                offerObj.TitleAR = txtTitleAR.Text;
                offerObj.DescriptionEN = txtDescriptionEN.Text;
                offerObj.DescriptionAR = txtDescriptionAR.Text;
                offerObj.CountryId = countryId.Value;
                offerObj.Phone = ddlCode.SelectedValue + " " + txtPhone.Text;
                offerObj.Link = txtLink.Text;
                offerObj.Photo = photo;
                offerObj.ViewersCount = ParseHelper.GetInt(txtViewersCount.Text).Value;
                offerObj.IsActive = chkActive.Checked;
                offerObj.Ordering = ParseHelper.GetInt(txtOrdering.Text);
                offerObj.ModifiedOn = DateTime.Now;

                offersRepository.Update(offerObj);

                if (chkSendPush.Checked)
                {
                    pushOfferId = offerObj.Id;
                    pushMessageEN = offerObj.TitleEN;
                    pushMessageAR = offerObj.TitleAR;

                    Thread th = new Thread(DoWork);
                    th.Start();
                }

                if (fuPhoto.HasFile && !string.IsNullOrEmpty(OldPhoto))
                    DeletePhoto(OldPhoto);

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
                    DeletePhoto(photo);
            }
        }

        void Delete(Int32 id)
        {
            try
            {
                offersRepository = new OffersRepository();
                var offerObj = offersRepository.GetByKey(id);
                if (offerObj != null)
                {
                    string photo = offerObj.Photo;

                    offersRepository.DeleteAnyWay(offerObj.Id);

                    if (!string.IsNullOrEmpty(photo))
                    {
                        DeletePhoto(photo);
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

        bool SetData(Int32 id)
        {
            try
            {
                offersRepository = new OffersRepository();
                var offerObj = offersRepository.GetByKey(id);
                if (offerObj != null)
                {
                    txtTitleEN.Text = offerObj.TitleEN;
                    txtTitleAR.Text = offerObj.TitleAR;
                    txtDescriptionEN.Text = offerObj.DescriptionEN;
                    txtDescriptionAR.Text = offerObj.DescriptionAR;
                    ddlCountry.SelectedValue = offerObj.CountryId.ToString();

                    var phone = offerObj.Phone.Split(' ');
                    ddlCode.SelectedValue = phone[0];
                    txtPhone.Text = phone[1];

                    txtLink.Text = offerObj.Link;

                    txtViewersCount.Text = offerObj.ViewersCount.ToString();
                    chkActive.Checked = offerObj.IsActive;
                    txtOrdering.Text = offerObj.Ordering.HasValue ? offerObj.Ordering.ToString() : string.Empty;

                    if (!string.IsNullOrEmpty(offerObj.Photo))
                    {
                        rfvPhoto.Enabled = false;
                        aOld.HRef = imgOld.Src = AppSettings.OfferPhotos + offerObj.Photo;
                        div_old.Visible = true;
                        OldPhoto = offerObj.Photo;
                    }
                    else
                    {
                        rfvPhoto.Enabled = true;
                        OldPhoto = null;
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
                    !(new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminUserId"].ToString()), (int)AppEnums.AdminPages.Offers)))
                {
                    Response.Redirect(".");
                }

                BindCountries();
                LoadData();
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

        protected void gvOffers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditItem")
            {
                if (SetData(Int32.Parse(e.CommandArgument.ToString())))
                {
                    RecordId = Int32.Parse(e.CommandArgument.ToString());
                    pnlAllRecords.Visible = false;
                    pnlRecordDetails.Visible = true;
                }
            }
            else if (e.CommandName == "DeleteItem")
            {
                Delete(Int32.Parse(e.CommandArgument.ToString()));
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
            SearchCountry = ParseHelper.GetInt(ddlSearchCountry.SelectedValue);
            SearchKeyWord = txtSearchKeyWord.Text;
            AspNetPager1.CurrentPageIndex = 0;
            LoadData();
        }

        protected void gvOffers_Sorting(object sender, GridViewSortEventArgs e)
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
                if (gvOffersExcel.Rows.Count == 0)
                {
                    PresentHelper.ShowScriptMessage("Please search for the criteria you want to export.");
                    return;
                }

                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "offers" + DateTime.Now + ".xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                //Change the Header Row back to white color
                gvOffersExcel.HeaderRow.Style.Add("background-color", "#FFFFFF");

                for (int i = 0; i < gvOffersExcel.Rows.Count; i++)
                {
                    GridViewRow row = gvOffersExcel.Rows[i];

                    //Change Color back to white
                    row.BackColor = System.Drawing.Color.White;

                    //Apply text style to each Row
                    row.Attributes.Add("class", "textmode");
                }
                gvOffersExcel.RenderControl(htmltextwrtter);

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