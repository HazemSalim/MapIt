using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web
{
    public partial class Offers : MapIt.Lib.BasePage
    {
        #region Variables

        OffersRepository offersRepository;

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

        void BindOffers()
        {
            try
            {
                offersRepository = new OffersRepository();
                var list = offersRepository.Find(a => a.IsActive).OrderBy(c => c.Ordering.HasValue ? 0 : 1).ThenBy(c => c.Ordering).ThenByDescending(c => Guid.NewGuid());

                if (list != null && list.Count() > 0)
                {
                    rOffers.DataSource = list;
                    rOffers.DataBind();
                }
                else
                {
                    rOffers.DataSource = new List<object>();
                    rOffers.DataBind();
                }

                btnLoadMore.Visible = rOffers.Items.Count < TopVal ? false : true;
                list = null;
                offersRepository = null;
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
            TopVal += GeneralSetting.PageSize;
        }

        protected override void OnPreRender(EventArgs e)
        {
            BindOffers();
            base.OnPreRender(e);
        }

        protected void btnLoadMore_Click(object sender, EventArgs e)
        {
            BindOffers();
        }

        #endregion
    }
}