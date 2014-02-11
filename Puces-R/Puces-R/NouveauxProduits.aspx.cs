using System;
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
        private int noVendeur;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                chargerProduits();

                if (noVendeur == -1)
                {
                    Master.Titre = "Catalogue Global";
                }
                else
                {
                    Master.NoVendeur = noVendeur;
                }
            }
        }

        private void chargerProduits()
        {
            List<String> whereParts = new List<String>();

            if (int.TryParse(Request.Params["novendeur"], out noVendeur))
            {
                whereParts.Add("P.NoVendeur = " + noVendeur);
            }
            else
            {
                noVendeur = -1;
            }

            SqlDataAdapter adapteurProduits = new SqlDataAdapter("SELECT TOP 15 P.NoProduit, P.Photo, C.Description, P.Nom, P.PrixVente, P.NombreItems FROM PPProduits P " +
                                                                 "INNER JOIN PPCategories C ON C.NoCategorie = P.NoCategorie " +
                                                                 (noVendeur == -1 ? "" : "WHERE P.NoVendeur = " + noVendeur +  " ") +
                                                                 "ORDER BY DateCreation DESC", connexion);
            DataTable tableProduits = new DataTable();
            adapteurProduits.Fill(tableProduits);

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
                HyperLink hypDescriptionAbregee = (HyperLink)item.FindControl("hypDescriptionAbregee");
                Label lblNoProduit = (Label)item.FindControl("lblNoProduit");
                Image imgProduit = (Image)item.FindControl("imgProduit");
                Label lblCategorie = (Label)item.FindControl("lblCategorie");
                Label lblPrixDemande = (Label)item.FindControl("lblPrixDemande");
                Label lblQuantite = (Label)item.FindControl("lblQuantite");

                DataRowView drvFilm = (DataRowView)e.Item.DataItem;

                long noProduit = (long)drvFilm["NoProduit"];

                Object photo = drvFilm["Photo"];
                String urlImage;
                if (photo is DBNull)
                {
                    urlImage = "Images/image_non_disponible.png";
                }
                else
                {
                    urlImage = "Images/Televerse/" + (String)photo;
                }
                String strCategorie = (String)drvFilm["Description"];
                String strDescriptionAbregee = (String)drvFilm["Nom"];
                decimal decPrixDemande = 0;//(decimal)drvFilm["PrixVente"];
                short intQuantite = (short)drvFilm["NombreItems"];

                lblNoProduit.Text = "No. " + noProduit.ToString();
                imgProduit.ImageUrl = urlImage;
                hypDescriptionAbregee.Text = strDescriptionAbregee;
                hypDescriptionAbregee.Enabled = false;
                lblCategorie.Text = strCategorie;
                lblPrixDemande.Text = "Prix de vente: " + decPrixDemande.ToString("C");
                lblQuantite.Text = "Quantité: " + intQuantite.ToString();
            }
        }
    }
}