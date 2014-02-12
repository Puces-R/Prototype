using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Puces_R
{
    public partial class ProfilVendeur : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                    image = Session["ID"] +  "." + split[1];
                    
                    //StatusLabel.Text = "Upload status: File uploaded!";

                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                    //StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
            
            ecrireFichierXML(hidColor.Value,image);
        }

        protected void ecrireFichierXML(String value,String image)
        {

            StreamWriter fEcrit; StreamReader fLit;
            String strNomFichier;
            String strLigneLue;
            Int16 i;

            if(image=="")
            {
                image = "logo.png";
            }
            strNomFichier = Server.MapPath("XML/"+Session["ID"] + ".xml");

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
    }
}