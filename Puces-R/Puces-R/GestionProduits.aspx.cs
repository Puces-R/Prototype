﻿using System;
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
    public partial class GestionProduits : System.Web.UI.Page
    {
        SqlConnection myConnection = Librairie.Connexion;
        private int noCategorie;

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.ChargerItems += chargerProduits;

            if (!IsPostBack)
            {
                btnAjouter.PostBackUrl = Chemin.Ajouter(btnAjouter.PostBackUrl, "Retour au produit");
                Librairie.Autorisation(false, false, true, false);
                chargerProduits();
                
                SqlDataAdapter adapteurCategories = new SqlDataAdapter("SELECT DISTINCT C.Description, C.NoCategorie FROM PPCategories C INNER JOIN PPProduits P ON C.NoCategorie = P.NoCategorie AND P.NoVendeur = " + Session["ID"], myConnection);
                DataTable tableCategories = new DataTable();
                adapteurCategories.Fill(tableCategories);

                ddlCategorie.DataSource = tableCategories;
                ddlCategorie.DataTextField = "Description";
                ddlCategorie.DataValueField = "NoCategorie";
                ddlCategorie.DataBind();
                ddlCategorie.Items.Add(new ListItem("Toutes", "-1"));
                ddlCategorie.SelectedValue = noCategorie.ToString();


                Master.Master.NoVendeur = (int)(Session["ID"]);

                mvCommandes.ActiveViewIndex = tableCategories.Rows.Count == 0 ? 1 : 0;
                Master.AfficherPremierePage();

                //SqlDataAdapter adapteurProduits = new SqlDataAdapter("SELECT NoProduit,Photo,C.Description,Nom,PrixDemande,NombreItems FROM PPProduits P INNER JOIN PPCategories C ON C.NoCategorie = P.NoCategorie where P.NoVendeur=" + Session["ID"], myConnection);
                //DataTable tableProduits = new DataTable();
                //adapteurProduits.Fill(tableProduits);
                //dtlProduits.DataSource = tableProduits;
                //dtlProduits.DataBind();
            }
        }

        private void chargerProduits(object sender, EventArgs e)
        {
            chargerProduits();
        }

        private void chargerProduits()
        {
            List<String> whereParts = new List<String>();

            if (txtCritereRecherche.Text != string.Empty)
            {
                String colonne = "P.DateCreation";
                switch (ddlTypeRecherche.SelectedIndex)
                {
                    case 0:
                        colonne = "P.DateCreation";
                        break;
                    case 1:
                        colonne = "P.NoProduit";
                        break;
                    case 2:
                        colonne = "P.Description";
                        break;
                }
                whereParts.Add(colonne + " LIKE @critere");
            }

           

            if (IsPostBack)
            {
                noCategorie = int.Parse(ddlCategorie.SelectedValue);
                if (noCategorie != -1)
                {
                    whereParts.Add("P.NoCategorie = " + noCategorie);
                }
            }
            else
            {
                if (int.TryParse(Request.Params["nocategorie"], out noCategorie))
                {
                    whereParts.Add("P.NoCategorie = " + noCategorie);
                }
                else
                {
                    noCategorie = -1;
                }
            }

            String whereClause;

            whereClause = String.Empty;
            whereParts.Add("P.NoVendeur = " + Session["ID"]);
            if (whereParts.Count > 0)
            {
                whereClause = " WHERE " + string.Join(" AND ", whereParts);
            }

            String orderByClause = " ORDER BY ";
            switch (ddlTrierPar.SelectedIndex)
            {
                case 0:
                    orderByClause += "P.NoProduit";
                    break;
                case 1:
                    orderByClause += "C.Description";
                    break;
                case 2:
                    orderByClause += "P.DateCreation";
                    break;
            }

            SqlDataAdapter adapteurProduits = new SqlDataAdapter("SELECT NoProduit FROM PPProduits P INNER JOIN PPCategories C ON C.NoCategorie = P.NoCategorie" + whereClause + orderByClause, myConnection);
            if (txtCritereRecherche.Text != string.Empty)
            {
                adapteurProduits.SelectCommand.Parameters.AddWithValue("@critere", "%" + txtCritereRecherche.Text + "%");
            }
            DataTable tableProduits = new DataTable();
            adapteurProduits.Fill(tableProduits);

            PagedDataSource objPds = new PagedDataSource();
            objPds.DataSource = new DataView(tableProduits);
            objPds.AllowPaging = true;
            objPds.PageSize = int.Parse(ddlParPage.SelectedValue);
            objPds.CurrentPageIndex = Master.PageActuelle;

            Master.NbPages = objPds.PageCount;

            dtlProduits.DataSource = objPds;
            dtlProduits.DataBind();
        }

        protected void AfficherPremierePage(object sender, EventArgs e)
        {
            Master.AfficherPremierePage();
        }

        protected void dtlProduits_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            DataListItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                BoiteProduit ctrProduit = (BoiteProduit)item.FindControl("ctrProduit");

                DataRowView drvProduit = (DataRowView)e.Item.DataItem;

                long noProduit = (long)drvProduit["NoProduit"];

                ctrProduit.NoProduit = noProduit;
                ctrProduit.NoSequentiel = Master.PageActuelle * int.Parse(ddlParPage.SelectedValue) + item.ItemIndex + 1;
            }
                //Label lblNoProduit = (Label)item.FindControl("lblNoProduit");
                //HyperLink hypProduit = (HyperLink)item.FindControl("hypProduit");
                //Label lblCategorie = (Label)item.FindControl("lblCategorie");
                //Label lblDescriptionAbregee = (Label)item.FindControl("lblDescriptionAbregee");
                //Label lblPrixDemande = (Label)item.FindControl("lblPrixDemande");
                //Label lblQuantite = (Label)item.FindControl("lblQuantite");
                //Button btnSupprimer = (Button)item.FindControl("btnSupprimer");
                //Button btnModifier = (Button)item.FindControl("btnModifier");

                // = (DataRowView)e.Item.DataItem;

                //long noProduit = (long)drvFilm["NoProduit"];
                //String urlImage = "Images/Televerse/" + (String)drvFilm["Photo"];
                //String strCategorie = (String)drvFilm["Description"];
                //String strDescriptionAbregee = (String)drvFilm["Nom"];
                //decimal decPrixDemande = (decimal)drvFilm["PrixDemande"];
                //short intQuantite = (short)drvFilm["NombreItems"];

                //lblNoProduit.Text = "No. " + noProduit.ToString();
                //hypProduit.ImageUrl = urlImage;
                //hypProduit.NavigateUrl = "DetailsProduit.aspx?noproduit=" + noProduit;
                //lblCategorie.Text = strCategorie;
                //lblDescriptionAbregee.Text = strDescriptionAbregee;
                //lblPrixDemande.Text = "Prix demandé: " + decPrixDemande.ToString("C");
                //lblQuantite.Text = "Quantité: " + intQuantite.ToString();

                      

                //
            
        }

        protected void dtlProduits_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            //TextBox txtQuantite = (TextBox)e.Item.FindControl("txtQuantite");
            String[] tableau = ((String)e.CommandArgument).Split('-');

            String Type = e.CommandName.ToString();
            //String numProduit = tableau[1];

            if (Type == "Supprimer")
            {
                Response.Redirect(Chemin.Ajouter("SuppressionProduits.aspx?noproduit="+e.CommandArgument.ToString(), "Retour aux produits"));
            }
            else if (Type == "Modifier") 
            {
                Response.Redirect(Chemin.Ajouter("ModificationProduits.aspx?noproduit="+e.CommandArgument.ToString(), "Retour aux produits"));
            }
            
        }

    }
}