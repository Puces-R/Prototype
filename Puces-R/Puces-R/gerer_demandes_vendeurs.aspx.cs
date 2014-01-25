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
                btnRefuser.CommandArgument = drvDemande["NoVendeur"].ToString();
                btn_accepter.CommandArgument = drvDemande["NoVendeur"].ToString();
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
            SqlCommand commande_refuser_demande = new SqlCommand("DELETE FROM PPVendeurs WHERE NoVendeur = " + e.CommandArgument, myConnection);
            commande_refuser_demande.ExecuteNonQuery();

            DataTable tableProduits = charge_demandes();

            rptDemandes.DataSource = new DataView(tableProduits);
            rptDemandes.DataBind();

            myConnection.Close();
        }

        protected void acceptation_demande(object sender, CommandEventArgs e)
        {
            myConnection.Open();
            SqlCommand commande_accepter_demande = new SqlCommand("UPDATE PPVendeurs SET Statut = 0 WHERE NoVendeur = " + e.CommandArgument, myConnection);
            commande_accepter_demande.ExecuteNonQuery();

            DataTable tableProduits = charge_demandes();

            rptDemandes.DataSource = new DataView(tableProduits);
            rptDemandes.DataBind();
            myConnection.Close();
        }
    }
}