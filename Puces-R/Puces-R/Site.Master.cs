﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Puces_R.Controles;
using System.Drawing;
using System.Text;
using System.Collections.Specialized;

namespace Puces_R
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        public static String UrlActuel
        {
            get
            {
                var nameValueCollection = HttpUtility.ParseQueryString(HttpContext.Current.Request.QueryString.ToString());
                nameValueCollection.Remove("chemin");
                return HttpContext.Current.Request.Path + "?" + nameValueCollection;
            }
        }

        private String UrlRetour
        {
            set
            {
                if (value != null)
                {
                    hypRetour.ForeColor = ColorTranslator.FromHtml("#0052AE");
                    hypRetour.NavigateUrl = value;
                }
            }
        }

        private static String CheminRetour
        {
            get
            {
                if (HttpContext.Current.Request.Params["chemin"] != null)
                {
                    return Decoder((String)HttpContext.Current.Request.Params["chemin"]);
                }
                else
                {
                    return null;
                }
            }
        }

        public bool MenuVisible
        {
            get
            {
                return divMenu.Visible;
            }
            set
            {
                divMenu.Visible = value;
            }
        }

        public String Titre
        {
            set
            {
                lblTitre.Text = value;
                pnlTitre.Visible = true;
            }
        }

        public Control Menu
        {
            get
            {
                if (menu.Controls.Count == 0)
                {
                    if (Session["Type"] != null)
                    {
                        Control c = null;
                        hlDeconnexion.Visible = true;
                        switch ((char)Session["Type"])
                        {
                            case 'C':
                                c = LoadControl("~/Controles/MenuClient.ascx");
                                break;
                            case 'V':
                                c = LoadControl("~/Controles/MenuVendeur.ascx");
                                break;
                            case 'G':
                                c = LoadControl("~/Controles/MenuGestionnaire.ascx");
                                break;
                            default:
                                throw new InvalidOperationException();
                        }
                        MenuItem mi = new MenuItem("Messages");
                        mi.NavigateUrl = "~/BoiteMessage.aspx";
                        SqlCommand cmdNbMessages = new SqlCommand("SELECT COUNT(*) FROM PPDestinatairesMessages WHERE Boite = 1 AND Lu = 0 AND NoDestinataire = @no", myConnection);
                        cmdNbMessages.Parameters.AddWithValue("@no", Session["ID"]);

                        myConnection.Open();
                        int nbMessages = int.Parse(cmdNbMessages.ExecuteScalar().ToString());
                        myConnection.Close();
                        if (nbMessages > 0)
                        {
                            mi.Text += " (" + nbMessages + ")";
                        }
                        ((Menu)c.FindControl("ctrMenu")).Items.Add(mi);
                        menu.Controls.Add(c);
                    }
                    else
                    {
                        menu.Controls.Add(LoadControl("~/Controles/MenuInvite.ascx"));
                        hlDeconnexion.Visible = false;
                    }
                }
                return menu.Controls[0];
            }
        }

        public long NoVendeur
        {
            set
            {
                SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

                SqlCommand commandVendeur = new SqlCommand("SELECT NomAffaires FROM PPVendeurs WHERE NoVendeur = " + value, myConnection);

                myConnection.Open();
                Titre = (String)commandVendeur.ExecuteScalar();
                myConnection.Close();

                if (Menu is MenuClient)
                {
                    ((MenuClient)Menu).NoVendeur = value;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (menu.Controls.Count == 0)
            {
                if (Session["Type"] != null)
                {
                    Control c = null;
                    hlDeconnexion.Visible = true;
                    switch ((char)Session["Type"])
                    {
                        case 'C':
                            c = LoadControl("~/Controles/MenuClient.ascx");
                            break;
                        case 'V':
                            c = LoadControl("~/Controles/MenuVendeur.ascx");
                            break;
                        case 'G':
                            c = LoadControl("~/Controles/MenuGestionnaire.ascx");
                            break;
                        default:
                            throw new InvalidOperationException();
                    }
                    MenuItem mi = new MenuItem("Messages");
                    mi.NavigateUrl = "~/BoiteMessage.aspx";
                    SqlCommand cmdNbMessages = new SqlCommand("SELECT COUNT(*) FROM PPDestinatairesMessages WHERE Boite = 1 AND Lu = 0 AND NoDestinataire = @no", myConnection);
                    cmdNbMessages.Parameters.AddWithValue("@no", Session["ID"]);

                    myConnection.Open();
                    int nbMessages = int.Parse(cmdNbMessages.ExecuteScalar().ToString());
                    myConnection.Close();
                    if (nbMessages > 0)
                    {
                        mi.Text += " (" + nbMessages + ")";
                    }
                    ((Menu)c.FindControl("ctrMenu")).Items.Add(mi);
                    menu.Controls.Add(c);
                }
                else
                {
                    menu.Controls.Add(LoadControl("~/Controles/MenuInvite.ascx"));
                    hlDeconnexion.Visible = false;
                }
            }

            if (!IsPostBack)
            {
                if (Session["Type"] != null)
                {
                    SqlCommand commandUtilisateur;

                    switch ((char)Session["Type"])
                    {
                        case 'C':
                            commandUtilisateur = new SqlCommand("SELECT Prenom + ' ' + Nom AS NomComplet FROM PPClients WHERE NoClient = " + Session["ID"], myConnection);
                            break;
                        case 'V':
                            hypDevenirVendeur.Visible = false;
                            commandUtilisateur = new SqlCommand("SELECT Prenom + ' ' + Nom AS NomComplet FROM PPVendeurs WHERE NoVendeur = " + Session["ID"], myConnection);
                            break;
                        case 'G':
                            hypDevenirVendeur.Visible = false;
                            commandUtilisateur = new SqlCommand("SELECT Prenom + ' ' + Nom AS NomComplet FROM PPGestionnaires WHERE NoGestionnaire = " + Session["ID"], myConnection);
                            break;
                        default:
                            throw new InvalidOperationException();
                    }

                    myConnection.Open();
                    Object objNomComplet = commandUtilisateur.ExecuteScalar();
                    myConnection.Close();

                    if (objNomComplet is DBNull)
                    {
                        lblBonjour.Text = "Utilisateur";
                    }
                    else
                    {
                        lblBonjour.Text = (String)objNomComplet;
                    }
                }
                else
                {
                    lblBonjour.Text = "Visiteur";
                }

                if (CheminRetour != null)
                {
                    List<String> parties = new List<String>(CheminRetour.Split(';'));
                    String dernierePartie = parties.Last();
                    String urlRetour = dernierePartie;
                    if (parties.Count > 1)
                    {
                        parties.RemoveAt(parties.Count - 1);
                        urlRetour += "&chemin=" + Encoder(String.Join(";", parties));
                    }
                    this.UrlRetour = urlRetour;
                }
            }
        }

        public static String AjouterChemin(string adresse)
        {
            String parametre = String.Empty;
            if (CheminRetour != null)
            {
                parametre += CheminRetour + ";";
            }
            parametre += UrlActuel;
            return adresse + (adresse.Contains("?") ? "&" : "?") + "chemin=" + Encoder(parametre);
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
