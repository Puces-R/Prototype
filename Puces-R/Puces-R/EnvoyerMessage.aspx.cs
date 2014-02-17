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
    public partial class EnvoyerMessage : System.Web.UI.Page
    {
        SqlConnection connexion = Librairie.Connexion;
        private int[] lstNoDestinataires;
        int noBrouillon = -1;

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
            else if (Request.QueryString["NoMessage"] != null)
            {
                int noMessage = -1;
                if (int.TryParse(Request.QueryString["NoMessage"], out noMessage))
                {
                    SqlCommand cmdTrouverMessage = new SqlCommand("SELECT Sujet, Contenu, FichierJoint FROM PPMessages WHERE NoMessage = @no AND NoExpediteur = @id AND Boite = -2", connexion);
                    SqlCommand cmdDestinataires = new SqlCommand("SELECT NoDestinataire FROM PPDestinatairesMessages WHERE NoMessage = @no", connexion);
                    cmdTrouverMessage.Parameters.AddWithValue("@no", noMessage);
                    cmdTrouverMessage.Parameters.AddWithValue("@id", Session["ID"]);

                    connexion.Open();
                    SqlDataReader sdrMsg = cmdTrouverMessage.ExecuteReader();
                    if (sdrMsg.Read())
                    {
                        noBrouillon = noMessage;
                        if (!IsPostBack)
                        {
                            tbSujet.Text = sdrMsg["Sujet"].ToString();
                            tbMessage.Text = sdrMsg["Contenu"].ToString();
                        }
                    }
                    sdrMsg.Close();

                    if (noBrouillon != -1 && !IsPostBack)
                    {
                        cmdDestinataires.Parameters.AddWithValue("@no", noBrouillon);
                        SqlDataReader sdrDest = cmdDestinataires.ExecuteReader();
                        List<int> tmpDest = new List<int>();
                        for (int i = 0; sdrDest.Read(); i++)
                        {
                            tmpDest.Add(int.Parse(sdrDest["NoDestinataire"].ToString()));
                        }
                        lstNoDestinataires = tmpDest.ToArray();
                        sdrDest.Close();
                    }
                    connexion.Close();
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
                SqlCommand cmdMessage = new SqlCommand();
                cmdMessage.Connection = connexion;

                int noMessage;
                connexion.Open();
                if (noBrouillon < 0)
                {
                    cmdMessage.CommandText = "INSERT INTO PPMessages values(@no, @from, @date, @sujet, @message, @file, -1)\n";
                    SqlCommand cmdNoMessage = new SqlCommand("SELECT ISNULL(MAX(NoMessage), 0) + 1 FROM PPMessages", connexion);
                    noMessage = int.Parse(cmdNoMessage.ExecuteScalar().ToString());
                }
                else
                {
                    cmdMessage.CommandText = "UPDATE PPMessages SET NoExpediteur = @from, DateEnvoi = @date, Sujet = @sujet, Contenu = @message, FichierJoint= @file, Boite = -1 WHERE NoMessage = @no\n";
                    cmdMessage.CommandText += "DELETE FROM PPDestinatairesMessages WHERE NoMessage = @no\n";
                    noMessage = noBrouillon;
                }

                string filename = null;
                if (upload.HasFile)
                {
                    try
                    {
                        filename = Path.GetFileName(upload.FileName);
                        upload.SaveAs(MapPath("MsgDownload/msg" + noMessage + "_" + filename));
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                        filename = null;
                    }
                }

                cmdMessage.Parameters.AddWithValue("@no", noMessage);
                cmdMessage.Parameters.AddWithValue("@from", Session["ID"]);
                cmdMessage.Parameters.AddWithValue("@date", DateTime.Now);
                cmdMessage.Parameters.AddWithValue("@sujet", tbSujet.Text);
                cmdMessage.Parameters.AddWithValue("@message", tbMessage.Text);
                cmdMessage.Parameters.AddWithValue("@file", filename == null ? DBNull.Value : (object)filename);

                for (int i = 0; i < lbDestinataires.Items.Count; i++)
                {
                    cmdMessage.CommandText += string.Format("INSERT INTO PPDestinatairesMessages values(@rcpt{0}, @no, 'false', 1)\n", i);
                    cmdMessage.Parameters.AddWithValue(string.Format("@rcpt{0}", i), lbDestinataires.Items[i].Value);
                }
                cmdMessage.ExecuteNonQuery();
                connexion.Close();

                Response.Redirect(Chemin.UrlRetour);
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

        protected void sauvegarderMessage(object sender, EventArgs e)
        {
            SqlCommand cmdDestinataires = new SqlCommand("INSERT INTO PPDestinatairesMessages values(@no, @rcpt, 'false', -2)", connexion);
            SqlCommand cmdSauvegarder = new SqlCommand();
            cmdSauvegarder.Connection = connexion;

            int noMessage;

            connexion.Open();
            if (noBrouillon < 0)
            {
                cmdSauvegarder.CommandText = "INSERT INTO PPMessages values(@no, @from, @date, @sujet, @message, NULL, -2)\n";
                SqlCommand cmdNoMessage = new SqlCommand("SELECT ISNULL(MAX(NoMessage), 0) + 1 FROM PPMessages", connexion);
                noMessage = int.Parse(cmdNoMessage.ExecuteScalar().ToString());
            }
            else
            {
                cmdSauvegarder.CommandText = "UPDATE PPMessages SET NoExpediteur = @from, DateEnvoi = @date, Sujet = @sujet, Contenu = @message, Boite = -2 WHERE NoMessage = @no\n";
                cmdSauvegarder.CommandText += "DELETE FROM PPDestinatairesMessages WHERE NoMessage = @no\n";
                noMessage = noBrouillon;
            }

            cmdSauvegarder.Parameters.AddWithValue("@no", noMessage);
            cmdSauvegarder.Parameters.AddWithValue("@from", Session["ID"]);
            cmdSauvegarder.Parameters.AddWithValue("@date", DateTime.Now);
            cmdSauvegarder.Parameters.AddWithValue("@sujet", tbSujet.Text);
            cmdSauvegarder.Parameters.AddWithValue("@message", tbMessage.Text);

            for (int i = 0; i < lbDestinataires.Items.Count; i++)
            {
                cmdSauvegarder.CommandText += string.Format("INSERT INTO PPDestinatairesMessages values(@rcpt{0}, @no, 'false', -2)\n", i);
                cmdSauvegarder.Parameters.AddWithValue(string.Format("@rcpt{0}", i), lbDestinataires.Items[i].Value);
            }
            cmdSauvegarder.ExecuteNonQuery();
            connexion.Close();

            Response.Redirect(Chemin.UrlRetour);

        }
    }
}