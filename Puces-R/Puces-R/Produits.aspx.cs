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
using System.Text.RegularExpressions;

namespace Puces_R
{
    public partial class Produits : System.Web.UI.Page
    {
        private String noVendeurs;
        private String noCategories;

        SqlConnection myConnection = Librairie.Connexion;

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
                Librairie.Autorisation(false, true, false, false);
                RechercheAvance = false;

                chargerProduits();
                
                SqlDataAdapter adapteurCategories = new SqlDataAdapter("SELECT DISTINCT C.Description, C.NoCategorie FROM PPCategories C INNER JOIN PPProduits P ON C.NoCategorie = P.NoCategorie WHERE Disponibilité = 1", myConnection);
                DataTable tableCategories = new DataTable();
                adapteurCategories.Fill(tableCategories);
                AjouterCategories(ddlCategorie, tableCategories);
                AjouterCategories(cblCategorie, tableCategories);
                ddlCategorie.Items.Add(new ListItem("Toutes", "-1"));
                Librairie.InitialiserListe("nocategorie", ddlCategorie);

                SqlDataAdapter adapteurVendeurs = new SqlDataAdapter("SELECT DISTINCT V.NomAffaires, V.NoVendeur FROM PPVendeurs V INNER JOIN PPProduits P On V.NoVendeur = P.Novendeur WHERE Disponibilité = 1", myConnection);
                DataTable tableVendeurs = new DataTable();
                adapteurVendeurs.Fill(tableVendeurs);
                AjouterVendeurs(ddlVendeur, tableVendeurs);
                AjouterVendeurs(cblVendeur, tableVendeurs);
                ddlVendeur.Items.Add(new ListItem("Tous", "-1"));
                Librairie.InitialiserListe("novendeur", ddlVendeur);
                
                Master.AfficherPremierePage();
            }
            else if (Request.Params.Get("__EVENTTARGET") == "MainContent_udpProduits")
            {
                Master.AfficherPremierePage();
            }

