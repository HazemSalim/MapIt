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

namespace MapIt.Web.Admin
{
    public partial class Brokers : System.Web.UI.Page
    {
        #region Variables

        BrokersRepository brokersRepository;
        CountriesRepository countriesRepository;
        CitiesRepository citiesRepository;

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
                    brokersRepository = new BrokersRepository();
                    var areas = from s in brokersRepository.Entities.Areas.Where(a => a.City.CountryId == countryId.Value).ToList()
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

        void LoadData()
        {
            try
            {
                brokersRepository = new BrokersRepository();
                IQueryable<MapIt.Data.Broker> list;

                if (Search)
                {
                    list = brokersRepository.Search(null, SearchCountry, null, null, null, null, SearchKeyWord);
                }
                else
                {
                    list = brokersRepository.GetAll().AsQueryable();
                }

                if (list != null && list.Count() > 0)
                {
                    list = list.OrderByDescending(p => p.Id);

                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        list = SortHelper.SortList<MapIt.Data.Broker>(list, SortExpression, SortDirection);
                    }

                    gvBrokersExcel.DataSource = list;
                    gvBrokersExcel.DataBind();

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    pds.DataSource = list.ToList();
                    gvBrokers.DataSource = pds;
                    gvBrokers.DataBind();
                    AspNetPager1.RecordCount = list.Count();
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvBrokersExcel.DataSource = new List<Broker>();
                    gvBrokersExcel.DataBind();

                    gvBrokers.DataSource = new List<Broker>();
                    gvBrokers.DataBind();
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
            txtFullName.Text = txtPhone.Text = txtEmail.Text = txtLink.Text = txtDetailsEN.Text = txtDetailsAR.Text = string.Empty;
            chkActive.Checked = true;
            div_old.Visible = false;
            gvBrokers.SelectedIndex = -1;
            RecordId = null;
            OldPhoto = null;
            div_areas.Visible = true;
        }

        List<BrokerArea> GetAreasList()
        {
            try
            {
                var areasList = new List<BrokerArea>();
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
                            areasList.Add(new BrokerArea { AreaId = aId.Value });
                        }
                    }
                }

