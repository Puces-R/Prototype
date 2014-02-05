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
            ((SiteMaster)Master).Titre = "Le nom d'affaire";

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
                lb_vendeur.CommandArgument = results["NoVendeur"].ToString();

                ((SiteMaster)Master).Titre = results["NomAffaires"].ToString();

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
    }
}