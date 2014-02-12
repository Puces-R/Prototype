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
    public partial class GestionCommandesVendeur : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.ChargerItems += ChargerCommandes;

            if (!IsPostBack)
            {
                if (Session["ID"] == null)
                {
                    Response.Redirect("Default.aspx", true);
                }
                //SELECT DISTINCT V.NoClient, (V.Nom +' ' +V.Prenom) AS NomComplet FROM PPClients V INNER JOIN PPCommandes C ON V.noClient = C.NoClient WHERE C.NoVendeur =10
                SqlDataAdapter adapteurVendeurs = new SqlDataAdapter("SELECT DISTINCT V.NoClient, (V.Nom +' ' +V.Prenom) AS NomComplet FROM PPClients V INNER JOIN PPCommandes C ON V.noClient = C.NoClient WHERE C.NoVendeur ="+Session["ID"], myConnection);
                DataTable tableVendeurs = new DataTable();
                adapteurVendeurs.Fill(tableVendeurs);

                ddlVendeur.DataSource = tableVendeurs;
                ddlVendeur.DataTextField = "NomComplet";
                ddlVendeur.DataValueField = "NoClient";
                ddlVendeur.DataBind();
                ddlVendeur.Items.Add(new ListItem("Tous", "-1") { Selected = true });

                ChargerCommandes();

                Master.AfficherPremierePage();
            }
        }

        protected void dlCommandes_OnItemDataBound(object sender, DataListItemEventArgs e)
        {
            DataListItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                BoiteCommande ctrCommande = (BoiteCommande)item.FindControl("ctrCommande");

                DataRowView drvCommande = (DataRowView)e.Item.DataItem;

                ctrCommande.NoCommande = (long)drvCommande["NoCommande"];
                ctrCommande.NoClient = (long)drvCommande["NoClient"];
            }
        }

        private void ChargerCommandes(object sender, EventArgs e)
        {
            ChargerCommandes();
        }

        private void ChargerCommandes()
        {
            String whereClause = " WHERE V.NoVendeur = " + Session["ID"];

            int noVendeur = int.Parse(ddlVendeur.SelectedValue);

            if (noVendeur != -1)
            {
                whereClause += " AND C.NoClient = " + noVendeur;
            }

            SqlDataAdapter adapteurCommandes = new SqlDataAdapter("SELECT C.NoCommande ,C.NoClient FROM PPCommandes C INNER JOIN PPVendeurs V ON C.NoVendeur = V.NoVendeur" + whereClause + " ORDER BY DateCommande DESC", myConnection);
            DataTable tableCommandes = new DataTable();
            adapteurCommandes.Fill(tableCommandes);

            PagedDataSource objPds = new PagedDataSource();
            objPds.DataSource = new DataView(tableCommandes);
            objPds.AllowPaging = true;
            objPds.PageSize = int.Parse(ddlParPage.SelectedValue);
            objPds.CurrentPageIndex = Master.PageActuelle;

            Master.NbPages = objPds.PageCount;

            dlCommandes.DataSource = objPds;
            dlCommandes.DataBind();

            mvCommandes.ActiveViewIndex = tableCommandes.Rows.Count == 0 ? 1 : 0;
        }

        protected void AfficherPremierePage(object sender, EventArgs e)
        {
            Master.AfficherPremierePage();
        }

        //SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        //        SqlDataAdapter adapteurProduits = new SqlDataAdapter("SELECT  * from PPCommandes where NoVendeur=" + Session["ID"] + "  ORDER BY Statut DESC, DateCommande DESC", myConnection);
        //        DataTable tableProduits = new DataTable();
        //        adapteurProduits.Fill(tableProduits);
        //        rptProduits.DataSource = tableProduits;
        //        rptProduits.DataBind();
        //    }
        //}


        //protected void rptProduits_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    RepeaterItem item = e.Item;

        //    if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
        //    {
        //        HyperLink lblNoProduit = (HyperLink)item.FindControl("hypCommande");
        //        Label lblNoClient = (Label)item.FindControl("lblNoClient");
        //        Label lblNoVendeur = (Label)item.FindControl("lblNoVendeur");
        //        Label lblDateCommande = (Label)item.FindControl("lblDateCommande");
        //        Label lblTypeLivraison = (Label)item.FindControl("lblTypeLivraison");
        //        Label lblMontantTotal = (Label)item.FindControl("lblMontantTotal");
        //        Label lblTPS = (Label)item.FindControl("lblTPS");
        //        Label lblTVQ = (Label)item.FindControl("lblTVQ");
        //        Label lblPoids = (Label)item.FindControl("lblPoidsTotal");
        //        Label lblStatut = (Label)item.FindControl("lblStatut");
        //        Label lblAutorisation = (Label)item.FindControl("lblNoAutorisation");
        //        Button btnMAJQuantite = (Button)item.FindControl("btnCommande");

        //         = (DataRowView)e.Item.DataItem;

        //        long noCommande = (long)drvFilm["NoCommande"];
        //        //String urlImage = "Images/Televerse/" + (String)drvFilm["Photo"];
        //        Int64 strCategorie = (Int64)drvFilm["NoClient"];
        //        Int64 noVendeur = (Int64)drvFilm["NoVendeur"];
        //        DateTime strDate = (DateTime)drvFilm["DateCommande"];
        //        //decimal decPrixDemande = (decimal)drvFilm["Livraison"];
        //        //short intQuantite = (short)drvFilm["TypeLivraison"];
        //        //Decimal noPanier = (Decimal)drvFilm["MontantTotal"];
        //        //Decimal tps = (Decimal)drvFilm["TPS"];
        //        //Decimal tvq = (Decimal)drvFilm["TVQ"];
        //        //Decimal poidstotal = (Decimal)drvFilm["PoidsTotal"];
        //        //String Statut = (String)drvFilm["Statut"];
        //        //String strAutorisation = (String)drvFilm["NoAutorisation"];



        //        String decPrixDemande = Convert.ToString(drvFilm["Livraison"].ToString().Replace(',', '.'));
        //        //Response.Write(decPrixDemande);
        //        String intQuantite = Convert.ToString(drvFilm["TypeLivraison"]);
        //        //Response.Write(intQuantite);
        //        String noPanier = Convert.ToString(drvFilm["MontantTotal"]);
        //        String tps = Convert.ToString(drvFilm["TPS"]);
        //        String tvq = Convert.ToString(drvFilm["TVQ"]);
        //        String poidstotal = Convert.ToString(drvFilm["PoidsTotal"]);
        //        //Decimal tps = (Decimal)drvFilm["TPS"];
        //        //Decimal tvq = (Decimal)drvFilm["TVQ"];
        //        //Decimal poidstotal = (Decimal)drvFilm["PoidsTotal"];

        //        String Statut = (String)drvFilm["Statut"];
        //        // String strAutorisation = (String)drvFilm["NoAutorisation"];


        //        lblNoProduit.Text = "No." + noCommande.ToString();
        //        lblNoProduit.NavigateUrl = "DetailsCommandes.aspx?noCommande=" + noCommande;
        //        // imgProduit.ImageUrl = urlImage;
        //        lblNoClient.Text = strCategorie.ToString();
        //        //lblNoVendeur.Text = noVendeur.ToString();
        //        lblDateCommande.Text = strDate.ToShortDateString();

        //        switch (intQuantite.ToString())
        //        {
        //            case "1": lblTypeLivraison.Text = "Poste régulière"; ; break;
        //            case "2": lblTypeLivraison.Text = "Poste prioritaire"; break;
        //            case "3": lblTypeLivraison.Text = "Transport privé"; break;
        //        }

        //        // lblTypeLivraison.Text = intQuantite.ToString();
        //        lblMontantTotal.Text = noPanier.ToString() + " $";
        //        lblTPS.Text = tps + " $";
        //        lblTVQ.Text = tvq + " $";
        //        lblPoids.Text = poidstotal.ToString() + " kg";
        //        switch (Statut)
        //        {
        //            case "p": lblStatut.Text = "Prêt pour livraison"; ; break;
        //            case "l": lblStatut.Text = "Livré"; break;
        //        }

        //        // lblAutorisation.Text = strAutorisation;
        //        btnMAJQuantite.CommandArgument = noCommande.ToString() + "-" + Statut;
        //    }
        //}

        ////protected void rptProduits_ItemDataBound(object sender, RepeaterItemEventArgs e)
        ////{
        ////    RepeaterItem item = e.Item;

        ////    if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
        ////    {


        ////        Label lblNoProduit = (Label)item.FindControl("lblNoCommande");
        ////        Label lblNoClient = (Label)item.FindControl("lblNoClient");
        ////        Label lblNoVendeur = (Label)item.FindControl("lblNoVendeur");
        ////        Label lblDateCommande = (Label)item.FindControl("lblDateCommande");
        ////        Label lblTypeLivraison = (Label)item.FindControl("lblTypeLivraison");
        ////        Label lblMontantTotal = (Label)item.FindControl("lblMontantTotal");
        ////        Label lblTPS = (Label)item.FindControl("lblTPS");
        ////        Label lblTVQ = (Label)item.FindControl("lblTVQ");
        ////        Label lblPoids = (Label)item.FindControl("lblPoidsTotal");
        ////        Label lblStatut = (Label)item.FindControl("lblStatut");
        ////        Label lblAutorisation = (Label)item.FindControl("lblNoAutorisation");
        ////        Button btnMAJQuantite = (Button)item.FindControl("btnMAJQuantite");

        ////         = (DataRowView)e.Item.DataItem;

        ////        //long noCommande = (long)drvFilm["NoCommande"];
        ////        ////String urlImage = "Images/Televerse/" + (String)drvFilm["Photo"];
        ////        //Int64 strCategorie = (Int64)drvFilm["NoClient"];
        ////        //Int64 noVendeur = (Int64)drvFilm["NoVendeur"];
        ////        //DateTime strDate = (DateTime)drvFilm["DateCommande"];
        ////        //decimal decPrixDemande = Convert.ToDecimal((String)drvFilm["Livraison"]);
        ////        //short intQuantite = (short)drvFilm["TypeLivraison"];
        ////        //Decimal noPanier = (Decimal)drvFilm["MontantTotal"];
        ////        //Decimal tps = (Decimal)drvFilm["TPS"];
        ////        //Decimal tvq = (Decimal)drvFilm["TVQ"];
        ////        //Decimal poidstotal = (Decimal)drvFilm["PoidsTotal"];
        ////        //String Statut = (String)drvFilm["Statut"];
        ////        //String strAutorisation = (String)drvFilm["NoAutorisation"];

        ////        long noCommande = (long)drvFilm["NoCommande"];
        ////        //String urlImage = "Images/Televerse/" + (String)drvFilm["Photo"];
        ////        Int64 strCategorie = (Int64)drvFilm["NoClient"];
        ////        Int64 noVendeur = (Int64)drvFilm["NoVendeur"];
        ////        DateTime strDate = (DateTime)drvFilm["DateCommande"];
        ////        //Response.Write(decimal.Parse(drvFilm["Livraison"].ToString().Replace(',', '.')));
        ////        String decPrixDemande = Convert.ToString(drvFilm["Livraison"].ToString().Replace(',','.'));
        ////        //Response.Write(decPrixDemande);
        ////        String intQuantite = Convert.ToString(drvFilm["TypeLivraison"]);
        ////        //Response.Write(intQuantite);
        ////        String noPanier = Convert.ToString(drvFilm["MontantTotal"]);
        ////        String tps = Convert.ToString(drvFilm["TPS"]);
        ////        String tvq = Convert.ToString(drvFilm["TVQ"]);
        ////        String poidstotal = Convert.ToString(drvFilm["PoidsTotal"]);
        ////        //Decimal tps = (Decimal)drvFilm["TPS"];
        ////        //Decimal tvq = (Decimal)drvFilm["TVQ"];
        ////        //Decimal poidstotal = (Decimal)drvFilm["PoidsTotal"];
        ////        //String Statut = (String)drvFilm["Statut"];
        ////        //String strAutorisation = (String)drvFilm["NoAutorisation"];

        ////        lblNoProduit.Text = "No. " + noCommande.ToString();
        ////        // imgProduit.ImageUrl = urlImage;
        ////        lblNoClient.Text = strCategorie.ToString();
        ////        lblNoVendeur.Text = noVendeur.ToString();
        ////        lblDateCommande.Text = "Livraison : " + strDate.ToShortDateString();
        ////        lblTypeLivraison.Text = intQuantite.ToString();
        ////        lblMontantTotal.Text = noPanier.ToString();
        ////        lblTPS.Text = tps;
        ////        lblTVQ.Text = tvq;
        ////        lblPoids.Text = poidstotal.ToString();
        ////        //lblStatut.Text = Statut;
        ////        //lblAutorisation.Text = strAutorisation;
        ////       // btnMAJQuantite.CommandArgument = noCommande.ToString() + "-" + Statut;
        ////    }
        ////}

        //protected void rptProduits_ItemCommand(object sender, RepeaterCommandEventArgs e)
        //{
        //    //TextBox txtQuantite = (TextBox)e.Item.FindControl("txtQuantite");

        //    SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        //    String[] statut = ((String)e.CommandArgument).Split('-');
        //    String noC = statut[0];
        //    String stat = statut[1];

        //    //Response.Write(noC + "----" + stat);
        //    myConnection.Open();

        //    if (stat == "l")
        //    {
        //        SqlCommand commandeMAJQuantite = new SqlCommand("UPDATE PPCommandes SET Statut ='p' WHERE NoCommande = " + noC, myConnection);
        //        commandeMAJQuantite.ExecuteNonQuery();
        //    }
        //    else if (stat == "p")
        //    {

        //        SqlCommand commandeMAJQuantite = new SqlCommand("UPDATE PPCommandes SET Statut ='l' WHERE NoCommande = " + noC, myConnection);
        //        commandeMAJQuantite.ExecuteNonQuery();
        //    }

        //    else
        //    {
        //        //Response.Write("ALLO");
        //    }
        //    // SqlCommand commandeMAJQuantite = new SqlCommand("UPDATE PPArticlesEnPanier SET NbItems = " + txtQuantite.Text + " WHERE NoPanier = " + e.CommandArgument, myConnection);
        //    // commandeMAJQuantite.ExecuteNonQuery();
        //    myConnection.Close();

        //    // calculerCouts();
        //}

    }
}