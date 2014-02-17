﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Puces_R
{
    public partial class visualiser_stats_rapports : System.Web.UI.Page
    {
        SqlConnection myConnection = Librairie.Connexion;

        protected void Page_Load(object sender, EventArgs e)
        {
            int min_option = 5, nb_option = 5, increment = 1;

            Master.Titre = "Statistiques & Rapports";
            chargerGraphiquev0(sender, e);

            if (!IsPostBack)
            {
                Librairie.Autorisation(false, false, false, true);
                ajouter_list_item(ddlNbMois_v1, min_option, nb_option, increment);
                ajouter_list_item(ddlNbVendeurs_v2, min_option, nb_option, increment);
                ajouter_list_item(ddlNbVendeurs_v3, min_option, nb_option, increment);
                ajouter_list_item(ddlNbVendeurs_v4, min_option, nb_option, increment);
            }
        }
        
        protected void chargerGraphiquev1(object sender, EventArgs e)
        {
            string req = "";
            string js_tab = "";
            SqlCommand charger;
            myConnection.Open();

            for (int i = 0; i < Convert.ToInt32(ddlNbMois_v1.SelectedValue); i++)
            {
                req += " SELECT COUNT(*) AS NbNouveauxVendeurs ";
                req += " FROM PPVendeurs ";
                req += " WHERE DateCreation BETWEEN DATEADD(mm, " + -i + ", CAST(RTRIM(LTRIM(STR(YEAR(GETDATE())))) + '-' + RTRIM(LTRIM(STR(MONTH(GETDATE())))) + '-01' AS SMALLDATETIME)) ";
                req += " AND DATEADD(dd, -1, DATEADD(mm, " + ((i == 0 ? 1 : -i + 1)) + ", CAST(RTRIM(LTRIM(STR(YEAR(GETDATE())))) + '-' + RTRIM(LTRIM(STR(MONTH(GETDATE())))) + '-01' AS SMALLDATETIME))) ";

                charger = new SqlCommand(req, myConnection);

                object results = charger.ExecuteScalar();
                js_tab = "{'Nouveaux vendeurs':" + Convert.ToInt32(results) + ",'Mois':'" + DateTime.Now.AddMonths(-i).ToString("MMM") + "'}," + js_tab;

                req = "";
            }

            generer_script("v1", js_tab, true);
            myConnection.Close();
            mvStats.SetActiveView(v1);
        }

        protected void chargerGraphiquev2(object sender, EventArgs e)
        {
            string req = "";
            string js_tab = "";
            SqlCommand charger;
            myConnection.Open();
                       
            req += " SELECT TOP (" + ddlNbVendeurs_v2.SelectedValue + ") NomAffaires, SUM(MontantTotal) Total ";
            req += " FROM PPVendeurs, PPCommandes ";
            req += " WHERE PPVendeurs.NoVendeur =  PPCommandes.NoVendeur ";
            req += " GROUP BY NomAffaires ";
            req += " ORDER BY SUM(MontantTotal) DESC ";

            charger = new SqlCommand(req, myConnection);
            SqlDataReader results = charger.ExecuteReader();

            while (results.Read())
                js_tab = "{'Total des ventes':" + results["Total"].ToString().Replace(',', '.') + ",'Vendeur':\"" + results["NomAffaires"].ToString() + "\"}," + js_tab;

            generer_script("v2", js_tab, false);
            myConnection.Close();
            mvStats.SetActiveView(v2);
        }

        protected void chargerGraphiquev3(object sender, EventArgs e)
        {
            string req = "";
            string js_tab = "";
            SqlCommand charger;
            myConnection.Open();

            req += " SELECT TOP (" + ddlNbVendeurs_v3.SelectedValue + ") NomAffaires, COUNT(*) Visites ";
            req += " FROM PPVendeurs, PPVendeursClients ";
            req += " WHERE PPVendeurs.NoVendeur =  PPVendeursClients.NoVendeur ";
            req += " GROUP BY NomAffaires ";
            req += " ORDER BY COUNT(*) DESC ";

            charger = new SqlCommand(req, myConnection);
            SqlDataReader results = charger.ExecuteReader();

            while (results.Read())
                js_tab = "{'Nombre de visites':" + results["Visites"].ToString().Replace(',', '.') + ",'Vendeur':\"" + results["NomAffaires"].ToString() + "\"}," + js_tab;

            generer_script("v3", js_tab, false);
            myConnection.Close();
            mvStats.SetActiveView(v3);
        }

        protected void chargerGraphiquev4(object sender, EventArgs e)
        {
            string req = "";
            string js_tab = "";
            SqlCommand charger;
            myConnection.Open();

            req += " SELECT TOP (" + ddlNbVendeurs_v4.SelectedValue + ") NomAffaires, COUNT(DISTINCT NoClient) Paniers ";
            req += " FROM PPVendeurs, PPArticlesEnPanier ";
            req += " WHERE PPVendeurs.NoVendeur =  PPArticlesEnPanier.NoVendeur ";
            req += " GROUP BY NomAffaires ";
            req += " ORDER BY COUNT(DISTINCT NoClient) DESC ";

            charger = new SqlCommand(req, myConnection);
            SqlDataReader results = charger.ExecuteReader();

            while (results.Read())
                js_tab = "{'Nombre de paniers':" + results["Paniers"].ToString().Replace(',', '.') + ",'Vendeur':\"" + results["NomAffaires"].ToString() + "\"}," + js_tab;

            generer_script("v4", js_tab, false);
            myConnection.Close();
            mvStats.SetActiveView(v4);
        }

        protected void chargerGraphiquev0(object sender, EventArgs e)
        {
            string js_tab = "";
            int total_vendeur = 0;
            SqlCommand charger = new SqlCommand("SELECT Statut, COUNT(*) NbVendeurs FROM PPVendeurs GROUP BY Statut ", myConnection);
            myConnection.Open();
            SqlDataReader results = charger.ExecuteReader();

            while (results.Read())
            {
                switch (results["Statut"].ToString())
                {
                    case "0" :
                        js_tab = "{ 'Nombre de vendeurs dans cette catégorie': " + results["NbVendeurs"].ToString() + ", 'Catégorie': 'Actifs' }, " + js_tab;
                        total_vendeur += Convert.ToInt32(results["NbVendeurs"]);
                        break;
                    case "1":
                        js_tab = "{ 'Nombre de vendeurs dans cette catégorie': " + results["NbVendeurs"].ToString() + ", 'Catégorie': 'Inactifs' }, " + js_tab;
                        total_vendeur += Convert.ToInt32(results["NbVendeurs"]);
                        break;
                    case "2":
                        js_tab = "{ 'Nombre de vendeurs dans cette catégorie': " + results["NbVendeurs"].ToString() + ", 'Catégorie': \"En attente d'approbation\" }, " + js_tab;
                        total_vendeur += Convert.ToInt32(results["NbVendeurs"]);
                        break;
                }            
            }
            lbl_total_vendeurs.Text = total_vendeur.ToString();
            generer_script_pie("v0", js_tab, "Répartition des vendeurs selon le statut");
            myConnection.Close();
            mvStats.SetActiveView(v0);
        }

        protected void chargerGraphiquec1(object sender, EventArgs e)
        {

            mvStats.SetActiveView(c1);
        }

        protected void chargerGraphiquec2(object sender, EventArgs e)
        {

            mvStats.SetActiveView(c2);
        }

        protected void chargerGraphiquec3(object sender, EventArgs e)
        {

            mvStats.SetActiveView(c3);
        }

        protected void chargerGraphiquec4(object sender, EventArgs e)
        {

            mvStats.SetActiveView(c4);
        }

        protected void chargerGraphiquec0(object sender, EventArgs e)
        {

            mvStats.SetActiveView(c0);
        }

        protected void generer_script(string view, string js_tab, bool trois_d)
        {
            string script = "";

            script += " <script type='text/javascript' > ";
            script += " 	window.onload =  ";
            script += " 		function ()  ";
            script += " 		{  ";
            script += " 			onLoadDoc();  ";
            script += " 		} \n  ";
            script += " 		var chart1;  ";
            script += " 		function onLoadDoc()  ";
            script += " 		{  ";
            script += " 			chart1 = new cfx.Chart();  ";
            script += " 			chart1.getAnimations().getLoad().setEnabled(true);  ";
            script += " 			chart1.setGallery(cfx.Gallery.Bar);  ";
            if (trois_d)
            {
                script += " 			chart1.getView3D().setEnabled(true);  ";
                script += " 			chart1.getView3D().setRotated(true); ";
                script += " 			chart1.getView3D().setAngleX(30);   ";
                script += " 			chart1.getView3D().setAngleY(-20);  ";
                script += " 			chart1.getView3D().setBoxThickness(10);    ";
                script += " 			chart1.getView3D().setDepth(160);  ";
            }
            script += " 			chart1.getView3D().setShadow(cfx.Shadow.Fixed);  ";
            script += " 			var items = [";
            script += js_tab;
            script += " 			]; ";
            script += " 			chart1.setDataSource(items);  ";
            script += " 			var chartDiv = document.getElementById('chart_" + view + "'); ";
            script += " 			chart1.create(chartDiv); ";
            script += " 		}  ";
            script += " </script> ";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script_" + view, script, false);
        }

        protected void generer_script_pie(string view, string js_tab, string titre)
        {
            string script = "";

            script += " <script type='text/javascript' > ";
            script += " 	window.onload =  ";
            script += " 		function ()  ";
            script += " 		{  ";
            script += " 			onLoadDoc2();  ";
            script += " 		} \n  ";
            script += " 		var chart2;  ";
            script += " 		function onLoadDoc2()  ";
            script += " 		{  ";
            script += " 			chart2 = new cfx.Chart();  ";
            script += " 			chart2.getAnimations().getLoad().setEnabled(true);  ";
            script += " 			PopulateCarProduction(chart2);  ";
            script += " 			chart2.setGallery(cfx.Gallery.Pie);  ";
            script += " 			var data = chart2.getData();  ";
            script += " 			data.setPoints(6);  ";
            script += " 			var titles = chart2.getTitles();  ";
            script += " 			var title = new cfx.TitleDockable(); ";
            script += " 			title.setText('" + titre + "');  ";
            script += " 			titles.add(title);  ";
            script += " 			chart2.getAllSeries().getPointLabels().setVisible(true);   ";
            script += " 		}  ";
            script += " 		function PopulateCarProduction(chart2)  ";
            script += " 		{ var items = [";
            script += js_tab;
            script += " 			]; chart2.setDataSource(items);  ";
            script += " 			var chartDiv2 = document.getElementById('chart_" + view + "');  ";
            script += " 			chart2.create(chartDiv2);  ";
            script += " 		}  ";
            script += " </script> ";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script_" + view, script, false);
        }

        protected void ajouter_list_item(DropDownList ddl, int premier, int nb, int increment)
        {
            ddl.Items.Add(new ListItem(null, premier.ToString(), true));

            for (int i = premier + increment; i <= premier + nb * increment; i += increment)
                ddl.Items.Add(new ListItem(null, i.ToString()));
        }
    }
}