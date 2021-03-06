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
        SqlConnection myConnection = Librairie.Connexion;

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
                lblTitreSansMenu.Text = value;
                pnlTitreAvecLigne.Visible = (value != null);
                mvTitre.ActiveViewIndex = 0;
            }
        }

        public long NoVendeur
        {
            set
            {
                SqlCommand commandVendeur = new SqlCommand("SELECT NomAffaires FROM PPVendeurs WHERE NoVendeur = " + value, myConnection);

                myConnection.Open();
                String nomAffaires = (String)commandVendeur.ExecuteScalar();
                myConnection.Close();

                if ((char)Session["Type"] == 'C')
                {
                    pnlTitreAvecLigne.Visible = true;

                    lblTitreAvecMenu.Text = nomAffaires;

                    this.ctrMenuVendeur.NoVendeur = value;
                    
                    myConnection.Open();
                    SqlCommand commandXML = new SqlCommand("SELECT Configuration FROM PPVendeurs WHERE NoVendeur = " + value, myConnection);
                    Object nom = commandXML.ExecuteScalar();

                    imgLogo.Visible = false;
                    pnlTitre.BackColor = Color.LightGray;

                    if (!(nom is DBNull))
                    {
                        LectureXML lecture = new LectureXML(Convert.ToInt64(nom));

                        if (lecture.Existe)
                        {
                            pnlTitre.BackColor = ColorTranslator.FromHtml("#" + lecture.Couleur);
                            imgLogo.ImageUrl = "~/Images/Logo/" + lecture.NomLogo;
                            imgLogo.Visible = true;
                        }
                    }
                    
                    mvTitre.ActiveViewIndex = 1;

                    SqlCommand commandNbVisitesAujourdhui = new SqlCommand("SELECT COUNT(*) FROM PPVendeursClients WHERE NoVendeur = " + value + " AND NoClient = " + Session["ID"] + " AND DateVisite = '" + DateTime.Today + "'", myConnection);
                    int nbVisitesAujourdhui = (int)commandNbVisitesAujourdhui.ExecuteScalar();
                    if (nbVisitesAujourdhui == 0)
                    {
                        SqlCommand commandAjouterVisite = new SqlCommand("INSERT INTO PPVendeursClients VALUES (" + value + ", " + Session["ID"] + ", '" + DateTime.Today + "')", myConnection);
                        commandAjouterVisite.ExecuteNonQuery();
                    }

                    myConnection.Close();
                }
                else
                {
                    Titre = nomAffaires;
                }
            }
        }

        private void loadMenu()
        {
            if (menu.Controls.Count == 0)
            {
                if (Session["Type"] != null)
                {
                    Control c = null;
                    hlDeconnexion.Visible = true;
                    hlMessage.Visible = true;
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
                    menu.Controls.Clear();
                    try
                    {
                        menu.Controls.Add(c);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            menu.Controls.Add(c); // Weird error...
                        }
                        catch (Exception)
                        {
                        }
                    }

                    if (!IsPostBack)
                    {
                        SqlCommand cmdNbMessages = new SqlCommand("SELECT COUNT(*) FROM PPDestinatairesMessages WHERE Boite = 1 AND Lu = 0 AND NoDestinataire = @no", myConnection);
                        cmdNbMessages.Parameters.AddWithValue("@no", Session["ID"]);

                        myConnection.Open();
                        int nbMessages = int.Parse(cmdNbMessages.ExecuteScalar().ToString());
                        myConnection.Close();
                        if (nbMessages > 0)
                        {
                            hlMessage.Text += " (" + nbMessages + ")";
                        }
                    }
                }
                else
                {
                    menu.Controls.Add(LoadControl("~/Controles/MenuInvite.ascx"));
                    hlDeconnexion.Visible = false;
                    hlMessage.Visible = false;
                }
            }
        }

        public UserControl Menu
        {
            get
            {
                loadMenu();
                return (UserControl)menu.Controls[0];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            loadMenu();
            hypDevenirVendeur.NavigateUrl = Chemin.Ajouter(hypDevenirVendeur.NavigateUrl, "Retour à la page précédente");
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

                if (Chemin.UrlRetour != null)
                {
                    hypRetour.ForeColor = ColorTranslator.FromHtml("#0052AE");
                    hypRetour.NavigateUrl = Chemin.UrlRetour;
                    hypRetour.Text = "◄ " + Chemin.TexteRetour;
                }
            }
        }
    }
}
