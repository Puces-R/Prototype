using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class CommuniquerClient : System.Web.UI.Page
    {
        SqlConnection myConnection = Librairie.Connexion;
        long noClient = 0;
        long[] dest = new long[1];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Librairie.Autorisation(false, false, true, false);
            }

            if (!Int64.TryParse(Request.Params["noClient"], out noClient))
            {
                Response.Redirect(Chemin.UrlRetour == null ? "GestionCommandesVendeur.aspx" : Chemin.UrlRetour);
            }
            else
            {
                SqlCommand commandeClient = new SqlCommand("SELECT * FROM PPClients WHERE NoClient = @cli AND NoClient IN (SELECT NoClient FROM PPCommandes WHERE NoVendeur = @ven GROUP BY NoClient)", myConnection);
                commandeClient.Parameters.AddWithValue("@cli", noClient);
                commandeClient.Parameters.AddWithValue("@ven", Session["ID"]);

                myConnection.Open();

                SqlDataReader lecteurClient = commandeClient.ExecuteReader();
                if (lecteurClient.Read())
                {
                    dest[0] = lecteurClient["NoClient"] == DBNull.Value ? 0 : Convert.ToInt64(lecteurClient["NoClient"]);

                    //ViewState.Add("noClientMessage", dest[0]);
                    this.lblPrenom.Text = lecteurClient["Prenom"] is DBNull ? "" : (String)lecteurClient["Prenom"];
                    this.lblNom.Text = lecteurClient["Nom"] is DBNull ? "" : (String)lecteurClient["Nom"];
                    this.lblRue.Text = lecteurClient["Rue"] is DBNull ? "" : (String)lecteurClient["Rue"];
                    this.lblVille.Text = lecteurClient["Ville"] is DBNull ? "" : (String)lecteurClient["Ville"];
                    this.lblProvince.Text = lecteurClient["Province"] is DBNull ? "" : Librairie.provinceTexte((String)lecteurClient["Province"]);
                    this.lblPays.Text = lecteurClient["Pays"] is DBNull ? "Canada" : (String)lecteurClient["Pays"];
                    this.lblCodePostal.Text = lecteurClient["CodePostal"] is DBNull ? "" : CodePostal.Format((String)lecteurClient["CodePostal"]);
                    this.lblTelephone1.Text = lecteurClient["Tel1"] is DBNull ? "" : Telephone.Format((String)lecteurClient["Tel1"]);
                    this.lblTelephone2.Text = lecteurClient["Tel2"] is DBNull ? "Aucun" : Telephone.Format((String)lecteurClient["Tel2"]);

                    //ctrBoitePanier.NoVendeur = (int)Session["ID"];
                    //ctrBoitePanier.NoClient = noClient;
                    //ctrBoitePanier.Titre = "Le panier du client #" + noClient;
                    //ctrBoitePanier.ChargerArticlesEnPanier();

                    //btnCourrielExterne.CommandArgument = dest[0].ToString();
                    //btnCourrierInterne.CommandArgument = dest[0].ToString();
                }
                else
                {
                    Response.Redirect(Chemin.UrlRetour == null ? "GestionCommandesVendeur.aspx" : Chemin.UrlRetour);
                }

                myConnection.Close();
            }
        }

        protected void changer_view(object sender, CommandEventArgs e)
        {
            switch (Convert.ToInt64(e.CommandArgument.ToString()))
            {
                case 1:
                    Librairie.Messagerie(dest, null, null, true, "Retour");
                    break;
                case 2:
                    Librairie.Courriel(dest, null, null, true, "Retour");
                    break;
            }
        }
    }
}