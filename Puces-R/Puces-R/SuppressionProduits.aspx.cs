﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class SuppressionProduits : System.Web.UI.Page
    {
        int noProduit = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Librairie.Autorisation(false, false, true, false);
            }
            if (!int.TryParse(Request.Params["noproduit"], out noProduit))
            {

                Response.Redirect(Chemin.UrlRetour == null ? "AccueilVendeur.aspx" : Chemin.UrlRetour);
            }
            else
            {
                chargerCategorie();
                chargerDonnees();

                if (verifierSiProduitDansPanier())
                {
                    lblAvertissement.Text = "LE PRODUIT EST PRÉSENTEMENT DANS LE PANIER D'UN CLIENT!";
                }
                //Response.Write(noProduit.ToString());
            }

        }

        protected void supprimerProduits(object sender, EventArgs e)
        {
            String maChaineDeConnexion = "Data Source=sqlinfo.cgodin.qc.ca;Initial Catalog=BD6B8_424R;Persist Security Info=True;User ID=6B8equipe424r;Password=Password2";
            SqlConnection maConnexion = new SqlConnection();
            maConnexion.ConnectionString = maChaineDeConnexion;
            maConnexion.Open();

            SqlCommand maCommande;
            if (verifierSiProduitEstCommande())
            {
                maCommande = new SqlCommand("UPDATE PPProduits SET NombreItems=0,Disponibilité=0 where NoProduit=" + noProduit, maConnexion);
                maCommande.ExecuteNonQuery();

                Response.Redirect("GestionProduits.aspx");

            }
            else
            {
                maCommande = new SqlCommand("DELETE FROM PPProduits WHERE NoProduit=" + noProduit, maConnexion);
                maCommande.ExecuteNonQuery();

                Response.Redirect("GestionProduits.aspx");

            }
            maConnexion.Close();
        }
        protected bool verifierSiProduitEstCommande()
        {
            bool produitDansCommande = false;
            SqlConnection dbConn = new SqlConnection();
            String maChaineDeConnexion = "Data Source=sqlinfo.cgodin.qc.ca;Initial Catalog=BD6B8_424R;Persist Security Info=True;User ID=6B8equipe424r;Password=Password2";
            SqlConnection maConnexion = new SqlConnection();
            maConnexion.ConnectionString = maChaineDeConnexion;
            maConnexion.Open();

            SqlCommand maCommande = new SqlCommand("select * from PPDetailsCommandes where NoProduit=" + noProduit, maConnexion);
            object rep = maCommande.ExecuteScalar();
            if (rep != null)
            {
                produitDansCommande = true;
            }
            else
            {
                produitDansCommande = false;
            }
            maConnexion.Close();
            return produitDansCommande;

        }

        protected bool verifierSiProduitDansPanier()
        {
            bool produitDansPanier = false;
            SqlConnection dbConn = new SqlConnection();
            String maChaineDeConnexion = "Data Source=sqlinfo.cgodin.qc.ca;Initial Catalog=BD6B8_424R;Persist Security Info=True;User ID=6B8equipe424r;Password=Password2";
            SqlConnection maConnexion = new SqlConnection();
            maConnexion.ConnectionString = maChaineDeConnexion;
            maConnexion.Open();

            SqlCommand maCommande = new SqlCommand("select * from PPArticlesEnPanier where NoProduit=" + noProduit, maConnexion);
            object rep = maCommande.ExecuteScalar();
            if (rep != null)
            {
                produitDansPanier = true;
            }
            else
            {
                produitDansPanier = false;
            }
            maConnexion.Close();
            return produitDansPanier;
        }

        protected void chargerDonnees()
        {
            SqlConnection dbConn = new SqlConnection();
            String maChaineDeConnexion = "Data Source=sqlinfo.cgodin.qc.ca;Initial Catalog=BD6B8_424R;Persist Security Info=True;User ID=6B8equipe424r;Password=Password2";
            SqlConnection maConnexion = new SqlConnection();
            maConnexion.ConnectionString = maChaineDeConnexion;
            maConnexion.Open();

            SqlCommand maCommande = new SqlCommand("select * from PPProduits where NoProduit=" + noProduit + " AND NoVendeur = " + Session["ID"], maConnexion);
            object rep = maCommande.ExecuteScalar();

            if (rep != null)
            {

                SqlDataReader repT = maCommande.ExecuteReader();
                while (repT.Read())
                {
                    int categorie = (int)repT[2];
                    LoaderCategorie(categorie);

                    tbDescAbregee.Text = (String)repT["Nom"];
                    tbDescComplete.Text = (String)repT["Description"];
                    object photo = repT["Photo"];
                    if (!(photo is DBNull))
                    {
                        imgProduits.ImageUrl = "Images/Televerse/" + photo.ToString();
                    }
                    else
                    {
                        imgProduits.ImageUrl = "Images/image_non_disponible.png";
                    }
                    tbPrix.Text = Convert.ToString((Decimal)repT["PrixDemande"]);
                    tbNbItems.Text = Convert.ToString((Int16)repT["NombreItems"]);
                    Boolean dispo = (Boolean)repT["Disponibilité"];
                    if (dispo)
                    {
                        cbDisponibilite.Checked = true;
                    }
                    else
                    {
                        cbDisponibilite.Checked = false;
                    }

                    //Double prixV = (Decimal)repT[10];
                    tbPois.Text = Convert.ToString((Decimal)repT["Poids"]);
                    //DateTime dateCreation = (DateTime)repT[12];

                }
            }
            else
            {
                Response.Redirect(Chemin.UrlRetour == null ? "AccueilVendeur.aspx" : Chemin.UrlRetour);
            }
            maConnexion.Close();
        }

        public void LoaderCategorie(int numero)
        {

            switch (numero)
            {
                case 10: ddlCategorieProduits.SelectedIndex = 1; break;
                case 20: ddlCategorieProduits.SelectedIndex = 2; break;
                case 30: ddlCategorieProduits.SelectedIndex = 3; break;
                case 40: ddlCategorieProduits.SelectedIndex = 4; break;
                case 50: ddlCategorieProduits.SelectedIndex = 5; break;
                case 60: ddlCategorieProduits.SelectedIndex = 6; break;
                case 70: ddlCategorieProduits.SelectedIndex = 7; break;
                case 80: ddlCategorieProduits.SelectedIndex = 8; break;
            }




        }

        public void chargerCategorie()
        {
            SqlConnection dbConn = new SqlConnection();
            String maChaineDeConnexion = "Data Source=sqlinfo.cgodin.qc.ca;Initial Catalog=BD6B8_424R;Persist Security Info=True;User ID=6B8equipe424r;Password=Password2";
            SqlConnection maConnexion = new SqlConnection();
            maConnexion.ConnectionString = maChaineDeConnexion;
            maConnexion.Open();

            SqlCommand maCommande = new SqlCommand("select * from PPCategories ", maConnexion);
            SqlDataReader rep = maCommande.ExecuteReader();

            ddlCategorieProduits.Items.Add("");
            while (rep.Read())
            {
                String nom = (String)rep[1];
                ddlCategorieProduits.Items.Add(nom);
            }
            maConnexion.Close();



        }
    }
}