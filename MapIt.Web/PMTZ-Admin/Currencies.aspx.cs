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
    public partial class Currencies : System.Web.UI.Page
    {
        #region Variables

        CurrenciesRepository currenciesRepository;

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
                currenciesRepository = new CurrenciesRepository();
                var list = currenciesRepository.GetAll();
                if (list != null && list.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        list = SortHelper.SortList<Currency>(list, SortExpression, SortDirection);
                    }

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    pds.DataSource = list.ToList();
                    gvCurrencies.DataSource = pds;
                    gvCurrencies.DataBind();
                    AspNetPager1.RecordCount = list.Count();
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvCurrencies.DataSource = new List<Currency>();
                    gvCurrencies.DataBind();
                    AspNetPager1.Visible = false;
                }

            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        public void ClearControls()
        {
            ddlDigits.SelectedIndex = 0;
            ddlFormat.SelectedIndex = 0;
            txtTitleAR.Text = txtTitleEN.Text = txtSymbolAR.Text =
                txtSymbolEN.Text = txtCode.Text = txtExchangeRate.Text = txtNotes.Text = string.Empty;
            chkActive.Checked = true;
            btnCancel.Visible = false;
            btnSave.Text = "Add new Currency";
            gvCurrencies.SelectedIndex = -1;
            RecordId = null;
        }

        void Add()
        {
            try
            {
                int? code = ParseHelper.GetInt(txtCode.Text);
                if (!code.HasValue)
                {
                    PresentHelper.ShowScriptMessage("Enter valid code");
                    return;
                }

                Currency currencyObj = new Currency();
                currencyObj.TitleEN = txtTitleEN.Text;
                currencyObj.TitleAR = txtTitleAR.Text;
                currencyObj.SymbolEN = txtSymbolEN.Text;
                currencyObj.SymbolAR = txtSymbolAR.Text;
                currencyObj.Digits = ParseHelper.GetInt(ddlDigits.SelectedValue).Value;
                currencyObj.Format = ddlFormat.SelectedValue;
                currencyObj.Code = code.Value;
                currencyObj.ExchangeRate = ParseHelper.GetDouble(txtExchangeRate.Text).Value;
                currencyObj.Notes = txtNotes.Text;
                currencyObj.IsActive = chkActive.Checked;

                currenciesRepository = new CurrenciesRepository();
                currenciesRepository.Add(currencyObj);
                ClearControls();
                LoadData();
                PresentHelper.ShowScriptMessage("Add successfully");
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
                int? code = ParseHelper.GetInt(txtCode.Text);
                if (!code.HasValue)
                {
                    PresentHelper.ShowScriptMessage("Enter valid code");
                    return;
                }

                currenciesRepository = new CurrenciesRepository();
                Currency currencyObj = currenciesRepository.GetByKey(id);
                currencyObj.TitleEN = txtTitleEN.Text;
                currencyObj.TitleAR = txtTitleAR.Text;
                currencyObj.SymbolEN = txtSymbolEN.Text;
                currencyObj.SymbolAR = txtSymbolAR.Text;
                currencyObj.Digits = ParseHelper.GetInt(ddlDigits.SelectedValue).Value;
                currencyObj.Format = ddlFormat.SelectedValue;
                currencyObj.Code = code.Value;
                currencyObj.ExchangeRate = ParseHelper.GetDouble(txtExchangeRate.Text).Value;
                currencyObj.Notes = txtNotes.Text;
                currencyObj.IsActive = chkActive.Checked;

                currenciesRepository = new CurrenciesRepository();
                currenciesRepository.Update(currencyObj);
                ClearControls();
                LoadData();
                PresentHelper.ShowScriptMessage("Update successfully");
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
                currenciesRepository = new CurrenciesRepository();
                currenciesRepository.Delete(id);
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
                currenciesRepository = new CurrenciesRepository();
                Currency currencyObj = currenciesRepository.GetByKey(id);
                if (currencyObj != null)
                {
                    txtTitleEN.Text = currencyObj.TitleEN;
                    txtTitleAR.Text = currencyObj.TitleAR;
                    txtSymbolEN.Text = currencyObj.SymbolEN;
                    txtSymbolAR.Text = currencyObj.SymbolAR;
                    ddlDigits.SelectedValue = currencyObj.Digits.ToString();
                    ddlFormat.SelectedValue = currencyObj.Format;
                    txtCode.Text = currencyObj.Code.ToString();
                    txtExchangeRate.Text = currencyObj.ExchangeRate.ToString();
                    txtNotes.Text = currencyObj.Notes;
                    chkActive.Checked = currencyObj.IsActive;

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
                    !(new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminUserId"].ToString()), (int)AppEnums.AdminPages.Currencies)))
                {
                    Response.Redirect(".");
                }

                LoadData();
            }
        }

        protected void gvCurrencies_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditItem")
            {
                int id = 0;
                int index = 0;
                string[] args = e.CommandArgument.ToString().Split(',');
                if (args.Length == 2 && int.TryParse(args[0], out id) && int.TryParse(args[1], out index))
                {
                    if (SetData(id))
                    {
                        gvCurrencies.SelectedIndex = index;
                        RecordId = id;
                        btnSave.Text = "Update Currency";
                        btnCancel.Visible = true;
                    }
                }
            }
            else if (e.CommandName == "DeleteItem")
            {
                Delete(int.Parse(e.CommandArgument.ToString()));
            }
        }

        protected void gvCurrencies_Sorting(object sender, GridViewSortEventArgs e)
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
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Page.Validate(btnSave.ValidationGroup);

            base.OnPreRenderComplete(e);
        }

        #endregion Events
    }
}