                return areasList;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return new List<BrokerArea>();
            }
        }

        string SavePhoto()
        {
            try
            {
                string imageExt = Path.GetExtension(fuPhoto.FileName);
                string imageName = Guid.NewGuid().ToString();
                string imagePath = AppSettings.BrokerPhotos + imageName + imageExt;

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
                    File.Delete(Server.MapPath(AppSettings.BrokerPhotos + photo));
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

                if (fuPhoto.HasFile)
                {
                    photo = SavePhoto();
                    if (string.IsNullOrEmpty(photo))
                    {
                        PresentHelper.ShowScriptMessage("Error in saving photo");
                        return;
                    }
                }

                brokersRepository = new BrokersRepository();
                var brokerObj = new MapIt.Data.Broker();

                brokerObj.FullName = txtFullName.Text;
                brokerObj.CityId = cityId.Value;
                brokerObj.Phone = ddlCode.SelectedValue + " " + txtPhone.Text;
                brokerObj.Email = txtEmail.Text;
                brokerObj.Link = txtLink.Text;
                brokerObj.DetailsEN = txtDetailsEN.Text;
                brokerObj.DetailsAR = txtDetailsAR.Text;
                brokerObj.Photo = photo;
                brokerObj.AllAreas = allAreas.Checked;
                brokerObj.IsActive = chkActive.Checked;
                brokerObj.AddedOn = DateTime.Now;

                //------------- save areas ------------------//
                var areasList = GetAreasList();
                areasList.ForEach(brokerObj.BrokerAreas.Add);
                //------------------------------------------//

                brokersRepository.Add(brokerObj);

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

                int? cityId = ParseHelper.GetInt(ddlCity.SelectedValue);
                if (!cityId.HasValue)
                {
                    PresentHelper.ShowScriptMessage("Select City");
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

                brokersRepository = new BrokersRepository();
                var brokerObj = brokersRepository.GetByKey(id);

                brokerObj.FullName = txtFullName.Text;
                brokerObj.CityId = cityId.Value;
                brokerObj.Phone = ddlCode.SelectedValue + " " + txtPhone.Text;
                brokerObj.Email = txtEmail.Text;
                brokerObj.Link = txtLink.Text;
                brokerObj.DetailsEN = txtDetailsEN.Text;
                brokerObj.DetailsAR = txtDetailsAR.Text;
                brokerObj.Photo = photo;
                brokerObj.AllAreas = allAreas.Checked;
                brokerObj.IsActive = chkActive.Checked;
                brokerObj.ModifiedOn = DateTime.Now;

                //------------- delete areas ------------------//
                brokersRepository.DeleteBrokerAreas(brokerObj);
                //---------------------------------------------//

                //------------- save areas ------------------//
                var areasList = GetAreasList();
                areasList.ForEach(brokerObj.BrokerAreas.Add);
                //------------------------------------------//

                brokersRepository.Update(brokerObj);

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
                brokersRepository = new BrokersRepository();
                int result = brokersRepository.DeleteAnyWay(id);
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

        bool SetData(Int32 id)
        {
            try
            {
                brokersRepository = new BrokersRepository();
                var brokerObj = brokersRepository.GetByKey(id);
                if (brokerObj != null)
                {
                    txtFullName.Text = brokerObj.FullName;
                    ddlCountry.SelectedValue = brokerObj.City.CountryId.ToString();
                    BindCities();
                    ddlCity.SelectedValue = brokerObj.CityId.ToString();

                    var phone = brokerObj.Phone.Split(' ');
                    ddlCode.SelectedValue = phone[0];
                    txtPhone.Text = phone[1];

                    txtEmail.Text = brokerObj.Email;
                    txtLink.Text = brokerObj.Link;
                    txtDetailsEN.Text = brokerObj.DetailsEN;
                    txtDetailsAR.Text = brokerObj.DetailsAR;

                    chkActive.Checked = brokerObj.IsActive;

                    if (!string.IsNullOrEmpty(brokerObj.Photo))
                    {
                        aOld.HRef = imgOld.Src = AppSettings.BrokerPhotos + brokerObj.Photo;
                        div_old.Visible = true;
                        OldPhoto = brokerObj.Photo;
                    }
                    else
                    {
                        OldPhoto = null;
                    }

                    // load areas
                    var areas = from s in brokersRepository.Entities.Areas.Where(a => a.City.CountryId == brokerObj.City.CountryId)
                                join p in brokersRepository.Entities.BrokerAreas.Where(ap => ap.BrokerId == id) on s equals p.Area into op
                                from p in op.DefaultIfEmpty()
                                select new { Area = s.TitleEN, AreaId = s.Id, IsCovered = (!p.Equals(null) ? true : false) };

                    rAreas.DataSource = areas.OrderBy(a => a.Area).ToList();
                    rAreas.DataBind();

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
                    !(new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminUserId"].ToString()), (int)AppEnums.AdminPages.Brokers)))
                {
                    Response.Redirect(".");
                }

                BindCountries();
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

        protected void gvBrokers_RowCommand(object sender, GridViewCommandEventArgs e)
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

        protected void gvBrokers_Sorting(object sender, GridViewSortEventArgs e)
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
                if (gvBrokersExcel.Rows.Count == 0)
                {
                    PresentHelper.ShowScriptMessage("Please search for the criteria you want to export.");
                    return;
                }

                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "brokers" + DateTime.Now + ".xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                //Change the Header Row back to white color
                gvBrokersExcel.HeaderRow.Style.Add("background-color", "#FFFFFF");

                for (int i = 0; i < gvBrokersExcel.Rows.Count; i++)
                {
                    GridViewRow row = gvBrokersExcel.Rows[i];

                    //Change Color back to white
                    row.BackColor = System.Drawing.Color.White;

                    //Apply text style to each Row
                    row.Attributes.Add("class", "textmode");
                }
                gvBrokersExcel.RenderControl(htmltextwrtter);

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