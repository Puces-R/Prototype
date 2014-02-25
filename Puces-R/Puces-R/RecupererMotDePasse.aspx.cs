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
            if (!IsPostBack)
            {
                Librairie.Autorisation(true, false, false, false);
            }
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
                c.Message = "Bonjour, <br /><br />" +
                            "Vous avez fait une demande de récupération de mot de passe <br /> <br />" +
                            "Vos identifiants sont les suivants : <br />" +
                            "Adresse courriel : " + tbCourriel.Text.Trim().ToLower() + "<br />" +
                            "Mot de passe : " + cmdMdp.ExecuteScalar().ToString() + "<br /><br />" +
                            "Vous n'avez qu'à <a href=\"http://424r.cgodin.qc.ca\">cliquer ici</a> pour revenir au site Des Petites Puces. <br /><br />" +
                            "Cordialement, <br />" +
                            "Les Petites Puces";
                connexion.Close();
                c.envoyer();

                Response.Redirect("Default.aspx");
            }
        }
    }
}