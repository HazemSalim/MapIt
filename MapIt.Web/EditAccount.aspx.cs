using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using MapIt.Helpers;
using MapIt.Repository;

namespace MapIt.Web
{
    public partial class EditAccount : MapIt.Lib.BasePage
    {
        #region Variables

        UsersRepository usersRepository;
        CountriesRepository countriesRepository;
        UserTypesRepository userTypesRepository;

        #endregion

        #region Properties



        #endregion

        #region Methods

        void BindCountries()
        {
            try
            {
                countriesRepository = new CountriesRepository();
                var list = countriesRepository.Find(c => c.IsActive).ToList();
                list = Culture.ToLower() == "ar-kw" ? list.OrderBy(c => c.TitleAR).ToList() : list.OrderBy(c => c.TitleEN).ToList();

                if (list != null && list.Count > 0)
                {
                    ddlCountry.DataValueField = "Id";
                    ddlCountry.DataTextField = Resources.Resource.db_title_col;

                    ddlCode.DataValueField = "CCode";
                    ddlCode.DataTextField = "FullCode";

                    ddlCountry.DataSource = list;
                    ddlCountry.DataBind();

                    ddlCode.DataSource = list.OrderBy(c => c.ISOCode).ToList();
                    ddlCode.DataBind();
                }
                string country = "Kuwait";
                var defaultcountryobj = countriesRepository.Find(x => x.TitleEN.Trim().ToLower().Contains(country.ToLower())).SingleOrDefault();
                if (defaultcountryobj != null)
                {
                    ddlCountry.SelectedValue = defaultcountryobj.Id.ToString();
                    ddlCode.SelectedValue = defaultcountryobj.CCode.ToString();
                }
                list = null;
                countriesRepository = null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void BindUserTypes()
        {
            try
            {
                userTypesRepository = new UserTypesRepository();
                var list = userTypesRepository.GetAll().ToList();
                list = Culture.ToLower() == "ar-kw" ? list.OrderBy(c => c.TitleAR).ToList() : list.OrderBy(c => c.TitleEN).ToList();

                if (list != null && list.Count > 0)
                {
                    ddlUserTypes.DataValueField = "Id";
                    ddlUserTypes.DataTextField = Resources.Resource.db_title_col;

                    ddlUserTypes.DataSource = list;
                    ddlUserTypes.DataBind();

                    ddlUserTypes.Items.Insert(0, new ListItem(Resources.Resource.select, "0"));
                }
                list = null;
                userTypesRepository = null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void BindUser()
        {
            try
            {
                usersRepository = new UsersRepository();
                var userObj = usersRepository.GetByKey(UserId);
                if (userObj != null)
                {
                    ddlSex.SelectedValue = userObj.Sex.ToString();
                    txtBDate.Text = userObj.BirthDate.HasValue ? userObj.BirthDate.Value.ToString("yyyy-MM-dd") : string.Empty;

                    var phone = userObj.Phone.Split(' ');
                    ddlCode.SelectedValue = phone[0];
                    txtPhone.Text = phone[1];

                    txtEmail.Text = userObj.Email;
                    txtFirstName.Text = userObj.FirstName;
                    txtLastName.Text = userObj.LastName;

                    ddlCountry.SelectedValue = userObj.CountryId > 0 ? userObj.CountryId.ToString() : "0";
                    ddlUserTypes.SelectedValue = userObj.UserTypeID > 0 ? userObj.UserTypeID.ToString() : "0";
                    ddlLanguage.SelectedValue = !string.IsNullOrEmpty(userObj.Lang) ? userObj.Lang.ToString() : "0";
                }
                else
                {
                    Response.Redirect("~/Login?ReturnUrl=" + Request.RawUrl, true);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void Edit()
        {
            try
            {
                usersRepository = new UsersRepository();

                var userObj = usersRepository.GetByKey(UserId);
                if (userObj == null)
                {
                    PresentHelper.ShowScriptMessage(Resources.Resource.error);
                    return;
                }

                userObj.Sex = ParseHelper.GetInt(ddlSex.SelectedValue).Value;
                userObj.BirthDate = !string.IsNullOrEmpty(txtBDate.Text) ? ParseHelper.GetDate(txtBDate.Text, "yyyy-MM-dd", null) : null;
                userObj.Phone = ddlCode.SelectedValue + " " + txtPhone.Text;
                userObj.Email = txtEmail.Text;
                userObj.LastName = txtLastName.Text;
                userObj.FirstName = txtFirstName.Text;


                userObj.Lang = ddlLanguage.SelectedValue != "0" ? ddlLanguage.SelectedValue : "";

                if (ddlCountry.SelectedValue != "0")
                    userObj.CountryId = int.Parse(ddlCountry.SelectedValue);

                if (ddlUserTypes.SelectedValue != "0")
                    userObj.UserTypeID = int.Parse(ddlUserTypes.SelectedValue);
                else
                    userObj.UserTypeID = null;


                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    userObj.Password = AuthHelper.GetMD5Hash(txtPassword.Text);
                }
                userObj.ModifiedOn = DateTime.Now;

                usersRepository.Update(userObj);

                PresentHelper.ShowScriptMessage(Resources.Resource.save_success);
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
                BindCountries();
                BindUserTypes();
                BindUser();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate(btnSave.ValidationGroup);
            if (Page.IsValid)
            {
                Edit();
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