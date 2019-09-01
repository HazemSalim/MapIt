using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web
{
    public partial class TechSupport : MapIt.Lib.BasePage
    {
        #region Variables

        ContentPagesRepository contentPagesRepository;

        #endregion

        #region Properties



        #endregion

        #region Methods

        void LoadData()
        {
            try
            {
                contentPagesRepository = new ContentPagesRepository();

                var pageObj = contentPagesRepository.GetByKey((int)AppEnums.ContentPages.TechSupport);
                if (pageObj != null)
                {
                    if (!pageObj.IsActive)
                    {
                        Response.Redirect(".", false);
                        return;
                    }

                    litContent.Text = Culture.ToLower() == "ar-kw" ? pageObj.ContentAR : pageObj.ContentEN;
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
                LoadData();
            }
        }

        #endregion
    }
}
