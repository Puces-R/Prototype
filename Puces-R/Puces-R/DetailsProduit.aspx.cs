using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Puces_R.Controles;
using System.Drawing;

namespace Puces_R
{
    public partial class DetailsProduit : System.Web.UI.Page
    {
        private bool DejaEvalue
        {
            get
            {
                return (bool)ViewState["DejaEvalue"];
            }
            set
            {
                ViewState["DejaEvalue"] = value;
            }
        }

        private long NoVendeur
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

        SqlConnection myConnection = Librairie.Connexion;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Librairie.Autorisation(false, true, true, false);

                int noProduit = Librairie.LireParametre<int>("noproduit");

                String whereClause = " WHERE P.noProduit = " + noProduit;
                if ((char)Session["Type"] == 'V')
                {
                    trQtt.Visible =
                    btnAjouterLaMienne.Visible =
                    btnAjouterPanier.Visible =
                    btnEnvoyerMessage.Visible = false;
                    whereClause += " AND P.NoVendeur = " + Session["ID"];
                }
                else
                {
                    btnModifierProduit.Visible =
                    btnSupprimerProduit.Visible = false;
                }

                SqlCommand commandeProduit = new SqlCommand("SELECT P.NoProduit,Photo,P.Description,C.Description AS Categorie,P.Nom,PrixDemande,PrixVente,NombreItems,Poids,P.DateCreation,P.DateMAJ,V.NoVendeur,NomAffaires FROM PPProduits P INNER JOIN PPCategories C ON C.NoCategorie = P.NoCategorie INNER JOIN PPVendeurs V ON P.NoVendeur = V.NoVendeur" + whereClause, myConnection);

                myConnection.Open();
                SqlDataReader lecteurProduit = commandeProduit.ExecuteReader();
                if (lecteurProduit.Read())
                {

                    Object photo = lecteurProduit["Photo"];
                    String urlImage;
                    if (photo is DBNull)
                    {
                        urlImage = "Images/image_non_disponible.png";
                    }
                    else
                    {
                        urlImage = "Images/Televerse/" + (String)photo;
                    }
                    this.imgProduit.ImageUrl = urlImage;

                    this.lblProduit.Text = (String)lecteurProduit["Nom"];
                    this.lblCategorie.Text = (String)lecteurProduit["Categorie"];
                    this.lblDescription.Text = (String)lecteurProduit["Description"];

                    decimal decPrixDemande = (decimal)lecteurProduit["PrixDemande"];

                    this.lblPrixDemande.Text = decPrixDemande.ToString("C");

                    Object objPrixVente = lecteurProduit["PrixVente"];
                    decimal decPrixVente;
                    if (objPrixVente is DBNull)
                    {
                        decPrixVente = decPrixDemande;
                    }
                    else
                    {
                        decPrixVente = (decimal)objPrixVente;
                    }
                    this.lblPrixEnVente.Text = decPrixVente.ToString("C");

                    short quantite = (short)lecteurProduit["NombreItems"];
                    if (quantite > 0)
                    {
                        lblQuantiteDisponible.Text = quantite.ToString();
                    }
                    else
                    {
                        lblQuantiteDisponible.Text = "En rupture de stock";
                        lblQuantiteDisponible.ForeColor = Color.Red;
                    }

                    this.lblDateCreation.Text = ((DateTime)lecteurProduit["DateCreation"]).ToShortDateString();

                    this.NoVendeur = (long)lecteurProduit["NoVendeur"];
                    Master.NoVendeur = this.NoVendeur;

                    object dateMAJ = lecteurProduit["DateMAJ"];
                    if (dateMAJ is DBNull)
                    {
                        this.lblDateMiseAJour.Text = "Jamais modifié";
                    }
                    else
                    {
                        this.lblDateMiseAJour.Text = lecteurProduit["DateMAJ"].ToString();
                    }
                }
                else
                {
                    myConnection.Close();
                    Response.Redirect(Chemin.UrlRetour == null ? ((char)Session["Type"] == 'V' ? "AccueilVendeur.aspx" : "AccueilClient.aspx") : Chemin.UrlRetour);
                }
                myConnection.Close();

