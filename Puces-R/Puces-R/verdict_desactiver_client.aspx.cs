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
    public partial class verdict_desactiver_client : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
        int no_client;
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
                if (Session["desactiver_client"] != null)
                {
                    if (Session["desactiver_client"].ToString() != "")
                    {
                        ((SiteMaster)Master).Titre = "Voulez vous vraiment désactiver ce client";
                        no_client = Convert.ToInt32(Session["desactiver_client"].ToString());
                        mv_verdict.SetActiveView(view_un_client);
                        Session["desactiver_client"] = null;
                    }
                }
                else
                {
                    if (Session["desactiver_liste"] != null)
                    {
                        if (Session["desactiver_liste"].ToString() != "")
                        {
                            ((SiteMaster)Master).Titre = "Voulez vous vraiment désactiver ces clients";
                            liste_a_desactiver = Session["desactiver_liste"].ToString();
                            charge_liste();
                            mv_verdict.SetActiveView(view_liste);
                        }
                    }
                    else Response.Redirect("Default.aspx");
                }
            }

            myConnection.Open();
            SqlCommand charger = new SqlCommand("SELECT * FROM PPclients WHERE Noclient = " + no_client, myConnection);

            SqlDataReader results = charger.ExecuteReader();

            if (results.Read())
            {
                titre_demande.Text = results["Prenom"].ToString() + " " + results["Nom"].ToString();
                addr_demande.Text = results["Rue"].ToString() + ", " + results["Ville"].ToString() + ", " + results["Pays"].ToString();
                tels_demande.Text = results["Tel1"].ToString();
                courriel_demande.Text = results["AdresseEmail"].ToString();                
                nb_connexions.Text = results["NbConnexions"].ToString();
                date_demande.Text = results["DateCreation"].ToString();
                btn_desactiver.CommandArgument = results["NoClient"].ToString();
            }

            myConnection.Close();
        }         

        protected void desactiver_un_client(object sender, CommandEventArgs e)
        {
            myConnection.Open();
            SqlCommand commande_verifier_visite = new SqlCommand("SELECT NoClient FROM PPVendeursClients WHERE NoClient = " + e.CommandArgument, myConnection);
            object visite = commande_verifier_visite.ExecuteScalar();

            if (visite != DBNull.Value)
            {
                SqlCommand commande_effacer = new SqlCommand("DELETE FROM PPClients WHERE NoClient = " + e.CommandArgument, myConnection);
                commande_effacer.ExecuteNonQuery();
            }
            else
            {
                using (myConnection)
                {
                    SqlTransaction transaction;
                    transaction = myConnection.BeginTransaction();

                    try
                    {
                        SqlCommand commande_existance_table = new SqlCommand("SELECT * from sysobjects where name='HistoCommandes' and Xtype='T' ", myConnection, transaction);
                        SqlDataReader rd1 = commande_existance_table.ExecuteReader();

                        if (!rd1.Read())
                        {
                            SqlCommand commande_creer_table = new SqlCommand("SELECT * INTO HistoCommandes FROM PPCommandes WHERE 0 = 1 ", myConnection, transaction);
                            commande_creer_table.ExecuteNonQuery();
                        }

                        SqlCommand commande_existance_table2 = new SqlCommand("SELECT * from sysobjects where name='HistoDetailsCommandes' and Xtype='T' ", myConnection, transaction);
                        SqlDataReader rd2 = commande_existance_table2.ExecuteReader();

                        if (!rd2.Read())
                        {
                            SqlCommand commande_creer_table = new SqlCommand("SELECT * INTO HistoDetailsCommandes FROM PPDetailsCommandes WHERE 0 = 1 ", myConnection, transaction);
                            commande_creer_table.ExecuteNonQuery();
                        }

                        SqlCommand commande_deplacer_details = new SqlCommand("INSERT INTO HistoDetailsCommandes SELECT * FROM PPDetailsCommandes WHERE NoCommande IN (SELECT NoCommande FROM PPCommandes WHERE NoClient = " + e.CommandArgument + " ) ", myConnection, transaction);
                        commande_deplacer_details.ExecuteNonQuery();
                        SqlCommand commande_effacer_details = new SqlCommand("DELETE FROM PPDetailsCommandes WHERE NoClient = " + e.CommandArgument, myConnection, transaction);
                        commande_effacer_details.ExecuteNonQuery();

                        SqlCommand commande_deplacer_commande = new SqlCommand("INSERT INTO HistoCommandes SELECT * FROM PPCommandes WHERE NoClient = " + e.CommandArgument, myConnection, transaction);
                        commande_deplacer_commande.ExecuteNonQuery();
                        SqlCommand commande_effacer_commande = new SqlCommand("DELETE FROM PPCommandes WHERE NoClient = " + e.CommandArgument, myConnection, transaction);
                        commande_effacer_commande.ExecuteNonQuery();

                        transaction.Commit();
                        Session["msg"] = "Le client " + titre_demande.Text + " a bien été désactivé.";
                        Response.Redirect("gerer_inactivite_clients.aspx");
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        Session["err_msg"] = "Erreur lors de la mise à jour de la base de données : " + ex.ToString();
                        Response.Redirect("gerer_inactivite_clients.aspx");
                    }
                }
            }

            myConnection.Close();
        }

        protected void desactiver_liste_client(object sender, CommandEventArgs e)
        {
            List<String> tab_desactiver = new List<String>();
            //tab_desactiver = liste_a_desactiver.Split(',');
            tab_desactiver.AddRange(Session["desactiver_liste"].ToString().Split(','));

            foreach (string client_a_desctiver in tab_desactiver)
                desactiver_un_client(Convert.ToInt32(client_a_desctiver));

            Session["msg"] = "Les clients sélectionnés ont bien été désactivés.";
            Session["desactiver_liste"] = "";
            Response.Redirect("gerer_inactivite_clients.aspx");
        }

        public void desactiver_un_client(int client_a_desactiver)
        {
            myConnection.Open();
            SqlCommand commande_verifier_visite = new SqlCommand("SELECT NoClient FROM PPVendeursClients WHERE NoClient = " + client_a_desactiver, myConnection);
            object visite = commande_verifier_visite.ExecuteScalar();

            if (visite != DBNull.Value)
            {
                SqlCommand commande_effacer = new SqlCommand("DELETE FROM PPClients WHERE NoClient = " + client_a_desactiver, myConnection);
                commande_effacer.ExecuteNonQuery();
            }
            else
            {
                using (myConnection)
                {
                    SqlTransaction transaction;
                    transaction = myConnection.BeginTransaction();

                    try
                    {
                        SqlCommand commande_existance_table = new SqlCommand("SELECT * from sysobjects where name='HistoCommandes' and Xtype='T' ", myConnection, transaction);
                        SqlDataReader rd1 = commande_existance_table.ExecuteReader();

                        if (!rd1.Read())
                        {
                            SqlCommand commande_creer_table = new SqlCommand("SELECT * INTO HistoCommandes FROM PPCommandes WHERE 0 = 1 ", myConnection, transaction);
                            commande_creer_table.ExecuteNonQuery();
                        }

                        SqlCommand commande_existance_table2 = new SqlCommand("SELECT * from sysobjects where name='HistoDetailsCommandes' and Xtype='T' ", myConnection, transaction);
                        SqlDataReader rd2 = commande_existance_table2.ExecuteReader();

                        if (!rd2.Read())
                        {
                            SqlCommand commande_creer_table = new SqlCommand("SELECT * INTO HistoDetailsCommandes FROM PPDetailsCommandes WHERE 0 = 1 ", myConnection, transaction);
                            commande_creer_table.ExecuteNonQuery();
                        }

                        SqlCommand commande_deplacer_details = new SqlCommand("INSERT INTO HistoDetailsCommandes SELECT * FROM PPDetailsCommandes WHERE NoCommande IN (SELECT NoCommande FROM PPCommandes WHERE NoClient = " + client_a_desactiver + " ) ", myConnection, transaction);
                        commande_deplacer_details.ExecuteNonQuery();
                        SqlCommand commande_effacer_details = new SqlCommand("DELETE FROM PPDetailsCommandes WHERE NoClient = " + client_a_desactiver, myConnection, transaction);
                        commande_effacer_details.ExecuteNonQuery();

                        SqlCommand commande_deplacer_commande = new SqlCommand("INSERT INTO HistoCommandes SELECT * FROM PPCommandes WHERE NoClient = " + client_a_desactiver, myConnection, transaction);
                        commande_deplacer_commande.ExecuteNonQuery();
                        SqlCommand commande_effacer_commande = new SqlCommand("DELETE FROM PPCommandes WHERE NoClient = " + client_a_desactiver, myConnection, transaction);
                        commande_effacer_commande.ExecuteNonQuery();

                        transaction.Commit();
                        Session["msg"] = "Le client " + titre_demande.Text + " a bien été désactivé.";
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        Session["err_msg"] = "Erreur lors de la mise à jour de la base de données pour le client: " + client_a_desactiver + ". Message: " + ex.ToString();
                        Response.Redirect("gerer_inactivite_clients.aspx");
                    }
                }
            }

            myConnection.Close();
        }

        private DataTable charge_liste()
        {
            string req_liste = "SELECT * FROM PPclients WHERE Noclient IN ( " + liste_a_desactiver + " ) ";

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
                item_a_desactiver.Text = drvinactif1["Prenom"].ToString() + " " + drvinactif1["Nom"].ToString();
            }
        }
    }
}