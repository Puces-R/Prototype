using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Puces_R
{
    public partial class Province : System.Web.UI.UserControl
    {
        public string CodeProvince
        {
            get
            {
                return ddlProvince.SelectedValue;
            }
            set
            {
                ddlProvince.SelectedValue = value.ToUpper();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}