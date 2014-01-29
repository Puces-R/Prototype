using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class VoirMessage : System.Web.UI.Page
    {
        SqlConnection connexion = Librairie.Connexion;

        protected void Page_Load(object sender, EventArgs e)
        {
            Int64 noMessage;
            if (Request.QueryString["No"] != null && Int64.TryParse(Request.QueryString["No"], out noMessage))
            {

                SqlCommand cmdMessage = new SqlCommand("SELECT X.AdresseEmail, M.DateEnvoi, M.Sujet, M.Contenu FROM PPMessages AS M INNER JOIN " +
                                                       "(SELECT NoClient AS No, AdresseEmail FROM PPClients UNION " +
                                                        "SELECT NoVendeur AS No, AdresseEmail FROM PPVendeurs UNION " +
                                                        "SELECT NoGestionnaire AS No, AdresseEmail FROM PPGestionnaires) AS X " +
                                                        "ON M.Envoyeur = X.No " +
                                                        "WHERE M.NoMessage = @noMsg AND " +
                                                        "(M.Envoyeur = @id OR M.Recepteur = @id)", connexion);

                SqlCommand cmdLu = new SqlCommand("UPDATE PPMessages SET Lu = 1 WHERE NoMessage = @noMsg AND Recepteur = @noRcpt", connexion);

                cmdMessage.Parameters.AddWithValue("@noMsg", noMessage);
                cmdMessage.Parameters.AddWithValue("@id", 10700 /*Session["ID"].ToString()*/);

                cmdLu.Parameters.AddWithValue("@noMsg", noMessage);
                cmdLu.Parameters.AddWithValue("@noRcpt", 10700 /*Session["ID"]*/);

                connexion.Open();

                cmdLu.ExecuteNonQuery();

                SqlDataReader sdr = cmdMessage.ExecuteReader();

                if (sdr.Read())
                {
                    lblDate.Text = ((DateTime)sdr["DateEnvoi"]).ToString("d MMMM yyyy à h\\hmm");
                    lblDe.Text = sdr["AdresseEmail"].ToString();
                    lblMessage.Text = sdr["Contenu"].ToString().Replace("\r\n", "<br />");
                    lblSujet.Text = sdr["Sujet"].ToString();
                }
                else
                {
                    Response.Redirect("BoiteMessage.aspx", true);
                }

                connexion.Close();
            }
            else
            {
                Response.Redirect("BoiteMessage.aspx", true);
            }
        }
    }
}