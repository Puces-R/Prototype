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

        protected void inscription(object sender, EventArgs e)
        {
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
                cmdAjoutVendeur.Parameters.AddWithValue("@nomAffaire", ctrProfil.NomAffaires);
                cmdAjoutVendeur.Parameters.AddWithValue("@nom", ctrProfil.Nom);
                cmdAjoutVendeur.Parameters.AddWithValue("@prenom", ctrProfil.Prenom);
                cmdAjoutVendeur.Parameters.AddWithValue("@rue", ctrProfil.Adresse);

                cmdAjoutVendeur.Parameters.AddWithValue("@ville", ctrProfil.Ville);
                cmdAjoutVendeur.Parameters.AddWithValue("@province", ctrProfil.Province);
                cmdAjoutVendeur.Parameters.AddWithValue("@codePostal", ctrProfil.CodePostal);
                cmdAjoutVendeur.Parameters.AddWithValue("@pays", ctrProfil.Pays);
                cmdAjoutVendeur.Parameters.AddWithValue("@tel1", ctrProfil.Tel1);

                cmdAjoutVendeur.Parameters.AddWithValue("@tel2", ctrProfil.Tel2 == string.Empty ? DBNull.Value : (object)ctrProfil.Tel2);
                cmdAjoutVendeur.Parameters.AddWithValue("@courriel", tbIdentifiants.Adresse);
                cmdAjoutVendeur.Parameters.AddWithValue("@mdp", tbIdentifiants.MotDePasse);
                cmdAjoutVendeur.Parameters.AddWithValue("@maxLivraison", ctrProfil.PoidsMaximum);
                cmdAjoutVendeur.Parameters.AddWithValue("@gratuite", ctrProfil.LivraisonGratuite);
                cmdAjoutVendeur.Parameters.AddWithValue("@taxes", ctrProfil.Taxes);

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