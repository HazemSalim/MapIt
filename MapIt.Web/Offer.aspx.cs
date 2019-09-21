using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web
{
    public partial class Offer : MapIt.Lib.BasePage
    {
        #region Variables

        OffersRepository offersRepository;

        #endregion

        #region Properties

        public int OfferId
        {
            get
            {
                int id = 0;
                if (ViewState["OfferId"] != null && int.TryParse(ViewState["OfferId"].ToString().Trim(), out id))
                    return id;

                return 0;
            }
            set
            {
                ViewState["OfferId"] = value;
            }
        }

        #endregion

        #region Methods

        void LoadData()
        {
            try
            {
                offersRepository = new OffersRepository();

                var offerObj = offersRepository.GetByKey(OfferId);
                if (offerObj != null)
                {
                    if (!offerObj.IsActive)
                    {
                        Response.Redirect(".", false);
                        return;
                    }

                    //Increase Viewers Count
                    offerObj.ViewersCount++;
                    offersRepository.Update(offerObj);

                    Title = Culture.ToLower() == "ar-kw" ? GeneralSetting.TitleAR + " | " + offerObj.TitleAR : GeneralSetting.TitleEN + " | " + offerObj.TitleEN;
                    imgOffer.Alt = litTitle.Text = Culture.ToLower() == "ar-kw" ? offerObj.TitleAR : offerObj.TitleEN;
                    imgOffer.Src = offerObj.FinalPhoto;

                    litViewers.Text = offerObj.ViewersCount.ToString();

                    aWhatsapp.InnerText = aPhone.InnerText = offerObj.Phone;
                    //aWhatsapp.HRef = aPhone.HRef = "tel:" + offerObj.Phone;

                    aLink.InnerText = offerObj.Link;
                    aLink.HRef = offerObj.Link;

                    litDesc.Text = Culture.ToLower() == "ar-kw" ? offerObj.DescriptionAR : offerObj.DescriptionEN;
                }
                else
                {
                    Response.Redirect(".");
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                Response.Redirect(".");
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.Page.RouteData.Values["PageName"] != null)
                {
                    string pageName = this.Page.RouteData.Values["PageName"].ToString();
                    int? id = ParseHelper.GetInt(Regex.Match(pageName, @"(?<=(\D|^))\d+(?=\D*$)"));

                    if (id.HasValue)
                    {
                        OfferId = id.Value;
                        LoadData();
                    }
                    else
                    {
                        Response.Redirect(".");
                    }
                }
                else
                {
                    Response.Redirect(".");
                }
            }
        }

        #endregion
    }
}