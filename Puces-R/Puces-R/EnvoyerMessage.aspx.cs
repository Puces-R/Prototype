using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class EnvoyerMessage : System.Web.UI.Page
    {
        SqlConnection connexion = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2;");

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void envoyerMessage(object sender, EventArgs e)
        {
            SqlCommand cmdEnvoyer = new SqlCommand("INSERT INTO PPMessages values(@from, @rcpt, @date, @sujet, @contenu, 0, @no)", connexion);
            SqlCommand cmdNoMessage = new SqlCommand("SELECT ISNULL(MAX(NoMessage), 0) + 1 FROM PPMessages", connexion);

            connexion.Open();

            Int64 noMessage = Convert.ToInt64(cmdNoMessage.ExecuteScalar().ToString());
            cmdEnvoyer.Parameters.AddWithValue("@rcpt", 10700);
            cmdEnvoyer.Parameters.AddWithValue("@from", tbRecepteur.Text);
            cmdEnvoyer.Parameters.AddWithValue("@date", DateTime.Now);
            cmdEnvoyer.Parameters.AddWithValue("@sujet", tbSujet.Text);
            cmdEnvoyer.Parameters.AddWithValue("@contenu", tbMessage.Text);
            cmdEnvoyer.Parameters.AddWithValue("@no", noMessage);

            cmdEnvoyer.ExecuteNonQuery();

            connexion.Close();
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