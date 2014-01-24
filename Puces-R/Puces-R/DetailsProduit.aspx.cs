using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Puces_R
{
    public partial class DetailsProduit : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int noProduit;
                if (!int.TryParse(Request.Params["noproduit"], out noProduit))
                {
                    Response.Redirect("Default.aspx", true);
                }

                String whereClause = " WHERE P.noProduit = " + noProduit;

                SqlCommand commandeProduit = new SqlCommand("SELECT P.NoProduit,Photo,P.Description,C.Description AS Categorie,Nom,PrixDemande,PrixVente,NombreItems,Poids,DateCreation,DateMAJ FROM PPProduits P INNER JOIN PPCategories C ON C.NoCategorie = P.NoCategorie" + whereClause, myConnection);

                myConnection.Open();

                SqlDataReader lecteurProduit = commandeProduit.ExecuteReader();
                lecteurProduit.Read();

                this.imgProduit.ImageUrl = "Images/Televerse/" + lecteurProduit["Photo"].ToString();

                this.lblProduit.Text = lecteurProduit["Nom"].ToString();
                this.lblCategorie.Text = lecteurProduit["Categorie"].ToString();
                this.lblDescription.Text = lecteurProduit["Description"].ToString();
                this.lblPrixDemande.Text = ((decimal)lecteurProduit["PrixDemande"]).ToString("C");
                this.lblPrixEnVente.Text = ((decimal)lecteurProduit["PrixVente"]).ToString("C");
                this.lblQuantiteDisponible.Text = lecteurProduit["NombreItems"].ToString();
                this.lblDateCreation.Text = ((DateTime)lecteurProduit["DateCreation"]).ToShortDateString();

                object dateMAJ = lecteurProduit["DateMAJ"];
                if (dateMAJ is DBNull)
                {
                    this.lblDateMiseAJour.Text = "Jamais modifié";
                }
                else
                {
                    this.lblDateMiseAJour.Text = lecteurProduit["DateMAJ"].ToString();
                }

                myConnection.Close();
            }
        }

        protected void btnAjouterPanier_Click(object sender, EventArgs e)
        {
            String nbItems = txtQuantite.Text;
            String noClient = Request.Params["noclient"];
            String noProduit = Request.Params["noproduit"];
            String noVendeur = noProduit.Substring(5);
            String noPanier = noClient + noProduit;
            
            myConnection.Open();
            SqlCommand commandeDejaPresent = new SqlCommand("SELECT COUNT(*) FROM PPArticlesEnPanier WHERE noPanier = " + noPanier, myConnection);
            bool dejaPresent = (int)commandeDejaPresent.ExecuteScalar() > 0;
            if (dejaPresent)
            {
                SqlCommand commandeMAJQuantite = new SqlCommand("UPDATE PPArticlesEnPanier SET NbItems = " + nbItems + " WHERE NoPanier = " + noPanier, myConnection);
                commandeMAJQuantite.ExecuteNonQuery();
            }
            else
            {
                String dateCreation = DateTime.Now.ToShortDateString();
                String values = String.Join(",", noPanier, noClient, noVendeur, noProduit, dateCreation, nbItems);
                SqlCommand commandeAjout = new SqlCommand("INSERT INTO PPArticlesEnPanier VALUES (" + values + ")", myConnection);
                commandeAjout.ExecuteNonQuery();
            }
            myConnection.Close();
            
            Response.Redirect("Panier.aspx?noclient=" + noClient);
        }
    }
}