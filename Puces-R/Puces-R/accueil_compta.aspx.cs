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
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
        string whereClause, orderByClause = " ORDER BY ";
        PagedDataSource pdsDemandes = new PagedDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {
            verifier_mois_compta();

            List<String> whereParts = new List<String>();

            if (txtCritereRecherche.Text != string.Empty)
            {
                String colonne = "PPVendeurs.NomAffaires";
                switch (ddlTypeRecherche.SelectedIndex)
                {
                    case 0:
                        colonne = "PPVendeurs.NomAffaires";
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
                    orderByClause += "PPVendeurs.NoVendeur";
                    break;
                case 1:
                    orderByClause += "PPVendeurs.NomAffaires";
                    break;
                case 2:
                    orderByClause += "PPVendeurs.DateCreation DESC";
                    break;
                case 3:
                    orderByClause += "MontantDu DESC";
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

            Master.Master.Titre = "Vendeurs en retard de paiement";
            Master.ChargerItems += charge_retard;

            if (!IsPostBack)
            {
                Master.AfficherPremierePage();
            } 
        }

        private void charge_retard(object sender, EventArgs e)
        {
            charge_retard();
        }

        protected void AfficherPremierePage(object sender, EventArgs e)
        {
            Master.AfficherPremierePage();
        }

        private DataTable charge_retard()
        {
            string req = "";

            req += " SELECT PPVendeurs.NoVendeur, PPVendeurs.NomAffaires, Nom, Prenom, PPVendeurs.DateCreation, R2.MontantDu, ISNULL(Statut, 0) ";
            req += " FROM PPVendeurs , ";
            req += " (SELECT  NoVendeur, SUM(Montant) AS MontantDu ";
            req += " FROM  PPSuiviCompta ";
            req += " WHERE (DatePaiement IS NULL) ";
            req += " GROUP BY NoVendeur ";
            req += " ) AS R2 WHERE PPVendeurs.NoVendeur = R2.NoVendeur " + whereClause + orderByClause;
            SqlDataAdapter adapteurDemandes = new SqlDataAdapter(req, myConnection);
            DataTable tableDemandes = new DataTable();
            adapteurDemandes.Fill(tableDemandes);
            //Response.Write(req );

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
                LinkButton lbl_nom_affaire = (LinkButton)item.FindControl("lbl_nom_affaire");
                LinkButton lbl_nom_vendeur = (LinkButton)item.FindControl("lbl_nom_vendeur");
                //Label date_demande = (Label)item.FindControl("date_demande");
                LinkButton lbl_montant_du = (LinkButton)item.FindControl("lbl_montant_du");

                DataRowView drvDemande = (DataRowView)e.Item.DataItem;
                lbl_num.Text = (pdsDemandes.CurrentPageIndex * pdsDemandes.PageSize + e.Item.ItemIndex + 1).ToString();
                lbl_nom_affaire.Text = drvDemande["NomAffaires"].ToString();
                lbl_nom_vendeur.Text = drvDemande["Prenom"].ToString() + " " + drvDemande["Nom"].ToString();
                //date_demande.Text = drvDemande["DateCreation"].ToString();
                lbl_montant_du.Text = Convert.ToDecimal(drvDemande["MontantDu"]).ToString("N") + " $";

                lbl_num.CommandArgument = drvDemande["NoVendeur"].ToString();
                lbl_nom_affaire.CommandArgument = drvDemande["NoVendeur"].ToString();
                lbl_nom_vendeur.CommandArgument = drvDemande["NoVendeur"].ToString();
                lbl_montant_du.CommandArgument = drvDemande["NoVendeur"].ToString();
            }
        }

        protected void voir_histo(object sender, CommandEventArgs e)
        {
            Session["histo_no_vendeur"] = e.CommandArgument.ToString();
            Response.Redirect("histo_redevance_vendeur.aspx");
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
                    //Response.Write(inserer_nouveau_mois.CommandText);
                    //Response.Write(derniere_date.ToString() + " - " + DateTime.Now.Date.ToString());
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

                    while (date_courante.AddMonths(1) != comp_now)
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
                    }
                    SqlCommand inserer_tous_mois = new SqlCommand(req_all_update.Remove(req_all_update.Length - 2), myConnection);
                    inserer_tous_mois.ExecuteNonQuery();
                    //Response.Write(inserer_tous_mois.CommandText);
                }
            }
            myConnection.Close();
        }
    }
}