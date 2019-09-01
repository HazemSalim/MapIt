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
    public partial class FAQs : MapIt.Lib.BasePage
    {
        #region Variables

        FAQsRepository faqsRepository;

        #endregion

        #region Properties



        #endregion

        #region Methods

        void LoadData()
        {
            try
            {
                faqsRepository = new FAQsRepository();
                var list = faqsRepository.Find(a => a.IsActive).OrderBy(p => p.Ordering);

                if (list != null && list.Count() > 0)
                {
                    rFAQs.DataSource = list;
                    rFAQs.DataBind();
                }
                else
                {
                    rFAQs.DataSource = new List<object>();
                    rFAQs.DataBind();
                }

                list = null;
                faqsRepository = null;
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
