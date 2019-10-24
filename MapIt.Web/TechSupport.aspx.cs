using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;
using MapIt.Data;
using System.Collections.Generic;

namespace MapIt.Web
{
    public partial class TechSupport : BasePage
    {
        #region Variables

        TechMessagesRepository techMessagesRepository;
        #endregion

        #region Properties

        public string Id
        {
            get
            {
                if (ViewState["id"] != null)
                    return ViewState["id"].ToString();
                return null;
            }
            set
            {
                ViewState["id"] = value;
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

        #endregion

        #region Methods

        void Submit()
        {
            try
            {
                var messageObj = new TechMessage();
                messageObj.UserId = UserId;
                messageObj.Sender = "User";
                messageObj.TextMessage = txtNotes.Text;
                messageObj.IsRead = false;
                messageObj.AddedOn = DateTime.Now;

                techMessagesRepository = new TechMessagesRepository();
                techMessagesRepository.Add(messageObj);

                AppMails.SendNewTechMessageToAdmin(messageObj.Id);


                ClearControls();
                LoadData();
                PresentHelper.ShowScriptMessage("Sent");
            }
            catch (Exception ex)
            {
                PresentHelper.ShowScriptMessage(Resources.Resource.error);
                LogHelper.LogException(ex);
            }
        }

        void LoadData()
        {
            try
            {
                techMessagesRepository = new TechMessagesRepository();
                //techMessagesRepository.SetRead(UserId);

                var list = techMessagesRepository.Find(m => m.UserId == UserId).OrderByDescending(m => m.AddedOn).AsQueryable();
                if (list != null && list.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        list = SortHelper.SortList(list, SortExpression, SortDirection);
                    }

                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;

                    pds.DataSource = list.ToList();
                    gvMessages.DataSource = pds;
                    gvMessages.DataBind();
                    AspNetPager1.RecordCount = list.Count();
                    AspNetPager1.Visible = true;
                }
                else
                {
                    gvMessages.DataSource = new List<TechMessage>();
                    gvMessages.DataBind();
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
            txtNotes.Text = string.Empty;
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

                LoadData();
            }
        }

        protected void gvMessages_Sorting(object sender, GridViewSortEventArgs e)
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Page.Validate(btnSubmit.ValidationGroup);
            if (Page.IsValid)
            {
                Submit();
            }
        }

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    ClearControls();
        //}

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Page.Validate(btnSubmit.ValidationGroup);

            base.OnPreRenderComplete(e);
        }

        #endregion
    }
}