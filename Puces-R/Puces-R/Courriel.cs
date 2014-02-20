using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace Puces_R
{
    public class Courriel
    {
        private List<string> _destinataires = new List<string>();

        private string _sujet = "";
        private string _message = "";
        private string _nomEnvoyeur = "";
        private string _courrielEnvoyeur = "";
        public string err_msg = "Message d'erreur: ";

        public string Sujet
        {
            get
            {
                return _sujet;
            }
            set
            {
                _sujet = value;
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
            }
        }

        private static bool local = true; // Mettre à false avant de publier sur le ftp

        public static SmtpClient client = new SmtpClient()
        {
            Credentials = local ? new NetworkCredential("petitespuces@towardnewobjects.org", "NWa7dZ") : null,
            Host = local ? "smtpout.secureserver.net" : "192.168.10.25"
        };

        public Courriel() : this("(Aucun message)", "(Vide)") { }

        public Courriel(string sujet, string message)
        {
            _sujet = sujet;
            _message = message;
        }

        public Courriel(string sujet, string message, string destinataire)
            : this(sujet, message)
        {
            _destinataires.Add(destinataire);
        }

        public Courriel(string sujet, string message, string[] destinataires)
            : this(sujet, message)
        {
            _destinataires.AddRange(destinataires);
        }

        public void ajouterDestinataire(string destinataire)
        {
            _destinataires.Add(destinataire);
        }

        public void ajouterDestinataire(string[] destinataires)
        {
            _destinataires.AddRange(destinataires);
        }

        public void changerEnvoyeur(string nom, string email)
        {
            _courrielEnvoyeur = email;
            _nomEnvoyeur = nom;
        }

        public bool envoyer()
        {
            MailMessage courriel = new MailMessage();

            courriel.From = new MailAddress(_courrielEnvoyeur == "" ? "no-reply@lespetitespuces.com" : _courrielEnvoyeur, _nomEnvoyeur == "" ? "Les Petites Puces" : _nomEnvoyeur);
            foreach (string s in _destinataires)
            {
                courriel.To.Add(s);
            }
            courriel.Subject = _sujet;
            courriel.IsBodyHtml = true;
            courriel.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;


            courriel.Body = _message;
            courriel.BodyEncoding = System.Text.Encoding.UTF8;


            try
            {
                client.Send(courriel);
                return true;
            }
            catch (Exception e)
            {
                err_msg += e.Message;
                return false;
            }

        }
    }
}