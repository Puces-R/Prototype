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


            ((SiteMaster)(Master.Master)).Titre = "Nouvelles demandes de vendeurs";
            ((NavigationItems)Master).ChargerItems += charge_demandes;

            if (!IsPostBack)
            {
                ((NavigationItems)Master).AfficherPremierePage();
            } 
        }

        private void charge_demandes(object sender, EventArgs e)
        {
            charge_demandes();
        }

        protected void AfficherPremierePage(object sender, EventArgs e)
        {
            ((NavigationItems)Master).AfficherPremierePage();
        }

        private DataTable charge_demandes()
        {
            SqlDataAdapter adapteurDemandes = new SqlDataAdapter("SELECT * FROM PPVendeurs " + whereClause + orderByClause, myConnection);
            DataTable tableDemandes = new DataTable();
            adapteurDemandes.Fill(tableDemandes);
            myConnection.Close();

            PagedDataSource pdsDemandes = new PagedDataSource();
            pdsDemandes.DataSource = new DataView(tableDemandes);
            pdsDemandes.AllowPaging = true;
            pdsDemandes.PageSize = int.Parse(ddlParPage.SelectedValue);

            pdsDemandes.CurrentPageIndex = ((NavigationItems)Master).PageActuelle;
            ((NavigationItems)Master).NbPages = pdsDemandes.PageCount;

            rptDemandes.DataSource = pdsDemandes;
            rptDemandes.DataBind();

            return tableDemandes;
        }

        protected void rptDemandes_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            DataListItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {

                Label titre_demande = (Label)item.FindControl("titre_demande");
                Label addr_demande = (Label)item.FindControl("addr_demande");
                Label tels_demande = (Label)item.FindControl("tels_demande");
                Label courriel_demande = (Label)item.FindControl("courriel_demande");
                Label charge_max_demande = (Label)item.FindControl("charge_max_demande");
                Label livraison_gratuite = (Label)item.FindControl("livraison_gratuite");
                Label date_demande = (Label)item.FindControl("date_demande");
                Button btn_accepter = (Button)item.FindControl("btn_accepter");
                Button btn_refuser = (Button)item.FindControl("btn_refuser");

                DataRowView drvDemande = (DataRowView)e.Item.DataItem;
                                
                titre_demande.Text = drvDemande["NomAffaires"].ToString() + ", par " + drvDemande["Prenom"].ToString() + " " + drvDemande["Nom"].ToString();
                addr_demande.Text = drvDemande["Rue"].ToString() + ", " + drvDemande["Ville"].ToString() + ", " + drvDemande["Pays"].ToString();
                tels_demande.Text = drvDemande["Tel1"].ToString();
                courriel_demande.Text = drvDemande["AdresseEmail"].ToString();
                charge_max_demande.Text = drvDemande["MaxLivraison"].ToString() + "Kg";
                livraison_gratuite.Text = drvDemande["LivraisonGratuite"].ToString();
                date_demande.Text = drvDemande["DateCreation"].ToString();
                btn_accepter.CommandArgument = drvDemande["NoVendeur"].ToString();
                btn_refuser.CommandArgument = drvDemande["NoVendeur"].ToString();
            }
        }

        protected void rptDemandes_ItemCommand(object sender, CommandEventArgs e)
        {
            
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
    }
}