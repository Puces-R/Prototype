using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Puces_R.Controles
{
    public partial class MenuInvite : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string urlRequete = System.IO.Path.GetFileName(Request.Url.AbsoluteUri);
            foreach (MenuItem item in ctrMenu.Items)
            {
                string urlItem = System.IO.Path.GetFileName(item.NavigateUrl);
                if (item.Selectable)
                {
                    item.Selected = urlRequete.Contains(urlItem);
                }
            }
        }
    }
}