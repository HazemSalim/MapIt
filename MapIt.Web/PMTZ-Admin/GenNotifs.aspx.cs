using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;
using System.Threading;
using System.IO;

namespace MapIt.Web.Admin
{
    public partial class GenNotifs : System.Web.UI.Page
    {
        #region Variables

        GenNotifsRepository genNotifsRepository;
        PropertiesRepository propertiesRepository;
        ServicesRepository servicesRepository;


        int pushGNotifId;
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

        void CheckQueryString()
        {
            long propertyID = 0,serviceID=0;
            if (Request.QueryString["pid"] != null && long.TryParse(Request.QueryString["pid"], out propertyID))
            {
                if (propertyID > 0)
                {
                    ddlProperties.SelectedValue = propertyID.ToString();
                    btnAddNew_Click(null, null);
                }
            }
            if (Request.QueryString["sid"] != null && long.TryParse(Request.QueryString["sid"], out serviceID))
            {
                if (serviceID > 0)
                {
                    ddlServices.SelectedValue = serviceID.ToString();
                    btnAddNew_Click(null, null);
                }
            }
        }

        void BindProperties()
        {
            try
            {
                propertiesRepository = new PropertiesRepository();
                List<Data.Property> data = propertiesRepository.Find(c => c.IsActive).ToList();
                if (data != null)
                {
                    ddlProperties.DataSource = data.OrderByDescending(c => c.AddedOn).ToList();
                    ddlProperties.DataBind();
                }

                data = null;
                propertiesRepository = null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }


        void BindServices()
        {
            try
            {
                servicesRepository = new ServicesRepository();
                var data = servicesRepository.Find(c => c.IsActive).ToList();
                if (data != null)
                {
                    ddlServices.DataSource = data.OrderByDescending(c => c.AddedOn).ToList();
                    ddlServices.DataBind();
                }

                data = null;
                servicesRepository = null;
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
                genNotifsRepository = new GenNotifsRepository();
                IQueryable<GenNotif> list;
                list = genNotifsRepository.GetAll().AsQueryable();

                if (list != null && list.Count() > 0)
                {
                    list = list.OrderByDescending(p => p.Id);

                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        list = SortHelper.SortList(list, SortExpression, SortDirection);
                    }

                    PagedDataSource pds = new PagedDataSource
                    {
                        AllowPaging = true,
                        PageSize = AspNetPager1.PageSize,
                        CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1,

                        DataSource = list.ToList()
                    };
                    gvGenNotifs.DataSource = pds;
                    gvGenNotifs.DataBind();
                    AspNetPager1.RecordCount = list.Count();
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvGenNotifs.DataSource = new List<Offer>();
                    gvGenNotifs.DataBind();
                    AspNetPager1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        public void ClearControls()
        {
            txtTitleEN.Text = txtTitleAR.Text = string.Empty;
            gvGenNotifs.SelectedIndex = -1;
            RecordId = null;

            OldPhoto = null;
        }

        string SavePhoto()
        {
            try
            {
                string imageExt = Path.GetExtension(fuPhoto.FileName);
                string imageName = Guid.NewGuid().ToString();
                string imagePath = AppSettings.PackagePhotos + imageName + imageExt;

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
                    File.Delete(Server.MapPath(AppSettings.PackagePhotos + photoName));
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
            int type = (int)AppEnums.NotifTypes.General;

            long? propertyID = null, serviceID = null;
            if (ddlProperties.SelectedValue != "")
            {
                propertyID = long.Parse(ddlProperties.SelectedValue);
                type = (int)AppEnums.NotifTypes.Property;
            }
            else if (ddlServices.SelectedValue != "")
            {
                serviceID = long.Parse(ddlServices.SelectedValue);
                type = (int)AppEnums.NotifTypes.Service;
            }

            string photo = string.Empty;
            if (fuPhoto.HasFile)
            {
                photo = SavePhoto();
                if (string.IsNullOrEmpty(photo))
                {
                    PresentHelper.ShowScriptMessage("Error in saving photo");
                    return;
                }
            }


            
            photo = !string.IsNullOrEmpty(photo) ? AppSettings.WebsiteURL + AppSettings.PackagePhotos + photo : string.Empty;

            AppPushs.Push(type, null, pushGNotifId, propertyID, serviceID, null, pushMessageEN, pushMessageAR, photo);
        }

        void Add()
        {
            try
            {
                genNotifsRepository = new GenNotifsRepository();
                var genNotifObj = new GenNotif
                {
                    TitleEN = txtTitleEN.Text,
                    TitleAR = txtTitleAR.Text,
                    AddedOn = DateTime.Now
                };

                genNotifsRepository.Add(genNotifObj);

                pushGNotifId = genNotifObj.Id;
                pushMessageEN = genNotifObj.TitleEN;
                pushMessageAR = genNotifObj.TitleAR;

                Thread th = new Thread(DoWork);
                th.Start();

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
            }
        }

        void Delete(int id)
        {
            try
            {
                genNotifsRepository = new GenNotifsRepository();
                int result = genNotifsRepository.DeleteAnyWay(id);
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

        #endregion Methods

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AdminUserId"] != null && ParseHelper.GetInt(Session["AdminUserId"].ToString()) > 1 &&
                    !new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminUserId"].ToString()), (int)AppEnums.AdminPages.GenNotifs))
                {
                    Response.Redirect(".");
                }

                BindProperties();
                BindServices();

                LoadData();
                pnlAllRecords.Visible = true;
                pnlRecordDetails.Visible = false;

                CheckQueryString();
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
            Add();
        }

        protected void gvGenNotifs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                Delete(Int32.Parse(e.CommandArgument.ToString()));
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
            pnlAllRecords.Visible = true;
            pnlRecordDetails.Visible = false;
        }

        protected void gvGenNotifs_Sorting(object sender, GridViewSortEventArgs e)
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

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Page.Validate(btnSave.ValidationGroup);

            base.OnPreRenderComplete(e);
        }

        #endregion Events
    }
}