﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

namespace Puces_R
{
    public partial class InscriptionClient : System.Web.UI.Page
    {
        SqlConnection connexion = Librairie.Connexion;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void inscription(object sender, EventArgs e)
        {

            Page.Validate();

            if (Page.IsValid)
            {
                connexion.Open();
                SqlCommand cmdNoClient = new SqlCommand("SELECT ISNULL(MAX(NoClient), 9999) + 1 FROM PPClients", connexion);
                int noClient = int.Parse(cmdNoClient.ExecuteScalar().ToString());
                SqlCommand cmdInsertion = new SqlCommand("INSERT INTO PPClients(NoClient, AdresseEmail, MotDePasse, DateCreation) values (@no, @adresse, @mdp, @date)", connexion);
                cmdInsertion.Parameters.AddWithValue("@no", noClient);
                cmdInsertion.Parameters.AddWithValue("@adresse", ctlIdentifiants.Adresse);
                cmdInsertion.Parameters.AddWithValue("@mdp", ctlIdentifiants.MotDePasse);
                cmdInsertion.Parameters.AddWithValue("@date", DateTime.Now.Date);

                cmdInsertion.ExecuteNonQuery();

                connexion.Close();

                Courriel c = new Courriel();

                c.ajouterDestinataire(ctlIdentifiants.Adresse);
                c.Sujet = "Inscription aux Petites Puces";
                c.Message = "NoClient = " + noClient + "<br />" +
                                "Adresse courriel = " + ctlIdentifiants.Adresse + "<br />" +
                                "Mot de passe = " + ctlIdentifiants.MotDePasse;
                c.envoyer();

            }
        }
    }
}