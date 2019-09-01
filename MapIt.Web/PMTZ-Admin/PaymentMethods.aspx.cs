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
    public partial class PaymentMethods : System.Web.UI.Page
    {
        #region Variables

        PaymentMethodsRepository paymentMethodsRepository;

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
                paymentMethodsRepository = new PaymentMethodsRepository();
                var list = paymentMethodsRepository.GetAll();
                if (list != null && list.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        list = SortHelper.SortList<PaymentMethod>(list, SortExpression, SortDirection);
                    }

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    pds.DataSource = list.ToList();
                    gvPaymentMethods.DataSource = pds;
                    gvPaymentMethods.DataBind();
                    AspNetPager1.RecordCount = list.Count();
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvPaymentMethods.DataSource = new List<PaymentMethod>();
                    gvPaymentMethods.DataBind();
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
            txtTitleAR.Text = txtTitleEN.Text = txtCostFeePercent.Text = string.Empty;
            chkActive.Checked = true;
            btnCancel.Visible = false;
            btnSave.Text = "Add new Payment Method";
            gvPaymentMethods.SelectedIndex = -1;
            RecordId = null;
        }

        void Add()
        {
            try
            {
                PaymentMethod paymentMethodObj = new PaymentMethod();
                paymentMethodObj.TitleEN = txtTitleEN.Text;
                paymentMethodObj.TitleAR = txtTitleAR.Text;
                paymentMethodObj.CostFeePercent = ParseHelper.GetDouble(txtCostFeePercent.Text).Value;
                paymentMethodObj.IsActive = chkActive.Checked;

                paymentMethodsRepository = new PaymentMethodsRepository();
                paymentMethodsRepository.Add(paymentMethodObj);
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
                paymentMethodsRepository = new PaymentMethodsRepository();
                PaymentMethod paymentMethodObj = paymentMethodsRepository.GetByKey(id);
                paymentMethodObj.TitleEN = txtTitleEN.Text;
                paymentMethodObj.TitleAR = txtTitleAR.Text;
                paymentMethodObj.CostFeePercent = ParseHelper.GetDouble(txtCostFeePercent.Text).Value;
                paymentMethodObj.IsActive = chkActive.Checked;

                paymentMethodsRepository = new PaymentMethodsRepository();
                paymentMethodsRepository.Update(paymentMethodObj);
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
                paymentMethodsRepository = new PaymentMethodsRepository();
                paymentMethodsRepository.Delete(id);
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
                paymentMethodsRepository = new PaymentMethodsRepository();
                PaymentMethod paymentMethodObj = paymentMethodsRepository.GetByKey(id);
                if (paymentMethodObj != null)
                {
                    txtTitleEN.Text = paymentMethodObj.TitleEN;
                    txtTitleAR.Text = paymentMethodObj.TitleAR;
                    txtCostFeePercent.Text = paymentMethodObj.CostFeePercent.ToString();
                    chkActive.Checked = paymentMethodObj.IsActive;

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
                    !(new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminUserId"].ToString()), (int)AppEnums.AdminPages.PaymentMethods)))
                {
                    Response.Redirect(".");
                }

                LoadData();
            }
        }

        protected void gvPaymentMethods_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        gvPaymentMethods.SelectedIndex = index;
                        RecordId = id;
                        btnSave.Text = "Update Payment Method";
                        btnCancel.Visible = true;
                    }
                }
            }
            else if (e.CommandName == "DeleteItem")
            {
                Delete(int.Parse(e.CommandArgument.ToString()));
            }
        }

        protected void gvPaymentMethods_Sorting(object sender, GridViewSortEventArgs e)
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