using System;
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
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
        int no_commande;

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (Session["err_msg"] != null)
                if (Session["err_msg"].ToString() != "")
                {
                    Response.Write(Session["err_msg"]);
                    Session["err_msg"] = "";
                }

            if (Session["no_commande_redevance"] != null)
            {
                if (Session["no_commande_redevance"].ToString() != "")
                {
                    no_commande = Convert.ToInt32(Session["no_commande_redevance"]);
                }
                else Response.Redirect("Default.aspx");
            }
            else Response.Redirect("Default.aspx");

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
                lbl_montant_vente.Text = results["MontantVente"].ToString();
                lbl_redevance.Text = results["Redevance"].ToString() + "lb";
                lbl_tps.Text = results["FraisTPS"].ToString();
                lbl_tvq.Text = results["FraisTVQ"].ToString();
                lbl_frais_livraison.Text = results["FraisLivraison"].ToString();
                lbl_num_autorisation.Text = results["NoAutorisation"].ToString();
                lbl_frais_lesi.Text = results["FraisLesi"].ToString();
            }

            ((SiteMaster)Master).Titre = "Détails de la commande de \"" + results["NomAffaires"].ToString() + "\"";
            myConnection.Close();
        }         
    }
}