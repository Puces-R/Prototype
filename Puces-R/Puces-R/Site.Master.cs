using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        public String Titre
        {
            set
            {
                lblTitre.Text = value;
                pnlTitre.Visible = true;
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
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
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
                            commandUtilisateur = new SqlCommand("SELECT Prenom + ' ' + Nom AS NomComplet FROM PPVendeurs WHERE NoVendeur = " + Session["ID"], myConnection);
                            break;
                        case 'G':
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
            }
        }
    }
}
