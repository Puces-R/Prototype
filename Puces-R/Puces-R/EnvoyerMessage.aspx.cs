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
        SqlConnection connexion = Librairie.Connexion;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void envoyerMessage(object sender, EventArgs e)
        {
            SqlCommand cmdEnvoyer = new SqlCommand("INSERT INTO PPMessages values(@no, @from, @rcpt, @date, @sujet, @contenu, 0, 0)", connexion);
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

            Response.Redirect("BoiteMessage.aspx");
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