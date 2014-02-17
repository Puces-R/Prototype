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
            if (!IsPostBack)
            {
                Librairie.Autorisation(false, true, true, true);
            }
            this.Header.DataBind();
            char type = (char)Session["Type"];
            switch (type)
            {
                case 'C':
                    MenuItem vendeurC = new MenuItem("Vendeurs", "VC");
                    MenuItem gestionnaireC = new MenuItem("Gestionnaires", "GC");
                    vendeurC.NavigateUrl = "javascript:changeType('VC')";
                    gestionnaireC.NavigateUrl = "javascript:changeType('GC')";
                    menuType.Items.Add(vendeurC);
                    menuType.Items.Add(gestionnaireC);
                    break;
                case 'V':
                    MenuItem clientV = new MenuItem("Client", "CV");
                    MenuItem gestionnaireV = new MenuItem("Gestionnaires", "GV");
                    clientV.NavigateUrl = "javascript:changeType('CV')";
                    gestionnaireV.NavigateUrl = "javascript:changeType('GV')";
                    menuType.Items.Add(clientV);
                    menuType.Items.Add(gestionnaireV);
                    break;
                case 'G':
                    MenuItem clientG = new MenuItem("Clients", "CG");
                    MenuItem vendeurG = new MenuItem("Vendeurs", "VG");
                    MenuItem gestionnaireG = new MenuItem("Gestionnaires", "GG");
                    clientG.NavigateUrl = "javascript:changeType('CG')";
                    vendeurG.NavigateUrl = "javascript:changeType('VG')";
                    gestionnaireG.NavigateUrl = "javascript:changeType('GG')";
                    menuType.Items.Add(clientG);
                    menuType.Items.Add(vendeurG);
                    menuType.Items.Add(gestionnaireG);
                    break;
            }
        }

        [System.Web.Services.WebMethod]
        public static string GetResultats(string name, string id, string type = "ZZ")
        {
            string retour = ";;;";
            string[] lstId = id.Split(new char[] { ',' });
            Dictionary<string, SqlCommand> cmdChosen = new Dictionary<string, SqlCommand>();

            switch (type[1])
            {
                case 'C':
                    cmdChosen.Add("Vendeur", new SqlCommand("SELECT NoVendeur 'No', RTRIM(NomAffaires) + ' &lt;' + AdresseEmail + '&gt;' 'Texte' FROM PPVendeurs", connexion));
                    cmdChosen.Add("Gestionnaire", new SqlCommand("SELECT NoGestionnaire 'No', RTRIM(Nom) + ', ' + RTRIM(Prenom) + ' &lt;' + AdresseEmail + '&gt;' 'Texte' FROM PPGestionnaires", connexion));
                    break;
                case 'V':
                    cmdChosen.Add("Client", new SqlCommand("SELECT NoClient 'No', ISNULL(Nom + ', ' + RTRIM(Prenom) + ' &lt;' + AdresseEmail + '&gt;', AdresseEmail) 'Texte' FROM PPClients", connexion));
                    cmdChosen.Add("Gestionnaire", new SqlCommand("SELECT NoGestionnaire 'No', RTRIM(Nom) + ', ' + RTRIM(Prenom) + ' &lt;' + AdresseEmail + '&gt;' 'Texte' FROM PPGestionnaires", connexion));
                    break;
                case 'G':
                    cmdChosen.Add("Client", new SqlCommand("SELECT NoClient 'No', ISNULL(Nom + ', ' + RTRIM(Prenom) + ' &lt;' + AdresseEmail + '&gt;', AdresseEmail) 'Texte' FROM PPClients", connexion));
                    cmdChosen.Add("Vendeur", new SqlCommand("SELECT NoVendeur 'No', RTRIM(NomAffaires) + ' &lt;' + AdresseEmail + '&gt;' 'Texte' FROM PPVendeurs", connexion));
                    cmdChosen.Add("Gestionnaire", new SqlCommand("SELECT NoGestionnaire 'No', RTRIM(Nom) + ', ' + RTRIM(Prenom) + ' &lt;' + AdresseEmail + '&gt;' 'Texte' FROM PPGestionnaires", connexion));
                    break;
            }
            connexion.Open();
            if (id != string.Empty)
            {
                string[] param = new string[lstId.Length];

                for (int i = 0; i < lstId.Length; i++)
                {
                    param[i] = string.Format("@no{0}", i);
                    foreach (string s in cmdChosen.Keys)
                    {
                        cmdChosen[s].Parameters.AddWithValue(param[i], lstId[i]);
                    }
                }
                foreach (string s in cmdChosen.Keys)
                {
                    cmdChosen[s].CommandText += string.Format(" WHERE No" + s + " IN ({0})", string.Join(", ", param));
                    SqlDataReader sdrChosen = cmdChosen[s].ExecuteReader();
                    for (int i = 0; sdrChosen.Read(); i++)
                    {
                        if (i == 0)
                        {
                            retour += "<li><strong>" + s + "</strong></li>";
                        }
                        retour += "<li>";
                        retour += "<a href=\"javascript:deselectionner(" + sdrChosen["No"].ToString() + ")\" >" + sdrChosen["Texte"].ToString() + "</a>";
                        retour += "</li>";
                    }
                    sdrChosen.Close();
                }
            }


            if ((type[0] == 'C' || type[0] == 'V' || type[0] == 'G') && type.Length == 2)
            {
                SqlCommand cmdAutocomplete = null;
                string champNo = "";

                switch (type[0])
                {
                    case 'C':
                        cmdAutocomplete = new SqlCommand("SELECT NoClient 'No', ISNULL(Nom + ', ' + RTRIM(Prenom) + ' &lt;' + AdresseEmail + '&gt;', AdresseEmail) 'Texte' FROM PPClients WHERE (Nom LIKE @nom OR AdresseEmail LIKE @nom OR Prenom LIKE @nom)", connexion);
                        champNo = "NoClient";
                        break;
                    case 'V':
                        cmdAutocomplete = new SqlCommand("SELECT NoVendeur 'No', RTRIM(NomAffaires) + ' &lt;' + AdresseEmail + '&gt;' 'Texte' FROM PPVendeurs WHERE (NomAffaires LIKE @nom OR AdresseEmail LIKE @nom)", connexion);
                        champNo = "NoVendeur";
                        break;
                    case 'G':
                        cmdAutocomplete = new SqlCommand("SELECT NoGestionnaire 'No', RTRIM(Nom) + ', ' + RTRIM(Prenom) + ' &lt;' + AdresseEmail + '&gt;' 'Texte' FROM PPGestionnaires WHERE (Nom LIKE @nom OR AdresseEmail LIKE @nom OR Prenom LIKE @nom) ", connexion);
                        champNo = "NoGestionnaire";
                        break;
                }

                if (id != string.Empty)
                {
                    string[] param = new string[lstId.Length];

                    for (int i = 0; i < lstId.Length; i++)
                    {
                        param[i] = string.Format("@no{0}", i);
                        cmdAutocomplete.Parameters.AddWithValue(param[i], lstId[i]);
                    }
                    cmdAutocomplete.CommandText += string.Format(" AND " + champNo + " NOT IN ({0})", string.Join(", ", param));
                }
                cmdAutocomplete.CommandText += " ORDER BY " + champNo + " DESC";
                cmdAutocomplete.Parameters.AddWithValue("@nom", "%" + name + "%");
                Regex rgx = new Regex("(" + Regex.Escape(name) + ")", RegexOptions.IgnoreCase);
                SqlDataReader sdrAutocomplete = cmdAutocomplete.ExecuteReader();
                while (sdrAutocomplete.Read())
                {
                    string no = sdrAutocomplete["No"].ToString();
                    string texte = sdrAutocomplete["Texte"].ToString();
                    retour = "<li>" + retour;
                    retour = "<a href=\"javascript:selectionner(" + no + ")\" >" + rgx.Replace(texte, "<strong style=\"color: red;\">$0</strong>", 1) + "</a>" + retour;
                    retour = "</li>\n" + retour;
                }
                sdrAutocomplete.Close();
            }
            connexion.Close();
            return retour == string.Empty ? ";;;" : retour;
        }
    }
}