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
    public partial class GestionProduits : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
        private int noCategorie;

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.ChargerItems += chargerProduits;

            if (!IsPostBack)
            {

                chargerProduits();
                
                SqlDataAdapter adapteurCategories = new SqlDataAdapter("SELECT DISTINCT C.Description, C.NoCategorie FROM PPCategories C INNER JOIN PPProduits P ON C.NoCategorie = P.NoCategorie AND P.NoVendeur = " + Session["ID"], myConnection);
                DataTable tableCategories = new DataTable();
                adapteurCategories.Fill(tableCategories);

                ddlCategorie.DataSource = tableCategories;
                ddlCategorie.DataTextField = "Description";
                ddlCategorie.DataValueField = "NoCategorie";
                ddlCategorie.DataBind();
                ddlCategorie.Items.Add(new ListItem("Toutes", "-1"));
                ddlCategorie.SelectedValue = noCategorie.ToString();


                Master.Master.NoVendeur = (int)(Session["ID"]);
                Master.AfficherPremierePage();

                //SqlDataAdapter adapteurProduits = new SqlDataAdapter("SELECT NoProduit,Photo,C.Description,Nom,PrixDemande,NombreItems FROM PPProduits P INNER JOIN PPCategories C ON C.NoCategorie = P.NoCategorie where P.NoVendeur=" + Session["ID"], myConnection);
                //DataTable tableProduits = new DataTable();
                //adapteurProduits.Fill(tableProduits);
                //dtlProduits.DataSource = tableProduits;
                //dtlProduits.DataBind();
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

            whereClause = String.Empty;
            whereParts.Add("P.NoVendeur = " + Session["ID"]);
            if (whereParts.Count > 0)
            {
                whereClause = " WHERE " + string.Join(" AND ", whereParts);
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
            objPds.CurrentPageIndex = Master.PageActuelle;

            Master.NbPages = objPds.PageCount;

            dtlProduits.DataSource = objPds;
            dtlProduits.DataBind();
        }

        protected void AfficherPremierePage(object sender, EventArgs e)
        {
            Master.AfficherPremierePage();
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
                    Button btnSupprimer = (Button)item.FindControl("btnSupprimer");
                    Button btnModifier = (Button)item.FindControl("btnModifier");

                    DataRowView drvProduit = (DataRowView)e.Item.DataItem;

                    long noProduit = (long)drvProduit["NoProduit"];

                    Object photo = drvProduit["Photo"];
                    String urlImage;
                    if (photo is DBNull)
                    {
                        urlImage = "Images/image_non_disponible.png";
                    }
                    else
                    {
                        urlImage = "Images/Televerse/" + (String)photo;
                    }
                    String strCategorie = (String)drvProduit["Description"];
                    String strDescriptionAbregee = (String)drvProduit["Nom"];
                    decimal decPrixDemande = (decimal)drvProduit["PrixDemande"];
                    short intQuantite = (short)drvProduit["NombreItems"];

                    lblNoProduit.Text = "No. " + noProduit.ToString();
                    imgProduit.ImageUrl = urlImage;
                    hypDescriptionAbregee.Text = strDescriptionAbregee;
                    hypDescriptionAbregee.NavigateUrl = "DetailsProduit.aspx?noproduit=" + noProduit;
                    lblCategorie.Text = strCategorie;
                    lblPrixDemande.Text = "Prix demandé: " + decPrixDemande.ToString("C");
                    lblQuantite.Text = "Quantité: " + intQuantite.ToString();

                    btnSupprimer.CommandArgument = noProduit.ToString();
                    btnModifier.CommandArgument = noProduit.ToString();
                }
                //Label lblNoProduit = (Label)item.FindControl("lblNoProduit");
                //HyperLink hypProduit = (HyperLink)item.FindControl("hypProduit");
                //Label lblCategorie = (Label)item.FindControl("lblCategorie");
                //Label lblDescriptionAbregee = (Label)item.FindControl("lblDescriptionAbregee");
                //Label lblPrixDemande = (Label)item.FindControl("lblPrixDemande");
                //Label lblQuantite = (Label)item.FindControl("lblQuantite");
                //Button btnSupprimer = (Button)item.FindControl("btnSupprimer");
                //Button btnModifier = (Button)item.FindControl("btnModifier");

                // = (DataRowView)e.Item.DataItem;

                //long noProduit = (long)drvFilm["NoProduit"];
                //String urlImage = "Images/Televerse/" + (String)drvFilm["Photo"];
                //String strCategorie = (String)drvFilm["Description"];
                //String strDescriptionAbregee = (String)drvFilm["Nom"];
                //decimal decPrixDemande = (decimal)drvFilm["PrixDemande"];
                //short intQuantite = (short)drvFilm["NombreItems"];

                //lblNoProduit.Text = "No. " + noProduit.ToString();
                //hypProduit.ImageUrl = urlImage;
                //hypProduit.NavigateUrl = "DetailsProduit.aspx?noproduit=" + noProduit;
                //lblCategorie.Text = strCategorie;
                //lblDescriptionAbregee.Text = strDescriptionAbregee;
                //lblPrixDemande.Text = "Prix demandé: " + decPrixDemande.ToString("C");
                //lblQuantite.Text = "Quantité: " + intQuantite.ToString();

                      

                //Response.Write(btnSupprimer.CommandName);
            
        }

        protected void dtlProduits_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            //TextBox txtQuantite = (TextBox)e.Item.FindControl("txtQuantite");
            String[] tableau = ((String)e.CommandArgument).Split('-');

            String Type = e.CommandName.ToString();
            //String numProduit = tableau[1];

            if (Type == "Supprimer")
            {
                Response.Redirect("SuppressionProduits.aspx?noproduit="+e.CommandArgument.ToString());
            }
            else if (Type == "Modifier") 
            {
                Response.Redirect("ModificationProduits.aspx?noproduit="+e.CommandArgument.ToString());
            }
            
        }

    }
}