                SqlDataAdapter adapteurEvaluations = new SqlDataAdapter("SELECT E.Cote, E.Commentaire, E.DateCreation, ISNULL(C.Prenom + ' ' + C.Nom, AdresseEmail) AS NomComplet FROM PPEvaluations E INNER JOIN PPClients C ON E.NoClient = C.NoClient WHERE E.NoProduit = " + noProduit + " AND C.NoClient != " + Session["ID"], myConnection);
                DataTable tableEvaluations = new DataTable();
                adapteurEvaluations.Fill(tableEvaluations);

                rptEvaluations.DataSource = tableEvaluations;
                rptEvaluations.DataBind();

                SqlCommand commandClient = new SqlCommand("SELECT ISNULL(Prenom + ' ' + Nom, AdresseEmail) AS NomComplet FROM PPClients WHERE NoClient = " + Session["ID"], myConnection);

                myConnection.Open();
                lblClient.Text = (String)commandClient.ExecuteScalar();
                myConnection.Close();

                SqlCommand commandeEvaluation = new SqlCommand("SELECT Cote, Commentaire, DateCreation FROM PPEvaluations WHERE NoProduit = " + noProduit + " AND NoClient = " + Session["ID"], myConnection);

                myConnection.Open();
                SqlDataReader lecteurEvaluation = commandeEvaluation.ExecuteReader();
                                
                DejaEvalue = lecteurEvaluation.Read();
                if (DejaEvalue && (char)Session["Type"] == 'C')
                {
                    ctrEtoiles.Cote = (decimal)lecteurEvaluation["Cote"];
                    txtCommentaire.Text = (String)lecteurEvaluation["Commentaire"];
                    afficherEvaluation();
                }

                myConnection.Close();
                
