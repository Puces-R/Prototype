﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Puces_R
{
    public partial class details_commande_redevance : System.Web.UI.Page
    {
        SqlConnection myConnection = Librairie.Connexion;
        int no_commande;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Librairie.Autorisation(false, false, false, true);
            }
            if (Session["err_msg"] != null)
                if (Session["err_msg"].ToString() != "")
                {
                    
                    Session["err_msg"] = "";
                }

            if (Session["no_commande_redevance"] != null)
            {
                if (Session["no_commande_redevance"].ToString() != "")
                {
                    no_commande = Convert.ToInt32(Session["no_commande_redevance"]);
                }
                else Librairie.RefuserAutorisation();
            }
            else Librairie.RefuserAutorisation();

            myConnection.Open();
            string req = "";

            req += " SELECT NomAffaires, NoCommande, PPClients.Nom, PPClients.Prenom, MontantVente, Redevance, FraisTPS, FraisTVQ, FraisLivraison, NoAutorisation, FraisLesi ";
            req += " FROM PPClients, PPVendeurs, PPHistoriquePaiements ";
            req += " WHERE PPClients.NoClient = PPHistoriquePaiements.NoClient ";
            req += " AND PPVendeurs.NoVendeur = PPHistoriquePaiements.NoVendeur ";
            req += " AND NoCommande = " + no_commande;

            SqlCommand charger = new SqlCommand(req, myConnection);

            SqlDataReader results = charger.ExecuteReader();

            if (results.Read())
            {
                titre_demande.Text = "Commande No: " + results["NoCommande"].ToString();
                lbl_nom_vendeur.Text = results["NomAffaires"].ToString();
                lbl_nom_client.Text = results["Prenom"].ToString() + " " + results["Nom"].ToString();
                lbl_montant_vente.Text = Convert.ToDecimal(results["MontantVente"]).ToString("N") + " $";
                lbl_redevance.Text = Convert.ToDecimal(results["Redevance"]).ToString("N") + " $";
                lbl_tps.Text = Convert.ToDecimal(results["FraisTPS"]).ToString("N") + " $";
                lbl_tvq.Text = Convert.ToDecimal(results["FraisTVQ"]).ToString("N") + " $";
                lbl_frais_livraison.Text = Convert.ToDecimal(results["FraisLivraison"]).ToString("N") + " $";
                lbl_num_autorisation.Text = results["NoAutorisation"].ToString();
                lbl_frais_lesi.Text = Convert.ToDecimal(results["FraisLesi"]).ToString("N") + " $";
            }

            ((SiteMaster)Master).Titre = "Détails de la commande" + " de \"" + results["NomAffaires"].ToString() + "\"";
            myConnection.Close();
        }         
    }
}