using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace Puces_R
{
    public partial class gerer_inactivite_vendeurs : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
        string req_inactif = ""; 
        string whereClause, orderByClause = " ORDER BY ";
        int anneesMaximal;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            List<String> whereParts = new List<String>();

            if (txtCritereRecherche.Text != string.Empty)
            {
                String colonne = "PPVendeurs.NomAffaires";
                switch (ddlTypeRecherche.SelectedIndex)
                {
                    case 0:
                        colonne = "PPVendeurs.NomAffaires";
                        break;
                }
                whereParts.Add(colonne + " LIKE '%" + txtCritereRecherche.Text + "%'");
            }

            whereParts.Add("PPVendeurs.NoVendeur IN ");

            if (whereParts.Count > 0)
            {
                whereClause += " WHERE " + string.Join(" AND ", whereParts);
            }

            //String orderByClause = " ORDER BY ";
            switch (ddlTrierPar.SelectedIndex)
            {
                case 0:
                    orderByClause += "PPVendeurs.NoVendeur ";
                    break;
                case 1:
                    orderByClause += "PPVendeurs.NomAffaires ";
                    break;
                case 2:
                    orderByClause += "PPVendeurs.DateCreation DESC ";
                    break;
            }
            
            anneesMaximal = int.Parse(ddlTempsInnactivite.SelectedValue);
                                   
            if (Session["err_msg"] != null)
                if (Session["err_msg"].ToString() != "")
                {
                    Response.Write(Session["err_msg"]);
                    Session["err_msg"] = "";
                }
            
            ((SiteMaster)(Master.Master)).Titre = "Gestion de l'inactivité des vendeurs";
            ((NavigationItems)Master).ChargerItems += charge_inactifs1;

            if (!IsPostBack)
            {
                ((NavigationItems)Master).AfficherPremierePage();
            } 
        }

        private void charge_inactifs1(object sender, EventArgs e)
        {
            charge_inactifs1();
        }


        private DataTable charge_inactifs1()
        {
            req_inactif = "SELECT * FROM PPVendeurs " + whereClause;
            req_inactif += "( SELECT PPVendeurs.NoVendeur ";
            req_inactif += "FROM PPVendeurs, ( ";
            req_inactif += "					SELECT PPVendeurs.NoVendeur, MAX(DATEADD(yy, " + anneesMaximal + ",PPProduits.DateCreation)) maxdate ";
            req_inactif += "					FROM PPVendeurs, PPProduits   ";
            req_inactif += "					WHERE PPVendeurs.NoVendeur = PPProduits.NoVendeur ";
            req_inactif += "					GROUP BY PPVendeurs.NoVendeur ";
            req_inactif += "				  ) R2 ";
            req_inactif += "WHERE R2.maxdate < GETDATE() "; 
            req_inactif += "AND PPVendeurs.NoVendeur = R2.NoVendeur ";
            req_inactif += "INTERSECT ";
            req_inactif += "SELECT PPVendeurs.NoVendeur ";
            req_inactif += "FROM PPVendeurs, ( ";
            req_inactif += "					SELECT PPVendeurs.NoVendeur, MAX(DATEADD(yy," + anneesMaximal + ",PPCommandes.DateCommande)) maxdate ";
            req_inactif += "					FROM PPVendeurs, PPCommandes   ";
            req_inactif += "					WHERE PPVendeurs.NoVendeur = PPCommandes.NoVendeur ";
            req_inactif += "					GROUP BY PPVendeurs.NoVendeur ";
            req_inactif += "				  ) R3 ";
            req_inactif += "WHERE R3.maxdate < GETDATE() "; 
            req_inactif += "AND PPVendeurs.NoVendeur = R3.NoVendeur ";
            req_inactif += "UNION ";
            req_inactif += "SELECT PPVendeurs.NoVendeur ";
            req_inactif += "FROM PPVendeurs, ( ";
            req_inactif += "					SELECT PPVendeurs.NoVendeur, COUNT(NoCommande) nbCommandes ";
            req_inactif += "					FROM PPVendeurs LEFT OUTER JOIN PPCommandes ";
            req_inactif += "					ON PPVendeurs.NoVendeur = PPCommandes.NoCommande ";
            req_inactif += "					GROUP BY PPVendeurs.NoVendeur ";
            req_inactif += "				  ) R5 ";
            req_inactif += "WHERE R5.nbCommandes = 0 ";
            req_inactif += "AND PPVendeurs.NoVendeur = R5.NoVendeur ";
            req_inactif += "INTERSECT ";
            req_inactif += "SELECT PPVendeurs.NoVendeur ";
            req_inactif += "FROM PPVendeurs, ( ";
            req_inactif += "					SELECT PPVendeurs.NoVendeur, COUNT(NoProduit) nbProduits ";
            req_inactif += "					FROM PPVendeurs LEFT OUTER JOIN PPProduits ";
            req_inactif += "					ON PPVendeurs.NoVendeur = PPProduits.NoVendeur ";
            req_inactif += "					GROUP BY PPVendeurs.NoVendeur ";
            req_inactif += "				  ) R4 ";
            req_inactif += "WHERE R4.nbProduits = 0 ";
            req_inactif += "AND PPVendeurs.NoVendeur = R4.NoVendeur ) AND ISNULL(Statut, 0) <> 1 " + orderByClause;
            //req_inactif += orderByClause;

            SqlDataAdapter adapteurInnactif1 = new SqlDataAdapter(req_inactif, myConnection);
            DataTable tableInnactif1 = new DataTable();
            adapteurInnactif1.Fill(tableInnactif1);

            PagedDataSource pdsDemandes = new PagedDataSource();
            pdsDemandes.DataSource = new DataView(tableInnactif1);
            pdsDemandes.AllowPaging = true;
            pdsDemandes.PageSize = int.Parse(ddlParPage.SelectedValue);

            pdsDemandes.CurrentPageIndex = ((NavigationItems)Master).PageActuelle;
            ((NavigationItems)Master).NbPages = pdsDemandes.PageCount;

            rptInnactifs1.DataSource = pdsDemandes;
            rptInnactifs1.DataBind();
            
            myConnection.Close();

            return tableInnactif1;
        }

        protected void rptInnactifs1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            DataListItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                Label titre_inactif1 = (Label)item.FindControl("titre_inactif1");
                Label addr_inactif1 = (Label)item.FindControl("addr_inactif1");
                Label tels_inactif1 = (Label)item.FindControl("tels_inactif1");
                Label courriel_inactif1 = (Label)item.FindControl("courriel_inactif1");
                Label charge_max_inactif1 = (Label)item.FindControl("charge_max_inactif1");
                Label livraison_gratuite_inactif1 = (Label)item.FindControl("livraison_gratuite_inactif1");
                Label date_inactif1 = (Label)item.FindControl("date_inactif1");
                Button btn_desactiver = (Button)item.FindControl("btn_desactiver");
                
                DataRowView drvinactif1 = (DataRowView)e.Item.DataItem;

                titre_inactif1.Text = drvinactif1["NomAffaires"].ToString() + ", par " + drvinactif1["Prenom"].ToString() + " " + drvinactif1["Nom"].ToString();
                addr_inactif1.Text = drvinactif1["Rue"].ToString() + ", " + drvinactif1["Ville"].ToString() + ", " + drvinactif1["Pays"].ToString();
                tels_inactif1.Text = drvinactif1["Tel1"].ToString();
                courriel_inactif1.Text = drvinactif1["AdresseEmail"].ToString();
                charge_max_inactif1.Text = drvinactif1["MaxLivraison"].ToString() + "Kg";
                if (drvinactif1["LivraisonGratuite"] != DBNull.Value) livraison_gratuite_inactif1.Text = drvinactif1["LivraisonGratuite"].ToString();
                date_inactif1.Text = drvinactif1["DateCreation"].ToString();
                //btnRefuser.CommandArgument = drvinactif1["AdresseEmail"].ToString();
                btn_desactiver.CommandArgument = drvinactif1["NoVendeur"].ToString();
            }
        }

        protected void desactiver_vendeur(object sender, CommandEventArgs e)
        {
            myConnection.Open();       
            using (myConnection)
            {
                SqlTransaction transaction;

                transaction = myConnection.BeginTransaction();
                  
                try
                {
                    string commande = "UPDATE PPVendeurs SET Statut = 1 WHERE NoVendeur = " + e.CommandArgument + "; ";
                    commande += "DELETE FROM PPProduits WHERE NoVendeur = " + e.CommandArgument + " AND NoProduit NOT IN (SELECT NoProduit FROM PPDetailsCommandes);";
                    commande += "UPDATE PPProduits SET Disponibilité = 0 WHERE NoVendeur = " + e.CommandArgument + " AND NoProduit IN (SELECT NoProduit FROM PPDetailsCommandes);";
                    SqlCommand commande_desactiver_vendeur = new SqlCommand(commande, myConnection, transaction);
                    commande_desactiver_vendeur.ExecuteNonQuery();
                    transaction.Commit();
                    DataTable tableProduits = charge_inactifs1();

                    rptInnactifs1.DataSource = new DataView(tableProduits);
                    rptInnactifs1.DataBind();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Session["err_msg"] = "Erreur lors de la mise à jour de la base de données: " + ex.ToString();
                    Response.Write(ex.ToString());
                }
            }           

            myConnection.Close();
        }

        protected void AfficherPremierePage(object sender, EventArgs e)
        {
            ((NavigationItems)Master).AfficherPremierePage();
        }
    }
}