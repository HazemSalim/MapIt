using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Data;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;
using System.Threading;

namespace MapIt.Web.Admin
{
    public partial class AddFreeCredit : System.Web.UI.Page
    {
        #region Variables

        UsersRepository usersRepository;
        CountriesRepository countriesRepository;
        UserCreditsRepository userCreditsRepository;

        #endregion Variables

        #region Properties

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

        public long? RecordId
        {
            get
            {
                long id = 0;

                if (ViewState["RecordId"] != null && long.TryParse(ViewState["RecordId"].ToString(), out id))
                    return id;

                return null;
            }
            set
            {
                ViewState["RecordId"] = value;
            }
        }

        public bool Search
        {
            get
            {
                bool s = false;
                if (ViewState["Search"] != null && bool.TryParse(ViewState["Search"].ToString(), out s))
                    return s;
                return false;
            }
            set
            {
                ViewState["Search"] = value;
            }
        }

        public int? SearchSexStatus
        {
            get
            {
                int t = 0;
                if (ViewState["SearchSexStatus"] != null && int.TryParse(ViewState["SearchSexStatus"].ToString(), out t))
                    return t;
                return null;
            }
            set
            {
                ViewState["SearchSexStatus"] = value;
            }
        }

        public int? SearchCountry
        {
            get
            {
                int t = 0;
                if (ViewState["SearchCountry"] != null && int.TryParse(ViewState["SearchCountry"].ToString(), out t))
                    return t;
                return null;
            }
            set
            {
                ViewState["SearchCountry"] = value;
            }
        }

        public int? SearchActiveStatus
        {
            get
            {
                int t = 0;
                if (ViewState["SearchActiveStatus"] != null && int.TryParse(ViewState["SearchActiveStatus"].ToString(), out t))
                    return t;
                return null;
            }
            set
            {
                ViewState["SearchActiveStatus"] = value;
            }
        }

        public DateTime? SearchBDateFrom
        {
            get
            {
                if (ViewState["SearchBDateFrom"] != null)
                    return DateTime.Parse(ViewState["SearchBDateFrom"].ToString());
                return null;
            }
            set
            {
                ViewState["SearchBDateFrom"] = value;
            }
        }

        public DateTime? SearchBDateTo
        {
            get
            {
                if (ViewState["SearchBDateTo"] != null)
                    return DateTime.Parse(ViewState["SearchBDateTo"].ToString());
                return null;
            }
            set
            {
                ViewState["SearchBDateTo"] = value;
            }
        }

        public DateTime? SearchCDateFrom
        {
            get
            {
                if (ViewState["SearchCDateFrom"] != null)
                    return DateTime.Parse(ViewState["SearchCDateFrom"].ToString());
                return null;
            }
            set
            {
                ViewState["SearchCDateFrom"] = value;
            }
        }

        public DateTime? SearchCDateTo
        {
            get
            {
                if (ViewState["SearchCDateTo"] != null)
                    return DateTime.Parse(ViewState["SearchCDateTo"].ToString());
                return null;
            }
            set
            {
                ViewState["SearchCDateTo"] = value;
            }
        }

