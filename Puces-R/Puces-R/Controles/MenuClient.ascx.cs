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
    }
}