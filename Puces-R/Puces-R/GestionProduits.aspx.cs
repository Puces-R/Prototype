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
    public partial class GestionProduits : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlDataAdapter adapteurProduits = new SqlDataAdapter("SELECT NoProduit,Photo,C.Description,Nom,PrixDemande,NombreItems FROM PPProduits P INNER JOIN PPCategories C ON C.NoCategorie = P.NoCategorie where P.NoVendeur=" + Session["ID"], myConnection);
                DataTable tableProduits = new DataTable();
                adapteurProduits.Fill(tableProduits);
                dtlProduits.DataSource = tableProduits;
                dtlProduits.DataBind();
            }
        }

        protected void dtlProduits_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            DataListItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {

                Label lblNoProduit = (Label)item.FindControl("lblNoProduit");
                HyperLink hypProduit = (HyperLink)item.FindControl("hypProduit");
                Label lblCategorie = (Label)item.FindControl("lblCategorie");
                Label lblDescriptionAbregee = (Label)item.FindControl("lblDescriptionAbregee");
                Label lblPrixDemande = (Label)item.FindControl("lblPrixDemande");
                Label lblQuantite = (Label)item.FindControl("lblQuantite");
                Button btnSupprimer = (Button)item.FindControl("btnSupprimer");
                Button btnModifier = (Button)item.FindControl("btnModifier");

                DataRowView drvFilm = (DataRowView)e.Item.DataItem;

                long noProduit = (long)drvFilm["NoProduit"];
                String urlImage = "Images/Televerse/" + (String)drvFilm["Photo"];
                String strCategorie = (String)drvFilm["Description"];
                String strDescriptionAbregee = (String)drvFilm["Nom"];
                decimal decPrixDemande = (decimal)drvFilm["PrixDemande"];
                short intQuantite = (short)drvFilm["NombreItems"];

                lblNoProduit.Text = "No. " + noProduit.ToString();
                hypProduit.ImageUrl = urlImage;
                hypProduit.NavigateUrl = "DetailsProduit.aspx?noproduit=" + noProduit;
                lblCategorie.Text = strCategorie;
                lblDescriptionAbregee.Text = strDescriptionAbregee;
                lblPrixDemande.Text = "Prix demandé: " + decPrixDemande.ToString("C");
                lblQuantite.Text = "Quantité: " + intQuantite.ToString();

                btnSupprimer.CommandArgument =  noProduit.ToString();
                btnModifier.CommandArgument =  noProduit.ToString();

                //Response.Write(btnSupprimer.CommandName);
            }
        }

        protected void dtlProduits_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            //TextBox txtQuantite = (TextBox)e.Item.FindControl("txtQuantite");
            String[] tableau = ((String)e.CommandArgument).Split('-');

            String Type = e.CommandName.ToString();
            //String numProduit = tableau[1];

            if (Type == "Supprimer")
            {
                Response.Redirect("SuppressionProduits.aspx?noproduit="+e.CommandArgument.ToString());
            }
            else if (Type == "Modifier") 
            {
                Response.Redirect("ModificationProduits.aspx?noproduit="+e.CommandArgument.ToString());
            }
            
        }

    }
}