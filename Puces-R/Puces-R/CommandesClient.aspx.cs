using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Puces_R.Controles;

namespace Puces_R
{
    public partial class CommandesClient : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        protected void Page_Load(object sender, EventArgs e)
        {
            ((NavigationItems)Master).ChargerItems += ChargerCommandes;

            if (!IsPostBack)
            {
                ChargerCommandes();

                if (Session["ID"] == null)
                {
                    Response.Redirect("Default.aspx", true);
                }

                ((NavigationItems)Master).CriteresVisibles = false;

                ((NavigationItems)Master).AfficherPremierePage();
            }
        }

        protected void dlCommandes_OnItemDataBound(object sender, DataListItemEventArgs e)
        {
            DataListItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                Literal litVendeur = (Literal)item.FindControl("litVendeur");
                Label lblNoCommande = (Label)item.FindControl("lblNoCommande");
                Label lblDate = (Label)item.FindControl("lblDate");
                Label lblStatut = (Label)item.FindControl("lblStatut");
                Label lblNoAutorisation = (Label)item.FindControl("lblNoAutorisation");
                MontantsFactures ctrMontantsFactures = (MontantsFactures)item.FindControl("ctrMontantsFactures");

                DataRowView drvCommande = (DataRowView)e.Item.DataItem;

                litVendeur.Text = (String)drvCommande["NomAffaires"];
                lblNoCommande.Text = ((long)drvCommande["NoCommande"]).ToString();
                lblDate.Text = ((DateTime)drvCommande["DateCommande"]).ToString();

                switch ((String)drvCommande["Statut"])
                {
                    case "p":
                        lblStatut.Text = "Prêt à livrer";
                        break;
                    case "l":
                        lblStatut.Text = "Livré";
                        break;
                }

                lblNoAutorisation.Text = (String)drvCommande["NoAutorisation"];
                ctrMontantsFactures.NoCommande = (long)drvCommande["NoCommande"];
                ctrMontantsFactures.ChargerModesDeLivraison();
            }
        }

        private void ChargerCommandes(object sender, EventArgs e)
        {
            ChargerCommandes();
        }

        private void ChargerCommandes()
        {
            SqlDataAdapter adapteurCommandes = new SqlDataAdapter("SELECT * FROM PPCommandes C INNER JOIN PPVendeurs V ON C.NoVendeur = V.NoVendeur WHERE C.NoClient = " + Session["ID"] + " ORDER BY DateCommande DESC" , myConnection);
            DataTable tableCommandes = new DataTable();
            adapteurCommandes.Fill(tableCommandes);

            PagedDataSource objPds = new PagedDataSource();
            objPds.DataSource = new DataView(tableCommandes);
            objPds.AllowPaging = true;
            objPds.PageSize = 15;
            objPds.CurrentPageIndex = ((NavigationItems)Master).PageActuelle;

            ((NavigationItems)Master).NbPages = objPds.PageCount;

            dlCommandes.DataSource = objPds;
            dlCommandes.DataBind();
        }
    }
}