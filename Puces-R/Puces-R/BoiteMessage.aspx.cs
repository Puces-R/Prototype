using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

namespace Puces_R
{

    public partial class BoiteMessage : System.Web.UI.Page
    {
        SqlConnection connexion = Librairie.Connexion;
        int noExpediteur = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Librairie.Autorisation(false, true, true, true);
            }
            Update();
        }

        private void makeMenu(int boite)
        {
            menuAction.Items.Clear();
            menuAction.Items.Add(new MenuItem("Nouveau message", "New"));
            if (boite > 0)
            {
                menuAction.Items.Add(new MenuItem("Marquer comme lu", "Read"));
                menuAction.Items.Add(new MenuItem("Marquer comme non-lu", "Unread"));
                menuAction.Items.Add(new MenuItem(boite == 2 ? "Désarchiver" : "Archiver", boite == 2 ? "Unarchive" : "Archive"));
                menuAction.Items.Add(new MenuItem(boite == 3 ? "Restaurer" : "Supprimer", boite == 3 ? "Restore" : "Delete"));
                if (boite == 3)
                {
                    menuAction.Items.Add(new MenuItem("Supprimer définitivement", "DestroyDestinataire"));
                }
            }
            else if (boite < 0)
            {
                menuAction.Items.Add(new MenuItem("Supprimer définitivement", "DestroyExpediteur"));
            }
        }

        private void Fill(int boite, int tri, char ordre)
        {
            ListeMessage.Controls.Clear();
            linkDe.Text = boite < 0 ? "À" : "De";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connexion;
            string ordreCmd = "";

            switch (tri)
            {
                case 0:
                    ordreCmd += "Personne";
                    if (!IsPostBack)
                    {
                        linkDe.Text += "&nbsp;" + (ordre == 'D' ? "&#x25B2;" : "&#x25BC;");
                    }
                    break;
                case 1:
                    ordreCmd += "Sujet";
                    if (!IsPostBack)
                    {
                        linkSujet.Text += "&nbsp;" + (ordre == 'D' ? "&#x25B2;" : "&#x25BC;");
                    }
                    break;
                default:
                    ordreCmd += "DateEnvoi";
                    if (!IsPostBack)
                    {
                        linkDate.Text += "&nbsp;" + (ordre == 'A' ? "&#x25BC;" : "&#x25B2;");
                    }
                    break;
            }

            switch (ordre)
            {
                case 'A':
                    ordreCmd += " ASC";
                    break;
                case 'D':
                    ordreCmd += " DESC";
                    break;
            }

            if (boite > 0)
            {
                cmd.CommandText = "SELECT M.NoMessage, DM.Lu, X.Texte AS 'Personne', M.Sujet, M.DateEnvoi FROM PPDestinatairesMessages AS DM INNER JOIN " +
                                  "PPMessages AS M ON DM.NoMessage = M.NoMessage INNER JOIN  " +
                                        "(SELECT NoClient AS No, ISNULL(Nom + ', ' + RTRIM(Prenom) + ' <' + AdresseEmail + '>', AdresseEmail) + ' (Client)' AS Texte FROM PPClients UNION " +
                                         "SELECT NoVendeur AS No, RTRIM(NomAffaires) + ' <' + AdresseEmail + '> (Vendeur)' AS Texte FROM PPVendeurs UNION " +
                                         "SELECT NoGestionnaire AS No, RTRIM(Nom) + ', ' + RTRIM(Prenom) + ' <' + AdresseEmail + '> (Gestionnaire)' AS Texte FROM PPGestionnaires) AS X ON X.No = M.NoExpediteur " +
                                   "WHERE  (DM.Boite = @noBoite) AND (DM.NoDestinataire = @id) " +
                                   "ORDER BY " + ordreCmd;
            }
            else if (boite < 0)
            {
                cmd.CommandText = "SELECT DM.NoMessage, COUNT(*) - 1 AS NbDestinataires, M.Sujet, M.DateEnvoi, " +
                                    "(SELECT Texte FROM " +
                                        "(SELECT NoClient AS No, ISNULL(Nom + ', ' + RTRIM(Prenom) + ' <' + AdresseEmail + '>', AdresseEmail) + ' (Client)' AS Texte FROM PPClients UNION " +
                                         "SELECT NoVendeur AS No, RTRIM(NomAffaires) + ' <' + AdresseEmail + '> (Vendeur)' AS Texte FROM PPVendeurs UNION " +
                                         "SELECT NoGestionnaire AS No, RTRIM(Nom) + ', ' + RTRIM(Prenom) + ' <' + AdresseEmail + '> (Gestionnaire)' AS Texte FROM PPGestionnaires) AS X " +
                                     "WHERE (No = MIN(DM.NoDestinataire))) AS Personne " +
                                  "FROM PPDestinatairesMessages AS DM INNER JOIN PPMessages AS M ON DM.NoMessage = M.NoMessage " +
                                    "WHERE (M.NoExpediteur = @id) AND (M.Boite = @noBoite) " +
                                    "GROUP BY DM.NoMessage, M.Sujet, M.DateEnvoi " +
                                    "ORDER BY " + ordreCmd;
            }
            cmd.Parameters.AddWithValue("@id", Session["ID"]);
            cmd.Parameters.AddWithValue("@noBoite", boite);

            connexion.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            int nbMessages = 0;
            for (nbMessages = 0; sdr.Read(); nbMessages++)
            {
                int nbDestinataires = (boite > 0) ? -1 : int.Parse(sdr["NbDestinataires"].ToString());
                LigneMessage l = (LigneMessage)Page.LoadControl("~/Controles/LigneMessage.ascx");
                ListeMessage.Controls.Add(l);
                l.De = sdr["Personne"].ToString() + (nbDestinataires <= 0 ? "" : " et " + nbDestinataires + " autre" + (nbDestinataires < 2 ? "" : "s"));
                l.Sujet = sdr["Sujet"].ToString();
                l.Date = (DateTime)sdr["DateEnvoi"];
                l.Lu = boite < 0 ? true : (Boolean)sdr["Lu"];
                l.NoMessage = (Int64)sdr["NoMessage"];
                l.Brouillon = boite == -2;
            }
            if (nbMessages == 0)
            {
                TableRow tr = new TableRow();
                TableCell td = new TableCell();
                td.ColumnSpan = 4;
                td.Text = "Il n'y a pas de message";
                td.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
                td.Style.Add(HtmlTextWriterStyle.FontStyle, "italic");
                tr.Controls.Add(td);
                ListeMessage.Controls.Add(tr);
            }
            sdr.Close();
            connexion.Close();
        }

        private void Update()
        {
            int boite;
            int tri;
            char ordre;
            Int64 noMessage;
            divMessage.Visible = false;
            bool lu = false;

            if (Session["Lu"] != null)
            {
                lu = true;
                Session.Remove("Lu");
            }

            if (!IsPostBack && Request.QueryString["No"] != null && Int64.TryParse(Request.QueryString["No"], out noMessage))
            {
                SqlCommand cmdEstDestinataire = new SqlCommand("SELECT CASE WHEN COUNT(*) > 0 THEN 'true' ELSE 'false' END FROM PPDestinatairesMessages WHERE (NoMessage = @noMsg) AND (NoDestinataire = @noRcpt) AND (Boite > 0)", connexion);
                SqlCommand cmdEstExpediteur = new SqlCommand("SELECT CASE WHEN COUNT(*) = 1 THEN 'true' ELSE 'false' END FROM PPMessages WHERE (NoMessage = @noMsg) AND (NoExpediteur = @id) AND (Boite < 0)", connexion);

                SqlCommand cmdMessage = new SqlCommand("SELECT M.NoExpediteur, X.Texte, M.DateEnvoi, M.Sujet, M.Contenu, M.FichierJoint FROM PPMessages M " +
                    "INNER JOIN PPDestinatairesMessages DM ON M.NoMessage = DM.NoMessage " +
                    "INNER JOIN (SELECT NoClient AS No, ISNULL(Nom + ', ' + RTRIM(Prenom) + ' <' + AdresseEmail + '>', AdresseEmail) + ' (Client)' AS Texte FROM PPClients UNION " +
                                "SELECT NoVendeur AS No, RTRIM(NomAffaires) + ' <' + AdresseEmail + '> (Vendeur)' AS Texte FROM PPVendeurs UNION " +
                                "SELECT NoGestionnaire AS No, RTRIM(Nom) + ', ' + RTRIM(Prenom) + ' <' + AdresseEmail + '> (Gestionnaire)' AS Texte FROM PPGestionnaires) AS X " +
                    "ON X.No = M.NoExpediteur WHERE (M.NoMessage = @noMsg)", connexion);
                SqlCommand cmdDestinataires = new SqlCommand("SELECT Texte FROM " +
                         "(SELECT NoClient AS No, ISNULL(Nom + ', ' + RTRIM(Prenom) + ' (' + AdresseEmail + ')', AdresseEmail) + ' [Client]' AS Texte FROM PPClients UNION " +
                          "SELECT NoVendeur AS No, RTRIM(NomAffaires) + ' (' + AdresseEmail + ') [Vendeur]' AS Texte FROM PPVendeurs UNION " +
                          "SELECT NoGestionnaire AS No, RTRIM(Nom) + ', ' + RTRIM(Prenom) + ' (' + AdresseEmail + ') [Gestionnaire]' AS Texte FROM PPGestionnaires) AS X " +
                          "WHERE (No IN (SELECT NoDestinataire FROM PPDestinatairesMessages WHERE (NoMessage = @noMsg)))", connexion);

                    SqlCommand cmdLu = new SqlCommand("UPDATE PPDestinatairesMessages SET Lu = 1 WHERE NoMessage = @noMsg AND NoDestinataire = @noRcpt", connexion);

                cmdEstDestinataire.Parameters.AddWithValue("@noMsg", noMessage);
                cmdEstDestinataire.Parameters.AddWithValue("@noRcpt", Session["ID"].ToString());

                cmdEstExpediteur.Parameters.AddWithValue("@noMsg", noMessage);
                cmdEstExpediteur.Parameters.AddWithValue("@id", Session["ID"].ToString());

                cmdMessage.Parameters.AddWithValue("@noMsg", noMessage);
                cmdMessage.Parameters.AddWithValue("@id", Session["ID"].ToString());

                cmdDestinataires.Parameters.AddWithValue("@noMsg", noMessage);

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
                    if (lu)
                    {
                        cmdLu.ExecuteNonQuery();
                    }

                    SqlDataReader sdr = cmdMessage.ExecuteReader();
                    bool ok;
                    if (ok = sdr.Read())
                    {
                        lblDate.Text = ((DateTime)sdr["DateEnvoi"]).ToString("d MMMM yyyy à h\\hmm");
                        lblDe.Text = sdr["Texte"].ToString();
                        lblMessage.Text = sdr["Contenu"].ToString().Replace("\r\n", "<br />");
                        lblSujet.Text = sdr["Sujet"].ToString();
                        noExpediteur = int.Parse(sdr["NoExpediteur"].ToString());
                        if (sdr["FichierJoint"] != DBNull.Value)
                        {
                            trPiece.Visible = true;
                            btnDownload.Text = sdr["FichierJoint"].ToString();
                        }
                    }
                    divMessage.Visible = ok;
                    sdr.Close();
                    if (ok)
                    {
                        sdr = cmdDestinataires.ExecuteReader();
                        int nbDest = 0;
                        for (nbDest = 0; sdr.Read(); nbDest++)
                        {
                            lbDestinataires.Items.Add(sdr["Texte"].ToString());
                        }
                        lbDestinataires.Rows = nbDest > 5 ? 5 : nbDest;
                        sdr.Close();
                    }
                }

                connexion.Close();
            }

            if (!int.TryParse(Request.QueryString["Boite"], out boite) || boite < -2 || boite > 3 || boite == 0)
            {
                boite = 1;
            }

            if (!int.TryParse(Request.QueryString["Tri"], out tri) || tri < 0 || tri > 2)
            {
                tri = 2;
            }

            if (!char.TryParse(Request.QueryString["Ordre"], out ordre) || (ordre != 'A' && ordre != 'D'))
            {
                ordre = tri == 2 ? 'D' : 'A';
            }
            makeMenu(boite);
            Fill(boite, tri, ordre);

            if (!IsPostBack)
            {
                ddlBoite.SelectedValue = boite.ToString();
            }
        }

        protected void clickOption(object sender, MenuEventArgs e)
        {
            ParametresGet paramGet = new ParametresGet(Request.RawUrl);
            string boite = paramGet.Get("Boite");
            string boiteTxt = "Retour à la boîte principale";
            switch (boite)
            {
                case "2":
                    boiteTxt = "Retour aux messages archivés";
                    break;
                case "3":
                    boiteTxt = "Retour à la corbeille";
                    break;
                case "-1":
                    boiteTxt = "Retour aux messages envoyés";
                    break;
                case "-2":
                    boiteTxt = "Retour aux brouillons";
                    break;
            }

            if (e.Item.Value == "New")
            {
                Response.Redirect(Chemin.Ajouter("EnvoyerMessage.aspx", boiteTxt), true);
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connexion;
                List<Int64> selectionne = new List<Int64>();
                foreach (Control c in ListeMessage.Controls)
                {
                    if (c is LigneMessage)
                    {
                        LigneMessage l = (LigneMessage)c;
                        if (l.Checked)
                        {
                            selectionne.Add(l.NoMessage);
                        }
                    }
                }


                if (selectionne.Count > 0)
                {
                    string[] param = new string[selectionne.Count];

                    for (int i = 0; i < selectionne.Count; i++)
                    {
                        param[i] = string.Format("@no{0}", i);
                        cmd.Parameters.AddWithValue(param[i], selectionne[i]);
                        Response.Write(selectionne[i] + "<br />");
                    }

                    switch (e.Item.Value)
                    {
                        case "Read":
                            cmd.CommandText = string.Format("UPDATE PPDestinatairesMessages SET Lu = 'True' WHERE NoMessage IN ({0}) AND NoDestinataire = @id", string.Join(", ", param));
                            break;
                        case "Unread":
                            cmd.CommandText = string.Format("UPDATE PPDestinatairesMessages SET Lu = 'False' WHERE NoMessage IN ({0}) AND NoDestinataire = @id", string.Join(", ", param));
                            break;
                        case "Delete":
                            cmd.CommandText = string.Format("UPDATE PPDestinatairesMessages SET Boite = 3 WHERE NoMessage IN ({0}) AND NoDestinataire = @id", string.Join(", ", param));
                            break;
                        case "DestroyDestinataire":
                            cmd.CommandText = string.Format("UPDATE PPDestinatairesMessages SET Boite = 0 WHERE NoMessage IN ({0}) AND NoDestinataire = @id", string.Join(", ", param));
                            break;
                        case "DestroyExpediteur":
                            cmd.CommandText = string.Format("UPDATE PPMessages SET Boite = 0 WHERE NoMessage IN ({0}) AND NoExpediteur = @id", string.Join(", ", param)); // Le id est une vérification en plus
                            break;
                        case "Archive":
                            cmd.CommandText = string.Format("UPDATE PPDestinatairesMessages SET Boite = 2 WHERE NoMessage IN ({0}) AND NoDestinataire = @id", string.Join(", ", param));
                            break;
                        case "Restore":
                        case "Unarchive":
                            cmd.CommandText = string.Format("UPDATE PPDestinatairesMessages SET Boite = 1 WHERE NoMessage IN ({0}) AND NoDestinataire = @id", string.Join(", ", param));
                            break;
                    }
                    cmd.Parameters.AddWithValue("@id", Session["ID"]);

                    connexion.Open();
                    cmd.ExecuteNonQuery();
                    connexion.Close();
                    Response.Redirect(Request.RawUrl);
                }
            }
        }

        protected void changeBoite(object sender, EventArgs e)
        {
            ParametresGet param = new ParametresGet(Request.RawUrl, new string[] { "No", "Boite", "Tri", "Ordre" });
            string boite = param.Get("Boite");
            param.Set("Boite", ddlBoite.SelectedValue);
            string boiteTxt = "Retour à la boîte de réception";
            switch (boite)
            {
                case "2":
                    boiteTxt = "Retour aux messages archivés";
                    break;
                case "3":
                    boiteTxt = "Retour à la corbeille";
                    break;
                case "-1":
                    boiteTxt = "Retour aux messages envoyés";
                    break;
                case "-2":
                    boiteTxt = "Retour aux brouillons";
                    break;
            }
            Response.Redirect(Chemin.Ajouter("BoiteMessage.aspx" + param.Parametres, boiteTxt), true);
        }

        protected void ordre(object sender, EventArgs e)
        {
            ParametresGet param = new ParametresGet(Request.RawUrl, new string[] { "No", "Boite", "Tri", "Ordre", "texteretour", "cheminretour" });
            int noTri = -1;

            if (sender == linkDe)
            {
                noTri = 0;
            }
            else if (sender == linkSujet)
            {
                noTri = 1;
            }
            else if (sender == linkDate)
            {
                noTri = 2;
            }

            if (Request.QueryString["Tri"] != null && Request.QueryString["Tri"] == noTri.ToString())
            {
                if (Request.QueryString["Ordre"] != null)
                {
                    if (Request.QueryString["Ordre"] == "A")
                    {
                        param.Set("Ordre", "D");
                    }
                    else if (Request.QueryString["Ordre"] == "D")
                    {
                        param.Set("Ordre", "A");
                    }
                    else if (noTri == 2)
                    {
                        param.Set("Ordre", "A");
                    }
                    else
                    {
                        param.Set("Ordre", "D");
                    }
                }
                else if (noTri == 2)
                {
                    param.Set("Ordre", "A");
                }
                else
                {
                    param.Set("Ordre", "D");
                }
            }

            if (noTri > -1)
            {
                param.Set("Tri", noTri.ToString());
            }

            Response.Redirect("BoiteMessage.aspx" + param.Parametres);
        }

        protected void download(object sender, EventArgs e)
        {
            Session["NoDownload"] = int.Parse(Request.QueryString["No"]);
            Response.Redirect("TelechargementMessage.ashx");
        }

        protected void repondre(object sender, EventArgs e)
        {

            Librairie.Messagerie(new int[] { noExpediteur }, "RE : " + lblSujet.Text, null, false, "Retour au message");
        }


    }
}