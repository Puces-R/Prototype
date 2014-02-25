using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class ModificationProduits : System.Web.UI.Page
    {

        private bool DansPanier
        {
            get
            {
                return (bool)ViewState["Panier"];
            }
            set
            {
                ViewState["Panier"] = value;
            }
        }

        SqlConnection myConnection = Librairie.Connexion;
        int noProduit = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Librairie.Autorisation(false, false, true, false);
                if (!int.TryParse(Request.Params["noproduit"], out noProduit))
                {
                    Response.Redirect(Chemin.UrlRetour == null ? "AccueilVendeur.aspx" : Chemin.UrlRetour, true);
                }
                else
                {
                    String whereClause = " WHERE NoProduit = " + noProduit.ToString() + " AND NoVendeur = " + Session["ID"];

                    SqlCommand commandeClient = new SqlCommand("SELECT * FROM PPProduits" + whereClause, myConnection);

                    myConnection.Open();

                    SqlDataReader lecteurClient = commandeClient.ExecuteReader();
                    if (lecteurClient.Read())
                    {
                        ctrProduit.NoCategorie = Convert.ToInt32(lecteurClient["NoCategorie"]);
                        ctrProduit.DescriptionAbregee = lecteurClient["Nom"].ToString();
                        ctrProduit.PrixDemande = Convert.ToDecimal(lecteurClient["PrixDemande"]);
                        ctrProduit.DescriptionComplete = lecteurClient["Description"].ToString();
                        ctrProduit.NbItems = Convert.ToInt32(lecteurClient["NombreItems"]);
                        if (!(lecteurClient["PrixVente"] is DBNull))
                        {
                            ctrProduit.PrixVente = Convert.ToDecimal(lecteurClient["PrixVente"]);
                        }
                        ctrProduit.Poids = Convert.ToDecimal(lecteurClient["Poids"]);
                        ctrProduit.Disponibilite = Convert.ToBoolean(lecteurClient["Disponibilité"]);

                        object photo = lecteurClient["Photo"];
                        if (!(photo is DBNull))
                        {
                            imgProduits.ImageUrl = "Images/Televerse/" + photo.ToString();
                        }
                        else
                        {
                            imgProduits.ImageUrl = "Images/image_non_disponible.png";
                        }
                    }
                    else
                    {
                        myConnection.Close();
                        Response.Redirect(Chemin.UrlRetour == null ? "AccueilVendeur.aspx" : Chemin.UrlRetour, true);
                    }
                    myConnection.Close();
                }
            }
        }

        protected void modifierProduit(object sender, EventArgs e)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                SqlCommand cmdModifier = new SqlCommand(
                    "UPDATE PPProduits SET " +
                    "NoCategorie = @categorie, " +
                    "Nom = @descAbregee, " +
                    "PrixDemande = @prixDemande, " +
                    "Description = @descComplete, " +
                    "NombreItems = @nb, " +
                    "PrixVente = @prixVente, " +
                    "Poids = @poids, " +
                    "Disponibilité = @dispo, " +
                    "DateMAJ = @date " +
                    "WHERE NoProduit = @no"
                    , myConnection);

                cmdModifier.Parameters.AddWithValue("@categorie", ctrProduit.NoCategorie);
                cmdModifier.Parameters.AddWithValue("@descAbregee", ctrProduit.DescriptionAbregee);
                cmdModifier.Parameters.AddWithValue("@prixDemande", ctrProduit.PrixDemande);
                cmdModifier.Parameters.AddWithValue("@descComplete", ctrProduit.DescriptionComplete);
                cmdModifier.Parameters.AddWithValue("@nb", ctrProduit.NbItems);
                cmdModifier.Parameters.AddWithValue("@prixVente", ctrProduit.PrixVente == -1 ? DBNull.Value : (object)ctrProduit.PrixVente);
                cmdModifier.Parameters.AddWithValue("@poids", ctrProduit.Poids);
                cmdModifier.Parameters.AddWithValue("@dispo", ctrProduit.Disponibilite);
                cmdModifier.Parameters.AddWithValue("@date", DateTime.Now);
                cmdModifier.Parameters.AddWithValue("@no", Request.Params["noproduit"]);

                myConnection.Open();
                cmdModifier.ExecuteNonQuery();
                if (!ctrProduit.Disponibilite)
                {
                    SqlCommand cmdPanier = new SqlCommand("DELETE FROM PPArticlesEnPanier WHERE NoProduit = @no", myConnection);
                    cmdPanier.Parameters.AddWithValue("@no", Request.Params["noproduit"]);
                    cmdPanier.ExecuteNonQuery();
                }
                myConnection.Close();

                if (Chemin.UrlRetour == null)
                {
                    Response.Redirect("GestionProduits.aspx");
                }
                else
                {
                    Response.Redirect(Chemin.UrlRetour);
                }
            }
        }
    }
}