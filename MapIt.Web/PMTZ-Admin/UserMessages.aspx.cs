using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web.Admin
{
    public partial class UserMessages : System.Web.UI.Page
    {
        #region Variables

        TechMessagesRepository techMessagesRepository;

        string pushMessageEN;
        string pushMessageAR;

        #endregion Variables

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
                techMessagesRepository = new TechMessagesRepository();
                techMessagesRepository.SetRead(UserId);

                var list = techMessagesRepository.Find(m => m.UserId == UserId).OrderByDescending(m => m.AddedOn).AsQueryable();
                if (list != null && list.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(SortExpression))
                    {
                        list = SortHelper.SortList<TechMessage>(list, SortExpression, SortDirection);
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
            txtMessage.Text = string.Empty;
            btnCancel.Visible = false;
            gvMessages.SelectedIndex = -1;
            RecordId = null;
        }

        void DoWork()
        {
            AppPushs.Push((int)AppEnums.NotifTypes.Message, UserId, null, null, null, null, pushMessageEN, pushMessageAR);
        }

        void Save()
        {
            try
            {
                var messageObj = new TechMessage();
                messageObj.UserId = UserId;
                messageObj.Sender = "Support";
                messageObj.TextMessage = txtMessage.Text;
                messageObj.IsRead = true;
                messageObj.AddedOn = DateTime.Now;

                techMessagesRepository = new TechMessagesRepository();
                techMessagesRepository.Add(messageObj);

                // Send push ...
                pushMessageEN = "Support sent: " + txtMessage.Text;
                pushMessageAR = "الدعم الفني ارسل: " + txtMessage.Text;

                Thread th = new Thread(DoWork);
                th.Start();

                ClearControls();
                LoadData();
                PresentHelper.ShowScriptMessage("Sent");
            }
            catch (Exception ex)
            {
                PresentHelper.ShowScriptMessage("Error in adding");
                LogHelper.LogException(ex);
            }
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

                long id = 0;
                if (Request.QueryString["id"] != null && long.TryParse(Request.QueryString["id"], out id))
                {
                    UserId = id;

                    var repository = new UsersRepository();
                    var pObj = repository.GetByKey(UserId);
                    litTitle.Text = pObj.FullName;

                    LoadData();
                }
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        #endregion Events
    }
}