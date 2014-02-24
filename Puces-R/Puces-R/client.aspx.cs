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
    public partial class client : System.Web.UI.Page
    {
        SqlConnection myConnection = Librairie.Connexion;
        int no_client, min_option = 5, nb_option = 5, increment = 1;
        long[] dest = new long[1];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Librairie.Autorisation(false, false, false, true);
                visualiser_stats_rapports.ajouter_list_item(ddlNbMois_c1, min_option, nb_option, increment);
                visualiser_stats_rapports.ajouter_list_item(ddlNbMois_c2, min_option, nb_option, increment);
            }
            Master.Titre = "Gérer le client";

            if (Session["selected_client"] != null)
            {
                if (Session["selected_client"].ToString() != "")
                    no_client = Convert.ToInt32(Session["selected_client"].ToString());
                dest[0] = no_client;
            }
            //else Response.Redirect("connexion.aspx");

            charger_info();
            mvVendeur.SetActiveView(View1);

            if (Session["msg"] != null)
                if (Session["msg"].ToString() != "")
                {
                    div_msg.InnerText = Session["msg"].ToString();
                    Session["msg"] = null;
                }

            if (Session["err_msg"] != null)
                if (Session["err_msg"].ToString() != "")
                {
                    Response.Write(Session["err_msg"]);
                    Session["err_msg"] = null;
                }
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
                        Librairie.Messagerie(dest, null, null, true, "Retour à la page de gestion du client");
                        break;
                    case 3:
                        Librairie.Courriel(dest, null, null, true, "Retour à la page de gestion du client");
                        break;
                    case 4:
                        Session["desactiver_client"] = no_client.ToString();
                        Session["retour_desactiver_client"] = "client.aspx";
                        Response.Redirect(Chemin.Ajouter("verdict_desactiver_client.aspx", "Retour à la page de gestion du client"));
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
            SqlCommand charger = new SqlCommand("SELECT * FROM PPClients WHERE NoClient = " + no_client, myConnection);

            SqlDataReader results = charger.ExecuteReader();

            if (results.Read())
            {
                lbl_nom_complet.Text = results["Prenom"].ToString() + " " + results["Nom"].ToString();
                lbl_adresse.Text = results["Rue"].ToString() + ", " + results["Ville"].ToString() + ", " + results["Pays"].ToString();
                lbl_nb_connexion.Text = results["NbConnexions"].ToString();
                lbl_courriel.Text = results["AdresseEmail"].ToString();
                lbl_date_insc.Text = results["DateCreation"].ToString();
                lbl_date_maj.Text = results["DateMAJ"].ToString();
                
                switch (results["Statut"].ToString())
                {
                    case "1":
                        lbl_statut.Text = "Inactif";
                        lb_desactiver.Enabled = false;
                        break;
                    default:
                        lbl_statut.Text = "Actif";
                        break;
                }

                if (results["Tel1"] != DBNull.Value)
                    lbl_tel1.Text = Telephone.Format(results["Tel1"].ToString());
                if (results["Tel2"] != DBNull.Value)
                    lbl_tel2.Text = Telephone.Format(results["Tel1"].ToString());
            }
            myConnection.Close();
        }

        protected void generer_stat()
        {
            chargerFavVendeurs();
            chargerDonneesGraphiques1();
            chargerDonneesGraphiques2();
            mvVendeur.SetActiveView(View2);
        }
        protected void generer_stat(object sender, EventArgs e)
        {
            generer_stat();
        }

        private DataTable chargerFavVendeurs()
        {
            string req = "SELECT PPVendeurs.NomAffaires, R1.total FROM PPVendeurs INNER JOIN (SELECT NoVendeur, SUM(MontantTotal) AS total FROM PPCommandes WHERE (NoClient = " + 
                no_client + ") GROUP BY NoVendeur) AS R1 ON PPVendeurs.NoVendeur = R1.NoVendeur ORDER BY R1.total DESC";

            SqlDataAdapter adapteurResultats = new SqlDataAdapter(req, myConnection);
            DataTable tableResultats = new DataTable();
            //Response.Write(ddlCategorie.SelectedValue + req + orderByClause);
            adapteurResultats.Fill(tableResultats);
            myConnection.Close();

            PagedDataSource objPds = new PagedDataSource();
            objPds.DataSource = new DataView(tableResultats);
            objPds.AllowPaging = true;
            objPds.PageSize = 5;
            objPds.CurrentPageIndex = 0;

            lbl_nb_vendeurs.Text = tableResultats.Rows.Count.ToString();

            rptFavVendeurs.DataSource = objPds;
            rptFavVendeurs.DataBind();

            return tableResultats;
        }

        protected void rptFavVendeurs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {

                Label nom_vendeur_favoris = (Label)item.FindControl("nom_vendeur_favoris");
                Label total_commande_vendeur_favoris = (Label)item.FindControl("total_commande_vendeur_favoris");

                DataRowView drvDemande = (DataRowView)e.Item.DataItem;

                nom_vendeur_favoris.Text = drvDemande["NomAffaires"].ToString();
                total_commande_vendeur_favoris.Text = Convert.ToDecimal(drvDemande["Total"]).ToString("N") + " $";
            }
        }

        protected void chargerDonneesGraphiques1(object sender, EventArgs e)
        {
            chargerDonneesGraphiques1();
        }

        protected void chargerDonneesGraphiques1()
        {
            string req = "";
            SqlCommand charger;
            string js_tab = "";
            myConnection.Open();

            for (int i = 0; i < Convert.ToInt32(ddlNbMois_c1.SelectedValue); i++)
            {
                req += " SELECT        SUM(MontantTotal) AS SommeMontants ";
                req += " FROM            PPCommandes ";
                req += " WHERE        (NoClient = " + no_client + ")";
                req += " AND DateCommande BETWEEN DATEADD(mm, " + -i + ", CAST(RTRIM(LTRIM(STR(YEAR(GETDATE())))) + '-' + RTRIM(LTRIM(STR(MONTH(GETDATE())))) + '-01' AS SMALLDATETIME)) ";
                req += " AND DATEADD(dd, -1, DATEADD(mm, " + ((i == 0 ? 1 : -i + 1)) + ", CAST(RTRIM(LTRIM(STR(YEAR(GETDATE())))) + '-' + RTRIM(LTRIM(STR(MONTH(GETDATE())))) + '-01' AS SMALLDATETIME))) ";
                req += " GROUP BY CAST(RTRIM(LTRIM(STR(YEAR(DateCommande)))) + '-' + RTRIM(LTRIM(STR(MONTH(DateCommande)))) + '-01' AS SMALLDATETIME); ";

                charger = new SqlCommand(req, myConnection);

                object results = charger.ExecuteScalar();
                js_tab = "{'Achats':" + Convert.ToInt32(results) + ",'Mois':'" + DateTime.Now.AddMonths(-i).ToString("MMM") + "'}," + js_tab;

                req = "";
            }

            visualiser_stats_rapports.generer_script("c1", js_tab, true, Page.ClientScript, false);
            mvVendeur.SetActiveView(View2);
            myConnection.Close();
        }

        protected void chargerDonneesGraphiques2(object sender, EventArgs e)
        {
            chargerDonneesGraphiques2();
        }

        protected void chargerDonneesGraphiques2()
        {
            string req = "";
            SqlCommand charger;
            string js_tab = "";
            myConnection.Open();

            for (int i = 0; i < Convert.ToInt32(ddlNbMois_c2.SelectedValue); i++)
            {
                req += " SELECT COUNT(MontantTotal) AS SommeMontants ";
                req += " FROM PPCommandes ";
                req += " WHERE (NoClient = " + no_client + ")";
                req += " AND DateCommande BETWEEN DATEADD(mm, " + -i + ", CAST(RTRIM(LTRIM(STR(YEAR(GETDATE())))) + '-' + RTRIM(LTRIM(STR(MONTH(GETDATE())))) + '-01' AS SMALLDATETIME)) ";
                req += " AND DATEADD(dd, -1, DATEADD(mm, " + ((i == 0 ? 1 : -i + 1)) + ", CAST(RTRIM(LTRIM(STR(YEAR(GETDATE())))) + '-' + RTRIM(LTRIM(STR(MONTH(GETDATE())))) + '-01' AS SMALLDATETIME))) ";
                req += " GROUP BY CAST(RTRIM(LTRIM(STR(YEAR(DateCommande)))) + '-' + RTRIM(LTRIM(STR(MONTH(DateCommande)))) + '-01' AS SMALLDATETIME); ";

                charger = new SqlCommand(req, myConnection);

                object results = charger.ExecuteScalar();
                js_tab = "{'Nombre de commandes':" + Convert.ToInt32(results) + ",'Mois':'" + DateTime.Now.AddMonths(-i).ToString("MMM") + "'}," + js_tab;

                req = "";
            }

            visualiser_stats_rapports.generer_script("c2", js_tab, true, Page.ClientScript, true);
            mvVendeur.SetActiveView(View2);
            myConnection.Close();
        }
   }
}