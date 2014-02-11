using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;
using Microsoft.Reporting.WebForms;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace Puces_R
{
    public partial class ResultatPaiement : System.Web.UI.Page
    {
        private bool transactionAccepte = false;
        private Facture facture;

        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        private long NoCommande
        {
            get
            {
                return (long)ViewState["NoCommande"];
            }
            set
            {
                ViewState["NoCommande"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int noClient = (int)Session["ID"];
                long noVendeur = long.Parse(Request.Params["novendeur"]);
                short codeLivraison = short.Parse(Request.Params["codelivraison"]);

                HttpRequest requete = HttpContext.Current.Request;

                String noAutorisation = (String)requete.Form["NoAutorisation"];
                String dateAutorisation = (String)requete.Form["DateAutorisation"];
                String fraisMarchand = (String)requete.Form["FraisMarchand"];

                switch (noAutorisation)
                {
                    case "0":
                        lblMessageResultat.Text = "Transaction annulée par l'utilisateur";
                        break;
                    case "1":
                        lblMessageResultat.Text = "Transaction refusée : Date d'expiration dépassée";
                        break;
                    case "2":
                        lblMessageResultat.Text = "Transaction refusée : Limite de crédit atteinte";
                        break;
                    case "3":
                        lblMessageResultat.Text = "Transaction refusée : Carte refusée";
                        break;
                    default:
                        transactionAccepte = true;
                        mvActionMessage.ActiveViewIndex = 1;
                        phRapport.Visible = true;
                        lblMessageResultat.Text = "Merci, la transaction à été acceptée!";
                        lblMessageResultat.ForeColor = Color.Green;
                        facture = new Facture(noClient, noVendeur, codeLivraison);

                        SqlCommand commandeTypeLivraison = new SqlCommand("SELECT Description FROM PPTypesLivraison WHERE CodeLivraison = " + codeLivraison, myConnection);

                        myConnection.Open();
                        String typeLivraison = (String)commandeTypeLivraison.ExecuteScalar();
                        myConnection.Close();

                        ctrRapport.LocalReport.SetParameters(new ReportParameter("SousTotal", facture.SousTotal.ToString()));
                        ctrRapport.LocalReport.SetParameters(new ReportParameter("Poids", facture.PoidsTotal.ToString()));
                        ctrRapport.LocalReport.SetParameters(new ReportParameter("TypeLivraison", typeLivraison));
                        ctrRapport.LocalReport.SetParameters(new ReportParameter("CoutLivraison", facture.PrixLivraison.ToString()));
                        ctrRapport.LocalReport.SetParameters(new ReportParameter("TPS", facture.PrixTPS.ToString()));
                        ctrRapport.LocalReport.SetParameters(new ReportParameter("TVQ", facture.PrixTVQ.ToString()));
                        ctrRapport.LocalReport.SetParameters(new ReportParameter("GrandTotal", facture.GrandTotal.ToString()));
                        ctrRapport.LocalReport.SetParameters(new ReportParameter("NoAutorisation", noAutorisation));
                        ctrRapport.LocalReport.SetParameters(new ReportParameter("DateAutorisation", dateAutorisation));

                        break;
                }

                hypReessayer.NavigateUrl = "Commande.aspx?novendeur=" + noVendeur + "&codelivraison=" + codeLivraison;
            }
        }

        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
            if (transactionAccepte)
            {
                EffectuerTransaction();
            }
        }

        public void EffectuerTransaction()
        {
            myConnection.Open();

            SqlTransaction transaction = myConnection.BeginTransaction();

            try
            {
                SqlCommand commandeNoCommande = new SqlCommand("SELECT MAX(NoCommande) FROM PPCommandes", myConnection, transaction);

                NoCommande = (long)commandeNoCommande.ExecuteScalar() + 1;

                SqlCommand commandePaiement = new SqlCommand("INSERT INTO PPCommandes VALUES (@noCommande, @noClient, @noVendeur, @dateCommande, @livraison, @typeLivraison, @montantTotal, @TPS, @TVQ, @poidsTotal, @statut, @noAutorisation)", myConnection, transaction);

                SqlParameterCollection parameters = commandePaiement.Parameters;

                parameters.Add(new SqlParameter("noCommande", NoCommande));
                parameters.Add(new SqlParameter("noClient", Session["ID"]));
                parameters.Add(new SqlParameter("noVendeur", facture.NoVendeur));
                parameters.Add(new SqlParameter("dateCommande", DateTime.Now));
                parameters.Add(new SqlParameter("livraison", facture.PrixLivraison));
                parameters.Add(new SqlParameter("typeLivraison", facture.CodeLivraison));
                parameters.Add(new SqlParameter("montantTotal", facture.SousTotal));
                parameters.Add(new SqlParameter("TPS", facture.PrixTPS));
                parameters.Add(new SqlParameter("TVQ", facture.PrixTVQ));
                parameters.Add(new SqlParameter("poidsTotal", facture.PoidsTotal));
                parameters.Add(new SqlParameter("statut", "p"));
                parameters.Add(new SqlParameter("noAutorisation", 1));

                commandePaiement.ExecuteNonQuery();

                SqlCommand commandeNbItems = new SqlCommand("UPDATE P SET NombreItems = P.NombreItems - A.NbItems FROM PPProduits P INNER JOIN PPArticlesEnPanier A ON P.NoProduit = A.NoProduit WHERE A.NoClient = " + Session["ID"] + " AND P.NoVendeur = " + facture.NoVendeur, myConnection, transaction);
                commandeNbItems.ExecuteNonQuery();

                SqlCommand commandeViderPanier = new SqlCommand("DELETE FROM PPArticlesEnPanier WHERE NoClient = " + Session["ID"] + " AND NoVendeur = " + facture.NoVendeur, myConnection, transaction);
                commandeViderPanier.ExecuteNonQuery();

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string filenameExtension;

                byte[] bytes = ctrRapport.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                MemoryStream flux = new MemoryStream(bytes);

                Attachment attachement = new Attachment(flux, "BonDeCommande-" + NoCommande + ".pdf");
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new NetworkCredential("e.clubdegolf@gmail.com", "TouraTou");
                client.EnableSsl = true;

                EnvoyerMessage(client, GetAdresse("PPClients", " WHERE NoClient = " + facture.NoClient, transaction), attachement);
                EnvoyerMessage(client, GetAdresse("PPVendeurs", " WHERE NoVendeur = " + facture.NoVendeur, transaction), attachement);

                transaction.Commit();
            }
            catch (SqlException)
            {
                transaction.Rollback();
            }

            myConnection.Close();
        }

        private MailAddress GetAdresse(string nomTable, string whereClause, SqlTransaction transaction)
        {
            SqlCommand commandeCourriel = new SqlCommand("SELECT AdresseEmail, Prenom + ' ' + Nom AS NomComplet FROM " + nomTable + whereClause, myConnection, transaction);
            SqlDataReader lecteurCourriel = commandeCourriel.ExecuteReader();
            lecteurCourriel.Read();
            string courrielClient = (String)lecteurCourriel["AdresseEmail"];
            string nomComplet = (String)lecteurCourriel["NomComplet"];
            lecteurCourriel.Close();
            return new MailAddress(courrielClient, nomComplet);
        }

        private void EnvoyerMessage(SmtpClient client, MailAddress adresse, Attachment attachement)
        {
            MailMessage message = new MailMessage();
            message.Body = "Bonjour " + adresse.DisplayName + ", \n\n";
            message.Body += "La commande incluse en pièce jointe a été approuvée.\n\n";
            message.Body += "Merci.\n";
            message.Attachments.Add(attachement);
            message.Subject = "Commande #" + NoCommande;
            message.From = new MailAddress("e.clubdegolf@gmail.com", "Gestionnaire de LesPetiesPuces.com");
            message.To.Add(adresse);
            client.Credentials = new NetworkCredential("e.clubdegolf@gmail.com", "TouraTou");
            client.EnableSsl = true;
            client.Send(message);
        }
    }
}