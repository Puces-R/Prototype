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
    public partial class histo_redevance_vendeur : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
        string whereClause, orderByClause = " ORDER BY ";
        int no_vendeur;

        protected void Page_Load(object sender, EventArgs e)
        {
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
                    orderByClause += " Montant DESC";
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

            if (Session["histo_no_vendeur"] != null)
                if (Session["histo_no_vendeur"].ToString() != "")
                {
                    no_vendeur = Convert.ToInt32(Session["histo_no_vendeur"]);
                }
                else Response.Redirect("Default.aspx");
            else Response.Redirect("Default.aspx");

            Master.ChargerItems += charge_redevences;

            if (!IsPostBack)
            {
                Master.AfficherPremierePage();
            }

            myConnection.Open();
            SqlCommand commande_enregistrer = new SqlCommand("SELECT NomAffaires FROM PPVendeurs WHERE NoVendeur = " + no_vendeur, myConnection);
            object no_affaire = commande_enregistrer.ExecuteScalar();
            Master.Master.Titre = "Historique des redevences de \"" + no_affaire.ToString() + "\"";
        }

        private void charge_redevences(object sender, EventArgs e)
        {
            charge_redevences();
        }

        protected void AfficherPremierePage(object sender, EventArgs e)
        {
            Master.AfficherPremierePage();
        }

        private DataTable charge_redevences()
        {
            SqlDataAdapter adapteurDemandes = new SqlDataAdapter("SELECT * FROM PPSuiviCompta WHERE NoVendeur = " + no_vendeur, myConnection);
            DataTable tableDemandes = new DataTable();
            adapteurDemandes.Fill(tableDemandes);
            //Response.Write(req );

            PagedDataSource pdsDemandes = new PagedDataSource();
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
                Label lbl_num = (Label)item.FindControl("lbl_num");
                Label lbl_mois = (Label)item.FindControl("lbl_mois");
                Label date_paiement = (Label)item.FindControl("date_paiement");
                Label lbl_montant = (Label)item.FindControl("lbl_montant");
                Button btn_enregistrer_paiement = (Button)item.FindControl("btn_enregistrer_paiement");
                Button btn_voir_details_redevence = (Button)item.FindControl("btn_voir_details_redevence");

                DataRowView drvDemande = (DataRowView)e.Item.DataItem;

                lbl_num.Text = (e.Item.ItemIndex + 1).ToString();

                string[] tab_date = drvDemande["Mois"].ToString().Split('-');
                DateTime mois = new DateTime(Convert.ToInt32(tab_date[0]), Convert.ToInt32(tab_date[1]), Convert.ToInt32(tab_date[2].Remove(3)));
                lbl_mois.Text = mois.ToString("MMMM yyyy").ToUpperInvariant();

                date_paiement.Text = (drvDemande["DatePaiement"] == DBNull.Value ? "Non payé" : drvDemande["DatePaiement"].ToString());
                lbl_montant.Text = drvDemande["Montant"].ToString();
                btn_enregistrer_paiement.CommandArgument = drvDemande["NoVendeur"].ToString() + ";" + drvDemande["Mois"].ToString();
                btn_enregistrer_paiement.Visible = (drvDemande["DatePaiement"] == DBNull.Value);
                btn_voir_details_redevence.CommandArgument = drvDemande["NoVendeur"].ToString() + ";" + drvDemande["Mois"].ToString();

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
            charge_redevences();
        }

        protected void voir_details_redevence(object sender, CommandEventArgs e)
        {
        }
    }
}