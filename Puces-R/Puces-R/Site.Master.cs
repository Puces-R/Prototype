using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        public String Titre
        {
            set
            {
                lblTitre.Text = value;
                pnlTitre.Visible = true;
            }
        }

        public long NoVendeur
        {
            set
            {
                SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

                SqlCommand commandVendeur = new SqlCommand("SELECT NomAffaires FROM PPVendeurs WHERE NoVendeur = " + value, myConnection);

                myConnection.Open();
                Titre = (String)commandVendeur.ExecuteScalar();
                myConnection.Close();
            }
        }
    }
}
