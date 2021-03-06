﻿using System;
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
    public partial class gerer_clients : System.Web.UI.Page
    {
        SqlConnection myConnection = Librairie.Connexion;
        string whereClause, orderByClause = " ORDER BY ";
        string[] param;
        string[] mots;
        PagedDataSource objPds = new PagedDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Librairie.Autorisation(false, false, false, true);
            }
            Master.Titre = "Gérer les clients";
            
            List<String> whereParts = new List<String>();

            if (txtCritereRecherche.Text.Trim() != string.Empty)
            {
                mots = txtCritereRecherche.Text.Trim().Split(' ');
                param = new string[mots.Length];

                for (int i = 0; i < mots.Length; i++)
                {
                    param[i] = "@mot" + i;
                    whereParts.Add("Nom" + " LIKE @mot" + i);
                    whereParts.Add("Prenom" + " LIKE @mot" + i);
                    whereParts.Add("Nom" + " LIKE @mot" + i);
                    whereParts.Add("AdresseEmail" + " LIKE @mot" + i);
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
                    orderByClause += " V.AdresseEmail ";
                    break;
                case 1:
                    orderByClause += " V.Nom ";
                    break;
                case 2:
                    orderByClause += " V.DateCreation ";
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
            string req = "SELECT * FROM PPClients V " + whereClause;

            //if ((ddlCategorie.SelectedValue != "-1") && (ddlCategorie.SelectedValue != ""))
            //    req += (txtCritereRecherche.Text == string.Empty? " WHERE " : " AND ") + " V.NoVendeur IN (SELECT NoVendeur FROM PPProduits P, PPCategories C WHERE P.NoCategorie = C.NoCategorie AND C.NoCategorie = " + ddlCategorie.SelectedValue + " GROUP BY NoVendeur) ";

            SqlDataAdapter adapteurResultats = new SqlDataAdapter(req + orderByClause, myConnection);
            for (int i = 0 ; txtCritereRecherche.Text.Trim() != string.Empty && i < mots.Length ; i++)
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
                LinkButton nom_complet = (LinkButton)item.FindControl("nom_complet");
                LinkButton adresse_courriel = (LinkButton)item.FindControl("adresse_courriel");

                DataRowView drvVendeurs = (DataRowView)e.Item.DataItem;

                lbl_num.Text = (objPds.CurrentPageIndex * objPds.PageSize + e.Item.ItemIndex + 1).ToString();
                if (drvVendeurs["Prenom"].ToString() + " " + drvVendeurs["Nom"].ToString() == " ")
                {
                    nom_complet.Text = "Non fourni";
                    nom_complet.ForeColor = System.Drawing.Color.Black;
                    nom_complet.Enabled = false;
                }
                else nom_complet.Text = drvVendeurs["Prenom"].ToString() + " " + drvVendeurs["Nom"].ToString();
                adresse_courriel.Text = drvVendeurs["AdresseEmail"].ToString();

                lbl_num.CommandArgument = drvVendeurs["NoClient"].ToString();
                nom_complet.CommandArgument = drvVendeurs["NoClient"].ToString();
                adresse_courriel.CommandArgument = drvVendeurs["NoClient"].ToString();
            }
        }

        protected void selectionner_client(object sender, CommandEventArgs e)
        {
            Session["selected_client"] = e.CommandArgument;
            Response.Redirect(Chemin.Ajouter("client.aspx", "Retour à la liste des clients"));
        }     
    }
}