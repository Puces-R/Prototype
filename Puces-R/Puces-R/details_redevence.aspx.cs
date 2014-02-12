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
    public partial class details_redevence : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
        string whereClause, orderByClause = " ORDER BY ";

        protected void Page_Load(object sender, EventArgs e)
        {
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

            req += " SELECT PPVendeurs.NoVendeur, PPVendeurs.NomAffaires, PPVendeurs.DateCreation, R2.MontantDu, ISNULL(Statut, 0) ";
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
                Label lbl_nom_affaire = (Label)item.FindControl("lbl_nom_affaire");
                Label date_demande = (Label)item.FindControl("date_demande");
                Label lbl_montant_du = (Label)item.FindControl("lbl_montant_du");
                Button btn_voir_histo = (Button)item.FindControl("btn_voir_histo");

                DataRowView drvDemande = (DataRowView)e.Item.DataItem;

                lbl_num.Text = (e.Item.ItemIndex + 1).ToString();
                lbl_nom_affaire.Text = drvDemande["NomAffaires"].ToString();
                date_demande.Text = drvDemande["DateCreation"].ToString();
                lbl_montant_du.Text = drvDemande["MontantDu"].ToString();
                btn_voir_histo.CommandArgument = drvDemande["NoVendeur"].ToString();
            }
        }

        protected void voir_histo(object sender, CommandEventArgs e)
        {
            Session["histo_no_vendeur"] = e.CommandArgument.ToString();
            Response.Redirect("histo_redevance_vendeur.aspx");
        }
    }
}