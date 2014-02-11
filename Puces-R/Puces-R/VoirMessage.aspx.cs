using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Net;
using System.IO;

namespace Puces_R
{
    public partial class VoirMessage : System.Web.UI.Page
    {
        SqlConnection connexion = Librairie.Connexion;

        private int noExpediteur = -1;

        protected void repondre(object sender, EventArgs e)
        {
            Librairie.Messagerie(new int[] {noExpediteur} , "RE : " + lblSujet.Text);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Int64 noMessage;
            if (Request.QueryString["No"] != null && Int64.TryParse(Request.QueryString["No"], out noMessage))
            {
                
                SqlCommand cmdEstDestinataire = new SqlCommand("SELECT CASE WHEN COUNT(*) > 0 THEN 'true' ELSE 'false' END FROM PPDestinatairesMessages WHERE (NoMessage = @noMsg) AND (NoDestinataire = @noRcpt) AND (Boite > 0)", connexion);
                SqlCommand cmdEstExpediteur = new SqlCommand("SELECT CASE WHEN COUNT(*) = 1 THEN 'true' ELSE 'false' END FROM PPMessages WHERE (NoMessage = @noMsg) AND (NoExpediteur = @id) AND (Boite < 0)", connexion);

                SqlCommand cmdMessage = new SqlCommand("SELECT M.NoExpediteur, X.Texte, M.DateEnvoi, M.Sujet, M.Contenu FROM PPMessages M " +
                    "INNER JOIN PPDestinatairesMessages DM ON M.NoMessage = DM.NoMessage " +
                    "INNER JOIN (SELECT NoClient AS No, ISNULL(Nom + ', ' + RTRIM(Prenom) + ' <' + AdresseEmail + '>', AdresseEmail) + ' (Client)' AS Texte FROM PPClients UNION " +
                                "SELECT NoVendeur AS No, RTRIM(NomAffaires) + ' <' + AdresseEmail + '> (Vendeur)' AS Texte FROM PPVendeurs UNION " +
                                "SELECT NoGestionnaire AS No, RTRIM(Nom) + ', ' + RTRIM(Prenom) + ' <' + AdresseEmail + '> (Gestionnaire)' AS Texte FROM PPGestionnaires) AS X " +
                    "ON X.No = M.NoExpediteur WHERE (M.NoMessage = @noMsg)", connexion);

                SqlCommand cmdLu = new SqlCommand("UPDATE PPDestinatairesMessages SET Lu = 1 WHERE NoMessage = @noMsg AND NoDestinataire = @noRcpt", connexion);

                cmdEstDestinataire.Parameters.AddWithValue("@noMsg", noMessage);
                cmdEstDestinataire.Parameters.AddWithValue("@noRcpt", Session["ID"].ToString());

                cmdEstExpediteur.Parameters.AddWithValue("@noMsg", noMessage);
                cmdEstExpediteur.Parameters.AddWithValue("@id", Session["ID"].ToString());

                cmdMessage.Parameters.AddWithValue("@noMsg", noMessage);
                cmdMessage.Parameters.AddWithValue("@id", Session["ID"].ToString());

                cmdLu.Parameters.AddWithValue("@noMsg", noMessage);
                cmdLu.Parameters.AddWithValue("@noRcpt", Session["ID"]);


                connexion.Open();

                bool estDestinataire = bool.Parse(cmdEstDestinataire.ExecuteScalar().ToString());
                bool estExpediteur = bool.Parse(cmdEstExpediteur.ExecuteScalar().ToString());

                if (estDestinataire || estExpediteur)
                {

                    if (estExpediteur)
                    {
                        lnkRepondre.Visible = false;
                    }

                    cmdLu.ExecuteNonQuery();

                    SqlDataReader sdr = cmdMessage.ExecuteReader();

                    if (sdr.Read())
                    {
                        lblDate.Text = ((DateTime)sdr["DateEnvoi"]).ToString("d MMMM yyyy à h\\hmm");
                        lblDe.Text = sdr["Texte"].ToString();
                        lblMessage.Text = sdr["Contenu"].ToString().Replace("\r\n", "<br />");
                        lblSujet.Text = sdr["Sujet"].ToString();
                        noExpediteur = int.Parse(sdr["NoExpediteur"].ToString());
                    }
                    else
                    {
                        // Bonne gestion de l'erreur ?
                        Response.Redirect("BoiteMessage.aspx", true);
                    }
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