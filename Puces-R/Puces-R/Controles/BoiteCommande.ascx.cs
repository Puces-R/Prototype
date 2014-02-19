using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R.Controles
{
    public partial class BoiteCommande : System.Web.UI.UserControl
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");


        public String Titre
        {
           
            set
            {
                hypNomClient.Visible = true;
                lblVendeur.Visible = false;
                lblDate.Visible = false;
                hypNomClient.Text = value.ToString();
                
            }
        }

        public String URL
        {
           
            set
            {
                hypNoCommande.Visible = true;
                lblNoCommande.Visible = false;
                hypNoCommande.Text=value.ToString();
                hypNoCommande.NavigateUrl = "~/DetailsCommandes.aspx?noCommande=" + value.ToString();
            }
        }


        public long NoCommande
        {
            get
            {
                return ctrMontantsFactures.NoCommande;
            }
            set
            {
                ctrMontantsFactures.NoCommande = value;
            }
        }

        public long NoClient
        {
          
            

            set
            {

                btnChanger.Visible = true;
                lblClient.Visible = true;
                lblNoClient.Visible = true;
                lblNoClient.Text = value.ToString();
                hypNomClient.NavigateUrl = "~/CommuniquerClient.aspx?noClient=" + value;
                lblNoClient.NavigateUrl = "~/CommuniquerClient.aspx?noClient=" + value;
            }
        }

        public bool SetBoutton
        {
            set
            {

                btnChanger.Visible = true;
                lblClient.Visible = true;
                lblNoClient.Visible = true;
                
            }
        }

        protected void ChangerStatut(object sender, EventArgs e) 
        {
            SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
           
            String statut = ((String) btnChanger.CommandArgument);

            myConnection.Open();
            SqlCommand maC = new SqlCommand("Select Statut from PPCommandes where NoCommande=" + statut, myConnection);
            object nb = (object)maC.ExecuteScalar();

            String statutCommande = nb.ToString();

            //Response.Write(noC + "----" + stat);


            if (statutCommande == "l")
            {
                SqlCommand commandeMAJQuantite = new SqlCommand("UPDATE PPCommandes SET Statut ='p' WHERE NoCommande = " + statut, myConnection);
                commandeMAJQuantite.ExecuteNonQuery();
            }
            else if (statutCommande == "p")
            {

                SqlCommand commandeMAJQuantite = new SqlCommand("UPDATE PPCommandes SET Statut ='l' WHERE NoCommande = " + statut, myConnection);
                commandeMAJQuantite.ExecuteNonQuery();
            }

            else
            {
                //Response.Write("ALLO");
            }
           
            myConnection.Close();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnLoad(e);

            myConnection.Open();

            SqlCommand commandeCommande = new SqlCommand("SELECT TOP(1) V.NomAffaires, C.NoCommande, C.DateCommande, C.NoAutorisation, C.TypeLivraison, C.Statut FROM PPCommandes C INNER JOIN PPVendeurs V ON C.NoVendeur = V.NoVendeur WHERE C.NoCommande = " + NoCommande + " ORDER BY DateCommande DESC", myConnection);
            SqlDataReader lecteurCommande = commandeCommande.ExecuteReader();

            lecteurCommande.Read();

            lblVendeur.Text = (String)lecteurCommande["NomAffaires"];
            lblNoCommande.Text = ((long)lecteurCommande["NoCommande"]).ToString();
            lblDate.Text = "(" + ((DateTime)lecteurCommande["DateCommande"]).ToString() + ")";

            switch ((String)lecteurCommande["Statut"])
            {
                case "p":
                    lblStatut.Text = "Prêt à livrer";
                    break;
                case "l":
                    lblStatut.Text = "Livré";
                    break;
            }

        
            lblNoAutorisation.Text = (String)lecteurCommande["NoAutorisation"];
            ctrMontantsFactures.NoCommande = (long)lecteurCommande["NoCommande"];
            ctrMontantsFactures.CodeLivraison = (short)lecteurCommande["TypeLivraison"];
            btnChanger.CommandArgument = Convert.ToString((long)lecteurCommande["NoCommande"]);
            myConnection.Close();
        }
    }
}