using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class ChoixDestinataires : System.Web.UI.Page
    {
        static SqlConnection connexion = Librairie.Connexion;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static string GetResultats(string name, string id)
        {
            string[] lstId = id.Split(new char[] { ',' });
            SqlCommand cmdAutocomplete = new SqlCommand("SELECT NoVendeur, NomAffaires 'Texte' FROM PPVendeurs WHERE NomAffaires LIKE @nom", connexion);
            SqlCommand cmdChosen = new SqlCommand("SELECT NoVendeur, NomAffaires 'Texte' FROM PPVendeurs", connexion);
            if (id != string.Empty)
            {
                string[] param = new string[lstId.Length];

                for (int i = 0; i < lstId.Length; i++)
                {
                    param[i] = string.Format("@no{0}", i);
                    cmdAutocomplete.Parameters.AddWithValue(param[i], lstId[i]);
                    cmdChosen.Parameters.AddWithValue(param[i], lstId[i]);
                }
                cmdAutocomplete.CommandText += string.Format(" AND NoVendeur NOT IN ({0})", string.Join(", ", param));
                cmdChosen.CommandText += string.Format(" WHERE NoVendeur IN ({0})", string.Join(", ", param));
            }
            cmdAutocomplete.Parameters.AddWithValue("@nom", "%" + name + "%");
            string retour = "";
            Regex rgx = new Regex("(" + Regex.Escape(name) + ")", RegexOptions.IgnoreCase);
            try
            {
                connexion.Open();
                SqlDataReader sdrAutocomplete = cmdAutocomplete.ExecuteReader();
                while (sdrAutocomplete.Read())
                {
                    retour += "<li>";
                    retour += "<a href=\"javascript:selectionner(" + sdrAutocomplete["NoVendeur"].ToString() + ")\" >" + rgx.Replace(sdrAutocomplete["Texte"].ToString(), "<strong style=\"color: red;\">$0</strong>", 1) + "</a>";
                    retour += "</li>\n";
                }
                retour += ";;;";
                sdrAutocomplete.Close();
                if (id != string.Empty)
                {
                    SqlDataReader sdrChosen = cmdChosen.ExecuteReader();
                    while (sdrChosen.Read())
                    {
                        retour += "<li>";
                        retour += "<a href=\"javascript:deselectionner(" + sdrChosen["NoVendeur"].ToString() + ")\" >" + sdrChosen["Texte"].ToString() + "</a>";
                        retour += "</li>";
                    }
                    sdrChosen.Close();
                }
                connexion.Close();
            }
            catch (Exception)
            {
                connexion.Close();
            }
            return retour;
        }
    }
}