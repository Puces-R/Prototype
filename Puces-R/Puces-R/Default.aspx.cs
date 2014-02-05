using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class Default : System.Web.UI.Page
    {
        SqlConnection connexion = Librairie.Connexion;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
        }

        protected void seConnecter(object sender, EventArgs e)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                switch ((char)Session["Type"])
                {
                    case 'C':
                        Response.Redirect("AccueilClient.aspx", false);
                        break;
                    case 'V':
                        Response.Redirect("AcceuilVendeur.aspx", false);
                        break;
                    case 'G':
                        Response.Redirect("accueil_gestionnaire.aspx", false);
                        break;
                }
            }
        }

        protected void existe(object sender, ServerValidateEventArgs e)
        {
            SqlCommand cmdConnexion = new SqlCommand("SELECT No, Type FROM " +
                                                            "(SELECT NoClient AS No, 'C' AS Type, AdresseEmail, MotDePasse FROM PPClients UNION " +
                                                             "SELECT NoVendeur AS No, 'V' AS Type, AdresseEmail, MotDePasse FROM PPVendeurs UNION " +
                                                             "SELECT NoGestionnaire AS No, 'G' AS Type, AdresseEmail, MotDePasse FROM PPGestionnaires) AS X " +
                                                         "WHERE (AdresseEmail LIKE @adr) AND (MotDePasse COLLATE sql_latin1_General_CP1_cs_as LIKE @mdp)", connexion);

            cmdConnexion.Parameters.AddWithValue("@adr", tbCourriel.Text);
            cmdConnexion.Parameters.AddWithValue("@mdp", tbMotPasse.MotPasse);
            connexion.Open();
            SqlDataReader sdr = cmdConnexion.ExecuteReader();
            if (sdr.Read())
            {
                e.IsValid = true;
                Session["ID"] = int.Parse(sdr["No"].ToString());
                Session["Type"] = char.Parse(sdr["Type"].ToString());
            }
            else
            {
                e.IsValid = false;
            }

            connexion.Close();
        }

        protected void defautClient(object sender, EventArgs e)
        {
            Session["ID"] = 30063;
            Session["Type"] = 'C';
            Response.Redirect("AccueilClient.aspx", false);
        }

        protected void defautVendeur(object sender, EventArgs e)
        {
            Session["ID"] = 10;
            Session["Type"] = 'V';
            Response.Redirect("AcceuilVendeur.aspx", false);
        }

        protected void defautGestionnaire(object sender, EventArgs e)
        {
            Session["ID"] = 1;
            Session["Type"] = 'G';
            Response.Redirect("accueil_gestionnaire.aspx", false);
        }
    }
}