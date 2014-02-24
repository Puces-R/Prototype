using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Puces_R
{
    public partial class InsertionProduits : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Librairie.Autorisation(false, false, true, false);
            }
        }
        public void validationSaisie(Object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                insertionProduits(sender, e);
            }
            else
            {
                lblAvertissement.Text = "Il y a eu un problème lors de l'insertion. Veuillez corriger les erreurs!";
            }
        }
        
        public void insertionProduits(Object sender, EventArgs e)
        {
            SqlConnection maConnexion = Librairie.Connexion;
            maConnexion.Open();
            long cat = ctrProduit.NoCategorie;

            SqlCommand maCommande = new SqlCommand("SELECT ISNULL(MAX(NoProduit), NoVendeur * 100000) + 1 FROM PPProduits WHERE (NoVendeur = @no) GROUP BY NoVendeur", maConnexion);
            maCommande.Parameters.AddWithValue("@no", Session["ID"]);

            int no = Convert.ToInt32(maCommande.ExecuteScalar());
            ctrProduit.NoProduit = no;

            SqlCommand cmdInsertion = new SqlCommand("INSERT INTO PPProduits VALUES(@no, @vendeur, @categorie, @descAbregee, @descComplete, @nomFichier, @prixDemande, @nb, @dispo,NULL, @prixVente, @poids, @date, NULL)", maConnexion);

            cmdInsertion.Parameters.AddWithValue("@no", ctrProduit.NoProduit);
            cmdInsertion.Parameters.AddWithValue("@vendeur", Session["ID"]);
            cmdInsertion.Parameters.AddWithValue("@categorie", ctrProduit.NoCategorie);
            cmdInsertion.Parameters.AddWithValue("@descAbregee", ctrProduit.DescriptionAbregee);
            cmdInsertion.Parameters.AddWithValue("@descComplete", ctrProduit.DescriptionComplete);
            cmdInsertion.Parameters.AddWithValue("@nomFichier", ctrProduit.Fichier == null ? DBNull.Value : (object)ctrProduit.Fichier);
            cmdInsertion.Parameters.AddWithValue("@prixDemande", ctrProduit.PrixDemande);
            cmdInsertion.Parameters.AddWithValue("@nb", ctrProduit.NbItems);
            cmdInsertion.Parameters.AddWithValue("@dispo", ctrProduit.Disponibilite);
            cmdInsertion.Parameters.AddWithValue("@prixVente", ctrProduit.PrixVente == -1 ? DBNull.Value : (object)ctrProduit.PrixVente);
            cmdInsertion.Parameters.AddWithValue("@poids", ctrProduit.Poids);
            cmdInsertion.Parameters.AddWithValue("@date", DateTime.Now);

            cmdInsertion.ExecuteNonQuery();
            maConnexion.Close();
        }

    }
}