        public string SearchKeyWord
        {
            get
            {
                if (ViewState["SearchKeyWord"] != null)
                    return ViewState["SearchKeyWord"].ToString();
                return null;
            }
            set
            {
                ViewState["SearchKeyWord"] = value;
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

        void BindCountries()
        {
            try
            {
                ddlSearchCountry.DataValueField = "Id";
                ddlSearchCountry.DataTextField = "TitleEN";

                countriesRepository = new CountriesRepository();
                var data = countriesRepository.Find(c => c.IsActive).ToList();
                if (data != null)
                {
                    ddlSearchCountry.DataSource = data.OrderBy(c => c.TitleEN).ToList();
                    ddlSearchCountry.DataBind();
                }

                data = null;
                countriesRepository = null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void LoadData()
        {
            try
            {
                usersRepository = new UsersRepository();
                IQueryable<MapIt.Data.User> usersList;

                if (Search)
                {
                    usersList = usersRepository.Search(null, SearchSexStatus, SearchCountry, SearchActiveStatus, SearchCDateFrom, SearchCDateTo, SearchKeyWord);
                }
                else
                {
                    usersList = usersRepository.GetAll();
                }

                if (usersList != null && usersList.Count() > 0)
                {
                    usersList = usersList.OrderByDescending(u => u.Id);

                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        usersList = SortHelper.SortList<MapIt.Data.User>(usersList, SortExpression, SortDirection);
                    }

                    int count = usersList.Count();
                    usersList = usersList.Skip((AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize).Take(AspNetPager1.PageSize);

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    //pds.DataSource = usersList.ToList();
                    gvUsers.DataSource = usersList.ToList();
                    gvUsers.DataBind();
                    AspNetPager1.RecordCount = count;
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvUsers.DataSource = new List<MapIt.Data.User>();
                    gvUsers.DataBind();
                    AspNetPager1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        public void ClearSearch()
        {
            ddlSearchCountry.SelectedIndex = ddlSearchSexStatus.SelectedIndex = ddlSearchActiveStatus.SelectedIndex = 0;
            txtSearchCDateFrom.Text = txtSearchCDateTo.Text = txtSearchKeyWord.Text = SearchKeyWord = string.Empty;
            SearchCountry = SearchActiveStatus = null;
            SearchCDateFrom = SearchCDateTo = null;
            Search = false;
            AspNetPager1.CurrentPageIndex = 0;
        }

        void AddFree()
        {
            try
            {
                List<int> ids = new List<int>();

                for (int i = 0; i < gvUsers.Rows.Count; i++)
                {
                    CheckBox chk = gvUsers.Rows[i].FindControl("chkItem") as CheckBox;
                    HiddenField hfId = gvUsers.Rows[i].FindControl("hfId") as HiddenField;
                    if (chk != null && chk.Checked == true && hfId != null && !string.IsNullOrEmpty(hfId.Value.Trim()))
                        ids.Add(ParseHelper.GetInt(hfId.Value).Value);
                }

                if (ids.Count == 0)
                {
                    PresentHelper.ShowScriptMessage("Select at least one user to add credit.");
                    return;
                }

                foreach (var item in ids)
                {
                    userCreditsRepository = new UserCreditsRepository();
                    var userCredit = new UserCredit()
                    {
                        TransNo = string.Empty,
                        UserId = item,
                        PackageId = null,
                        PaymentMethodId = (int)AppEnums.PaymentMethods.Free,
                        CurrencyId = Currency.Id,
                        ExchangeRate = Currency.ExchangeRate,
                        Amount = ParseHelper.GetInt(txtAmount.Text).Value,
                        PaymentStatus = (int)AppEnums.PaymentStatus.Paid,
                        TransOn = DateTime.Now
                    };

                    userCreditsRepository.Add(userCredit);
                    userCredit.TransNo = "TRN" + (userCredit.Id).ToString("D6");
                    userCreditsRepository.Update(userCredit);

                    AppMails.SendNewCreditToUser(userCredit.Id);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void DoWork()
        {
            AddFree();
        }

        #endregion Methods

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

                BindCountries();
                LoadData();
            }
        }

        protected void btnAddFree_Click(object sender, EventArgs e)
        {
            try
            {
                Thread th = new Thread(DoWork);
                th.Start();

                //AddFree();

                PresentHelper.ShowScriptMessage("Credit added successfully.");
            }
            catch (Exception ex)
            {
                PresentHelper.ShowScriptMessage("Error in adding");
                LogHelper.LogException(ex);
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search = true;

            SearchSexStatus = ParseHelper.GetInt(ddlSearchSexStatus.SelectedValue);
            SearchCountry = ParseHelper.GetInt(ddlSearchCountry.SelectedValue);
            SearchActiveStatus = ParseHelper.GetInt(ddlSearchActiveStatus.SelectedValue);
            SearchCDateFrom = ParseHelper.GetDate(txtSearchCDateFrom.Text, "yyyy-MM-dd", null);
            SearchCDateTo = ParseHelper.GetDate(txtSearchCDateTo.Text, "yyyy-MM-dd", null);
            SearchKeyWord = txtSearchKeyWord.Text;

            AspNetPager1.CurrentPageIndex = 0;
            LoadData();
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        #endregion Events
    }
}