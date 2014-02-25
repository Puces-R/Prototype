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
    public partial class stats_desactiver_client : System.Web.UI.Page
    {
        SqlConnection myConnection = Librairie.Connexion;
        int no_client, nb_liens;
        string liste_a_desactiver;
        string[] liste_nb_liens;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["msg"] != null)
                if (Session["msg"].ToString() != "")
                {
                    div_msg.InnerText = Session["msg"].ToString();
                    Session["msg"] = null;
                }

            if (Session["err_msg"] != null)
                if (Session["err_msg"].ToString() != "")
                {
                    
                    Session["err_msg"] = null;
                }

            if (!IsPostBack)
            {
                Librairie.Autorisation(false, false, false, true);
                if (Session["desactiver_client"] != null)
                {
                    if (Session["desactiver_client"].ToString() != "")
                    {
                        ((SiteMaster)Master).Titre = "Détails du client désactivé";
                        no_client = Convert.ToInt32(Session["desactiver_client"].ToString());
                        nb_liens = Convert.ToInt32(Session["nb_liens"].ToString());
                        mv_verdict.SetActiveView(view_un_client);
                        //Session["desactiver_client"] = null;
                    }
                }
                else
                {
                    if (Session["desactiver_liste"] != null)
                    {
                        if (Session["desactiver_liste"].ToString() != "")
                        {
                            ((SiteMaster)Master).Titre = "Détails des clients désactivés";
                            liste_a_desactiver = Session["desactiver_liste"].ToString();
                            liste_nb_liens = Session["liste_nb_liens"].ToString().Split(',');
                            charge_liste();
                            mv_verdict.SetActiveView(view_liste);
                        }
                    }
                    else Librairie.RefuserAutorisation();
                }
            }

            myConnection.Open();
            SqlCommand charger = new SqlCommand("SELECT * FROM PPClients WHERE Noclient = " + no_client, myConnection);

            SqlDataReader results = charger.ExecuteReader();            

            if (results.Read())
            {
                titre_demande.Text = results["Prenom"].ToString() + " " + results["Nom"].ToString();
                addr_demande.Text = results["Rue"].ToString() + ", " + results["Ville"].ToString() + ", " + results["Pays"].ToString();
                tels_demande.Text = (results["Tel1"] != DBNull.Value ? Telephone.Format(results["Tel1"].ToString()) : "") + (results["Tel2"] != DBNull.Value ? ", " + Telephone.Format(results["Tel2"].ToString()) : "");
                courriel_demande.Text = results["AdresseEmail"].ToString();                
                nb_connexions.Text = results["NbConnexions"].ToString();
                date_demande.Text = results["DateCreation"].ToString();
                lbl_nb_lien.Text = nb_liens.ToString();
                btn_voir_plus.CommandArgument = results["NoClient"].ToString();
            }

            myConnection.Close();
        }  

        private DataTable charge_liste()
        {
            string req_liste = "SELECT * FROM PPClients WHERE Noclient IN ( " + liste_a_desactiver + " ) ";

            SqlDataAdapter adapteurInnactif1 = new SqlDataAdapter(req_liste, myConnection);
            DataTable tableInnactif1 = new DataTable();
            adapteurInnactif1.Fill(tableInnactif1);
            //

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
                LinkButton lbl_client = (LinkButton)item.FindControl("lbl_client");

                DataRowView drvinactif1 = (DataRowView)e.Item.DataItem;

                lbl_client.Text = (e.Item.ItemIndex + 1).ToString() + " - " + drvinactif1["Prenom"].ToString() + " " + 
                    drvinactif1["Nom"].ToString() + ", " + drvinactif1["AdresseEmail"].ToString();
                lbl_client.CommandArgument = drvinactif1["NoClient"].ToString();
            }
        }

        protected void voir_plus(object sender, CommandEventArgs e)
        {
            Session["client_desactive"] = e.CommandArgument.ToString();
            Response.Redirect(Chemin.Ajouter("details_redevance.aspx", "Retour aux statistiques de la desactivation"));  
        }
    }
}