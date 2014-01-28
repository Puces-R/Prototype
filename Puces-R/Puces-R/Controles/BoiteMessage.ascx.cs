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

        public void Fill(SqlCommand cmd, bool sent = false)
        {
            Liste.Controls.Clear();
            lblLabel.Text = sent ? "À" : "De";
            SqlDataReader sdr = cmd.ExecuteReader();
            int cmpt = 0;
            for (cmpt = 0; sdr.Read(); cmpt++)
            {
                LigneMessage l = (LigneMessage)Page.LoadControl("~/Controles/LigneMessage.ascx");
                Liste.Controls.Add(l);
                l.De = sdr["AdresseEmail"].ToString();
                l.Sujet = sdr["Sujet"].ToString();
                l.Date = (DateTime)sdr["DateEnvoi"];
                l.Lu = sent ? true : (Boolean)sdr["Lu"];
                l.NoMessage = (Int64)sdr["NoMessage"];
            }
            if (cmpt == 0)
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