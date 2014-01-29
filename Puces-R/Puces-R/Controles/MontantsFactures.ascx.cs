using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Puces_R.Controles
{
    public partial class MontantsFactures : System.Web.UI.UserControl
    {
        public String NavigateUrl
        {
            get
            {
                return (String)ViewState["NavigateUrl"];
            }
            set
            {
                ViewState["NavigateUrl"] = value;
            }
        }

        public int NoVendeur
        {
            get
            {
                return (int)ViewState["NoVendeur"];
            }
            set
            {
                ViewState["NoVendeur"] = value;
            }
        }

        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SqlDataAdapter adapteurCategories = new SqlDataAdapter("SELECT * FROM PPTypesLivraison", myConnection);
                DataTable tableCategories = new DataTable();
                adapteurCategories.Fill(tableCategories);

                ddlModesLivraison.DataSource = tableCategories;
                ddlModesLivraison.DataTextField = "Description";
                ddlModesLivraison.DataValueField = "CodeLivraison";
                ddlModesLivraison.DataBind();

                CalculerCouts();
            }
        }

        public void CalculerCouts()
        {
            if (Session["ID"] == null)
            {
                Response.Redirect("Default.aspx", true);
            }

            String whereClause = " WHERE A.NoClient = " + Session["ID"] + " AND P.NoVendeur = " + NoVendeur;

            SqlDataAdapter adapteurProduits = new SqlDataAdapter("SELECT NbItems, PrixDemande, Poids FROM PPProduits P INNER JOIN PPCategories C ON C.NoCategorie = P.NoCategorie INNER JOIN PPArticlesEnPanier A ON A.NoProduit = P.NoProduit" + whereClause, myConnection);
            DataTable tableProduits = new DataTable();
            adapteurProduits.Fill(tableProduits);

            decimal sousTotal = 0;
            decimal poidsTotal = 0;

            foreach (DataRow produit in tableProduits.Rows)
            {
                short nbItems = (short)produit["NbItems"];
                sousTotal += nbItems * (decimal)produit["PrixDemande"];
                poidsTotal += nbItems * (decimal)produit["Poids"];
            }

            lblPoidsTotal.Text = poidsTotal.ToString() + " lbs.";
            lblSousTotal.Text = sousTotal.ToString("C");

            myConnection.Open();

            SqlCommand commandeVendeur = new SqlCommand("SELECT NomAffaires FROM PPVendeurs WHERE NoVendeur = " + NoVendeur, myConnection);

            SqlCommand commandeLivraison = new SqlCommand("SELECT P.Tarif FROM PPTypesPoids T INNER JOIN PPPoidsLivraisons P ON T.CodePoids = P.CodePoids WHERE P.CodeLivraison = " + ddlModesLivraison.SelectedValue + " AND " + poidsTotal.ToString().Replace(",", ".") + " BETWEEN T.PoidsMin AND T.PoidsMax", myConnection);
            decimal prixLivraison = (decimal)commandeLivraison.ExecuteScalar();

            decimal prixAvecLivraison = sousTotal + prixLivraison;

            SqlCommand commandeTauxTPS = new SqlCommand("SELECT TOP(1) TauxTPS FROM PPTaxeFederale ORDER BY DateEffectiveTPS DESC", myConnection);
            decimal tauxTPS = ((decimal)commandeTauxTPS.ExecuteScalar()) / 100;

            SqlCommand commandeTauxTVQ = new SqlCommand("SELECT TOP(1) TauxTVQ FROM PPTaxeProvinciale ORDER BY DateEffectiveTVQ DESC", myConnection);
            decimal tauxTVQ = ((decimal)commandeTauxTVQ.ExecuteScalar()) / 100;

            decimal prixTPS = prixAvecLivraison * tauxTPS;
            decimal prixTVQ = prixAvecLivraison * tauxTVQ;

            decimal grandTotal = prixAvecLivraison + prixTPS + prixTVQ;

            lblLivraison.Text = prixLivraison.ToString("C");
            lblTauxTPS.Text = "(" + tauxTPS.ToString("P") + ")";
            lblTauxTVQ.Text = "(" + tauxTVQ.ToString("P") + ")";
            lblTPS.Text = prixTPS.ToString("C");
            lblTVQ.Text = prixTVQ.ToString("C");
            lblGrandTotal.Text = grandTotal.ToString("C");

            myConnection.Close();
        }

        protected void ddlModesLivraison_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CalculerCouts();
        }

        protected void btnCommander_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(NavigateUrl, true);
        }
    }
}