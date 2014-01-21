using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class IdentifiantsInscription : System.Web.UI.UserControl
    {
        SqlConnection connexion = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2;");

        public string Adresse
        {
            get
            {
                return tbCourriel.Text;
            }
            set
            {
                tbCourriel.Text = value;
            }
        }

        public string MotDePasse
        {
            get
            {
                return tbMotPasse.Text;
            }
        }

        protected void validerAdresseExistante(object sender, ServerValidateEventArgs e)
        {
            SqlCommand cmdAdresse = new SqlCommand("SELECT CASE WHEN COUNT(AdresseEmail) > 0 THEN 'true' ELSE 'false' END " + 
                                                   "FROM (SELECT AdresseEmail FROM PPClients UNION " + 
                                                         "SELECT AdresseEmail FROM PPVendeurs) X " + 
                                                   "WHERE (AdresseEmail = @adresse)", connexion);
            cmdAdresse.Parameters.AddWithValue("@adresse", tbCourriel.Text);
            connexion.Open();
            e.IsValid = !bool.Parse(cmdAdresse.ExecuteScalar().ToString());
            connexion.Close();
        }

        protected void validerMotPasse(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = tbMotPasse.Text == tbMotPasseConfirmation.Text;
        }
    }
}