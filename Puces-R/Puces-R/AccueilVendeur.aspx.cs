using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Puces_R.Controles;

namespace Puces_R 
{
    public partial class AccueilVendeur : System.Web.UI.Page
    {
        SqlConnection myConnection = Librairie.Connexion;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Librairie.Autorisation(false, false, true, false);
            }
            myConnection.Open();
            SqlCommand maC = new SqlCommand("Select Count(*) from PPVendeursClients where NoVendeur="+Session["ID"],myConnection);
            object nb = (object)maC.ExecuteScalar();
            SqlCommand commandeNotes = new SqlCommand("select AVG(Cote) FROM PPEvaluations  where SUBSTRING( CAST(NoProduit as varchar(50)),0,3) ="+Session["ID"],myConnection);
            object note = (object)commandeNotes.ExecuteScalar();
            myConnection.Close();

            //Response.Write(note.ToString());
            if (note is DBNull)
            {
                lblEvaluation.Visible = false;
            }
            else
            {
                ctrEtoiles.Cote = Convert.ToDecimal(note);
            }

            nbVisite.Text = Convert.ToString(nb);

            SqlDataAdapter adapteurPaniers = new SqlDataAdapter("SELECT TOP 5 A.NoClient, C.Prenom + ' ' + C.Nom AS NomComplet FROM PPArticlesEnPanier A INNER JOIN PPClients C ON A.NoClient = C.NoClient WHERE A.NoVendeur = " + Session["ID"] + " GROUP BY A.NoClient, A.NoVendeur, C.Prenom, C.Nom ORDER BY MAX(A.DateCreation)", myConnection);
            DataTable tablePaniers = new DataTable();
            adapteurPaniers.Fill(tablePaniers);

            rptPaniers.DataSource = new DataView(tablePaniers);
            rptPaniers.DataBind();

            mvPanier.ActiveViewIndex = tablePaniers.Rows.Count == 0 ? 1 : 0;

            SqlDataAdapter adapteurProduits = new SqlDataAdapter("SELECT TOP 5 * from PPCommandes where Statut='p' and NoVendeur="+Session["ID"] +" order by DateCommande DESC ", myConnection);
            DataTable tableProduits = new DataTable();
            adapteurProduits.Fill(tableProduits);

            rptCommandes.DataSource = tableProduits;
            rptCommandes.DataBind();

