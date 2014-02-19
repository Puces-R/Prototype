using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Puces_R
{
    public partial class DoubleMdp : System.Web.UI.UserControl
    {

        public string MotDePasse
        {
            get
            {
                return tbMDP1.MotPasse;
            }
        }

        public bool IsValid
        {
            get
            {
                return tbMDP1.IsValid && (valTbMDP2.IsValid);
            }
        }

        public bool Changement
        {
            set
            {
                tbMDP1.Label = (value ? "Nouveau m" : "M") + "ot de passe";
                tbMDP2.Label = "Confirmer le mot de passe";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void validerMDPIdentique(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = (tbMDP1.MotPasse == tbMDP2.MotPasse);
        }
    }
}