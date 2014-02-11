﻿using System;
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
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ID"] == null)
                {
                    hypTous.NavigateUrl = Chemin.Ajouter("~/NouveauxProduits.aspx", "Retour à l'accueil");
                }
                else
                {
                    hypTous.NavigateUrl = Chemin.Ajouter("~/Produits.aspx", "Retour à l'accueil");
                }

                SqlDataAdapter adapteurCategories = new SqlDataAdapter("SELECT Description, NoCategorie FROM PPCategories", myConnection);
                DataTable tableCategories = new DataTable();
                adapteurCategories.Fill(tableCategories);

                rptCategories.DataSource = new DataView(tableCategories);
                rptCategories.DataBind();
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

                SqlDataAdapter adapteurVendeurs = new SqlDataAdapter("SELECT P.NoVendeur, V.NomAffaires, P.NoCategorie, COUNT(P.NoProduit) AS NbProduits FROM PPVendeurs V INNER JOIN PPProduits P ON V.NoVendeur = P.NoVendeur WHERE P.NoCategorie = " + noCategorie + " AND P.Disponibilité = 1 GROUP BY P.NoVendeur, V.NomAffaires, P.NoCategorie", myConnection);
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

                if (Session["ID"] != null)
                {
                    hypVendeur.NavigateUrl = Chemin.Ajouter("~/Produits.aspx?novendeur=" + noVendeur + "&nocategorie=" + noCategorie, "Retour à l'accueil");
                }
                else
                {
                    hypVendeur.NavigateUrl = Chemin.Ajouter("~/NouveauxProduits.aspx?novendeur=" + noVendeur, "Retour à l'accueil");
                }

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
    }
}