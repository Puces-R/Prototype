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
        private long NoVendeur
        {
            get
            {
                return (long)ViewState["NoVendeur"];
            }
            set
            {
                ViewState["NoVendeur"] = value;
            }
        }

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

                SqlCommand commandeProduit = new SqlCommand("SELECT P.NoProduit,Photo,P.Description,C.Description AS Categorie,P.Nom,PrixDemande,PrixVente,NombreItems,Poids,P.DateCreation,P.DateMAJ,V.NoVendeur,NomAffaires FROM PPProduits P INNER JOIN PPCategories C ON C.NoCategorie = P.NoCategorie INNER JOIN PPVendeurs V ON P.NoVendeur = V.NoVendeur" + whereClause, myConnection);

                myConnection.Open();

                SqlDataReader lecteurProduit = commandeProduit.ExecuteReader();
                lecteurProduit.Read();

                this.imgProduit.ImageUrl = "Images/Televerse/" + lecteurProduit["Photo"].ToString();

                this.lblProduit.Text = (String)lecteurProduit["Nom"];
                this.lblCategorie.Text = (String)lecteurProduit["Categorie"];
                this.lblDescription.Text = (String)lecteurProduit["Description"];
                this.lblPrixDemande.Text = ((decimal)lecteurProduit["PrixDemande"]).ToString("C");
                this.lblPrixEnVente.Text = ((decimal)lecteurProduit["PrixVente"]).ToString("C");
                this.lblQuantiteDisponible.Text = lecteurProduit["NombreItems"].ToString();
                this.lblDateCreation.Text = ((DateTime)lecteurProduit["DateCreation"]).ToShortDateString();
                ((SiteMaster)Master).Vendeur = (String)lecteurProduit["NomAffaires"];

                this.NoVendeur = (long)lecteurProduit["NoVendeur"];

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
                String values = String.Join(",", noPanier, noClient, NoVendeur, noProduit, dateCreation, nbItems);
                SqlCommand commandeAjout = new SqlCommand("INSERT INTO PPArticlesEnPanier VALUES (" + values + ")", myConnection);
                commandeAjout.ExecuteNonQuery();
            }
            myConnection.Close();

            Response.Redirect("Panier.aspx?noclient=" + noClient + "&novendeur=" + NoVendeur);
        }
    }
}