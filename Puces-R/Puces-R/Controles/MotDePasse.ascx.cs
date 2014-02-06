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
            get
            {
                return reqMDP.Display != ValidatorDisplay.None;
            }
            set
            {
                reqMDP.Display = value ? ValidatorDisplay.Static : ValidatorDisplay.None;
            }
        }

        public string MotPasse
        {
            get
            {
                return tbMDP.Text;
            }
        }

        public bool IsValid
        {
            get
            {
                return !Obligatoire || reqMDP.IsValid;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}