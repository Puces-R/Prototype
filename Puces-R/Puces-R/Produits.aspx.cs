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
    public partial class Produits : System.Web.UI.Page
    {
        private String noVendeurs;
        private String noCategories;
        
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        private bool RechercheAvance
        {
            get
            {
                return (bool)ViewState["RechercheAvance"];
            }
            set
            {
                ViewState["RechercheAvance"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.ChargerItems += chargerProduits;

            if (!IsPostBack)
            {
                RechercheAvance = false;

                chargerProduits();
                
                SqlDataAdapter adapteurCategories = new SqlDataAdapter("SELECT DISTINCT C.Description, C.NoCategorie FROM PPCategories C INNER JOIN PPProduits P ON C.NoCategorie = P.NoCategorie WHERE Disponibilité = 1", myConnection);
                DataTable tableCategories = new DataTable();
                adapteurCategories.Fill(tableCategories);
                AjouterCategories(ddlCategorie, tableCategories);
                ddlCategorie.Items.Add(new ListItem("Toutes", "-1"));
                AjouterCategories(cblCategorie, tableCategories);

                SqlDataAdapter adapteurVendeurs = new SqlDataAdapter("SELECT DISTINCT V.NomAffaires, V.NoVendeur FROM PPVendeurs V INNER JOIN PPProduits P On V.NoVendeur = P.Novendeur WHERE Disponibilité = 1", myConnection);
                DataTable tableVendeurs = new DataTable();
                adapteurVendeurs.Fill(tableVendeurs);
                AjouterVendeurs(ddlVendeur, tableVendeurs);
                ddlVendeur.Items.Add(new ListItem("Tous", "-1"));
                AjouterVendeurs(cblVendeur, tableVendeurs);

                Master.AfficherPremierePage();
            }
        }

        private void AjouterCategories(ListControl controle, DataTable tableCategories)
        {
            controle.DataSource = tableCategories;
            controle.DataTextField = "Description";
            controle.DataValueField = "NoCategorie";
            controle.DataBind();
            controle.SelectedValue = noCategories;
        }

        private void AjouterVendeurs(ListControl controle, DataTable tableVendeurs)
        {
            controle.DataSource = tableVendeurs;
            controle.DataTextField = "NomAffaires";
            controle.DataValueField = "NoVendeur";
            controle.DataBind();
            controle.SelectedValue = noVendeurs;
        }

        protected void dtlProduits_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            DataListItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                HyperLink hypDescriptionAbregee = (HyperLink)item.FindControl("hypDescriptionAbregee");
                Label lblNoProduit = (Label)item.FindControl("lblNoProduit");
                System.Web.UI.WebControls.Image imgProduit = (System.Web.UI.WebControls.Image)item.FindControl("imgProduit");
                Label lblCategorie = (Label)item.FindControl("lblCategorie");
                Label lblPrixDemande = (Label)item.FindControl("lblPrixDemande");
                Label lblQuantite = (Label)item.FindControl("lblQuantite");
                Label lblEvaluation = (Label)item.FindControl("lblEvaluation");

                DataRowView drvFilm = (DataRowView)e.Item.DataItem;

                long noProduit = (long)drvFilm["NoProduit"];

                Object photo = drvFilm["Photo"];
                String urlImage;
                if (photo is DBNull)
                {
                    urlImage = "Images/image_non_disponible.png";
                }
                else
                {
                    urlImage = "Images/Televerse/" + (String)photo;
                }
                String strCategorie = (String)drvFilm["Description"];
                String strDescriptionAbregee = (String)drvFilm["Nom"];
                decimal decPrixDemande = (decimal)drvFilm["PrixDemande"];
                short intQuantite = (short)drvFilm["NombreItems"];

                if (drvFilm["Evaluation"] is DBNull)
                {
                    lblEvaluation.Text = "Aucune évaluation";
                }
                else
                {
                    decimal evaluation = (decimal)drvFilm["Evaluation"];
                    lblEvaluation.Text = "Cote moyenne: " + evaluation.ToString("N1") + " / 5";
                }
                

                lblNoProduit.Text = "No. " + noProduit.ToString();
                imgProduit.ImageUrl = urlImage;
                hypDescriptionAbregee.Text = strDescriptionAbregee;
                hypDescriptionAbregee.NavigateUrl = Chemin.Ajouter("DetailsProduit.aspx?noproduit=" + noProduit, "Retour aux produits");
                lblCategorie.Text = strCategorie;
                lblPrixDemande.Text = "Prix demandé: " + decPrixDemande.ToString("C");
                if (intQuantite > 0)
                {
                    lblQuantite.Text = "Quantité: " + intQuantite.ToString();
                }
                else
                {
                    lblQuantite.Text = "En rupture de stock";
                    lblQuantite.ForeColor = Color.Red;
                    hypDescriptionAbregee.Enabled = false;
                }
            }
        }

        private void chargerProduits(object sender, EventArgs e)
        {
            chargerProduits();
        }

        private void chargerProduits()
        {
            List<String> whereParts = new List<String>();

            if (txtCritereRecherche.Text != string.Empty)
            {
                String colonne = "P.DateCreation";
                switch (ddlTypeRecherche.SelectedIndex)
                {
                    case 0:
                        colonne = "P.DateCreation";
                        break;
                    case 1:
                        colonne = "P.NoProduit";
                        break;
                    case 2:
                        colonne = "P.Description";
                        break;
                }
                whereParts.Add(colonne + " LIKE '%" + txtCritereRecherche.Text + "%'");
            }

            if (IsPostBack)
            {
                if (RechercheAvance)
                {
                    List<String> tabVendeurs = new List<String>();
                    foreach (ListItem item in cblVendeur.Items)
                    {
                        if (item.Selected) tabVendeurs.Add(item.Value);
                    }
                    if (tabVendeurs.Count > 0)
                    {
                        noVendeurs = String.Join(",", tabVendeurs);
                    }
                }
                else if (ddlVendeur.SelectedValue != "-1")
                {
                    noVendeurs = ddlVendeur.SelectedValue;
                }
            }
            else
            {
                noVendeurs = Request.Params["novendeur"];
            }
            if (noVendeurs != null)
            {
                whereParts.Add("P.NoVendeur IN (" + noVendeurs + ")");
            }

            if (IsPostBack)
            {
                if (RechercheAvance)
                {
                    List<String> tabCategories = new List<String>();
                    foreach (ListItem item in cblCategorie.Items)
                    {
                        if (item.Selected) tabCategories.Add(item.Value);
                    }
                    if (tabCategories.Count > 0)
                    {
                        noCategories = String.Join(",", tabCategories); 
                    }
                }
                else if (ddlCategorie.SelectedValue != "-1")
                {
                    noCategories = ddlCategorie.SelectedValue;
                }
            }
            else
            {
                noCategories = Request.Params["nocategorie"];
            }
            if (noCategories != null)
            {
                whereParts.Add("P.NoCategorie IN (" + noCategories + ")");
            }

            whereParts.Add("P.Disponibilité = 1");

            String whereClause;
            if (whereParts.Count > 0)
            {
                whereClause = " WHERE " + string.Join(" AND ", whereParts);
            }
            else
            {
                whereClause = string.Empty;
            }

            String orderByClause = " ORDER BY ";
            switch (ddlTrierPar.SelectedIndex)
            {
                case 0:
                    orderByClause += "P.NoProduit";
                    break;
                case 1:
                    orderByClause += "C.Description";
                    break;
                case 2:
                    orderByClause += "P.DateCreation DESC";
                    break;
                case 3:
                    orderByClause += "Evaluation DESC";
                    break;
            }

            SqlDataAdapter adapteurProduits = new SqlDataAdapter("SELECT P.NoProduit, P.Photo, C.Description, P.Nom, P.PrixDemande, P.NombreItems, P.DateCreation, AVG(E.Cote) AS Evaluation FROM PPProduits P INNER JOIN PPCategories C ON C.NoCategorie = P.NoCategorie LEFT JOIN PPEvaluations E ON E.NoProduit = P.NoProduit" + whereClause + " GROUP BY P.NoProduit, P.Photo, C.Description, P.Nom, P.PrixDemande, P.NombreItems, P.DateCreation" + orderByClause, myConnection);
            DataTable tableProduits = new DataTable();
            adapteurProduits.Fill(tableProduits);

            PagedDataSource objPds = new PagedDataSource();
            objPds.DataSource = new DataView(tableProduits);
            objPds.AllowPaging = true;
            objPds.PageSize = int.Parse(ddlParPage.SelectedValue);
            objPds.CurrentPageIndex = Master.PageActuelle;

            Master.NbPages = objPds.PageCount;

            dtlProduits.DataSource = objPds;
            dtlProduits.DataBind();
        }

        protected void AfficherPremierePage(object sender, EventArgs e)
        {
            Master.AfficherPremierePage();
        }

        protected void btnRechercheAvance_OnClick(object sender, EventArgs e)
        {
            RechercheAvance = !RechercheAvance;

            mvCategorie.ActiveViewIndex = RechercheAvance ? 1 : 0;
            mvVendeur.ActiveViewIndex = RechercheAvance ? 1 : 0;

            Master.AfficherPremierePage();
        }
    }
}