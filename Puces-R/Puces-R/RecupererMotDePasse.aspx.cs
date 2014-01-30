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
        SqlConnection connexion = Librairie.Connexion;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void adresseExiste(object sender, ServerValidateEventArgs e)
        {
            tbCourriel.Text = tbCourriel.Text.Trim();

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

            if (Page.IsValid)
            {
                SqlCommand cmdMdp = new SqlCommand("SELECT MotDePasse FROM " +
                "(SELECT MotDePasse, AdresseEmail FROM PPClients UNION " +
                 "SELECT MotDePasse, AdresseEmail FROM PPVendeurs UNION " +
                 "SELECT MotDePasse, AdresseEmail FROM PPGestionnaires) AS X " +
                 "WHERE (AdresseEmail = @adresse)", connexion);

                cmdMdp.Parameters.AddWithValue("@adresse", tbCourriel.Text);

                Courriel c = new Courriel();
                c.ajouterDestinataire(tbCourriel.Text.Trim());
                c.Sujet = "Récupération du mot de passe";
                connexion.Open();
                c.Message = "Mot de passe : " + cmdMdp.ExecuteScalar().ToString();
                connexion.Close();
                c.envoyer();
            }
        }
    }
}