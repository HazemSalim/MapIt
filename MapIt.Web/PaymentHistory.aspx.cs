using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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
    public partial class PaymentHistory : MapIt.Lib.BasePage
    {
        #region Variables

        UserBalanceLogsRepository userBalanceLogsRepository;

        #endregion

        #region Properties

        public int TopVal
        {
            get
            {
                int val = 0;
                if (ViewState["TopVal"] != null && int.TryParse(ViewState["TopVal"].ToString(), out val))
                    return val;

                return 0;
            }
            set
            {
                ViewState["TopVal"] = value;
            }
        }

        #endregion

        #region Methods

        void LoadData()
        {
            try
            {
                userBalanceLogsRepository = new UserBalanceLogsRepository();
                var list = userBalanceLogsRepository.Find(i => i.UserId == UserId).OrderByDescending(p => p.Id).Take(TopVal);
                if (list != null && list.Count() > 0)
                {
                    gvLogs.DataSource = list;
                    gvLogs.DataBind();
                }
                else
                {
                    gvLogs.DataSource = new List<object>();
                    gvLogs.DataBind();
                }

                btnLoadMore.Visible = gvLogs.Rows.Count < TopVal ? false : true;
                list = null;
                userBalanceLogsRepository = null;
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
            if (UserId <= 0)
            {
                Response.Redirect("~/Login?ReturnUrl=" + Request.RawUrl, true);
            }

            TopVal += GeneralSetting.PageSize;
        }

        protected override void OnPreRender(EventArgs e)
        {
            LoadData();
            base.OnPreRender(e);
        }

        protected void btnLoadMore_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        #endregion
    }
}