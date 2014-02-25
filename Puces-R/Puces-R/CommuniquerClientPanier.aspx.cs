using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class CommuniquerClientPanier : System.Web.UI.Page
    {
        SqlConnection myConnection = Librairie.Connexion;
        int noClient = 0;
        long[] dest = new long[1];

        protected void changer(Object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Data Insert Successfully...');", true);
            dest[0] = (long)ViewState["noClientMessage"];

            Librairie.Messagerie(dest, null, null, true, "Retour");

        }

        protected void changer_view(object sender, CommandEventArgs e)
        {
            if (e != null)
            {
                long allo = dest[0];
                dest[0] = (long)ViewState["noClientMessage"];
                switch (Convert.ToInt32(e.CommandArgument.ToString()))
                {

                    case 1:
                        Librairie.Messagerie(dest, null, null, true, "Retour");
                        break;
                    case 2:
                        Librairie.Courriel(dest, null, null, true, "Retour");
                        break;

                    default:

                        break;
                }
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {



            if (!IsPostBack)
            {
                Librairie.Autorisation(false, false, true, false);
                if (!int.TryParse(Request.Params["noClient"], out noClient))
                {
                    Response.Redirect(Chemin.UrlRetour == null ? "GererPanierVendeur.aspx" : Chemin.UrlRetour, true);
                }

                else
                {
                    SqlCommand commandeClient = new SqlCommand("SELECT * FROM PPClients WHERE NoClient = @cli AND NoClient IN (SELECT NoClient FROM PPArticlesEnPanier WHERE NoVendeur = @ven GROUP BY NoClient)", myConnection);
                    commandeClient.Parameters.AddWithValue("@cli", noClient);
                    commandeClient.Parameters.AddWithValue("@ven", Session["ID"]);

                    myConnection.Open();

                    SqlDataReader lecteurClient = commandeClient.ExecuteReader();
                    if (lecteurClient.Read())
                    {

                        dest[0] = lecteurClient["NoClient"] is DBNull ? 0 : Convert.ToInt32(lecteurClient["NoClient"]); ;

                        ViewState.Add("noClientMessage", dest[0]);
                        this.lblPrenom.Text = lecteurClient["Prenom"] is DBNull ? "" : (String)lecteurClient["Prenom"];
                        this.lblNom.Text = lecteurClient["Nom"] is DBNull ? "" : (String)lecteurClient["Nom"];
                        this.lblRue.Text = lecteurClient["Rue"] is DBNull ? "" : (String)lecteurClient["Rue"];
                        this.lblVille.Text = lecteurClient["Ville"] is DBNull ? "" : (String)lecteurClient["Ville"];
                        this.lblProvince.Text = lecteurClient["Province"] is DBNull ? "" : Librairie.provinceTexte((String)lecteurClient["Province"]);
                        this.lblPays.Text = lecteurClient["Pays"] is DBNull ? "Canada" : (String)lecteurClient["Pays"];
                        this.lblCodePostal.Text = lecteurClient["CodePostal"] is DBNull ? "" : CodePostal.Format((String)lecteurClient["CodePostal"]);
                        this.lblTelephone1.Text = lecteurClient["Tel1"] is DBNull ? "" : Telephone.Format((String)lecteurClient["Tel1"]);
                        this.lblTelephone2.Text = lecteurClient["Tel2"] is DBNull ? "Aucun" : Telephone.Format((String)lecteurClient["Tel2"]);

                        ctrBoitePanier.NoVendeur = (int)Session["ID"];
                        ctrBoitePanier.NoClient = noClient;
                        ctrBoitePanier.Titre = "Le panier du client #" + noClient;
                        ctrBoitePanier.ChargerArticlesEnPanier();
                    }
                    else
                    {
                        myConnection.Close();
                        Response.Redirect(Chemin.UrlRetour == null ? "GererPanierVendeur.aspx" : Chemin.UrlRetour);
                    }



                    myConnection.Close();
                }
            }

        }



    }
}