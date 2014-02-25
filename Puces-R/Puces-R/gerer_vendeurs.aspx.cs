using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace Puces_R
{
    public partial class gerer_vendeurs : System.Web.UI.Page
    {
        SqlConnection myConnection = Librairie.Connexion;
        string whereClause, orderByClause = " ORDER BY ";
        string[] mots;
        string[] param;
        //private int noCategorie;
        PagedDataSource objPds = new PagedDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.Titre = "Gérer les vendeurs";
            
            if (!IsPostBack)
            {
                Librairie.Autorisation(false, false, false, true);
                String whereClause = String.Empty;
                SqlDataAdapter adapteurCategories = new SqlDataAdapter("SELECT DISTINCT C.Description, C.NoCategorie FROM PPCategories C INNER JOIN PPProduits P ON C.NoCategorie = P.NoCategorie" , myConnection);
                DataTable tableCategories = new DataTable();
                adapteurCategories.Fill(tableCategories);

                ddlCategorie.DataSource = tableCategories;
                ddlCategorie.DataTextField = "Description";
                ddlCategorie.DataValueField = "NoCategorie";
                ddlCategorie.DataBind();
                ListItem li = new ListItem("Toutes", "-1");
                li.Selected = true;
                ddlCategorie.Items.Add(li);
                //ddlCategorie.SelectedValue = noCategorie.ToString();
            }

            List<String> whereParts = new List<String>();

            if (txtCritereRecherche.Text.Trim() != string.Empty)
            {
                mots = txtCritereRecherche.Text.Trim().Split(' ');
                param = new string[mots.Length];

                for (int i = 0; i < mots.Length; i++)
                {
                    param[i] = "@mot" + i;
                    whereParts.Add("Nom" + " LIKE " + param[i]);
                    whereParts.Add("NomAffaires" + " LIKE " + param[i]);
                    whereParts.Add("Prenom" + " LIKE " + param[i]);
                    whereParts.Add("Nom" + " LIKE " + param[i]);
                    whereParts.Add("AdresseEmail" + " LIKE " + param[i]);
                }
            }

            //String whereClause;
            if (whereParts.Count > 0 )
            {
                whereClause = " WHERE (" + string.Join(" OR ", whereParts) + ") ";
                if ((datepicker3.Text != string.Empty) && (datepicker4.Text != string.Empty))
                {
                    whereClause += " AND (DateCreation < '" + datepicker4.Text + "' AND DateCreation > '" + datepicker3.Text + "') ";
                }
            }
            else
            {
                whereClause = "";
                if ((datepicker3.Text != string.Empty) && (datepicker4.Text != string.Empty))
                {
                    whereClause += " WHERE DateCreation < '" + datepicker4.Text + "' AND DateCreation > '" + datepicker3.Text + "' ";
                }
            }

            if (ddlStatut.SelectedValue != "-1")
                whereClause += (whereClause == "" ? " WHERE " : " AND " ) + "ISNULL(Statut, 0) = " + ddlStatut.SelectedValue + " ";
            
            switch (ddlTrierPar.SelectedIndex)
            {
                case 0:
                    orderByClause += "V.NomAffaires";
                    break;
                case 1:
                    orderByClause += "V.Nom";
                    break;
                case 2:
                    orderByClause += "V.DateCreation";
                    break;
                default:
                    orderByClause = "";
                    break;
            }
            orderByClause += ddlOrdre.SelectedValue;
            
            if (Session["err_msg"] != null)
                if (Session["err_msg"].ToString() != "")
                {
                    
                    Session["err_msg"] = "";
                }

            chargerResultats();

            ctrNavigation.PageChangee += changerDePage;
        }

        protected void changerDePage(object sender, EventArgs e)
        {
            chargerResultats();
        }

        private DataTable chargerResultats()
        {
            string req = "SELECT * FROM PPVendeurs V " + whereClause;

            if ((ddlCategorie.SelectedValue != "-1") && (ddlCategorie.SelectedValue != ""))
                req += (whereClause == "" ? " WHERE " : " AND ") + " V.NoVendeur IN (SELECT NoVendeur FROM PPProduits P, PPCategories C WHERE P.NoCategorie = C.NoCategorie AND C.NoCategorie = " + ddlCategorie.SelectedValue + " GROUP BY NoVendeur) ";

            SqlDataAdapter adapteurResultats = new SqlDataAdapter(req + orderByClause, myConnection);
            for (int i = 0; txtCritereRecherche.Text.Trim() != string.Empty && i < mots.Length; i++)
            {
                adapteurResultats.SelectCommand.Parameters.AddWithValue(param[i], "%" + mots[i] + "%");
            }
            DataTable tableResultats = new DataTable();
            //
            adapteurResultats.Fill(tableResultats);
            myConnection.Close();

            objPds.DataSource = new DataView(tableResultats);
            objPds.AllowPaging = true;
            objPds.PageSize = int.Parse(ddlParPage.SelectedValue);
            objPds.CurrentPageIndex = ctrNavigation.PageActuelle;

            ctrNavigation.NbPages = objPds.PageCount;

            rptVendeurs.DataSource = objPds;
            rptVendeurs.DataBind();

            if (tableResultats.Rows.Count == 0)
                no_result.Visible = true;
            else no_result.Visible = false;

            return tableResultats;
        }

        protected void rptVendeurs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                LinkButton lbl_num = (LinkButton)item.FindControl("lbl_num");
                LinkButton lbl_nom_affaire = (LinkButton)item.FindControl("lbl_nom_affaire");
                LinkButton nom_complet = (LinkButton)item.FindControl("nom_complet");

                DataRowView drvVendeurs = (DataRowView)e.Item.DataItem;

                lbl_num.Text = (objPds.CurrentPageIndex * objPds.PageSize + e.Item.ItemIndex + 1).ToString();
                lbl_nom_affaire.Text = drvVendeurs["NomAffaires"].ToString();
                nom_complet.Text = drvVendeurs["Prenom"].ToString() + " " + drvVendeurs["Nom"].ToString();

                lbl_num.CommandArgument = drvVendeurs["NoVendeur"].ToString();
                lbl_nom_affaire.CommandArgument = drvVendeurs["NoVendeur"].ToString();
                nom_complet.CommandArgument = drvVendeurs["NoVendeur"].ToString();
            }
        }

        protected void selectionner_vendeur(object sender, CommandEventArgs e)
        {
            Session["selected_vendeur"] = e.CommandArgument;
            Response.Redirect(Chemin.Ajouter("vendeur.aspx", "Retour à la liste des vendeurs"));
        }

        protected void rptVendeurs_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {

        }               
    }
}