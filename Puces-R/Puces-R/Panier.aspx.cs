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
        private int NoVendeur
        {
            get
            {
                return (int)ViewState["NoVendeur"];
            }
            set
            {
                ViewState["NoVendeur"] = value;
            }
        }

        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                chargerProduits();
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            btnCommander.Visible = String.IsNullOrEmpty(ctrMontantsFactures.MessageErreur);
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

            this.NoVendeur = noVendeur;
            ctrMenu.NoVendeur = noVendeur;
            ctrMontantsFactures.NoVendeur = noVendeur;
            ((SiteMaster)Master).NoVendeur = noVendeur;

            String whereClause = " WHERE A.NoClient = " + Session["ID"] + " AND P.NoVendeur = " + noVendeur;

            SqlDataAdapter adapteurProduits = new SqlDataAdapter("SELECT P.NoProduit,Photo,C.Description,Nom,PrixDemande,NombreItems,Poids,A.NbItems,A.NoPanier FROM PPProduits P INNER JOIN PPCategories C ON C.NoCategorie = P.NoCategorie INNER JOIN PPArticlesEnPanier A ON A.NoProduit = P.NoProduit" + whereClause, myConnection);
            DataTable tableProduits = new DataTable();
            adapteurProduits.Fill(tableProduits);

            rptProduits.DataSource = new DataView(tableProduits);
            rptProduits.DataBind();

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
                Button btnSupprimer = (Button)item.FindControl("btnSupprimer");

                DataRowView drvFilm = (DataRowView)e.Item.DataItem;

                long noProduit = (long)drvFilm["NoProduit"];
                String urlImage = "Images/Televerse/" + (String)drvFilm["Photo"];
                String strCategorie = (String)drvFilm["Description"];
                String strDescriptionAbregee = (String)drvFilm["Nom"];
                decimal decPrixDemande = (decimal)drvFilm["PrixDemande"];
                short intQuantite = (short)drvFilm["NbItems"];
                long noPanier = (long)drvFilm["NoPanier"];

                lblNoProduit.Text = noProduit.ToString();
                imgProduit.ImageUrl = urlImage;
                lblCategorie.Text = strCategorie;
                lblDescriptionAbregee.Text = strDescriptionAbregee;
                lblPrixDemande.Text = "Prix demandé: " + decPrixDemande.ToString("C");
                txtQuantite.Text = intQuantite.ToString();
                btnMAJQuantite.CommandArgument = noPanier.ToString();
                btnSupprimer.CommandArgument = noPanier.ToString();
            }
        }

        protected void rptProduits_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            Validate();

            TextBox txtQuantite = (TextBox)e.Item.FindControl("txtQuantite");

            SqlCommand commandeProduit = null;

            if (e.CommandName == "Supprimer" || (e.CommandName == "MAJQuantite" && txtQuantite.Text == "0"))
            {
                commandeProduit = new SqlCommand("DELETE FROM PPArticlesEnPanier WHERE NoPanier = " + e.CommandArgument, myConnection);
            }
            else if (e.CommandName == "MAJQuantite" && IsValid)
            {
                commandeProduit = new SqlCommand("UPDATE PPArticlesEnPanier SET NbItems = " + txtQuantite.Text + " WHERE NoPanier = " + e.CommandArgument, myConnection);
            }

            if (commandeProduit != null)
            {
                myConnection.Open();
                commandeProduit.ExecuteNonQuery();
                myConnection.Close();
                chargerProduits();
            }

            ctrMontantsFactures.MessageErreur = IsValid ? null : "Les quantités sont invalides!";
            ctrMontantsFactures.CalculerCouts();
        }

        protected void btnViderPanier_OnClick(object sender, EventArgs e)
        {
            myConnection.Open();

            SqlCommand commandeSuppression = new SqlCommand("DELETE FROM PPArticlesEnPanier WHERE NoClient = " + Session["ID"] + " AND NoVendeur = " + NoVendeur, myConnection);
            commandeSuppression.ExecuteNonQuery();

            myConnection.Close();

            chargerProduits();
            ctrMontantsFactures.CalculerCouts();
        }

        protected void btnCommander_OnClick(object sender, EventArgs e)
        {
            if (IsValid)
            {
                Response.Redirect("Commande.aspx?novendeur=" + NoVendeur + "&codelivraison=" + ctrMontantsFactures.CodeLivraison, true);
            }
        }

        protected void valQuantite_OnServerValidate(object sender, ServerValidateEventArgs e)
        {
            short nbDemande;
            if (short.TryParse(e.Value, out nbDemande))
            {
                CustomValidator valQuantite = (CustomValidator)sender;
                RepeaterItem item = (RepeaterItem)valQuantite.Parent;
                Label lblNoProduit = (Label)item.FindControl("lblNoProduit");
                long noProduit = long.Parse(lblNoProduit.Text);
                myConnection.Open();

                SqlCommand commandeNbItems = new SqlCommand("SELECT NombreItems FROM PPProduits WHERE NoProduit = " + noProduit, myConnection);
                short nbDisponible = (short)commandeNbItems.ExecuteScalar();

                myConnection.Close();

                e.IsValid = (nbDemande <= nbDisponible);
            }
            else
            {
                e.IsValid = false;
            }
        }
    }
}