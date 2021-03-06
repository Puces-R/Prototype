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
    public partial class NettoyerPanier : System.Web.UI.UserControl
    {
        SqlConnection myConnection = Librairie.Connexion;

        public long NoVendeur
        {
            get
            {
                return (long)ViewState["NoVendeur"];
            }
            set
            {
                ViewState["NoVendeur"] = value;
            }
        }

        public long NoClient
        {
            get
            {
                return (long)ViewState["NoClient"];
            }
            set
            {
                ViewState["NoClient"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataAdapter adapteurProduits = new SqlDataAdapter("SELECT Nom, NbItems, PrixVente, A.NoProduit FROM PPArticlesEnPanier A INNER JOIN PPProduits P ON A.NoProduit = P.NoProduit WHERE A.NoVendeur = " + NoVendeur + " AND A.NoClient = " + NoClient + "AND A.DateCreation <  DATEADD(month, -6, GetDate())", myConnection);
            DataTable tableProduits = new DataTable();
            adapteurProduits.Fill(tableProduits);

            rptProduits.DataSource = new DataView(tableProduits);
            rptProduits.DataBind();
        }

        protected void rptProduits_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                HyperLink hypProduit = (HyperLink)item.FindControl("hypProduit");
                Label lblQuantite = (Label)item.FindControl("lblQuantite");
                Label lblPrixUnitaire = (Label)item.FindControl("lblPrixUnitaire");
                Label lblPrixTotal = (Label)item.FindControl("lblPrixTotal");

                DataRowView drvProduit = (DataRowView)e.Item.DataItem;

                String produit = (String)drvProduit["Nom"];
                short quantite = (short)drvProduit["NbItems"];
                decimal prixUnitaire = (decimal)drvProduit["PrixVente"];
                decimal prixTotal = quantite * prixUnitaire;
                long noProduit = (long)drvProduit["NoProduit"];

                hypProduit.Text = produit;
                hypProduit.NavigateUrl = "../DetailsProduit.aspx?noproduit=" + noProduit;
                lblQuantite.Text = quantite.ToString();
                lblPrixUnitaire.Text = prixUnitaire.ToString("C");
                lblPrixTotal.Text = prixTotal.ToString("C");
            }
        }
    }
}