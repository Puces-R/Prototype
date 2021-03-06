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
    public partial class histo_redevance_vendeur : System.Web.UI.Page
    {
        SqlConnection myConnection = Librairie.Connexion;
        string whereClause, orderByClause = " ORDER BY ";
        int no_vendeur;
        PagedDataSource pdsDemandes = new PagedDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Librairie.Autorisation(false, false, false, true);
            }
            List<String> whereParts = new List<String>();
                        
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
                    orderByClause += " Mois ";
                    break;
                case 1:
                    orderByClause += " DatePaiement ";
                    break;
                case 2:
                    orderByClause += " Montant ";
                    break;
            }
            orderByClause += ddlOrdre.SelectedValue;

            if (Session["msg"] != null)
                if (Session["msg"].ToString() != "")
                {
                    div_msg.InnerText = Session["msg"].ToString();
                    Session["msg"] = "";
                }

            if (Session["err_msg"] != null)
                if (Session["err_msg"].ToString() != "")
                {
                    
                    Session["err_msg"] = "";
                }

            if (Session["histo_no_vendeur"] != null)
                if (Session["histo_no_vendeur"].ToString() != "")
                {
                    no_vendeur = Convert.ToInt32(Session["histo_no_vendeur"]);
                }
                else Librairie.RefuserAutorisation();
            else Librairie.RefuserAutorisation();

            Master.ChargerItems += charge_redevances;

            if (!IsPostBack)
            {
                Master.AfficherPremierePage();
            }

            myConnection.Open();
            SqlCommand commande_enregistrer = new SqlCommand("SELECT NomAffaires FROM PPVendeurs WHERE NoVendeur = " + no_vendeur, myConnection);
            object no_affaire = commande_enregistrer.ExecuteScalar();
            Master.Master.Titre = "Historique des redevances de \"" + no_affaire.ToString() + "\"";

            myConnection.Close();
        }

        private void charge_redevances(object sender, EventArgs e)
        {
            charge_redevances();
        }

        protected void AfficherPremierePage(object sender, EventArgs e)
        {
            Master.AfficherPremierePage();
        }

        private DataTable charge_redevances()
        {
            myConnection.Open();
            SqlDataAdapter adapteurDemandes = new SqlDataAdapter("SELECT * FROM PPSuiviCompta WHERE NoVendeur = " + no_vendeur + " " + orderByClause, myConnection);
            DataTable tableDemandes = new DataTable();
            adapteurDemandes.Fill(tableDemandes);
            //

            pdsDemandes.DataSource = new DataView(tableDemandes);
            pdsDemandes.AllowPaging = true;
            pdsDemandes.PageSize = int.Parse(ddlParPage.SelectedValue);

            pdsDemandes.CurrentPageIndex = Master.PageActuelle;
            Master.NbPages = pdsDemandes.PageCount;

            rptRetard.DataSource = pdsDemandes;
            rptRetard.DataBind();
            myConnection.Close();

            return tableDemandes;
        }

        protected void rptRetard_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                LinkButton lbl_num = (LinkButton)item.FindControl("lbl_num");
                LinkButton lbl_mois = (LinkButton)item.FindControl("lbl_mois");
                LinkButton date_paiement = (LinkButton)item.FindControl("date_paiement");
                LinkButton lbl_montant = (LinkButton)item.FindControl("lbl_montant");
                Button btn_enregistrer_paiement = (Button)item.FindControl("btn_enregistrer_paiement");

                DataRowView drvDemande = (DataRowView)e.Item.DataItem;

                string[] tab_date = drvDemande["Mois"].ToString().Split('-');
                DateTime mois = new DateTime(Convert.ToInt32(tab_date[0]), Convert.ToInt32(tab_date[1]), Convert.ToInt32(tab_date[2].Remove(3)));

                lbl_num.Text = (pdsDemandes.CurrentPageIndex * pdsDemandes.PageSize + e.Item.ItemIndex + 1).ToString();
                lbl_mois.Text = mois.ToString("MMMM yyyy").ToUpperInvariant();
                date_paiement.Text = (drvDemande["DatePaiement"] == DBNull.Value ? "Non payé" : drvDemande["DatePaiement"].ToString());
                lbl_montant.Text = Convert.ToDecimal(drvDemande["Montant"]).ToString("N") + " $";
                btn_enregistrer_paiement.CommandArgument = drvDemande["NoVendeur"].ToString() + ";" + drvDemande["Mois"].ToString();
                btn_enregistrer_paiement.Visible = (drvDemande["DatePaiement"] == DBNull.Value);

                lbl_num.CommandArgument = drvDemande["NoVendeur"].ToString() + ";" + drvDemande["Mois"].ToString();
                lbl_mois.CommandArgument = drvDemande["NoVendeur"].ToString() + ";" + drvDemande["Mois"].ToString();
                date_paiement.CommandArgument = drvDemande["NoVendeur"].ToString() + ";" + drvDemande["Mois"].ToString();
                lbl_montant.CommandArgument = drvDemande["NoVendeur"].ToString() + ";" + drvDemande["Mois"].ToString();

                if (drvDemande["DatePaiement"] == DBNull.Value)
                {
                    System.Web.UI.HtmlControls.HtmlTableRow ligne_histo = (System.Web.UI.HtmlControls.HtmlTableRow)item.FindControl("ligne_histo");
                    ligne_histo.Style.Add("background-color", "#FFA5A7");
                }
            }
        }

        protected void enregistrer_paiement(object sender, CommandEventArgs e)
        {
            myConnection.Open();
            string[] tab_args = e.CommandArgument.ToString().Split(';');
            SqlCommand commande_enregistrer = new SqlCommand("UPDATE PPSuiviCompta SET DatePaiement = GETDATE() WHERE NoVendeur = " + tab_args[0] + " AND Mois = '" + tab_args[1] + "'", myConnection);
            //
            commande_enregistrer.ExecuteNonQuery();
            Session["histo_no_vendeur"] = tab_args[0];
            Session["msg"] = "Le paiement a bien été enregistré";
            myConnection.Close();
            charge_redevances();
        }

        protected void voir_details_redevance(object sender, CommandEventArgs e)
        {
            Session["no_vendeur_no_commande"] = e.CommandArgument;
            Response.Redirect(Chemin.Ajouter("details_redevance.aspx", "Retour à la liste des mois"));
        }
    }
}