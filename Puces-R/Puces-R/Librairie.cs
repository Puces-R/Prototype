using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

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

        public static void Messagerie(int[] destinataires, string sujet = null, string message = null)
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

            System.Web.HttpContext.Current.Response.Redirect("EnvoyerMessage.aspx");
        }
    }
}