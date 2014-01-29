using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class RecupererMotDePasse : System.Web.UI.Page
    {
        SqlConnection connexion = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2;");

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void adresseExiste(object sender, ServerValidateEventArgs e)
        {
            SqlCommand cmdExiste = new SqlCommand("SELECT CASE WHEN COUNT(AdresseEmail) = 0 THEN 'false' ELSE 'true' END FROM " +
                "(SELECT AdresseEmail FROM PPClients UNION " + 
                 "SELECT AdresseEmail FROM PPVendeurs UNION " + 
                 "SELECT AdresseEmail FROM PPGestionnaires) AS X " +
                 "WHERE (AdresseEmail = @adresse)", connexion);
            cmdExiste.Parameters.AddWithValue("@adresse", tbCourriel.Text);

            connexion.Open();
            e.IsValid = bool.Parse(cmdExiste.ExecuteScalar().ToString());
            connexion.Close();
        }

        protected void envoyerMdp(object sender, EventArgs e)
        {
            Page.Validate();
        }
    }
}