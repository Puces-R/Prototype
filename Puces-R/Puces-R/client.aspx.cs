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
    public partial class client : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
        int no_client;

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.Titre = "Gérer le client";

            if (Session["selected_client"] != null)
            {
                if (Session["selected_client"].ToString() != "")
                    no_client = Convert.ToInt32(Session["selected_client"].ToString());
            }
            //else Response.Redirect("connexion.aspx");

            charger_info();
            mvVendeur.SetActiveView(View1);
        }


        protected void changer_view(object sender, CommandEventArgs e)
        {
            if (e != null)
            {
                switch (Convert.ToInt32(e.CommandArgument.ToString()))
                {
                    //case 1:
                    //    generer_stat();
                    //    mvVendeur.SetActiveView(View2);
                    //    break;
                    //case 2:
                    //    mvVendeur.SetActiveView(View3);
                    //    break;
                    default:
                        charger_info();
                        mvVendeur.SetActiveView(View1);
                        break;
                }
            }
            else
            {
                charger_info();
                mvVendeur.SetActiveView(View1);
            }
        }
        
        protected void charger_info()
        {
            myConnection.Open();
            SqlCommand charger = new SqlCommand("SELECT * FROM PPClients WHERE NoClient = " + no_client, myConnection);

            SqlDataReader results = charger.ExecuteReader();

            if (results.Read())
            {
                lbl_nom_complet.Text = results["Prenom"].ToString() + " " + results["Nom"].ToString();
                lbl_adresse.Text = results["Rue"].ToString() + ", " + results["Ville"].ToString() + ", " + results["Pays"].ToString();
                lbl_nb_connexion.Text = results["NbConnexions"].ToString();
                lbl_courriel.Text = results["AdresseEmail"].ToString();
                lbl_date_insc.Text = results["DateCreation"].ToString();
                lbl_date_maj.Text = results["DateMAJ"].ToString();
                
                switch (results["Statut"].ToString())
                {
                    case "0":
                        lbl_statut.Text = "Actif";
                        break;
                    case "1":
                        lbl_statut.Text = "Inactif";
                        break;
                    case "2":
                        lbl_statut.Text = "En attende d'approbation";
                        break;
                    case "3":
                        lbl_statut.Text = "En retard de paiement";
                        break;
                }

                lbl_tel1.Text = results["Tel1"].ToString();
                lbl_tel2.Text = results["Tel2"].ToString();
            }
            myConnection.Close();
        }
          
   }
}