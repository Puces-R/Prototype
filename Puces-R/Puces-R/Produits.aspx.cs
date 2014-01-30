using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Puces_R
{
    public partial class Produits : System.Web.UI.Page
    {
        private int noVendeur;
        private int noCategorie;

        private int NbPages
        {
            get
            {
                return (int)ViewState["NbPages"];
            }
            set
            {
                ViewState["NbPages"] = value;
            }
        }

        private int PageActuelle
        {
            get
            {
                return (int)ViewState["PageActuelle"];
            }
            set
            {
                ViewState["PageActuelle"] = value;
            }
        }
        
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageActuelle = 0;
 
                chargerProduits();

                String whereClause = String.Empty;

                if (noVendeur != -1)
                {
                    whereClause =  " WHERE P.NoVendeur = " + noVendeur;
                }

                SqlDataAdapter adapteurCategories = new SqlDataAdapter("SELECT DISTINCT C.Description, C.NoCategorie FROM PPCategories C INNER JOIN PPProduits P ON C.NoCategorie = P.NoCategorie" + whereClause, myConnection);
                DataTable tableCategories = new DataTable();
                adapteurCategories.Fill(tableCategories);

                ddlCategorie.DataSource = tableCategories;
                ddlCategorie.DataTextField = "Description";
                ddlCategorie.DataValueField = "NoCategorie";
                ddlCategorie.DataBind();
                ddlCategorie.Items.Add(new ListItem("Toutes", "-1"));
                ddlCategorie.SelectedValue = noCategorie.ToString();

                if (noVendeur == -1)
                {
                    ((SiteMaster)Master).Titre = "Catalogue Global";
                }
                else
                {
                    ((SiteMaster)Master).NoVendeur = noVendeur;
                }
            }
        }

        protected void dtlProduits_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            DataListItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                HyperLink hypDescriptionAbregee = (HyperLink)item.FindControl("hypDescriptionAbregee");
                Label lblNoProduit = (Label)item.FindControl("lblNoProduit");
                Image imgProduit = (Image)item.FindControl("imgProduit");
                Label lblCategorie = (Label)item.FindControl("lblCategorie");
                Label lblPrixDemande = (Label)item.FindControl("lblPrixDemande");
                Label lblQuantite = (Label)item.FindControl("lblQuantite");

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

                lblNoProduit.Text = "No. " + noProduit.ToString();
                imgProduit.ImageUrl = urlImage;
                hypDescriptionAbregee.Text = strDescriptionAbregee;
                hypDescriptionAbregee.NavigateUrl = "DetailsProduit.aspx?noproduit=" + noProduit;
                lblCategorie.Text = strCategorie;
                lblPrixDemande.Text = "Prix demandé: " + decPrixDemande.ToString("C");
                lblQuantite.Text = "Quantité: " + intQuantite.ToString();
            }
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

            if (int.TryParse(Request.Params["novendeur"], out noVendeur))
            {
                whereParts.Add("P.NoVendeur = " + noVendeur);
            }
            else
            {
                noVendeur = -1;
            }

            if (!IsPostBack)
            {
                if (noVendeur != -1)
                {
                    ctrMenu.NoVendeur = noVendeur;
                }
            }

            if (IsPostBack)
            {
                noCategorie = int.Parse(ddlCategorie.SelectedValue);
                if (noCategorie != -1)
                {
                    whereParts.Add("P.NoCategorie = " + noCategorie);
                }
            }
            else
            {
                if (int.TryParse(Request.Params["nocategorie"], out noCategorie))
                {
                    whereParts.Add("P.NoCategorie = " + noCategorie);
                }
                else
                {
                    noCategorie = -1;
                }
            }

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
                    orderByClause += "P.DateCreation";
                    break;
            }

            SqlDataAdapter adapteurProduits = new SqlDataAdapter("SELECT NoProduit,Photo,C.Description,Nom,PrixDemande,NombreItems FROM PPProduits P INNER JOIN PPCategories C ON C.NoCategorie = P.NoCategorie" + whereClause + orderByClause, myConnection);
            DataTable tableProduits = new DataTable();
            adapteurProduits.Fill(tableProduits);

            PagedDataSource objPds = new PagedDataSource();
            objPds.DataSource = new DataView(tableProduits);
            objPds.AllowPaging = true;
            objPds.PageSize = int.Parse(ddlParPage.SelectedValue);
            objPds.CurrentPageIndex = PageActuelle;

            NbPages = objPds.PageCount;

            dtlProduits.DataSource = objPds;
            dtlProduits.DataBind();

            pnlLeftNavigation.Visible = (PageActuelle > 0);
            pnlRightNavigation.Visible = (PageActuelle < NbPages - 1);
            pnlLigneNavigation.Visible = pnlLeftNavigation.Visible || pnlRightNavigation.Visible;
        }

        protected void btnFirst_OnClick(object sender, EventArgs e)
        {
            PageActuelle = 0;
            chargerProduits();
        }

        protected void btnPrevious_OnClick(object sender, EventArgs e)
        {
            PageActuelle -= 1;
            chargerProduits();
        }

        protected void btnNext_OnClick(object sender, EventArgs e)
        {
            PageActuelle += 1;
            chargerProduits();
        }

        protected void btnLast_OnClick(object sender, EventArgs e)
        {
            PageActuelle = NbPages - 1;
            chargerProduits();
        }

        protected void btnRecherche_OnClick(object sender, EventArgs e)
        {
            PageActuelle = 0;
            chargerProduits();
        }

        protected void ddlTrierPar_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PageActuelle = 0;
            chargerProduits();
        }

        protected void ddlParPage_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PageActuelle = 0;
            chargerProduits();
        }

        protected void ddlCategorie_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PageActuelle = 0;
            chargerProduits();
        }
    }
}