                calculerCoteMoyenne();
            }
        }

        private void calculerCoteMoyenne()
        {
            String noProduit = Request.Params["noproduit"];

            SqlCommand commandeMoyenne = new SqlCommand("SELECT AVG(Cote) AS Moyenne, COUNT(NoClient) AS NbEvaluations FROM PPEvaluations WHERE NoProduit = " + noProduit, myConnection);

            myConnection.Open();

            SqlDataReader lecteurMoyenne = commandeMoyenne.ExecuteReader();

            lecteurMoyenne.Read();

            int nbEvaluations = (int)lecteurMoyenne["NbEvaluations"];

            litNbEvaluations.Text = nbEvaluations.ToString();
            object objMoyenne = lecteurMoyenne["Moyenne"];
            if (!(objMoyenne is DBNull))
            {
                lblCoteMoyenne.Text = ((decimal)lecteurMoyenne["Moyenne"]).ToString("N1") + " / 5";
            }

            if (nbEvaluations > 0)
            {
                mvMoyenneOuMessage.ActiveViewIndex = 0;
            }
            else
            {
                mvMoyenneOuMessage.ActiveViewIndex = 1;
            }

            myConnection.Close();
        }

        protected void btnAjouterPanier_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (IsValid)
            {
                String nbItems = txtQuantite.Text;
                String noProduit = Request.Params["noproduit"];
                String noPanier = Session["ID"] + noProduit;

                myConnection.Open();

                SqlCommand commandeDejaPresent = new SqlCommand("SELECT COUNT(*) FROM PPArticlesEnPanier WHERE noPanier = " + noPanier, myConnection);
                bool dejaPresent = (int)commandeDejaPresent.ExecuteScalar() > 0;
                if (dejaPresent)
                {
                    SqlCommand commandeMAJQuantite = new SqlCommand("UPDATE PPArticlesEnPanier SET NbItems = " + nbItems + " WHERE NoPanier = " + noPanier, myConnection);
                    commandeMAJQuantite.ExecuteNonQuery();
                }
                else
                {
                    String dateCreation = DateTime.Now.ToShortDateString();
                    String values = String.Join(",", noPanier, Session["ID"], NoVendeur, noProduit, dateCreation, nbItems);
                    SqlCommand commandeAjout = new SqlCommand("INSERT INTO PPArticlesEnPanier VALUES (" + values + ")", myConnection);
                    commandeAjout.ExecuteNonQuery();
                }

                myConnection.Close();

                Response.Redirect(Chemin.Ajouter("Panier.aspx?noclient=" + Session["ID"] + "&novendeur=" + NoVendeur, "Retourner aux détails du produit"));
            }
        }

        protected void btnEnvoyerMessage_Click(object sender, EventArgs e)
        {
            String message = "Question sur le produit '" + lblProduit.Text + "' (#" + Request.Params["noproduit"] + ")";
            Librairie.Messagerie(new int[] { (int)NoVendeur }, message, null, true);
        }

        protected void rptEvaluations_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                Label lblClient = (Label)item.FindControl("lblClient");
                Etoiles ctrEtoiles = (Etoiles)item.FindControl("ctrEtoiles");
                Label lblCommentaire = (Label)item.FindControl("lblCommentaire");

                DataRowView drvPanier = (DataRowView)e.Item.DataItem;

                String nomComplet = (String)drvPanier["NomComplet"];
                decimal cote = (decimal)drvPanier["Cote"];
                String commentaire = (String)drvPanier["Commentaire"];

                lblClient.Text = nomComplet;
                ctrEtoiles.Cote = cote;
                lblCommentaire.Text = commentaire;
            }
        }

        protected void btnSoumettre_OnClick(object sender, EventArgs e)
        {
            if (txtCommentaire.Text.Length > 150)
            {
                lblErreurCommentaire.Text = "Limite de 150 caractères dépassée (" + txtCommentaire.Text.Length + ")";
            }
            else
            {
                lblErreurCommentaire.Text = "";
                String noProduit = Request.Params["noproduit"];

                SqlCommand commandeEvaluation;

                String commentaire = txtCommentaire.Text.Replace("'", "''");

                if (DejaEvalue)
                {
                    String valeurs = "Cote = " + ctrEtoiles.Cote.ToString().Replace(",", ".") + ", Commentaire = '" + commentaire + "', DateMaj = '" + DateTime.Now + "'";
                    commandeEvaluation = new SqlCommand("UPDATE PPEvaluations SET " + valeurs + " WHERE NoClient = " + Session["ID"] + " AND NoProduit = " + noProduit, myConnection);
                }
                else
                {
                    String valeurs = String.Join(", ", Session["ID"], noProduit, ctrEtoiles.Cote, "'" + commentaire + "'", "NULL", "'" + DateTime.Now + "'");
                    commandeEvaluation = new SqlCommand("INSERT INTO PPEvaluations VALUES (" + valeurs + ")", myConnection);
                }

                myConnection.Open();
                commandeEvaluation.ExecuteNonQuery();
                myConnection.Close();

                calculerCoteMoyenne();
                DejaEvalue = true;
            }
        }

        protected void btnAjouterLaMienne_OnClick(object sender, EventArgs e)
        {
            afficherEvaluation();
        }

        private void afficherEvaluation()
        {
            btnAjouterLaMienne.Visible = false;
            pnlEvaluation.Visible = true;
        }

        protected void valQuantite_OnServerValidate(object sender, ServerValidateEventArgs e)
        {
            short nbDemande;
            if (short.TryParse(e.Value, out nbDemande))
            {
                if (nbDemande < 1)
                {
                    e.IsValid = false;
                    valQuantite.Text = "Vous devez commander au moins un article";
                }
                else
                {
                    myConnection.Open();

                    SqlCommand commandeNbItems = new SqlCommand("SELECT NombreItems FROM PPProduits WHERE NoProduit = " + Request.Params["noproduit"], myConnection);
                    short nbDisponible = (short)commandeNbItems.ExecuteScalar();

                    myConnection.Close();

                    e.IsValid = (nbDemande <= nbDisponible);
                    valQuantite.Text = "Vous avez dépassé la quantité disponible";
                }
            }
            else
            {
                e.IsValid = false;
                valQuantite.Text = "Le format est invalide";
            }
        }

        protected void btnSupprimerProduit_Click(object sender, EventArgs e)
        {
            Response.Redirect("SuppressionProduits.aspx?noproduit=" + Request.Params["noproduit"]);
        }

        protected void btnModifierProduit_Click(object sender, EventArgs e)
        {
            Response.Redirect("ModificationProduits.aspx?noproduit=" + Request.Params["noproduit"]);
        }
    }
}