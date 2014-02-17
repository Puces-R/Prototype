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
    public partial class DesactiverPanierVendeur : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
        int no_client;
        string liste_a_desactiver;
        String[] split;

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
                if (Session["desactiver_panier"] != null)
                {
                    if (Session["desactiver_panier"].ToString() != "")
                    {
                        ((SiteMaster)Master).Titre = "Voulez vous vraiment supprimer ce panier";
                        //no_client = Convert.ToInt32(Session["desactiver_panier"].ToString());
                       
                        //
                        string nom = Session["desactiver_panier"].ToString();
                        split = nom.Split('-');

                        long noClient = (long)Convert.ToInt64(split[0]);

                        string nomComplet;
                        if (split[0].Trim() == "")
                        {
                            nomComplet = "Client #" + noClient;
                        }
                        else
                        {
                            nomComplet = split[1];
                        }

                        ctrBoitePanier.NoVendeur = (int)Session["ID"];
                        ctrBoitePanier.NoClient = noClient;
                        ctrBoitePanier.Titre = nomComplet;
                        ctrBoitePanier.ChargerArticlesEnPanier();

                        mv_verdict.SetActiveView(view_un_vendeur);
                    }
                }
                else
                {
                    if (Session["desactiver_liste"] != null)
                    {
                        if (Session["desactiver_liste"].ToString() != "")
                        {
                            ((SiteMaster)Master).Titre = "Voulez vous vraiment supprimer ces paniers";
                            liste_a_desactiver = Session["desactiver_liste"].ToString();
                            charge_liste();
                            mv_verdict.SetActiveView(view_liste);
                        }
                    }
                    else Response.Redirect("Default.aspx");
                }
            }

            //BoitePanier ctrBoitePanier = (BoitePanier)item.FindControl("ctrBoitePanier");

          

            //Session["desactiver_panier"] = null;
            //myConnection.Open();
            ////SqlCommand charger = new SqlCommand("SELECT * FROM PPVendeurs WHERE NoVendeur = " + no_client, myConnection);

            //SqlDataReader results = charger.ExecuteReader();

            //if (results.Read())
            //{
            //    //titre_demande.Text = results["NomAffaires"].ToString() + ", par " + results["Prenom"].ToString() + " " + results["Nom"].ToString();
            //    //addr_demande.Text = results["Rue"].ToString() + ", " + results["Ville"].ToString() + ", " + results["Pays"].ToString();
            //    //tels_demande.Text = results["Tel1"].ToString();
            //    //courriel_demande.Text = results["AdresseEmail"].ToString();
            //    //charge_max_demande.Text = results["MaxLivraison"].ToString() + "lb";
            //    //livraison_gratuite.Text = results["LivraisonGratuite"].ToString();
            //    //date_demande.Text = results["DateCreation"].ToString();
            //    //btn_desactiver.CommandArgument = results["NoVendeur"].ToString();
            //}

            //myConnection.Close();  
        }


        protected void desactiver_un_Panier(object sender, CommandEventArgs e)
        {
            myConnection.Open();
            using (myConnection)
            {
                SqlTransaction transaction;

                transaction = myConnection.BeginTransaction();

                try
                {
                    string commande = "DELETE FROM PPArticlesEnPanier where NoVendeur=" + Session["ID"] +" and NoClient="+ split[0] +"; ";
                    SqlCommand commande_desactiver_vendeur = new SqlCommand(commande, myConnection, transaction);
                    commande_desactiver_vendeur.ExecuteNonQuery();
                    transaction.Commit();
                    //Session["msg"] = "Le vendeur " + titre_demande.Text + " a bien été désactivé.";
                    Response.Redirect("GererPanierVendeur.aspx");
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    Session["err_msg"] = "Erreur lors de la mise à jour de la base de données: " + ex.ToString();
                    Response.Redirect("GererPanierVendeur.aspx");
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
            {
                String[] liste = vendeur_a_desctiver.Split('-');
                desactiver_un_Panier(Convert.ToInt32(liste[0]));
            }

            Session["msg"] = "Les paniers sélectionnés ont bien été supprimés.";
            Session["desactiver_liste"] = "";
            Response.Redirect("gerer_inactivite_vendeurs.aspx");
        }

        public void desactiver_un_Panier(int vendeur_a_desactiver)
        {
            //SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
            myConnection.Open();
            using (myConnection)
            {
                SqlTransaction transaction;

                transaction = myConnection.BeginTransaction();

                try
                {
                    string commande = "DELETE FROM PPArticlesEnPanier where NoVendeur=" + Session["ID"] + " and NoClient=" + vendeur_a_desactiver + "; ";
                    SqlCommand commande_desactiver_vendeur = new SqlCommand(commande, myConnection, transaction);
                    commande_desactiver_vendeur.ExecuteNonQuery();
                    transaction.Commit();
                    //Session["msg"] = "Le vendeur " + titre_demande.Text + " a bien été désactivé.";
                    
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

        private string creerListeNoClient()
        {
            string[] tab = liste_a_desactiver.Split(',');
            for (int i = 0; i < tab.Length; i++)
            {
                tab[i] = tab[i].Split('-')[0];
            }

            return string.Join(", ", tab);
        }
        private DataTable charge_liste()
        {
            String chaine = creerListeNoClient();
            //string req_liste = "SELECT * FROM PPVendeurs WHERE NoVendeur IN ( " + liste_a_desactiver + " ) ";
            string req_liste="SELECT  (C.Nom + ' ' + C.Prenom) AS NomC, C.NoClient,V.NomAffaires, A.NoVendeur, SUM(A.NbItems * P.PrixVente) AS SousTotal, MAX(A.DateCreation) AS DerniereMAJ FROM PPArticlesEnPanier AS A INNER JOIN PPVendeurs AS V ON A.NoVendeur = V.NoVendeur INNER JOIN PPProduits AS P ON A.NoProduit = P.NoProduit inner join PPClients AS C on A.NoClient = C.NoClient" + " WHERE A.NoVendeur="+Session["ID"] +" and C.NoClient IN ( " + chaine + " ) " + " GROUP BY V.NomAffaires, A.NoVendeur, C.Nom,C.Prenom,C.NoClient ";
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
                item_a_desactiver.Text = " Panier de " +drvinactif1["NomC"].ToString() + ", pour un total de " + drvinactif1["SousTotal"].ToString() ;
            }
        }
    }
}