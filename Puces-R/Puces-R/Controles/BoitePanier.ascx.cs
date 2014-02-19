using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Puces_R.Controles
{
    public partial class BoitePanier : System.Web.UI.UserControl
    {
        SqlConnection myConnexion = Librairie.Connexion;

        public String Titre
        {
            set
            {
                hypTitre.Text = value;
            }
        }

        public String NavigateUrl
        {
            set
            {
                hypTitre.NavigateUrl = value;
            }
        }

        public long NoClient
        {
            get
            {
                return (long)ViewState["NoClient"];
            }
            set
            {
                ViewState["NoClient"] = value;
            }
        }

        public long NoVendeur
        {
            get
            {
                return (long)ViewState["NoVendeur"];
            }
            set
            {
                ViewState["NoVendeur"] = value;
            }
        }

        public void ChargerArticlesEnPanier() 
        {
            SqlCommand commandePanier = new SqlCommand("SELECT TOP(1) SUM(A.NbItems * ISNULL(P.PrixVente, P.PrixDemande)) AS SousTotal FROM PPArticlesEnPanier AS A INNER JOIN PPProduits AS P ON A.NoProduit = P.NoProduit WHERE A.NoClient = " + NoClient + " AND A.NoVendeur = " + NoVendeur + " GROUP BY A.NoVendeur", myConnexion);

            myConnexion.Open();

            SqlDataReader lecteurPanier = commandePanier.ExecuteReader();
            lecteurPanier.Read();

            decimal sousTotal = (decimal)lecteurPanier["SousTotal"];

            myConnexion.Close();

            lblSousTotal.Text = sousTotal.ToString("C");
            ctrProduits.NoVendeur = NoVendeur;
            ctrProduits.NoClient = NoClient;
            ctrProduits.ChargerArticlesEnPanier();
        }
    }
}