            Master.ScriptManager.RegisterAsyncPostBackControl(cblCategorie);
            Master.ScriptManager.RegisterAsyncPostBackControl(cblVendeur);
            Master.ScriptManager.RegisterAsyncPostBackControl(ddlTypeRecherche);
            Master.ScriptManager.RegisterAsyncPostBackControl(txtCritereRecherche);
            Master.ScriptManager.RegisterAsyncPostBackControl(ddlTrierPar);
            Master.ScriptManager.RegisterAsyncPostBackControl(ddlParPage);
            Master.ScriptManager.RegisterAsyncPostBackControl(txtAPartirDe);
            Master.ScriptManager.RegisterAsyncPostBackControl(txtJusquA);
        }

        private void AjouterCategories(ListControl controle, DataTable tableCategories)
        {
            controle.DataSource = tableCategories;
            controle.DataTextField = "Description";
            controle.DataValueField = "NoCategorie";
            controle.DataBind();
        }

        private void AjouterVendeurs(ListControl controle, DataTable tableVendeurs)
        {
            controle.DataSource = tableVendeurs;
            controle.DataTextField = "NomAffaires";
            controle.DataValueField = "NoVendeur";
            controle.DataBind();
        }

        protected void dtlProduits_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            DataListItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                BoiteProduit ctrProduit = (BoiteProduit)item.FindControl("ctrProduit");
                
                DataRowView drvProduit = (DataRowView)e.Item.DataItem;

                ctrProduit.NoProduit = (long)drvProduit["NoProduit"];
                ctrProduit.NoSequentiel = Master.PageActuelle * int.Parse(ddlParPage.SelectedValue) + item.ItemIndex + 1;
            }
        }

        private void chargerProduits(object sender, EventArgs e)
        {
            chargerProduits();
        }

        private void chargerProduits()
        {
            List<String> whereParts = new List<String>();

            if (txtCritereRecherche.Text.Trim() != string.Empty)
            {
                String colonne = "P.DateCreation";
                switch (ddlTypeRecherche.SelectedIndex)
                {
                    case 0:
                        colonne = "P.Nom";
                        break;
                    case 1:
                        colonne = "P.NoProduit";
                        break;
                }
                whereParts.Add(colonne + " LIKE @critere");
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
                String parametre = Request.Params["novendeur"];
                if (parametre != null && Regex.IsMatch(parametre, "^\\d+(?:,\\d+)*$"))
                {
                    noVendeurs = parametre;
                }
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

            if (RechercheAvance)
            {
                DateTime aPartirDe;
                DateTime jusquA;

                if (Librairie.LireEtValiderPlage(txtAPartirDe.Text, out aPartirDe))
                {
                    if (Librairie.LireEtValiderPlage(txtJusquA.Text, out jusquA))
                    {
                        whereParts.Add("P.DateCreation BETWEEN '" + txtAPartirDe.Text + "' AND '" + txtJusquA.Text + "'");
                    }
                    else
                    {                   
                        whereParts.Add("P.DateCreation > '" + txtAPartirDe.Text + "'");
                    }
                }
                else
                {
                    if (Librairie.LireEtValiderPlage(txtJusquA.Text, out jusquA))
                    {
                        whereParts.Add("P.DateCreation < '" + txtJusquA.Text + "'");
                    }
                    else
                    {
                        txtJusquA.Text = string.Empty;
                    }
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
                case 3:
                    orderByClause += "Evaluation";
                    break;
            }
            orderByClause += " " + ddlOrdre.SelectedValue;
            
            SqlDataAdapter adapteurProduits = new SqlDataAdapter("SELECT P.NoProduit, C.Description, P.Nom, P.PrixDemande, P.DateCreation, AVG(E.Cote) AS Evaluation FROM PPProduits P INNER JOIN PPCategories C ON C.NoCategorie = P.NoCategorie LEFT JOIN PPEvaluations E ON E.NoProduit = P.NoProduit" + whereClause + " GROUP BY P.NoProduit, P.Photo, C.Description, P.Nom, P.PrixDemande, P.NombreItems, P.DateCreation" + orderByClause, myConnection);
            if (txtCritereRecherche.Text.Trim() != string.Empty)
            {
                adapteurProduits.SelectCommand.Parameters.AddWithValue("@critere", "%" + txtCritereRecherche.Text.Trim() + "%");
            }
            DataTable tableProduits = new DataTable();
            adapteurProduits.Fill(tableProduits);

            if (tableProduits.Rows.Count > 0)
            {
                PagedDataSource objPds = new PagedDataSource();
                objPds.DataSource = new DataView(tableProduits);
                if (ddlParPage.SelectedValue != "-1")
                {
                    objPds.AllowPaging = true;
                    objPds.PageSize = int.Parse(ddlParPage.SelectedValue);
                    objPds.CurrentPageIndex = Master.PageActuelle;
                }

                Master.NbPages = objPds.PageCount;

                dtlProduits.DataSource = objPds;
                dtlProduits.DataBind();

                mvProduits.ActiveViewIndex = 0;
            }
            else
            {
                mvProduits.ActiveViewIndex = 1;
            }

            if (noVendeurs == null || noVendeurs.Split(',').Length > 1)
            {
                Master.Master.Titre = null;
            }
            else
            {
                Master.Master.NoVendeur = long.Parse(noVendeurs);
            }
        }

        protected void AfficherPremierePage(object sender, EventArgs e)
        {
            Validate();
            if (IsValid)
            {
                Master.AfficherPremierePage();
            }
        }

        protected void btnRechercheAvance_OnClick(object sender, EventArgs e)
        {
            RechercheAvance = !RechercheAvance;

            mvCategorie.ActiveViewIndex = RechercheAvance ? 1 : 0;
            mvVendeur.ActiveViewIndex = RechercheAvance ? 1 : 0;
            pnlDeJusquA.Visible = RechercheAvance;

            btnRechercheAvance.Text = RechercheAvance ? "▲" : "▼";

            if (RechercheAvance)
            {
                if (ddlVendeur.SelectedValue == "-1")
                {
                    foreach (ListItem item in cblVendeur.Items)
                    {
                        item.Selected = true;
                    }
                }
                else
                {
                    cblVendeur.ClearSelection();
                    cblVendeur.Items.FindByValue(ddlVendeur.SelectedValue).Selected = true;
                }

                if (ddlCategorie.SelectedValue == "-1")
                {
                    foreach (ListItem item in cblCategorie.Items)
                    {
                        item.Selected = true;
                    }
                }
                else
                {
                    cblCategorie.ClearSelection();
                    cblCategorie.Items.FindByValue(ddlCategorie.SelectedValue).Selected = true;
                }
            }
        }

        protected void ValiderDate(object sender, ServerValidateEventArgs e)
        {
            DateTime date;
            e.IsValid = Librairie.LireEtValiderPlage(e.Value, out date);
        }
    }
}