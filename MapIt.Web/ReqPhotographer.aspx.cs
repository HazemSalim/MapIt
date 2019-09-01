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

namespace MapIt.Web
{
    public partial class ReqPhotographer : MapIt.Lib.BasePage
    {
        #region Variables

        PhotographersRepository photographersRepository;

        #endregion

        #region Properties



        #endregion

        #region Methods

        void Save()
        {
            try
            {
                photographersRepository = new PhotographersRepository();

                var pObj = new MapIt.Data.Photographer();

                pObj.FullName = txtFullName.Text;
                pObj.Country = txtCountry.Text;
                pObj.City = txtCity.Text;
                pObj.Phone = txtCode.Text + " " + txtPhone.Text;
                pObj.Email = txtEmail.Text;
                pObj.Details = txtDetails.Text;
                pObj.IsActive = false;
                pObj.AddedOn = DateTime.Now;

                photographersRepository.Add(pObj);

                AppMails.SendNewPhotoRequestToAdmin(pObj.Id);

                PresentHelper.ShowScriptMessage(Resources.Resource.req_save_success);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                PresentHelper.ShowScriptMessage(Resources.Resource.error);
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserId != 0)
            {
                UserId = 0;
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            Page.Validate(btnSend.ValidationGroup);
            if (Page.IsValid)
            {
                Save();
            }
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Page.Validate(btnSend.ValidationGroup);

            base.OnPreRenderComplete(e);
        }

        #endregion
    }
}
