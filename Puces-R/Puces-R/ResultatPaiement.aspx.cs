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
        private bool transactionAccepte = false;
        private Facture facture;

        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
        
        protected void Page_Load(object sender, EventArgs e)
        {
            int noClient = (int)Session["ID"];
            long noVendeur = long.Parse(Request.Params["novendeur"]);
            short codeLivraison = short.Parse(Request.Params["codelivraison"]);

            HttpRequest requete = HttpContext.Current.Request;

            String noAutorisation = (String)requete.Form["NoAutorisation"];
            String dateAutorisation = (String)requete.Form["DateAutorisation"];
            String fraisMarchand = (String)requete.Form["FraisMarchand"];

            switch (noAutorisation)
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
                    transactionAccepte = true;
                    hypReessayer.Visible = false;
                    litMessageResultat.Text = "Transaction acceptée";
                    facture = new Facture(noClient, noVendeur, codeLivraison);
                    break;
            }

            hypReessayer.NavigateUrl = "Commande.aspx?novendeur=" + noVendeur + "&codelivraison=" + codeLivraison;
        }

        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
            if (transactionAccepte)
            {
                EffectuerTransaction();
            }
        }

        public void EffectuerTransaction()
        {
            myConnection.Open();

            SqlCommand commandeNoCommande = new SqlCommand("SELECT MAX(NoCommande) FROM PPCommandes", myConnection);

            long noCommande = (long)commandeNoCommande.ExecuteScalar() + 1;

            SqlCommand commandePaiement = new SqlCommand("INSERT INTO PPCommandes VALUES (@noCommande, @noClient, @noVendeur, @dateCommande, @livraison, @typeLivraison, @montantTotal, @TPS, @TVQ, @poidsTotal, @statut, @noAutorisation)", myConnection);

            SqlParameterCollection parameters = commandePaiement.Parameters;

            parameters.Add(new SqlParameter("noCommande", noCommande));
            parameters.Add(new SqlParameter("noClient", Session["ID"]));
            parameters.Add(new SqlParameter("noVendeur", facture.NoVendeur));
            parameters.Add(new SqlParameter("dateCommande", DateTime.Now));
            parameters.Add(new SqlParameter("livraison", facture.PrixLivraison));
            parameters.Add(new SqlParameter("typeLivraison", facture.CodeLivraison));
            parameters.Add(new SqlParameter("montantTotal", facture.SousTotal));
            parameters.Add(new SqlParameter("TPS", facture.PrixTPS));
            parameters.Add(new SqlParameter("TVQ", facture.PrixTVQ));
            parameters.Add(new SqlParameter("poidsTotal", facture.PoidsTotal));
            parameters.Add(new SqlParameter("statut", "p"));
            parameters.Add(new SqlParameter("noAutorisation", 1));

            commandePaiement.ExecuteNonQuery();

            SqlCommand commandeNbItems = new SqlCommand("UPDATE P SET NombreItems = P.NombreItems - A.NbItems FROM PPProduits P INNER JOIN PPArticlesEnPanier A ON P.NoProduit = A.NoProduit WHERE A.NoClient = " + Session["ID"] + " AND P.NoVendeur = " + facture.NoVendeur, myConnection);
            commandeNbItems.ExecuteNonQuery();

            SqlCommand commandeViderPanier = new SqlCommand("DELETE FROM PPArticlesEnPanier WHERE NoClient = " + Session["ID"] + " AND NoVendeur = " + facture.NoVendeur, myConnection);
            commandeViderPanier.ExecuteNonQuery();

            myConnection.Close();
        }
    }
}