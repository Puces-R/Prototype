using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class BoiteMessage : System.Web.UI.Page
    {
        SqlConnection connexion = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2;");

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlCommand cmdMessages = new SqlCommand("SELECT M.NoMessage, X.AdresseEmail, M.DateEnvoi, M.Sujet, M.Lu FROM PPMessages AS M INNER JOIN " +
                                               "(SELECT NoClient AS No, AdresseEmail FROM PPClients UNION " +
                                                "SELECT NoVendeur AS No, AdresseEmail FROM PPVendeurs UNION " +
                                                "SELECT NoGestionnaire AS No, AdresseEmail FROM PPGestionnaires) AS X " +
                                                "ON M.Envoyeur = X.No " +
                                                "WHERE (M.Recepteur = 10700) ORDER BY M.DateEnvoi DESC", connexion);

            SqlCommand cmdEnvoyes = new SqlCommand("SELECT M.NoMessage, X.AdresseEmail, M.DateEnvoi, M.Sujet FROM PPMessages AS M INNER JOIN " +
                                               "(SELECT NoClient AS No, AdresseEmail FROM PPClients UNION " +
                                                "SELECT NoVendeur AS No, AdresseEmail FROM PPVendeurs UNION " +
                                                "SELECT NoGestionnaire AS No, AdresseEmail FROM PPGestionnaires) AS X " +
                                                "ON M.Recepteur = X.No " +
                                                "WHERE (M.Envoyeur = 10700) ORDER BY M.DateEnvoi DESC", connexion);

            connexion.Open();

            SqlDataReader sdrMessages = cmdMessages.ExecuteReader();
            for (int i = 0; sdrMessages.Read(); i++)
            {
                LigneMessage l = (LigneMessage)Page.LoadControl("~/Controles/LigneMessage.ascx");
                ListeMessage.Controls.Add(l);
                l.De = sdrMessages["AdresseEmail"].ToString();
                l.Sujet = sdrMessages["Sujet"].ToString();
                l.Date = (DateTime)sdrMessages["DateEnvoi"];
                l.Lu = (Boolean)sdrMessages["Lu"];
                l.NoMessage = (Int64)sdrMessages["NoMessage"];
            }
            sdrMessages.Close();

            SqlDataReader sdrEnvoyes = cmdEnvoyes.ExecuteReader();
            for (int i = 0; sdrEnvoyes.Read(); i++)
            {
                LigneMessage l = (LigneMessage)Page.LoadControl("~/Controles/LigneMessage.ascx");
                ListeEnvoye.Controls.Add(l);
                l.De = sdrEnvoyes["AdresseEmail"].ToString();
                l.Sujet = sdrEnvoyes["Sujet"].ToString();
                l.Date = (DateTime)sdrEnvoyes["DateEnvoi"];
                l.NoMessage = (Int64)sdrEnvoyes["NoMessage"];
            }
            sdrEnvoyes.Close();

            connexion.Close();
        }

        protected void clickOption(object sender, MenuEventArgs e)
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
                }

                switch (e.Item.Value)
                {
                    case "Lu":
                        cmd.CommandText = string.Format("UPDATE PPMessages SET Lu = 'True' WHERE NoMessage IN ({0})", string.Join(", ", param));
                        break;
                    case "Non-lu":
                        cmd.CommandText = string.Format("UPDATE PPMessages SET Lu = 'False' WHERE NoMessage IN ({0})", string.Join(", ", param));
                        break;
                }

                connexion.Open();
                cmd.ExecuteNonQuery();
                connexion.Close();
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void voirMessage(object sender, MenuEventArgs e)
        {
            divMessages.Visible = divEnvoyes.Visible = false;
            switch (e.Item.Value)
            {
                case "Sent":
                    divEnvoyes.Visible = true;
                    break;
                case "Box":
                    divMessages.Visible = true;
                    break;
            }
        }
    }
}