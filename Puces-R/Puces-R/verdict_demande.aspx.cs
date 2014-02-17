using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace Puces_R
{
    public partial class verdict_demande : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
        int no_vendeur;

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.Titre = "Verdict de la demande";

            if (Session["err_msg"] != null)
                if (Session["err_msg"].ToString() != "")
                {
                    Response.Write(Session["err_msg"]);
                    Session["err_msg"] = "";
                }

            if (!IsPostBack)
            {
                Librairie.Autorisation(false, false, false, true);
                if (Session["acceptation_vendeur"] != null)
                {
                    if (Session["acceptation_vendeur"].ToString() != "")
                    {
                        no_vendeur = Convert.ToInt32(Session["acceptation_vendeur"].ToString());
                        mv_verdict.SetActiveView(view_acceptation);
                        Session["acceptation_vendeur"] = null;
                    }
                }
                else
                {
                    if (Session["refus_vendeur"] != null)
                    {
                        if (Session["refus_vendeur"].ToString() != "")
                        {
                            no_vendeur = Convert.ToInt32(Session["refus_vendeur"].ToString());
                            mv_verdict.SetActiveView(view_refus);
                            Session["refus_vendeur"] = null;
                        }
                    }
                    else Response.Redirect("Default.aspx");
                }
            }

            myConnection.Open();
            SqlCommand charger = new SqlCommand("SELECT * FROM PPVendeurs WHERE NoVendeur = " + no_vendeur, myConnection);

            SqlDataReader results = charger.ExecuteReader();

            if (results.Read())
            {
                titre_demande.Text = results["NomAffaires"].ToString() + ", par " + results["Prenom"].ToString() + " " + results["Nom"].ToString();
                addr_demande.Text = results["Rue"].ToString() + ", " + results["Ville"].ToString() + ", " + results["Pays"].ToString();
                tels_demande.Text = results["Tel1"].ToString();
                courriel_demande.Text = results["AdresseEmail"].ToString();
                charge_max_demande.Text = results["MaxLivraison"].ToString() + "lb";
                livraison_gratuite.Text = results["LivraisonGratuite"].ToString();
                date_demande.Text = results["DateCreation"].ToString();
                btn_accepter.CommandArgument = results["NoVendeur"].ToString();
                btn_refuser.CommandArgument = results["NoVendeur"].ToString();
                cont_mail_acceptation.Text = "Bonjour " + results["Prenom"].ToString() + " " + results["Nom"].ToString() + "\nFélicitations! Votre inscription sur LesPetitesPuces.com a été acceptée.";
                cont_mail_refus.Text = "Bonjour " + results["Prenom"].ToString() + " " + results["Nom"].ToString() + "\nVotre inscription sur LesPetitesPuces.com n'a pas été acceptée.";
            }

            myConnection.Close();
        }
         

        protected void refus_demande(object sender, CommandEventArgs e)
        {
            MailMessage msg = new MailMessage();

            myConnection.Open();
            SqlCommand commande_refuser_demande = new SqlCommand("DELETE FROM PPVendeurs WHERE NoVendeur = " + e.CommandArgument.ToString(), myConnection);
            commande_refuser_demande.ExecuteNonQuery();
            //Response.Write(commande_accepter_demande.CommandText);

            msg.Body = cont_mail_refus.Text;

            try
            {
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);

                msg.Subject = "Refus de la demande d'abonnement au site LesPetiesPuces.com";
                msg.From = new MailAddress("e.clubdegolf@gmail.com", "Gestionnaire de LesPetiesPuces.com");
                msg.To.Add(new MailAddress(courriel_demande.Text));
                SmtpServer.Credentials = new NetworkCredential("e.clubdegolf@gmail.com", "TouraTou");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(msg);

            }
            catch (Exception k)
            {
                Session["err_msg"] = "Echec de l'envoi du mail de confirmation: " + k.ToString();
            }

            Session["msg"] = "Le vendeur " + titre_demande.Text + " a bien été refusé.";
            Response.Redirect("gerer_demandes_vendeurs.aspx");

            myConnection.Close();
        }

        protected void acceptation_demande(object sender, CommandEventArgs e)
        {
            MailMessage msg = new MailMessage();
            
            myConnection.Open();
            SqlCommand commande_accepter_demande = new SqlCommand("UPDATE PPVendeurs SET Statut = 0, Pourcentage = " + taux_facturation.Text + " WHERE NoVendeur = " + e.CommandArgument.ToString(), myConnection);
            commande_accepter_demande.ExecuteNonQuery();
            //Response.Write(commande_accepter_demande.CommandText);
            
            msg.Body = cont_mail_acceptation.Text;
            msg.Body += "\nPourcentage de facturation retenu par le gestionnaire: " + taux_facturation.Text + "%";

            try
            {
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);                

                msg.Subject = "Acceptation de la demande d'abonnement au site LesPetiesPuces.com";
                msg.From = new MailAddress("e.clubdegolf@gmail.com", "Gestionnaire de LesPetiesPuces.com");
                msg.To.Add(new MailAddress(courriel_demande.Text));
                SmtpServer.Credentials = new NetworkCredential("e.clubdegolf@gmail.com", "TouraTou");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(msg);

            }
            catch (Exception k)
            {
                Session["err_msg"] = "Echec de l'envoi du mail de confirmation: " + k.ToString();
            }

            Session["msg"] = "Le vendeur " + titre_demande.Text + " a bien été accepté.";

            Response.Redirect("gerer_demandes_vendeurs.aspx");
            
            myConnection.Close();
        }
    }
}