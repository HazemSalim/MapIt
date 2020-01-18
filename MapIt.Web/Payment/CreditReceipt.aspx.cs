using System;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web.Payment
{
    public partial class CreditReceipt : BasePage
    {
        #region Variables

        GeneralSettingsRepository generalSettingsRepository;
        CurrenciesRepository currenciesRepository;
        UserCreditsRepository userCreditsRepository;

        #endregion

        #region Properties

        public string TransNo
        {
            get
            {
                if (ViewState["TransNo"] != null && !string.IsNullOrEmpty(ViewState["TransNo"].ToString().Trim()))
                    return ViewState["TransNo"].ToString().Trim();

                return string.Empty;
            }
            set
            {
                ViewState["TransNo"] = value;
            }
        }

        #endregion

        #region Method

        void LoadData()
        {
            try
            {
                generalSettingsRepository = new GeneralSettingsRepository();
                currenciesRepository = new CurrenciesRepository();
                userCreditsRepository = new UserCreditsRepository();

                var creditObj = userCreditsRepository.First(o => o.TransNo == TransNo);
                if (creditObj != null)
                {
                    //if (User != null && creditObj.UserId != UserId)
                    //{
                    //    Response.Redirect(".");
                    //}

                    litComAddress.Text = generalSettingsRepository.Get().Address;
                    lblComPhone.Text = generalSettingsRepository.Get().Phone;

                    lblId.Text = creditObj.UserId.ToString();
                    lblUser.Text = creditObj.User.FullName;
                    lblPhone.Text = creditObj.User.Phone;
                    lblNo.Text = creditObj.TransNo;
                    lblPackage.Text = creditObj.Package.TitleEN;
                    lblPackage.Text = creditObj.Package.TitleEN;
                    lblPaymentMethod.Text = creditObj.PaymentMethod.TitleEN;
                    lblAmount.Text = creditObj.Amount + " " + creditObj.Currency.SymbolEN; ;
                    lblDate.Text = creditObj.TransOn.ToString("MMMM dd, yyyy hh:mm tt");
                    litTerms.Text = generalSettingsRepository.Get().InvoiceTerms;
                }
                else
                {
                    Response.Redirect(".");
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["trn"] != null && !string.IsNullOrEmpty(Request.QueryString["trn"].Trim()))
                {
                    TransNo = Request.QueryString["trn"].Trim();
                    LoadData();
                }
                else
                {
                    Response.Redirect("..");
                }
            }
        }

        #endregion
    }
}