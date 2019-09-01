using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web
{
    public partial class ResetPassword : MapIt.Lib.BasePage
    {
        #region Variables

        UsersRepository usersRepository;

        #endregion

        #region Properties

        public string Key
        {
            get
            {
                if (ViewState["Key"] != null)
                    return ViewState["Key"].ToString();
                return null;
            }
            set
            {
                ViewState["Key"] = value;
            }
        }

        public string Code
        {
            get
            {
                if (ViewState["Code"] != null)
                    return ViewState["Code"].ToString();
                return null;
            }
            set
            {
                ViewState["Code"] = value;
            }
        }

        public long? UserId
        {
            get
            {
                long id = 0;

                if (ViewState["UserId"] != null && long.TryParse(ViewState["UserId"].ToString(), out id))
                    return id;

                return null;
            }
            set
            {
                ViewState["UserId"] = value;
            }
        }

        public DateTime? ExDateTime
        {
            get
            {
                if (ViewState["ExDateTime"] != null)
                    return DateTime.Parse(ViewState["ExDateTime"].ToString());
                return null;
            }
            set
            {
                ViewState["ExDateTime"] = value;
            }
        }

        #endregion

        #region Methods

        void ValidateKey()
        {
            try
            {
                var codes = Key.Split(new Char[] { 'm', 't' });
                Code = codes[0];
                UserId = ParseHelper.GetInt64(codes[1].ToString());
                ExDateTime = ParseHelper.GetDate(codes[2].ToString(), "yyyyMMddHHmmss", null);

                if (!string.IsNullOrEmpty(Code) && UserId.HasValue && ExDateTime.HasValue && ExDateTime.Value.AddHours(2) > DateTime.Now)
                {
                    usersRepository = new UsersRepository();
                    var userObj = usersRepository.GetByKey(UserId);
                    if (userObj != null && userObj.ActivationCode == Code)
                    {
                        return;
                    }
                    else
                    {
                        PresentHelper.ShowScriptMessage(Resources.Resource.msg_not_valid_key, "/Login");
                    }
                }
                else
                {
                    PresentHelper.ShowScriptMessage(Resources.Resource.msg_not_valid_key, "/Login");
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                Response.Redirect(".");
            }
        }

        void Save()
        {
            try
            {
                usersRepository = new UsersRepository();
                var userObj = usersRepository.GetByKey(UserId);

                if (userObj != null)
                {
                    if (!string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
                        userObj.Password = AuthHelper.GetMD5Hash(txtNewPassword.Text);

                    usersRepository.Update(userObj);
                }
                else
                {
                    PresentHelper.ShowScriptMessage(Resources.Resource.error);
                    return;
                }
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
            if (!IsPostBack)
            {
                if (Request.QueryString["key"] != null && !string.IsNullOrEmpty(Request.QueryString["key"].Trim()))
                {
                    Key = Request.QueryString["key"].Trim();
                    ValidateKey();
                }
                else
                {
                    Response.Redirect(".");
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate(btnSave.ValidationGroup);
            if (Page.IsValid)
            {
                Save();
                Response.Redirect("~/Login", true);
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