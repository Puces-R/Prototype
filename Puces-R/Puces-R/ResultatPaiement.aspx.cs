using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class ResultatPaiement : System.Web.UI.Page
    {
        private String NoAutorisation
        {
            get
            {
                return (String)ViewState["NoAutorisation"];
            }
            set
            {
                ViewState["NoAutorisation"] = value;
            }
        }
        
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        protected void Page_Load(object sender, EventArgs e)
        {
            long noVendeur = long.Parse(Request.Params["novendeur"]);
            short codeLivraison = short.Parse(Request.Params["codelivraison"]);

            HttpRequest requete = HttpContext.Current.Request;

            NoAutorisation = (String)requete.Form["NoAutorisation"];
            String dateAutorisation = (String)requete.Form["DateAutorisation"];
            String fraisMarchand = (String)requete.Form["FraisMarchand"];
            
            switch (NoAutorisation)
            {
                case "0":
                    litMessageResultat.Text = "Transaction annulée par l'utilisateur";
                    break;
                case "1":
                    litMessageResultat.Text = "Transaction refusée : Date d'expiration dépassée";
                    break;
                case "2":
                    litMessageResultat.Text = "Transaction refusée : Limite de crédit atteinte";
                    break;
                case "3":
                    litMessageResultat.Text = "Transaction refusée : Carte refusée";
                    break;
                default:

                    litMessageResultat.Text = "Transaction acceptée";
                    pnlMontantsFactures.Visible = true;
                    ctrMontantsFactures.NoVendeur = noVendeur;
                    ctrMontantsFactures.CodeLivraison = codeLivraison;
                    ctrMontantsFactures.ChargerModesDeLivraison();
                    break;
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (int.Parse(NoAutorisation) >= 1000)
            {
                ctrMontantsFactures.EffectuerTransaction();
            }
        }
    }
}