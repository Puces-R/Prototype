using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;


namespace Puces_R.Controles
{
    public partial class ProfilVendeur : System.Web.UI.UserControl
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

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

                this.tbNomAffaires.Text = (String)lecteurClient["NomAffaires"];
                this.txtPrenom.Text = (String)lecteurClient["Prenom"];
                this.txtNom.Text = (String)lecteurClient["Nom"];
                //this.txtRue.Text = (String)lecteurClient["Rue"];
                Adresse.NoAdresse = (String)lecteurClient["Rue"];
                this.txtVille.Text = (String)lecteurClient["Ville"];
                this.ctrProvince.CodeProvince = (String)lecteurClient["Province"];
                this.txtPays.Text = (String)lecteurClient["Pays"];
                this.ctrCodePostal.Code = (String)lecteurClient["CodePostal"];
                this.ctrTelephone1.NoTelephone = (String)lecteurClient["Tel1"];
                this.ctrTelephone2.NoTelephone = lecteurClient["Tel2"] == DBNull.Value ? null : (String)lecteurClient["Tel2"];
                this.tbMaxLivraison.Text = Convert.ToString((int)lecteurClient["MaxLivraison"]);
                this.tbLivraisonGratuite.Text = Convert.ToString((Decimal)lecteurClient["LivraisonGratuite"]);
                this.lblMAJ.Text = lecteurClient["DateMAJ"] == DBNull.Value ? "Date non disponible" :Convert.ToString((DateTime)lecteurClient["DateMAJ"]);
                this.lblCourriel.Text = (String)lecteurClient["AdresseEmail"];
                this.tbPourcentage.Text = Convert.ToString((Decimal)lecteurClient["Pourcentage"]);
                this.cbTaxes.Checked = (Boolean)lecteurClient["Taxes"];
                //this.ctrTelephone.NoTelephone = (String)lecteurClient["Tel1"]; 

                //Object noCellulaire = lecteurClient["Tel2"];
                //if (!(noCellulaire is DBNull))
                //{
                //    this.ctrCellulaire.NoTelephone = (String)noCellulaire;
                //}

                myConnection.Close();
            }
        }
        protected void LireXML() 
        {

        }

        protected void ecrireFichierXML() 
        {
            
       StreamWriter fEcrit; StreamReader fLit;
       String strNomFichier;
       String strLigneLue;
       Int16 i;
      
           strNomFichier = Server.MapPath(Session["ID"]+".xml"); 

           /* Création du fichier texte */
           fEcrit = new StreamWriter(strNomFichier);


           /*<?xml version="1.0" encoding="utf-8" ?>
<college>
  <departement no="101" nom="Biologie">*/

fEcrit.WriteLine(" <?xml version=\""+"1.0" +"encoding=\""+"utf-8" +"\"?>");
fEcrit.WriteLine("");
fEcrit.Close();

        }

        protected void sauverProfil(object sender, EventArgs e) 
        {
            Page.Validate();
            Response.Write(Page.IsValid);

            if (Page.IsValid) 
            {
                Dictionary<String, String> dicPaires = new Dictionary<String, String>();

                dicPaires.Add("NomAffaires",this.tbNomAffaires.Text);
                dicPaires.Add("Prenom", this.txtPrenom.Text);
                dicPaires.Add("Nom", this.txtNom.Text);
               // dicPaires.Add("Rue", this.txtRue.Text);
                dicPaires.Add("Ville", this.txtVille.Text);
                dicPaires.Add("Province", this.ctrProvince.CodeProvince);
                dicPaires.Add("Pays", this.txtPays.Text);
                dicPaires.Add("CodePostal", this.ctrCodePostal.Code);
                dicPaires.Add("Tel1", this.ctrTelephone1.NoTelephone);
                dicPaires.Add("Tel2", this.ctrTelephone2.NoTelephone);
               String max = this.tbMaxLivraison.Text;
               String gratuit = this.tbLivraisonGratuite.Text;

              
                //dicPaires.Add("LivraisonGratuite",this.tbLivraisonGratuite.Text);
                dicPaires.Add("DateMAJ",DateTime.Now.ToShortDateString());

                String taxesC = cbTaxes.Checked == true ? "1" : "0";
                List<String> tabPaires = new List<String>();
                foreach (String clee in dicPaires.Keys)
                {
                    tabPaires.Add(clee + " = '" + dicPaires[clee] + "'");
                }
                String strPaires = String.Join(", ", tabPaires);

                SqlCommand commandeClient = new SqlCommand("UPDATE PPVendeurs SET " + strPaires + " , Taxes=" + taxesC + ",MaxLivraison=@max,LivraisonGratuite=@gratuit WHERE NoVendeur = " + Session["ID"], myConnection);

                commandeClient.Parameters.AddWithValue("@max", Convert.ToInt32(this.tbMaxLivraison.Text.Replace('.',',')));
                commandeClient.Parameters.AddWithValue("@gratuit", Convert.ToDecimal(this.tbLivraisonGratuite.Text.Replace('.', ',')));

                myConnection.Open();
                commandeClient.ExecuteNonQuery();
                myConnection.Close();
                Response.Redirect("AccueilVendeur.aspx");
            }
        }
    }
}