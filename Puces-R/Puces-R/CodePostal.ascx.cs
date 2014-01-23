using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Puces_R
{
    public partial class CodePostal : System.Web.UI.UserControl
    {
        public string Code
        {
            get
            {
                return tbCodePostal.Text == string.Empty ? null : tbCodePostal.Text.ToUpper();
            }
            set
            {
                tbCodePostal.Text = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}