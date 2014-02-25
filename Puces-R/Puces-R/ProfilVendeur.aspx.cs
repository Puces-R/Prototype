using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
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
                Librairie.Autorisation(false, false, true, false);
                hl.NavigateUrl = Chemin.Ajouter(hl.NavigateUrl, "Retour au profil");
                lireXML();

                SqlCommand commandeClient = new SqlCommand("SELECT AdresseEmail, NomAffaires, Prenom, Nom, Rue, Ville, Province, Pays, CodePostal, Tel1, Tel2, MaxLivraison, LivraisonGratuite, DateMAJ, Pourcentage, Taxes FROM PPVendeurs WHERE NoVendeur = @no", myConnection);
                commandeClient.Parameters.AddWithValue("@no", Session["ID"]);

                myConnection.Open();
                SqlDataReader lecteurClient = commandeClient.ExecuteReader();
                lecteurClient.Read();

                lblCourriel.Text = lecteurClient["AdresseEmail"].ToString().ToLower();
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
                ctrProfil.PoidsMaximum = Convert.ToDecimal(lecteurClient["MaxLivraison"]);
                ctrProfil.LivraisonGratuite = (Decimal)lecteurClient["LivraisonGratuite"];
                lblMAJ.Text = lecteurClient["DateMAJ"] == DBNull.Value ? "Jamais" : Convert.ToString((DateTime)lecteurClient["DateMAJ"]);
                if (lecteurClient["Pourcentage"] is DBNull)
                {
                    lblTaux.Text = "Votre profil n'a pas encore été accepté";
                }
                else
                {
                    lblTaux.Text = Convert.ToString((Decimal)lecteurClient["Pourcentage"] * 100) + " %";
                }
                ctrProfil.Taxes = (Boolean)lecteurClient["Taxes"];

                myConnection.Close();
            }

            // ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "DefinirCouleur('0020A3');", true);
        }



        protected void lireXML()
        {

            if (File.Exists(MapPath("XML/" + Session["ID"].ToString() + ".xml")))
            {
                XmlTextReader xmlEnLecture = new XmlTextReader(MapPath("XML/" + Session["ID"].ToString() + ".xml"));
                xmlEnLecture.WhitespaceHandling = WhitespaceHandling.None;
                String nomLogo = "";
                String noCouleur = "";

                while (xmlEnLecture.Read())
                {
                    switch (xmlEnLecture.NodeType)
                    {

                        case XmlNodeType.Element:

                            String nom = xmlEnLecture.Name;
                            switch (nom)
                            {
                                case "couleur":
                                    if (xmlEnLecture.HasAttributes)
                                    {
                                        xmlEnLecture.MoveToFirstAttribute();
                                        do
                                        {
                                            switch (xmlEnLecture.Name)
                                            {
                                                case "Valeur": noCouleur = xmlEnLecture.Value; break;

                                            }
                                        }
                                        while (xmlEnLecture.MoveToNextAttribute());
                                    }
                                    break;

                                case "logo":
                                    if (xmlEnLecture.HasAttributes)
                                    {
                                        xmlEnLecture.MoveToFirstAttribute();
                                        do
                                        {
                                            switch (xmlEnLecture.Name)
                                            {

                                                case "ImageURL": nomLogo = xmlEnLecture.Value; ; break;
                                            }
                                        }
                                        while (xmlEnLecture.MoveToNextAttribute());
                                    }
                                    break;



                            }
                            break;

                    }
                }
                xmlEnLecture.Close();


                hidColor.Value = noCouleur;
                hidImage.Value = nomLogo;

            }
            else
            {
                
            }
        }

        protected void sauverFavori(object sender, EventArgs e)
        {
          
            String image = "";

            if (fileUploaderLogo.HasFile)
            {
                try
                {

                    image = Path.GetFileName(fileUploaderLogo.FileName);
                    string[] split = image.Split('.');
                    fileUploaderLogo.SaveAs(MapPath("Images/Logo/" + Session["ID"] + "." + split[1]));
                    image = Session["ID"] + "." + split[1];
                    //hidImage.Value = image;
                }
                catch (Exception ex)
                {
                    
                }
            }

            CustomStyleImage.Validate();
            if (CustomStyleImage.IsValid)
            {
                if (image == "")
                {
                    ecrireFichierXML(hidColor.Value, hidImage.Value);
                }
                else
                {
                    ecrireFichierXML(hidColor.Value, image);
                }
            }
            Response.Redirect("AccueilVendeur.aspx");

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

            fEcrit.WriteLine("<?xml version=\"" + "1.0" + "\" encoding=\"" + "utf-8" + "\"?>");
            fEcrit.WriteLine("<Configuration>");
            fEcrit.WriteLine("<couleur Valeur=\"" + value + "\"> </couleur>");
            fEcrit.WriteLine("<logo ImageURL=\"" + image + "\"> </logo>");
            fEcrit.WriteLine("</Configuration>");
            fEcrit.Close();

            myConnection.Open();
            SqlCommand maC = new SqlCommand("UPDATE PPVENDEURS SET Configuration=" + Session["ID"] +"where NoVendeur="+Session["ID"], myConnection);
            maC.ExecuteNonQuery();
            myConnection.Close();

        }

        protected void verifierFormat(object sender, ServerValidateEventArgs e)
        {
            if (fileUploaderLogo.HasFile)
            {
                try
                {
                    String filename = Path.GetFileName(fileUploaderLogo.FileName);
                    string[] split = filename.Split('.');
                    e.IsValid = (split[1] == "jpg" || split[1] == "png" || split[1] == "gif" || split[1] == "jpeg");
                }
                catch (Exception ex)
                {
                    
                }
            }
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

                if (Chemin.UrlRetour == null)
                {
                    Response.Redirect("AccueilVendeur.aspx");
                }
                else
                {
                    Response.Redirect(Chemin.UrlRetour);
                }
            }
        }
    }
}