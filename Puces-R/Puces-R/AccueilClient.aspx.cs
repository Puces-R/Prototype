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
    public partial class AccueilClient : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["ID"] == null)
                {
                    Response.Redirect("Default.aspx", true);
                }

                SqlDataAdapter adapteurCategories = new SqlDataAdapter("SELECT Description, NoCategorie FROM PPCategories", myConnection);
                DataTable tableCategories = new DataTable();
                adapteurCategories.Fill(tableCategories);

                rptCategories.DataSource = new DataView(tableCategories);
                rptCategories.DataBind();

                SqlDataAdapter adapteurPaniers = new SqlDataAdapter("SELECT V.NomAffaires, A.NoVendeur, SUM(A.NbItems * P.PrixVente) AS SousTotal FROM PPArticlesEnPanier AS A INNER JOIN PPVendeurs AS V ON A.NoVendeur = V.NoVendeur INNER JOIN PPProduits AS P ON A.NoProduit = P.NoProduit WHERE A.NoClient = " + Session["ID"] + " GROUP BY V.NomAffaires, A.NoVendeur", myConnection);
                DataTable tablePaniers = new DataTable();
                adapteurPaniers.Fill(tablePaniers);

                rptPaniers.DataSource = new DataView(tablePaniers);
                rptPaniers.DataBind();
            }
        }

        protected void rptCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                Label lblCategorie = (Label)item.FindControl("lblCategorie");
                Repeater rptVendeurs = (Repeater)item.FindControl("rptVendeurs");

                DataRowView drvCategorie = (DataRowView)e.Item.DataItem;

                String description = (String)drvCategorie["Description"];
                int noCategorie = (int)drvCategorie["NoCategorie"];

                lblCategorie.Text = description;

                SqlDataAdapter adapteurVendeurs = new SqlDataAdapter("SELECT P.NoVendeur, V.NomAffaires, P.NoCategorie, COUNT(P.NoProduit) AS NbProduits FROM PPVendeurs V INNER JOIN PPProduits P ON V.NoVendeur = P.NoVendeur WHERE P.NoCategorie = " + noCategorie + " GROUP BY P.NoVendeur, V.NomAffaires, P.NoCategorie", myConnection);
                DataTable tableVendeurs = new DataTable();
                adapteurVendeurs.Fill(tableVendeurs);

                rptVendeurs.DataSource = new DataView(tableVendeurs);
                rptVendeurs.DataBind();
            }
        }

        protected void rptVendeurs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                HyperLink hypVendeur = (HyperLink)item.FindControl("hypVendeur");
                Label lblNbProduits = (Label)item.FindControl("lblNbProduits");

                DataRowView drvVendeur = (DataRowView)e.Item.DataItem;

                String description = (String)drvVendeur["NomAffaires"];
                long noVendeur = (long)drvVendeur["NoVendeur"];
                int noCategorie = (int)drvVendeur["NoCategorie"];
                int nbProduits = (int)drvVendeur["NbProduits"];

                hypVendeur.Text = description;
                hypVendeur.NavigateUrl = "Produits.aspx?novendeur=" + noVendeur + "&nocategorie=" + noCategorie;
                lblNbProduits.Text = nbProduits.ToString();
            }
            else if (item.ItemType == ListItemType.Footer)
            {
                Repeater rptVendeurs = (Repeater)sender;

                if (rptVendeurs.Items.Count > 0)
                {
                    Panel pnlAucunVendeur = (Panel)item.FindControl("pnlAucunVendeur");

                    pnlAucunVendeur.Visible = false;
                }
            }
        }

        protected void rptPaniers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                HyperLink hypVendeur = (HyperLink)item.FindControl("hypVendeur");
                Label lblSousTotal = (Label)item.FindControl("lblSousTotal");
                TablePanier ctrProduits = (TablePanier)item.FindControl("ctrProduits");
                
                DataRowView drvPanier = (DataRowView)e.Item.DataItem;

                String vendeur = (String)drvPanier["NomAffaires"];
                decimal sousTotal = (decimal)drvPanier["SousTotal"];
                long noVendeur = (long)drvPanier["NoVendeur"];

                hypVendeur.Text = vendeur;
                hypVendeur.NavigateUrl = "Panier.aspx?novendeur=" + noVendeur;
                lblSousTotal.Text = sousTotal.ToString("C");
                ctrProduits.NoVendeur = noVendeur;
            }
        }
    }
}