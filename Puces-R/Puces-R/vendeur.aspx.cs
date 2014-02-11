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
    public partial class vendeur : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
        int no_vendeur;

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.Titre = "Le nom d'affaire";

            if (Session["selected_vendeur"] != null)
            {
                if (Session["selected_vendeur"].ToString() != "")
                    no_vendeur = Convert.ToInt32(Session["selected_vendeur"].ToString());
            }
            //else Response.Redirect("connexion.aspx");

            charger_info();
            mvVendeur.SetActiveView(View1);
        }

        protected void selectionner_vendeur(object sender, CommandEventArgs e)
        {
            Session["hist_vendeur"] = e.CommandArgument;
            Response.Redirect("en_construction.aspx");
        }

        protected void changer_view(object sender, CommandEventArgs e)
        {
            if (e != null)
            {
                switch (Convert.ToInt32(e.CommandArgument.ToString()))
                {
                    case 1:
                        generer_stat();
                        mvVendeur.SetActiveView(View2);
                        break;
                    case 2:
                        mvVendeur.SetActiveView(View3);
                        break;
                    default:
                        charger_info();
                        mvVendeur.SetActiveView(View1);
                        break;
                }
            }
            else
            {
                charger_info();
                mvVendeur.SetActiveView(View1);
            }
        }
        
        protected void charger_info()
        {
            myConnection.Open();
            SqlCommand charger = new SqlCommand("SELECT * FROM PPVendeurs WHERE NoVendeur = " + no_vendeur, myConnection);

            SqlDataReader results = charger.ExecuteReader();

            if (results.Read())
            {
                lbl_nom_complet.Text = results["Prenom"].ToString() + " " + results["Nom"].ToString();
                lbl_adresse.Text = results["Rue"].ToString() + ", " + results["Ville"].ToString() + ", " + results["Pays"].ToString();
                lbl_charge_max.Text = results["MaxLivraison"].ToString() + " Kg";
                lbl_courriel.Text = results["AdresseEmail"].ToString();
                lbl_date_insc.Text = results["DateCreation"].ToString();
                lbl_date_maj.Text = results["DateMAJ"].ToString();
                lbl_livraison_gratuite.Text = "$" + results["LivraisonGratuite"].ToString();
                //lb_vendeur.CommandArgument = results["NoVendeur"].ToString();

                Master.Titre = results["NomAffaires"].ToString();

                switch (results["Statut"].ToString())
                {
                    case "0":
                        lbl_statut.Text = "Actif";
                        break;
                    case "1":
                        lbl_statut.Text = "Inactif";
                        break;
                    case "2":
                        lbl_statut.Text = "En attende d'approbation";
                        break;
                    case "3":
                        lbl_statut.Text = "En retard de payement";
                        break;
                }

                lbl_taux_redevence.Text = results["Pourcentage"].ToString();
                lbl_taxes.Text = (results["Taxes"].ToString() == "True" ? "Oui" : "Non");
                lbl_tel1.Text = results["Tel1"].ToString();
                lbl_tel2.Text = results["Tel2"].ToString();
            }
            myConnection.Close();
        }

        protected void generer_stat()
        {
            chargerBestClients();
            chargerDonneesGraphiques();
            mvVendeur.SetActiveView(View2);
        }

        private DataTable chargerBestClients()
        {
            string req = "SELECT PPClients.NoClient, PPClients.Nom, PPClients.Prenom, R1.total FROM PPClients INNER JOIN (SELECT NoClient, SUM(MontantTotal) AS total FROM PPCommandes WHERE (NoVendeur = " + no_vendeur + ") GROUP BY NoClient) AS R1 ON PPClients.NoClient = R1.NoClient ORDER BY R1.total DESC";
                        
            SqlDataAdapter adapteurResultats = new SqlDataAdapter(req , myConnection);
            DataTable tableResultats = new DataTable();
            //Response.Write(ddlCategorie.SelectedValue + req + orderByClause);
            adapteurResultats.Fill(tableResultats);
            myConnection.Close();

            PagedDataSource objPds = new PagedDataSource();
            objPds.DataSource = new DataView(tableResultats);
            objPds.AllowPaging = true;
            objPds.PageSize = 5;
            objPds.CurrentPageIndex = 0;

            lbl_nb_clients.Text = tableResultats.Rows.Count.ToString();
            
            rptBestClients.DataSource = objPds;
            rptBestClients.DataBind();
            
            return tableResultats;
        }

        protected void rptBestClients_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {

                Label nom_meilleur_client = (Label)item.FindControl("nom_meilleur_client");
                Label total_commande_meilleur_client = (Label)item.FindControl("total_commande_meilleur_client");

                DataRowView drvDemande = (DataRowView)e.Item.DataItem;

                nom_meilleur_client.Text = drvDemande["Prenom"].ToString() + " " + drvDemande["Nom"].ToString();
                total_commande_meilleur_client.Text = "$" + drvDemande["total"].ToString();
            }
        }

        protected void chargerDonneesGraphiques(object sender, EventArgs e)
        {
            chargerDonneesGraphiques();
        }

        protected void chargerDonneesGraphiques()
        {
            string req = "";
            SqlCommand charger;
            string js_tab = "", script = "<script type='text/javascript' > window.onload = function () { onLoadDoc(); onLoadDoc2(); } \n var chart1, chart2; function onLoadDoc() { chart1 = new cfx.Chart(); chart1.getAnimations().getLoad().setEnabled(true); chart1.setGallery(cfx.Gallery.Bar); chart1.getView3D().setEnabled(true); chart1.getView3D().setRotated(true);chart1.getView3D().setAngleX(30);  chart1.getView3D().setAngleY(-20); chart1.getView3D().setBoxThickness(10);   chart1.getView3D().setDepth(160); chart1.getView3D().setShadow(cfx.Shadow.Fixed); var items = [";
            myConnection.Open();

            for (int i = 0; i < Convert.ToInt32(ddlNbMois.SelectedValue); i++)
            {
                req += " SELECT        SUM(MontantTotal) AS SommeMontants ";
                req += " FROM            PPCommandes ";
                req += " WHERE        (NoVendeur = " + no_vendeur + ")" ;
                req += " AND DateCommande BETWEEN DATEADD(mm, " + -i + ", CAST(RTRIM(LTRIM(STR(YEAR(GETDATE())))) + '-' + RTRIM(LTRIM(STR(MONTH(GETDATE())))) + '-01' AS SMALLDATETIME)) ";
                req += " AND DATEADD(dd, -1, DATEADD(mm, " + ((i == 0 ? 1 : -i + 1)) + ", CAST(RTRIM(LTRIM(STR(YEAR(GETDATE())))) + '-' + RTRIM(LTRIM(STR(MONTH(GETDATE())))) + '-01' AS SMALLDATETIME))) ";
                req += " GROUP BY CAST(RTRIM(LTRIM(STR(YEAR(DateCommande)))) + '-' + RTRIM(LTRIM(STR(MONTH(DateCommande)))) + '-01' AS SMALLDATETIME); ";

                charger = new SqlCommand(req, myConnection);

                object results = charger.ExecuteScalar();
                js_tab = "{'Ventes':" + Convert.ToInt32(results) + ",'Mois':'" + DateTime.Now.AddMonths(-i).ToString("MMM") + "'}," + js_tab;

                req = "";
            }
            script += js_tab + " ];chart1.setDataSource(items); var chartDiv = document.getElementById('ChartDiv1');chart1.create(chartDiv);} function onLoadDoc2() { chart2 = new cfx.Chart(); chart2.getAnimations().getLoad().setEnabled(true); PopulateCarProduction(chart2); chart2.setGallery(cfx.Gallery.Pie); var data = chart2.getData(); data.setPoints(6); var titles = chart2.getTitles(); var title = new cfx.TitleDockable(); title.setText('Répartition des clients par catégorie'); titles.add(title); chart2.getAllSeries().getPointLabels().setVisible(true);  } function PopulateCarProduction(chart2) { var items = [{ 'Nombre de clients dans cette catégorie': val1, 'Catégorie': 'Potentiels' }, { 'Nombre de clients dans cette catégorie': val2, 'Catégorie': 'Actifs' }, { 'Nombre de clients dans cette catégorie': val3, 'Catégorie': 'Visiteurs' }]; chart2.setDataSource(items); var chartDiv2 = document.getElementById('ChartDiv2'); chart2.create(chartDiv2); } </script>";

            req = "SELECT (SELECT COUNT(*) AS Expr1  FROM (SELECT DISTINCT NoClient FROM PPVendeursClients WHERE (NoVendeur = " + no_vendeur + " )) AS derivedtbl_1) AS Expr1, (SELECT COUNT(*) AS Expr1 FROM (SELECT DISTINCT NoClient  FROM PPArticlesEnPanier WHERE (NoVendeur = " + no_vendeur + " )) AS derivedtbl_2) AS Expr2, (SELECT COUNT(*) AS Expr1 FROM (SELECT DISTINCT NoClient FROM PPCommandes WHERE (NoVendeur = " + no_vendeur + " )) AS derivedtbl_3) AS Expr3";
            charger = new SqlCommand(req, myConnection);
            SqlDataReader results2 = charger.ExecuteReader();

            if (results2.Read())
            {
                script = script.Replace("val1", results2[1].ToString()).Replace("val2", results2[2].ToString()).Replace("val3", results2[0].ToString());
            }

            RegisterClientScriptBlock("script1", script);
            mvVendeur.SetActiveView(View2);
            myConnection.Close();
        }
    }
}