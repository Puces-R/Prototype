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
        int[] dest = new int[1];

        protected void changer(Object sender, EventArgs e)
        {
            // Trace.Warn("changer");
            Response.Write("DEDANS ONCLICK");
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Data Insert Successfully...');", true);
            dest[0] = (Int32)ViewState["noClientMessage"];

            Librairie.Messagerie(dest, null, null, true, "Retour");

        }

        protected void changer_view(object sender, CommandEventArgs e)
        {
            if (e != null)
            {
                int allo = dest[0];
                dest[0] = (Int32)ViewState["noClientMessage"];
                // Response.Write(allo);
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
                    Response.Redirect(Chemin.UrlRetour == null ? "AccueilVendeur.aspx" : Chemin.UrlRetour, true);
                }

                else
                {
                    String whereClause = " WHERE NoClient = " + noClient.ToString();

                    SqlCommand commandeClient = new SqlCommand("SELECT * FROM PPClients" + whereClause, myConnection);


                    myConnection.Open();

                    SqlDataReader lecteurClient = commandeClient.ExecuteReader();
                    if (lecteurClient.Read())
                    {

                        dest[0] = lecteurClient["NoClient"] is DBNull ? 0 : Convert.ToInt32(lecteurClient["NoClient"]); ;
                        // Response.Write(dest[0]);

                        ViewState.Add("noClientMessage", dest[0]);
                        this.txtPrenom.Text = lecteurClient["Prenom"] is DBNull ? "" : (String)lecteurClient["Prenom"];
                        this.txtNom.Text = lecteurClient["Nom"] is DBNull ? "" : (String)lecteurClient["Nom"];
                        this.txtRue.Text = lecteurClient["Rue"] is DBNull ? "" : (String)lecteurClient["Rue"];
                        this.txtVille.Text = lecteurClient["Ville"] is DBNull ? "" : (String)lecteurClient["Ville"];
                        this.ctrProvince.CodeProvince = lecteurClient["Province"] is DBNull ? "" : (String)lecteurClient["Province"];
                        this.txtPays.Text = lecteurClient["Pays"] is DBNull ? "Canada" : (String)lecteurClient["Pays"];
                        this.ctrCodePostal.Code = lecteurClient["CodePostal"] is DBNull ? "" : (String)lecteurClient["CodePostal"];
                        this.ctrTelephone.NoTelephone = lecteurClient["Tel1"] is DBNull ? "" : (String)lecteurClient["Tel1"];
                        this.ctrCellulaire.NoTelephone = lecteurClient["Tel2"] is DBNull ? "" : (String)lecteurClient["Tel2"];

                        ctrBoitePanier.NoVendeur = (int)Session["ID"];
                        ctrBoitePanier.NoClient = noClient;
                        ctrBoitePanier.Titre = "Le panier du client #" + noClient;
                        ctrBoitePanier.ChargerArticlesEnPanier();
                    }
                    else
                    {
                        myConnection.Close();
                        Response.Redirect(Chemin.UrlRetour == null ? "AccueilVendeur.aspx" : Chemin.UrlRetour);
                    }



                    myConnection.Close();
                }
            }

        }



    }
}