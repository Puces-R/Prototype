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
    public partial class Panier : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                chargerProduits();
            }
        }

        private void chargerProduits()
        {
            if (Session["ID"] == null)
            {
                Response.Redirect("Default.aspx", true);
            }

            int noVendeur;
            if (!int.TryParse(Request.Params["novendeur"], out noVendeur))
            {
                Response.Redirect("Default.aspx", true);
            }

            ctrMenu.NoVendeur = noVendeur;
            ctrMontantsFactures.NoVendeur = noVendeur;
            ((SiteMaster)Master).NoVendeur = noVendeur;

            String whereClause = " WHERE A.NoClient = " + Session["ID"] + " AND P.NoVendeur = " + noVendeur;

            SqlDataAdapter adapteurProduits = new SqlDataAdapter("SELECT P.NoProduit,Photo,C.Description,Nom,PrixDemande,NombreItems,Poids,A.NbItems,A.NoPanier FROM PPProduits P INNER JOIN PPCategories C ON C.NoCategorie = P.NoCategorie INNER JOIN PPArticlesEnPanier A ON A.NoProduit = P.NoProduit" + whereClause, myConnection);
            DataTable tableProduits = new DataTable();
            adapteurProduits.Fill(tableProduits);

            rptProduits.DataSource = new DataView(tableProduits);
            rptProduits.DataBind();

            ctrMontantsFactures.NavigateUrl = "Commande.aspx?novendeur=" + noVendeur;

            mvMain.ActiveViewIndex = tableProduits.Rows.Count == 0 ? 1 : 0;
        }

        protected void rptProduits_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {

                Label lblNoProduit = (Label)item.FindControl("lblNoProduit");
                Image imgProduit = (Image)item.FindControl("imgProduit");
                Label lblCategorie = (Label)item.FindControl("lblCategorie");
                Label lblDescriptionAbregee = (Label)item.FindControl("lblDescriptionAbregee");
                Label lblPrixDemande = (Label)item.FindControl("lblPrixDemande");
                TextBox txtQuantite = (TextBox)item.FindControl("txtQuantite");
                Button btnMAJQuantite = (Button)item.FindControl("btnMAJQuantite");

                DataRowView drvFilm = (DataRowView)e.Item.DataItem;

                long noProduit = (long)drvFilm["NoProduit"];
                String urlImage = "Images/Televerse/" + (String)drvFilm["Photo"];
                String strCategorie = (String)drvFilm["Description"];
                String strDescriptionAbregee = (String)drvFilm["Nom"];
                decimal decPrixDemande = (decimal)drvFilm["PrixDemande"];
                short intQuantite = (short)drvFilm["NbItems"];
                long noPanier = (long)drvFilm["NoPanier"];

                lblNoProduit.Text = "No. " + noProduit.ToString();
                imgProduit.ImageUrl = urlImage;
                lblCategorie.Text = strCategorie;
                lblDescriptionAbregee.Text = strDescriptionAbregee;
                lblPrixDemande.Text = "Prix demandé: " + decPrixDemande.ToString("C");
                txtQuantite.Text = intQuantite.ToString();
                btnMAJQuantite.CommandArgument = noPanier.ToString();
            }
        }

        protected void rptProduits_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            TextBox txtQuantite = (TextBox)e.Item.FindControl("txtQuantite");

            SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

            myConnection.Open();
            if (txtQuantite.Text == "0")
            {
                SqlCommand commandeSuppression = new SqlCommand("DELETE FROM PPArticlesEnPanier WHERE NoPanier = " + e.CommandArgument, myConnection);
                commandeSuppression.ExecuteNonQuery();
            }
            else
            {
                SqlCommand commandeMAJQuantite = new SqlCommand("UPDATE PPArticlesEnPanier SET NbItems = " + txtQuantite.Text + " WHERE NoPanier = " + e.CommandArgument, myConnection);
                commandeMAJQuantite.ExecuteNonQuery();
            }
            myConnection.Close();

            chargerProduits();
            ctrMontantsFactures.CalculerCouts();
        }

        protected void btnCommander_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("Commande.aspx?novendeur=" + Request.Params["novendeur"], true);
        }        
    }
}