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
    public partial class Broker : MapIt.Lib.BasePage
    {
        #region Variables

        BrokersRepository brokersRepository;

        #endregion

        #region Properties

        public int BrokerId
        {
            get
            {
                int id = 0;
                if (ViewState["BrokerId"] != null && int.TryParse(ViewState["BrokerId"].ToString().Trim(), out id))
                    return id;

                return 0;
            }
            set
            {
                ViewState["BrokerId"] = value;
            }
        }

        #endregion

        #region Methods

        void LoadData()
        {
            try
            {
                brokersRepository = new BrokersRepository();

                var brokerObj = brokersRepository.GetByKey(BrokerId);
                if (brokerObj != null)
                {
                    if (!brokerObj.IsActive)
                    {
                        Response.Redirect(".", false);
                        return;
                    }

                    Title = Culture.ToLower() == "ar-kw" ? GeneralSetting.TitleAR + " | " + brokerObj.FullName : GeneralSetting.TitleEN + " | " + brokerObj.FullName;
                    imgPhoto.Src = brokerObj.FinalPhoto;
                    imgPhoto.Alt = brokerObj.FullName;

                    litFullname.Text = brokerObj.FullName;
                    litDetails.Text = Culture.ToLower() == "ar-kw" ? brokerObj.DetailsAR : brokerObj.DetailsEN;

                    aPhone.InnerText = brokerObj.Phone;
                    //aPhone.HRef = "tel:" + brokerObj.Phone;

                    if (!string.IsNullOrEmpty(brokerObj.Email))
                    {
                        aEmail.InnerText = brokerObj.Email;
                        aEmail.HRef = "mailto:" + brokerObj.Email;
                    }

                    if (brokerObj.AllAreas)
                    {
                        span_allareas.Visible = true;
                    }
                    else
                    {
                        if (brokerObj.BrokerAreas.Count > 0)
                        {
                            rAreas.DataSource = brokerObj.BrokerAreas;
                            rAreas.DataBind();
                        }
                    }
                }
                else
                {
                    Response.Redirect(".");
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                Response.Redirect(".");
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.Page.RouteData.Values["PageName"] != null)
                {
                    string pageName = this.Page.RouteData.Values["PageName"].ToString();
                    int? id = ParseHelper.GetInt(Regex.Match(pageName, @"(?<=(\D|^))\d+(?=\D*$)"));

                    if (id.HasValue)
                    {
                        BrokerId = id.Value;
                        LoadData();
                    }
                    else
                    {
                        Response.Redirect(".");
                    }
                }
                else
                {
                    Response.Redirect(".");
                }
            }
        }

        #endregion
    }
}