﻿using System;
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
        SqlConnection myConnection = Librairie.Connexion;
        int no_vendeur;

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.Titre = "Verdict de la demande";
            if (Session["err_msg"] != null)
                if (Session["err_msg"].ToString() != "")
                {
                    
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
                        Master.Titre = "Acceptation de la demande";
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
                            Master.Titre = "Refus de la demande";
                        }
                    }
                    else
                    {
                        if (Session["details_demande"] != null)
                        {
                            if (Session["details_demande"].ToString() != "")
                            {
                                no_vendeur = Convert.ToInt32(Session["details_demande"].ToString());
                                mv_verdict.SetActiveView(view_details);
                                Session["details_demande"] = null;
                                Master.Titre = "Détails de la demande";
                            }
                        }
                        else Librairie.RefuserAutorisation();
                    }
                }
            }

            myConnection.Open();
            SqlCommand charger = new SqlCommand("SELECT * FROM PPVendeurs WHERE NoVendeur = " + no_vendeur, myConnection);

            SqlDataReader results = charger.ExecuteReader();

            if (results.Read())
            {
                titre_demande.Text = results["NomAffaires"].ToString() + ", par " + results["Prenom"].ToString() + " " + results["Nom"].ToString();
                addr_demande.Text = results["Rue"].ToString() + ", " + results["Ville"].ToString() + ", " + results["Pays"].ToString();
                tels_demande.Text = (results["Tel1"] != DBNull.Value ? Telephone.Format(results["Tel1"].ToString()) : "") + (results["Tel2"] != DBNull.Value ? ", " + Telephone.Format(results["Tel2"].ToString()) : "");
                courriel_demande.Text = results["AdresseEmail"].ToString();
                charge_max_demande.Text = results["MaxLivraison"].ToString() + " Lbs";
                livraison_gratuite.Text = Convert.ToDecimal(results["LivraisonGratuite"]).ToString("N") + " $";
                date_demande.Text = results["DateCreation"].ToString();
                btn_accepter.CommandArgument = results["NoVendeur"].ToString();
                btn_refuser.CommandArgument = results["NoVendeur"].ToString();
                cont_mail_acceptation.Text = "Bonjour " + results["Prenom"].ToString() + " " + results["Nom"].ToString() + ". \nFélicitations! Votre inscription sur LesPetitesPuces.com a été acceptée.";
                cont_mail_refus.Text = "Bonjour " + results["Prenom"].ToString() + " " + results["Nom"].ToString() + ". \nVotre inscription sur LesPetitesPuces.com n'a pas été acceptée.";
                btn_accepter_details.CommandArgument = results["NoVendeur"].ToString();
                btn_refuser_details.CommandArgument = results["NoVendeur"].ToString();
            }

            myConnection.Close();
        }
         

        protected void refus_demande(object sender, CommandEventArgs e)
        {
            myConnection.Open();
            SqlCommand commande_refuser_demande = new SqlCommand("DELETE FROM PPVendeurs WHERE NoVendeur = " + e.CommandArgument.ToString(), myConnection);
            commande_refuser_demande.ExecuteNonQuery();

            Courriel conf_mail = new Courriel("Refus de la demande d'abonnement au site LesPetiesPuces.com", cont_mail_refus.Text, courriel_demande.Text);

            if (conf_mail.envoyer())
                Session["msg"] = "Le vendeur " + titre_demande.Text + " a bien été refusé.";
            else Session["msg"] = "Le vendeur " + titre_demande.Text + " a bien été refusé mais une erreur est survenue lors de l'envoi du courriel de refus";
            Response.Redirect("gerer_demandes_vendeurs.aspx");

            myConnection.Close();
        }

        protected void acceptation_demande(object sender, CommandEventArgs e)
        {
            string msg = "";
            
            myConnection.Open();
            SqlCommand commande_accepter_demande = new SqlCommand("UPDATE PPVendeurs SET Statut = 0, Pourcentage = " + taux_facturation.Text + " WHERE NoVendeur = " + e.CommandArgument.ToString(), myConnection);
            commande_accepter_demande.ExecuteNonQuery();
            
            msg = cont_mail_acceptation.Text;
            msg += " \n\nTaux de redevance retenu par le gestionnaire: " + taux_facturation.Text + "%";
            
            Courriel conf_mail = new Courriel("Acceptation de la demande d'abonnement au site LesPetiesPuces.com", msg, courriel_demande.Text);
            
            if (conf_mail.envoyer())
                Session["msg"] = "Le vendeur " + titre_demande.Text + " a bien été accepté.";
            else Session["msg"] = "Le vendeur " + titre_demande.Text + " a bien été accepté mais une erreure est survenue lors de l'envoi du courriel de confirmation";


            myConnection.Close();

            Response.Redirect("gerer_demandes_vendeurs.aspx");
        }

        protected void refus_details_demande(object sender, CommandEventArgs e)
        {
            Session["refus_vendeur"] = e.CommandArgument.ToString();
            Response.Redirect(Chemin.Ajouter("verdict_demande.aspx", "Retour à l'accueil"));
        }

        protected void acceptation_details_demande(object sender, CommandEventArgs e)
        {
            Session["acceptation_vendeur"] = e.CommandArgument.ToString();
            Response.Redirect(Chemin.Ajouter("verdict_demande.aspx", "Retour à l'accueil"));
        }
    }
}