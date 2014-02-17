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
    public partial class accueil_gestionnaire : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Librairie.Autorisation(false, false, false, true);
            }
            ((SiteMaster)Master).Titre = "Accueil";

            rptMeilleursClients.DataSource = chargerMeilleursClients();
            rptMeilleursClients.DataBind();

            rptMeilleursVendeurs.DataSource = chargerMeilleursVendeurs();
            rptMeilleursVendeurs.DataBind();

            rptDemandes.DataSource = chargerDemandes();
            rptDemandes.DataBind();

            chargeStats();
        }

        private DataTable chargerMeilleursClients()
        {
            string req = "";
            req += " SELECT Prenom, Nom, Total, PPClients.NoClient ";
            req += " FROM PPClients, ( ";
            req += " 					SELECT TOP 5 NoClient, SUM(MontantTotal) Total ";
            req += " 					FROM PPCommandes ";
            req += " 					WHERE MONTH(DateCommande) = MONTH(GETDATE()) ";
            req += " 					AND YEAR(DateCommande) = YEAR(GETDATE()) ";
            req += " 					GROUP BY NoClient ";
            req += " 					ORDER BY SUM(MontantTotal) DESC ";
            req += " 				) R2 ";
            req += " WHERE PPClients.NoClient = R2.NoClient ";

            return returnRes(req);
        }

        private DataTable chargerMeilleursVendeurs()
        {
            string req = "";
            req += " SELECT NomAffaires, Prenom, Nom, Total ";
            req += " FROM PPVendeurs, ( ";
            req += " 					SELECT TOP 5 NoVendeur, SUM(MontantTotal) Total ";
            req += " 					FROM PPCommandes ";
            req += " 					WHERE MONTH(DateCommande) = MONTH(GETDATE()) ";
            req += " 					AND YEAR(DateCommande) = YEAR(GETDATE()) ";
            req += " 					GROUP BY NoVendeur ";
            req += " 					ORDER BY SUM(MontantTotal) DESC ";
            req += " 				) R2 ";
            req += " WHERE PPVendeurs.NoVendeur = R2.NoVendeur ";

            return returnRes(req);
        }

        private DataTable chargerDemandes()
        {
            string req = "";
            req += " SELECT        TOP (5) NomAffaires, Prenom, Nom ";
            req += " FROM            PPVendeurs ";
            req += " WHERE        (Statut = 2) ";
            req += " ORDER BY DateCreation DESC ";

            return returnRes(req);
        }

        private DataTable returnRes(string req)
        {
            SqlDataAdapter adapteurResultats = new SqlDataAdapter(req, myConnection);
            DataTable tableResultats = new DataTable();
            //Response.Write(ddlCategorie.SelectedValue + req + orderByClause);
            adapteurResultats.Fill(tableResultats);
            myConnection.Close();

            return tableResultats;
        }

        protected void rptMeilleursClients_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                Label lbl_meilleur_client = (Label)item.FindControl("lbl_meilleur_client");

                DataRowView drvVendeurs = (DataRowView)e.Item.DataItem;

                lbl_meilleur_client.Text = drvVendeurs["Prenom"].ToString() + " " + drvVendeurs["Nom"].ToString() + ": " + drvVendeurs["Total"].ToString() + "$";
            }
        }

        protected void rptMeilleursVendeurs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                Label lbl_meilleur_vendeur = (Label)item.FindControl("lbl_meilleur_vendeur");

                DataRowView drvVendeurs = (DataRowView)e.Item.DataItem;

                lbl_meilleur_vendeur.Text = drvVendeurs["NomAffaires"].ToString() + ", par " + drvVendeurs["Prenom"].ToString() + " " + drvVendeurs["Nom"].ToString() + ": " + drvVendeurs["Total"].ToString() + "$";
            }
        }

        protected void rptDemandes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                Label lbl_nom_demande = (Label)item.FindControl("lbl_nom_demande");

                DataRowView drvVendeurs = (DataRowView)e.Item.DataItem;

                lbl_nom_demande.Text = drvVendeurs["NomAffaires"].ToString() + ", par " + drvVendeurs["Prenom"].ToString() + " " + drvVendeurs["Nom"].ToString();
            }
        }

        private void chargeStats()
        {
            string req = "";

            req += " SELECT ( SELECT COUNT(*) FROM PPVendeurs WHERE MONTH(DateCreation) = MONTH(GETDATE()) AND YEAR(DateCreation) = YEAR(GETDATE())) nbNouveauxVendeurs,  ";
            req += " 	( SELECT COUNT(*) FROM PPVendeurs ) nbVendeurs, ";
            req += " 	( SELECT COUNT(*) FROM PPClients WHERE MONTH(DateCreation) = MONTH(GETDATE()) AND YEAR(DateCreation) = YEAR(GETDATE()) ) nbNouveauxClients, ";
            req += " 	( SELECT COUNT(*) FROM PPClients ) nbClients, ";
            req += " 	( SELECT COUNT(*) FROM PPVendeurs WHERE Statut = 2 ) nbDemandes ";

            myConnection.Open();
            SqlCommand charger = new SqlCommand(req, myConnection);

            SqlDataReader results = charger.ExecuteReader();

            if (results.Read())
            {
                lbl_nb_nouv_vendeurs.Text = results["nbNouveauxVendeurs"].ToString();
                lbl_nb_vendeurs.Text = results["nbVendeurs"].ToString();
                lbl_nb_nouv_clients.Text = results["nbNouveauxClients"].ToString();
                lbl_nb_clients.Text = results["nbClients"].ToString();
                lbl_nb_demandes.Text = results["nbDemandes"].ToString();
            }

            myConnection.Close();
        }
    }
}