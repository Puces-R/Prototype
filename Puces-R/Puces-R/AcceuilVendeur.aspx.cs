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
    public partial class AcceuilVendeur : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        protected void Page_Load(object sender, EventArgs e)
        {
            myConnection.Open();
            SqlCommand maC = new SqlCommand("Select Count(*) from PPVendeursClients where NoVendeur=20",myConnection);
            object nb = (object)maC.ExecuteScalar();
            myConnection.Close();

            nbVisite.Text = nbVisite.Text + " : " + Convert.ToString(nb);

            SqlDataAdapter adapteurPaniers = new SqlDataAdapter("SELECT (C.Nom + C.Prenom) AS NomC, C.NoClient,V.NomAffaires, A.NoVendeur, SUM(A.NbItems * P.PrixVente) AS SousTotal FROM PPArticlesEnPanier AS A INNER JOIN PPVendeurs AS V ON A.NoVendeur = V.NoVendeur INNER JOIN PPProduits AS P ON A.NoProduit = P.NoProduit inner join PPClients AS C on A.NoClient = C.NoClient where A.NoVendeur="+Session["ID"] +" GROUP BY V.NomAffaires, A.NoVendeur, C.Nom,C.Prenom,C.NoClient", myConnection);
            DataTable tablePaniers = new DataTable();
            adapteurPaniers.Fill(tablePaniers);

            rptPaniers.DataSource = new DataView(tablePaniers);
            rptPaniers.DataBind();

            SqlDataAdapter adapteurProduits = new SqlDataAdapter("SELECT * from PPCommandes where Statut='I'", myConnection);
            DataTable tableProduits = new DataTable();
            adapteurProduits.Fill(tableProduits);
            rptProduits.DataSource = tableProduits;
            rptProduits.DataBind();
        }

        protected void rptCommandes_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            //TextBox txtQuantite = (TextBox)e.Item.FindControl("txtQuantite");

            SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

            String[] statut = ((String)e.CommandArgument).Split('-');
            String noC = statut[0];
            String stat = statut[1];

           // Response.Write(noC + "----" + stat);
            myConnection.Open();

            if (stat == "O")
            {
                SqlCommand commandeMAJQuantite = new SqlCommand("UPDATE PPCommandes SET Statut ='I' WHERE NoCommande = " + noC, myConnection);
                commandeMAJQuantite.ExecuteNonQuery();
            }
            else if (stat == "I")
            {

                SqlCommand commandeMAJQuantite = new SqlCommand("UPDATE PPCommandes SET Statut ='O' WHERE NoCommande = " + noC, myConnection);
                commandeMAJQuantite.ExecuteNonQuery();
            }

            else
            {
                Response.Write("ALLO");
            }
            // SqlCommand commandeMAJQuantite = new SqlCommand("UPDATE PPArticlesEnPanier SET NbItems = " + txtQuantite.Text + " WHERE NoPanier = " + e.CommandArgument, myConnection);
            // commandeMAJQuantite.ExecuteNonQuery();
            myConnection.Close();

            // calculerCouts();
        }

        protected void rptCommandes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {


                HyperLink lblNoProduit = (HyperLink)item.FindControl("hypCommande");
                Label lblNoClient = (Label)item.FindControl("lblNoClient");
                Label lblNoVendeur = (Label)item.FindControl("lblNoVendeur");
                Label lblDateCommande = (Label)item.FindControl("lblDateCommande");
                Label lblTypeLivraison = (Label)item.FindControl("lblTypeLivraison");
                Label lblMontantTotal = (Label)item.FindControl("lblMontantTotal");
                Label lblTPS = (Label)item.FindControl("lblTPS");
                Label lblTVQ = (Label)item.FindControl("lblTVQ");
                Label lblPoids = (Label)item.FindControl("lblPoidsTotal");
                Label lblStatut = (Label)item.FindControl("lblStatut");
                Label lblAutorisation = (Label)item.FindControl("lblNoAutorisation");
                Button btnMAJQuantite = (Button)item.FindControl("btnMAJQuantite");

                DataRowView drvFilm = (DataRowView)e.Item.DataItem;

                long noCommande = (long)drvFilm["NoCommande"];
                //String urlImage = "Images/Televerse/" + (String)drvFilm["Photo"];
                Int64 strCategorie = (Int64)drvFilm["NoClient"];
                Int64 noVendeur = (Int64)drvFilm["NoVendeur"];
                DateTime strDate = (DateTime)drvFilm["DateCommande"];
                decimal decPrixDemande = (decimal)drvFilm["Livraison"];
                short intQuantite = (short)drvFilm["TypeLivraison"];
                Decimal noPanier = (Decimal)drvFilm["MontantTotal"];
                Decimal tps = (Decimal)drvFilm["TPS"];
                Decimal tvq = (Decimal)drvFilm["TVQ"];
                Decimal poidstotal = (Decimal)drvFilm["PoidsTotal"];
                String Statut = (String)drvFilm["Statut"];
                String strAutorisation = (String)drvFilm["NoAutorisation"];

                lblNoProduit.Text = "No." + noCommande.ToString();
                lblNoProduit.NavigateUrl = "DetailsCommandes.aspx?noCommande="+noCommande;
                // imgProduit.ImageUrl = urlImage;
                lblNoClient.Text = strCategorie.ToString();
                lblNoVendeur.Text = noVendeur.ToString();
                lblDateCommande.Text = "Livraison : " + strDate.ToShortDateString();
                lblTypeLivraison.Text = intQuantite.ToString();
                lblMontantTotal.Text = noPanier.ToString();
                lblTPS.Text = tps.ToString("C");
                lblTVQ.Text = tvq.ToString("C");
                lblPoids.Text = poidstotal.ToString();
                lblStatut.Text = Statut;
                lblAutorisation.Text = strAutorisation;
                //btnMAJQuantite.CommandArgument = noCommande.ToString() + "-" + Statut;
            }
        }

        protected void rptPaniers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                HyperLink hypVendeur = (HyperLink)item.FindControl("hypVendeur");
                Label lblSousTotal = (Label)item.FindControl("lblSousTotal");
                Repeater rptProduits = (Repeater)item.FindControl("rptProduits");

                DataRowView drvPanier = (DataRowView)e.Item.DataItem;

                String vendeur = (String)drvPanier["NomAffaires"];
                decimal sousTotal = (decimal)drvPanier["SousTotal"];
                long noVendeur = (long)drvPanier["NoVendeur"];

                hypVendeur.Text = vendeur;
                hypVendeur.NavigateUrl = "Panier.aspx?noclient=10000&novendeur=" + noVendeur;
                lblSousTotal.Text = sousTotal.ToString("C");

                SqlDataAdapter adapteurProduits = new SqlDataAdapter("SELECT Nom, NbItems, PrixVente, A.NoProduit FROM PPArticlesEnPanier A INNER JOIN PPProduits P ON A.NoProduit = P.NoProduit WHERE A.NoVendeur = " + noVendeur + " AND A.NoClient = 10000", myConnection);
                DataTable tableProduits = new DataTable();
                adapteurProduits.Fill(tableProduits);

                rptProduits.DataSource = new DataView(tableProduits);
                rptProduits.DataBind();
            }
        }

        protected void rptProduits_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                HyperLink hypProduit = (HyperLink)item.FindControl("hypProduit");
                Label lblQuantite = (Label)item.FindControl("lblQuantite");
                Label lblPrixUnitaire = (Label)item.FindControl("lblPrixUnitaire");
                Label lblPrixTotal = (Label)item.FindControl("lblPrixTotal");

                DataRowView drvProduit = (DataRowView)e.Item.DataItem;

                String produit = (String)drvProduit["Nom"];
                short quantite = (short)drvProduit["NbItems"];
                decimal prixUnitaire = (decimal)drvProduit["PrixVente"];
                decimal prixTotal = quantite * prixUnitaire;
                long noProduit = (long)drvProduit["NoProduit"];

                hypProduit.Text = produit;
                hypProduit.NavigateUrl = "DetailsProduit.aspx?noclient=10000&noproduit=" + noProduit;
                lblQuantite.Text = quantite.ToString();
                lblPrixUnitaire.Text = prixUnitaire.ToString("C");
                lblPrixTotal.Text = prixTotal.ToString("C");
            }
        }
    }
}