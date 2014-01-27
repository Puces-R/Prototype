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
    public partial class gerer_demandes_vendeurs : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable tableProduits = charge_demandes();

                rptDemandes.DataSource = new DataView(tableProduits);
                rptDemandes.DataBind();
            }

            if (Session["err_msg"] != null)
                if (Session["err_msg"] != "")
                {
                    Response.Write(Session["err_msg"]);
                    Session["err_msg"] = "";
                }

            foreach (RepeaterItem item in rptDemandes.Items)
            {
                Label courriel = (Label)item.FindControl("courriel_demande");
            }
        }

        private DataTable charge_demandes()
        {

            SqlDataAdapter adapteurDemandes = new SqlDataAdapter("SELECT * FROM PPVendeurs WHERE Statut = 2", myConnection);
            DataTable tableDemandes = new DataTable();
            adapteurDemandes.Fill(tableDemandes);
            myConnection.Close();

            return tableDemandes;
        }

        protected void rptDemandes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {

                Label titre_demande = (Label)item.FindControl("titre_demande");
                Label addr_demande = (Label)item.FindControl("addr_demande");
                Label tels_demande = (Label)item.FindControl("tels_demande");
                Label courriel_demande = (Label)item.FindControl("courriel_demande");
                Label charge_max_demande = (Label)item.FindControl("charge_max_demande");
                Label livraison_gratuite = (Label)item.FindControl("livraison_gratuite");
                Label date_demande = (Label)item.FindControl("date_demande");
                Button btnRefuser = (Button)item.FindControl("btn_refuser");
                Button btn_accepter = (Button)item.FindControl("btn_accepter");
                TextBox cont_mail_acceptation = (TextBox)item.FindControl("cont_mail_acceptation");
                TextBox cont_mail_refus = (TextBox)item.FindControl("cont_mail_refus");

                DataRowView drvDemande = (DataRowView)e.Item.DataItem;
                                
                titre_demande.Text = drvDemande["NomAffaires"].ToString() + ", par " + drvDemande["Prenom"].ToString() + " " + drvDemande["Nom"].ToString();
                addr_demande.Text = drvDemande["Rue"].ToString() + ", " + drvDemande["Ville"].ToString() + ", " + drvDemande["Pays"].ToString();
                tels_demande.Text = drvDemande["Tel1"].ToString();
                courriel_demande.Text = drvDemande["AdresseEmail"].ToString();
                charge_max_demande.Text = drvDemande["MaxLivraison"].ToString() + "Kg";
                livraison_gratuite.Text = drvDemande["LivraisonGratuite"].ToString();
                date_demande.Text = drvDemande["DateCreation"].ToString();
                btnRefuser.CommandArgument = drvDemande["AdresseEmail"].ToString();
                btn_accepter.CommandArgument = drvDemande["AdresseEmail"].ToString();
                cont_mail_acceptation.Text = "Bonjour " + drvDemande["Prenom"].ToString() + " " + drvDemande["Nom"].ToString() + "\nFélicitations! Votre inscription sur LesPetitesPuces.com a été acceptée.";
                cont_mail_refus.Text = "Bonjour " + drvDemande["Prenom"].ToString() + " " + drvDemande["Nom"].ToString() + "\nVotre inscription sur LesPetitesPuces.com n'a pas été acceptée.";
            }
        }

        protected void rptDemandes_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            
        }

        protected void refus_demande(object sender, CommandEventArgs e)
        {
            myConnection.Open();
            SqlCommand commande_refuser_demande = new SqlCommand("DELETE FROM PPVendeurs WHERE AdresseEmail = '" + e.CommandArgument + "'", myConnection);
            TextBox tbMail, tbTaux;
            commande_refuser_demande.ExecuteNonQuery();

            try
            {
                MailMessage msg = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);

                //Response.Write(rptDemandes.Items.Count);
                foreach (RepeaterItem item in rptDemandes.Items)
                {
                    Label courriel = (Label) item.FindControl("courriel_demande");
                    if (courriel.Text == e.CommandArgument.ToString())
                    {
                        tbMail = (TextBox)item.FindControl("cont_mail_refus");
                        //tbTaux = (TextBox)item.FindControl("taux_facturation");
                        msg.Body = tbMail.Text;
                        //msg.Body += " \nTaux de facturation retenu par le gestionnaire: " + tbTaux.Text + "%";
                    }
                    //Response.Write("_" + courriel.Text + "_");
                    //Response.Write(rptDemandes.Items.Count);
                }

                msg.Subject = "Refus de la demande d'abonnement au site LesPetiesPuces.com";
                msg.From = new MailAddress("e.clubdegolf@gmail.com", "Gestionnaire de LesPetiesPuces.com");
                msg.To.Add(new MailAddress(e.CommandArgument.ToString()));
                SmtpServer.Credentials = new NetworkCredential("e.clubdegolf@gmail.com", "TouraTou");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(msg);

            }
            catch (Exception k)
            {
                Session["err_msg"] = "Echec de l'envoi du mail de confirmation: " + k.ToString();
            }

            DataTable tableProduits = charge_demandes();

            rptDemandes.DataSource = new DataView(tableProduits);
            rptDemandes.DataBind();

            myConnection.Close();
        }

        protected void acceptation_demande(object sender, CommandEventArgs e)
        {
            TextBox tbMail, tbTaux;
            string taux_val = "";
            MailMessage msg = new MailMessage();

            //Response.Write(rptDemandes.Items.Count);
            foreach (RepeaterItem item in rptDemandes.Items)
            {
                Label courriel = (Label)item.FindControl("courriel_demande");
                if (courriel.Text == e.CommandArgument.ToString())
                {
                    tbMail = (TextBox)item.FindControl("cont_mail_acceptation");
                    tbTaux = (TextBox)item.FindControl("taux_facturation");
                    msg.Body = tbMail.Text;
                    msg.Body += " \nTaux de facturation retenu par le gestionnaire: " + tbTaux.Text + "%";
                    taux_val = tbTaux.Text;
                }
                //Response.Write("_" + courriel.Text + "_");
                //Response.Write(rptDemandes.Items.Count);
            }

            myConnection.Open();
            SqlCommand commande_accepter_demande = new SqlCommand("UPDATE PPVendeurs SET Statut = 0, Pourcentage = " + taux_val + " WHERE AdresseEmail = '" + e.CommandArgument + "'", myConnection);
            commande_accepter_demande.ExecuteNonQuery();
            //Response.Write(commande_accepter_demande.CommandText);
            try
            {
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);                

                msg.Subject = "Acceptation de la demande d'abonnement au site LesPetiesPuces.com";
                msg.From = new MailAddress("e.clubdegolf@gmail.com", "Gestionnaire de LesPetiesPuces.com");
                msg.To.Add(new MailAddress(e.CommandArgument.ToString()));
                SmtpServer.Credentials = new NetworkCredential("e.clubdegolf@gmail.com", "TouraTou");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(msg);

            }
            catch (Exception k)
            {
                Session["err_msg"] = "Echec de l'envoi du mail de confirmation: " + k.ToString();
            }

            DataTable tableProduits = charge_demandes();

            rptDemandes.DataSource = new DataView(tableProduits);
            rptDemandes.DataBind();

            myConnection.Close();
        }
    }
}