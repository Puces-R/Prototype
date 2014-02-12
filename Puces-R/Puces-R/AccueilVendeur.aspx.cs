﻿using System;
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
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        protected void Page_Load(object sender, EventArgs e)
        {
            myConnection.Open();
            SqlCommand maC = new SqlCommand("Select Count(*) from PPVendeursClients where NoVendeur="+Session["ID"],myConnection);
            object nb = (object)maC.ExecuteScalar();
            myConnection.Close();

            nbVisite.Text = nbVisite.Text + " : " + Convert.ToString(nb);

            SqlDataAdapter adapteurPaniers = new SqlDataAdapter("SELECT TOP 5 (C.Nom + ' ' + C.Prenom) AS NomC, C.NoClient,V.NomAffaires, A.NoVendeur, SUM(A.NbItems * P.PrixVente) AS SousTotal FROM PPArticlesEnPanier AS A INNER JOIN PPVendeurs AS V ON A.NoVendeur = V.NoVendeur INNER JOIN PPProduits AS P ON A.NoProduit = P.NoProduit inner join PPClients AS C on A.NoClient = C.NoClient where A.NoVendeur="+Session["ID"] +" GROUP BY V.NomAffaires, A.NoVendeur, C.Nom,C.Prenom,C.NoClient ORDER BY SousTotal DESC", myConnection);
            DataTable tablePaniers = new DataTable();
            adapteurPaniers.Fill(tablePaniers);

            rptPaniers.DataSource = new DataView(tablePaniers);
            rptPaniers.DataBind();

            SqlDataAdapter adapteurProduits = new SqlDataAdapter("SELECT TOP 5 * from PPCommandes where Statut='p' and NoVendeur="+Session["ID"] +" order by DateCommande DESC ", myConnection);
            DataTable tableProduits = new DataTable();
            adapteurProduits.Fill(tableProduits);
            rptCommandes.DataSource = tableProduits;
            rptCommandes.DataBind();
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
                lblNoProduit.NavigateUrl = "DetailsCommandes.aspx?noCommande=" + noCommande;
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
                HyperLink hypVendeur = (HyperLink)item.FindControl("hypVendeur");
                Label lblSousTotal = (Label)item.FindControl("lblSousTotal");
                Label lblNom = (Label)item.FindControl("lblNom");
                Label lblNo = (Label)item.FindControl("lblNumero");
                Label lblDate = (Label)item.FindControl("lblDateMAJ");

                //TablePanier ctrPanier = (TablePanier)item.FindControl("ctrPanier");

                DataRowView drvPanier = (DataRowView)e.Item.DataItem;

                String vendeur = (String)drvPanier["NomAffaires"];
                decimal sousTotal = (decimal)drvPanier["SousTotal"];
                long noVendeur = (long)drvPanier["NoVendeur"];
                long noClient = (long)drvPanier["NoClient"];

                
                //long numero = (long)drvPanier["NoClient"];
                //ctrPanier.NoClient = (long)numero;

                String date = "";
                SqlCommand maC = new SqlCommand("select top 1 DateCreation from PPArticlesEnPanier where NoVendeur="+Session["ID"] +" and NoClient="+noClient.ToString() +"order by DateCreation desc ", myConnection);
                myConnection.Open();

               SqlDataReader rep= maC.ExecuteReader();
               if (rep.Read()) 
               {
                   date = Convert.ToString((DateTime)rep[0]);
                   //Response.Write(rep[0].ToString()+" ----   ");
               }

               myConnection.Close();

               String nom = drvPanier["NomC"] == DBNull.Value ? "Nom Inconnu " : (String)drvPanier["NomC"];
               
                hypVendeur.Text = nom;
                hypVendeur.NavigateUrl = "Panier.aspx?noclient=" + noClient + "&novendeur=" + noVendeur;

                lblDate.Text = date;
                lblNom.Text = nom;
                lblNo.Text = noClient.ToString();
                //ctrPanier.NoClient = noClient;
                //ctrPanier.NoVendeur = (int)Session["ID"];

                lblSousTotal.Text = sousTotal.ToString("C");
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