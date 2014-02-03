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
            if (!IsPostBack)
            {
                if (Session["ID"] == null)
                {
                    Response.Redirect("Default.aspx", true);
                }

                SqlDataAdapter adapteurCommandes = new SqlDataAdapter("SELECT * FROM PPCommandes C INNER JOIN PPVendeurs V ON C.NoVendeur = V.NoVendeur WHERE NoClient = " + Session["ID"], myConnection);
                DataTable tableCommandes = new DataTable();
                adapteurCommandes.Fill(tableCommandes);

                rptCommandes.DataSource = new DataView(tableCommandes);
                rptCommandes.DataBind();
            }
        }

        protected void rptCommandes_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                Label lblNoCommande = (Label)item.FindControl("lblNoCommande");
                Label lblVendeur = (Label)item.FindControl("lblVendeur");
                Label lblDate = (Label)item.FindControl("lblDate");
                Label lblStatut = (Label)item.FindControl("lblStatut");
                Label lblNoAutorisation = (Label)item.FindControl("lblNoAutorisation");
                MontantsFactures ctrMontantsFactures = (MontantsFactures)item.FindControl("ctrMontantsFactures");

                DataRowView drvPanier = (DataRowView)e.Item.DataItem;

                lblNoCommande.Text = ((long)drvPanier["NoCommande"]).ToString();
                lblVendeur.Text = (String)drvPanier["NomAffaires"];
                lblDate.Text = ((DateTime)drvPanier["DateCommande"]).ToString();
                lblStatut.Text = (String)drvPanier["Statut"];
                lblNoAutorisation.Text = (String)drvPanier["NoAutorisation"];
                ctrMontantsFactures.NoCommande = (long)drvPanier["NoCommande"];
            }
        }
    }
}