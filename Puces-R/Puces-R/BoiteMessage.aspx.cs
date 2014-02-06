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

        protected void Page_Load(object sender, EventArgs e)
        {
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
                    break;
                case 1:
                    ordreCmd += "Sujet";
                    break;
                case 2:
                    ordreCmd += "DateEnvoi";
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
                cmd.CommandText = "SELECT        M.NoMessage, DM.Lu, X.Texte AS 'Personne', M.Sujet, M.DateEnvoi FROM PPDestinatairesMessages AS DM INNER JOIN " +
                                  "PPMessages AS M ON DM.NoMessage = M.NoMessage INNER JOIN  " +
                                  "(SELECT NoClient AS No, RTRIM(ISNULL(Nom + ', ' + Prenom, 'Anonyme')) + ' (' + CAST(NoClient AS varchar(10)) + ')' AS Texte FROM PPClients UNION " +
                                   "SELECT NoVendeur AS 'No', RTRIM(NomAffaires) + ' (' + CAST(NoVendeur AS varchar(10)) + ')' AS 'Texte' FROM PPVendeurs UNION " +
                                   "SELECT NoGestionnaire AS 'No', RTRIM(ISNULL(Nom + ', ' + Prenom, 'Anonyme')) + ' (' + CAST(NoGestionnaire AS varchar(10)) + ')' AS 'Texte' FROM PPGestionnaires) AS X ON X.No = M.NoExpediteur " +
                                   "WHERE  (DM.Boite = @noBoite) AND (DM.NoDestinataire = @id) " +
                                   "ORDER BY " + ordreCmd;
            }
            else if (boite < 0)
            {
                cmd.CommandText = "SELECT M.NoMessage, M.Sujet, M.NoExpediteur 'Personne', M.DateEnvoi FROM PPMessages M " +
                                    "WHERE (M.NoExpediteur = @id) AND (M.Boite = @noBoite) " +
                                    "ORDER BY " + ordreCmd;
            }
            cmd.Parameters.AddWithValue("@id", Session["ID"]);
            cmd.Parameters.AddWithValue("@noBoite", boite);

            connexion.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            int nbMessages = 0;
            for (nbMessages = 0; sdr.Read(); nbMessages++)
            {
                LigneMessage l = (LigneMessage)Page.LoadControl("~/Controles/LigneMessage.ascx");
                ListeMessage.Controls.Add(l);
                l.De = sdr["Personne"].ToString();
                l.Sujet = sdr["Sujet"].ToString();
                l.Date = (DateTime)sdr["DateEnvoi"];
                l.Lu = boite < 0 ? true : (Boolean)sdr["Lu"];
                l.NoMessage = (Int64)sdr["NoMessage"];
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
            if (e.Item.Value == "New")
            {
                Response.Redirect("EnvoyerMessage.aspx", true);
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
            ParametresGet param = new ParametresGet(Request.RawUrl, new string[] { "Boite", "Tri", "Ordre" });
            param.Set("Boite", ddlBoite.SelectedValue);
            Response.Redirect("BoiteMessage.aspx" + param.Parametres, true);
        }

        protected void ordre(object sender, EventArgs e)
        {
            ParametresGet param = new ParametresGet(Request.RawUrl, new string[] { "Boite", "Tri", "Ordre" });
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
    }
}