﻿using System;
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

        private static SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("raikou4@gmail.com", "p0k3m0n2515"),
            EnableSsl = true
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

        public bool envoyer()
        {
            MailMessage courriel = new MailMessage();

            courriel.From = new MailAddress("no-reply@lespetitespuces.com", "Les Petites Puces");
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
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}