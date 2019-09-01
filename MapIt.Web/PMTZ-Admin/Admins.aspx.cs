using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web.Admin
{
    public partial class Admins : System.Web.UI.Page
    {
        #region Variables

        AdminUsersRepository adminUsersRepository;
        CountriesRepository countriesRepository;

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

        #endregion

        #region Methods

        void LoadData()
        {
            try
            {
                adminUsersRepository = new AdminUsersRepository();
                var usersList = adminUsersRepository.GetAll();
                if (usersList != null && usersList.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        usersList = SortHelper.SortList<AdminUser>(usersList, SortExpression, SortDirection);
                    }

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    pds.DataSource = usersList.ToList();
                    gvUsers.DataSource = pds;
                    gvUsers.DataBind();
                    AspNetPager1.RecordCount = usersList.Count();
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvUsers.DataSource = new List<AdminUser>();
                    gvUsers.DataBind();
                    AspNetPager1.Visible = false;
                }

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
                ddlCode.DataValueField = "CCode";
                ddlCode.DataTextField = "FullCode";

                countriesRepository = new CountriesRepository();
                var data = countriesRepository.Find(c => c.IsActive).OrderBy(c => c.ISOCode).ToList();
                if (data != null)
                {
                    ddlCode.DataSource = data;
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

        public void ClearControls()
        {
            txtFullName.Enabled = true;
            txtUserName.Enabled = true;
            txtFullName.Text = txtPhone.Text = txtEmail.Text = txtUserName.Text =
            txtPassword.Text = txtConfirmPassword.Text = string.Empty;
            txtPassword.Attributes.Add("value", string.Empty);
            txtConfirmPassword.Attributes.Add("value", string.Empty);
            rfvPassword.Enabled = rfvConfirmPassword.Enabled = true;
            chkActive.Enabled = chkActive.Checked = true;
            gvUsers.SelectedIndex = -1;
            RecordId = null;
            div_perm.Visible = true;

            // load permissions
            adminUsersRepository = new AdminUsersRepository();
            var permissions = from s in adminUsersRepository.Entities.AdminPages.ToList()
                              select new { AdminPage = s.Title, AdminPageId = s.Id, IsAccessible = false };

            rPermissions.DataSource = permissions;
            rPermissions.DataBind();
        }

        List<AdminPermission> GetPermissionList()
        {
            try
            {
                var permissionList = new List<AdminPermission>();
                for (int i = 0; i < rPermissions.Items.Count; i++)
                {
                    HiddenField hfAdminPageId = rPermissions.Items[i].FindControl("hfAdminPageId") as HiddenField;
                    CheckBox chkAccessible = rPermissions.Items[i].FindControl("chkAccessible") as CheckBox;
                    if (hfAdminPageId != null && chkAccessible != null)
                    {
                        int? pId = ParseHelper.GetInt(hfAdminPageId.Value);
                        bool? access = ParseHelper.GetBool(chkAccessible.Checked);

                        if (pId.HasValue && access.HasValue)
                        {
                            permissionList.Add(new AdminPermission { AdminPageId = pId.Value, IsAccessible = access.Value });
                        }
                    }
                }

                return permissionList;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return new List<AdminPermission>();
            }
        }

        void Add()
        {
            try
            {
                adminUsersRepository = new AdminUsersRepository();
                var auObj = adminUsersRepository.GetByUserName(txtEmail.Text);
                if (auObj != null)
                {
                    PresentHelper.ShowScriptMessage("This user name already exist");
                    return;
                }

                AdminUser adminUserObj = new AdminUser();
                adminUserObj.FullName = txtFullName.Text;
                adminUserObj.Phone = !string.IsNullOrEmpty(txtPhone.Text) ? ddlCode.SelectedValue + " " + txtPhone.Text : string.Empty;
                adminUserObj.Email = txtEmail.Text;
                adminUserObj.UserName = txtUserName.Text;
                adminUserObj.Password = AuthHelper.GetMD5Hash(txtPassword.Text);
                adminUserObj.IsActive = chkActive.Checked;
                adminUserObj.AddedOn = DateTime.Now;
                adminUserObj.AdminUserId = ParseHelper.GetInt(Session["AdminUserId"].ToString());

                //------------- save permissions ------------------//
                var permissionsList = GetPermissionList();
                permissionsList.ForEach(adminUserObj.AdminPermissions.Add);
                //------------------------------------------------//

                adminUsersRepository.Add(adminUserObj);
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

        void Update(int id)
        {
            try
            {
                adminUsersRepository = new AdminUsersRepository();
                var auObj = adminUsersRepository.GetByUserName(txtEmail.Text, id);
                if (auObj != null)
                {
                    PresentHelper.ShowScriptMessage("This user name already exist");
                    return;
                }

                AdminUser adminUserObj = adminUsersRepository.GetByKey(id);
                adminUserObj.FullName = txtFullName.Text;
                adminUserObj.Phone = !string.IsNullOrEmpty(txtPhone.Text) ? ddlCode.SelectedValue + " " + txtPhone.Text : string.Empty;
                adminUserObj.Email = txtEmail.Text;
                adminUserObj.UserName = txtUserName.Text;

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
                        adminUserObj.Password = AuthHelper.GetMD5Hash(txtPassword.Text);
                    }
                }

                adminUserObj.IsActive = chkActive.Checked;
                adminUserObj.ModifiedOn = DateTime.Now;
                adminUserObj.AdminUserId = ParseHelper.GetInt(Session["AdminUserId"].ToString());

                //------------- delete permissions ------------------//
                adminUsersRepository.DeleteAdminUserPermissions(adminUserObj);
                //------------------------------------------------//

                //------------- save permissions ------------------//
                var permissionsList = GetPermissionList();
                permissionsList.ForEach(adminUserObj.AdminPermissions.Add);
                //------------------------------------------------//

                adminUsersRepository.Update(adminUserObj);

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

        void Delete(int id)
        {
            try
            {
                adminUsersRepository = new AdminUsersRepository();
                adminUsersRepository.Delete(id);
                ClearControls();
                LoadData();
                PresentHelper.ShowScriptMessage("Delete successfully");
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
                adminUsersRepository = new AdminUsersRepository();
                AdminUser adminUserObj = adminUsersRepository.GetByKey(id);
                if (adminUserObj != null)
                {
                    txtFullName.Text = adminUserObj.FullName;

                    if (!string.IsNullOrEmpty(adminUserObj.Phone))
                    {
                        var phone = adminUserObj.Phone.Split(' ');
                        ddlCode.SelectedValue = phone[0];
                        txtPhone.Text = phone[1];
                    }

                    txtEmail.Text = adminUserObj.Email;
                    txtUserName.Text = adminUserObj.UserName;
                    chkActive.Checked = adminUserObj.IsActive;

                    if (id == 1)
                    {
                        txtFullName.Enabled = false;
                        txtUserName.Enabled = false;
                    }

                    rfvPassword.Enabled = rfvConfirmPassword.Enabled = false;

                    // load permissions
                    var permissions = from s in adminUsersRepository.Entities.AdminPages
                                      join p in adminUsersRepository.Entities.AdminPermissions.Where(ap => ap.AdminUserId == id) on s equals p.AdminPage into op
                                      from p in op.DefaultIfEmpty()
                                      select new { AdminPage = s.Title, AdminPageId = s.Id, IsAccessible = (!p.Equals(null) ? p.IsAccessible : false) };

                    rPermissions.DataSource = permissions.ToList();
                    rPermissions.DataBind();

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
                    !(new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminUserId"].ToString()), (int)AppEnums.AdminPages.AdminUsers)))
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

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditItem")
            {
                if (SetData(int.Parse(e.CommandArgument.ToString())))
                {
                    RecordId = int.Parse(e.CommandArgument.ToString());
                    pnlAllRecords.Visible = false;
                    pnlRecordDetails.Visible = true;

                    if (RecordId > 1)
                    {
                        div_perm.Visible = true;
                        chkActive.Enabled = true;
                    }
                    else
                    {
                        div_perm.Visible = false;
                        chkActive.Enabled = false;
                    }
                }
            }
            else if (e.CommandName == "DeleteItem")
            {
                int aUserId = int.Parse(e.CommandArgument.ToString());
                if (aUserId > 1)
                {
                    Delete(aUserId);
                }
                else
                {
                    PresentHelper.ShowScriptMessage("The administrator user can not be deleted.");
                }
            }
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