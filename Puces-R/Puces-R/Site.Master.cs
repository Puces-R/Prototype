using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Puces_R
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        public String Vendeur
        {
            set
            {
                lblVendeur.Text = value;
                pnlVendeur.Visible = true;
            }
        }
    }
}
