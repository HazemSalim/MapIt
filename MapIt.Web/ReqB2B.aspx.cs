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
    public partial class ReqB2B : MapIt.Lib.BasePage
    {
        #region Variables

        MerchantsRepository merchantsRepository;

        #endregion

        #region Properties



        #endregion

        #region Methods

        void Save()
        {
            try
            {
                merchantsRepository = new MerchantsRepository();

                var mObj = new MapIt.Data.Merchant();

                mObj.FullName = txtFullName.Text;
                mObj.Country = txtCountry.Text;
                mObj.City = txtCity.Text;
                mObj.Phone = txtCode.Text + " " + txtPhone.Text;
                mObj.Email = txtEmail.Text;
                mObj.CompanyName = txtComName.Text;
                mObj.Details = txtDetails.Text;
                mObj.IsActive = false;
                mObj.AddedOn = DateTime.Now;

                merchantsRepository.Add(mObj);

                AppMails.SendNewPhotoRequestToAdmin(mObj.Id);

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
