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

        private void Update()
        {
            if (menuAction.FindItem("Restore") != null)
            {
                menuAction.FindItem("Restore").Text = "Supprimer";
                menuAction.FindItem("Restore").Value = "Delete";
            }

            if (menuAction.FindItem("Unarchive") != null)
            {
                menuAction.FindItem("Unarchive").Text = "Archiver";
                menuAction.FindItem("Unarchive").Value = "Archive";
            }
            if (menuAction.FindItem(/*CRUSH ! KILL ! */ "Destroy" /* ! SWAG !*/) != null)
            {
                menuAction.Items.Remove(menuAction.FindItem("Destroy"));
            }

            int i;
            if (!int.TryParse(Request.QueryString["Box"], out i) || i < -2 || i > 3 || i == 0)
            {
                i = 1;
            }
            ListeMessage.Fill(connexion, i);
            switch (i)
            {
                case -1:
                case -2:
                    menuAction.Visible = false;
                    break;
                case 2:
                    menuAction.FindItem("Archive").Text = "Désarchiver";
                    menuAction.FindItem("Archive").Value = "Unarchive";
                    break;
                case 3:
                    menuAction.FindItem("Delete").Text = "Restaurer";
                    menuAction.FindItem("Delete").Value = "Restore";
                    menuAction.Items.Add(new MenuItem("Supprimer complètement", "Destroy"));
                    break;
            }

            if (!IsPostBack)
            {
                ddlBoite.SelectedValue = i.ToString();
            }
        }

        protected void clickOption(object sender, MenuEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connexion;
            Int64[] selectionne = ListeMessage.Checked;


            if (selectionne.Length > 0)
            {
                string[] param = new string[selectionne.Length];

                for (int i = 0; i < selectionne.Length; i++)
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
                    case "Destroy":
                        cmd.CommandText = string.Format("UPDATE PPDestinatairesMessages SET Boite = 0 WHERE NoMessage IN ({0}) AND NoDestinataire = @id", string.Join(", ", param));
                        break;
                    case "Archive":
                        cmd.CommandText = string.Format("UPDATE PPDestinatairesMessages SET Boite = 2 WHERE NoMessage IN ({0}) AND NoDestinataire = @id", string.Join(", ", param));
                        break;
                    case "Restore":
                    case "Unarchive":
                        cmd.CommandText = string.Format("UPDATE PPDestinatairesMessages SET Boite = 1 WHERE NoMessage IN ({0}) AND NoDestinataire = @id", string.Join(", ", param));
                        break;
                }

                cmd.Parameters.AddWithValue("@id", 10000);

                connexion.Open();
                cmd.ExecuteNonQuery();
                connexion.Close();
                Response.Redirect(Request.RawUrl);
            }
        }
        /*
        private void Update()
        {
            if (menuAction.FindItem("Restore") != null)
            {
                menuAction.FindItem("Restore").Text = "Supprimer";
                menuAction.FindItem("Restore").Value = "Delete";
            }

            if (menuAction.FindItem("Unarchive") != null)
            {
                menuAction.FindItem("Unarchive").Text = "Archiver";
                menuAction.FindItem("Unarchive").Value = "Archive";
            }
            menuAction.Visible = true;

            SqlCommand cmd = null;
            string box = "";

            if (Request.QueryString["Box"] != null)
            {
                box = Request.QueryString["Box"];

                switch (box)
                {
                    case "Sent":
                        cmd = new SqlCommand("SELECT M.NoMessage, X.AdresseEmail, M.DateEnvoi, M.Sujet FROM PPMessages AS M INNER JOIN " +
                                               "(SELECT NoClient AS No, AdresseEmail FROM PPClients UNION " +
                                                "SELECT NoVendeur AS No, AdresseEmail FROM PPVendeurs UNION " +
                                                "SELECT NoGestionnaire AS No, AdresseEmail FROM PPGestionnaires) AS X " +
                                                "ON M.Recepteur = X.No " +
                                                "WHERE (M.Envoyeur = 10700) ORDER BY M.DateEnvoi DESC", connexion);
                        menuAction.Visible = false;
                        break;
                    case "Deleted":
                        cmd = new SqlCommand("SELECT M.NoMessage, X.AdresseEmail, M.DateEnvoi, M.Sujet, M.Lu FROM PPMessages AS M INNER JOIN " +
                                               "(SELECT NoClient AS No, AdresseEmail FROM PPClients UNION " +
                                                "SELECT NoVendeur AS No, AdresseEmail FROM PPVendeurs UNION " +
                                                "SELECT NoGestionnaire AS No, AdresseEmail FROM PPGestionnaires) AS X " +
                                                "ON M.Envoyeur = X.No " +
                                                "WHERE (M.Recepteur = 10700) AND M.Boite = -1 ORDER BY M.DateEnvoi DESC", connexion);
                        menuAction.FindItem("Delete").Text = "Restaurer";
                        menuAction.FindItem("Delete").Value = "Restore";
                        break;
                    case "Archived":
                        cmd = new SqlCommand("SELECT M.NoMessage, X.AdresseEmail, M.DateEnvoi, M.Sujet, M.Lu FROM PPMessages AS M INNER JOIN " +
                                               "(SELECT NoClient AS No, AdresseEmail FROM PPClients UNION " +
                                                "SELECT NoVendeur AS No, AdresseEmail FROM PPVendeurs UNION " +
                                                "SELECT NoGestionnaire AS No, AdresseEmail FROM PPGestionnaires) AS X " +
                                                "ON M.Envoyeur = X.No " +
                                                "WHERE (M.Recepteur = 10700) AND M.Boite = 1 ORDER BY M.DateEnvoi DESC", connexion);
                        menuAction.FindItem("Archive").Text = "Désarchiver";
                        menuAction.FindItem("Archive").Value = "Unarchive";
                        break;
                    case "Draft":
                        cmd = new SqlCommand("SELECT * FROM PPMessages WHERE NoMessage = -1000", connexion);
                        break;
                    default:
                        cmd = new SqlCommand("SELECT M.NoMessage, X.AdresseEmail, M.DateEnvoi, M.Sujet, M.Lu FROM PPMessages AS M INNER JOIN " +
                                               "(SELECT NoClient AS No, AdresseEmail FROM PPClients UNION " +
                                                "SELECT NoVendeur AS No, AdresseEmail FROM PPVendeurs UNION " +
                                                "SELECT NoGestionnaire AS No, AdresseEmail FROM PPGestionnaires) AS X " +
                                                "ON M.Envoyeur = X.No " +
                                                "WHERE (M.Recepteur = 10700) AND M.Boite = 0 ORDER BY M.DateEnvoi DESC", connexion);
                        break;
                }
            }
            else
            {
                cmd = new SqlCommand("SELECT M.NoMessage, X.AdresseEmail, M.DateEnvoi, M.Sujet, M.Lu FROM PPMessages AS M INNER JOIN " +
                                               "(SELECT NoClient AS No, AdresseEmail FROM PPClients UNION " +
                                                "SELECT NoVendeur AS No, AdresseEmail FROM PPVendeurs UNION " +
                                                "SELECT NoGestionnaire AS No, AdresseEmail FROM PPGestionnaires) AS X " +
                                                "ON M.Envoyeur = X.No " +
                                                "WHERE (M.Recepteur = 10700) AND M.Boite = 0 ORDER BY M.DateEnvoi DESC", connexion);
            }


            connexion.Open();
            ListeMessage.Fill(cmd, box == "Sent");
            connexion.Close();
        }
        */

        protected void changeBoite(object sender, EventArgs e)
        {
            Response.Redirect("BoiteMessage.aspx?Box=" + ddlBoite.SelectedValue, true);
        }
    }
}