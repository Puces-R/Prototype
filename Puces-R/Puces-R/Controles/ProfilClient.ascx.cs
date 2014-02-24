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
        SqlConnection myConnection = Librairie.Connexion;

        public string Province
        {
            get
            {
                return ctrProvince.CodeProvince;
            }
        }

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
                Librairie.Autorisation(false, true, false, false);
                
                String whereClause = " WHERE NoClient = " + Session["ID"];

                SqlCommand commandeClient = new SqlCommand("SELECT * FROM PPClients" + whereClause, myConnection);

                myConnection.Open();

                SqlDataReader lecteurClient = commandeClient.ExecuteReader();
                lecteurClient.Read();

                this.lblCourriel.Text = (String)lecteurClient["AdresseEmail"];
                this.txtPrenom.Text = lecteurClient["Prenom"] is DBNull ? "" : (String)lecteurClient["Prenom"];
                this.txtNom.Text = lecteurClient["Nom"] is DBNull ? "" : (String)lecteurClient["Nom"];
                this.txtRue.Text = lecteurClient["Rue"] is DBNull ? "" : (String)lecteurClient["Rue"];
                this.txtVille.Text = lecteurClient["Ville"] is DBNull ? "" : (String)lecteurClient["Ville"];
                this.ctrProvince.CodeProvince = lecteurClient["Province"] is DBNull ? "" : (String)lecteurClient["Province"];
                this.txtPays.Text = lecteurClient["Pays"] is DBNull ? "Canada" : (String)lecteurClient["Pays"];
                this.ctrCodePostal.Code = lecteurClient["CodePostal"] is DBNull ? "" : (String)lecteurClient["CodePostal"];
                this.ctrTelephone.NoTelephone = lecteurClient["Tel1"] is DBNull ? "" : (String)lecteurClient["Tel1"];
                this.ctrCellulaire.NoTelephone = lecteurClient["Tel2"] is DBNull ? "" : (String)lecteurClient["Tel2"];

                myConnection.Close();
            }
        }

        public void Sauvegarder()
        {
            SqlCommand cmdProfil = new SqlCommand(
                "UPDATE PPClients SET " +
                "Prenom = @prenom, " +
                "Nom = @nom, " +
                "Rue = @rue, " +
                "Ville = @ville, " +
                "Province = @province, " +
                "Pays = @pays, " +
                "CodePostal = @codepostal, " +
                "Tel1 = @tel1, " +
                "Tel2 = @tel2 " +
                "WHERE NoClient = @no"
                , myConnection);

            cmdProfil.Parameters.AddWithValue("@prenom", this.txtPrenom.Text);
            cmdProfil.Parameters.AddWithValue("@nom", this.txtNom.Text);
            cmdProfil.Parameters.AddWithValue("@rue", this.txtRue.Text);
            cmdProfil.Parameters.AddWithValue("@ville", this.txtVille.Text);
            cmdProfil.Parameters.AddWithValue("@province", this.ctrProvince.CodeProvince);
            cmdProfil.Parameters.AddWithValue("@pays", this.txtPays.Text);
            cmdProfil.Parameters.AddWithValue("@codepostal", this.ctrCodePostal.Code);
            cmdProfil.Parameters.AddWithValue("@tel1", this.ctrTelephone.NoTelephone);
            cmdProfil.Parameters.AddWithValue("@tel2", this.ctrCellulaire.NoTelephone == null ? DBNull.Value : (object)this.ctrCellulaire.NoTelephone);
            cmdProfil.Parameters.AddWithValue("@no", Session["ID"]);

            myConnection.Open();

            cmdProfil.ExecuteNonQuery();

            SqlCommand commandeDateMaj = new SqlCommand("UPDATE PPClients SET DateMAJ = @DateMAJ WHERE NoClient = " + Session["ID"], myConnection);
            commandeDateMaj.Parameters.AddWithValue("DateMAJ", DateTime.Now);
            commandeDateMaj.ExecuteNonQuery();

            myConnection.Close();
        }
    }
}