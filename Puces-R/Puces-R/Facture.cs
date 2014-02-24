using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace Puces_R
{
    [Serializable]
    public class Facture
    {
        SqlConnection myConnection = Librairie.Connexion;

        public long NoClient {get; private set;}
        public long NoVendeur { get; private set; }
        public decimal SousTotal { get; private set; }
        public decimal PoidsTotal { get; private set; }
        public decimal PoidsMaximal { get; private set; }
        public decimal PrixLivraison { get; private set; }
        public decimal PrixTPS { get; private set; }
        public decimal PrixTVQ { get; private set; }
        public decimal TauxTPS { get; private set; }
        public decimal TauxTVQ { get; private set; }
        public short CodeLivraison { get; private set; }

        public decimal GrandTotal
        {
            get
            {
                return SousTotal + PrixLivraison + PrixTPS + PrixTVQ;
            }
        }

        private Facture(short codeLivraison)
        {
            this.CodeLivraison = codeLivraison;
        }

        public Facture(long noClient, long noVendeur, short codeLivraison, bool commande = false) : this(noClient, noVendeur, codeLivraison, null, commande) { }

        public Facture(long noClient, long noVendeur, short codeLivraison) : this(noClient, noVendeur, codeLivraison, null) { }

        public Facture(long noClient, long noVendeur, short codeLivraison, String provinceClient, bool commande = false) : this(codeLivraison)
        {
            this.NoClient = noClient;
            this.NoVendeur = noVendeur;

            String whereClause = " WHERE A.NoClient = " + noClient + " AND P.NoVendeur = " + noVendeur;

            SqlDataAdapter adapteurProduits = new SqlDataAdapter("SELECT NbItems, " + (commande ? "ISNULL(PrixVente, PrixDemande)" : "PrixDemande") + " AS Prix, Poids FROM PPProduits P INNER JOIN PPCategories C ON C.NoCategorie = P.NoCategorie INNER JOIN PPArticlesEnPanier A ON A.NoProduit = P.NoProduit" + whereClause, myConnection);
            DataTable tableProduits = new DataTable();
            adapteurProduits.Fill(tableProduits);

            this.SousTotal = 0;
            this.PoidsTotal = 0;

            foreach (DataRow produit in tableProduits.Rows)
            {
                short nbItems = (short)produit["NbItems"];
                SousTotal += nbItems * (decimal)produit["Prix"];
                PoidsTotal += nbItems * (decimal)produit["Poids"];
            }

            myConnection.Open();

            SqlCommand commandeVendeur = new SqlCommand("SELECT Province, LivraisonGratuite, MaxLivraison, Taxes FROM PPVendeurs WHERE NoVendeur = " + noVendeur, myConnection);
            SqlDataReader lecteurVendeur = commandeVendeur.ExecuteReader();

            lecteurVendeur.Read();

            decimal livraisonGratuite = (decimal)lecteurVendeur["LivraisonGratuite"];
            String provinceVendeur = (String)lecteurVendeur["Province"];
            this.PoidsMaximal = (int)lecteurVendeur["MaxLivraison"];
            bool taxes = (bool)lecteurVendeur["Taxes"];

            lecteurVendeur.Close();

            if (codeLivraison == 1 && SousTotal >= livraisonGratuite)
            {
                PrixLivraison = 0;
            }
            else
            {
                SqlCommand commandeLivraison = new SqlCommand("SELECT P.Tarif FROM PPTypesPoids T INNER JOIN PPPoidsLivraisons P ON T.CodePoids = P.CodePoids WHERE P.CodeLivraison = " + codeLivraison + " AND " + PoidsTotal.ToString().Replace(",", ".") + " BETWEEN T.PoidsMin AND T.PoidsMax", myConnection);
                PrixLivraison = (decimal)commandeLivraison.ExecuteScalar();
            }

            decimal prixAvecLivraison = SousTotal + PrixLivraison;

            SqlCommand commandeTauxTPS = new SqlCommand("SELECT TOP(1) TauxTPS FROM PPTaxeFederale ORDER BY DateEffectiveTPS DESC", myConnection);
            this.TauxTPS = ((decimal)commandeTauxTPS.ExecuteScalar()) / 100;

            SqlCommand commandeTauxTVQ = new SqlCommand("SELECT TOP(1) TauxTVQ FROM PPTaxeProvinciale ORDER BY DateEffectiveTVQ DESC", myConnection);
            this.TauxTVQ = ((decimal)commandeTauxTVQ.ExecuteScalar()) / 100;

            if (taxes)
            {
                this.PrixTPS = prixAvecLivraison * TauxTPS;
                if (provinceVendeur == "QC")
                {
                    this.PrixTVQ = prixAvecLivraison * TauxTVQ;
                }
            }

            myConnection.Close();
        }

        public Facture(long noCommande, short codeLivraison) : this(codeLivraison)
        {
            myConnection.Open();

            SqlCommand commandeCommande = new SqlCommand("SELECT * FROM PPCommandes WHERE NoCommande = " + noCommande, myConnection);
            SqlDataReader lecteurCommande = commandeCommande.ExecuteReader();

            lecteurCommande.Read();

            this.NoVendeur = (long)lecteurCommande["NoVendeur"];
            this.PoidsTotal = (decimal)lecteurCommande["PoidsTotal"];
            this.CodeLivraison = (short)lecteurCommande["TypeLivraison"];
            this.PrixLivraison = (decimal)lecteurCommande["Livraison"];
            this.PrixTPS = (decimal)lecteurCommande["TPS"];
            this.PrixTVQ = (decimal)lecteurCommande["TVQ"];
            this.SousTotal = (decimal)lecteurCommande["MontantTotal"];

            lecteurCommande.Close();
            myConnection.Close();
        }
    }
}