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
        decimal sousTotal;
        decimal poidsTotal;
        short codeLivraison;
        decimal prixLivraison;
        decimal prixTPS;
        decimal prixTVQ;
        decimal grandTotal;

        public bool PoidsMaxAtteint
        {
            get
            {
                return (mvPartieBas.ActiveViewIndex == 1);
            }
        }

        public long NoVendeur
        {
            get
            {
                return (long)ViewState["NoVendeur"];
            }
            set
            {
                ViewState["NoVendeur"] = value;
            }
        }

        public long NoCommande
        {
            get
            {
                return (long)ViewState["NoCommande"];
            }
            set
            {
                ViewState["NoCommande"] = value;
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

            if (ViewState["NoCommande"] != null)
            {
                mvPartieBas.ActiveViewIndex = 0;
                ddlModesLivraison.Enabled = false;

                myConnection.Open();

                SqlCommand commandeCommande = new SqlCommand("SELECT * FROM PPCommandes WHERE NoCommande = " + NoCommande, myConnection);
                SqlDataReader lecteurCommande = commandeCommande.ExecuteReader();

                lecteurCommande.Read();

                NoVendeur = (long)lecteurCommande["NoVendeur"];
                poidsTotal = (decimal)lecteurCommande["PoidsTotal"];
                codeLivraison = (short)lecteurCommande["TypeLivraison"];
                prixLivraison = (decimal)lecteurCommande["Livraison"];
                prixTPS = (decimal)lecteurCommande["TPS"];
                prixTVQ = (decimal)lecteurCommande["TVQ"];
                grandTotal = (decimal)lecteurCommande["MontantTotal"];

                lecteurCommande.Close();
                myConnection.Close();
            }
            else
            {
                String whereClause = " WHERE A.NoClient = " + Session["ID"] + " AND P.NoVendeur = " + NoVendeur;

                SqlDataAdapter adapteurProduits = new SqlDataAdapter("SELECT NbItems, PrixDemande, Poids FROM PPProduits P INNER JOIN PPCategories C ON C.NoCategorie = P.NoCategorie INNER JOIN PPArticlesEnPanier A ON A.NoProduit = P.NoProduit" + whereClause, myConnection);
                DataTable tableProduits = new DataTable();
                adapteurProduits.Fill(tableProduits);

                sousTotal = 0;
                poidsTotal = 0;

                foreach (DataRow produit in tableProduits.Rows)
                {
                    short nbItems = (short)produit["NbItems"];
                    sousTotal += nbItems * (decimal)produit["PrixDemande"];
                    poidsTotal += nbItems * (decimal)produit["Poids"];
                }

                myConnection.Open();

                SqlCommand commandeVendeur = new SqlCommand("SELECT Province, LivraisonGratuite, MaxLivraison FROM PPVendeurs WHERE NoVendeur = " + NoVendeur, myConnection);
                SqlDataReader lecteurVendeur = commandeVendeur.ExecuteReader();

                lecteurVendeur.Read();

                decimal livraisonGratuite = (decimal)lecteurVendeur["LivraisonGratuite"];
                String province = (String)lecteurVendeur["Province"];
                int poidsMax = (int)lecteurVendeur["MaxLivraison"];

                lecteurVendeur.Close();

                if (poidsTotal <= poidsMax)
                {
                    mvPartieBas.ActiveViewIndex = 0;
                }
                else
                {
                    mvPartieBas.ActiveViewIndex = 1;
                    lblPoidsMax.Text = "Le poids dépasse le maximum de " + poidsMax + " lbs.";
                }

                codeLivraison = short.Parse(ddlModesLivraison.SelectedValue);

                if (codeLivraison == 1 && sousTotal >= livraisonGratuite)
                {
                    prixLivraison = 0;
                }
                else
                {
                    SqlCommand commandeLivraison = new SqlCommand("SELECT P.Tarif FROM PPTypesPoids T INNER JOIN PPPoidsLivraisons P ON T.CodePoids = P.CodePoids WHERE P.CodeLivraison = " + codeLivraison + " AND " + poidsTotal.ToString().Replace(",", ".") + " BETWEEN T.PoidsMin AND T.PoidsMax", myConnection);
                    prixLivraison = (decimal)commandeLivraison.ExecuteScalar();
                }

                decimal prixAvecLivraison = sousTotal + prixLivraison;

                SqlCommand commandeTauxTPS = new SqlCommand("SELECT TOP(1) TauxTPS FROM PPTaxeFederale ORDER BY DateEffectiveTPS DESC", myConnection);
                decimal tauxTPS = ((decimal)commandeTauxTPS.ExecuteScalar()) / 100;

                SqlCommand commandeTauxTVQ = new SqlCommand("SELECT TOP(1) TauxTVQ FROM PPTaxeProvinciale ORDER BY DateEffectiveTVQ DESC", myConnection);
                decimal tauxTVQ = ((decimal)commandeTauxTVQ.ExecuteScalar()) / 100;

                prixTPS = prixAvecLivraison * tauxTPS;

                if (province == "QC")
                {
                    prixTVQ = prixAvecLivraison * tauxTVQ;
                }
                else
                {
                    prixTVQ = 0;
                }

                grandTotal = prixAvecLivraison + prixTPS + prixTVQ;

                lblTauxTPS.Text = "(" + tauxTPS.ToString("P3") + ")";
                lblTauxTVQ.Text = "(" + tauxTVQ.ToString("P3") + ")";
            }

            lblPoidsTotal.Text = poidsTotal.ToString() + " lbs.";
            lblSousTotal.Text = sousTotal.ToString("C");
            lblLivraison.Text = prixLivraison.ToString("C");
            lblTPS.Text = prixTPS.ToString("C");
            lblTVQ.Text = prixTVQ.ToString("C");
            lblGrandTotal.Text = grandTotal.ToString("C");

            myConnection.Close();
        }

        protected void ddlModesLivraison_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CalculerCouts();
        }

        public void ViderPanierEtCreerCommande()
        {
            CalculerCouts();

            myConnection.Open();

            SqlCommand commandeNoCommande = new SqlCommand("SELECT MAX(NoCommande) FROM PPCommandes", myConnection);

            long noCommande = (long)commandeNoCommande.ExecuteScalar() + 1;

            String values = String.Join(", ", noCommande, Session["ID"], NoVendeur, "'" + DateTime.Now + "'", prixLivraison, codeLivraison, grandTotal, prixTPS, prixTVQ, poidsTotal, "'l'", 1);

            SqlCommand commandePaiement = new SqlCommand("INSERT INTO PPCommandes VALUES (" + values + ")", myConnection);
            commandePaiement.ExecuteNonQuery();

            SqlCommand commandeViderPanier = new SqlCommand("DELETE FROM PPArticlesEnPanier WHERE NoClient = " + Session["ID"] + " AND NoVendeur = " + NoVendeur, myConnection);
            commandeViderPanier.ExecuteNonQuery();

            myConnection.Close();

            Response.Redirect("CommandesClient.aspx");
        }
    }
}