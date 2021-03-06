﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Puces_R.Controles
{
    public partial class NouveauxProduits : System.Web.UI.Page
    {
        private SqlConnection connexion = Librairie.Connexion;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Librairie.Autorisation(true, false, false, false);
                chargerProduits();
            }
        }

        private void chargerProduits()
        {
            List<String> whereParts = new List<String>();

            int noVendeur;
            if (int.TryParse(Request.Params["novendeur"], out noVendeur))
            {
                whereParts.Add("P.NoVendeur = " + noVendeur);
                Master.Titre = "Nouveaux produits";
            }
            else
            {
                Master.Titre = "Nouveaux produits";
            }

            int noCategorie;
            if(int.TryParse(Request.Params["nocategorie"], out noCategorie))
            {
                whereParts.Add("P.NoCategorie = " + noCategorie);
            }

            whereParts.Add("P.Disponibilité = 1");

            String whereClause;
            if (whereParts.Count > 0)
            {
                whereClause = " WHERE " + string.Join(" AND ", whereParts);
            }
            else
            {
                whereClause = string.Empty;
            }

            SqlDataAdapter adapteurProduits = new SqlDataAdapter("SELECT TOP 15 P.NoProduit, P.Photo, C.Description, P.Nom, P.PrixVente, P.NombreItems FROM PPProduits P " +
                                                                 "INNER JOIN PPCategories C ON C.NoCategorie = P.NoCategorie" + whereClause + " ORDER BY DateCreation DESC", connexion);
            DataTable tableProduits = new DataTable();
            adapteurProduits.Fill(tableProduits);
            if (tableProduits.Rows.Count == 0)
            {
                ParametresGet param = new ParametresGet(Request.RawUrl, new string[] { "texteretour", "cheminretour" });
                Response.Redirect("NouveauxProduits.aspx" + param.Parametres);
            }

            PagedDataSource objPds = new PagedDataSource();
            objPds.DataSource = new DataView(tableProduits);
            objPds.AllowPaging = false;

            dtlProduits.DataSource = objPds;
            dtlProduits.DataBind();
        }

        protected void dtlProduits_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            DataListItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                BoiteProduit ctrProduit = (BoiteProduit)item.FindControl("ctrProduit");

                DataRowView drvProduit = (DataRowView)e.Item.DataItem;

                ctrProduit.NoProduit = (long)drvProduit["NoProduit"];
                ctrProduit.NoSequentiel = item.ItemIndex + 1;
            }
        }
    }
}