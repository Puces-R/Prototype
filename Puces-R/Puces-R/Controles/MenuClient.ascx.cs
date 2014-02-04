using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;

namespace Puces_R.Controles
{
    public partial class MenuClient : System.Web.UI.UserControl
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        protected void Page_Load(object sender, EventArgs e)
        {
            String urlPage = Path.GetFileNameWithoutExtension(Request.Url.AbsoluteUri);
            SelectionnerCourant(ctrMenu.Items, urlPage);
        }

        private void SelectionnerCourant(MenuItemCollection items, String urlPage)
        {
            foreach (MenuItem item in items)
            {
                if (item.Selectable)
                {
                    String urlItem = Path.GetFileNameWithoutExtension(item.NavigateUrl);
                    item.Selected = String.Equals(urlItem, urlPage);
                }
                SelectionnerCourant(item.ChildItems, urlPage);
            }
        }

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