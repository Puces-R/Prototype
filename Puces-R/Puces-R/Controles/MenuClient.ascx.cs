using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R.Controles
{
    public partial class MenuClient : System.Web.UI.UserControl
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        public long NoVendeur
        {
            set
            {
                SqlCommand commandePanierVide = new SqlCommand("SELECT COUNT(*) FROM PPArticlesEnPanier WHERE NoVendeur = " + value + " AND NoClient = " + Session["ID"], myConnection);

                myConnection.Open();
                int nbItems = (int)commandePanierVide.ExecuteScalar();
                myConnection.Close();

                MenuItem itmPanier;

                if (ctrMenu.FindItem("Panier") == null)
                {
                    MenuItem itmProduits = new MenuItem("Produits", "Produits");
                    itmProduits.NavigateUrl = "../Produits.aspx?novendeur=" + value;
                    ctrMenu.Items.AddAt(1, itmProduits);

                    itmPanier = new MenuItem("Panier", "Panier");
                    itmPanier.NavigateUrl = "../Panier.aspx?novendeur=" + value;
                    ctrMenu.Items.AddAt(2, itmPanier);
                }
                else
                {
                    itmPanier = ctrMenu.FindItem("Panier");
                }

                itmPanier.Text = "Panier (" + nbItems + " item" + (nbItems > 1 ? "s" : "") + ")";
            }
        }
    }
}