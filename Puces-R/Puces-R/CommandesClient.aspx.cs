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
            Master.ChargerItems += ChargerCommandes;

            if (!IsPostBack)
            {
                if (Session["ID"] == null)
                {
                    Response.Redirect("Default.aspx", true);
                }
                Librairie.Autorisation(false, true, false, false);

                SqlDataAdapter adapteurVendeurs = new SqlDataAdapter("SELECT DISTINCT V.NomAffaires, V.NoVendeur FROM PPVendeurs V INNER JOIN PPCommandes C ON V.NoVendeur = C.NoVendeur WHERE C.NoClient = " + Session["ID"], myConnection);
                DataTable tableVendeurs = new DataTable();
                adapteurVendeurs.Fill(tableVendeurs);

                ddlVendeur.DataSource = tableVendeurs;
                ddlVendeur.DataTextField = "NomAffaires";
                ddlVendeur.DataValueField = "NoVendeur";
                ddlVendeur.DataBind();
                ddlVendeur.Items.Add(new ListItem("Tous", "-1"){Selected=true});
                ddlVendeur.SelectedValue = (Request.Params["novendeur"] == null ? "-1" : Request.Params["novendeur"]);

                ChargerCommandes();
                
                Master.AfficherPremierePage();
            }
        }

        protected void dlCommandes_OnItemDataBound(object sender, DataListItemEventArgs e)
        {
            DataListItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                BoiteCommande ctrCommande = (BoiteCommande)item.FindControl("ctrCommande");

                DataRowView drvCommande = (DataRowView)e.Item.DataItem;

                ctrCommande.NoCommande = (long)drvCommande["NoCommande"];
            }
        }

        private void ChargerCommandes(object sender, EventArgs e)
        {
            ChargerCommandes();
        }

        private void ChargerCommandes()
        {
            String whereClause = " WHERE C.NoClient = " + Session["ID"];

            string noVendeur;
            if (IsPostBack)
            {
                noVendeur = ddlVendeur.SelectedValue;
            }
            else
            {
                noVendeur = Request.Params["novendeur"];
            }
            if (noVendeur != null && noVendeur != "-1")
            {
                whereClause += " AND V.NoVendeur = " + noVendeur;
            }

            SqlDataAdapter adapteurCommandes = new SqlDataAdapter("SELECT C.NoCommande FROM PPCommandes C INNER JOIN PPVendeurs V ON C.NoVendeur = V.NoVendeur" + whereClause + " ORDER BY DateCommande DESC" , myConnection);
            DataTable tableCommandes = new DataTable();
            adapteurCommandes.Fill(tableCommandes);

            PagedDataSource objPds = new PagedDataSource();
            objPds.DataSource = new DataView(tableCommandes);
            objPds.AllowPaging = true;
            objPds.PageSize = int.Parse(ddlParPage.SelectedValue);
            objPds.CurrentPageIndex = Master.PageActuelle;

            Master.NbPages = objPds.PageCount;

            dlCommandes.DataSource = objPds;
            dlCommandes.DataBind();

            mvCommandes.ActiveViewIndex = tableCommandes.Rows.Count == 0 ? 1 : 0;

            if (noVendeur == null || noVendeur == "-1")
            {
                Master.Master.Titre = null;
            }
            else
            {
                Master.Master.NoVendeur = long.Parse(noVendeur);
            }
        }

        protected void AfficherPremierePage(object sender, EventArgs e)
        {
            Master.AfficherPremierePage();
        }
    }
}