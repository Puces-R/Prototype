using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

namespace Puces_R
{
    public partial class InscriptionClient : System.Web.UI.Page
    {
        SqlConnection connexion = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2;");

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void inscription(object sender, EventArgs e)
        {
            
            Page.Validate();
            
            if (Page.IsValid)
            {
                connexion.Open();
                SqlCommand cmdNoClient = new SqlCommand("SELECT ISNULL(MAX(NoClient), 9900) + 100 FROM PPClients", connexion);
                int noClient = int.Parse(cmdNoClient.ExecuteScalar().ToString());
                SqlCommand cmdInsertion = new SqlCommand("INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse, DateCreation) values (@no, @adresse, @mdp, @date)", connexion);
                cmdInsertion.Parameters.AddWithValue("@no", noClient);
                cmdInsertion.Parameters.AddWithValue("@adresse", ctlIdentifiants.Adresse);
                cmdInsertion.Parameters.AddWithValue("@mdp", ctlIdentifiants.MotDePasse);
                cmdInsertion.Parameters.AddWithValue("@date", DateTime.Now.Date);

                cmdInsertion.ExecuteNonQuery();

                connexion.Close();
                // Vérifier les courriels : envoi irrégulier
                SmtpClient client = new SmtpClient();
                MailMessage courriel = new MailMessage();

                courriel.From = new MailAddress("no-reply@lespetitespuces.com", "Les Petites Puces");
                courriel.To.Add(ctlIdentifiants.Adresse);
                courriel.Subject = "Inscription aux Petites Puces";
                courriel.IsBodyHtml = true;
                courriel.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;


                courriel.Body = "NoClient = " + noClient + "<br />" +
                                "Adresse courriel = " + ctlIdentifiants.Adresse + "<br />" +
                                "Mot de passe = " + ctlIdentifiants.MotDePasse;
                courriel.BodyEncoding = System.Text.Encoding.UTF8;

                client.Host = "smtp.gmail.com";
                client.Port = 465;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("raikou4@gmail.com", "p0k3m0n2515");


                try
                {
                    client.Send(courriel);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }
    }
}