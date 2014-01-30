using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class CourrielControle : System.Web.UI.UserControl
    {
        SqlConnection connexion = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2;");

        public bool Existe
        {
            set
            {
               adresseExiste.Visible = value;
            }
        }

        public bool Obligatoire
        {
            set
            {
                reqCourriel.Visible = value;
            }
        }

        public bool IsValid
        {
            get
            {
                return (!reqCourriel.Visible || reqCourriel.IsValid) && formatCourriel.IsValid && (!adresseExiste.Visible || adresseExiste.IsValid);
            }
        }

        public string Adresse
        {
            get
            {
                return tbCourriel.Text.ToLower();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void validerAdresseExistante(object sender, ServerValidateEventArgs e)
        {
            SqlCommand cmdAdresse = new SqlCommand("SELECT CASE WHEN COUNT(AdresseEmail) > 0 THEN 'true' ELSE 'false' END " +
                                                   "FROM (SELECT AdresseEmail FROM PPClients UNION " +
                                                         "SELECT AdresseEmail FROM PPVendeurs UNION " +
                                                         "SELECT AdresseEmail FROM PPGestionnaires) X " +
                                                   "WHERE (AdresseEmail = @adresse)", connexion);
            cmdAdresse.Parameters.AddWithValue("@adresse", tbCourriel.Text);
            connexion.Open();
            e.IsValid = !bool.Parse(cmdAdresse.ExecuteScalar().ToString());
            connexion.Close();
        }
    }
}