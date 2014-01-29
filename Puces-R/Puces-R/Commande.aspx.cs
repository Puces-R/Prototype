using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class Commande : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int noVendeur;
                if (!int.TryParse(Request.Params["novendeur"], out noVendeur))
                {
                    Response.Redirect("Default.aspx", true);
                }

                ((SiteMaster)Master).NoVendeur = noVendeur;
                ctrMenu.NoVendeur = noVendeur;
                ctrMontantsFactures.NoVendeur = noVendeur;
                ctrTablePanier.NoVendeur = noVendeur;
            }
        }
    }
}