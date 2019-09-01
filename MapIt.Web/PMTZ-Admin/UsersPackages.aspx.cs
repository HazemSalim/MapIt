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
    public partial class UsersPackages : System.Web.UI.Page
    {
        #region Variables

        PackagesRepository packagesRepository;

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
                packagesRepository = new PackagesRepository();
                var list = packagesRepository.GetAll();
                if (list != null && list.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        list = SortHelper.SortList<Package>(list, SortExpression, SortDirection);
                    }

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    pds.DataSource = list.ToList();
                    gvPackages.DataSource = pds;
                    gvPackages.DataBind();
                    AspNetPager1.RecordCount = list.Count();
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvPackages.DataSource = new List<Package>();
                    gvPackages.DataBind();
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
            txtTitleEN.Text = txtTitleAR.Text = txtDescriptionEN.Text = txtDescriptionAR.Text = txtPrice.Text = aOld.HRef = imgOld.Src = string.Empty;
            chkActive.Checked = true;
            div_old.Visible = false;
            gvPackages.SelectedIndex = -1;
            RecordId = null;
            OldPhoto = null;
        }

        string SaveImage()
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

        void DeleteImage(string imageName)
        {
            try
            {
                try
                {
                    File.Delete(Server.MapPath(AppSettings.PackagePhotos + imageName));
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
                if (fuPhoto.HasFile)
                {
                    photo = SaveImage();
                    if (string.IsNullOrEmpty(photo))
                    {
                        PresentHelper.ShowScriptMessage("Error in saving photo");
                        return;
                    }
                }

                var packageObj = new Package();
                packageObj.TitleEN = txtTitleEN.Text;
                packageObj.TitleAR = txtTitleAR.Text;
                packageObj.DescriptionEN = txtDescriptionEN.Text;
                packageObj.DescriptionAR = txtDescriptionAR.Text;
                packageObj.Photo = photo;
                packageObj.Price = ParseHelper.GetDouble(txtPrice.Text).Value;
                packageObj.IsActive = chkActive.Checked;
                packageObj.AddedOn = DateTime.Now;
                packageObj.AdminUserId = ParseHelper.GetInt(Session["AdminUserId"].ToString());

                packagesRepository = new PackagesRepository();
                packagesRepository.Add(packageObj);
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
                if (fuPhoto.HasFile)
                {
                    photo = SaveImage();
                    if (string.IsNullOrEmpty(photo))
                    {
                        PresentHelper.ShowScriptMessage("Error in saving photo");
                        return;
                    }
                }

                packagesRepository = new PackagesRepository();
                var packageObj = packagesRepository.GetByKey(id);
                packageObj.TitleEN = txtTitleEN.Text;
                packageObj.TitleAR = txtTitleAR.Text;
                packageObj.DescriptionEN = txtDescriptionEN.Text;
                packageObj.DescriptionAR = txtDescriptionAR.Text;
                packageObj.Photo = photo;
                packageObj.Price = ParseHelper.GetDouble(txtPrice.Text).Value;
                packageObj.IsActive = chkActive.Checked;
                packageObj.ModifiedOn = DateTime.Now;
                packageObj.AdminUserId = ParseHelper.GetInt(Session["AdminUserId"].ToString());

                packagesRepository = new PackagesRepository();
                packagesRepository.Update(packageObj);

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
                packagesRepository = new PackagesRepository();
                var packageObj = packagesRepository.GetByKey(id);
                if (packageObj != null)
                {
                    string photo = packageObj.Photo;

                    packagesRepository.Delete(packageObj);

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
                packagesRepository = new PackagesRepository();
                var packageObj = packagesRepository.GetByKey(id);
                if (packageObj != null)
                {
                    txtTitleEN.Text = packageObj.TitleEN;
                    txtTitleAR.Text = packageObj.TitleAR;
                    txtDescriptionEN.Text = packageObj.DescriptionEN;
                    txtDescriptionAR.Text = packageObj.DescriptionAR;
                    txtPrice.Text = packageObj.Price.ToString();
                    chkActive.Checked = packageObj.IsActive;

                    if (!string.IsNullOrEmpty(packageObj.Photo))
                    {
                        aOld.HRef = imgOld.Src = AppSettings.PackagePhotos + packageObj.Photo;
                        div_old.Visible = true;
                        OldPhoto = packageObj.Photo;
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
                    !(new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminUserId"].ToString()), (int)AppEnums.AdminPages.Packages)))
                {
                    Response.Redirect(".");
                }

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

        protected void gvPackages_RowCommand(object sender, GridViewCommandEventArgs e)
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

        protected void gvPackages_Sorting(object sender, GridViewSortEventArgs e)
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