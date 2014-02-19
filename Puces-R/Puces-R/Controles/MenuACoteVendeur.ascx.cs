using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;

namespace Puces_R.Controles
{
    public partial class MenuACoteVendeur : UserControl
    {
        SqlConnection myConnection = Librairie.Connexion;

        public long NoVendeur
        {
            set
            {
                loadNbItems(value);
                MenuItem itmPanier = ctrMenu.FindItem("Panier");
                MenuItem itmProduits = ctrMenu.FindItem("Produits");
                MenuItem itmHistorique = ctrMenu.FindItem("Historique");

                itmPanier.NavigateUrl = "../Panier.aspx?novendeur=" + value;
                itmProduits.NavigateUrl = "../Produits.aspx?novendeur=" + value;
                itmHistorique.NavigateUrl = "../CommandesClient.aspx?novendeur=" + value;
            }
        }

        public void loadNbItems(long noVendeur)
        {
            SqlCommand commandePanierVide = new SqlCommand("SELECT COUNT(*) FROM PPArticlesEnPanier WHERE NoVendeur = " + noVendeur + " AND NoClient = " + Session["ID"], myConnection);

            myConnection.Open();
            int nbItems = (int)commandePanierVide.ExecuteScalar();
            myConnection.Close();

            ctrMenu.FindItem("Panier").Text = "Panier (" + nbItems + " item" + (nbItems > 1 ? "s" : "") + ")";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Librairie.SelectionnerItemMenuActuel(ctrMenu.Items, Path.GetFileNameWithoutExtension(Request.Url.AbsoluteUri));
        }
    }
}