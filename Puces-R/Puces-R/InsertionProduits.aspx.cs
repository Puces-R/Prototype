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
    public partial class InsertionProduits : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                chargerCategorie();
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

        public int avoirCategorie(String categorie)
        {
            int noCat = 0;
            switch (categorie) 
            {
                case "Articles de maison": noCat=10; break;
                case "Mobiliers": noCat = 20; break;
                case "Articles électroniques": noCat = 30; break;
                case "Articles de bébé et enfants": noCat = 40; break;
                case "Rénovation, bricolage, loisirs": noCat = 50; break;
                case "Vêtements et articles de sports": noCat = 60; break;
                case "Articles de musique": noCat = 70; break;
                case "Varia": noCat = 80; break;
            }
            return noCat;

        }

        public void validationSaisie(Object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
               insertionProduits( sender,  e);
            }
            else
            {
                Response.Write("Il y a eu un problème lors de l'insertion. Veuillez corriger les erreurs!");
            }


        }

        public void insertionProduits(Object sender, EventArgs e)
        {
            SqlConnection dbConn = new SqlConnection();
            String maChaineDeConnexion = "Data Source=sqlinfo.cgodin.qc.ca;Initial Catalog=BD6B8_424R;Persist Security Info=True;User ID=6B8equipe424r;Password=Password2";
            SqlConnection maConnexion = new SqlConnection();
            maConnexion.ConnectionString = maChaineDeConnexion;
            maConnexion.Open();
            int cat = avoirCategorie(ddlCategorieProduits.SelectedItem.Text);
            Response.Write(cat.ToString());

            int grand = -999;
            SqlCommand maCommande = new SqlCommand("select * from PPProduits where NoVendeur=10", maConnexion);

            SqlDataReader rep = maCommande.ExecuteReader();
            while (rep.Read())
            {
                Int64 no = (Int64)rep[0];
                String numero= Convert.ToString(no).Substring(2);

                int noprod = Convert.ToInt32(numero);

                if(noprod>grand)
                {
                    grand = noprod;
                }

            }
            rep.Close();
            
            grand++;
            Response.Write(grand.ToString());

            String total = "";
            String nb0 = "";
            total = total + Convert.ToString(grand);
            for (int i = 0; total.Length < 5;i++ )
            {
                nb0 += "0";
                total = nb0 + total;
                
            }

            Response.Write(total);
            String noProduit = "10" + total;
            Int64 numProduit=Convert.ToInt64(noProduit);


            SqlCommand maCommande1 = new SqlCommand("INSERT INTO PPProduits VALUES(" + numProduit + ",10," + cat + ",'" + tbDescAbregee.Text + "','" + tbDescComplete.Text + "','1000010.jpg'," + tbPrix.Text + "," + tbNbItems.Text + ",1,NULL," + tbPrix.Text + "," + tbPois.Text + "," + "'2007-05-02'," + "NULL)", maConnexion);
             maCommande1.ExecuteNonQuery();
            //Response.Write("VALIDATION EST EFFICACE");
            maConnexion.Close();
        }

    }
}