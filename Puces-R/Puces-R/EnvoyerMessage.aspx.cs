using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Puces_R
{
    public partial class EnvoyerMessage : System.Web.UI.Page
    {
        SqlConnection connexion = Librairie.Connexion;
        private int[] lstNoDestinataires;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScript.GetPostBackEventReference(this, string.Empty);
            string parametres = Request["__EVENTARGUMENT"];
            if (Request["__EVENTTARGET"] == "ChoixDestinataires" && parametres != string.Empty)
            {
                lbDestinataires.Items.Clear();
                string[] lstParametres = parametres.Split(",".ToArray());
                lstNoDestinataires = new int[lstParametres.Length];
                for (int i = 0; i < lstNoDestinataires.Length; i++)
                {
                    lstNoDestinataires[i] = int.Parse(lstParametres[i]);
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connexion;

                string[] param = new string[lstNoDestinataires.Length];

                for (int i = 0; i < lstNoDestinataires.Length; i++)
                {
                    param[i] = string.Format("@no{0}", i);
                    cmd.Parameters.AddWithValue(param[i], lstNoDestinataires[i]);
                }
                cmd.CommandText = "SELECT No, Texte FROM " +
                                  "(SELECT NoClient AS No, ISNULL(Nom + ', ' + RTRIM(Prenom) + ' <' + AdresseEmail + '>', AdresseEmail) + ' (Client)' AS Texte FROM PPClients UNION " +
                                   "SELECT NoVendeur AS No, RTRIM(NomAffaires) + ' <' + AdresseEmail + '> (Vendeur)' AS Texte FROM PPVendeurs UNION " +
                                   "SELECT NoGestionnaire AS No, RTRIM(Nom) + ', ' + RTRIM(Prenom) + ' <' + AdresseEmail + '> (Gestionnaire)' AS Texte FROM PPGestionnaires) AS X " +
                     string.Format("WHERE (No IN ({0}))", string.Join(", ", param));
                connexion.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    ListItem i = new ListItem(sdr["Texte"].ToString(), sdr["No"].ToString());
                    lbDestinataires.Items.Add(i);
                }
                sdr.Close();
                connexion.Close();
            }

            lbDestinataires.Rows = lbDestinataires.Items.Count < 5 ? (lbDestinataires.Items.Count > 0 ? lbDestinataires.Items.Count : 1) : 5;

        }

        protected void envoyerMessage(object sender, EventArgs e)
        {
            if (lbDestinataires.Items.Count > 0 && tbSujet.Text.Trim() != string.Empty && tbMessage.Text.Trim() != string.Empty)
            {
                SqlCommand cmdMessage = new SqlCommand("INSERT INTO PPMessages values(@no, @from, @date, @sujet, @contenu, NULL, -1, NULL)", connexion);
                SqlCommand cmdNoMessage = new SqlCommand("SELECT ISNULL(MAX(NoMessage), 0) + 1 FROM PPMessages", connexion);
                SqlCommand cmdDestinataires = new SqlCommand("INSERT INTO PPDestinatairesMessages values(@rcpt, @no, 'false', 1, NULL)", connexion);

                connexion.Open();

                Int64 noMessage = Convert.ToInt64(cmdNoMessage.ExecuteScalar().ToString());
                cmdMessage.Parameters.AddWithValue("@no", noMessage);
                cmdMessage.Parameters.AddWithValue("@from", Session["ID"]);
                cmdMessage.Parameters.AddWithValue("@date", DateTime.Now);
                cmdMessage.Parameters.AddWithValue("@sujet", tbSujet.Text);
                cmdMessage.Parameters.AddWithValue("@contenu", tbMessage.Text);

                cmdDestinataires.Parameters.AddWithValue("@no", noMessage);
                cmdDestinataires.Parameters.Add("@rcpt", SqlDbType.BigInt);
                cmdMessage.ExecuteNonQuery();
                foreach (ListItem li in lbDestinataires.Items)
                {
                    cmdDestinataires.Parameters["@rcpt"].Value = li.Value;
                    cmdDestinataires.ExecuteNonQuery();
                }
                connexion.Close();

                Response.Redirect("BoiteMessage.aspx");
            }
        }

        protected void apercuMessage(object sender, EventArgs e)
        {
            divApercu.Visible = true;
            lblDate.Text = DateTime.Now.ToString("d MMMM yyyy à hh\\hmm");
            lblDe.Text = "test";
            lblSujet.Text = tbSujet.Text;
            lblMessage.Text = tbMessage.Text.Replace("\r\n", "<br />");
        }
    }
}