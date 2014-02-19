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
        bool _obligatoire = true;
        bool _longueur = true;

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
                _obligatoire = value;
                tdValidationMDP.Visible = _longueur || _obligatoire;
                reqMDP.Visible = value;
            }
        }

        public bool Longueur
        {
            get
            {
                return custMDP.Visible;
            }
            set
            {
                _longueur = value;
                tdValidationMDP.Visible = _longueur || _obligatoire;
                custMDP.Visible = value;
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
                return (!Obligatoire || reqMDP.IsValid) && (!Longueur || custMDP.IsValid);
            }
        }

        public void longueurMDP(object sender, ServerValidateEventArgs e)
        {
            if (!Obligatoire || reqMDP.IsValid)
            {
                tbMDP.Text = tbMDP.Text.Trim();
                e.IsValid = tbMDP.Text.Length >= 6;
            }
            else
            {
                e.IsValid = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}