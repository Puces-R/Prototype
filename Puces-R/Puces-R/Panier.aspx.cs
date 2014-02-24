using System;
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

        SqlConnection myConnection = Librairie.Connexion;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Librairie.Autorisation(false, true, false, false);
                int noVendeur = Librairie.LireParametre<int>("novendeur");

                ctrMontantsFactures.NoVendeur =
                Master.NoVendeur =
                this.NoVendeur = noVendeur;

                chargerProduits();
            }
        }

        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
            btnCommander.Visible = String.IsNullOrEmpty(ctrMontantsFactures.MessageErreur);
        }

        private void chargerProduits()
        {
            ((MenuACoteVendeur)Page.Master.FindControl("ctrMenuVendeur")).loadNbItems(NoVendeur);
            String whereClause = " WHERE A.NoClient = " + Session["ID"] + " AND P.NoVendeur = " + NoVendeur;

            SqlDataAdapter adapteurProduits = new SqlDataAdapter("SELECT P.NoProduit,Photo,C.Description,Nom,PrixDemande,NombreItems,Poids,A.NbItems,A.NoPanier,NombreItems FROM PPProduits P INNER JOIN PPCategories C ON C.NoCategorie = P.NoCategorie INNER JOIN PPArticlesEnPanier A ON A.NoProduit = P.NoProduit" + whereClause, myConnection);
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
                Label lblQuantiteDispo = (Label)item.FindControl("lblQuantiteDispo");
                TextBox txtQuantite = (TextBox)item.FindControl("txtQuantite");
                Button btnMAJQuantite = (Button)item.FindControl("btnMAJQuantite");
                Button btnSupprimer = (Button)item.FindControl("btnSupprimer");

                DataRowView drvProduit = (DataRowView)e.Item.DataItem;

                long noProduit = (long)drvProduit["NoProduit"];
                String urlImage = "Images/" + (drvProduit["Photo"] is DBNull ? "image_non_disponible.png" : "Televerse/" + (String)drvProduit["Photo"]);
                String strCategorie = (String)drvProduit["Description"];
                String strDescriptionAbregee = (String)drvProduit["Nom"];
                decimal decPrixDemande = (decimal)drvProduit["PrixDemande"];
                short intQuantite = (short)drvProduit["NbItems"];
                long noPanier = (long)drvProduit["NoPanier"];
                int qttDispo = int.Parse(drvProduit["NombreItems"].ToString());

                lblNoProduit.Text = noProduit.ToString();
                imgProduit.ImageUrl = urlImage;
                lblCategorie.Text = strCategorie;
                lblDescriptionAbregee.Text = strDescriptionAbregee;
                lblPrixDemande.Text = "Prix demandé: " + decPrixDemande.ToString("C");
                lblQuantiteDispo.Text = qttDispo.ToString();
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

            ctrMontantsFactures.MessageErreur = IsValid ? null : "Les quantités sont invalides";
            //ctrMontantsFactures.CalculerCouts();
        }

        protected void btnViderPanier_OnClick(object sender, EventArgs e)
        {
            myConnection.Open();

            SqlCommand commandeSuppression = new SqlCommand("DELETE FROM PPArticlesEnPanier WHERE NoClient = " + Session["ID"] + " AND NoVendeur = " + NoVendeur, myConnection);
            commandeSuppression.ExecuteNonQuery();

            myConnection.Close();

            chargerProduits();
            //ctrMontantsFactures.CalculerCouts();
        }

        protected void btnCommander_OnClick(object sender, EventArgs e)
        {
            if (IsValid)
            {
                Response.Redirect(Chemin.Ajouter("Commande.aspx?novendeur=" + NoVendeur + "&codelivraison=" + ctrMontantsFactures.CodeLivraison, "Retour au panier"), true);
            }
        }

        protected void valQuantite_OnServerValidate(object sender, ServerValidateEventArgs e)
        {
            CustomValidator valQuantite = (CustomValidator)sender;
            short nbDemande;
            if (short.TryParse(e.Value, out nbDemande))
            {
                if (nbDemande < 0)
                {
                    valQuantite.Text = "Vous devez commander au moins un article";
                    e.IsValid = false;
                }
                else
                {
                    RepeaterItem item = (RepeaterItem)valQuantite.Parent;
                    Label lblNoProduit = (Label)item.FindControl("lblNoProduit");
                    long noProduit = long.Parse(lblNoProduit.Text);
                    myConnection.Open();

                    SqlCommand commandeNbItems = new SqlCommand("SELECT NombreItems FROM PPProduits WHERE NoProduit = " + noProduit, myConnection);
                    short nbDisponible = (short)commandeNbItems.ExecuteScalar();

                    myConnection.Close();

                    e.IsValid = (nbDemande <= nbDisponible);
                    valQuantite.Text = "Vous avez dépassé la quantité disponible";
                }
            }
            else
            {
                e.IsValid = false;
                valQuantite.Text = "Le format est invalide";
            }
        }
    }
}