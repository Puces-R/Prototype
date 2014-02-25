using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Puces_R
{
    public class Librairie
    { 
        public static SqlConnection Connexion
        {
            get
            {
                return new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2;");
            }
        }

        public static SqlConnection ConnexionIP
        {
            get
            {
                return new SqlConnection("Server=10.2.50.19;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2;");
            }
        }

        public static String lireXML(String nomF)
        {
            String retour = "";
            
            if (File.Exists(nomF))
            {
                retour += "O|";
                XmlTextReader xmlEnLecture = new XmlTextReader(nomF);
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
                                                case "Valeur": noCouleur = xmlEnLecture.Value; retour += noCouleur+"|"; break;

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

                                                case "ImageURL": nomLogo = xmlEnLecture.Value; retour+=nomLogo; break;
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
            }
            else 
            {
                retour = "N|N|N";
            }

            return retour;
        }

        public static void Messagerie(long[] destinataires, string sujet = null, string message = null, bool fixer = false, string retour = "Retour")
        {
            if (destinataires != null)
            {
                System.Web.HttpContext.Current.Session["ListeDestinataires"] = destinataires;
            }

            if (sujet != null)
            {
                System.Web.HttpContext.Current.Session["Sujet"] = sujet;
            }

            if (message != null)
            {
                System.Web.HttpContext.Current.Session["Message"] = message;
            }

            System.Web.HttpContext.Current.Session["Fixer"] = fixer;
            System.Web.HttpContext.Current.Response.Redirect(Chemin.Ajouter("EnvoyerMessage.aspx", retour));
        }

        public static void Courriel(long[] destinataires, string sujet = null, string message = null, bool fixer = false, string retour = "Retour")
        {
            if (destinataires != null)
            {
                System.Web.HttpContext.Current.Session["ListeDestinataires"] = destinataires;
            }

            if (sujet != null)
            {
                System.Web.HttpContext.Current.Session["Sujet"] = sujet;
            }

            if (message != null)
            {
                System.Web.HttpContext.Current.Session["Message"] = message;
            }

            System.Web.HttpContext.Current.Session["Fixer"] = fixer;
            System.Web.HttpContext.Current.Response.Redirect(Chemin.Ajouter("EnvoyerCourriel.aspx", retour));
        }


        public static void Autorisation(bool visiteur, bool client, bool vendeur, bool gestionnaire)
        {
            if (HttpContext.Current.Session["Type"] is char)
            {
                char type = (char)HttpContext.Current.Session["Type"];
                if ((!client && type == 'C') || (!vendeur && type == 'V') || (!gestionnaire && type == 'G') || (!visiteur && !"CVG".Contains(type)))
                {
                    RefuserUtilisateur(type);
                }
            }
            else if (!visiteur)
            {
                RefuserVisiteur();
            }
        }

        public static void RefuserAutorisation()
        {
            if (HttpContext.Current.Session["Type"] is char)
            {
                RefuserUtilisateur((char)HttpContext.Current.Session["Type"]);
            }
            else
            {
                RefuserVisiteur();
            }
        }

        private static void RefuserVisiteur()
        {
            HttpContext.Current.Response.Redirect("Deconnexion.ashx", true);
        }
        
        private static void RefuserUtilisateur(char type)
        {
            String pageAccueil;
            switch (type)
            {
                case 'C':
                    pageAccueil = "AccueilClient.aspx";
                    break;
                case 'V':
                    pageAccueil = "AccueilVendeur.aspx";
                    break;
                case 'G':
                    pageAccueil = "accueil_gestionnaire.aspx";
                    break;
                default:
                    throw new ArgumentException();
            }
            HttpContext.Current.Response.Redirect(pageAccueil, true);
        }

        public static void InitialiserListe(string nomParametre, DropDownList liste)
        {
            if (HttpContext.Current.Request.Params[nomParametre] == null)
            {
                liste.SelectedValue = "-1";
            }
            else
            {
                int noVendeur;
                if (int.TryParse(HttpContext.Current.Request.Params[nomParametre], out noVendeur))
                {
                    liste.SelectedValue = noVendeur.ToString();
                }
                else
                {
                    RefuserAutorisation();
                }
            }
        }

        public static T LireParametre<T>(string nomParametre)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if(converter != null)
                {
                    try
                    {
                        return (T)converter.ConvertFromString(HttpContext.Current.Request.Params[nomParametre]);
                    }
                    catch (Exception)
                    {
                        return default(T);
                    }
                }
            }
            catch(NotSupportedException)
            {
                Librairie.RefuserAutorisation();
            }
            return default(T);
        }

        public static void SelectionnerItemMenuActuel(MenuItemCollection items, String urlPage)
        {
            foreach (MenuItem item in items)
            {
                if (item.Selectable)
                {
                    String urlItem = Path.GetFileNameWithoutExtension(item.NavigateUrl);
                    item.Selected = String.Equals(urlItem, urlPage);
                }
                SelectionnerItemMenuActuel(item.ChildItems, urlPage);
            }
        }

        public static void activer_cocher_tout(Control div_chck, string id_case, string class_cases)
        {
            string script = "";
            script += " <script> ";
            script += " 	$(document).ready(function () { ";
            script += " 		$('#" + id_case + "').click(function () { ";
            script += " 			var cases = $(\".basRectangle\").find('." + class_cases + "'); ";
            script += " 			if (this.checked) { ";
            script += " 				cases.prop('checked', 'checked'); ";
            script += " 			} else { ";
            script += " 				cases.prop('checked', ''); ";
            script += " 			} ";
            script += " 		}); ";
            script += " 	}); ";
            script += " </script> ";

            ScriptManager.RegisterStartupScript(div_chck, div_chck.GetType(), "script_cocher", script, false);
        }

        public static void activer_cocher_tout(Control div_chck)
        {
            string script = "";
            script += " <script> ";
            script += " 	$(document).ready(function () { ";
            script += " 		$('#cb_tout').click(function () { ";
            script += " 			var cases = $(\".basRectangle\").find(':checkbox'); ";
            script += " 			if (this.checked) { ";
            script += " 				cases.prop('checked', 'checked'); ";
            script += " 			} else { ";
            script += " 				cases.prop('checked', ''); ";
            script += " 			} ";
            script += " 		}); ";
            script += " 	}); ";
            script += " </script> ";

            ScriptManager.RegisterStartupScript(div_chck, div_chck.GetType(), "script_cocher", script, false);
        }

        public static bool LireEtValiderPlage(String texte, out DateTime date)
        {
            if (DateTime.TryParse(texte, out date))
            {
                return ValiderPlage(date);
            }
            else
            {
                return false;
            }
        }

        public static bool ValiderPlage(DateTime date)
        {
            return (date >= new DateTime(1900, 01, 01, 00, 00, 00) && date <= new DateTime(2079, 06, 06, 23, 59, 00));
        }

        public static string provinceTexte(string province)
        {
            switch (province.Trim().ToUpper())
            {
                case "QC":
                    return "Québec";
                case "ON":
                    return "Ontario";
                case "NB":
                    return "Nouveau-Brunswick";
            }
            return "";
        }
    }
}