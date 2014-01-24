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
    public partial class Panier : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                calculerCouts();
            }
        }

        private void calculerCouts()
        {
            int noClient;
            if (!int.TryParse(Request.Params["noclient"], out noClient))
            {
                Response.Redirect("Default.aspx", true);
            }

            String whereClause = " WHERE A.NoClient = " + noClient;

            SqlDataAdapter adapteurProduits = new SqlDataAdapter("SELECT P.NoProduit,Photo,C.Description,Nom,PrixDemande,NombreItems,Poids,A.NbItems,A.NoPanier FROM PPProduits P INNER JOIN PPCategories C ON C.NoCategorie = P.NoCategorie INNER JOIN PPArticlesEnPanier A ON A.NoProduit = P.NoProduit" + whereClause, myConnection);
            DataTable tableProduits = new DataTable();
            adapteurProduits.Fill(tableProduits);

            decimal sousTotal = 0;
            decimal poidsTotal = 0;

            foreach (DataRow produit in tableProduits.Rows)
            {
                short nbItems = (short)produit["NbItems"];
                sousTotal += nbItems * (decimal)produit["PrixDemande"];
                poidsTotal += nbItems * (decimal)produit["Poids"];
            }

            lblPoidsTotal.Text = poidsTotal.ToString() + " lbs.";
            lblSousTotal.Text = sousTotal.ToString("C");

            myConnection.Open();

            SqlCommand commandeLivraison = new SqlCommand("SELECT P.Tarif FROM PPTypesPoids T INNER JOIN PPPoidsLivraisons P ON T.CodePoids = P.CodePoids WHERE P.CodeLivraison = 1 AND " + poidsTotal.ToString().Replace(",", ".") + " BETWEEN T.PoidsMin AND T.PoidsMax", myConnection);
            decimal prixLivraison = (decimal)commandeLivraison.ExecuteScalar();

            decimal prixAvecLivraison = sousTotal + prixLivraison;

            SqlCommand commandeTauxTPS = new SqlCommand("SELECT TOP(1) TauxTPS FROM PPTaxeFederale ORDER BY DateEffectiveTPS DESC", myConnection);
            decimal tauxTPS = ((decimal)commandeTauxTPS.ExecuteScalar()) / 100;

            SqlCommand commandeTauxTVQ = new SqlCommand("SELECT TOP(1) TauxTVQ FROM PPTaxeProvinciale ORDER BY DateEffectiveTVQ DESC", myConnection);
            decimal tauxTVQ = ((decimal)commandeTauxTVQ.ExecuteScalar()) / 100;

            decimal prixTPS = prixAvecLivraison * tauxTPS;
            decimal prixTVQ = prixAvecLivraison * tauxTVQ;

            decimal grandTotal = prixAvecLivraison + prixTPS + prixTVQ;

            lblLivraison.Text = prixLivraison.ToString("C");
            lblTauxTPS.Text = "(" + tauxTPS.ToString("P") + ")";
            lblTauxTVQ.Text = "(" + tauxTVQ.ToString("P") + ")";
            lblTPS.Text = prixTPS.ToString("C");
            lblTVQ.Text = prixTVQ.ToString("C");
            lblGrandTotal.Text = grandTotal.ToString("C");

            myConnection.Close();

            rptProduits.DataSource = new DataView(tableProduits);
            rptProduits.DataBind();
        }

        protected void rptProduits_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {

                Label lblNoProduit = (Label)item.FindControl("lblNoProduit");
                Image imgProduit = (Image)item.FindControl("imgProduit");
                Label lblCategorie = (Label)item.FindControl("lblCategorie");
                Label lblDescriptionAbregee = (Label)item.FindControl("lblDescriptionAbregee");
                Label lblPrixDemande = (Label)item.FindControl("lblPrixDemande");
                TextBox txtQuantite = (TextBox)item.FindControl("txtQuantite");
                Button btnMAJQuantite = (Button)item.FindControl("btnMAJQuantite");

                DataRowView drvFilm = (DataRowView)e.Item.DataItem;

                long noProduit = (long)drvFilm["NoProduit"];
                String urlImage = "Images/Televerse/" + (String)drvFilm["Photo"];
                String strCategorie = (String)drvFilm["Description"];
                String strDescriptionAbregee = (String)drvFilm["Nom"];
                decimal decPrixDemande = (decimal)drvFilm["PrixDemande"];
                short intQuantite = (short)drvFilm["NbItems"];
                long noPanier = (long)drvFilm["NoPanier"];

                lblNoProduit.Text = "No. " + noProduit.ToString();
                imgProduit.ImageUrl = urlImage;
                lblCategorie.Text = strCategorie;
                lblDescriptionAbregee.Text = strDescriptionAbregee;
                lblPrixDemande.Text = "Prix demandé: " + decPrixDemande.ToString("C");
                txtQuantite.Text = intQuantite.ToString();
                btnMAJQuantite.CommandArgument = noPanier.ToString();
            }
        }

        protected void rptProduits_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            TextBox txtQuantite = (TextBox)e.Item.FindControl("txtQuantite");

            SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

            myConnection.Open();
            if (txtQuantite.Text == "0")
            {
                SqlCommand commandeSuppression = new SqlCommand("DELETE FROM PPArticlesEnPanier WHERE NoPanier = " + e.CommandArgument, myConnection);
                commandeSuppression.ExecuteNonQuery();
            }
            else
            {
                SqlCommand commandeMAJQuantite = new SqlCommand("UPDATE PPArticlesEnPanier SET NbItems = " + txtQuantite.Text + " WHERE NoPanier = " + e.CommandArgument, myConnection);
                commandeMAJQuantite.ExecuteNonQuery();
            }
            myConnection.Close();

            calculerCouts();
        }
    }
}