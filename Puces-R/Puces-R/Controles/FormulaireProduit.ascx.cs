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
    public partial class FormulaireProduit : System.Web.UI.UserControl
    {
        private int _noProduit = -1;

        public int NoProduit
        {
            get
            {
                return _noProduit;
            }
            set
            {
                _noProduit = value;
            }
        }

        public long NoCategorie
        {
            get
            {
                return long.Parse(ddlCategorieProduits.SelectedValue);
            }
            set
            {
                ddlCategorieProduits.SelectedValue = value.ToString();
            }
        }

        public string DescriptionAbregee
        {
            get
            {
                return tbDescAbregee.Text.Trim();
            }
            set
            {
                tbDescAbregee.Text = value.Trim();
            }
        }

        public decimal PrixDemande
        {
            get
            {
                return decimal.Parse(tbPrix.Text.Replace('.', ','));
            }
            set
            {
                tbPrix.Text = value.ToString("#0.00");
            }
        }

        public string DescriptionComplete
        {
            get
            {
                return tbDescComplete.Text.Trim();
            }
            set
            {
                tbDescComplete.Text = value.Trim();
            }
        }

        public string Fichier
        {
            get
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
                        string[] split = filename.Split('.');
                        // String nom[] = filename.Split('.');
                        //string ext = System.IO.Path.GetExtension(this.File1.PostedFile.FileName);
                        // Response.Write(uplNomFichier.PostedFile.ContentType);
                        //SqlCommand maC = new SqlCommand("select Photo from PPProduits where NoVendeur="+Session["ID"]);
                        uplNomFichier.SaveAs(MapPath("~/Images/Televerse/" + Session["ID"] + _noProduit.ToString() + "." + split[1]));
                        filename = Session["ID"] + _noProduit.ToString() + "." + split[1];
                        //StatusLabel.Text = "Upload status: File uploaded!";

                    }
                    catch (Exception)
                    {
                        return null;
                        //StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                    }
                }
                else
                {
                    return null;
                }
                return filename;
            }
        }

        public int NbItems
        {
            get
            {
                return int.Parse(tbNbItems.Text.Trim());
            }
            set
            {
                tbNbItems.Text = value.ToString();
            }
        }

        public decimal PrixVente
        {
            get
            {
                if (tbPrixVente.Text.Trim() == string.Empty)
                {
                    return -1;
                }
                return decimal.Parse(tbPrixVente.Text.Trim());
            }
            set
            {
                tbPrixVente.Text = value.ToString("#0.00");
            }
        }

        public decimal Poids
        {
            get
            {
                return decimal.Parse(tbPois.Text.Trim());
            }
            set
            {
                tbPois.Text = value.ToString("#0.0");
            }
        }

        public bool Disponibilite
        {
            get
            {
                return cbDisponibilite.Checked;
            }
            set
            {
                cbDisponibilite.Checked = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            SqlConnection connexion = Librairie.Connexion;

            SqlCommand maCommande = new SqlCommand("select NoCategorie, Description from PPCategories ", connexion);
            connexion.Open();
            SqlDataReader rep = maCommande.ExecuteReader();
            while (rep.Read())
            {
                ddlCategorieProduits.Items.Add(new ListItem(rep["Description"].ToString(), rep["NoCategorie"].ToString()));
            }
            connexion.Close();
        }

        protected void verifierFormat(object sender, ServerValidateEventArgs e)
        {
            if (uplNomFichier.HasFile)
            {
                try
                {
                    String filename = Path.GetFileName(uplNomFichier.FileName);
                    string[] split = filename.Split('.');
                    e.IsValid = (split[1] == "jpg" || split[1] == "png" || split[1] == "gif" || split[1] == "jpeg");
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }
    }
}