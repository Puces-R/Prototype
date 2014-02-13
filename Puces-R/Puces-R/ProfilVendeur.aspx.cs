using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class ProfilVendeur : System.Web.UI.Page
    {
        SqlConnection myConnection = Librairie.Connexion;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ID"] == null)
                {
                    Response.Redirect("Default.aspx", true);
                }

                String whereClause = " WHERE NoVendeur = " + Session["ID"];

                SqlCommand commandeClient = new SqlCommand("SELECT * FROM PPVendeurs" + whereClause, myConnection);

                myConnection.Open();

                SqlDataReader lecteurClient = commandeClient.ExecuteReader();
                lecteurClient.Read();

                ctrProfil.NomAffaires = (String)lecteurClient["NomAffaires"];
                ctrProfil.Prenom = (String)lecteurClient["Prenom"];
                ctrProfil.Nom = (String)lecteurClient["Nom"];
                ctrProfil.Adresse = (String)lecteurClient["Rue"];
                ctrProfil.Ville = (String)lecteurClient["Ville"];
                ctrProfil.Province = (String)lecteurClient["Province"];
                ctrProfil.Pays = (String)lecteurClient["Pays"];
                ctrProfil.CodePostal = (String)lecteurClient["CodePostal"];
                ctrProfil.Tel1 = (String)lecteurClient["Tel1"];
                ctrProfil.Tel2 = lecteurClient["Tel2"] == DBNull.Value ? null : (String)lecteurClient["Tel2"];
                ctrProfil.PoidsMaximum = (int)lecteurClient["MaxLivraison"];
                ctrProfil.LivraisonGratuite = (Decimal)lecteurClient["LivraisonGratuite"];
                lblMAJ.Text = lecteurClient["DateMAJ"] == DBNull.Value ? "Jamais" : Convert.ToString((DateTime)lecteurClient["DateMAJ"]);
                lblTaux.Text = Convert.ToString((Decimal)lecteurClient["Pourcentage"] * 100) + " %";
                ctrProfil.Taxes = (Boolean)lecteurClient["Taxes"];
                /*
                this.ctrTelephone.NoTelephone = (String)lecteurClient["Tel1"]; 

                Object noCellulaire = lecteurClient["Tel2"];
                if (!(noCellulaire is DBNull))
                {
                    this.ctrCellulaire.NoTelephone = (String)noCellulaire;
                }
                */
                myConnection.Close();
            }

            //Response.Write("salut");
            Session["couleur"] = "0020A3";
            string str = "<script type=\"text/javascript\">$(" + "'#jqxColorPicker" + "').jqxColorPicker('setColor', '#0020A3');</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", str);

            // ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "DefinirCouleur('0020A3');", true);
        }

        protected void sauverFavori(object sender, EventArgs e)
        {
            Response.Write(hidColor.Value);
            String image = "";

            if (fileUploaderLogo.HasFile)
            {
                try
                {

                    image = Path.GetFileName(fileUploaderLogo.FileName);

                    string[] split = image.Split('.');
                    // String nom[] = filename.Split('.');
                    //string ext = System.IO.Path.GetExtension(this.File1.PostedFile.FileName);
                    // Response.Write(uplNomFichier.PostedFile.ContentType);
                    //SqlCommand maC = new SqlCommand("select Photo from PPProduits where NoVendeur="+Session["ID"]);
                    fileUploaderLogo.SaveAs(MapPath("Images/Logo/" + Session["ID"] + "." + split[1]));
                    image = Session["ID"] + "." + split[1];

                    //StatusLabel.Text = "Upload status: File uploaded!";

                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                    //StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }

            ecrireFichierXML(hidColor.Value, image);
        }

        protected void ecrireFichierXML(String value, String image)
        {

            StreamWriter fEcrit; StreamReader fLit;
            String strNomFichier;
            String strLigneLue;
            Int16 i;

            if (image == "")
            {
                image = "logo.png";
            }
            strNomFichier = Server.MapPath("XML/" + Session["ID"] + ".xml");

            /* Création du fichier texte */
            fEcrit = new StreamWriter(strNomFichier);


            /*<?xml version="1.0" encoding="utf-8" ?>
 <college>
   <departement no="101" nom="Biologie">*/

            fEcrit.WriteLine(" <?xml version=\"" + "1.0" + "\" encoding=\"" + "utf-8" + "\"?>");
            fEcrit.WriteLine("<couleur Valeur=\"" + value + "\"> </couleur>");
            fEcrit.WriteLine("<logo ImageURL=\"" + image + "\"> </logo>");
            fEcrit.Close();

        }

        protected void sauvegarder(object sender, EventArgs e)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                SqlCommand cmdMAJ = new SqlCommand();
                cmdMAJ.Connection = myConnection;
                cmdMAJ.CommandText = "UPDATE PPVendeurs " +
                                     "SET NomAffaires = @nomAffaires, Nom = @nom, Prenom = @prenom, " +
                                         "Rue = @rue, Ville = @ville, Province = @province, " +
                                         "CodePostal = @codePostal, Pays = @pays, Tel1 = @tel1, " +
                                         "Tel2 = @tel2, MaxLivraison = @poids, LivraisonGratuite = @gratuit, " +
                                         "Taxes = @taxes, DateMAJ = @date " +
                                     "WHERE NoVendeur = @no";

                cmdMAJ.Parameters.AddWithValue("@nomAffaires", ctrProfil.NomAffaires);
                cmdMAJ.Parameters.AddWithValue("@nom", ctrProfil.Nom);
                cmdMAJ.Parameters.AddWithValue("@prenom", ctrProfil.Prenom);

                cmdMAJ.Parameters.AddWithValue("@rue", ctrProfil.Adresse);
                cmdMAJ.Parameters.AddWithValue("@ville", ctrProfil.Ville);
                cmdMAJ.Parameters.AddWithValue("@province", ctrProfil.Province);

                cmdMAJ.Parameters.AddWithValue("@codePostal", ctrProfil.CodePostal);
                cmdMAJ.Parameters.AddWithValue("@pays", ctrProfil.Pays);
                cmdMAJ.Parameters.AddWithValue("@tel1", ctrProfil.Tel1);

                cmdMAJ.Parameters.AddWithValue("@tel2", ctrProfil.Tel2 == null ? DBNull.Value : (object)ctrProfil.Tel2);
                cmdMAJ.Parameters.AddWithValue("@poids", ctrProfil.PoidsMaximum);
                cmdMAJ.Parameters.AddWithValue("@gratuit", ctrProfil.LivraisonGratuite);

                cmdMAJ.Parameters.AddWithValue("@taxes", ctrProfil.Taxes);
                cmdMAJ.Parameters.AddWithValue("@date", DateTime.Now);
                cmdMAJ.Parameters.AddWithValue("@no", Session["ID"]);

                myConnection.Open();
                cmdMAJ.ExecuteNonQuery();
                myConnection.Close();

                Response.Redirect("AccueilVendeur.aspx");
            }
        }
    }
}