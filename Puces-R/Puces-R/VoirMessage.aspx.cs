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
        SqlConnection connexion = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2;");

        protected void Page_Load(object sender, EventArgs e)
        {
            Int64 noMessage = (Int64)Session["NoMessage"];
            Session.Remove("NoMessage");

            SqlCommand cmdMessage = new SqlCommand("SELECT X.AdresseEmail, M.DateEnvoi, M.Sujet, M.Contenu FROM PPMessages AS M INNER JOIN " +
                                                   "(SELECT NoClient AS No, AdresseEmail FROM PPClients UNION " +
                                                    "SELECT NoVendeur AS No, AdresseEmail FROM PPVendeurs UNION " +
                                                    "SELECT NoGestionnaire AS No, AdresseEmail FROM PPGestionnaires) AS X " +
                                                    "ON M.Envoyeur = X.No " +
                                                    "WHERE M.Recepteur = 10700 AND M.NoMessage = @noMsg", connexion);

            SqlCommand cmdLu = new SqlCommand("UPDATE PPMessages SET Lu = 1 WHERE NoMessage = @noMsg AND Recepteur = @noRcpt", connexion);

            cmdMessage.Parameters.AddWithValue("@noMsg", noMessage);
            cmdMessage.Parameters.AddWithValue("@noRcpt", 10700); // cmdMessage.Parameters.AddWithValue("@noRcpt", (Int64)Session["ID"]);

            cmdLu.Parameters.AddWithValue("@noMsg", noMessage);
            cmdLu.Parameters.AddWithValue("@noRcpt", 10700); // cmdLu.Parameters.AddWithValue("@noRcpt", (Int64)Session["ID"]);

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

            connexion.Close();
        }
    }
}