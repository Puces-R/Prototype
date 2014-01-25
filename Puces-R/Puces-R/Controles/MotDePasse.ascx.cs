using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Puces_R
{
    public partial class MotDePasse : System.Web.UI.UserControl
    {
        public string Label
        {
            set
            {
                lbl.Text = value;
            }
        }

        public bool Obligatoire
        {
            set
            {
                tdReqMDP.Visible = value;
            }
        }

        public string MotPasse
        {
            get
            {
                return tbMDP.Text;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}