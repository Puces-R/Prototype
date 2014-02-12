using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class InscriptionVendeur : System.Web.UI.Page
    {
        SqlConnection connexion = Librairie.Connexion;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void inscription(object sender, EventArgs e)
        {
            
            tbNomAffaires.Text = tbNomAffaires.Text.Trim();
            tbNom.Text = tbNom.Text.Trim();
            tbPrenom.Text = tbPrenom.Text.Trim();
            tbRue.Text = tbRue.Text.Trim();
            tbVille.Text = tbVille.Text.Trim();
            tbPays.Text = tbPays.Text.Trim();

            Page.Validate();

            if (Page.IsValid)
            {
                SqlCommand cmdNoVendeur = new SqlCommand("SELECT ISNULL(MAX(NoVendeur), 0) + 1 FROM PPVendeurs", connexion);
                SqlCommand cmdAjoutVendeur = new SqlCommand("INSERT INTO PPVendeurs values(@no, @nomAffaire, @nom, @prenom, @rue, " + 
                                                                                          "@ville, @province, @codePostal, @pays, @tel1, " +
                                                                                          "@tel2, @courriel, @mdp, @maxLivraison, @gratuite, @taxes, " +
                                                                                          "NULL, @config, @creation, NULL, @status)", connexion);
                SqlCommand cmdVendeur = new SqlCommand("SELECT NomAffaires FROM PPVendeurs WHERE NoVendeur = @no", connexion);

                
                connexion.Open();
                int noVendeur = int.Parse(cmdNoVendeur.ExecuteScalar().ToString());
                cmdAjoutVendeur.Parameters.AddWithValue("@no", noVendeur);
                cmdAjoutVendeur.Parameters.AddWithValue("@nomAffaire", tbNomAffaires.Text == string.Empty ? DBNull.Value : (object)tbNomAffaires.Text);
                cmdAjoutVendeur.Parameters.AddWithValue("@nom", tbNom.Text == string.Empty ? DBNull.Value : (object)tbNom.Text);
                cmdAjoutVendeur.Parameters.AddWithValue("@prenom", tbPrenom.Text == string.Empty ? DBNull.Value : (object)tbPrenom.Text);
                cmdAjoutVendeur.Parameters.AddWithValue("@rue", tbRue.Text == string.Empty ? DBNull.Value : (object)tbRue.Text);

                cmdAjoutVendeur.Parameters.AddWithValue("@ville", tbVille.Text == string.Empty ? DBNull.Value : (object)tbVille.Text);
                cmdAjoutVendeur.Parameters.AddWithValue("@province", tbProvince.CodeProvince);
                cmdAjoutVendeur.Parameters.AddWithValue("@codePostal", tbCodePostal.Code == null ? DBNull.Value : (object)tbCodePostal.Code);
                cmdAjoutVendeur.Parameters.AddWithValue("@pays", tbPays.Text == string.Empty ? DBNull.Value : (object)tbPays.Text);
                cmdAjoutVendeur.Parameters.AddWithValue("@tel1", tbTel1.NoTelephone == string.Empty ? DBNull.Value : (object)tbTel1.NoTelephone);

                cmdAjoutVendeur.Parameters.AddWithValue("@tel2", tbTel2.NoTelephone == string.Empty ? DBNull.Value : (object)tbTel2.NoTelephone);
                cmdAjoutVendeur.Parameters.AddWithValue("@courriel", tbIdentifiants.Adresse);
                cmdAjoutVendeur.Parameters.AddWithValue("@mdp", tbIdentifiants.MotDePasse);
                cmdAjoutVendeur.Parameters.AddWithValue("@maxLivraison", tbPoids.Text);
                cmdAjoutVendeur.Parameters.AddWithValue("@gratuite", tbPrixLivraison.Text);
                cmdAjoutVendeur.Parameters.AddWithValue("@taxes", cbTaxes.Checked);

                cmdAjoutVendeur.Parameters.AddWithValue("@config", DBNull.Value); // Placeholder
                cmdAjoutVendeur.Parameters.AddWithValue("@creation", DateTime.Now.Date);
                cmdAjoutVendeur.Parameters.AddWithValue("@status", DBNull.Value); // Libre ? À hardcoder

                cmdAjoutVendeur.ExecuteNonQuery();

                cmdVendeur.Parameters.AddWithValue("@no", noVendeur); // ??

                connexion.Close();
                string retourUrgence = "Default.aspx";
                if ((char)Session["Type"] == 'C')
                {
                    retourUrgence = "AccueilClient.aspx";
                }
                Response.Redirect(Chemin.UrlRetour == null ? retourUrgence : Chemin.UrlRetour);
            }
        }
    }
}