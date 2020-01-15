using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web
{
    public partial class BuyCredit : BasePage
    {
        #region Variables

        PackagesRepository packagesRepository;
        UserCreditsRepository userCreditsRepository;
        //PaymentMethodsRepository paymentMethodsRepository;

        #endregion

        #region Properties

        public int PackageId
        {
            get
            {
                int val = 0;
                if (ViewState["PackageId"] != null && int.TryParse(ViewState["PackageId"].ToString(), out val))
                    return val;

                return 0;
            }
            set
            {
                ViewState["PackageId"] = value;
            }
        }

        #endregion

        #region Methods

        void LoadData()
        {
            try
            {
                packagesRepository = new PackagesRepository();
                var list = packagesRepository.Find(p => p.IsActive)
                    .OrderBy(p => p.Price);

                if (list != null && list.Count() > 0)
                {
                    rPackages.DataSource = list;
                    rPackages.DataBind();
                }
                else
                {
                    rPackages.DataSource = new List<object>();
                    rPackages.DataBind();
                }

                list = null;
                packagesRepository = null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void Buy(int packageId, int paymentId)
        {
            try
            {
                packagesRepository = new PackagesRepository();
                var packageObj = packagesRepository.GetByKey(packageId);

                if (packageObj != null)
                {
                    userCreditsRepository = new UserCreditsRepository();
                    var userCreditObj = new UserCredit()
                    {
                        TransNo = string.Empty,
                        UserId = User.Id,
                        PackageId = packageObj.Id,
                        PaymentMethodId = paymentId,
                        CurrencyId = GeneralSetting.DefaultCurrencyId,
                        ExchangeRate = GeneralSetting.DefaultCurrency.ExchangeRate,
                        Amount = packageObj.Price,
                        PaymentStatus = paymentId == (int)AppEnums.PaymentMethods.Free ? (int)AppEnums.PaymentStatus.Paid : (int)AppEnums.PaymentStatus.NotPaid,
                        TransOn = DateTime.Now
                    };

                    userCreditsRepository.Add(userCreditObj);
                    userCreditObj.TransNo = "TRNS" + (userCreditObj.Id).ToString("D6");
                    userCreditsRepository.Update(userCreditObj);

                    if (userCreditObj.PaymentMethodId == (int)AppEnums.PaymentMethods.MyFatoorah)
                    {
                        // ----- Go to My Fatoorah payment ----- //
                        Response.Redirect("~/payment/Buy?trn=" + userCreditObj.TransNo, false);
                    }
                    //else if (userCreditObj.PaymentMethodId == (int)AppEnums.PaymentMethods.Other1)
                    //{
                    //    // ----- Go to Knet payment ----- //
                    //    Response.Redirect("~/knet/buy?trackid=" + userCreditObj.Id + "&total=" + userCreditObj.Amount, false);
                    //}
                    //else if (userCreditObj.PaymentMethodId == (int)AppEnums.PaymentMethods.Other2)
                    //{
                    //    // ----- Go to Cyber Source payment ----- //
                    //    Response.Redirect("~/cybersource/buy?trackid=" + userCreditObj.Id + "&total=" + userCreditObj.Amount, false);
                    //}
                    else
                    {
                        // ----- Credit Confirmation Message ----- //
                        PresentHelper.ShowScriptMessage(Resources.Resource.order_save_success, "/payment/creditreceipt?trans=" + userCreditObj.TransNo);
                    }
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
                if (UserId <= 0)
                {
                    Response.Redirect("~/Login?ReturnUrl=" + Request.RawUrl, true);
                }
                else
                {
                    LoadData();
                }
            }
        }

        protected void rPackages_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "BuyItem")
            {
                int? packageId = ParseHelper.GetInt(e.CommandArgument.ToString());
                if (packageId.HasValue)
                {
                    PackageId = packageId.Value;
                    Buy(PackageId, (int)AppEnums.PaymentMethods.MyFatoorah);
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            LoadData();
            base.OnPreRender(e);
        }

        #endregion
    }
}