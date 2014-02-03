using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R.Controles
{
    public partial class ProfilClient : System.Web.UI.UserControl
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        public bool AfficherCourrielEtMotDePasse
        {
            set
            {
                phCourrielEtMotDePasse.Visible = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ID"] == null)
                {
                    Response.Redirect("Default.aspx", true);
                }

                String whereClause = " WHERE NoClient = " + Session["ID"];

                SqlCommand commandeClient = new SqlCommand("SELECT * FROM PPClients" + whereClause, myConnection);

                myConnection.Open();

                SqlDataReader lecteurClient = commandeClient.ExecuteReader();
                lecteurClient.Read();

                this.lblCourriel.Text = (String)lecteurClient["AdresseEmail"];
                this.txtPrenom.Text = (String)lecteurClient["Prenom"];
                this.txtNom.Text = (String)lecteurClient["Nom"];
                this.txtRue.Text = (String)lecteurClient["Rue"];
                this.txtVille.Text = (String)lecteurClient["Ville"];
                this.ctrProvince.CodeProvince = (String)lecteurClient["Province"];
                this.txtPays.Text = (String)lecteurClient["Pays"];
                this.ctrCodePostal.Code = (String)lecteurClient["CodePostal"];
                this.ctrTelephone.NoTelephone = (String)lecteurClient["Tel1"];

                Object noCellulaire = lecteurClient["Tel2"];
                if (!(noCellulaire is DBNull))
                {
                    this.ctrCellulaire.NoTelephone = (String)noCellulaire;
                }

                myConnection.Close();
            }
        }

        public void Sauvegarder()
        {
            Dictionary<String, String> dicPaires = new Dictionary<String, String>();

            dicPaires.Add("Prenom", this.txtPrenom.Text);
            dicPaires.Add("Nom", this.txtNom.Text);
            dicPaires.Add("Rue", this.txtRue.Text);
            dicPaires.Add("Ville", this.txtVille.Text);
            dicPaires.Add("Province", this.ctrProvince.CodeProvince);
            dicPaires.Add("Pays", this.txtPays.Text);
            dicPaires.Add("CodePostal", this.ctrCodePostal.Code);
            dicPaires.Add("Tel1", this.ctrTelephone.NoTelephone);
            dicPaires.Add("Tel2", this.ctrCellulaire.NoTelephone);

            if (ctrMotDePasse.EstDeroule)
            {
                dicPaires.Add("MotDePasse", this.ctrMotDePasse.MotDePasse);
            }

            List<String> tabPaires = new List<String>();
            foreach (String clee in dicPaires.Keys)
            {
                tabPaires.Add(clee + " = '" + dicPaires[clee] + "'");
            }
            String strPaires = String.Join(", ", tabPaires);

            SqlCommand commandeClient = new SqlCommand("UPDATE PPClients SET " + strPaires +" WHERE NoClient = " + Session["ID"], myConnection);

            myConnection.Open();
            commandeClient.ExecuteNonQuery();
            myConnection.Close();
        }
    }
}