using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class ModifierMotPasse : System.Web.UI.Page
    {

        SqlConnection connexion = Librairie.Connexion;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void mdpValide(object sender, ServerValidateEventArgs e)
        {
            if (tbNouveauMotPasse.IsValid && tbMotPasse.IsValid)
            {
                SqlCommand cmdVerifierMdp = new SqlCommand("SELECT CASE WHEN COUNT(*) > 0 THEN 'true' ELSE 'false' END FROM " + TableFromType() +
                                                          " WHERE (" + IdColumnFromType() + " = @no) AND " +
                                                                 "(MotDePasse COLLATE sql_latin1_General_CP1_cs_as LIKE @mdp)", connexion);
                cmdVerifierMdp.Parameters.AddWithValue("@no", Session["ID"]);
                cmdVerifierMdp.Parameters.AddWithValue("@mdp", tbMotPasse.MotPasse);

                connexion.Open();
                e.IsValid = bool.Parse(cmdVerifierMdp.ExecuteScalar().ToString());
                connexion.Close();
            }
            else
            {
                e.IsValid = true;
            }
        }

        protected void modifierMdp(object sender, EventArgs e)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                SqlCommand cmdChangerMdp = new SqlCommand("UPDATE " + TableFromType() + " SET MotDePasse = @mdp WHERE (" + IdColumnFromType() + " = @no)", connexion);
                cmdChangerMdp.Parameters.AddWithValue("@mdp", tbNouveauMotPasse.MotDePasse);
                cmdChangerMdp.Parameters.AddWithValue("@no", Session["ID"]);
                connexion.Open();
                cmdChangerMdp.ExecuteNonQuery();
                connexion.Close();
            }
        }

        private string TableFromType()
        {
            switch ((Char)Session["Type"])
            {
                case 'V':
                    return "PPVendeurs";
                case 'C':
                    return "PPClients";
                case 'G':
                    return "PPGestionnaires";
            }

            return null;
        }

        private string IdColumnFromType()
        {
            switch ((Char)Session["Type"])
            {
                case 'V':
                    return "NoVendeur";
                case 'C':
                    return "NoClient";
                case 'G':
                    return "NoGestionnaire";
            }

            return null;
        }
    }
}