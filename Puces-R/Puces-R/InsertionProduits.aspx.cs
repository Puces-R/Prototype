using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Puces_R
{
    public partial class InsertionProduits : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Librairie.Autorisation(false, false, true, false);
                chargerCategorie();
            }
        }

        protected void validerPrixVente(object sender, ServerValidateEventArgs e)
        {
            if (tbPrix.Text == "" || rePrixDemande.IsValid == false)
            {
                e.IsValid = true;
            }
            else if (rePrixDemande.IsValid == true && rePrixVente.IsValid == true && (Convert.ToDecimal(tbPrixVente.Text.Replace('.', ',')) > Convert.ToDecimal(tbPrix.Text.Replace('.', ','))))
            {
                e.IsValid = false;
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


            while (rep.Read())
            {
                String nom = (String)rep[1];
                ddlCategorieProduits.Items.Add(nom);
            }
            maConnexion.Close();



        }

        protected void verifierFormat(object sender, ServerValidateEventArgs e) 
        {
            bool format = false;
            if (uplNomFichier.HasFile)
            {
                //Response.Write("HAS FILWE \n");
                try
                {

                    String filename = Path.GetFileName(uplNomFichier.FileName);
                    //uplNomFichier.SaveAs(MapPath("Images/Televerse/") + filename);
                    //Response.Write(filename);
                    string[] split = filename.Split('.');


                    if (split[1] == "jpg" || split[1] == "png" || split[1] == "gif" || split[1] == "jpeg")
                    {
                        format = true;
                        e.IsValid = true;
                    }
                    else
                    {
                        e.IsValid = false;
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                    //StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
            
                
           
        }

        protected string televerser(long noProduit)
        {
            string filename = "";
            if (uplNomFichier.HasFile)
            {
                //Response.Write("HAS FILWE \n");
                try
                {

                    filename = Path.GetFileName(uplNomFichier.FileName);
                    //uplNomFichier.SaveAs(MapPath("Images/Televerse/") + filename);
                    //Response.Write(filename);
                    string [] split = filename.Split('.');
                    
                   // String nom[] = filename.Split('.');
                    //string ext = System.IO.Path.GetExtension(this.File1.PostedFile.FileName);
                   // Response.Write(uplNomFichier.PostedFile.ContentType);
                    //SqlCommand maC = new SqlCommand("select Photo from PPProduits where NoVendeur="+Session["ID"]);
                    uplNomFichier.SaveAs(MapPath("Images/Televerse/"+Session["ID"]+noProduit.ToString()+"."+split[1]));
                    filename=Session["ID"]+noProduit.ToString()+"."+split[1];
                    Response.Write(filename);
                    //StatusLabel.Text = "Upload status: File uploaded!";

                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                    //StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }


            }
            return filename;
        }

        public int avoirCategorie(String categorie)
        {
            int noCat = 0;
            switch (categorie)
            {
                case "Articles de maison": noCat = 10; break;
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
                insertionProduits(sender, e);
            }
            else
            {
                //Response.Write("Il y a eu un problème lors de l'insertion. Veuillez corriger les erreurs!");
                lblAvertissement.Text = "Il y a eu un problème lors de l'insertion. Veuillez corriger les erreurs!";
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
            //Response.Write(cat.ToString());

            int grand = -999;
            SqlCommand maCommande = new SqlCommand("select * from PPProduits where NoVendeur="+Session["ID"], maConnexion);

            SqlDataReader rep = maCommande.ExecuteReader();
            while (rep.Read())
            {
                Int64 no = (Int64)rep[0];
                String numero = Convert.ToString(no).Substring(2);

                int noprod = Convert.ToInt32(numero);

                if (noprod > grand)
                {
                    grand = noprod;
                }

            }
            rep.Close();

            grand++;
            //Response.Write(grand.ToString());

            String total = "";
            String nb0 = "";
            total = total + Convert.ToString(grand);
            for (int i = 0; total.Length < 5; i++)
            {
                nb0 += "0";
                total = nb0 + total;

            }

           // Response.Write(total);
            String noProduit = Session["ID"] + total;
            Int64 numProduit = Convert.ToInt64(noProduit);
            String nomImage = televerser(numProduit);
            DateTime date = DateTime.Now;
            String disponibilite = "";
            if (cbDisponibilite.Checked == true)
            {
                disponibilite = "1";
            }
            else 
            {
                disponibilite = "0";
            }

            SqlCommand maCommande1 = new SqlCommand("INSERT INTO PPProduits VALUES(" + numProduit + ","+Session["ID"]+"," + cat + ",'" + tbDescAbregee.Text + "','" + tbDescComplete.Text + "','"+nomImage+"'," + tbPrix.Text + "," + tbNbItems.Text + ","+disponibilite+",NULL," + (tbPrixVente.Text=="" ?"NULL":tbPrixVente.Text ) + "," + tbPois.Text + "," + "'"+date.ToShortDateString()+"'," + "NULL)", maConnexion);
            maCommande1.ExecuteNonQuery();
            maConnexion.Close();
            //Response.Redirect("GestionProduits.aspx");
        }

    }
}