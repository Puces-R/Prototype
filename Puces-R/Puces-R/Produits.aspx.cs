using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Puces_R
{
    public partial class Produits : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

            SqlDataAdapter commandeFilms = new SqlDataAdapter("SELECT NoProduit,Photo,C.Description,Nom,PrixDemande,NombreItems FROM PPProduits P INNER JOIN PPCategories C ON C.NoCategorie = P.NoCategorie", myConnection);
            DataTable tableFilms = new DataTable();
            commandeFilms.Fill(tableFilms);

            PagedDataSource objPds = new PagedDataSource();
            objPds.DataSource = new DataView(tableFilms);
            objPds.AllowPaging = true;
            objPds.PageSize = 15;

            objPds.CurrentPageIndex = 0;
            dtlProduits.DataSource = objPds;
            dtlProduits.DataBind();
        }

        protected void dtlProduits_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            DataListItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {

                Label lblNoProduit = (Label)item.FindControl("lblNoProduit");
                Image imgProduit = (Image)item.FindControl("imgProduit");
                Label lblCategorie = (Label)item.FindControl("lblCategorie");
                Label lblDescriptionAbregee = (Label)item.FindControl("lblDescriptionAbregee");
                Label lblPrixDemande = (Label)item.FindControl("lblPrixDemande");
                Label lblQuantite = (Label)item.FindControl("lblQuantite");

                DataRowView drvFilm = (DataRowView)e.Item.DataItem;



                long noProduit = (long)drvFilm["NoProduit"];
                String urlImage = "Images/Televerse/" + (String)drvFilm["Photo"];
                String strCategorie = (String)drvFilm["Description"];
                String strDescriptionAbregee = (String)drvFilm["Nom"];
                decimal decPrixDemande = (decimal)drvFilm["PrixDemande"];
                short intQuantite = (short)drvFilm["NombreItems"];

                lblNoProduit.Text = "No. " + noProduit.ToString();
                imgProduit.ImageUrl = urlImage;
                lblCategorie.Text = strCategorie;
                lblDescriptionAbregee.Text = strDescriptionAbregee;
                lblPrixDemande.Text = "Prix demandé: " + decPrixDemande.ToString("C");
                lblQuantite.Text = "Quantité: " + intQuantite.ToString();
            }
        }
    }
}