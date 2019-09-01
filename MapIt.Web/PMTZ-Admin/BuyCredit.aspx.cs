using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web.Admin
{
    public partial class BuyCredit : System.Web.UI.Page
    {
        #region Variables

        PackagesRepository packagesRepository;
        UserCreditsRepository userCreditsRepository;

        #endregion

        #region Properties

        long UserId
        {
            get
            {
                long id = 0;
                if (ViewState["UserId"] != null && long.TryParse(ViewState["UserId"].ToString(), out id))
                    return id;
                return 0;
            }
            set
            {
                ViewState["UserId"] = value;
            }
        }

        private User _user = null;
        public User User
        {
            get
            {
                var repository = new UsersRepository();
                _user = repository.GetByKey(UserId);

                return _user;
            }
        }

        private Currency _currency = null;
        public Currency Currency
        {
            get
            {
                var repository = new GeneralSettingsRepository();
                _currency = repository.Get().DefaultCurrency;

                return _currency;
            }
        }

        #endregion

        #region Methods

        void LoadData()
        {
            try
            {
                packagesRepository = new PackagesRepository();
                var list = packagesRepository.Find(p => p.IsActive).OrderBy(p => p.Price);

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

        void Save(int packageId)
        {
            try
            {
                packagesRepository = new PackagesRepository();

                var packageObj = packagesRepository.GetByKey(packageId);

                if (packageObj != null)
                {
                    userCreditsRepository = new UserCreditsRepository();
                    var userCredit = new UserCredit()
                    {
                        TransNo = string.Empty,
                        UserId = User.Id,
                        PackageId = packageObj.Id,
                        PaymentMethodId = (int)AppEnums.PaymentMethods.Cash,
                        CurrencyId = Currency.Id,
                        ExchangeRate = Currency.ExchangeRate,
                        Amount = packageObj.Price,
                        PaymentStatus = (int)AppEnums.PaymentStatus.Paid,
                        TransOn = DateTime.Now
                    };

                    userCreditsRepository.Add(userCredit);
                    userCredit.TransNo = "TRN" + (userCredit.Id).ToString("D6");
                    userCreditsRepository.Update(userCredit);

                    AppMails.SendNewCreditToUser(userCredit.Id);
                    AppMails.SendNewCreditToAdmin(userCredit.Id);

                    PresentHelper.ShowScriptMessage("Credit added successfully.", "Users");
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
                if (Session["AdminUserId"] != null && (int)ParseHelper.GetInt(Session["AdminUserId"].ToString()) > 1 &&
                    !(new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminUserId"].ToString()), (int)AppEnums.AdminPages.Users)))
                {
                    Response.Redirect(".");
                }

                int id = 0;
                if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out id))
                {
                    UserId = id;

                    var repository = new UsersRepository();
                    var userObj = repository.GetByKey(UserId);
                    litTitle.Text = userObj.FirstName;

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
                    Save(packageId.Value);
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