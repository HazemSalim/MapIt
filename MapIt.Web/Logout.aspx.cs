using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MapIt.Web
{
    public partial class Logout : MapIt.Lib.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserId = 0;
            Response.Redirect(".", false);
        }
    }
}