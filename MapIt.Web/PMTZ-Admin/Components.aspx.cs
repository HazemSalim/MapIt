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
    public partial class Components : System.Web.UI.Page
    {
        #region Variables

        ComponentsRepository componentsRepository;

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
                componentsRepository = new ComponentsRepository();
                var list = componentsRepository.GetAll().OrderBy(c => c.Ordering).AsQueryable();
                if (list != null && list.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        list = SortHelper.SortList<Component>(list, SortExpression, SortDirection);
                    }

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    pds.DataSource = list.ToList();
                    gvComponents.DataSource = pds;
                    gvComponents.DataBind();
                    AspNetPager1.RecordCount = list.Count();
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvComponents.DataSource = new List<Component>();
                    gvComponents.DataBind();
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
            txtTitleEN.Text = txtTitleAR.Text = txtOrdering.Text = string.Empty;
            div_old.Visible = false;
            btnCancel.Visible = false;
            btnSave.Text = "Add new Component";
            gvComponents.SelectedIndex = -1;
            RecordId = null;
            OldPhoto = null;
        }

        string SavePhoto()
        {
            try
            {
                string imageExt = Path.GetExtension(fuPhoto.FileName);
                string imageName = Guid.NewGuid().ToString();
                string imagePath = AppSettings.ComponentPhotos + imageName + imageExt;

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
                    File.Delete(Server.MapPath(AppSettings.ComponentPhotos + photo));
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
                    photo = SavePhoto();
                    if (string.IsNullOrEmpty(photo))
                    {
                        PresentHelper.ShowScriptMessage("Error in saving photo");
                        return;
                    }
                }

                var componentObj = new Component();
                componentObj.TitleEN = txtTitleEN.Text;
                componentObj.TitleAR = txtTitleAR.Text;
                componentObj.Photo = photo;
                componentObj.Ordering = ParseHelper.GetInt(txtOrdering.Text).Value;

                componentsRepository = new ComponentsRepository();
                componentsRepository.Add(componentObj);
                ClearControls();
                LoadData();
                PresentHelper.ShowScriptMessage("Add successfully");
            }
            catch (Exception ex)
            {
                PresentHelper.ShowScriptMessage("Error in adding");
                LogHelper.LogException(ex);
                if (!string.IsNullOrEmpty(photo))
                    DeletePhoto(photo);
            }
        }

        void Update(int id)
        {
            string photo = OldPhoto;
            try
            {
                if (fuPhoto.HasFile)
                {
                    photo = SavePhoto();
                    if (string.IsNullOrEmpty(photo))
                    {
                        PresentHelper.ShowScriptMessage("Error in saving photo");
                        return;
                    }
                }

                componentsRepository = new ComponentsRepository();
                var componentObj = componentsRepository.GetByKey(id);
                componentObj.TitleEN = txtTitleEN.Text;
                componentObj.TitleAR = txtTitleAR.Text;
                componentObj.Photo = photo;
                componentObj.Ordering = ParseHelper.GetInt(txtOrdering.Text).Value;

                componentsRepository = new ComponentsRepository();
                componentsRepository.Update(componentObj);

                if (fuPhoto.HasFile && !string.IsNullOrEmpty(OldPhoto))
                    DeletePhoto(OldPhoto);

                ClearControls();
                LoadData();
                PresentHelper.ShowScriptMessage("Update successfully");
            }
            catch (Exception ex)
            {
                PresentHelper.ShowScriptMessage("Error in updating");
                LogHelper.LogException(ex);
                if (photo != OldPhoto)
                    DeletePhoto(photo);
            }
        }

        void Delete(int id)
        {
            try
            {
                componentsRepository = new ComponentsRepository();
                var componentObj = componentsRepository.GetByKey(id);
                if (componentObj != null)
                {
                    string photo = componentObj.Photo;

                    componentsRepository.Delete(componentObj);

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

        bool SetData(int id)
        {
            try
            {
                componentsRepository = new ComponentsRepository();
                var componentObj = componentsRepository.GetByKey(id);
                if (componentObj != null)
                {
                    txtTitleEN.Text = componentObj.TitleEN;
                    txtTitleAR.Text = componentObj.TitleAR;
                    txtOrdering.Text = componentObj.Ordering.ToString();

                    if (!string.IsNullOrEmpty(componentObj.Photo))
                    {
                        aOld.HRef = imgOld.Src = AppSettings.ComponentPhotos + componentObj.Photo;
                        div_old.Visible = true;
                        OldPhoto = componentObj.Photo;
                    }
                    else
                    {
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

        #endregion Methods

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AdminUserId"] != null && (int)ParseHelper.GetInt(Session["AdminUserId"].ToString()) > 1 &&
                    !(new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminUserId"].ToString()), (int)AppEnums.AdminPages.ProPages)))
                {
                    Response.Redirect(".");
                }

                LoadData();
            }
        }

        protected void gvComponents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditItem")
            {
                int id = 0;
                int index = 0;
                string[] args = e.CommandArgument.ToString().Split(',');
                if (args.Length == 2 && int.TryParse(args[0], out id) && int.TryParse(args[1], out index))
                {
                    if (SetData(id))
                    {
                        gvComponents.SelectedIndex = index;
                        RecordId = id;
                        btnSave.Text = "Update Component";
                        btnCancel.Visible = true;
                    }
                }
            }
            else if (e.CommandName == "DeleteItem")
            {
                Delete(int.Parse(e.CommandArgument.ToString()));
            }
        }

        protected void gvComponents_Sorting(object sender, GridViewSortEventArgs e)
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
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Page.Validate(btnSave.ValidationGroup);

            base.OnPreRenderComplete(e);
        }

        #endregion Events
    }
}