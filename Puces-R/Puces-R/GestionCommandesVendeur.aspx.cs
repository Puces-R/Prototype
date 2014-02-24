using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Puces_R.Controles;

namespace Puces_R
{
    public partial class GestionCommandesVendeur : System.Web.UI.Page
    {
        SqlConnection myConnection = Librairie.Connexion;

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.ChargerItems += ChargerCommandes;
             
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    Librairie.Autorisation(false, false, true, false);
                }
                SqlDataAdapter adapteurVendeurs = new SqlDataAdapter("SELECT DISTINCT V.NoClient, (V.Nom +' ' +V.Prenom) AS NomComplet FROM PPClients V INNER JOIN PPCommandes C ON V.noClient = C.NoClient WHERE C.NoVendeur ="+Session["ID"], myConnection);
                DataTable tableVendeurs = new DataTable();
                adapteurVendeurs.Fill(tableVendeurs);

                ddlVendeur.DataSource = tableVendeurs;
                ddlVendeur.DataTextField = "NomComplet";
                ddlVendeur.DataValueField = "NoClient";
                ddlVendeur.DataBind();
                ddlVendeur.Items.Add(new ListItem("Tous", "-1") { Selected = true });

                ChargerCommandes();

                Master.AfficherPremierePage();
            }
        }

        protected void dlCommandes_OnItemDataBound(object sender, DataListItemEventArgs e)
        {
            DataListItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                BoiteCommande ctrCommande = (BoiteCommande)item.FindControl("ctrCommande");

                DataRowView drvCommande = (DataRowView)e.Item.DataItem;

                ctrCommande.NoCommande = (long)drvCommande["NoCommande"];
                ctrCommande.URL = Convert.ToString((long)drvCommande["NoCommande"]);
                ctrCommande.NoClient = (long)drvCommande["NoClient"];
                ctrCommande.Titre = drvCommande["NomComplet"] == DBNull.Value ? "Nom Inconnu" : (String)drvCommande["NomComplet"];
            }
        }

        private void ChargerCommandes(object sender, EventArgs e)
        {
            ChargerCommandes();
        }

        private void ChargerCommandes()
        {
            List<String> whereParts = new List<String>();
            whereParts.Add("WHERE V.NoVendeur = " + Session["ID"]);

            int noVendeur = int.Parse(ddlVendeur.SelectedValue);

            if (noVendeur != -1)
            {
                whereParts.Add("C.NoClient = " + noVendeur);
            }

            if (txtCritereRecherche.Text != string.Empty)
            {
                String colonne = "NomComplet";
                switch (ddlTypeRecherche.SelectedIndex)
                {
                    case 0:
                        colonne = "(CI.Nom+ ' '+ CI.Prenom)  ";
                        break;
                }
                whereParts.Add(colonne + " LIKE @critere");
            }


           String orderByClause = "";
           switch (ddlStatut.SelectedIndex)
            {
                case 1:
                    whereParts.Add("C.Statut='p'");
                    break;
                case 2:
                   whereParts.Add("C.Statut='l'");
                    break;


            }
           string whereClause = "";
            if (whereParts.Count > 0)
            {
                whereClause = string.Join(" AND ", whereParts);
            }

            SqlDataAdapter adapteurCommandes = new SqlDataAdapter("SELECT C.NoCommande ,C.NoClient , (CI.Nom+ ' '+ CI.Prenom) as NomComplet , C.Statut FROM PPCommandes C INNER JOIN PPVendeurs V ON C.NoVendeur = V.NoVendeur inner join PPClients CI on C.NoClient= CI.NoClient " + whereClause + " ORDER BY C.Statut DESC, DateCommande DESC", myConnection);
            if (txtCritereRecherche.Text != string.Empty)
            {
                adapteurCommandes.SelectCommand.Parameters.AddWithValue("@critere", "%" + txtCritereRecherche.Text + "%");
            }
            DataTable tableCommandes = new DataTable();
            adapteurCommandes.Fill(tableCommandes);

            PagedDataSource objPds = new PagedDataSource();
            objPds.DataSource = new DataView(tableCommandes);
            objPds.AllowPaging = true;
            objPds.PageSize = int.Parse(ddlParPage.SelectedValue);
            objPds.CurrentPageIndex = Master.PageActuelle;

            Master.NbPages = objPds.PageCount;

            dlCommandes.DataSource = objPds;
            dlCommandes.DataBind();

            mvCommandes.ActiveViewIndex = tableCommandes.Rows.Count == 0 ? 1 : 0;
        }

        protected void AfficherPremierePage(object sender, EventArgs e)
        {
            Master.AfficherPremierePage();
        }

    }
}