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
        public enum TypeIdentification
        {
            INSCRIPTION, CONNEXION, MOT_DE_PASSE
        }

        public TypeIdentification Type
        {
            set
            {
                tbCourriel.Existe =
                trCourriel.Visible = trCourrielConfirmation.Visible = trMdpConfirmation.Visible = true;
                switch (value)
                {
                    case TypeIdentification.INSCRIPTION:
                        // RIEN
                        break;
                    case TypeIdentification.CONNEXION:
                        tbCourriel.Existe = trCourrielConfirmation.Visible = trMdpConfirmation.Visible = false;
                        break;
                    case TypeIdentification.MOT_DE_PASSE:
                        trCourriel.Visible = trCourrielConfirmation.Visible = false;
                        break;
                }
            }
        }

        SqlConnection connexion = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2;");

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