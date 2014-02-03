using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Puces_R.Controles
{
    public partial class Adresse : System.Web.UI.UserControl
    {

        public string Label
        {
            set
            {
                lblAdresse.Text = value;
            }
        }

        public string NoAdresse
        {
            get
            {
                string tel = tbAdresse.Text;
                return tel == string.Empty ? null : tel;
            }
            set
            {
                if (value == null)
                {
                    tbAdresse.Text ="";
                }
                else
                {

                    tbAdresse.Text = value;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}