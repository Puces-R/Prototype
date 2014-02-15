using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;
using Microsoft.Reporting.WebForms;
using System.Net;
using System.IO;
using System.Data;
using System.Configuration;
using System.Net.Mail;

namespace Puces_R
{
    public partial class ResultatPaiement : System.Web.UI.Page
    {
        private bool transactionAccepte = false;
        private Facture facture;
        String noAutorisation;
        String dateAutorisation;
        String fraisMarchand;

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

                noAutorisation = (String)requete.Form["NoAutorisation"];
                dateAutorisation = (String)requete.Form["DateAutorisation"];
                fraisMarchand = (String)requete.Form["FraisMarchand"];

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

                        ctrRapport.LocalReport.EnableExternalImages = true;
                        ctrRapport.LocalReport.DataSources.Clear();  
                        ctrRapport.LocalReport.ReportPath = "BonCommande.rdlc";

                        ctrRapport.LocalReport.SetParameters(new ReportParameter("SousTotal", facture.SousTotal.ToString()));
                        ctrRapport.LocalReport.SetParameters(new ReportParameter("Poids", facture.PoidsTotal.ToString()));
                        ctrRapport.LocalReport.SetParameters(new ReportParameter("TypeLivraison", typeLivraison));
                        ctrRapport.LocalReport.SetParameters(new ReportParameter("CoutLivraison", facture.PrixLivraison.ToString()));
                        ctrRapport.LocalReport.SetParameters(new ReportParameter("TPS", facture.PrixTPS.ToString()));
                        ctrRapport.LocalReport.SetParameters(new ReportParameter("TVQ", facture.PrixTVQ.ToString()));
                        ctrRapport.LocalReport.SetParameters(new ReportParameter("GrandTotal", facture.GrandTotal.ToString()));
                        ctrRapport.LocalReport.SetParameters(new ReportParameter("NoAutorisation", noAutorisation));
                        ctrRapport.LocalReport.SetParameters(new ReportParameter("DateAutorisation", dateAutorisation));

                        LectureXML lecture = new LectureXML(noVendeur);
                        if (lecture.Existe)
                        {
                            string cheminLogo = Server.MapPath("~/Images/Logo/" + lecture.NomLogo);
                            ctrRapport.LocalReport.SetParameters(new ReportParameter("LogoVendeur", cheminLogo));
                        }

                        SqlDataAdapter adapteurArticlesEnPanier = new SqlDataAdapter("SELECT P.Nom, P.Poids, P.PrixVente, A.NbItems, P.NoProduit, A.NoPanier FROM PPProduits P INNER JOIN PPArticlesEnPanier A ON P.NoProduit = A.NoProduit WHERE (A.NoClient = " + noClient + ") AND (A.NoVendeur = " + noVendeur + ") ", myConnection);
                        DataTable tableArticlesEnPanier = new DataTable();
                        adapteurArticlesEnPanier.Fill(tableArticlesEnPanier);
                        ctrRapport.LocalReport.DataSources.Add(new ReportDataSource("ArticlesEnPanierDetaille", tableArticlesEnPanier));

                        SqlDataAdapter adapteurClientDetaille = new SqlDataAdapter("SELECT NoClient, AdresseEmail, Prenom + ' ' + Nom AS NomComplet, Rue, Ville, Province, CodePostal, Pays, Tel1, Tel2 FROM PPClients WHERE NoClient = " + noClient, myConnection);
                        DataTable tableClientDetaille = new DataTable();
                        adapteurClientDetaille.Fill(tableClientDetaille);
                        ctrRapport.LocalReport.DataSources.Add(new ReportDataSource("ClientDetaille", tableClientDetaille));

                        SqlDataAdapter adapteurVendeurDetaille = new SqlDataAdapter("SELECT NoVendeur, NomAffaires, Prenom + ' ' + Nom AS NomComplet, Rue, Ville, Province, CodePostal, Pays, Tel1, Tel2, AdresseEmail FROM PPVendeurs WHERE NoVendeur = " + noVendeur, myConnection);
                        DataTable tableVendeurDetaille = new DataTable();
                        adapteurVendeurDetaille.Fill(tableVendeurDetaille);
                        ctrRapport.LocalReport.DataSources.Add(new ReportDataSource("VendeurDetaille", tableVendeurDetaille));

                        ctrRapport.LocalReport.Refresh();

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

                SqlCommand commandeCommande = new SqlCommand("INSERT INTO PPCommandes VALUES (@noCommande, @noClient, @noVendeur, @dateCommande, @livraison, @typeLivraison, @montantTotal, @TPS, @TVQ, @poidsTotal, @statut, @noAutorisation)", myConnection, transaction);

