using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class BoiteMessageControle : System.Web.UI.UserControl
    {

        private int _nbMessages;

        public new bool Visible
        {
            get
            {
                return divBoite.Visible;
            }
            set
            {
                divBoite.Visible = value;
            }
        }

        public int NbMessages
        {
            get
            {
                return _nbMessages;
            }
        }

        public void Fill(SqlConnection connexion, int noBoite)
        {
            Liste.Controls.Clear();
            lblLabel.Text = noBoite < 0 ? "À" : "De";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connexion;
            if (noBoite > 0)
            {
                cmd.CommandText = "SELECT M.NoMessage, DM.Lu, M.NoExpediteur 'Personne', M.Sujet, M.DateEnvoi FROM PPDestinatairesMessages DM " +
                                                    "INNER JOIN PPMessages M ON DM.NoMessage = M.NoMessage " +
                                                    "WHERE (DM.Boite = @noBoite) AND (DM.NoDestinataire = @id) " +
                                                    "ORDER BY M.DateEnvoi DESC";
            }
            else if (noBoite < 0)
            {
                cmd.CommandText = "SELECT NoMessage, Sujet, NoExpediteur 'Personne', DateEnvoi FROM PPMessages " +
                                    "WHERE (NoExpediteur = @id) AND (Boite = @noBoite) " +
                                    "ORDER BY DateEnvoi DESC";
            }
            cmd.Parameters.AddWithValue("@id", 10700);//Session["ID"]);
            cmd.Parameters.AddWithValue("@noBoite", noBoite);

            connexion.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            for (_nbMessages = 0; sdr.Read(); _nbMessages++)
            {
                LigneMessage l = (LigneMessage)Page.LoadControl("~/Controles/LigneMessage.ascx");
                Liste.Controls.Add(l);
                l.De = sdr["Personne"].ToString();
                l.Sujet = sdr["Sujet"].ToString();
                l.Date = (DateTime)sdr["DateEnvoi"];
                l.Lu = noBoite < 0 ? true : (Boolean)sdr["Lu"];
                l.NoMessage = (Int64)sdr["NoMessage"];
                Response.Write(l.NoMessage + "<br />");
            }
            if (_nbMessages == 0)
            {
                TableRow tr = new TableRow();
                TableCell td = new TableCell();
                td.ColumnSpan = 4;
                td.Text = "Il n'y a pas de message";
                td.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
                td.Style.Add(HtmlTextWriterStyle.FontStyle, "italic");
                tr.Controls.Add(td);
                Liste.Controls.Add(tr);
            }
            sdr.Close();
            connexion.Close();
        }

        public Int64[] Checked
        {
            get
            {
                List<Int64> selectionne = new List<Int64>();
                foreach (Control c in Liste.Controls)
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

                return selectionne.ToArray();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}