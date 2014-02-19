﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace Puces_R
{
    public partial class details_redevance : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
        string whereClause, orderByClause = " ORDER BY ", mois;
        int no_vendeur;
        PagedDataSource pdsDemandes = new PagedDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Librairie.Autorisation(false, false, false, true);
            }
            List<String> whereParts = new List<String>();

            if (txtCritereRecherche.Text != string.Empty)
            {
                String colonne = " PPClients.Nom + PPClients.Prenom";
                switch (ddlTypeRecherche.SelectedIndex)
                {
                    case 0:
                        colonne = " PPClients.Nom + PPClients.Prenom ";
                        break;
                }
                whereParts.Add(" AND " + colonne + " LIKE '%" + txtCritereRecherche.Text + "%'");
            }

            //String whereClause;
            if (whereParts.Count > 0)
            {
                whereClause = " " + string.Join(" AND ", whereParts);
            }
            else
            {
                //whereClause = " WHERE Statut = 2 ";
            }

            //String orderByClause = " ORDER BY ";
            switch (ddlTrierPar.SelectedIndex)
            {
                case 0:
                    orderByClause += " NoCommande ";
                    break;
                case 1:
                    orderByClause += " PPClients.Nom ";
                    break;
                case 2:
                    orderByClause += " DateVente ";
                    break;
                case 3:
                    orderByClause += " MontantVente ";
                    break;
            }
            
            if (Session["msg"] != null)
                if (Session["msg"].ToString() != "")
                {
                    div_msg.InnerText = Session["msg"].ToString();
                    Session["msg"] = "";
                }

            if (Session["err_msg"] != null)
                if (Session["err_msg"].ToString() != "")
                {
                    Response.Write(Session["err_msg"]);
                    Session["err_msg"] = "";
                }

            if (Session["no_vendeur_no_commande"] != null)
                if (Session["no_vendeur_no_commande"].ToString() != "")
                {
                    string[] tab_parametres = Session["no_vendeur_no_commande"].ToString().Split(';');
                    no_vendeur = Convert.ToInt32(tab_parametres[0]);
                    mois = tab_parametres[1];

                    myConnection.Open();
                    SqlCommand charger = new SqlCommand("SELECT NomAffaires, Montant, Mois FROM PPVendeurs, PPSuiviCompta WHERE PPSuiviCompta.NoVendeur = PPVendeurs.NoVendeur AND PPSuiviCompta.NoVendeur = " + no_vendeur + " AND YEAR(Mois) = " + mois.Split('-')[0] + " AND MONTH(Mois) = " + mois.Split('-')[1], myConnection);

                    SqlDataReader results = charger.ExecuteReader();

                    if (results.Read())
                    {
                        Master.Master.Titre = "Détails de la redevance de \"" + results["NomAffaires"].ToString() + "\" pour le mois de " +
                            Convert.ToDateTime(results["Mois"]).ToString("MMMM yyyy").ToUpperInvariant(); 
                        lbl_total.Text = "Montant de la redevance de ce mois: " + Convert.ToDecimal(results["Montant"]).ToString("N") + " $";
                    }
                    results.Close();
                    myConnection.Close();
                }
                else Response.Redirect("Deconnexion.ashx");
            else Response.Redirect("Deconnexion.ashx");

            Master.ChargerItems += charge_details_redevance;

            if (!IsPostBack)
            {
                Master.AfficherPremierePage();
            } 
        }

        private void charge_details_redevance(object sender, EventArgs e)
        {
            charge_details_redevance();
        }

        protected void AfficherPremierePage(object sender, EventArgs e)
        {
            Master.AfficherPremierePage();
        }

        private DataTable charge_details_redevance()
        {
            string req = "";

            req += " SELECT NoCommande, PPClients.Nom, PPClients.Prenom, PPHistoriquePaiements.NoHistorique, PPHistoriquePaiements.Redevance, PPHistoriquePaiements.DateVente, PPVendeurs.NomAffaires ";
            req += " FROM PPHistoriquePaiements, PPClients, PPVendeurs ";
            req += " WHERE PPHistoriquePaiements.NoVendeur = " + no_vendeur;
            req += " AND PPVendeurs.NoVendeur = PPHistoriquePaiements.NoVendeur ";
            req += " AND PPHistoriquePaiements.NoClient = PPClients.NoClient ";
            req += " AND YEAR(PPHistoriquePaiements.DateVente) = YEAR('" + mois + "') ";
            req += " AND MONTH(PPHistoriquePaiements.DateVente) = MONTH('" + mois + "') " + whereClause + orderByClause;
            SqlDataAdapter adapteurDemandes = new SqlDataAdapter(req, myConnection);
            DataTable tableDemandes = new DataTable();
            adapteurDemandes.Fill(tableDemandes);
            //Response.Write(req );

            pdsDemandes.DataSource = new DataView(tableDemandes);
            pdsDemandes.AllowPaging = true;
            pdsDemandes.PageSize = int.Parse(ddlParPage.SelectedValue);

            pdsDemandes.CurrentPageIndex = Master.PageActuelle;
            Master.NbPages = pdsDemandes.PageCount;

            rptDetailsRedevance.DataSource = pdsDemandes;
            rptDetailsRedevance.DataBind();
            myConnection.Close();
            
            return tableDemandes;
        }

        protected void rptDetailsRedevance_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                LinkButton lbl_num = (LinkButton)item.FindControl("lbl_num");
                LinkButton lbl_nom_client = (LinkButton)item.FindControl("lbl_nom_client");
                LinkButton lbl_redevance = (LinkButton)item.FindControl("lbl_redevance");
                LinkButton date_vente = (LinkButton)item.FindControl("date_vente");

                DataRowView drvDemande = (DataRowView)e.Item.DataItem;

                lbl_num.CommandArgument = drvDemande["NoCommande"].ToString();
                lbl_nom_client.CommandArgument = drvDemande["NoCommande"].ToString();
                lbl_redevance.CommandArgument = drvDemande["NoCommande"].ToString();
                date_vente.CommandArgument = drvDemande["NoCommande"].ToString();

                lbl_num.Text = (pdsDemandes.CurrentPageIndex * pdsDemandes.PageSize + e.Item.ItemIndex + 1).ToString();
                lbl_nom_client.Text = drvDemande["Prenom"].ToString() + " " + drvDemande["Nom"].ToString();
                lbl_redevance.Text = Convert.ToDecimal(drvDemande["Redevance"]).ToString("N") + " $";
                date_vente.Text = drvDemande["DateVente"].ToString();

                string[] str_mois = drvDemande["DateVente"].ToString().Split('-');
                DateTime mois = new DateTime(Convert.ToInt32(str_mois[0]), Convert.ToInt32(str_mois[1]), 1);
            }
        }

        protected void voir_details_commande_redevance(object sender, CommandEventArgs e)
        {
            Session["no_commande_redevance"] = e.CommandArgument.ToString();
            Response.Redirect(Chemin.Ajouter("details_commande_redevance.aspx", "Retour à la liste des commandes"));
        }
    }
}