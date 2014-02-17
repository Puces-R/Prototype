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
                SqlCommand commandePanierVide = new SqlCommand("SELECT COUNT(*) FROM PPArticlesEnPanier WHERE NoVendeur = " + value + " AND NoClient = " + Session["ID"], myConnection);

                myConnection.Open();
                int nbItems = (int)commandePanierVide.ExecuteScalar();
                myConnection.Close();

                MenuItem itmPanier = ctrMenu.FindItem("Panier");
                MenuItem itmProduits = ctrMenu.FindItem("Produits");
                MenuItem itmHistorique = ctrMenu.FindItem("Historique");

                itmPanier.NavigateUrl = "../Panier.aspx?novendeur=" + value;
                itmPanier.Text = "Panier (" + nbItems + " item" + (nbItems > 1 ? "s" : "") + ")";
                itmProduits.NavigateUrl = "../Produits.aspx?novendeur=" + value;
                itmHistorique.NavigateUrl = "../CommandesClient.aspx?novendeur=" + value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Librairie.SelectionnerItemMenuActuel(ctrMenu.Items, Path.GetFileNameWithoutExtension(Request.Url.AbsoluteUri));
        }
    }
}