using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Data;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web.Admin
{
    public partial class Users : System.Web.UI.Page
    {
        #region Variables

        UsersRepository usersRepository;
        CountriesRepository countriesRepository;

        #endregion Variables

        #region Properties

        public long? RecordId
        {
            get
            {
                long id = 0;

                if (ViewState["RecordId"] != null && long.TryParse(ViewState["RecordId"].ToString(), out id))
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

        public int? SearchSexStatus
        {
            get
            {
                int t = 0;
                if (ViewState["SearchSexStatus"] != null && int.TryParse(ViewState["SearchSexStatus"].ToString(), out t))
                    return t;
                return null;
            }
            set
            {
                ViewState["SearchSexStatus"] = value;
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

        public int? SearchActiveStatus
        {
            get
            {
                int t = 0;
                if (ViewState["SearchActiveStatus"] != null && int.TryParse(ViewState["SearchActiveStatus"].ToString(), out t))
                    return t;
                return null;
            }
            set
            {
                ViewState["SearchActiveStatus"] = value;
            }
        }

        public DateTime? SearchBDateFrom
        {
            get
            {
                if (ViewState["SearchBDateFrom"] != null)
                    return DateTime.Parse(ViewState["SearchBDateFrom"].ToString());
                return null;
            }
            set
            {
                ViewState["SearchBDateFrom"] = value;
            }
        }

        public DateTime? SearchBDateTo
        {
            get
            {
                if (ViewState["SearchBDateTo"] != null)
                    return DateTime.Parse(ViewState["SearchBDateTo"].ToString());
                return null;
            }
            set
            {
                ViewState["SearchBDateTo"] = value;
            }
        }

        public DateTime? SearchCDateFrom
        {
            get
            {
                if (ViewState["SearchCDateFrom"] != null)
                    return DateTime.Parse(ViewState["SearchCDateFrom"].ToString());
                return null;
            }
            set
            {
                ViewState["SearchCDateFrom"] = value;
            }
        }

        public DateTime? SearchCDateTo
        {
            get
            {
                if (ViewState["SearchCDateTo"] != null)
                    return DateTime.Parse(ViewState["SearchCDateTo"].ToString());
                return null;
            }
            set
            {
                ViewState["SearchCDateTo"] = value;
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
                usersRepository = new UsersRepository();
                IQueryable<MapIt.Data.User> usersList;

                if (Search)
                {
                    usersList = usersRepository.Search(null, SearchSexStatus, SearchCountry, SearchActiveStatus, SearchCDateFrom, SearchCDateTo, SearchKeyWord);
                }
                else
                {
                    usersList = usersRepository.GetAll();
                }

                if (usersList != null && usersList.Count() > 0)
                {
                    usersList = usersList.OrderByDescending(u => u.Id);

                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        usersList = SortHelper.SortList<MapIt.Data.User>(usersList, SortExpression, SortDirection);
                    }

                    //if (Search && (SearchSexStatus != null || SearchCountry != null || SearchActiveStatus != null || SearchCDateFrom != null || SearchCDateTo != null || !string.IsNullOrEmpty(SearchKeyWord)))
                    //{
                    gvUsersExcel.DataSource = usersList;
                    gvUsersExcel.DataBind();
                    //}

                    int count = usersList.Count();
                    usersList = usersList.Skip((AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize).Take(AspNetPager1.PageSize);

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    //pds.DataSource = usersList.ToList();
                    gvUsers.DataSource = usersList.ToList();
                    gvUsers.DataBind();
                    AspNetPager1.RecordCount = count;
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvUsersExcel.DataSource = new List<MapIt.Data.User>();
                    gvUsersExcel.DataBind();

                    gvUsers.DataSource = new List<MapIt.Data.User>();
                    gvUsers.DataBind();
                    AspNetPager1.Visible = false;
                }

                // Srearch criteria
                lblSearchCountry.Text = ddlSearchCountry.SelectedItem.Text;
                lblSearchSexStatus.Text = ddlSearchSexStatus.SelectedItem.Text;
                lblSearchActiveStatus.Text = ddlSearchActiveStatus.SelectedItem.Text;
                lblSearchCDateFrom.Text = !string.IsNullOrEmpty(txtSearchCDateFrom.Text) ? txtSearchCDateFrom.Text : "All Days";
                lblSearchCDateTo.Text = !string.IsNullOrEmpty(txtSearchCDateTo.Text) ? txtSearchCDateTo.Text : "All Days";
                lblSearchKeyword.Text = txtSearchKeyWord.Text;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        public void ClearSearch()
        {
            ddlSearchCountry.SelectedIndex = ddlSearchSexStatus.SelectedIndex = ddlSearchActiveStatus.SelectedIndex = 0;
            txtSearchCDateFrom.Text = txtSearchCDateTo.Text = txtSearchKeyWord.Text = SearchKeyWord = string.Empty;
            SearchCountry = SearchActiveStatus = null;
            SearchCDateFrom = SearchCDateTo = null;
            Search = false;
            AspNetPager1.CurrentPageIndex = 0;
        }

        public void ClearControls()
        {
            txtFirstName.Text = txtLastName.Text = txtBDate.Text = txtPhone.Text = txtEmail.Text = string.Empty;
            txtPassword.Text = txtConfirmPassword.Text = string.Empty;
            txtPassword.Attributes.Add("value", string.Empty);
            txtConfirmPassword.Attributes.Add("value", string.Empty);
            rfvPassword.Enabled = rfvConfirmPassword.Enabled = true;
            ddlSex.SelectedIndex = ddlCountry.SelectedIndex = ddlCode.SelectedIndex = 0;
            chkActive.Checked = true;
            gvUsers.SelectedIndex = -1;
            RecordId = null;
            OldPhoto = null;

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

        string SavePhoto()
        {
            try
            {
                string imageExt = Path.GetExtension(fuPhoto.FileName);
                string imageName = Guid.NewGuid().ToString();
                string imagePath = AppSettings.UserPhotos + imageName + imageExt;

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

        void DeletePhoto(string photoName)
        {
            try
            {
                try
                {
                    File.Delete(Server.MapPath(AppSettings.UserPhotos + photoName));
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

                usersRepository = new UsersRepository();
                var uObj = usersRepository.GetByPhone(txtPhone.Text);
                if (uObj != null)
                {
                    PresentHelper.ShowScriptMessage("This phone already exist");
                    return;
                }

                if (!string.IsNullOrEmpty(txtEmail.Text))
                {
                    uObj = usersRepository.GetByEmail(txtEmail.Text);
                    if (uObj != null)
                    {
                        PresentHelper.ShowScriptMessage("This email already exist");
                        return;
                    }
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

                var phoneList = GetPhoneList();

                var userObj = new MapIt.Data.User();
                userObj.FirstName = txtFirstName.Text;
                userObj.LastName = txtLastName.Text;
                userObj.Sex = ParseHelper.GetInt(ddlSex.SelectedValue);
                userObj.BirthDate = ParseHelper.GetDate(txtBDate.Text, "yyyy-MM-dd", null);
                userObj.CountryId = countryId.Value;
                userObj.Phone = ddlCode.SelectedValue + " " + txtPhone.Text;
                userObj.Email = txtEmail.Text;
                userObj.UserName = string.Empty;
                userObj.Password = AuthHelper.GetMD5Hash(txtPassword.Text);
                userObj.OtherPhones = string.Join(",", phoneList);
                userObj.Photo = photo;
                userObj.ActivationCode = AuthHelper.RandomCode(4);
                userObj.IsActive = chkActive.Checked;
                userObj.IsCanceled = false;
                userObj.AddedOn = DateTime.Now;

                usersRepository.Add(userObj);

                ClearSearch();
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
                    DeletePhoto(photo);
            }
        }

        void Update(long id)
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

                usersRepository = new UsersRepository();
                var uObj = usersRepository.GetByPhone(txtPhone.Text, id);
                if (uObj != null)
                {
                    PresentHelper.ShowScriptMessage("This phone already exist");
                    return;
                }

                if (!string.IsNullOrEmpty(txtEmail.Text))
                {
                    uObj = usersRepository.GetByEmail(txtEmail.Text, id);
                    if (uObj != null)
                    {
                        PresentHelper.ShowScriptMessage("This email already exist");
                        return;
                    }
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

                var userObj = usersRepository.GetByKey(id);

                if (userObj == null)
                {
                    PresentHelper.ShowScriptMessage("Error in updating");
                    return;
                }

                var phoneList = GetPhoneList();

                userObj.FirstName = txtFirstName.Text;
                userObj.LastName = txtLastName.Text;
                userObj.Sex = ParseHelper.GetInt(ddlSex.SelectedValue);
                userObj.BirthDate = ParseHelper.GetDate(txtBDate.Text, "yyyy-MM-dd", null);
                userObj.CountryId = countryId.Value;
                userObj.Phone = ddlCode.SelectedValue + " " + txtPhone.Text;
                userObj.Email = txtEmail.Text;
                userObj.UserName = string.Empty;

                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    if (string.IsNullOrEmpty(txtConfirmPassword.Text))
                    {
                        PresentHelper.ShowScriptMessage("Enter the password and the Confirm Password.");
                        txtPassword.Focus();
                        return;
                    }
                    else
                    {
                        userObj.Password = AuthHelper.GetMD5Hash(txtPassword.Text);
                    }
                }

                userObj.OtherPhones = string.Join(",", phoneList);
                userObj.Photo = photo;
                userObj.IsActive = chkActive.Checked;
                userObj.ModifiedOn = DateTime.Now;
                usersRepository.Update(userObj);

                if (fuPhoto.HasFile && !string.IsNullOrEmpty(OldPhoto))
                    DeletePhoto(OldPhoto);

                ClearSearch();
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

        void Delete(long id)
        {
            try
            {
                usersRepository = new UsersRepository();
                var userObj = usersRepository.GetByKey(id);
                if (userObj != null)
                {
                    string photo = userObj.Photo;

                    usersRepository.Delete(userObj);

                    if (!string.IsNullOrEmpty(photo))
                    {
                        DeletePhoto(photo);
                    }

                    ClearSearch();
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

        bool SetData(long id)
        {
            try
            {
                usersRepository = new UsersRepository();
                var userObj = usersRepository.GetByKey(id);
                if (userObj != null)
                {
                    txtFirstName.Text = userObj.FirstName;
                    txtLastName.Text = userObj.LastName;
                    ddlSex.SelectedValue = userObj.Sex.HasValue ? userObj.Sex.Value.ToString() : string.Empty;
                    txtBDate.Text = userObj.BirthDate.HasValue ? userObj.BirthDate.Value.ToString("yyyy-MM-dd") : string.Empty;
                    ddlCountry.SelectedValue = userObj.CountryId.ToString();

                    var phone = userObj.Phone.Split(' ');
                    ddlCode.SelectedValue = phone[0];
                    txtPhone.Text = phone[1];

                    txtEmail.Text = userObj.Email;

                    rfvPassword.Enabled = rfvConfirmPassword.Enabled = false;

                    chkActive.Checked = userObj.IsActive;

                    if (!string.IsNullOrEmpty(userObj.Photo))
                    {
                        aOld.HRef = imgOld.Src = AppSettings.UserPhotos + userObj.Photo;
                        div_old.Visible = true;
                        OldPhoto = userObj.Photo;
                    }

                    // Other Phones
                    if (!string.IsNullOrEmpty(userObj.OtherPhones))
                    {
                        if (userObj.OtherPhones.EndsWith(","))
                        {
                            userObj.OtherPhones = userObj.OtherPhones.TrimEnd(',');
                        }

                        var otherPhones = userObj.OtherPhones.Split(',').Select(p => new
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
                    !(new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminUserId"].ToString()), (int)AppEnums.AdminPages.Users)))
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
            ClearSearch();
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

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
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
            ClearSearch();
            ClearControls();
            pnlAllRecords.Visible = true;
            pnlRecordDetails.Visible = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search = true;

            SearchSexStatus = ParseHelper.GetInt(ddlSearchSexStatus.SelectedValue);
            SearchCountry = ParseHelper.GetInt(ddlSearchCountry.SelectedValue);
            SearchActiveStatus = ParseHelper.GetInt(ddlSearchActiveStatus.SelectedValue);
            SearchCDateFrom = ParseHelper.GetDate(txtSearchCDateFrom.Text, "yyyy-MM-dd", null);
            SearchCDateTo = ParseHelper.GetDate(txtSearchCDateTo.Text, "yyyy-MM-dd", null);
            SearchKeyWord = txtSearchKeyWord.Text;

            AspNetPager1.CurrentPageIndex = 0;
            LoadData();
        }

        protected void gvUsers_Sorting(object sender, GridViewSortEventArgs e)
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
                if (gvUsersExcel.Rows.Count == 0)
                {
                    PresentHelper.ShowScriptMessage("Please search for the criteria you want to export.");
                    return;
                }

                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "users" + DateTime.Now + ".xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                //Change the Header Row back to white color
                gvUsersExcel.HeaderRow.Style.Add("background-color", "#FFFFFF");

                for (int i = 0; i < gvUsersExcel.Rows.Count; i++)
                {
                    GridViewRow row = gvUsersExcel.Rows[i];

                    //Change Color back to white
                    row.BackColor = System.Drawing.Color.White;

                    //Apply text style to each Row
                    row.Attributes.Add("class", "textmode");
                }
                gvUsersExcel.RenderControl(htmltextwrtter);

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