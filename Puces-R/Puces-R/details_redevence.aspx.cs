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
    public partial class details_redevence : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
        string whereClause, orderByClause = " ORDER BY ", mois;
        int no_vendeur;

        protected void Page_Load(object sender, EventArgs e)
        {
            List<String> whereParts = new List<String>();

            if (txtCritereRecherche.Text != string.Empty)
            {
                String colonne = "Nom";
                switch (ddlTypeRecherche.SelectedIndex)
                {
                    case 0:
                        colonne = "Nom";
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
                    orderByClause += "NoCommande";
                    break;
                case 1:
                    orderByClause += "Nom";
                    break;
                case 2:
                    orderByClause += "DateVente";
                    break;
                case 3:
                    orderByClause += "MontantVente";
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
                }
                else Response.Redirect("Default.aspx");
            else Response.Redirect("Default.aspx");

            Master.Master.Titre = "Détails de la redevence";
            Master.ChargerItems += charge_details_redevence;

            if (!IsPostBack)
            {
                Master.AfficherPremierePage();
            } 
        }

        private void charge_details_redevence(object sender, EventArgs e)
        {
            charge_details_redevence();
        }

        protected void AfficherPremierePage(object sender, EventArgs e)
        {
            Master.AfficherPremierePage();
        }

        private DataTable charge_details_redevence()
        {
            string req = "";

            req += " SELECT NoCommande, PPClients.Nom, PPClients.Prenom, PPHistoriquePaiements.NoHistorique, PPHistoriquePaiements.Redevance, PPHistoriquePaiements.DateVente ";
            req += " FROM PPHistoriquePaiements, PPClients ";
            req += " WHERE PPHistoriquePaiements.NoVendeur = " + no_vendeur;
            req += " AND PPHistoriquePaiements.NoClient = PPClients.NoClient ";
            req += " AND YEAR(PPHistoriquePaiements.DateVente) = YEAR('" + mois + "') ";
            req += " AND MONTH(PPHistoriquePaiements.DateVente) = MONTH('" + mois + "') " + whereClause + orderByClause;
            SqlDataAdapter adapteurDemandes = new SqlDataAdapter(req, myConnection);
            DataTable tableDemandes = new DataTable();
            adapteurDemandes.Fill(tableDemandes);
            //Response.Write(req );

            PagedDataSource pdsDemandes = new PagedDataSource();
            pdsDemandes.DataSource = new DataView(tableDemandes);
            pdsDemandes.AllowPaging = true;
            pdsDemandes.PageSize = int.Parse(ddlParPage.SelectedValue);

            pdsDemandes.CurrentPageIndex = Master.PageActuelle;
            Master.NbPages = pdsDemandes.PageCount;

            rptDetailsRedevence.DataSource = pdsDemandes;
            rptDetailsRedevence.DataBind();
            myConnection.Close();

            return tableDemandes;
        }

        protected void rptDetailsRedevence_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                Label lbl_num = (Label)item.FindControl("lbl_num");
                Label lbl_nom_client = (Label)item.FindControl("lbl_nom_client");
                Label lbl_redevance = (Label)item.FindControl("lbl_redevance");
                Label date_vente = (Label)item.FindControl("date_vente");
                Button btn_voir_details_commande_redevance = (Button)item.FindControl("btn_voir_details_commande_redevance");

                DataRowView drvDemande = (DataRowView)e.Item.DataItem;

                lbl_num.Text = (e.Item.ItemIndex + 1).ToString();
                lbl_nom_client.Text = drvDemande["Prenom"].ToString() + " " + drvDemande["Nom"].ToString();
                lbl_redevance.Text = drvDemande["Redevance"].ToString();
                date_vente.Text = drvDemande["DateVente"].ToString();
                btn_voir_details_commande_redevance.CommandArgument = drvDemande["NoCommande"].ToString();
            }
        }

        protected void voir_details_commande_redevance(object sender, CommandEventArgs e)
        {
            Session["no_commande_redevance"] = e.CommandArgument.ToString();
            Response.Redirect("details_commande_redevance.aspx");
        }
    }
}