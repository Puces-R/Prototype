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
    public partial class gerer_vendeurs : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
        string whereClause, orderByClause = " ORDER BY ";

        protected void Page_Load(object sender, EventArgs e)
        {
            List<String> whereParts = new List<String>();

            if (txtCritereRecherche.Text != string.Empty)
            {
                String colonne = "PPVendeurs.NomAffaires";
                switch (ddlTypeRecherche.SelectedIndex)
                {
                    case 0:
                        colonne = "PPVendeurs.NomAffaires";
                        break;
                }
                whereParts.Add(colonne + " LIKE '%" + txtCritereRecherche.Text + "%'");
            }

            //String whereClause;
            if (whereParts.Count > 0)
            {
                whereClause = " WHERE Statut = 2 AND " + string.Join(" AND ", whereParts);
            }
            else
            {
                whereClause = " WHERE Statut = 2 ";
            }

            //String orderByClause = " ORDER BY ";
            switch (ddlTrierPar.SelectedIndex)
            {
                case 0:
                    orderByClause += "PPVendeurs.NoVendeur";
                    break;
                case 1:
                    orderByClause += "PPVendeurs.NomAffaires";
                    break;
                case 2:
                    orderByClause += "PPVendeurs.DateCreation DESC";
                    break;
            }

            //if (Page.IsPostBack)
            //{
            DataTable tableProduits = charge_demandes();

            PagedDataSource pdsDemandes = new PagedDataSource();
            pdsDemandes.DataSource = new DataView(tableProduits);
            pdsDemandes.AllowPaging = true;
            pdsDemandes.PageSize = int.Parse(ddlParPage.SelectedValue);

            pdsDemandes.CurrentPageIndex = 0;

            ((SiteMaster)Master).Titre = "Gérer les vendeurs";
            //}

            if (Session["err_msg"] != null)
                if (Session["err_msg"].ToString() != "")
                {
                    Response.Write(Session["err_msg"]);
                    Session["err_msg"] = "";
                }

        }

        private DataTable charge_demandes()
        {
            SqlDataAdapter adapteurDemandes = new SqlDataAdapter("SELECT * FROM PPVendeurs " + whereClause + orderByClause, myConnection);
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

            myConnection.Close();
        }

        protected void acceptation_demande(object sender, CommandEventArgs e)
        {
            TextBox tbMail, tbTaux;
            string taux_val = "";
            MailMessage msg = new MailMessage();

            //Response.Write(rptDemandes.Items.Count);
           

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

            myConnection.Close();
        }
    }
}