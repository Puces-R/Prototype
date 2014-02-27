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
    public partial class accueil_compta : System.Web.UI.Page
    {
        SqlConnection myConnection = Librairie.Connexion;
        string orderByClause = " ORDER BY ";
        PagedDataSource pdsDemandes = new PagedDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Librairie.Autorisation(false, false, false, true);
            }
            verifier_mois_compta();

            //String orderByClause = " ORDER BY ";
            switch (ddlTrierPar.SelectedIndex)
            {
                case 0:
                    orderByClause += " MontantAttendu ";
                    break;
                case 1:
                    orderByClause += " MontantRecu ";
                    break;
                case 2:
                    orderByClause += " MontantDu ";
                    break;
                case 3:
                    orderByClause += " Mois ";
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

            Master.Master.Titre = "Suivi des revedances";
            Master.ChargerItems += charge_redevances_mensuelle;

            if (!IsPostBack)
            {
                Master.AfficherPremierePage();
            }

            Session["client_desactive"] = null;
        }

        private void charge_redevances_mensuelle(object sender, EventArgs e)
        {
            charge_redevances_mensuelle();
        }

        protected void AfficherPremierePage(object sender, EventArgs e)
        {
            Master.AfficherPremierePage();
        }

        private DataTable charge_redevances_mensuelle()
        {
            string req = "";

            req += " SELECT SUM(Montant) MontantAttendu, SUM(CASE WHEN DatePaiement IS NOT NULL THEN Montant ELSE 0 END) MontantRecu, SUM(CASE WHEN DatePaiement IS NULL THEN Montant ELSE 0 END) MontantDu, Mois ";
            req += " FROM PPSuiviCompta ";
            req += " GROUP BY Mois ";

            SqlDataAdapter adapteurDemandes = new SqlDataAdapter(req + orderByClause, myConnection);
            DataTable tableDemandes = new DataTable();
            adapteurDemandes.Fill(tableDemandes);

            pdsDemandes.DataSource = new DataView(tableDemandes);
            pdsDemandes.AllowPaging = true;
            pdsDemandes.PageSize = int.Parse(ddlParPage.SelectedValue);

            pdsDemandes.CurrentPageIndex = Master.PageActuelle;
            Master.NbPages = pdsDemandes.PageCount;

            rptMois.DataSource = pdsDemandes;
            rptMois.DataBind();
            myConnection.Close();

            return tableDemandes;
        }

        protected void rptMois_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                LinkButton lbl_num = (LinkButton)item.FindControl("lbl_num");
                LinkButton lbl_mois = (LinkButton)item.FindControl("lbl_mois");
                LinkButton lbl_attendu = (LinkButton)item.FindControl("lbl_attendu");
                LinkButton lbl_recu = (LinkButton)item.FindControl("lbl_recu");
                LinkButton lbl_du = (LinkButton)item.FindControl("lbl_du");

                DataRowView drvDemande = (DataRowView)e.Item.DataItem;
                lbl_num.Text = (pdsDemandes.CurrentPageIndex * pdsDemandes.PageSize + e.Item.ItemIndex + 1).ToString();
                string[] tab_date = drvDemande["Mois"].ToString().Split('-');
                DateTime mois = new DateTime(Convert.ToInt32(tab_date[0]), Convert.ToInt32(tab_date[1]), Convert.ToInt32(tab_date[2].Remove(3)));

                lbl_num.Text = (pdsDemandes.CurrentPageIndex * pdsDemandes.PageSize + e.Item.ItemIndex + 1).ToString();
                lbl_mois.Text = mois.ToString("MMMM yyyy").ToUpperInvariant();
                lbl_attendu.Text = Convert.ToDecimal(drvDemande["MontantAttendu"]).ToString("N") + " $";
                lbl_recu.Text = Convert.ToDecimal(drvDemande["MontantRecu"]).ToString("N") + " $";
                lbl_du.Text = Convert.ToDecimal(drvDemande["MontantDu"]).ToString("N") + " $";

                lbl_num.CommandArgument = drvDemande["Mois"].ToString();
                lbl_mois.CommandArgument = drvDemande["Mois"].ToString();
                lbl_attendu.CommandArgument = drvDemande["Mois"].ToString();
                lbl_recu.CommandArgument = drvDemande["Mois"].ToString();
                lbl_du.CommandArgument = drvDemande["Mois"].ToString();
            }
        }

        protected void voir_redevances_mois(object sender, CommandEventArgs e)
        {
            Session["mois"] = e.CommandArgument.ToString();
            Response.Redirect(Chemin.Ajouter("vendeur_redevance.aspx", "Retour à l'accueil du suivi des redevances"));
        }

        protected void verifier_mois_compta()
        {
            string req = "INSERT INTO PPSuiviCompta VALUES ";

            myConnection.Open();
            SqlCommand recuperer_derniere_date = new SqlCommand("SELECT DATEADD(month, 1, MAX(Mois)) FROM PPSuiviCompta ", myConnection);
            object derniere_date = recuperer_derniere_date.ExecuteScalar();
            string[] tab_derniere_date = derniere_date.ToString().Split('-');

            if (derniere_date != DBNull.Value)
            {
                if ((Convert.ToInt32(tab_derniere_date[0]) != DateTime.Now.Year) || (Convert.ToInt32(tab_derniere_date[1]) != DateTime.Now.Month))
                {
                    SqlCommand charger = new SqlCommand("SELECT * FROM PPVendeurs ", myConnection);
                    SqlDataReader results = charger.ExecuteReader();

                    while (results.Read())
                    {
                        req += "( " + results["NoVendeur"] + ", '" + derniere_date.ToString() + "', (SELECT ISNULL(SUM (Redevance), 0) FROM PPHistoriquePaiements WHERE NoVendeur = " +
                            results["NoVendeur"] + " AND YEAR(DateVente) = " + Convert.ToInt32(tab_derniere_date[0]) + " AND MONTH(DateVente) = " +
                            Convert.ToInt32(tab_derniere_date[1]) + " ), CASE WHEN (SELECT ISNULL(SUM (Redevance), 0) FROM PPHistoriquePaiements WHERE NoVendeur = " +
                            results["NoVendeur"] + " AND YEAR(DateVente) = " + Convert.ToInt32(tab_derniere_date[0]) + " AND MONTH(DateVente) = " +
                            Convert.ToInt32(tab_derniere_date[1]) + " ) = 0 THEN GETDATE() ELSE NULL END ), ";
                    }
                    results.Close();
                    SqlCommand inserer_nouveau_mois = new SqlCommand(req.Remove(req.Length - 2), myConnection);
                    inserer_nouveau_mois.ExecuteNonQuery();
                }
            }
            else
            {
                SqlCommand recuperer_premiere_date = new SqlCommand("SELECT MIN(DateVente) FROM PPHistoriquePaiements ", myConnection);
                object premiere_date = recuperer_premiere_date.ExecuteScalar();
                SqlCommand charger = new SqlCommand("SELECT * FROM PPVendeurs ", myConnection);

                if (premiere_date != DBNull.Value)
                { 
                    string[] tab_premiere_date = premiere_date.ToString().Split('-');
                    DateTime dt_premiere = new DateTime(Convert.ToInt32(tab_premiere_date[0]), Convert.ToInt32(tab_premiere_date[1]), 1);
                    DateTime date_courante = dt_premiere.Date, comp_now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    string req_all_update = "INSERT INTO PPSuiviCompta VALUES ";

                    do
                    {
                        SqlDataReader results = charger.ExecuteReader();
                        while (results.Read())
                        {
                            req_all_update += "( " + results["NoVendeur"] + ", '" + date_courante.ToString() + "', (SELECT ISNULL(SUM (Redevance), 0) FROM PPHistoriquePaiements WHERE NoVendeur = " +
                            results["NoVendeur"] + " AND YEAR(DateVente) = " + date_courante.Year + " AND MONTH(DateVente) = " +
                            date_courante.Month + " ), CASE WHEN (SELECT ISNULL(SUM (Redevance), 0) FROM PPHistoriquePaiements WHERE NoVendeur = " +
                            results["NoVendeur"] + " AND YEAR(DateVente) = " + date_courante.Year + " AND MONTH(DateVente) = " +
                            date_courante.Month + " ) = 0 THEN GETDATE() ELSE NULL END ), ";
                        }
                        date_courante = date_courante.AddMonths(1);
                        results.Close();
                    } while (date_courante != comp_now);
                    SqlCommand inserer_tous_mois = new SqlCommand(req_all_update.Remove(req_all_update.Length - 2), myConnection);
                    inserer_tous_mois.ExecuteNonQuery();
                }
            }
            myConnection.Close();
        }
    }
}