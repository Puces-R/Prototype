using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Puces_R.Controles
{
    public partial class Categories : System.Web.UI.UserControl
    {
        SqlConnection myConnection = Librairie.Connexion;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hypTous.NavigateUrl = CreerLienProduits(String.Empty);

                SqlDataAdapter adapteurCategories = new SqlDataAdapter("SELECT C.Description, C.NoCategorie FROM PPCategories C INNER JOIN PPProduits P ON C.NoCategorie = P.NoCategorie WHERE P.Disponibilité = 1 AND P.NombreItems > 0 GROUP BY C.Description, C.NoCategorie HAVING COUNT(P.NoProduit) > 0", myConnection);
                DataTable tableCategories = new DataTable();
                adapteurCategories.Fill(tableCategories);
                rptCategories.DataSource = new DataView(tableCategories);
                rptCategories.DataBind();

                SqlDataAdapter adapteurVendeurs = new SqlDataAdapter("SELECT V.NomAffaires, V.NoVendeur FROM PPVendeurs V INNER JOIN PPProduits P ON V.NoVendeur = P.NoVendeur WHERE P.Disponibilité = 1 AND P.NombreItems > 0 GROUP BY V.NomAffaires, V.NoVendeur HAVING COUNT(P.NoProduit) > 0", myConnection);
                DataTable tableVendeurs = new DataTable();
                adapteurVendeurs.Fill(tableVendeurs);
                rptVendeurs.DataSource = new DataView(tableVendeurs);
                rptVendeurs.DataBind();
            }
        }

        protected void rptCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                HyperLink hypCategorie = (HyperLink)item.FindControl("hypCategorie");
                Repeater rptVendeursDeCategorie = (Repeater)item.FindControl("rptVendeursDeCategorie");

                DataRowView drvCategorie = (DataRowView)e.Item.DataItem;

                String description = (String)drvCategorie["Description"];
                int noCategorie = (int)drvCategorie["NoCategorie"];

                hypCategorie.Text = description;
                if (Session["Type"] != null)
                {
                    hypCategorie.NavigateUrl = CreerLienProduits("?nocategorie=" + noCategorie);
                }

                SqlDataAdapter adapteurVendeurs = new SqlDataAdapter("SELECT P.NoVendeur, V.NomAffaires, P.NoCategorie, COUNT(P.NoProduit) AS NbProduits FROM PPVendeurs V INNER JOIN PPProduits P ON V.NoVendeur = P.NoVendeur WHERE P.NoCategorie = " + noCategorie + " AND P.Disponibilité = 1 GROUP BY P.NoVendeur, V.NomAffaires, P.NoCategorie", myConnection);
                DataTable tableVendeurs = new DataTable();
                adapteurVendeurs.Fill(tableVendeurs);

                rptVendeursDeCategorie.DataSource = new DataView(tableVendeurs);
                rptVendeursDeCategorie.DataBind();
            }
        }

        protected void rptVendeursDeCategorie_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
                hypVendeur.NavigateUrl = CreerLienProduits("?novendeur=" + noVendeur + (Session["Type"] == null ? "" : "&nocategorie=" + noCategorie));

                lblNbProduits.Text = nbProduits.ToString();
            }
        }

        protected void rptVendeurs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                HyperLink hypVendeur = (HyperLink)item.FindControl("hypVendeur");
                Repeater rptCategoriesDeVendeur = (Repeater)item.FindControl("rptCategoriesDeVendeur");

                DataRowView drvVendeur = (DataRowView)e.Item.DataItem;

                String nomAffaires = (String)drvVendeur["NomAffaires"];
                long noVendeur = (long)drvVendeur["NoVendeur"];

                hypVendeur.Text = nomAffaires;
                hypVendeur.NavigateUrl = CreerLienProduits("?novendeur=" + noVendeur);

                SqlDataAdapter adapteurCategories = new SqlDataAdapter("SELECT P.NoVendeur, C.Description, P.NoCategorie, COUNT(P.NoProduit) AS NbProduits FROM PPCategories C INNER JOIN PPProduits P ON C.NoCategorie = P.NoCategorie WHERE P.NoVendeur = " + noVendeur + " AND P.Disponibilité = 1 GROUP BY P.NoVendeur, C.Description, P.NoCategorie", myConnection);
                DataTable tableCategories = new DataTable();
                adapteurCategories.Fill(tableCategories);

                rptCategoriesDeVendeur.DataSource = new DataView(tableCategories);
                rptCategoriesDeVendeur.DataBind();
            }
        }

        protected void rptCategoriesDeVendeur_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                HyperLink hypCategorie = (HyperLink)item.FindControl("hypCategorie");
                Label lblNbProduits = (Label)item.FindControl("lblNbProduits");

                DataRowView drvVendeur = (DataRowView)e.Item.DataItem;

                String description = (String)drvVendeur["Description"];
                long noVendeur = (long)drvVendeur["NoVendeur"];
                int noCategorie = (int)drvVendeur["NoCategorie"];
                int nbProduits = (int)drvVendeur["NbProduits"];

                hypCategorie.Text = description;
                hypCategorie.NavigateUrl = CreerLienProduits("?novendeur=" + noVendeur + (Session["Type"] == null ? "" : "&nocategorie=" + noCategorie));

                lblNbProduits.Text = nbProduits.ToString();
            }
        }

        protected void ddlRegroupement_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            mvCatalogue.ActiveViewIndex = ddlRegroupement.SelectedIndex;
        }

        private string CreerLienProduits(string arguments)
        {
            if (Session["ID"] == null)
            {
                return Chemin.Ajouter("~/NouveauxProduits.aspx" + arguments, "Retour à l'accueil");
            }
            else
            {
                return Chemin.Ajouter("~/Produits.aspx" + arguments, "Retour à l'accueil");
            }
        }
    }
}