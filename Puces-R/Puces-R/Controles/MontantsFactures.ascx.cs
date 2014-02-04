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
        decimal prixLivraison;
        decimal prixTPS;
        decimal prixTVQ;
        decimal grandTotal;

        public bool Enabled
        {
            set
            {
                ddlModesLivraison.Enabled = value;
            }
        }

        public decimal GrandTotal
        {
            get
            {
                return grandTotal;
            }
        }

        public String MessageErreur
        {
            get
            {
                return lblMessageErreur.Text;
            }
            set
            {
                mvPartieBas.ActiveViewIndex = (value == null ? 0 : 1);
                lblMessageErreur.Text = value;
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

        public short CodeLivraison
        {
            get
            {
                if (ViewState["CodeLivraison"] == null)
                {
                    return 1;
                }
                else
                {
                    return (short)ViewState["CodeLivraison"];
                }
            }
            set
            {
                ViewState["CodeLivraison"] = value;
            }
        }

        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChargerModesDeLivraison();
            }
        }

        public void ChargerModesDeLivraison()
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

        public void CalculerCouts()
        {
            if (Session["ID"] == null)
            {
                Response.Redirect("Default.aspx", true);
            }

            if (ViewState["NoCommande"] != null)
            {
                mvPartieBas.ActiveViewIndex = 0;

                myConnection.Open();

                SqlCommand commandeCommande = new SqlCommand("SELECT * FROM PPCommandes WHERE NoCommande = " + NoCommande, myConnection);
                SqlDataReader lecteurCommande = commandeCommande.ExecuteReader();

                lecteurCommande.Read();

                NoVendeur = (long)lecteurCommande["NoVendeur"];
                poidsTotal = (decimal)lecteurCommande["PoidsTotal"];
                CodeLivraison = (short)lecteurCommande["TypeLivraison"];
                prixLivraison = (decimal)lecteurCommande["Livraison"];
                prixTPS = (decimal)lecteurCommande["TPS"];
                prixTVQ = (decimal)lecteurCommande["TVQ"];
                grandTotal = (decimal)lecteurCommande["MontantTotal"];

                lecteurCommande.Close();
                myConnection.Close();
            }
            else if (ViewState["NoVendeur"] != null)
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

                if (poidsTotal > poidsMax)
                {
                    MessageErreur = "Le poids dépasse le maximum de " + poidsMax + " lbs.";
                }
                
                if (CodeLivraison == 1 && sousTotal >= livraisonGratuite)
                {
                    prixLivraison = 0;
                }
                else
                {
                    SqlCommand commandeLivraison = new SqlCommand("SELECT P.Tarif FROM PPTypesPoids T INNER JOIN PPPoidsLivraisons P ON T.CodePoids = P.CodePoids WHERE P.CodeLivraison = " + CodeLivraison + " AND " + poidsTotal.ToString().Replace(",", ".") + " BETWEEN T.PoidsMin AND T.PoidsMax", myConnection);
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

            ddlModesLivraison.SelectedValue = CodeLivraison.ToString();

            myConnection.Close();
        }

        protected void ddlModesLivraison_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CodeLivraison = short.Parse(ddlModesLivraison.SelectedValue);
            ChargerModesDeLivraison();
        }

        public void EffectuerTransaction()
        {
            myConnection.Open();

            SqlCommand commandeNoCommande = new SqlCommand("SELECT MAX(NoCommande) FROM PPCommandes", myConnection);

            long noCommande = (long)commandeNoCommande.ExecuteScalar() + 1;

            SqlCommand commandePaiement = new SqlCommand("INSERT INTO PPCommandes VALUES (@noCommande, @noClient, @noVendeur, @dateCommande, @livraison, @typeLivraison, @montantTotal, @TPS, @TVQ, @poidsTotal, @statut, @noAutorisation)", myConnection);

            SqlParameterCollection parameters = commandePaiement.Parameters;

            parameters.Add(new SqlParameter("noCommande", noCommande));
            parameters.Add(new SqlParameter("noClient", Session["ID"]));
            parameters.Add(new SqlParameter("noVendeur", NoVendeur));
            parameters.Add(new SqlParameter("dateCommande", DateTime.Now));
            parameters.Add(new SqlParameter("livraison", prixLivraison));
            parameters.Add(new SqlParameter("typeLivraison", CodeLivraison));
            parameters.Add(new SqlParameter("montantTotal", grandTotal));
            parameters.Add(new SqlParameter("TPS", prixTPS));
            parameters.Add(new SqlParameter("TVQ", prixTVQ));
            parameters.Add(new SqlParameter("poidsTotal", poidsTotal));
            parameters.Add(new SqlParameter("statut", "p"));
            parameters.Add(new SqlParameter("noAutorisation", 1));

            commandePaiement.ExecuteNonQuery();

            SqlCommand commandeViderPanier = new SqlCommand("DELETE FROM PPArticlesEnPanier WHERE NoClient = " + Session["ID"] + " AND NoVendeur = " + NoVendeur, myConnection);
            commandeViderPanier.ExecuteNonQuery();

            myConnection.Close();
        }
    }
}