using System;
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
        SqlConnection myConnection = Librairie.Connexion;
        string whereClause, orderByClause = " ORDER BY ", mois;
        int no_vendeur, no_client;
        PagedDataSource pdsDemandes = new PagedDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Librairie.Autorisation(false, false, false, true);
            }

            if (Session["client_desactive"] != null)
            {
                Session["no_vendeur_no_commande"] = null;
            }

            List<String> whereParts = new List<String>();

            if (Session["msg"] != null && Session["msg"].ToString() != "")
            {
                div_msg.InnerText = Session["msg"].ToString();
                Session["msg"] = null;
            }

            if (Session["err_msg"] != null && Session["err_msg"].ToString() != "")
            {
                
                Session["err_msg"] = null;
            }

            if (Session["no_vendeur_no_commande"] != null)
            {
                if (Session["no_vendeur_no_commande"].ToString() != "")
                {
                    if (txtCritereRecherche.Text.Trim() != string.Empty)
                    {
                        String colonne = " PPClients.Nom + PPClients.Prenom";
                        switch (ddlTypeRecherche.SelectedIndex)
                        {
                            case 0:
                                colonne = " PPClients.AdresseEmail ";
                                break;
                        }
                        whereParts.Add(" AND " + colonne + " LIKE @critere");
                    }

                    //String whereClause;
                    if (whereParts.Count > 0)
                    {
                        whereClause = " " + string.Join(" AND ", whereParts);
                    }

                    switch (ddlTrierPar.SelectedIndex)
                    {
                        case 0:
                            orderByClause += " NoCommande ";
                            break;
                        case 1:
                            orderByClause += " PPClients.AdresseEmail ";
                            break;
                        case 2:
                            orderByClause += " DateVente ";
                            break;
                        case 3:
                            orderByClause += " MontantVente ";
                            break;
                    }
                    orderByClause += ddlOrdre.SelectedValue;

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
                else
                {
                    Response.Redirect("Deconnexion.ashx");
                }
            }
            else
            {
                if (Session["client_desactive"] != null)
                {
                    if (txtCritereRecherche.Text.Trim() != string.Empty)
                    {
                        String colonne = " NomAffaires ";
                        switch (ddlTypeRecherche.SelectedIndex)
                        {
                            case 0:
                                colonne = " NomAffaires ";
                                break;
                        }
                        whereParts.Add(" AND " + colonne + " LIKE @critere");
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
                            orderByClause += " PPVendeurs.NomAffaires ";
                            break;
                        case 2:
                            orderByClause += " DateVente ";
                            break;
                        case 3:
                            orderByClause += " MontantVente ";
                            break;
                    }
                    orderByClause += ddlOrdre.SelectedValue;

                    no_client = Convert.ToInt32(Session["client_desactive"]);
                    Master.Master.Titre = "Détails de la commande";
                }
            }

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

            req += " SELECT PPClients.AdresseEmail, MontantVente, NoCommande, NomAffaires, PPHistoriquePaiements.NoHistorique, PPHistoriquePaiements.Redevance, PPHistoriquePaiements.DateVente, PPVendeurs.NomAffaires ";
            req += " FROM PPHistoriquePaiements, PPClients, PPVendeurs ";
            req += " WHERE PPHistoriquePaiements." + (Session["no_vendeur_no_commande"] != null ? "NoVendeur = " + no_vendeur : "NoClient = " + no_client);
            req += " AND PPVendeurs.NoVendeur = PPHistoriquePaiements.NoVendeur ";
            req += " AND PPHistoriquePaiements.NoClient = PPClients.NoClient ";
            if (Session["no_vendeur_no_commande"] != null)
            {
                req += " AND YEAR(PPHistoriquePaiements.DateVente) = YEAR('" + mois + "') ";
                req += " AND MONTH(PPHistoriquePaiements.DateVente) = MONTH('" + mois + "') ";
            }
            SqlDataAdapter adapteurDemandes = new SqlDataAdapter(req + whereClause + orderByClause, myConnection);
            if (txtCritereRecherche.Text.Trim() != string.Empty)
            {
                adapteurDemandes.SelectCommand.Parameters.AddWithValue("@critere", txtCritereRecherche.Text.Trim());
            }
            DataTable tableDemandes = new DataTable();
            adapteurDemandes.Fill(tableDemandes);
            //

            pdsDemandes.DataSource = new DataView(tableDemandes);
            pdsDemandes.AllowPaging = true;
            pdsDemandes.PageSize = int.Parse(ddlParPage.SelectedValue);

            pdsDemandes.CurrentPageIndex = Master.PageActuelle;
            Master.NbPages = pdsDemandes.PageCount;

            if (Session["no_vendeur_no_commande"] != null)
            {
                rptDetailsRedevance.DataSource = pdsDemandes;
                rptDetailsRedevance.DataBind();
                mv_commandes.SetActiveView(view_vendeur);
            }
            else
            {
                rptDetailsCommande.DataSource = pdsDemandes;
                rptDetailsCommande.DataBind();
                mv_commandes.SetActiveView(view_client);
            }
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
                lbl_nom_client.Text = drvDemande["AdresseEmail"].ToString();
                lbl_redevance.Text = Convert.ToDecimal(drvDemande["Redevance"]).ToString("N") + " $";
                date_vente.Text = drvDemande["DateVente"].ToString();

                string[] str_mois = drvDemande["DateVente"].ToString().Split('-');
                DateTime mois = new DateTime(Convert.ToInt32(str_mois[0]), Convert.ToInt32(str_mois[1]), 1);
            }
        }

        protected void rptDetailsCommande_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
                lbl_nom_client.Text = drvDemande["NomAffaire"].ToString();
                lbl_redevance.Text = Convert.ToDecimal(drvDemande["MontantVente"]).ToString("N") + " $";
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