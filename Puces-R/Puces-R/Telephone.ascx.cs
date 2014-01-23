using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Puces_R
{
    public partial class Telephone : System.Web.UI.UserControl
    {
        public string NoTelephone
        {
            get
            {
                return tbTel.Text;
            }
            set
            {
                tbTel.Text = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}