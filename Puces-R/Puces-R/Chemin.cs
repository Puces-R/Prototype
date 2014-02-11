using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;

namespace Puces_R
{
    public static class Chemin
    {
        private static Page Page
        {
            get
            {
                return (Page)HttpContext.Current.Handler;
            }
        }

        private static HttpRequest Request
        {
            get
            {
                return (HttpRequest)HttpContext.Current.Request;
            }
        }

        public static String UrlRetour
        {
            get
            {
                if (Parties != null)
                {
                    List<String> parties = new List<String>(Parties.Split(';'));
                    String dernierePartie = parties.Last();
                    String urlRetour = dernierePartie;
                    if (parties.Count > 1)
                    {
                        parties.RemoveAt(parties.Count - 1);
                        urlRetour += "&cheminretour=" + Encoder(String.Join(";", parties));
                    }
                    return urlRetour;
                }
                else
                {
                    return null;
                }
            }
        }

        public static String TexteRetour
        {
            get
            {
                if (Request.Params["texteretour"] != null)
                {
                    return (String)Request.Params["texteretour"];
                }
                else
                {
                    return "Retour";
                }
            }
        }

        private static String UrlActuel
        {
            get
            {
                var nameValueCollection = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                nameValueCollection.Remove("cheminretour");
                return Request.Path + "?" + nameValueCollection;
            }
        }

        private static String Parties
        {
            get
            {
                if (HttpContext.Current.Request.Params["cheminretour"] != null)
                {
                    return Decoder(Request.Params["cheminretour"]);
                }
                else
                {
                    return null;
                }
            }
        }

        public static String Ajouter(string adresse, string texteRetour)
        {
            String parametre = String.Empty;
            if (Parties != null)
            {
                parametre += Parties + ";";
            }
            parametre += UrlActuel;

            if (adresse.Contains("?"))
            {
                adresse += "&";
            }
            else if (!adresse.Contains("&"))
            {
                adresse += "?";
            }
            adresse += "cheminretour=" + Encoder(parametre);
            adresse += "&texteretour=" + texteRetour;
            return adresse;
        }

        private static String Encoder(String texte)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(texte));
        }

        private static String Decoder(String texte)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(texte));
        }
    }
}