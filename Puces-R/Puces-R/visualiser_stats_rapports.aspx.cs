using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Puces_R
{
    public partial class visualiser_stats_rapports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((SiteMaster)Master).Vendeur = "Statistiques & Rapports";
        }
    }
}