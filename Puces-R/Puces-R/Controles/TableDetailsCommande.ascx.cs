using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Puces_R.Controles
{
    public partial class TableDetailsCommande : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

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

        public long NoCommande
        {
            get
            {
                return (long)ViewState["NoCommande"];
            }
            set
            {
                ViewState["NoCommande"] = value;
            }
        }

        public void ChargerArticlesEnPanier()
        {
            SqlDataAdapter adapteurProduits = new SqlDataAdapter("SELECT P.Nom, A.Quantité, A.PrixVente, A.NoProduit FROM PPDetailsCommandes A INNER JOIN PPProduits P ON A.NoProduit = P.NoProduit inner join PPCommandes CO on A.Nocommande = CO.noCommande WHERE CO.NoCommande =" +NoCommande, myConnection);
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
                short quantite = (short)drvProduit["Quantité"];
                decimal prixUnitaire = (decimal)drvProduit["PrixVente"];
                decimal prixTotal = quantite * prixUnitaire;
                long noProduit = (long)drvProduit["NoProduit"];

                hypProduit.Text = produit;
                hypProduit.NavigateUrl = Chemin.Ajouter("../DetailsProduit.aspx?noproduit=" + noProduit, "Retour au details de la commande");
                lblQuantite.Text = quantite.ToString();
                lblPrixUnitaire.Text = prixUnitaire.ToString("C");
                lblPrixTotal.Text = prixTotal.ToString("C");
            }
        }
    }
}