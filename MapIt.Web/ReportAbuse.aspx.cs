using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web
{
    public partial class ReportAbuse : BasePage
    {
        #region Variables

        PropertiesRepository propertiesRepository;
        ServicesRepository servicesRepository;

        ReasonsRepository reasonsRepository;

        #endregion

        #region Properties

        public string Op
        {
            get
            {
                if (ViewState["op"] != null)
                    return ViewState["op"].ToString();
                return null;
            }
            set
            {
                ViewState["op"] = value;
            }
        }

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

        public string RefUrl
        {
            get
            {
                if (ViewState["RefUrl"] != null)
                    return ViewState["RefUrl"].ToString();
                return null;
            }
            set
            {
                ViewState["RefUrl"] = value;
            }
        }

        #endregion

        #region Methods

        void BindReportReasons()
        {
            try
            {
                reasonsRepository = new ReasonsRepository();
                var list = reasonsRepository.GetAll().ToList();
                list = Culture.ToLower() == "ar-kw" ? list.OrderBy(c => c.TitleAR).ToList() : list.OrderBy(c => c.TitleEN).ToList();

                if (list != null && list.Count > 0)
                {
                    ddlReasonTypes.DataValueField = "Id";
                    ddlReasonTypes.DataTextField = Culture.ToLower() == "ar-kw" ? "TitleAR" : "TitleEN";

                    ddlReasonTypes.DataSource = list;
                    ddlReasonTypes.DataBind();
                }
             
                list = null;
                reasonsRepository = null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void Submit()
        {
            try
            {
                if (UserId > 0)
                {
                    if (Op == "1")
                    {
                        propertiesRepository = new PropertiesRepository();
                        propertiesRepository.SetReport(long.Parse(Id), UserId, int.Parse(ddlReasonTypes.SelectedValue), txtNotes.Text);
                        Response.Redirect(RefUrl);

                    }
                    else if (Op == "2")
                    {
                        servicesRepository = new ServicesRepository();
                        servicesRepository.SetReport(long.Parse(Id), UserId, int.Parse(ddlReasonTypes.SelectedValue), txtNotes.Text);
                        Response.Redirect(RefUrl);

                    }
                }
                else
                {
                    PresentHelper.ShowScriptMessage(Resources.Resource.login_first, "/Login");
                }
            }
            catch (Exception ex)
            {
                PresentHelper.ShowScriptMessage(Resources.Resource.error);
                LogHelper.LogException(ex);
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefUrl = Request.UrlReferrer.ToString();
                if (Request.QueryString["op"] != null && Request.QueryString["op"].Trim() != string.Empty &&
                    Request.QueryString["id"] != null && Request.QueryString["id"].Trim() != string.Empty)
                {
                    Op = Request.QueryString["op"].ToLower().Trim();
                    Id = Request.QueryString["id"].ToLower().Trim();

                    if (Op == "1")
                    {
                        propertiesRepository = new PropertiesRepository();
                        var obj = propertiesRepository.GetByKey(long.Parse(Id));
                        if (obj != null)
                        {
                            Title = Resources.Resource.web_title + " | " + Culture.ToLower() == "ar-kw" ? obj.TitleAR : obj.TitleEN;
                            litTitle.Text = Culture.ToLower() == "ar-kw" ? obj.TitleAR : obj.TitleEN;
                        }
                     
                    }
                    else if (Op == "2")
                    {
                        servicesRepository = new ServicesRepository();
                        var obj = servicesRepository.GetByKey(long.Parse(Id));
                        if (obj != null)
                        {
                            Title = Resources.Resource.web_title + " | " + obj.Title;
                            litTitle.Text = obj.Title;
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Login", true);
                    }
                }
                else
                {
                    Response.Redirect("~/Login", true);
                }

                BindReportReasons();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Page.Validate(btnSubmit.ValidationGroup);
            if (Page.IsValid)
            {
                Submit();
            }
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Page.Validate(btnSubmit.ValidationGroup);

            base.OnPreRenderComplete(e);
        }

        #endregion
    }
}