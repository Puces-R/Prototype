using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class Identifiants : System.Web.UI.UserControl
    {
        public bool Inscription
        {
            get
            {
                return _inscription;
            }
            set
            {
                _inscription =
                trInscriptionMdp.Visible =
                trInscriptionCourriel.Visible = value;
            }
        }

        SqlConnection connexion = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2;");
        private bool _inscription = false;

        public string Adresse
        {
            get
            {
                return tbCourriel.Adresse;
            }
        }

        public string MotDePasse
        {
            get
            {
                return tbMotPasse.Text;
            }
        }

        protected void validerMotPasse(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = tbMotPasse.Text == tbMotPasseConfirmation.Text;
        }
    }
}