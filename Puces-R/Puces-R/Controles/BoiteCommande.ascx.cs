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

            myConnection.Close();
        }
    }
}