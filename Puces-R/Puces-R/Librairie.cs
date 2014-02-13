using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Web.UI;
using System.Web.UI.WebControls;

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

        public static void Messagerie(int[] destinataires, string sujet = null, string message = null, bool fixer = false, string retour = "Retour")
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

        public static void Courriel(int[] destinataires, string sujet = null, string message = null, bool fixer = false, string retour = "Retour")
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
    }
}