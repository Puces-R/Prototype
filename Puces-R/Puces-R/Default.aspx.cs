﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class Default : System.Web.UI.Page
    {
        SqlConnection connexion = Librairie.Connexion;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
        }

        protected void seConnecter(object sender, EventArgs e)
        {
            Page.Validate();

            SqlCommand cmdAddConnexion = new SqlCommand();
            cmdAddConnexion.Connection = connexion;
            cmdAddConnexion.Parameters.AddWithValue("@addr", tbCourriel.Text);
            cmdAddConnexion.Parameters.AddWithValue("@date", DateTime.Now);
            string redirection = "Default.aspx";
            if (Page.IsValid)
            {
                switch ((char)Session["Type"])
                {
                    case 'C':
                        cmdAddConnexion.CommandText = "UPDATE PPClients SET NbConnexions = ISNULL(NbConnexions, 0) + 1, DateDerniereConnexion = @date WHERE AdresseEmail = @addr";
                        redirection = "AccueilClient.aspx";
                        break;
                    case 'V':
                        cmdAddConnexion.CommandText = "UPDATE PPVendeurs SET NbConnexions = ISNULL(NbConnexions, 0) + 1, DateDerniereConnexion = @date WHERE AdresseEmail = @addr";
                        redirection = "AccueilVendeur.aspx";
                        break;
                    case 'G':
                        cmdAddConnexion.CommandText = "UPDATE PPGestionnaires SET NbConnexions = ISNULL(NbConnexions, 0) + 1, DateDerniereConnexion = @date WHERE AdresseEmail = @addr";
                        redirection = "accueil_gestionnaire.aspx";
                        break;
                }

                connexion.Open();
                cmdAddConnexion.ExecuteNonQuery();
                connexion.Close();

                Response.Redirect(redirection, false);
            }
        }

        protected void existe(object sender, ServerValidateEventArgs e)
        {
            SqlCommand cmdConnexion = new SqlCommand("SELECT No, Type FROM " +
                                                            "(SELECT NoClient AS No, 'C' AS Type, AdresseEmail, MotDePasse FROM PPClients UNION " +
                                                             "SELECT NoVendeur AS No, 'V' AS Type, AdresseEmail, MotDePasse FROM PPVendeurs UNION " +
                                                             "SELECT NoGestionnaire AS No, 'G' AS Type, AdresseEmail, MotDePasse FROM PPGestionnaires) AS X " +
                                                         "WHERE (AdresseEmail LIKE @adr) AND (MotDePasse COLLATE sql_latin1_General_CP1_cs_as LIKE @mdp)", connexion);

            cmdConnexion.Parameters.AddWithValue("@adr", tbCourriel.Text);
            cmdConnexion.Parameters.AddWithValue("@mdp", tbMotPasse.MotPasse);
            connexion.Open();
            SqlDataReader sdr = cmdConnexion.ExecuteReader();
            if (sdr.Read())
            {
                e.IsValid = true;
                Session["ID"] = int.Parse(sdr["No"].ToString());
                Session["Type"] = char.Parse(sdr["Type"].ToString());
            }
            else
            {
                e.IsValid = false;
            }

            connexion.Close();
        }

        protected void defautClient(object sender, EventArgs e)
        {
            Session["ID"] = 30063;
            Session["Type"] = 'C';
            Response.Redirect("AccueilClient.aspx", false);
        }

        protected void defautVendeur(object sender, EventArgs e)
        {
            Session["ID"] = 10;
            Session["Type"] = 'V';
            Response.Redirect("AccueilVendeur.aspx", false);
        }

        protected void defautGestionnaire(object sender, EventArgs e)
        {
            Session["ID"] = 1;
            Session["Type"] = 'G';
            Response.Redirect("accueil_gestionnaire.aspx", false);
        }
    }
}