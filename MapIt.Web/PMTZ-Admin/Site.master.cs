﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using MapIt.Helpers;
using MapIt.Data;
using MapIt.Repository;

namespace MapIt.Web.Admin
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        #region Variables

        TechMessagesRepository techMessagesRepository;

        #endregion Variables

        #region Events

        protected void Page_PreRender(object sender, EventArgs e)
        {
            GetNotificationData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserId"] == null || Session["AdminUserName"] == null)
            {
                Response.Redirect("Login?ReturnUrl=" + Request.RawUrl, true);
            }
            else
            {
                lblFullName.Text = Session["AdminFullName"].ToString();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            base.OnInit(e);
        }

        #endregion

        #region Methods
        private void GetNotificationData()
        {
            techMessagesRepository = new TechMessagesRepository();
            int unReadCount = techMessagesRepository.GetUnReadCount();
            if (unReadCount > 0)
            {
                litTechMessages.Text = "<a href='TechMessages.aspx' title ='Support Messages' class='fa fa-bell-o'><span class='fa fa-comment'></span><span class='num'>" +
                    unReadCount + "</span></a>";
            }
            else
            {
                litTechMessages.Text = "";
            }
        }
        #endregion

    }
}