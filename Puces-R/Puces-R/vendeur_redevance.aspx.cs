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
    public partial class vendeur_redevance : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
        string whereClause, orderByClause = " ORDER BY ";
        PagedDataSource pdsDemandes = new PagedDataSource();
        DateTime mois;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Librairie.Autorisation(false, false, false, true);
            }

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
                    orderByClause += " DatePaiement ";
                    break;
                case 3:
                    orderByClause += " Montant ";
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

            if (Session["mois"] != null)
                if (Session["mois"].ToString() != "")
                {
                    string[] tab_date = Session["mois"].ToString().Split('-');
                    mois = new DateTime(Convert.ToInt32(tab_date[0]), Convert.ToInt32(tab_date[1]), 1);
                }
                else Librairie.RefuserAutorisation();
            else Librairie.RefuserAutorisation();

            Master.Master.Titre = "Redevances mensuelles de " + mois.ToString("MMMM yyyy").ToUpperInvariant();
            Master.ChargerItems += charge_redevances;

            if (!IsPostBack)
            {
                Master.AfficherPremierePage();
            } 
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
            string req = "";

            req += " SELECT PPVendeurs.NoVendeur, PPVendeurs.NomAffaires, Nom, Prenom, DatePaiement, Montant, ISNULL(Statut, 0), Mois ";
            req += " FROM PPVendeurs , PPSuiviCompta ";
            req += " WHERE PPVendeurs.NoVendeur = PPSuiviCompta.NoVendeur ";
            req += " AND  Mois = '" + mois.ToString("u").Remove(mois.ToString().Length - 1) + "' " + whereClause + orderByClause;

            SqlDataAdapter adapteurDemandes = new SqlDataAdapter(req, myConnection);
            DataTable tableDemandes = new DataTable();
            adapteurDemandes.Fill(tableDemandes);
            //Response.Write(req );

            pdsDemandes.DataSource = new DataView(tableDemandes);
            pdsDemandes.AllowPaging = true;
            pdsDemandes.PageSize = int.Parse(ddlParPage.SelectedValue);

            pdsDemandes.CurrentPageIndex = Master.PageActuelle;
            Master.NbPages = pdsDemandes.PageCount;

            rptRedevances.DataSource = pdsDemandes;
            rptRedevances.DataBind();
            myConnection.Close();

            return tableDemandes;
        }

        protected void rptRedevances_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                LinkButton lbl_num = (LinkButton)item.FindControl("lbl_num");
                LinkButton lbl_nom_affaire = (LinkButton)item.FindControl("lbl_nom_affaire");
                LinkButton lbl_nom_vendeur = (LinkButton)item.FindControl("lbl_nom_vendeur");
                LinkButton lbl_date_paiement = (LinkButton)item.FindControl("lbl_date_paiement");
                LinkButton lbl_montant_du = (LinkButton)item.FindControl("lbl_montant_du");
                Button btn_enregistrer_paiement = (Button)item.FindControl("btn_enregistrer_paiement");

                DataRowView drvDemande = (DataRowView)e.Item.DataItem;

                lbl_num.Text = (pdsDemandes.CurrentPageIndex * pdsDemandes.PageSize + e.Item.ItemIndex + 1).ToString();
                lbl_nom_affaire.Text = drvDemande["NomAffaires"].ToString();
                lbl_nom_vendeur.Text = drvDemande["Prenom"].ToString() + " " + drvDemande["Nom"].ToString();
                lbl_date_paiement.Text = (drvDemande["DatePaiement"] == DBNull.Value ? "Non payé" : drvDemande["DatePaiement"].ToString());
                lbl_montant_du.Text = Convert.ToDecimal(drvDemande["Montant"]).ToString("N") + " $";
                btn_enregistrer_paiement.CommandArgument = drvDemande["NoVendeur"].ToString() + ";" + drvDemande["Mois"].ToString();
                btn_enregistrer_paiement.Visible = (drvDemande["DatePaiement"] == DBNull.Value);

                lbl_num.CommandArgument = drvDemande["NoVendeur"].ToString() + ";" + drvDemande["Mois"].ToString();
                lbl_nom_affaire.CommandArgument = drvDemande["NoVendeur"].ToString() + ";" + drvDemande["Mois"].ToString();
                lbl_nom_vendeur.CommandArgument = drvDemande["NoVendeur"].ToString() + ";" + drvDemande["Mois"].ToString();
                lbl_date_paiement.CommandArgument = drvDemande["NoVendeur"].ToString() + ";" + drvDemande["Mois"].ToString();
                lbl_montant_du.CommandArgument = drvDemande["NoVendeur"].ToString() + ";" + drvDemande["Mois"].ToString();

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
            //Response.Write(commande_enregistrer.CommandText);
            commande_enregistrer.ExecuteNonQuery();
            Session["histo_no_vendeur"] = tab_args[0];
            Session["msg"] = "Le paiement a bien été enregistré";
            myConnection.Close();
            charge_redevances();
        }

        protected void voir_details_redevance(object sender, CommandEventArgs e)
        {
            Session["no_vendeur_no_commande"] = e.CommandArgument;
            Response.Redirect(Chemin.Ajouter("details_redevance.aspx", "Retour à la liste des vendeurs de ce mois"));
        }
    }
}