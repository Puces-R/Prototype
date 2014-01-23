using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class SuppressionProduits : System.Web.UI.Page
    {
        int noProduit = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!int.TryParse(Request.Params["noproduit"], out noProduit))
            {

                Response.Redirect("http://fr.wikipedia.org/wiki/Wikip%C3%A9dia:Accueil_principal");
            }
            else 
            {
                chargerCategorie();
                chargerDonnees();
                //Response.Write(noProduit.ToString());
            }
            
        }

        protected void chargerDonnees() 
        {
            SqlConnection dbConn = new SqlConnection();
            String maChaineDeConnexion = "Data Source=sqlinfo.cgodin.qc.ca;Initial Catalog=BD6B8_424R;Persist Security Info=True;User ID=6B8equipe424r;Password=Password2";
            SqlConnection maConnexion = new SqlConnection();
            maConnexion.ConnectionString = maChaineDeConnexion;
            maConnexion.Open();

            SqlCommand maCommande = new SqlCommand("select * from PPProduits where NoProduit="+ noProduit, maConnexion);
            object rep = maCommande.ExecuteScalar();

            if (rep != null)
            {

                SqlDataReader repT = maCommande.ExecuteReader();
                while (repT.Read())
                {
                    int categorie = (int)repT[2];
                    LoaderCategorie(categorie);

                   tbDescAbregee.Text = (String)repT[3];
                   tbDescComplete.Text = (String)repT[4];
                   String photo = (String)repT[5];
                   imgProduits.ImageUrl = "Images/Televerse/" + photo;
                   tbPrix.Text= Convert.ToString((Decimal)repT[6]);
                   tbNbItems.Text = Convert.ToString((Int16)repT[7]);
                    Boolean dispo = (Boolean)repT[8];
                    if (dispo)
                    {
                        cbDisponibilite.Checked=true;
                    }
                    else 
                    {
                        cbDisponibilite.Checked = false;
                    }

                    //Double prixV = (Decimal)repT[10];
                    tbPois.Text = Convert.ToString((Decimal)repT[11]);
                    //DateTime dateCreation = (DateTime)repT[12];
                    
                }
            }
            else
            {
                Response.Redirect("http://fr.wikipedia.org/wiki/Wikip%C3%A9dia:Accueil_principal");
            }
            maConnexion.Close();
        }

        public void LoaderCategorie(int numero)
        {
            
            switch (numero)
            {
                case 10: ddlCategorieProduits.SelectedIndex=1; break;
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