                SqlParameterCollection parametersCommandes = commandeCommande.Parameters;

                parametersCommandes.Add(new SqlParameter("noCommande", NoCommande));
                parametersCommandes.Add(new SqlParameter("noClient", Session["ID"]));
                parametersCommandes.Add(new SqlParameter("noVendeur", facture.NoVendeur));
                parametersCommandes.Add(new SqlParameter("dateCommande", DateTime.Now));
                parametersCommandes.Add(new SqlParameter("livraison", facture.PrixLivraison));
                parametersCommandes.Add(new SqlParameter("typeLivraison", facture.CodeLivraison));
                parametersCommandes.Add(new SqlParameter("montantTotal", facture.SousTotal));
                parametersCommandes.Add(new SqlParameter("TPS", facture.PrixTPS));
                parametersCommandes.Add(new SqlParameter("TVQ", facture.PrixTVQ));
                parametersCommandes.Add(new SqlParameter("poidsTotal", facture.PoidsTotal));
                parametersCommandes.Add(new SqlParameter("statut", "p"));
                parametersCommandes.Add(new SqlParameter("noAutorisation", 1));

                commandeCommande.ExecuteNonQuery();

                SqlCommand commandeNoHistorique = new SqlCommand("SELECT MAX(NoHistorique) FROM PPHistoriquePaiements", myConnection, transaction);

                object objNoHistorique = commandeNoHistorique.ExecuteScalar();
                 
                long noHistorique;
                if (objNoHistorique is DBNull)
                {
                    noHistorique = 1;
                }
                else
                {
                    noHistorique = (long)objNoHistorique + 1;
                }

                SqlCommand commandePourcentage = new SqlCommand("SELECT Pourcentage FROM PPVendeurs WHERE NoVendeur = " + facture.NoVendeur, myConnection, transaction);
                decimal pourcentage = (decimal)commandePourcentage.ExecuteScalar();
                decimal redevance = facture.SousTotal * pourcentage;
                
                SqlCommand commandeHistorique = new SqlCommand("INSERT INTO PPHistoriquePaiements VALUES (@noHistorique, @montantVente, @noVendeur, @noClient, @noCommande, @dateVente, @noAutorisation, @fraisLesi, @redevance, @fraisLivraison, @fraisTPS, @fraisTVQ)", myConnection, transaction);

                SqlParameterCollection parametersHistorique = commandeHistorique.Parameters;

                parametersHistorique.Add(new SqlParameter("noHistorique", noHistorique));
                parametersHistorique.Add(new SqlParameter("montantVente", facture.SousTotal));
                parametersHistorique.Add(new SqlParameter("noVendeur", facture.NoVendeur));
                parametersHistorique.Add(new SqlParameter("noClient", Session["ID"]));
                parametersHistorique.Add(new SqlParameter("noCommande", NoCommande));
                parametersHistorique.Add(new SqlParameter("dateVente", DateTime.Now));
                parametersHistorique.Add(new SqlParameter("noAutorisation", noAutorisation));
                parametersHistorique.Add(new SqlParameter("fraisLesi", fraisMarchand));
                parametersHistorique.Add(new SqlParameter("redevance", redevance));
                parametersHistorique.Add(new SqlParameter("fraisLivraison", facture.PrixLivraison));
                parametersHistorique.Add(new SqlParameter("fraisTPS", facture.PrixTPS));
                parametersHistorique.Add(new SqlParameter("fraisTVQ", facture.PrixTVQ));
                
                commandeHistorique.ExecuteNonQuery();

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
                SmtpClient client = new SmtpClient("smtpout.secureserver.net", 80);
                client.Credentials = new NetworkCredential("petitespuces@towardnewobjects.org", "NWa7dZ");
                MailAddress source = new MailAddress("petitespuces@towardnewobjects.org", "Gestionnaire de LesPetiesPuces.com");

                EnvoyerMessage(client, GetAdresse("PPClients", " WHERE NoClient = " + facture.NoClient, transaction), attachement, source);
                EnvoyerMessage(client, GetAdresse("PPVendeurs", " WHERE NoVendeur = " + facture.NoVendeur, transaction), attachement, source);

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

        private void EnvoyerMessage(SmtpClient client, MailAddress adresse, Attachment attachement, MailAddress source)
        {
            MailMessage message = new MailMessage();
            message.Body = "Bonjour " + adresse.DisplayName + ", \n\n";
            message.Body += "La commande incluse en pièce jointe a été approuvée.\n\n";
            message.Body += "Merci.\n";
            message.Attachments.Add(attachement);
            message.Subject = "Commande #" + NoCommande;
            message.From = source;
            message.To.Add(adresse);
            client.Send(message);
        }
    }
}