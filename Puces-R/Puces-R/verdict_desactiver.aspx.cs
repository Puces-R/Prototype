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
    public partial class verdict_desactiver : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
        int no_vendeur;
        string liste_a_desactiver;

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (Session["err_msg"] != null)
                if (Session["err_msg"].ToString() != "")
                {
                    Response.Write(Session["err_msg"]);
                    Session["err_msg"] = "";
                }

            if (!IsPostBack)
            {
                if (Session["desactiver_vendeur"] != null)
                {
                    if (Session["desactiver_vendeur"].ToString() != "")
                    {
                        ((SiteMaster)Master).Titre = "Voulez vous vraiment désactiver ce vendeur";
                        no_vendeur = Convert.ToInt32(Session["desactiver_vendeur"].ToString());
                        mv_verdict.SetActiveView(view_un_vendeur);
                        Session["desactiver_vendeur"] = null;
                    }
                }
                else
                {
                    if (Session["desactiver_liste"] != null)
                    {
                        if (Session["desactiver_liste"].ToString() != "")
                        {
                            ((SiteMaster)Master).Titre = "Voulez vous vraiment désactiver ces vendeurs";
                            liste_a_desactiver = Session["desactiver_liste"].ToString();
                            charge_liste();
                            mv_verdict.SetActiveView(view_liste);
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
                btn_desactiver.CommandArgument = results["NoVendeur"].ToString();
            }

            myConnection.Close();
        }
         

        protected void desactiver_un_vendeur(object sender, CommandEventArgs e)
        {
            myConnection.Open();
            using (myConnection)
            {
                SqlTransaction transaction;

                transaction = myConnection.BeginTransaction();

                try
                {
                    string commande = "UPDATE PPVendeurs SET Statut = 1 WHERE NoVendeur = " + e.CommandArgument + "; ";
                    commande += "DELETE FROM PPProduits WHERE NoVendeur = " + e.CommandArgument + " AND NoProduit NOT IN (SELECT NoProduit FROM PPDetailsCommandes);";
                    commande += "UPDATE PPProduits SET Disponibilité = 0 WHERE NoVendeur = " + e.CommandArgument + " AND NoProduit IN (SELECT NoProduit FROM PPDetailsCommandes);";
                    SqlCommand commande_desactiver_vendeur = new SqlCommand(commande, myConnection, transaction);
                    commande_desactiver_vendeur.ExecuteNonQuery();
                    transaction.Commit();
                    Session["msg"] = "Le vendeur " + titre_demande.Text + " a bien été désactivé.";
                    Response.Redirect("gerer_inactivite_vendeurs.aspx");
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    Session["err_msg"] = "Erreur lors de la mise à jour de la base de données: " + ex.ToString();
                    Response.Redirect("gerer_inactivite_vendeurs.aspx");
                }
            }

            myConnection.Close();
        }

        protected void desactiver_liste_vendeur(object sender, CommandEventArgs e)
        {
            List<String> tab_desactiver = new List<String>();
            //tab_desactiver = liste_a_desactiver.Split(',');
            tab_desactiver.AddRange(Session["desactiver_liste"].ToString().Split(','));

            foreach (string vendeur_a_desctiver in tab_desactiver)
                desactiver_un_vendeur(Convert.ToInt32(vendeur_a_desctiver));

            Session["msg"] = "Les vendeurs sélectionnés ont bien été désactivés.";
            Session["desactiver_liste"] = "";
            Response.Redirect("gerer_inactivite_vendeurs.aspx");
        }

        public void desactiver_un_vendeur(int vendeur_a_desactiver)
        {
            SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
            myConnection.Open();
            using (myConnection)
            {
                SqlTransaction transaction;

                transaction = myConnection.BeginTransaction();

                try
                {
                    string commande = "UPDATE PPVendeurs SET Statut = 1 WHERE NoVendeur = " + vendeur_a_desactiver + "; ";
                    commande += "DELETE FROM PPProduits WHERE NoVendeur = " + vendeur_a_desactiver + " AND NoProduit NOT IN (SELECT NoProduit FROM PPDetailsCommandes);";
                    commande += "UPDATE PPProduits SET Disponibilité = 0 WHERE NoVendeur = " + vendeur_a_desactiver + " AND NoProduit IN (SELECT NoProduit FROM PPDetailsCommandes);";
                    SqlCommand commande_desactiver_vendeur = new SqlCommand(commande, myConnection, transaction);
                    commande_desactiver_vendeur.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    Session["err_msg"] = "Erreur lors de la mise à jour de la base de données pour le vendeur : " + vendeur_a_desactiver + ". Message: " + ex.ToString();
                    Response.Redirect("gerer_inactivite_vendeurs.aspx");
                }
            }

            myConnection.Close();
        }

        private DataTable charge_liste()
        {
            string req_liste = "SELECT * FROM PPVendeurs WHERE NoVendeur IN ( " + liste_a_desactiver + " ) ";

            SqlDataAdapter adapteurInnactif1 = new SqlDataAdapter(req_liste, myConnection);
            DataTable tableInnactif1 = new DataTable();
            adapteurInnactif1.Fill(tableInnactif1);
            //Response.Write(req_liste);

            rptInnactifs1.DataSource = tableInnactif1;
            rptInnactifs1.DataBind();

            myConnection.Close();

            return tableInnactif1;
        }

        protected void rptInnactifs1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                Label lbl_num = (Label)item.FindControl("lbl_num");
                Label item_a_desactiver = (Label)item.FindControl("item_a_desactiver");

                DataRowView drvinactif1 = (DataRowView)e.Item.DataItem;

                lbl_num.Text = (e.Item.ItemIndex + 1).ToString();
                item_a_desactiver.Text = drvinactif1["NomAffaires"].ToString() + ", par " + drvinactif1["Prenom"].ToString() + " " + drvinactif1["Nom"].ToString();
            }
        }
    }
}