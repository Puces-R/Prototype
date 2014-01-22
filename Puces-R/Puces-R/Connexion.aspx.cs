using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class Connexion : System.Web.UI.Page
    {
        SqlConnection connexion = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2;");

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void seConnecter(object sender, EventArgs e)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                SqlCommand cmdConnexion = new SqlCommand("SELECT No, Type FROM " +
                                                            "(SELECT NoClient AS No, 'C' AS Type, AdresseEmail, MotDePasse FROM PPClients UNION " +
                                                             "SELECT NoVendeur AS No, 'V' AS Type, AdresseEmail, MotDePasse FROM PPVendeurs UNION " +
                                                             "SELECT NoGestionnaire AS No, 'G' AS Type, AdresseEmail, MotDePasse FROM PPGestionnaires) AS X " +
                                                         "WHERE (AdresseEmail LIKE @adr) AND (MotDePasse COLLATE sql_latin1_General_CP1_cs_as LIKE @mdp)", connexion);
                cmdConnexion.Parameters.AddWithValue("@adr", tbIdentifiants.Adresse);
                cmdConnexion.Parameters.AddWithValue("@mdp", tbIdentifiants.MotDePasse);
                connexion.Open();
                SqlDataReader sdr = cmdConnexion.ExecuteReader();
                if (sdr.Read())
                {
                    Session["ID"] = int.Parse(sdr["No"].ToString());
                    Session["Type"] = char.Parse(sdr["Type"].ToString());

                    switch ((char)Session["Type"])
                    {
                        case 'C':
                            Response.Redirect("http://fr.wikipedia.org/wiki/Client", false);
                            break;
                        case 'V':
                            Response.Redirect("http://fr.wikipedia.org/wiki/Vendeur", false);
                            break;
                        case 'G':
                            Response.Redirect("http://fr.wikipedia.org/wiki/Gestionnaire", false);
                            break;
                    }
                }
                else
                {
                    Response.Write("NOPE");
                }

                connexion.Close();
            }
        }
    }
}