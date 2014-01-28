using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Puces_R.Controles
{
    public partial class MenuClient : System.Web.UI.UserControl
    {
        public long NoVendeur
        {
            set
            {
                MenuItem itmPanier = new MenuItem("Panier");
                itmPanier.NavigateUrl = "../Panier.aspx?novendeur=" + value;
                ctrMenu.Items.AddAt(1, itmPanier);
            }
        }
    }
}