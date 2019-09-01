using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MapIt.Helpers;
using MapIt.Repository;

namespace MapIt.Web.Admin
{
    public partial class _Default : System.Web.UI.Page
    {
        #region Variables

        UserCreditsRepository userCreditsRepository;
        UsersRepository usersRepository;
        PropertiesRepository propertiesRepository;

        #endregion

        #region Methods

        void LoadData()
        {
            try
            {
                usersRepository = new UsersRepository();
                litCreUsers.Text = usersRepository.Count().ToString();
                litPUsers.Text = "0";
                litNPUsers.Text = "0";
                litAcUsers.Text = usersRepository.Count(u => u.IsActive).ToString();

                userCreditsRepository = new UserCreditsRepository();
                litUserCredits.Text = userCreditsRepository.Count().ToString();

                propertiesRepository = new PropertiesRepository();
                litProperties.Text = propertiesRepository.Count().ToString();
                litPendingProperties.Text = propertiesRepository.Count(p => !p.IsActive).ToString();

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
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        #endregion
    }
}