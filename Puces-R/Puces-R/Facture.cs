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
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        public long NoClient {get; private set;}
        public long NoVendeur { get; private set; }
        public decimal SousTotal { get; private set; }
        public decimal PoidsTotal { get; private set; }
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

        public Facture(long noClient, long noVendeur, short codeLivraison) : this(codeLivraison)
        {
            this.NoClient = noClient;
            this.NoVendeur = noVendeur;

            String whereClause = " WHERE A.NoClient = " + noClient+ " AND P.NoVendeur = " + noVendeur;

            SqlDataAdapter adapteurProduits = new SqlDataAdapter("SELECT NbItems, PrixDemande, Poids FROM PPProduits P INNER JOIN PPCategories C ON C.NoCategorie = P.NoCategorie INNER JOIN PPArticlesEnPanier A ON A.NoProduit = P.NoProduit" + whereClause, myConnection);
            DataTable tableProduits = new DataTable();
            adapteurProduits.Fill(tableProduits);

            this.SousTotal = 0;
            this.PoidsTotal = 0;

            foreach (DataRow produit in tableProduits.Rows)
            {
                short nbItems = (short)produit["NbItems"];
                SousTotal += nbItems * (decimal)produit["PrixDemande"];
                PoidsTotal += nbItems * (decimal)produit["Poids"];
            }

            myConnection.Open();

            SqlCommand commandeVendeur = new SqlCommand("SELECT Province, LivraisonGratuite, MaxLivraison FROM PPVendeurs WHERE NoVendeur = " + noVendeur, myConnection);
            SqlDataReader lecteurVendeur = commandeVendeur.ExecuteReader();

            lecteurVendeur.Read();

            decimal livraisonGratuite = (decimal)lecteurVendeur["LivraisonGratuite"];
            String province = (String)lecteurVendeur["Province"];
            int poidsMax = (int)lecteurVendeur["MaxLivraison"];

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

            this.PrixTPS = prixAvecLivraison * TauxTPS;

            if (province == "QC")
            {
                this.PrixTVQ = prixAvecLivraison * TauxTVQ;
            }
            else
            {
                this.PrixTVQ = 0;
            }
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