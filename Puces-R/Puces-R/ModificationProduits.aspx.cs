using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class ModificationProduits : System.Web.UI.Page
    {

        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
        int noProduit = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!int.TryParse(Request.Params["noproduit"], out noProduit))
                {
                    Response.Redirect("Default.aspx", true);
                }

                else
                {
                    String whereClause = " WHERE NoProduit = " + noProduit.ToString();

                    SqlCommand commandeClient = new SqlCommand("SELECT * FROM PPProduits" + whereClause, myConnection);


                    myConnection.Open();

                    SqlDataReader lecteurClient = commandeClient.ExecuteReader();
                    lecteurClient.Read();
                    chargerCategorie();

                    LoaderCategorie((int)lecteurClient["NoCategorie"]);
                    this.tbDescAbregée.Text = (String)lecteurClient["Nom"];
                    this.tbComplete.Text = (String)lecteurClient["Description"]; ;
                    this.tbPrixDemande.Text = Convert.ToString((Decimal)lecteurClient["PrixDemande"]);
                    this.tbNbItems.Text = ((Int16)lecteurClient["NombreItems"]).ToString(); ;
                    this.tbDateVente.Text = lecteurClient["DateVente"] == DBNull.Value ? "Date non disponible" : Convert.ToString((DateTime)lecteurClient["DateVente"]);
                    this.tbPrixVente.Text = Convert.ToString((Decimal)lecteurClient["PrixVente"]); ;
                    this.tbPoids.Text = Convert.ToString((Decimal)lecteurClient["Poids"]); ;
                    this.tbDateCreation.Text = lecteurClient["DateCreation"] == DBNull.Value ? "Date non disponible" : Convert.ToString((DateTime)lecteurClient["DateCreation"]);
                    this.tbMAJ.Text = lecteurClient["DateMAJ"] == DBNull.Value ? "Date non disponible" : Convert.ToString((DateTime)lecteurClient["DateMAJ"]); ;
                    Boolean b = (Boolean)lecteurClient["Disponibilité"];
                    if (b) 
                    {
                        cbDisponibilité.Checked = true;
                    }
                    
                    //this.tbNomAffaires.Text = (String)lecteurClient["NomAffaires"];
                    //this.txtPrenom.Text = (String)lecteurClient["Prenom"];
                    //this.txtNom.Text = (String)lecteurClient["Nom"];
                    //this.txtRue.Text = (String)lecteurClient["Rue"];
                    //this.txtVille.Text = (String)lecteurClient["Ville"];
                    //this.ctrProvince.CodeProvince = (String)lecteurClient["Province"];
                    //this.txtPays.Text = (String)lecteurClient["Pays"];
                    //this.ctrCodePostal.Code = (String)lecteurClient["CodePostal"];


                    myConnection.Close();
                }
            }
        }

        public void LoaderCategorie(int numero)
        {

            switch (numero)
            {
                case 10: ddlCategorieProduits.SelectedIndex = 1; break;
                case 20: ddlCategorieProduits.SelectedIndex = 2; break;
                case 30: ddlCategorieProduits.SelectedIndex = 3; break;
                case 40: ddlCategorieProduits.SelectedIndex = 4; break;
                case 50: ddlCategorieProduits.SelectedIndex = 5; break;
                case 60: ddlCategorieProduits.SelectedIndex = 6; break;
                case 70: ddlCategorieProduits.SelectedIndex = 7; break;
                case 80: ddlCategorieProduits.SelectedIndex = 8; break;
            }

        }

        public void chargerCategorie()
        {
            SqlConnection dbConn = new SqlConnection();
            String maChaineDeConnexion = "Data Source=sqlinfo.cgodin.qc.ca;Initial Catalog=BD6B8_424R;Persist Security Info=True;User ID=6B8equipe424r;Password=Password2";
            SqlConnection maConnexion = new SqlConnection();
            maConnexion.ConnectionString = maChaineDeConnexion;
            maConnexion.Open();

            SqlCommand maCommande = new SqlCommand("select * from PPCategories ", maConnexion);
            SqlDataReader rep = maCommande.ExecuteReader();

            ddlCategorieProduits.Items.Add("");
            while (rep.Read())
            {
                String nom = (String)rep[1];
                ddlCategorieProduits.Items.Add(nom);
            }
            maConnexion.Close();



        }
    }
}