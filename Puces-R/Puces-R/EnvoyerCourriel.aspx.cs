using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Text;

namespace Puces_R
{
    public partial class EnvoyerCourriel : System.Web.UI.Page
    {
        SqlConnection connexion = Librairie.Connexion;
        private int[] lstNoDestinataires;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Librairie.Autorisation(false, true, true, true);
            }
            ClientScript.GetPostBackEventReference(this, string.Empty);
            string parametres = Request["__EVENTARGUMENT"];
            lstNoDestinataires = null;

            if (Request["__EVENTTARGET"] == "ChoixDestinataires" && parametres != string.Empty)
            {
                lbDestinataires.Items.Clear();
                string[] lstParametres = parametres.Split(",".ToArray());
                lstNoDestinataires = new int[lstParametres.Length];
                for (int i = 0; i < lstNoDestinataires.Length; i++)
                {
                    lstNoDestinataires[i] = int.Parse(lstParametres[i]);
                }
            }
            else if (Session["Sujet"] != null || Session["Message"] != null || Session["ListeDestinataires"] != null || Session["Fixer"] != null)
            {
                if (Session["Sujet"] != null)
                {
                    tbSujet.Text = Session["Sujet"].ToString();
                    Session.Remove("Sujet");
                }

                if (Session["Message"] != null)
                {
                    tbMessage.Text = Session["Message"].ToString();
                    Session.Remove("Message");
                }

                if (Session["ListeDestinataires"] != null)
                {
                    lstNoDestinataires = (int[])Session["ListeDestinataires"];
                    Session.Remove("ListeDestinataires");
                }

                if (Session["Fixer"] != null)
                {
                    btnDestinataire.Visible = !((bool)Session["Fixer"]);
                }
            }

            if (lstNoDestinataires != null && lstNoDestinataires.Length > 0)
            {
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
                SqlCommand cmdAdresse = new SqlCommand();
                SqlCommand cmdEnvoyeur = new SqlCommand("SELECT AdresseEmail, Nom + ', ' + Prenom AS NomComplet FROM PPGestionnaires WHERE NoGestionnaire = @no", connexion);
                cmdAdresse.Connection = connexion;

                string[] param = new string[lbDestinataires.Items.Count];

                for (int i = 0; i < lbDestinataires.Items.Count; i++)
                {
                    param[i] = string.Format("@no{0}", i);
                    cmdAdresse.Parameters.AddWithValue(param[i], lbDestinataires.Items[i].Value);
                }

                cmdAdresse.CommandText = "SELECT AdresseEmail FROM (SELECT NoClient AS No, AdresseEmail FROM PPClients UNION " +
                                   "SELECT NoVendeur AS No, AdresseEmail FROM PPVendeurs UNION " +
                                   "SELECT NoGestionnaire AS No, AdresseEmail FROM PPGestionnaires) AS X " +
                                   string.Format("WHERE (No IN ({0}))", string.Join(", ", param));
                cmdEnvoyeur.Parameters.AddWithValue("@no", Session["ID"]);

                Courriel c = new Courriel();

                c.Sujet = tbSujet.Text;
                c.Message = tbMessage.Text.Replace("\r\n", "<br />");

                connexion.Open();
                SqlDataReader sdr = cmdAdresse.ExecuteReader();
                while (sdr.Read())
                {
                    c.ajouterDestinataire(sdr["AdresseEmail"].ToString());
                }
                sdr.Close();
                sdr = cmdEnvoyeur.ExecuteReader();
                if (sdr.Read())
                {
                    c.changerEnvoyeur(sdr["NomComplet"].ToString() + " (Les Petites Puces)", sdr["AdresseEmail"].ToString());
                }
                sdr.Close();
                connexion.Close();

                c.envoyer();

                Response.Redirect(Chemin.UrlRetour == null ? "accueil_gestionnaire.aspx" : Chemin.UrlRetour);
            }
        }
    }
}