using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;


namespace Puces_R.Controles
{
    public partial class ProfilVendeur : System.Web.UI.UserControl
    {
        public string NomAffaires
        {
            get
            {
                return tbNomAffaires.Text.Trim();
            }
            set
            {
                tbNomAffaires.Text = value.Trim();
            }
        }

        public string Prenom
        {
            get
            {
                return txtPrenom.Text.Trim();
            }
            set
            {
                txtPrenom.Text = value.Trim();
            }
        }

        public string Nom
        {
            get
            {
                return txtNom.Text.Trim();
            }
            set
            {
                txtNom.Text = value.Trim();
            }
        }

        public string Adresse
        {
            get
            {
                return ctrAdresse.NoAdresse.Trim();
            }
            set
            {
                ctrAdresse.NoAdresse = value.Trim();
            }
        }

        public string Ville
        {
            get
            {
                return txtVille.Text.Trim();
            }
            set
            {
                txtVille.Text = value.Trim();
            }
        }

        public string Province
        {
            get
            {
                return ctrProvince.CodeProvince;
            }
            set
            {
                ctrProvince.CodeProvince = value.Trim().ToUpper();
            }
        }

        public string CodePostal
        {
            get
            {
                return ctrCodePostal.Code;
            }
            set
            {
                ctrCodePostal.Code = value.Trim();
            }
        }

        public string Pays
        {
            get
            {
                return txtPays.Text.Trim();
            }
            set
            {
                txtPays.Text = value.Trim();
            }
        }

        public string Tel1
        {
            get
            {
                return ctrTelephone1.NoTelephone;
            }
            set
            {
                ctrTelephone1.NoTelephone = value;
            }
        }

        public string Tel2
        {
            get
            {
                return ctrTelephone2.NoTelephone;
            }
            set
            {
                ctrTelephone2.NoTelephone = value;
            }
        }

        public int PoidsMaximum
        {
            get
            {
                return int.Parse(tbMaxLivraison.Text.Trim());
            }
            set
            {
                tbMaxLivraison.Text = value.ToString();
            }
        }

        public decimal LivraisonGratuite
        {
            get
            {
                return decimal.Parse(tbLivraisonGratuite.Text);
            }
            set
            {
                tbLivraisonGratuite.Text = value.ToString();
            }
        }

        public bool Taxes
        {
            get
            {
                return cbTaxes.Checked;
            }
            set
            {
                cbTaxes.Checked = value;
            }
        }
    }
}