            mvCommandes.ActiveViewIndex = tableProduits.Rows.Count == 0 ? 1 : 0;
        }

        protected void rptCommandes_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            //TextBox txtQuantite = (TextBox)e.Item.FindControl("txtQuantite");

            SqlConnection myConnection = Librairie.Connexion;

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
                //Response.Write("ALLO");
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
                
                DataRowView drvCommande = (DataRowView)e.Item.DataItem;

                long noCommande = (long)drvCommande["NoCommande"];
                //String urlImage = "Images/Televerse/" + (String)drvFilm["Photo"];
                Int64 strCategorie = (Int64)drvCommande["NoClient"];
                Int64 noVendeur = (Int64)drvCommande["NoVendeur"];
                DateTime strDate = (DateTime)drvCommande["DateCommande"];
                //decimal decPrixDemande = (decimal)drvFilm["Livraison"];
                //short intQuantite = (short)drvFilm["TypeLivraison"];
                //Decimal noPanier = (Decimal)drvFilm["MontantTotal"];
                //Decimal tps = (Decimal)drvFilm["TPS"];
                //Decimal tvq = (Decimal)drvFilm["TVQ"];
                //Decimal poidstotal = (Decimal)drvFilm["PoidsTotal"];
                //String Statut = (String)drvFilm["Statut"];
                //String strAutorisation = (String)drvFilm["NoAutorisation"];



                String decPrixDemande = Convert.ToString(drvCommande["Livraison"].ToString().Replace(',', '.'));
                //Response.Write(decPrixDemande);
                String intQuantite = Convert.ToString(drvCommande["TypeLivraison"]);
                //Response.Write(intQuantite);
                String noPanier = Convert.ToString(drvCommande["MontantTotal"]);
                String tps = Convert.ToString(drvCommande["TPS"]);
                String tvq = Convert.ToString(drvCommande["TVQ"]);
                String poidstotal = Convert.ToString(drvCommande["PoidsTotal"]);
                //Decimal tps = (Decimal)drvFilm["TPS"];
                //Decimal tvq = (Decimal)drvFilm["TVQ"];
                //Decimal poidstotal = (Decimal)drvFilm["PoidsTotal"];

                String Statut = (String)drvCommande["Statut"];
               // String strAutorisation = (String)drvFilm["NoAutorisation"];


                lblNoProduit.Text = "No." + noCommande.ToString();
                lblNoProduit.NavigateUrl = Chemin.Ajouter("DetailsCommandes.aspx?noCommande=" + noCommande,"Retour à l'Accueil");
                // imgProduit.ImageUrl = urlImage;
                lblNoClient.Text = strCategorie.ToString();
                //lblNoVendeur.Text = noVendeur.ToString();
                lblDateCommande.Text = strDate.ToShortDateString();

                switch (intQuantite.ToString())
                {
                    case "1": lblTypeLivraison.Text = "Poste régulière"; ; break;
                    case "2": lblTypeLivraison.Text = "Poste prioritaire"; break;
                    case "3": lblTypeLivraison.Text = "Transport privé"; break;
                }

               // lblTypeLivraison.Text = intQuantite.ToString();
                lblMontantTotal.Text = noPanier.ToString()+" $";
                lblTPS.Text = tps+" $";
                lblTVQ.Text = tvq+" $";
                lblPoids.Text = poidstotal.ToString()+" kg";
                switch (Statut) 
                {
                    case "p": lblStatut.Text = "Prêt pour livraison";  ; break;
                    case "l": lblStatut.Text = "Livré" ; break;
                }
                
               // lblAutorisation.Text = strAutorisation;
                //btnMAJQuantite.CommandArgument = noCommande.ToString() + "-" + Statut;
            }
        }

        protected void rptPaniers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                BoitePanier ctrBoitePanier = (BoitePanier)item.FindControl("ctrBoitePanier");

                DataRowView drvPanier = (DataRowView)e.Item.DataItem;

                long noClient = (long)drvPanier["NoClient"];

                string nomComplet;
                if (drvPanier["NomComplet"] is DBNull)
                {
                    nomComplet = "Client #" + noClient;
                }
                else
                {
                    nomComplet = (string)drvPanier["NomComplet"];
                }

                ctrBoitePanier.NoVendeur = (int)Session["ID"];
                ctrBoitePanier.NoClient = noClient;
                ctrBoitePanier.Titre = nomComplet;
                ctrBoitePanier.ChargerArticlesEnPanier();
            }
        }

        //protected void rptProduits_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    RepeaterItem item = e.Item;

        //    if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
        //    {
        //        HyperLink hypProduit = (HyperLink)item.FindControl("hypProduit");
        //        Label lblQuantite = (Label)item.FindControl("lblQuantite");
        //        Label lblPrixUnitaire = (Label)item.FindControl("lblPrixUnitaire");
        //        Label lblPrixTotal = (Label)item.FindControl("lblPrixTotal");

        //        DataRowView drvProduit = (DataRowView)e.Item.DataItem;

        //        String produit = (String)drvProduit["Nom"];
        //        short quantite = (short)drvProduit["NbItems"];
        //        decimal prixUnitaire = (decimal)drvProduit["PrixVente"];
        //        decimal prixTotal = quantite * prixUnitaire;
        //        long noProduit = (long)drvProduit["NoProduit"];

        //        hypProduit.Text = produit;
        //        hypProduit.NavigateUrl = "DetailsProduit.aspx?noclient=10000&noproduit=" + noProduit;
        //        lblQuantite.Text = quantite.ToString();
        //        lblPrixUnitaire.Text = prixUnitaire.ToString("C");
        //        lblPrixTotal.Text = prixTotal.ToString("C");
        //    }
        //}
    }
}