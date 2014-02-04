using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;

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

                short codeLivraison;
                if (!short.TryParse(Request.Params["codelivraison"], out codeLivraison))
                {
                    Response.Redirect("Default.aspx", true);
                }
                
                ((SiteMaster)Master).NoVendeur = noVendeur;
                ctrMenu.NoVendeur = noVendeur;
                ctrMontantsFactures.NoVendeur = noVendeur;
                ctrTablePanier.NoVendeur = noVendeur;
                ctrTablePanier.NoClient = (int)Session["ID"];
                ctrMontantsFactures.CodeLivraison = codeLivraison;
            }
        }

        protected void btnFacturer_OnClick(object sender, EventArgs e)
        {
            ctrProfilClient.Sauvegarder();

            long noVendeur = ctrMontantsFactures.NoVendeur;
            long codeLivraison = ctrMontantsFactures.CodeLivraison;

            SqlCommand commandeNomVendeur = new SqlCommand("SELECT NomAffaires FROM PPVendeurs WHERE NoVendeur = " + noVendeur, myConnection);

            myConnection.Open();
            String nomVendeur = (String)commandeNomVendeur.ExecuteScalar();
            myConnection.Close();

            Response.Clear();

            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            sb.Append(@"<body onload='document.forms[""form""].submit()'>");
            sb.Append("<form name='form' action='http://424w.cgodin.qc.ca/lmbrousseau/demo-lesi/lesi-effectue-paiement.php' method='post'>");

            String baliseInput = "<input type='hidden' name='{0}' value='{1}'>";

            sb.AppendFormat(baliseInput, "NoVendeur", noVendeur);
            sb.AppendFormat(baliseInput, "NomVendeur", nomVendeur);
            sb.AppendFormat(baliseInput, "NoCarteCredit", txtNumero.Text);
            sb.AppendFormat(baliseInput, "DateExpirationCarteCredit", txtDateExpiration.Text);
            sb.AppendFormat(baliseInput, "NoSecuriteCarteCredit", txtCCV.Text);
            sb.AppendFormat(baliseInput, "MontantPaiement", ctrMontantsFactures.GrandTotal);
            sb.AppendFormat(baliseInput, "NomPageRetour", "http://" + Request.Url.Authority + "/ResultatPaiement.aspx?novendeur=" + noVendeur + "&codeLivraison=" + codeLivraison);
            sb.AppendFormat(baliseInput, "InfoSuppl", String.Empty);

            sb.Append("</form>");
            sb.Append("</body>");
            sb.Append("</html>");

            Response.Write(sb.ToString());

            Response.End();
        }
    }
}