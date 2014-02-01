using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Puces_R.Controles
{
    public partial class ChangementMDP : System.Web.UI.UserControl
    {
        public String MotDePasse
        {
            get
            {
                return ctrMotDePasse.MotDePasse;
            }
        }

        public bool EstDeroule
        {
            get
            {
                return ctrMotDePasse.Visible;
            }
        }

        protected void btnMotDePasse_OnClick(object sender, EventArgs e)
        {
            ctrMotDePasse.Visible = !ctrMotDePasse.Visible;
            if (EstDeroule)
            {
                btnMotDePasse.Text = "Annuler";
            }
            else
            {
                btnMotDePasse.Text = "Changer de mot de passe";
            }
        }
    }
}