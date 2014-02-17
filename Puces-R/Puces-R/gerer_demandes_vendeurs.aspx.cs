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
    public partial class gerer_demandes_vendeurs : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
        string whereClause, orderByClause = " ORDER BY ";
        PagedDataSource pdsDemandes = new PagedDataSource();

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
                whereParts.Add(colonne + " LIKE '%" + txtCritereRecherche.Text + "%'");
            }

            //String whereClause;
            if (whereParts.Count > 0)
            {
                whereClause = " WHERE Statut = 2 AND " + string.Join(" AND ", whereParts);
            }
            else
            {
                whereClause = " WHERE Statut = 2 ";
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


            Master.Master.Titre = "Nouvelles demandes de vendeurs";
            Master.ChargerItems += charge_demandes;

            if (!IsPostBack)
            {
                Master.AfficherPremierePage();
            } 
        }

        private void charge_demandes(object sender, EventArgs e)
        {
            charge_demandes();
        }

        protected void AfficherPremierePage(object sender, EventArgs e)
        {
            Master.AfficherPremierePage();
        }

        private DataTable charge_demandes()
        {
            SqlDataAdapter adapteurDemandes = new SqlDataAdapter("SELECT * FROM PPVendeurs " + whereClause + orderByClause, myConnection);
            DataTable tableDemandes = new DataTable();
            adapteurDemandes.Fill(tableDemandes);
            myConnection.Close();

            pdsDemandes.DataSource = new DataView(tableDemandes);
            pdsDemandes.AllowPaging = true;
            pdsDemandes.PageSize = int.Parse(ddlParPage.SelectedValue);

            pdsDemandes.CurrentPageIndex = Master.PageActuelle;
            Master.NbPages = pdsDemandes.PageCount;

            rptDemandes.DataSource = pdsDemandes;
            rptDemandes.DataBind();

            return tableDemandes;
        }

        protected void rptDemandes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                LinkButton lbl_num = (LinkButton)item.FindControl("lbl_num");
                LinkButton lbl_nom_affaire = (LinkButton)item.FindControl("lbl_nom_affaire");
                LinkButton date_demande = (LinkButton)item.FindControl("date_demande");
                Button btn_accepter = (Button)item.FindControl("btn_accepter");
                Button btn_refuser = (Button)item.FindControl("btn_refuser");
                LinkButton lbl_nom_vendeur = (LinkButton)item.FindControl("lbl_nom_vendeur");

                DataRowView drvDemande = (DataRowView)e.Item.DataItem;

                lbl_num.Text = (pdsDemandes.CurrentPageIndex * pdsDemandes.PageSize + e.Item.ItemIndex + 1).ToString();
                lbl_nom_affaire.Text = drvDemande["NomAffaires"].ToString();
                date_demande.Text = drvDemande["DateCreation"].ToString();
                btn_accepter.CommandArgument = drvDemande["NoVendeur"].ToString();
                btn_refuser.CommandArgument = drvDemande["NoVendeur"].ToString();
                lbl_nom_vendeur.Text = drvDemande["Prenom"].ToString() + " " + drvDemande["Nom"].ToString();

                lbl_num.CommandArgument = drvDemande["NoVendeur"].ToString();
                lbl_nom_affaire.CommandArgument = drvDemande["NoVendeur"].ToString();
                date_demande.CommandArgument = drvDemande["NoVendeur"].ToString();
                lbl_nom_vendeur.CommandArgument = drvDemande["NoVendeur"].ToString();
            }
        }
        
        protected void refus_demande(object sender, CommandEventArgs e)
        {
            Session["refus_vendeur"] = e.CommandArgument.ToString();
            Response.Redirect("verdict_demande.aspx");            
        }

        protected void acceptation_demande(object sender, CommandEventArgs e)
        {
            Session["acceptation_vendeur"] = e.CommandArgument.ToString();
            Response.Redirect("verdict_demande.aspx");
        }

        protected void details_demande(object sender, CommandEventArgs e)
        {
            Session["details_demande"] = e.CommandArgument.ToString();
            Response.Redirect("verdict_demande.aspx");
        }
    }
}